using Application.DTOs.Incoming;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces
{
    public interface IAccountsService
    {
        Task<Account> CreateAccountAsync(AccountIncomingDto incomingDto, UserRoles role);
        Task ChangePhotoUrl(string accountId, string photoUrl);
    }
}
