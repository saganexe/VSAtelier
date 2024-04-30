using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VSAtelier.Models.Entities
{
    public class Forum
    {
        public int Id { get; set; }
        [StringLength(100)]
        [MaxLength(100)]
        [Required]
        public string textForm { get; set; }
        public string imagePhoto { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        public IEnumerable<Forum> Forums { get; set;}
        [ForeignKey("NewForumOG")]
        public int? NewForumOGId { get; set; }
        public Forum NewForumOG { get; set; }
       
        
    }
}
