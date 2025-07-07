using Inventory_Management2.Interfaces;
using Inventory_Management2.Models;
using Inventory_Management2.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Inventory_Management2.Repositories
{
    public class MailRepository : IMailRepository
    {

        private MailService _mailSettings;
        protected readonly IUserRepository _userRepository;

        public MailRepository(IOptions<MailService> mailSettings, IUserRepository userRepository)
        {
            _mailSettings = mailSettings.Value;
            _userRepository = userRepository;
        }

        public MailRepository()
        {
            _mailSettings = new MailService();
        }

        public void ConfigureService(MailService service)
        {
            _mailSettings = service;
        }

        public async Task NotifyEveryone(MailRequest request)
        {

            List<IdentityUser> users = await _userRepository.GetAllUsersAsync();


            foreach (IdentityUser user in users) {
                request.To = user.Email;
                await SendMailAsync(request);
            }

            
        }


        public async Task SendMailAsync(MailRequest request)
        {
            try
            {
                using var smtpClient = new SmtpClient(_mailSettings.SMTP_SERVER, _mailSettings.SMTP_PORT)
                {
                    Credentials = new NetworkCredential(_mailSettings.From, _mailSettings.Password),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_mailSettings.From, _mailSettings.UserName),
                    Subject = request.Subject,
                    Body = request.Body,
                    IsBodyHtml = request.IsHtml
                };

                mailMessage.To.Add(request.To);

               


                await smtpClient.SendMailAsync(mailMessage);
                
            }
            catch (Exception ex)
            {
               
                Console.WriteLine(ex);
            }
        }
    }
}
