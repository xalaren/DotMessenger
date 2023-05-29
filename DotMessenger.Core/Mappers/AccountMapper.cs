using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using DotMessenger.Core.Encryption;
using DotMessenger.Core.Mappers;
using DotMessenger.Core.Model.Entities;
using DotMessenger.Shared.DataTransferObjects;
using Microsoft.VisualBasic;

namespace DotMessenger.Core.Interactors.Mappers;

public static class AccountMapper
{
    public static AccountDto ToDto(this Account account)
    {
        if (account == null)
        {
            throw new ArgumentNullException();
        }

        var accountDto = new AccountDto()
        {
            Id = account.Id,
            Nickname = account.Nickname,
            Name = account.Name,
            Lastname = account.Lastname,
            Password = account.Password,
            Email = account.Email,
            Phone = account.Phone,
        };

        return accountDto;

    }

    public static Account ToEntity(this AccountDto accountDto)
    {
        if (accountDto == null)
        {
            throw new ArgumentNullException();
        }

        var account = new Account()
        {
            Id = accountDto.Id,
            Nickname = accountDto.Nickname,
            Name = accountDto.Name,
            Lastname = accountDto.Lastname,
            Password = SHA256Encryption.ComputeSha256Hash(accountDto.Password),
            Email = accountDto.Email,
            Phone = accountDto.Phone,
        };

        return account;
    }

    public static Account Assign(this Account account, AccountDto accountDto)
    {
        account.Nickname = accountDto.Nickname ?? account.Nickname;
        account.Name = accountDto.Name ?? account.Name;
        account.Lastname = accountDto.Lastname ?? account.Lastname;
        account.Password = accountDto.Password ?? account.Password;
        account.Email = accountDto.Email ?? account.Email;
        account.Phone = accountDto.Phone ?? account.Phone;

        return account;
    }
}