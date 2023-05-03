using DotMessenger.Core.Model.Entities;

namespace DotMessenger.Core.Repositories
{
    internal class MessageRepository : IRepository<Message>
    {
        private List<Message> messages = new List<Message>();

        public void Add(Message entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            messages.Add(entity);
        }

        public void Delete(Message entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            messages.Remove(entity);
        }

        public Message? FindById(int id)
        {
            return messages.SingleOrDefault(a => a.Id == id);
        }

        public IEnumerable<Message> GetAll()
        {
            foreach (var message in messages)
            {
                yield return message;
            }
        }

        public void Update(Message entity)
        {
            var foundAccount = FindById(entity.Id);
            if (foundAccount == null) throw new ArgumentNullException();

            foundAccount = entity;
        }
    }
}
