using DotMessenger.Core.Model.Entities;
using DotMessenger.Core.Repositories;

namespace DotMessenger.Core.Interactors
{
    public class AccountInteractor
    {
        private AccountRepository accountRepository = new AccountRepository();

        public void RegisterNewAccount
            (string nickname, string password, string email,
             string name, string lastname, DateTime birthDate, string phoneNumber)
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

            accountRepository.Add(account);
        }

        public int GetAge(DateTime birthDate)
        {
            var now = DateTime.Today;
            return now.Year - birthDate.Year - 1 +
                ((now.Month > birthDate.Month || now.Month == birthDate.Month && now.Day >= birthDate.Day) ? 1 : 0);
        }
    }
}
