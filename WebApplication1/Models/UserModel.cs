using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class UserModel
    {
        public int EmployeeId { get; set; }

        [Display(Name = "Full Name")]
        public string? FullName { get; set; }

        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "Address Line")]
        public string? AddressLine { get; set; }

        [Display(Name = "City")]
        public string? City { get; set; }
    }
}
