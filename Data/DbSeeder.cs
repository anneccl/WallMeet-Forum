using Microsoft.AspNetCore.Identity;
using TP5.Models;
using TP5.Utilities;

namespace TP5.Data
{
    /// <summary>
    /// Classe responsable d'initialiser la base de donnée
    /// </summary>
    public  static class DbSeeder
    {
        public static void UserCreation(ApplicationDbContext context)
        {
            // Si des users exite deja, ne fait rien
            if (context.Users.Any())
                return;

            // Création de users de base
            var users = new List<User>
            {
                new User
                {
                   FirstName = "Super",
                   LastName = "Admin",
                   Mail = "superadmin@wallmeet.com",
                   PasswordHash = PasswordHasher.HashPassword("Admin123!"),
                   IsAdmin = true
                },
                new User
                {
                   FirstName = "Emma",
                   LastName = "Tremblay",
                   Mail = "emmatremblay@wallmeet.com",
                   PasswordHash = PasswordHasher.HashPassword("Emma789!"),
                   IsAdmin = false
                },
                new User
                {
                   FirstName = "Lucas",
                   LastName = "Bouchard",
                   Mail = "lucasbouchard@wallmeet.com",
                   PasswordHash = PasswordHasher.HashPassword("Lucas456!"),
                   IsAdmin = false
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
