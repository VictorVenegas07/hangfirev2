using Hangfire;
using HangFireApi.HangfireJobs;
using HangFireApi.Infraestructure;
using HangFireApi.Seed;
using HangFireApi.Service;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
ContextExtension.AddMongoContext(builder.Services, builder.Configuration);
HangfireExtension.AddHangfire(builder.Services, builder.Configuration);

var app = builder.Build();

HangfireExtension.UseHangfire(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

EmpleadoSeed.GetEmpleados(app.Services.GetRequiredService<IMongoClient>()).Wait();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

HangfireJob.RegisterJobs();

app.Run();
