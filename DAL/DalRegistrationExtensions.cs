using DAL.Context;
using DAL.Contracts;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL;

public static class DalRegistrationExtensions
{
    public static void RegisterDalDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ContactsDbContext>(builder =>
        {
            var connectionString = configuration.GetConnectionString("Default");
            
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Connection string cannot be empty.");
            
            builder.UseMySQL(connectionString);
        });

        services.AddScoped<IContactRepository, ContactRepository>();
    }
}