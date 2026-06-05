using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using TP5.Data;
using TP5.Models;
using TP5.Utilities;

namespace TP5.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;
       
        public LoginModel (ApplicationDbContext context)
        {
            _context = context;
        }

        // Propriétés utilisés pour le login

        [BindProperty]
        public string Mail { get; set; }
        [BindProperty]
        [DataType("password")]
        public string Password { get; set; }

        public string MessageError { get; set; }


        // GET: Affichage de la page login
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        // POST: Traite la soumission du formulaire
        public async Task<IActionResult> OnPostAsync()
        {
            //Validation que les champs Mail et password ne sont pas vide
            if (string.IsNullOrWhiteSpace(Mail) || string.IsNullOrWhiteSpace(Password))
            {
                MessageError = "Veuillez entrer un courriel et un mot de passe.";
                return Page();
            }

            // Recherche du user par son courriel
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Mail == Mail);

            // Vérification du mdp
            if (user == null || !PasswordHasher.VerifyPassword(Password, user.PasswordHash))
            {
                MessageError = "Courriel ou mot de passe invalide";
                return Page();
            }

            // Stocke les infos dans la session (PAS le mot de passe)
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserFullName", $"{user.FirstName} {user.LastName}");
            HttpContext.Session.SetString("UserMail", user.Mail);
            HttpContext.Session.SetString("IsAdmin", user.IsAdmin.ToString());

            return Redirect("/Index");
        }
    }
}
