﻿using Application.DTOs.Incoming;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using PasswordGenerator;

namespace Infrastructure.Services
{
    public class AccountsService : IAccountsService
    {
        private UserManager<Account> _userManager;

        public AccountsService(UserManager<Account> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Account> CreateAccountAsync(AccountIncomingDto incomingDto, UserRoles role)
        {
            if (!incomingDto.Password.Equals(incomingDto.ReEnteredPassword))
                throw new InvalidOperationException("password and reEnteredPassword don't match");

            var accountByEmail = await _userManager.FindByEmailAsync(incomingDto.Email);
            if (accountByEmail is not null)
                throw new InvalidOperationException("account with such an email already exists");

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

            var createdAccount = await _userManager.FindByIdAsync(account.Id);
            createdAccount.CreatedBy = account.Id;
            result = await _userManager.UpdateAsync(createdAccount);
            if (!result.Succeeded)
                throw new InvalidOperationException(result.Errors.AsJson());

            result = await _userManager.AddToRoleAsync(account, role.ToString());//
            if (!result.Succeeded)
                throw new InvalidOperationException(result.Errors.AsJson());

            return account;
        }
    }
}