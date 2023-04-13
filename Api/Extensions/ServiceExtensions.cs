using Api.FilterAttributes;
using Application.Interfaces;
using Domain.Entities;
using IdentityServer;
using Infrastructure;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PasswordGenerator;

namespace Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthDbContext>(options => options.ConfigureDb(configuration));
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAccountsService, AccountsService>();
            services.AddScoped<IEmailService, EmailService>();
        }

        public static void ConfigureIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<Account, IdentityRole>(config =>
            {
                config.Password.RequireDigit = true;
                config.Password.RequireLowercase = true;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = true;
                config.Password.RequiredLength = 6;
            })
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddOperationalStore(config => {
                    config.ConfigureDbContext =
                        builder => builder.ConfigureDb(configuration);
                })
                .AddAspNetIdentity<Account>()
                .AddInMemoryClients(Config.Clients)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryApiResources(Config.ApiResources)
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddExtensionGrantValidator<ResourceOwnerEmailAndPasswordExtensionGrantValidator>()
                .AddDeveloperSigningCredential()
                .AddProfileService<ProfileService>();
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var identityServerConfig = configuration
                        .GetSection("IdentityServer");

            var scopes = identityServerConfig
                        .GetSection("Scopes");

            services.AddAuthentication(config =>
            config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, config =>
            {
                config.Authority = identityServerConfig
                    .GetSection("Address").Value;
                config.Audience = identityServerConfig
                    .GetSection("Audience").Value;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = identityServerConfig
                        .GetSection("Address").Value,
                    ValidateIssuer = true,
                    ValidAudience = scopes.GetSection("Basic").Value,

                    ValidateAudience = true
                };
            });
        }

        public static void ConfigureFilters(this IServiceCollection services)
        {
            services.AddScoped<ExtractAccountIdAttribute>();
        }

        public static void ConfigurePasswordGenerator(this IServiceCollection services, IConfiguration configuration)
        {
            var includeNumeric = Boolean.Parse(configuration.GetSection("PasswordGenerator:IncludeNumeric").Value);
            var includeLowercase = Boolean.Parse(configuration.GetSection("PasswordGenerator:IncludeLowercase").Value);
            var includeUppercase = Boolean.Parse(configuration.GetSection("PasswordGenerator:IncludeUppercase").Value);
            var includeSpecial = Boolean.Parse(configuration.GetSection("PasswordGenerator:IncludeSpecial").Value);
            var length = Int32.Parse(configuration.GetSection("PasswordGenerator:Length").Value);
            var settings = new PasswordSettings(includeLowercase, includeUppercase, includeNumeric, includeSpecial, length, 1, false);
            services.AddSingleton(settings);
            services.AddSingleton<IPassword, Password>();
        }
    }
}
