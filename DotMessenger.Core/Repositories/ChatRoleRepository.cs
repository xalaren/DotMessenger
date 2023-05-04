using DotMessenger.Core.Model.Entities;

namespace DotMessenger.Core.Repositories
{
    public class ChatRoleRepository : IRepository<ChatRole>
    {
        private List<ChatRole> chatRoles = new();

        public void Add(ChatRole entity)
        {
            if (entity == null) throw new ArgumentNullException();

            chatRoles.Add(entity);
        }

        public void Delete(ChatRole entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            chatRoles.Remove(entity);
        }

        public ChatRole? FindById(int id)
        {
            return chatRoles.SingleOrDefault(appRole => appRole.Id == id);
        }

        public IEnumerable<ChatRole> GetAll()
        {
            foreach (var chatRole in chatRoles)
            {
                yield return chatRole;
            }
        }

        public void Update(ChatRole entity)
        {
            var foundChatRole = FindById(entity.Id);
            if (foundChatRole == null) throw new ArgumentNullException();

            foundChatRole = entity;
        }
    }
}
