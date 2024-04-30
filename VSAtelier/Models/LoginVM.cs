using System.ComponentModel.DataAnnotations;

namespace VSAtelier.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Wpisz nazwę użytkownika.")]
        [Display(Name ="Nazwa użytkownika")]
        public string?  username { get; set; }
        [Required(ErrorMessage = "Wpisz hasło.")]
        [DataType(DataType.Password)]
        [Display(Name ="Hasło")]
        public string? password { get; set; }
        [Display(Name = "Zapamiętać hasło?")]
        public bool rememberMe { get; set; }
    }
}
