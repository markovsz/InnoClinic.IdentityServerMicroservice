using Application.DTOs.Incoming;
using Application.DTOs.Outgoing;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<TokensOutgoingDto> LogInAsync(LoginIncomingDto incomingDto);
        Task LogOutAsync(RefreshTokenIncomingDto incomingDto);
        Task<string> SignUpAsync(SignUpIncomingDto incomingDto);
        Task<RefreshedTokensOutgoingDto> GenerateAccessTokenAsync(RefreshTokenIncomingDto incomingDto);
    }
}
