using System.Globalization;

namespace TP5.Utilities
{
    public static class DateExtensions
    {
        private static readonly CultureInfo QuebecCulture = new("fr-CA");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FormaterDate(this DateTime date)
        {
            return date.ToString("dddd d MMMM yyyy 'à' HH 'h' mm", QuebecCulture);
        }

        /// <summary>
        /// Format court pour les commentaires.
        /// </summary>
        public static string FormaterDateCourt(this DateTime date)
        {
            return date.ToString("d MMMM yyyy', 'HH 'h' mm", QuebecCulture);
        }
    }
}