namespace Inventory_Management2.Models.ViewModels
{
    public class MailService
    {
        public string SMTP_SERVER { get; set; }

        public int SMTP_PORT { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
    }
}
