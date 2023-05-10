using DotMessenger.Core.Model.Entities;

namespace DotMessenger.Core.Repositories
{
    public class ChatRoleRepository : IRepository<ChatRole>
    {
        public List<ChatRole> ChatRoles { get; set; } = new List<ChatRole>();

        public ChatRoleRepository()
        {

        }

        public void Add(ChatRole entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(ChatRole entity)
        {
            throw new NotImplementedException();
        }

        public ChatRole? FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ChatRole> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(ChatRole entity)
        {
            throw new NotImplementedException();
        }
    }
}
