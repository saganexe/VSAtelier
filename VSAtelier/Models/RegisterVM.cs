using System.ComponentModel.DataAnnotations;

namespace VSAtelier.Models
{
    public class RegisterVM
    {
        [Required (ErrorMessage ="Podaj swoją nazwę użytkownika.")]
        [Display(Name ="Nazwa użytkownika")]
        public string? username { get; set; }
        [Required]
        [Display(Name = "Imię")]
        public string? name { get; set; }
        [Display(Name = "Nazwisko")]
        public string? surname { get; set; }
        [Required(ErrorMessage = "Pole adresu e-mail jest wymagane.")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Nieprawidłowy format adresu e-mail.")]
        [Display(Name = "Email")]
        public string? email { get; set; }
        [Required (ErrorMessage ="Wpisz hasło.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$",
        ErrorMessage = "Hasło nie zgodne z normami")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string? password { get; set; }
        [Required(ErrorMessage ="Powtórz hasło.")]
        [Compare("password", ErrorMessage = "Hasła nie są identyczne.")]
        [DataType (DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        public string? confirmPassword { get; set;}
        [Required (ErrorMessage ="Podaj adres zamieszkania.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Adres zamieszkania")]
        public string? address { get; set; }
        [Required(ErrorMessage = "Podaj numer telefonu")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Numer telefonu musi składać się z 9 cyfr.")]
        [Display(Name = "Numer telefonu")]
        public string? PhoneNumber { get; set; }

    }
}
