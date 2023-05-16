using DotMessenger.Core.Model.Entities;
using DotMessenger.Core.Repositories;
using DotMessenger.WebApi.Data.EntityFrameworkContexts;

namespace DotMessenger.WebApi.Data.EntityFrameworkRepositories
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