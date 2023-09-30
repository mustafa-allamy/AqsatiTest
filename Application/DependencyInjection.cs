using Application.Behaviors;
using Application.Localization;
using Application.Services;
using Common.Dto;
using Common.Forms;
using Domain.Entities.UserAndPermissions;
using FluentValidation;
using Infrastructure.Services;
using LazZiya.ExpressLocalization;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Reflection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //Mediator
        services.AddMediator(options =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;
        });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        //Fluent Validation
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        //Mapster
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Default.IgnoreNullValues(true).PreserveReference(true);
        Assembly applicationAssemblyDtos = typeof(BaseDto<,>).Assembly;
        typeAdapterConfig.Scan(applicationAssemblyDtos);

        Assembly applicationAssemblyForms = typeof(BaseForm<,>).Assembly;
        typeAdapterConfig.Scan(applicationAssemblyForms);

        services.AddHttpContextAccessor();
        services.AddTransient<ITokenService, TokenService>();

        services.AddScoped<UserManager<User>>();
        services.AddScoped<SignInManager<User>>();
        services.AddIdentityCore<User>().AddDefaultTokenProviders();

        return services;

    }
    public static void AddGlobalization(this IServiceCollection services)
    {

        var cultures = new List<CultureInfo> {
            new("en-US"),
            new("ar-AR")
        };
        services.AddMvc()
            .AddExpressLocalization<SharedResource>(
                ops =>
                {
                    ops.ResourcesPath = nameof(Localization);
                    ops.RequestLocalizationOptions = o =>
                    {
                        o.SupportedCultures = cultures;
                        o.SupportedUICultures = cultures;
                        o.DefaultRequestCulture = new RequestCulture("ar-AR");
                    };
                });
    }

}