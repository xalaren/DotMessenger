using DotMessenger.Core.Repositories;
using DotMessenger.WebApi.Data.EntityFrameworkContexts;

namespace DotMessenger.WebApi.Data.EntityFrameworkRepositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            context.SaveChanges();
        }
    }
}
