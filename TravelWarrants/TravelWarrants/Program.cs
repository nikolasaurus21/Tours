global using Microsoft.EntityFrameworkCore;
global using TravelWarrants.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TravelWarrants.Interfaces;
using TravelWarrants.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TravelWarrantsContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionDB")));

builder.Services.AddScoped<IClientsService,ClientsService>();
builder.Services.AddScoped<IVehiclesService,VehiclesService>();
builder.Services.AddScoped<IDriversService,DriversService>();
builder.Services.AddScoped<ICompanyService,CompanyService>();
builder.Services.AddScoped<IAccountService,AccountService>();
builder.Services.AddScoped<IServicesService,ServicesService>();
builder.Services.AddScoped<IToursService,ToursService>();
builder.Services.AddScoped<IPaymentsService,PaymentsService>();
builder.Services.AddScoped<ISearchesService,SearchesService>();
builder.Services.AddScoped<IStatusesService,StatusesService>();
builder.Services.AddScoped<IReportsService,ReportsService>();
builder.Services.AddScoped<IInovicesService,InovicesService>();
builder.Services.AddScoped<IProInoviceService,ProinoviceServices>();

var configuration = builder.Configuration;
builder.Services.AddCors(options =>
{
    var frontendUrl = configuration.GetValue<string>("FrontendUrl");
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(frontendUrl)
        .AllowAnyMethod()
        .AllowAnyHeader();
        
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
