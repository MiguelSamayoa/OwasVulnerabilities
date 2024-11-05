using System.ComponentModel.DataAnnotations;

namespace Owasp.Models
{
    public class User {
        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; } // Almacenar contraseñas en texto plano

        [Required]
        public UserRole Role { get; set; } // Usaremos un enum para Role

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
    }

    public enum UserRole {
        Admin = 1,
        Customer = 2
    }
}
