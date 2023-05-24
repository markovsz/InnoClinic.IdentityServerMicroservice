using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using IdentityModel.Client;
using Infrastructure.Extensions;
using InnoClinic.SharedModels.DTOs.Identity.Incoming;
using InnoClinic.SharedModels.DTOs.Identity.Outgoing;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PasswordGenerator;
using System.Net;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private UserManager<Account> _userManager;
        private IAccountsService _accountsService;
        private IEmailService _emailService;
        private IPassword _passwordGenerator;
        private IHttpClientFactory _httpClientFactory;
        private IConfiguration _configuration;

        public AuthService(UserManager<Account> userManager, IAccountsService accountsService, IEmailService emailService, IPassword passwordGenerator, IHttpClientFactory httpClientFactory, IConfiguration configuration) {
            _userManager = userManager;
            _accountsService = accountsService;
            _emailService = emailService;
            _passwordGenerator = passwordGenerator;
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
                throw new InvalidOperationException(tokensResponse.Json.ToString());

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

        public async Task SignUpAsync(SignUpIncomingDto incomingDto, UserRoles role)
        {
            if (role != UserRoles.Patient)
                throw new IncorrectDataException("incorrect role");
            var account = await _accountsService.CreateAccountAsync(incomingDto, role);
            var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(account);
            var returnUrl = _configuration.GetSection("IdentityServer:ReturnUrl").Value;
            string encodedEmailToken = WebUtility.UrlEncode(emailToken);
            var confirmationPath = returnUrl + $"?email={account.Email}&token={encodedEmailToken}";
            _emailService.SendEmailConfirmation(account.Email, confirmationPath);
        }

        public async Task<SignUpOutgoingDto> SignUpWithoutPasswordAsync(SignUpWithoutPasswordIncomingDto incomingDto, UserRoles role)
        {
            SignUpIncomingDto signUpDto = new SignUpIncomingDto();
            signUpDto.Email = incomingDto.Email;
            signUpDto.Password = _passwordGenerator.Next();
            signUpDto.ReEnteredPassword = signUpDto.Password;
            signUpDto.PhotoUrl = incomingDto.PhotoUrl;
            var account = await _accountsService.CreateAccountAsync(signUpDto, role);
            _emailService.SendCredentials(account.Email, signUpDto.Password);
            var outgoingDto = new SignUpOutgoingDto()
            {
                AccountId = account.Id,
            };
            return outgoingDto;
        }

        public async Task ConfirmEmailAsync(string email, string confirmationToken)
        {
            var account = await _userManager.FindByEmailAsync(email);
            if (account is null)
                throw new IncorrectDataException("account doesnt exist");
            await _userManager.IsEmailConfirmedAsync(account);
            var result = await _userManager.ConfirmEmailAsync(account, confirmationToken);
            if (!result.Succeeded)
                throw new InvalidOperationException(result.Errors.AsJson());
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
