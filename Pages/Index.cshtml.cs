using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TP5.Data;
using TP5.Models;

namespace TP5.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private const int MessagesPerPage = 5;

       public IndexModel( ApplicationDbContext context)
       {
            _context = context;
       }

        public List<Message> Messages { get; set; } // Messages à afficher

        // Pagination
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int TotalMessages { get; set; }

        // Info User connecté
        public string UserFullName { get; set; }
        public bool  IsAdmin { get; set; }
        public string UserMail { get; set; }


        // GET: Affiche le mur avec les messages
        public async Task<IActionResult>  OnGetAsync(int pageNumber = 1)
        {
            // Vérifie si connecté
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            //  infos pour la session
            UserFullName = HttpContext.Session.GetString("UserFullName") ?? "";
            IsAdmin = HttpContext.Session.GetString("IsAdmin") == "True";
            UserMail =HttpContext.Session.GetString("UserMail") ?? ""; 

            // Pagination
            CurrentPage = pageNumber < 1 ? 1 : pageNumber;
            TotalMessages = await _context.Messages.CountAsync();
            TotalPages = (int)Math.Ceiling(TotalMessages / (double)MessagesPerPage);

            // Récupération des messages avec auteurs et commentaires
            Messages = await _context.Messages
                .Include(m => m.User)
                .Include(m => m.Comments)
                    .ThenInclude(c => c.User)
                .OrderByDescending(m => m.Date)
                .Skip((CurrentPage - 1) * MessagesPerPage)
                .Take(MessagesPerPage)
                .ToListAsync();

            return Page();
        }


        // POST: Ajouter un message
        public async Task<IActionResult> OnPostAddMessageAsync(string Content)
        {
            // Vérifie si connecté
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            // Validation
            if (string.IsNullOrWhiteSpace(Content) || Content.Length > 2000)
            {
                return RedirectToPage("/Index");
            }

            var message = new Message
            {
                Content = Content.Trim(),
                UserId = userId.Value
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }

        // POST: Ajouter un commentaire
        public async Task<IActionResult> OnPostAddCommentAsync(int messageId, string Content)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            if (string.IsNullOrWhiteSpace(Content) || Content.Length > 500)
            {
                return RedirectToPage("/Index");
            }

            // Vérifier que le message existe
            var messageExists = await _context.Messages.AnyAsync(m => m.Id == messageId);
            if (!messageExists)
            {
                return RedirectToPage("/Index");
            }

            var comment = new Comment
            {
                Content = Content.Trim(),
                MessageId = messageId,
                UserId = userId.Value
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }

        // POST: Supprimer un message (avec autorisation)
        public async Task<IActionResult> OnPostDeleteMessageAsync(int messageId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            var isAdmin = HttpContext.Session.GetString("IsAdmin") == "True";

            var message = await _context.Messages.FindAsync(messageId);
            if (message == null)
            {
                return RedirectToPage("/Index");
            }

            // SÉCURITÉ: Vérifier que l'utilisateur a le droit de supprimer
            // (admin OU auteur du message)
            if (!isAdmin && message.UserId != userId.Value)
            {
                return Forbid(); // 403 Forbidden
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }

        // POST: Supprimer un commentaire (avec autorisation)
        public async Task<IActionResult> OnPostDeleteCommentAsync(int commentId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            var isAdmin = HttpContext.Session.GetString("IsAdmin") == "True";

            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null)
            {
                return RedirectToPage("/Index");
            }

            // SÉCURITÉ: Vérifier l'autorisation
            if (!isAdmin && comment.UserId != userId.Value)
            {
                return Forbid();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }

        // POST:
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }
    }
}
