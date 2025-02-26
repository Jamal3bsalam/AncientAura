using AncientAura.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities.Community
{
    public class React : BaseEntitiy<int>
    {
        public string? Type { get; set; } // "Like", "Love", "Angry", etc.
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        public int? PostId { get; set; }
        public Post? Post { get; set; }
        public int? CommentId { get; set; }
        public Comment? Comment { get; set; }
    }
}
