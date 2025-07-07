using Inventory_Management2.Models;
using Inventory_Management2.Models.ViewModels;

namespace Inventory_Management2.Interfaces
{
    public interface IMailRepository
    {

        public Task SendMailAsync(MailRequest request);

        public Task NotifyEveryone(MailRequest request);
        public void ConfigureService(MailService service);
    }
}
