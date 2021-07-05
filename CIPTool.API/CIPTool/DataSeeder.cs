using BusinessObjectLayer;
using BusinessObjectLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CIPTool
{
    public static class DataSeeder
    {
        public static IHost MigrateDatabase<T>(this IHost webHost) where T : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var db = services.GetRequiredService<T>();
                    db.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }
            return webHost;
        }

        public static async Task InitializeAsync(IServiceProvider services)
        {
            var roleManager = services
                .GetRequiredService<RoleManager<IdentityRole>>();
            await EnsureRolesAsync(roleManager, Constants.AssociateRole);
            await EnsureRolesAsync(roleManager, Constants.LeaderRole);
            await EnsureRolesAsync(roleManager, Constants.AdminRole);

            var userManager = services
                .GetRequiredService<UserManager<IdentityUser>>();
            await EnsureTestAdminRoleAsync(userManager);
        }

        private static async Task EnsureTestAdminRoleAsync(UserManager<IdentityUser> userManager)
        {
            var testHeadOfDepartment = await userManager.Users
                .Where(x => x.UserName == "abf82wi")
                .SingleOrDefaultAsync();

            if (testHeadOfDepartment == null)
            {
                testHeadOfDepartment = new Leader
                {
                    UserName = "abf82wi",
                    FirstName = "Felizian",
                    LastName = "Aberham",
                    DisplayName = "Aberham Felizian (RBRO/EPS)",
                    Group = "EPS",
                    Department = "EPS",
                    Email = "Felizian.Aberham@ro.bosch.com",
                    IsLeader = true
                };

                await userManager.CreateAsync(
                    testHeadOfDepartment, "Pas$1234");
                await userManager.AddToRoleAsync(
                    testHeadOfDepartment, Constants.LeaderRole);
            }

            var testHeadOfDepartment2 = await userManager.Users
               .Where(x => x.UserName == "grrzwzl")
               .SingleOrDefaultAsync();

            if (testHeadOfDepartment2 == null)
            {
                testHeadOfDepartment2 = new Leader
                {
                    UserName = "grrzwzl",
                    FirstName = "Rinaldo",
                    LastName = "Greiner",
                    DisplayName = "Greiner Rinaldo (RBRO/ESA RBRO/PJ-PE)",
                    Group = "ESA PJ-PE",
                    Department = "ESA PJ-PE",
                    Email = "Rinaldo.Greiner@de.bosch.com",
                    IsLeader = true
                };

                await userManager.CreateAsync(
                    testHeadOfDepartment2, "Pas$1234");
                await userManager.AddToRoleAsync(
                    testHeadOfDepartment2, Constants.LeaderRole);
            }

            var testLeader = await userManager.Users
               .Where(x => x.UserName == "rba5clj")
               .SingleOrDefaultAsync();

            if (testLeader == null)
            {
                testLeader = new Leader
                {
                    UserName = "rba5clj",
                    FirstName = "Razvan",
                    LastName = "Barlea",
                    Group = "EPS3",
                    Department = "EPS",
                    Leader = testHeadOfDepartment as Leader,
                    DisplayName = "Barlea Razvan (RBRO/EPS3)",
                    Email = "Razvan.Barlea@ro.bosch.com",
                    IsLeader = true
                };

                await userManager.CreateAsync(
                    testLeader, "Pas$1234");
                await userManager.AddToRoleAsync(
                    testLeader, Constants.LeaderRole);
            }

            var testAssociate = await userManager.Users
                .Where(x => x.UserName == "mai2clj")
                .SingleOrDefaultAsync();

            if (testAssociate == null)
            {
                testAssociate = new Associate
                {
                    UserName = "mai2clj",
                    FirstName = "Irina",
                    LastName = "Marginean",
                    Group = "EPS3",
                    Department = "EPS",
                    Leader = testLeader as Leader,
                    DisplayName = "FIXED-TERM Marginean Irina (RBRO/EPS3)",
                    Email = "fixed-term.Irina.Marginean@ro.bosch.com"
                };

                await userManager.CreateAsync(
                    testAssociate, "Pas$1234");
                await userManager.AddToRoleAsync(
                    testAssociate, Constants.AssociateRole);
            }

            var testAdmin = await userManager.Users
                .Where(x => x.UserName == "dvi4clj")
                .SingleOrDefaultAsync();

            if (testAdmin == null)
            {
                testAdmin = new Associate
                {
                    UserName = "dvi4clj",
                    FirstName = "Vlad-Ilie",
                    LastName = "Domnar",
                    Group = "PJ-PE",
                    Department = "PJ-PE",
                    Leader = testHeadOfDepartment2 as Leader,
                    DisplayName = "Domnar Vlad-Ilie (RBRO/PJ-PE)",
                    Email = "Vlad-Ilie.Domnar@ro.bosch.com"
                };

                await userManager.CreateAsync(
                    testAdmin, "Pas$1234");
                await userManager.AddToRoleAsync(
                    testAdmin, Constants.AdminRole);
            }
        }

        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager, string role)
        {
            var alreadyExists = await roleManager
                .RoleExistsAsync(role);

            if (alreadyExists) return;

            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
