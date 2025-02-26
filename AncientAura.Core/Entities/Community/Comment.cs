using AncientAura.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities.Community
{
    public class Comment : BaseEntitiy<int>
    {
        public string? Content { get; set; }

        // 1 - M Relation between User And Comment
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        public int? PostId { get; set; }
        public Post? Post { get; set; }
        public ICollection<React>? Reacts { get; set; } = new List<React>();


    }
}
