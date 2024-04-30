namespace VSAtelier.Models
{
    public class AddUserViewModel
    {
        public Guid? id { get; set; }
        public string? name { get; set; }
        public string? surname { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }

        public string? phoneNumber { get; set; }

        public bool? isAdmin { get; set; }
    }
}
