using DotMessenger.Core.Model.Entities;

namespace DotMessenger.Core.Repositories
{
    public interface IAppRolesRepository
    {
        void Add(AppRole appRole);
        AppRole? FindById(int appRoleId);
        AppRole? FindByName(string name);
        AppRole? FindByAccount(int accountId);
        void Remove(int appRoleId);
    }
}