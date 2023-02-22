using Application.Interfaces;
using Domain.Entities;
using IdentityServer;
using Infrastructure;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("dbConnection"),
                b => b.MigrationsAssembly("Infrastructure")));
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
        }

        public static void ConfigureIdentityServer(this IServiceCollection services)
        {
            services.AddIdentity<Account, IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>();

            services.AddIdentityServer()
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
                    ValidAudience = identityServerConfig
                        .GetSection("Scope").Value,

                    ValidateAudience = true
                };
            });
        }

        public static void ConfigureFilters(this IServiceCollection services)
        {
            services.AddScoped<ExtractAccountIdAttribute>();
        }
    }
}
