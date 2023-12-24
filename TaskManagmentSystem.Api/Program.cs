using Microsoft.EntityFrameworkCore;
using Serilog;
using TaskManagmentSystem.Application;
using TaskManagmentSystem.Application.Interfaces;
using TaskManagmentSystem.Application.Mapper;
using TaskManagmentSystem.Application.Services;
using TaskManagmentSystem.Domain.Interfaces;
using TaskManagmentSystem.Domain.Interfaces.Repositories;
using TaskManagmentSystem.Infrastructure;
using TaskManagmentSystem.Infrastructure.Repositories;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/task_managment_system_info.txt", rollingInterval: RollingInterval.Day) // create log file daily
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Configuration
  .SetBasePath(Directory.GetCurrentDirectory())
  .AddJsonFile("appsettings.json", false, true)
  .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", false, true)
  .AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// I prefer the following way to access database but I use the onConfigure way becasue I get some errors with this way.
#region DataBase Context
var connectionString = builder.Configuration.GetConnectionString("TaskManagmentDB");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<ApplicationDbContext>();
#endregion

builder.Services.AddCors();

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));

builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
