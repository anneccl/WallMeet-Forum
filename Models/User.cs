using System.ComponentModel.DataAnnotations;

namespace TP5.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Le prénom doit contenir entre 1 et 50 caractères.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Le nom doit contenir entre 1 et 50 caractères.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le courriel est obligatoire.")]
        [EmailAddress(ErrorMessage = "Format de courriel invalide.")]
        [StringLength(100, ErrorMessage = "Le courriel ne peut dépasser 100 caractères.")]
        public string Mail { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public bool IsAdmin { get; set; }

        
        public List<Message> Messages { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
    }
}