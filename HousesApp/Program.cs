using HousesApp.Db;
using HousesApp.Mapping;
using HousesApp.Repository;
using HousesApp.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IHouseRepository, HouseRepository>();
builder.Services.AddAutoMapper(typeof(HouseMappingProfile));
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HousesDb>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

Log.Logger = new LoggerConfiguration().MinimumLevel.Error().WriteTo.File("/log.txt",rollingInterval:RollingInterval.Month).CreateLogger();
builder.Host.UseSerilog();
builder.Services.AddControllers().AddNewtonsoftJson();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();




app.Run();
