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
using Services.HttpContex;
using Services.JwtHandler;
using Services.PayrollService;
using System;
using System.Threading.Tasks;

namespace PayrollSystem.Controllers.V1
{
    public class PayrollController : BaseController
    {
        private readonly UserManager<Employee> _userManager;
        private readonly ILogger<AccountsController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenMethod _jwtTokenMethod;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        private readonly IPayrollService _payrollService;
        public PayrollController(UserManager<Employee> userManager, 
             RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork,
             IEmployeeRepository employeeRepository, IUserContext userContext,
            IJwtTokenMethod jwtTokenMethod, ILogger<AccountsController> logger,
            IPayrollService payrollService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _jwtTokenMethod = jwtTokenMethod;
            _logger = logger;
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _payrollService = payrollService;
        }

        [HttpGet]
        [Route("employee/getpayroll")]
        public async Task<ActionResult<Result<CreateEmployeeResponseDTO>>> GetPayroll
            ([FromQuery] PayrollRequestDto model)
        {
            Result<PayrollResponseDto> response = new Result<PayrollResponseDto>();

            try
            {

                //var loggedInUserId = new Guid(_userContext.User.Claims.ToList()
                //    .FirstOrDefault(x => x.Type == "Id").Value);

                var loggedInUserId = _userContext.User.Claims.ToList()
                    .FirstOrDefault(x => x.Type == "Id").Value;

                if (string.IsNullOrEmpty(loggedInUserId.ToString()))
                {
                    response.Error = new Error()
                    {
                        Code = 400,
                        Type = "Bad Request."
                    };

                    response.Message = "Invalid user.";

                    _logger.LogError("Invalid user.");

                    return BadRequest(response);
                }

                _logger.LogInformation($"Loggedin user identity id : {loggedInUserId}.");

                var employeepayroll = _payrollService.getPayrollForMonthAandYear(int.Parse(loggedInUserId), model.monthindex, model.year);

                response.IsSuccess = true;
                response.Data = employeepayroll;

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

                return BadRequest(response);
            }
        }

    }
}
