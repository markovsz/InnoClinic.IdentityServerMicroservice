using Application.DTOs.Incoming;
using Application.DTOs.Outgoing;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using IdentityModel.Client;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private UserManager<Account> _userManager;
        private IHttpClientFactory _httpClientFactory;
        private IConfiguration _configuration;

        public AuthService(UserManager<Account> userManager, IHttpClientFactory httpClientFactory, IConfiguration configuration) {
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

                    Parameters =
                    {
                        { "email", incomingDto.Email },
                        { "password", incomingDto.Password },
                        { "Scope", basicScope + " " + refreshTokenScope }
                    },

                });

            if (tokensResponse.IsError)
                throw new InvalidOperationException("cannot request for tokens");

            var tokens = new TokensOutgoingDto
            {
                AccessToken = tokensResponse.AccessToken,
                RefreshToken = tokensResponse.RefreshToken
            };

            return tokens;
        }

        public async Task LogOutAsync(RefreshTokenIncomingDto incomingDto)
        {
            var client = _httpClientFactory.CreateClient();

            var identityServerConfig = _configuration
                .GetSection("IdentityServer");

            var identityServerAddress = identityServerConfig
                .GetSection("Address")
                .Value;

            var clientId = identityServerConfig
                .GetSection("ClientId")
                .Value;

            var discoveryDocument = await client.GetDiscoveryDocumentAsync(identityServerAddress);
            var revokedTokenResponse = await client.RevokeTokenAsync(
                new TokenRevocationRequest
                {
                    Address = discoveryDocument.RevocationEndpoint,
                    ClientId = clientId,
                     
                    Token = incomingDto.RefreshToken
                });

            if (revokedTokenResponse.IsError)
                throw new InvalidOperationException("cannot revoke a token");
        }

        public async Task<string> SignUpAsync(SignUpIncomingDto incomingDto)
        {
            if (!incomingDto.Password.Equals(incomingDto.ReEnteredPassword))
                throw new InvalidOperationException("password and reEnteredPassword don't match");

            var account = new Account
            {
                UserName = incomingDto.Email.Split('@')[0],
                Email = incomingDto.Email,
                NormalizedEmail = incomingDto.Email.ToLower(),
                EmailConfirmed = false,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(account, incomingDto.Password);
            if (!result.Succeeded)
                throw new InvalidOperationException(result.Errors.AsJson());

            account.CreatedBy = account.Id;
            result = await _userManager.UpdateAsync(account);
            if (!result.Succeeded)
                throw new InvalidOperationException(result.Errors.AsJson());

           result = await _userManager.AddToRoleAsync(account, nameof(UserRoles.Patient));
            if (!result.Succeeded)
                throw new InvalidOperationException(result.Errors.AsJson());

            return account.Id;
        }

        public async Task<RefreshedTokensOutgoingDto> GenerateAccessTokenAsync(RefreshTokenIncomingDto incomingDto)
        {
            var client = _httpClientFactory.CreateClient();

            var identityServerConfig = _configuration
                .GetSection("IdentityServer");

            var identityServerAddress = identityServerConfig
                .GetSection("Address")
                .Value;

            var clientId = identityServerConfig
                .GetSection("ClientId")
                .Value;

            var discoveryDocument = await client.GetDiscoveryDocumentAsync(identityServerAddress);
            var refreshTokensResponse = await client.RequestRefreshTokenAsync(
                new RefreshTokenRequest
                {
                    Address = discoveryDocument.TokenEndpoint,
                    ClientId = clientId,
                    RefreshToken = incomingDto.RefreshToken,
                });

            if (refreshTokensResponse.IsError)
                throw new InvalidOperationException("cannot refresh a token");

            var refreshedTokens = new RefreshedTokensOutgoingDto
            {
                AccessToken = refreshTokensResponse.AccessToken,
                RefreshToken = refreshTokensResponse.RefreshToken
            };

            return refreshedTokens;
        }
    }
}
