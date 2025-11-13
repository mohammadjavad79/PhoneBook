using Application;
using Application.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PhoneBookDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped<IPhoneBookApplicationService, PhoneBookApplicationService>();
builder.Services.AddScoped<IPhoneBookRepository, PhoneBookRepository>();

var app = builder.Build();

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
