﻿using DotMessenger.Core.Model.Entities;
using DotMessenger.Core.Repositories;

namespace DotMessenger.Core.Interactors
{
    public class AccountInteractor
    {
        private AccountRepository accountRepository = new AccountRepository();

        /// <summary>
        /// Register new account to system and add it to database
        /// </summary>
        /// <param name="nickname"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <param name="lastname"></param>
        /// <param name="birthDate"></param>
        /// <param name="phoneNumber"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void RegisterNewAccount
            (string nickname, string password, string email,
             string name, string lastname, DateTime birthDate, string? phoneNumber = null)
        {
            var account = new Account()
            {
                Nickname = nickname,
                Password = password,
                Email = email,
                Name = name,
                Lastname = lastname,
                BirthDate = birthDate,
                PhoneNumber = phoneNumber,
                Age = GetAge(birthDate)
            };

            if (!CheckForNullStrings(nickname, password, email, name, lastname))
            {
                throw new ArgumentNullException("Unable to register account, required fields are null");
            }
            
            accountRepository.Add(account);
        }

        /// <summary>
        /// Convert to age from birth date
        /// </summary>
        /// <param name="birthDate"></param>
        /// <returns>Age integer</returns>
        public int GetAge(DateTime birthDate)
        {
            var now = DateTime.Today;
            return now.Year - birthDate.Year - 1 +
                ((now.Month > birthDate.Month || now.Month == birthDate.Month && now.Day >= birthDate.Day) ? 1 : 0);
        }

        /// <summary>
        /// Checks that string is null
        /// </summary>
        /// <param name="values">String arguments</param>
        /// <returns>True if all strings are not null, false otherwise</returns>
        public bool CheckForNullStrings(params string[] values)
        {
            foreach(var value in values)
            {
                if (string.IsNullOrWhiteSpace(value)) return false;
            }

            return true;
        }
    }
}
