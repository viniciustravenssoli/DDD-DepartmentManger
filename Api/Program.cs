using AutoMapper;
using Domain.Entities;
using Infra.Context;
using Infra.Interfaces;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.DTOs;
using Services.Interfaces;
using Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

builder.Services.AddScoped<IPayrollService, PayrollService>();
builder.Services.AddScoped<IPayrollRepository, PayrollRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var AutoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeDto>().ReverseMap();
                cfg.CreateMap<Department, DepartmentDto>().ReverseMap();
                cfg.CreateMap<Payroll, PayrollDTO>().ReverseMap();
            });

builder.Services.AddSingleton(AutoMapperConfig.CreateMapper());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
