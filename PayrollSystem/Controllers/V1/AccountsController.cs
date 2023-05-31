using AutoMapper;
using DataLayer.DTO.Errors;
using DataLayer.DTO.Generic;
using DataLayer.DTO.Request;
using DataLayer.DTO.Response;
using DataLayer.Repository.Implementation;
using DataLayer.Repository.Interface;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.JwtHandler;
using System;
using System.Threading.Tasks;

namespace EWallet.API.Controllers.V1
{
    public class AccountsController : BaseController
    {
        private readonly UserManager<Employee> _userManager;
        private readonly ILogger<AccountsController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenMethod _jwtTokenMethod;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AccountsController(UserManager<Employee> userManager, 
             RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork,
             IEmployeeRepository employeeRepository,
            IJwtTokenMethod jwtTokenMethod, ILogger<AccountsController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _jwtTokenMethod = jwtTokenMethod;
            _logger = logger;
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("register-employee")]
        public async Task<ActionResult<Result<CreateEmployeeResponseDTO>>> RegisterAsync
            ([FromBody] CreateEmployeeRequestDTO model)
        {
            Result<CreateEmployeeResponseDTO> response = new Result<CreateEmployeeResponseDTO>();

            try
            {
                Employee employeeExist = await _userManager.FindByEmailAsync(model.Email);

                if (employeeExist != null)
                {
                    response.Error = new Error()
                    {
                        Code = 400,

                        Type = "Bad Request."
                    };

                    response.Message = "Email already exist.";

                    _logger.LogError("Employee registration failed. Email provided is already used.");

                    return BadRequest(response);
                }

                var cadre = await _unitOfWork.Cadres.GetByIdAsync(model.CadreId);
                if(cadre == null)
                {
                    response.Error = new Error();
                    return response;
                }
                Employee newEmployee = new Employee()
                {
                    //IdentityId = new Guid(newCustomer.Id),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Cadre = cadre,
                    DepartmentId = model.DepartmentId,
                };

                IdentityResult isCreated = await _userManager.CreateAsync(newEmployee, model.Password);

                if (!isCreated.Succeeded)
                {
                    response.Error = new Error()
                    {
                        Code = 400,

                        Type = "Bad Request."
                    };

                    response.Message = "Employee registration failed.";

                    _logger.LogError("Regsiter Employee endpoint---> Employee identity registration failed.");

                    response.Data = new CreateEmployeeResponseDTO(){
                        FirstName = newEmployee.FirstName,
                        LastName = newEmployee.LastName,
                        Email = newEmployee.Email,
                        CadreId = newEmployee.Cadre.CadreId,
                        //public DateTime CreatedDate { get; set; } = DateTime.Now;
                        DepartmentId = newEmployee.DepartmentId

                    };
                    response.IsSuccess = true;
                    return BadRequest(response);
                };



                bool roleExist = await _roleManager.RoleExistsAsync("Customer");
                
                if (roleExist)
                {
                    _logger.LogInformation($"Role customer exist. Adding role...");

                    var userRole = await _userManager.AddToRoleAsync(newEmployee, "Employee");

                    if (!userRole.Succeeded)
                    {
                        _logger.LogError("Cannot add role customer to user.");
                    }

                    _logger.LogInformation($"Role has been added to the customer account...");
                }

                string token = await _jwtTokenMethod.GenerateJwtToken(newEmployee);

                response.Data = new CreateEmployeeResponseDTO()
                {
                    FirstName = newEmployee.FirstName,
                    LastName = newEmployee.LastName,
                    Email = newEmployee.Email,
                    CadreId = newEmployee.Cadre.CadreId,
                    //public DateTime CreatedDate { get; set; } = DateTime.Now;
                    DepartmentId = newEmployee.DepartmentId,
                    Token = token
                };

                response.IsSuccess = true;

                response.Message = "Employee registration successful.";

                _logger.LogInformation("Employee registration successful.");

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");

                response.Error = new Error()
                {
                    Code = 500,
                    Type = "Bad Request."
                };

                response.Message = ex.StackTrace.ToString();

                return response;
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<Result<EmployeeLoginResponseDto>>> LoginAsync([FromBody] EmployeeLoginRequestDto model)
        {
            var response = new Result<EmployeeLoginResponseDto>();

            try
            {
                if (ModelState.IsValid)
                {
                    var existingUser = await _userManager.FindByEmailAsync(model.Email);

                    if (existingUser == null)
                    {
                        response.Error = new Error()
                        {
                            Code = 400,
                            Type = "Bad Request."
                        };

                        response.Message = "Invalid credential.";
                        _logger.LogError("Invalid Credential.");
                        return BadRequest(response);
                    };

                    var IsPasswordValid = await _userManager.CheckPasswordAsync(existingUser, model.Password);

                    if (!IsPasswordValid)
                    {
                        response.Error = new Error()
                        {
                            Code = 400,
                            Type = "Bad Request."
                        };

                        response.Message = "Invalid credential.";
                        response.Data = new EmployeeLoginResponseDto();
                        _logger.LogError("Invalid Credential.");
                        return BadRequest(response);
                    };

                    var token = await _jwtTokenMethod.GenerateJwtToken(existingUser);

                    response.IsSuccess = true;
                    response.Message = "Login successful";
                    response.Data = new EmployeeLoginResponseDto() { Token = token };
                    return Ok(response);
                }
                else
                {
                    response.Error = new Error()
                    {
                        Code = 400,
                        Type = "Bad Request."
                    };

                    response.Message = "Invalid payload.";
                    response.Data = new EmployeeLoginResponseDto();

                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");

                response.Error = new Error()
                {
                    Code = 500,
                    Type = "Server error."
                };

                response.Message = ex.StackTrace.ToString();
                return response;
            }
        }
    }
}
