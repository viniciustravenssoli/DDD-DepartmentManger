using System.Text;
using AutoMapper;
using Domain.Entities;
using Infra.Context;
using Infra.Interfaces;
using Infra.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.DTOs;
using Services.Interfaces;
using Services.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

IConfiguration _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

builder.Services.AddScoped<IPayrollService, PayrollService>();
builder.Services.AddScoped<IPayrollRepository, PayrollRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRolesRepository, RoleRepository>();

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
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "DepartamentManager", Version = "v1" });

        // Configuração para autenticação usando Bearer token (JWT)
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "Token de autorização Bearer no formato JWT",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey
        });

        // Adicione a política de autorização
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                },
                new string[] {}
            }
        });
    });

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
