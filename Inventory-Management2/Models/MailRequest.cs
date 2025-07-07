namespace Inventory_Management2.Models
{
    public class MailRequest
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; } = false;
        public string To { get; set; }
    }
}
