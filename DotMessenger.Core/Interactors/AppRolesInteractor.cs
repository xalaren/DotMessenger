using DotMessenger.Core.Model.Entities;
using DotMessenger.Core.Repositories;

namespace DotMessenger.Core.Interactors
{
    public class AppRolesInteractor
    {
        private readonly IAppRolesRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public AppRolesInteractor(IAppRolesRepository repository, IUnitOfWork unitOfWork)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("AppRolesRepository example was null", nameof(repository));
            }

            if(unitOfWork == null)
            {
                throw new ArgumentNullException("Unit of work example was null", nameof(unitOfWork));
            }

            this.repository = repository;
            this.unitOfWork = unitOfWork;
            CreateDefaultRoles();
        }

        private void CreateDefaultRoles()
        {
            var userRole = new AppRole()
            {
                Name = "user"
            };

            var adminRole = new AppRole()
            {
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

            unitOfWork.Commit();
        }
        
    }
}