using Microsoft.OpenApi.Models;
using PayrollSystem.ExtensionMethods;
using Microsoft.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using Services.BackgrundServices;
using PayrollSystem.ExtensionMethods.Seeders;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        //builder.Services.AddControllers();
        //// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        //builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddSwaggerGen();
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        var _config = configurationBuilder.Build();
        builder.Services.AddApplicationServices(_config);

        builder.Services.AddControllers();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "PayrollSystem.API", Version = "v1" });
        });

        builder.Services.ConfigureCors();

        builder.Services.AddSwaggerConfiguration();

        //services.AddModelValidation();

        builder.Services.AddAPIVersioning();

        builder.Services.AddJwtAuthentication(_config);
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.


        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PayrollSystem.API v1"));
        }

        SeedCadresandPositions.SeedCadresAndPositions(app);

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseHangfireDashboard();

        RecurringJob.AddOrUpdate<IMonthlyPayrollCalculation>(x => x.CreateMonthlyPayroll(), "0 0 25 * * *");
        //RecurringJob.AddOrUpdate<IMonthlyPayrollCalculation>(x => x.CreateMonthlyPayroll(), "10 0 * * * *");
    }
}