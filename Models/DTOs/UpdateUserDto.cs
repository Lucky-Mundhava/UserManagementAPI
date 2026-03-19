using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.Models.DTOs
{
    public class UpdateUserDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string Name { get; set; }
        
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address format.")]
        public string Email { get; set; }
        
        [Required]
        [RegularExpression("^(Admin|HR|Employee)$", ErrorMessage = "Role must be 'Admin', 'HR', or 'Employee'.")]
        public string Role { get; set; }
    }
}
