using DotMessenger.Core.Model.Entities;
using DotMessenger.Core.Repositories;

namespace DotMessenger.Core.Tests
{
    [TestFixture]
    public class AccountRepositoryTests
    {
        AccountRepository accountRepository;

        [SetUp]
        public void Setup()
        {
            accountRepository = new AccountRepository();
        }

        [Test]
        public void Update_Test()
        {
            var account = new Account
            {
                Id = 1,
                Name = "TestName",
            };

            accountRepository.Add(account);

            string name = "ChangedName";
            account.Name = name;

            accountRepository.Update(account);
            
            var actual = accountRepository.FindById(account.Id).Name;

            Assert.IsNotNull(actual);
            Assert.That(account.Name, Is.EqualTo(actual));
        }


    }
}
