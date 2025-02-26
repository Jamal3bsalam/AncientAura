using AncientAura.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities.Community
{
    public class Post : BaseEntitiy<int>
    {
        public string? Content { get; set; }

        public string? UserId { get; set; }
        public AppUser? User { get; set; }

        public ICollection<Comment>? Comments { get; set; } = new List<Comment>();
        public ICollection<React>? Reacts { get; set; } = new List<React>();
    }
}
