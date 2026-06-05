using System.ComponentModel.DataAnnotations;

namespace TP5.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le contenu du message est obligatoire.")]
        [StringLength(2000, MinimumLength = 1, ErrorMessage = "Le message doit contenir entre 1 et 2000 caractères.")]
        public string Content { get; set; } = string.Empty;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public List<Comment> Comments { get; set; } = new();

        public DateTime Date { get; private set; } = DateTime.Now;
    }
}