using Employee_Management_Microservice.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Employee_Management_Microservice.Repositories;
using Employee_Management_Microservice.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure DbContext to use the local database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register controllers
builder.Services.AddControllers();

// Register Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Register services
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enforce HTTPS
app.UseHttpsRedirection();

// Add authorization middleware
app.UseAuthorization();

// Map controller endpoints
app.MapControllers();

// Run the application
app.Run();