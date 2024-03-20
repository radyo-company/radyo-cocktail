using System.Reflection;
using Cocktail.Api.Configurations;
using Cocktail.Api.Interfaces;
using Cocktail.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace Cocktail.Api.Extensions;

public static class ServiceCollectionsExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        var allowCors = configuration["AllowedHosts"]?.Split(";") ?? new[] { "*" };
        services
            .AddTransient<IStartupTask, SeedDataStartupTask>()
            .ConfigureCorsPolicy(allowCors)
            .AddSwagger(configuration)
            .AddSerilog(configuration);

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["Auth0:Issuer"];
                options.Audience = configuration["Swagger:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateLifetime = true
                };
            });

        return services;
    }

    private static IServiceCollection AddSwagger( this IServiceCollection services, IConfiguration configuration )
    {
        var swaggerOption = configuration.GetSection( SwaggerOptions.Option ).Get< SwaggerOptions >();

        if ( swaggerOption == null ) return services;

        services.AddSwaggerGen( s =>
        {
            s.SupportNonNullableReferenceTypes();
            s.SwaggerDoc( swaggerOption.Version, new OpenApiInfo { Title = swaggerOption.Title, Version = swaggerOption.Version, Description = swaggerOption.Description } );
            
            if ( !swaggerOption.EnableOAuthAuthentication || string.IsNullOrEmpty( swaggerOption.AuthorizationUrl ) || string.IsNullOrEmpty( swaggerOption.TokenUrl ) ) return;

            s.AddSecurityDefinition( "oauth2", new OpenApiSecurityScheme
            {
                Description = "OAuth2.0 Auth Code with PKCE",
                Name = "oauth2",
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri( swaggerOption.AuthorizationUrl ),
                        TokenUrl = new Uri( swaggerOption.TokenUrl ),
                        Scopes = swaggerOption.ApiScopes?.Split( "," ).ToDictionary( scope => scope, scope => scope ) ?? new Dictionary< string, string >()
                    }
                }
            } );
            s.AddSecurityRequirement( new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                    },
                    swaggerOption.ApiScopes?.Split( "," ) ?? Array.Empty< string >()
                }
            } );
        } );
        return services;
    }
    
    public static IServiceCollection AddSerilog( this IServiceCollection services, IConfiguration configuration, bool toConsoleJson = true, bool toFilesJson = false )
    {
        var environment = configuration.GetValue< string >( "ASPNETCORE_ENVIRONMENT" ) ?? configuration.GetValue< string >( "DOTNET_ENVIRONMENT" );
        var assembly = Assembly.GetEntryAssembly()?.GetName();
        var logConfig = new LoggerConfiguration()
                       .Enrich.WithProperty( "Environment", environment )
                       .Enrich.WithProperty( "Version", assembly?.Version )
                       .Enrich.WithProperty( "ApplicationName", assembly?.Name?.ToLowerInvariant() )
                       .Enrich.FromLogContext()
                       .ReadFrom.Configuration( configuration );

        if ( environment == "Development" )
            logConfig.WriteTo.Console( theme: AnsiConsoleTheme.Code );

        Log.Logger = logConfig.CreateLogger();
        return services;
    }
}
