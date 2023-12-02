using Driver_WebAPI.Interfaces;
using Driver_WebAPI.Middlewares;
using Driver_WebAPI.Models;
using Driver_WebAPI.Repository;
using Driver_WebAPI.Services;
using Driver_WebAPI.Validators;
using FluentValidation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add serilog service
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

//Add SqlLite DB service
builder.Services.AddScoped<DriverDbContext>(_ => new DriverDbContext(builder.Configuration.GetConnectionString("DefaultConnection")!,
    builder.Configuration.GetValue<string>("DbScript")!));

//Add Services
builder.Services.AddScoped<IDriverRepository<Driver>,DriverRepository<Driver>>();
builder.Services.AddScoped<IDriverService,DriverService>();

//Add AutoMapper service
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Add Validators
builder.Services.AddValidatorsFromAssemblyContaining<DriverDtoValidators>();

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

//Add Exception Middleware
app.UseMiddleware<ExceptionMiddlewareHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
