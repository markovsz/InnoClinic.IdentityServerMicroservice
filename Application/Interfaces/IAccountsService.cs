using Domain.Entities;
using Domain.Enums;
using InnoClinic.SharedModels.DTOs.Identity.Incoming;

namespace Application.Interfaces
{
    public interface IAccountsService
    {
        Task<Account> CreateAccountAsync(AccountIncomingDto incomingDto, UserRoles role);
        Task ChangePhotoUrl(string accountId, string photoUrl);
    }
}
