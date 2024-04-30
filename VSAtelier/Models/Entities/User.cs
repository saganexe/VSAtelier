using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VSAtelier.Models.Entities
{
    public class User : IdentityUser
    {
        [StringLength(100)]
        [MaxLength(100)]
        [Required]
        public string? name { get; set; }
        public string? surname { get; set; }
        [Required]
        public string? address { get; set; }

    }
}
