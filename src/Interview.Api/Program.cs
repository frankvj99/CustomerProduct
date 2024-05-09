using Interview.Data.AutoMapper;
using Interview.Data.Contexts;
using Interview.Data.Services;
using Interview.Data.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Interview API",
        Version = "v1"
    });

    options.EnableAnnotations();
});

builder.Services.AddDbContext<InterviewContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("InterviewDb"), sqlServerOptions =>
    {
        sqlServerOptions.EnableRetryOnFailure();
    });
});

builder.Services.AddAutoMapper(typeof(CustomerProfile));

builder.Services.TryAddScoped<ICustomerService, CustomerService>();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");

    options.DocumentTitle = "Interview API";
});

app.UseHsts();
app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

using var serviceScope = app.Services.CreateScope();

var context = serviceScope.ServiceProvider.GetRequiredService<InterviewContext>();

context.Database.Migrate();

app.Run();

public partial class Program
{
}
