using Microsoft.AspNetCore.Identity;

namespace Inventory_Management2.Models.ViewModels
{
    public class UserCreate
    {

        public IdentityUser User { get; set; }
        public string Role { get; set; }
    }
}
