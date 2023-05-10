using DotMessenger.Core.Model.Entities;

namespace DotMessenger.Core.Repositories
{
    public class ChatRepository : IRepository<Chat>
    {
        public List<Chat> Chats { get; set; } = new List<Chat>();
        public void Add(Chat entity)
        {
            if (entity == null) throw new ArgumentNullException("Unable to add empty chat", nameof(entity));
            Chats.Add(entity);
        }

        public void Delete(Chat entity)
        {
            if (entity == null) throw new ArgumentNullException("Unable to remove an empty chat", nameof(entity));
            Chats.Remove(entity);
        }

        public Chat? FindById(int id)
        {
            return Chats.SingleOrDefault(account => account.Id == id);
        }

        public IEnumerable<Chat> GetAll()
        {
            foreach (var chat in Chats)
            {
                yield return chat;
            }
        }

        public void Update(Chat entity)
        {
            if (entity == null) throw new ArgumentNullException("Unable to update to empty chat", nameof(entity));

            var foundChat = FindById(entity.Id);

            if (foundChat == null) return;

            foundChat = entity;
        }
    }
}
