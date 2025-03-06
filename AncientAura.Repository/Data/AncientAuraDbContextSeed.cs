using AncientAura.Core.Entities.Identity;
using AncientAura.Repository.Data.Contexts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Repository.Data
{
    public class AncientAuraDbContextSeed
    {
        public async static Task SeedAppUser(UserManager<AppUser> _userManager)
        {
            if(_userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    Email = "gamalwork81@gmail.com",
                    FullName = "Jamal Abdelsalam Mohamed",
                    UserName = "Jamal_11",
                    PhoneNumber = "123456789",
                    ProfileImage = "Images\\ProfileImage\\Gamal.jpg"

                };
                await _userManager.CreateAsync(user, "Jamal@123");
            }
        }
    }
}
