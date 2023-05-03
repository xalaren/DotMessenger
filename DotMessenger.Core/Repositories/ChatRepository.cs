using DotMessenger.Core.Model.Entities;

namespace DotMessenger.Core.Repositories
{
    public class ChatRepository : IRepository<Chat>
    {
        private List<Chat> chats = new List<Chat>();

        public void Add(Chat entity)
        {
            if (entity == null) throw new ArgumentNullException();
            chats.Add(entity);
        }

        public void Delete(Chat entity)
        {
            if (entity == null) throw new ArgumentNullException();
            chats.Remove(entity);
        }
       
        public Chat? FindById(int id)
        {
            return chats.SingleOrDefault(c => c.Id == id);
        }

        public void Update(Chat entity)
        {
            if (entity == null) throw new ArgumentNullException();

            var foundChat = FindById(entity.Id);
            if (foundChat == null) return;

            foundChat = entity;
        }

        public IEnumerable<Chat> GetAll()
        {
           foreach(var chat in chats)
            {
                yield return chat;
            }
        }
    }
}
