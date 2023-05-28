using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotMessenger.Core.Model.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public ICollection<ChatProfile> ChatProfiles { get; set; } = new List<ChatProfile>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
