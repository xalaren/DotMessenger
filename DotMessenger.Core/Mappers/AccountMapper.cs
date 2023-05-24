using DotMessenger.Core.Model.Entities;
using DotMessenger.Shared.DataTransferObjects;

namespace DotMessenger.Core.Interactors.Mappers;

public static class AccountMapper
{
    public static AccountDto ToDto(this Account account)
    {
        if (account == null)
        {
            throw new ArgumentNullException();
        }

        return new AccountDto()
        {
            Id = account.Id,
            Nickname = account.Nickname,
            Name = account.Name,
            Lastname = account.Lastname,
            Password = account.Password,
            Email = account.Email,
            Phone = account.Phone,
            BirthDate = account.BirthDate,
            Age = account.Age
        };
    }

    public static Account ToEntity(this AccountDto accountDto)
    {
        if (accountDto == null)
        {
            throw new ArgumentNullException();
        }

        return new Account()
        {
            Id = accountDto.Id,
            Nickname = accountDto.Nickname,
            Name = accountDto.Name,
            Lastname = accountDto.Lastname,
            Password = accountDto.Password,
            Email = accountDto.Email,
            Phone = accountDto.Phone,
            BirthDate = accountDto.BirthDate,
            Age = accountDto.Age
        };
    }

    public static Account Assign(this Account account, AccountDto accountDto)
    {
        account.Nickname = accountDto.Nickname;
        account.Name = accountDto.Name;
        account.Lastname = accountDto.Lastname;
        account.Password = accountDto.Password;
        account.Email = accountDto.Email;
        account.Phone = accountDto.Phone;
        account.BirthDate = accountDto.BirthDate;
        account.Age = accountDto.Age;
    }
}