
using Customers.Api.Database;
using Customers.Api.Repositories;
using Customers.Api.Services;
using Customers.Api.Validation;
using Dapper;
using FluentValidation.AspNetCore;
using Microsoft.Net.Http.Headers;

namespace Customers.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
            // Add services to the container.

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var config = builder.Configuration;
            config.AddEnvironmentVariables("CustomersApi_");

            builder.Services.AddControllers();
            builder.Services.AddFluentValidation(x =>
            {
                x.RegisterValidatorsFromAssemblyContaining<Program>();
                x.DisableDataAnnotationsValidation = true;
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IDbConnectionFactory>(_ => new NpgSqlConnectionFactory(config.GetValue<string>("Database:ConnectionString")));
            builder.Services.AddSingleton<DatabaseInitializer>();
            builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
            builder.Services.AddSingleton<ICustomerService, CustomerService>();
            builder.Services.AddSingleton<IGitHubService, GitHubService>();
            builder.Services.AddHttpClient("Github", httpClient =>
            {
                httpClient.BaseAddress = new Uri(config.GetValue<string>("GitHub:ApiBaseUrl"));
                httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/vnd.github.v3+json");
                httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, $"Course-{Environment.MachineName}");
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<ValidationExceptionMiddleware>();

            app.MapControllers();

            var databaseInitializer = app.Services.GetRequiredService<DatabaseInitializer>();

            databaseInitializer.InitializeAsync().GetAwaiter().GetResult();

            app.Run();
        }
    }
}
