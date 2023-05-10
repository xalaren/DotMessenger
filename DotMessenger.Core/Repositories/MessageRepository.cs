using DotMessenger.Core.Model.Entities;

namespace DotMessenger.Core.Repositories
{
    public class MessageRepository : IRepository<Message>
    {
        public List<Message> Messages { get; set; } = new List<Message>();

        public void Add(Message entity)
        {
            if (entity == null) throw new ArgumentNullException("Unable to add empty message", nameof(entity));
            Messages.Add(entity);
        }

        public void Delete(Message entity)
        {
            if (entity == null) throw new ArgumentNullException("Unable to remove an empty message", nameof(entity));
            Messages.Remove(entity);
        }

        public Message? FindById(int id)
        {
            return Messages.SingleOrDefault(message => message.Id == id);
        }

        public IEnumerable<Message> GetAll()
        {
            foreach(var message in Messages)
            {
                yield return message;
            }
        }

        public void Update(Message entity)
        {
            if (entity == null) throw new ArgumentNullException("Unable to update to empty message", nameof(entity));

            var foundMessage = FindById(entity.Id);

            if (foundMessage == null) return;

            foundMessage = entity;
        }
    }
}
