namespace TP5.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string Action { get; set; } = string.Empty;    // "LOGIN_SUCCESS", "LOGIN_FAILED", "DELETE_MESSAGE", etc.
        public string Details { get; set; } = string.Empty;
        public int? UserId { get; set; }
        public string IpAddress { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
