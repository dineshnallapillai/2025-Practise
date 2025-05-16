using DiagnosticApi.Data;
using DiagnosticApi.Middleware;
using DiagnosticApi.Services;
using Microsoft.EntityFrameworkCore;
using DiagnosticApi.Validators;
using Euphoric.FluentValidation.AspNetCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);


// Register DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDiagnosticService, DiagnosticService>();

builder.Services.AddValidatorsFromAssemblyContaining<CreatePatientDtoValidator>();
builder.Services.AddAutoFluentValidations(); // Enables automatic model validation


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MappingConfig.RegisterMappings();

app.Run();
