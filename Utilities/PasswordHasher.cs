namespace TP5.Utilities
{
    public class PasswordHasher
    {
        //Pepper
        private const string Pepper = "TP5_S3cur1t3_P3pp3r_2026!";


        /// <summary>
        /// Fonction qui retourne le hash d'un mot de passe. Le salt est géré par BCrypt
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string HashPassword(string password)
        {
            password = password + Pepper; 
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// <summary>
        /// Vérifie si le mot de passe en clair correspond au hachage Bcrypt fourni.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static bool VerifyPassword(string password, string hash)
        {
            password =  password + Pepper;
            return BCrypt.Net.BCrypt.Verify(password, hash) ;
        }
    }
}
