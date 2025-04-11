
using FluentValidation.AspNetCore;
using HRM.Application;
using HRM.Application.Repositories;
using HRM.Application.Services;
using HRM.Infrastructure.RepositoryImplements;
using HRM.Infrastructure.ServiceImplements;
using HRM.Persistance;
using Microsoft.EntityFrameworkCore;

namespace HRM.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;

            // Add services to the container.
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddPersistance(builder.Configuration);
            services.AddValidators();
            services.AddFluentValidationAutoValidation();

            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IProjectEmployeeRepository, ProjectEmployeeRepository>();
            services.AddScoped<ISalaryRepository, SalaryRepository>();

            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IProjectEmployeeService, ProjectEmployeeService>();
            services.AddScoped<ISalaryService, SalaryService>();

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<HRMDbContext>();
                dbContext.Database.Migrate(); // This applies any pending migrations
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
