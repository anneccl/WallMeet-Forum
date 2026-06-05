using System.ComponentModel.DataAnnotations;

namespace TP5.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le contenu du commentaire est obligatoire.")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Le commentaire doit contenir entre 1 et 500 caractères.")]
        public string Content { get; set; } = string.Empty;

        public int MessageId { get; set; }
        public Message Message { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public DateTime Date { get; private set; } = DateTime.Now;
    }
}