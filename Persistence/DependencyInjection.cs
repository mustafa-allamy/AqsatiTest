using Domain.Entities.UserAndPermissions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Local")));
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.ConfigureIdentity();
        return services;

    }
    public static void ConfigureIdentity(this IServiceCollection services)
    {

        var builder = services.AddIdentityCore<User>(x =>
        {
            x.User.RequireUniqueEmail = false;
            x.Password.RequireUppercase = true;
            x.Password.RequireLowercase = true;
            x.Password.RequireDigit = true;
            x.Password.RequireNonAlphanumeric = false;
        });

        builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole<int>), services);
        builder.AddEntityFrameworkStores<ApplicationDbContext>();


    }
}