//using EWallet.DataLayer.AutoMapper;
//using EWallet.DataLayer.Contracts;
//using EWallet.DataLayer.Data;
//using EWallet.DataLayer.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Entities.Models;
using DataLayer;
using Services.HttpContex;
using DataLayer.AutoMapper;
using Services.BackgrundServices;
using Hangfire;
using Services.PayrollService;
using DataLayer.Repository.Interface;
using DataLayer.Repository.Implementation;
using DataAccess.Repositories;
//using EWallet.Utility.HttpContex;
//using EWallet.API.AsyncDataTransfer;
//using Hangfire;
//using EWallet.API.BgImplementation;
//using DinkToPdf.Contracts;
//using DinkToPdf;
//using EWallet.API.Filters;

namespace PayrollSystem.ExtensionMethods
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration _config)
        {
            var defcon = _config.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(_config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ICadreRepository, CadreRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IHousingAllowanceRepository, HousingAllowanceRepository>();
            services.AddScoped<IPensionRepository, PensionRepository>();
            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<ITaxRepository, TaxRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IBasicSalaryRepository, BasicSalaryRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();


            services.AddHangfire(config => config
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(_config.GetConnectionString("DefaultConnection"))
            );

            //services.AddMvc(options => options.Filters.Add<ValidationFilter>());

            //            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            //            services.AddHangfireServer();

            // services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);


            //            services.AddScoped<ModelValidationAttributeFilter>();

            services.AddTransient<IMonthlyPayrollCalculation, MonthlyPayrollCalculation>();


            services.AddScoped<IPayrollService, PayrollService>();

            services.AddScoped<IUserContext, UserContext>();

            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

            //            //services.AddSingleton<IMessageBusClient, MessageBusClient>();
            //            services.AddScoped<IMessageBusClient, MessageBusClient>();


            services.AddIdentity<Employee, IdentityRole>(option =>
            {
                option.SignIn.RequireConfirmedAccount = true;
                //option.SignIn.RequireConfirmedEmail = true;

            })
                    .AddEntityFrameworkStores<AppDbContext>();
                    //.AddDefaultTokenProviders();
            return services;
        }
    }
}
