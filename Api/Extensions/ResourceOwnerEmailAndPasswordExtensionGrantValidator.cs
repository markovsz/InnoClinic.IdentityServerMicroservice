using Api.Helpers;
using Domain.Entities;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using static IdentityModel.OidcConstants;

namespace Api.Extensions
{
    public class ResourceOwnerEmailAndPasswordExtensionGrantValidator : IExtensionGrantValidator
    {
        private readonly SignInManager<Account> _signInManager;
        private readonly UserManager<Account> _userManager;

        public ResourceOwnerEmailAndPasswordExtensionGrantValidator(UserManager<Account> userManager, SignInManager<Account> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string GrantType => IdentityConfiguration.ResourceOwnerEmailAndPassword;

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var email = context.Request.Raw.Get(StandardScopes.Email);
            var password = context.Request.Raw.Get(TokenRequest.Password);

            if(email is not null && password is not null)
            {
                var account = await _userManager.FindByEmailAsync(email);
                var isValid = await _signInManager.UserManager.CheckPasswordAsync(account, password);
                if (isValid)
                {
                    context.Result = new GrantValidationResult(account.Id, AuthenticationMethods.Password);
                    return;
                }
            }
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
        }
    }
}
