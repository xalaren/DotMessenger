using System;
using DotMessenger.Core.Model.Entities;

namespace DotMessenger.Core.Repositories
{
    public class AppRolesRepository : IRepository<AppRole>
    {
        public List<AppRole> AppRoles { get; set; } = new List<AppRole>();

        public void Add(AppRole entity)
        {
            if (entity == null) throw new ArgumentNullException("Unable to add empty app role", nameof(entity));
            AppRoles.Add(entity);
        }

        public void Delete(AppRole entity)
        {
            if (entity == null) throw new ArgumentNullException("Unable to remove an empty app role", nameof(entity));
            AppRoles.Remove(entity);
        }

        public AppRole? FindById(int id)
        {
            return AppRoles.SingleOrDefault(account => account.Id == id);
        }

        public IEnumerable<AppRole> GetAll()
        {
            foreach (var appRole in AppRoles)
            {
                yield return appRole;
            }
        }

        public void Update(AppRole entity)
        {
            if (entity == null) throw new ArgumentNullException("Unable to update to empty app role", nameof(entity));

            var foundAppRole = FindById(entity.Id);

            if (foundAppRole == null) return;

            foundAppRole = entity;
        }
    }
}
