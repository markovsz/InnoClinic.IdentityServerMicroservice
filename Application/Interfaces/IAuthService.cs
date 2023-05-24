using Domain.Enums;
using InnoClinic.SharedModels.DTOs.Identity.Incoming;
using InnoClinic.SharedModels.DTOs.Identity.Outgoing;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<TokensOutgoingDto> LogInAsync(LoginIncomingDto incomingDto);
        Task LogOutAsync(RefreshTokenIncomingDto incomingDto);
        Task SignUpAsync(SignUpIncomingDto incomingDto, UserRoles role);
        Task<SignUpOutgoingDto> SignUpWithoutPasswordAsync(SignUpWithoutPasswordIncomingDto incomingDto, UserRoles role);
        Task ConfirmEmailAsync(string email, string confirmationToken);
        Task<RefreshedTokensOutgoingDto> GenerateAccessTokenAsync(RefreshTokenIncomingDto incomingDto);
    }
}
