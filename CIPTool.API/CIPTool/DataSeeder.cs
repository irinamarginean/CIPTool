using BusinessObjectLayer;
using BusinessObjectLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CIPTool
{
    public class DataSeeder
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var roleManager = services
                .GetRequiredService<RoleManager<IdentityRole>>();
            await EnsureRolesAsync(roleManager);

            var userManager = services
                .GetRequiredService<UserManager<IdentityUser>>();
            await EnsureTestAdminRoleAsync(userManager);
        }

        private static async Task EnsureTestAdminRoleAsync(UserManager<IdentityUser> userManager)
        {
            var testAdmin = await userManager.Users
                .Where(x => x.Email == "fixed-term.irina.marginean@ro.bosch.com")
                .SingleOrDefaultAsync();

            if (testAdmin != null) return;

            testAdmin = new Associate
            {
                UserName = "mai2clj",
                FirstName = "Irina",
                LastName = "Marginean",
                Email = "fixed-term.irina.marginean@ro.bosch.com"
            };
            await userManager.CreateAsync(
                testAdmin, "Ir!na1234");
            await userManager.AddToRoleAsync(
                testAdmin, Constants.AdminRole);
        }

        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var alreadyExists = await roleManager
                .RoleExistsAsync(Constants.AdminRole);

            if (alreadyExists) return;

            await roleManager.CreateAsync(new IdentityRole(Constants.AdminRole));
        }
    }
}
