using DotMessenger.Adapter.EntityFrameworkContexts;
using DotMessenger.Core.Model.Entities;
using DotMessenger.Core.Repositories;

namespace DotMessenger.Adapter.EntityFrameworkRepositories
{
    public class AppRolesRepository : IAppRolesRepository
    {
        private readonly AppDbContext context;

        public AppRolesRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void Add(AppRole appRole)
        {
            context.AppRoles.Add(appRole);
        }

        public AppRole? FindByAccount(int accountId)
        {
            var account = context.Accounts.Find(accountId);

            if(account == null)
            {
                return null;
            }

            context.Entry(account)
                .Reference(account => account.AppRole)
                .Load();

            return account.AppRole;
        }

        public AppRole? FindById(int appRoleId)
        {
            return context.AppRoles.Find(appRoleId);
        }

        public AppRole? FindByName(string name)
        {
            return context.AppRoles.SingleOrDefault(appRole => string.Equals(appRole.Name, name));
        }

        public void Remove(int appRoleId)
        {
            var appRole = context.AppRoles.Find(appRoleId);

            if (appRole != null)
            {
                context.AppRoles.Remove(appRole);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}