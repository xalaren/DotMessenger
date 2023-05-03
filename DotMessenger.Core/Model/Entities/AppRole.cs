using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotMessenger.Core.Model.Entities
{
    public class AppRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsBlocked { get; set; }
    }
}
