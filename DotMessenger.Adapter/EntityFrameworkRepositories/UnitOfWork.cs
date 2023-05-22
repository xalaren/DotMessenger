using DotMessenger.Adapter.EntityFrameworkContexts;
using DotMessenger.Core.Repositories;

namespace DotMessenger.Adapter.EntityFrameworkRepositories
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
