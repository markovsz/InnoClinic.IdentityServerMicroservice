using Application.DTOs.Incoming;
using Application.DTOs.Outgoing;
using Application.Interfaces;
using Domain.Entities;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private SignInManager<Account> _signInManager;
        private UserManager<Account> _userManager;
        private IHttpClientFactory _httpClientFactory;
        private IConfiguration _configuration;

        public AuthService(UserManager<Account> userManager, SignInManager<Account> signInManager, IHttpClientFactory httpClientFactory, IConfiguration configuration) {
            _signInManager = signInManager;
            _userManager = userManager;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<TokensOutgoingDto> LogInAsync(LoginIncomingDto incomingDto)
        {
            var client = _httpClientFactory.CreateClient();

            var identityServerConfig = _configuration
                .GetSection("IdentityServer");

            var identityServerAddress = identityServerConfig
                .GetSection("Address")
                .Value;

            var grantType = identityServerConfig
                        .GetSection("GrantType")
                        .Value;
            var clientId = identityServerConfig
                        .GetSection("ClientId")
                        .Value;
            var scopes = identityServerConfig
                .GetSection("Scopes");

            var basicScope = scopes
                .GetSection("Basic")
                        .Value;
            var refreshTokenScope = scopes
                .GetSection("RefreshToken")
                .Value;

            var discoveryDocument = await client.GetDiscoveryDocumentAsync(identityServerAddress);
            var tokensResponse = await client.RequestTokenAsync(
                new TokenRequest
                {
                    Address = discoveryDocument.TokenEndpoint,
                    GrantType = grantType,
                    ClientId = clientId,
                    Scope = scope,

                    Parameters =
                    {
                        { "email", incomingDto.Email },
                        { "password", incomingDto.Password },
                        { "Scope", basicScope + " " + refreshTokenScope }
                    },

                });

            var tokens = new TokensOutgoingDto
            {
                AccessToken = tokensResponse.AccessToken,
                RefreshToken = tokensResponse.RefreshToken
            };

            return tokens;
        }

        [Authorize]
        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
            //var account = await _userManager.FindByNameAsync(incomingDto.UserName);
            //await _signInManager.SignOutAsync(account);
            //
            //var client = _httpClientFactory.CreateClient();
            //
            //var discoveryDocument = await client.GetDiscoveryDocumentAsync("https://localhost:7239");
            //var tokensResponse = await client.RequestClientCredentialsTokenAsync(
            //    new ClientCredentialsTokenRequest
            //    {
            //        Address = discoveryDocument.TokenEndpoint,
            //
            //        ClientId = "Profiles.Client",
            //        ClientSecret = "vfdsf&566efcn@!c_=",
            //
            //        Scope = "ProfilesAPI"
            //    });
            //
            //
            //
            //return tokens;
        }
    }
}
