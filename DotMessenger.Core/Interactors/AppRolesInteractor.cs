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
        }
    }
}