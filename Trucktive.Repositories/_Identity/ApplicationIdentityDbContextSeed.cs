using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trucktive.Core.Entities;

namespace Trucktive.Repositories._Identity
{
    public class ApplicationIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            string[] roleNames = { "Admin", "Supervisor" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }


            // Check for admin user
            var adminUser = await userManager.FindByEmailAsync("admin@trucktive.com");

            if (adminUser == null)
            {
                var user = new ApplicationUser()
                {
                    Email = "admin@trucktive.com",
                    UserName = "admin@trucktive.com",
                };

                var result = await userManager.CreateAsync(user, "P@ssw0rd");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }

            }
            else if (!(await userManager.IsInRoleAsync(adminUser, "Admin")))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }


            // Check for marketer user

            var marketerUser = await userManager.FindByEmailAsync("supervisor@trucktive.com");

            if (marketerUser == null)
            {
                var user = new ApplicationUser()
                {
                    Email = "supervisor@trucktive.com",
                    UserName = "supervisor@trucktive.com",

                };

                var result = await userManager.CreateAsync(user, "P@ssw0rd123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Supervisor");
                }

            }
            else if (!(await userManager.IsInRoleAsync(marketerUser, "Supervisor")))
            {
                await userManager.AddToRoleAsync(marketerUser, "Supervisor");
            }
        }
    }
}
