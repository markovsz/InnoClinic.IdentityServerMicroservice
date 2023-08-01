using Domain.Entities;
using Domain.Enums;
using InnoClinic.SharedModels.DTOs.Identity.Incoming;
using InnoClinic.SharedModels.DTOs.Identity.Outgoing;

namespace Application.Interfaces
{
    public interface IAccountsService
    {
        Task<Account> CreateAccountAsync(AccountIncomingDto incomingDto, UserRoles role);
        Task<AccountOutgoingDto> GetAccountAsync(string accountId);
        Task ChangePhotoUrl(string accountId, string photoUrl);
    }
}
