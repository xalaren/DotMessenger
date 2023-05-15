using DotMessenger.Core.Model.Entities;

namespace DotMessenger.Core.Repositories
{
    public interface IAppRolesRepository
    {
        void Add(AppRole appRole);
        AppRole? FindById(int appRoleId);
        AppRole? FindByName(string name);
        void Remove(int appRoleId);
        void Save();
    }
}