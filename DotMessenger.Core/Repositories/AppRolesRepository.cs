using DotMessenger.Core.Model.Entities;

namespace DotMessenger.Core.Repositories
{
    public class AppRolesRepository : IRepository<AppRole>
    {
        private List<AppRole> appRoles = new();

        public void Add(AppRole entity)
        {
            if (entity == null) throw new ArgumentNullException();

            appRoles.Add(entity);
        }

        public void Delete(AppRole entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            appRoles.Remove(entity);
        }

        public AppRole? FindById(int id)
        {
            return appRoles.SingleOrDefault(appRole => appRole.Id == id);
        }

        public IEnumerable<AppRole> GetAll()
        {
            foreach(var role in appRoles)
            {
                yield return role;
            }
        }

        public void Update(AppRole entity)
        {
            var foundAppRole = FindById(entity.Id);
            if (foundAppRole == null) throw new ArgumentNullException();

            foundAppRole = entity;
        }
    }
}
