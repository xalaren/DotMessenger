using DotMessenger.Core.Model.Entities;
using DotMessenger.Core.Repositories;

namespace DotMessenger.Core.Interactors
{
    public class AppRolesInteractor
    {
        private readonly IAppRolesRepository repository;

        public AppRolesInteractor(IAppRolesRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("AppRolesRepository example was null", nameof(repository));
            }
            
            this.repository = repository;
            CreateDefaultRoles();
        }

        private void CreateDefaultRoles()
        {
            var userRole = new AppRole()
            {
                Id = 1,
                Name = "user"
            };

            var adminRole = new AppRole()
            {
                Id = 2,
                Name = "admin"
            };

            if (repository.FindByName(userRole.Name) == null)
            {
                 repository.Add(userRole);
            }

            if (repository.FindByName(adminRole.Name) == null)
            {
                repository.Add(adminRole);
            }
        }
    }
}