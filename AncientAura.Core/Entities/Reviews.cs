using AncientAura.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities
{
    public class Reviews : BaseEntitiy<int>
    {
        public int? Rating { get; set; } // 1 - 5 scale
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? AncientSitesId { get; set; }
        public AncientSites? AncientSites { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser  { get; set; }
        public int? BooksId { get; set; }
        public Books? Books { get; set; }
        public int? DocumentriesId { get; set; }
        public Documentries? Documentries { get; set; }
        public int? ArticlesId { get; set; }
        public Articles? Articles { get; set; }
    }
}
