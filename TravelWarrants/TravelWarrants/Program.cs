global using Microsoft.EntityFrameworkCore;
global using TravelWarrants.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TravelWarrantsContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionDB")));

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetService<IConfiguration>();


builder.Services.AddCors(options =>
{
    var FrontendUrl = configuration.GetValue<string>("FrontendUrl");
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(FrontendUrl).AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
