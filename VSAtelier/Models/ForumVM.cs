using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using VSAtelier.Models.Entities;

namespace VSAtelier.Models
{
    public class ForumVM
    {
        public int Id { get; set; }

        [StringLength(100)]
       [MaxLength(100)]
       [Required]
        
        public string textForm { get; set; }                
        public IFormFile imagePhoto { get; set; }



        
    }
}