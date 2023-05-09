using DotMessenger.Core.Model.Abstract;

namespace DotMessenger.Core.Model.Entities
{
    public class UserList
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public string ListType { get; set; } = null!;

        public ICollection<IPerson> People { get; set; } = new List<IPerson>(); 
    }
}
