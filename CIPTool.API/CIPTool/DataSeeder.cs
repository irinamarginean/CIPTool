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
                    DisplayName = "Marginean Irina (RBRO/EPS3)",
                    Email = "Irina.Marginean@ro.bosch.com"
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

            var testAdminLeader = await userManager.Users
                .Where(x => x.UserName == "iop5bp")
                .SingleOrDefaultAsync();

            if (testAdminLeader == null)
            {
                testAdminLeader = new Leader
                {
                    UserName = "iop5bp",
                    FirstName = "Paul",
                    LastName = "Ionescu",
                    Group = "PJ-PLC PJ-PE",
                    Department = "PJ-PLC PJ-PE",
                    Leader = null,
                    DisplayName = "Ionescu Paul (RBRO/PJ-PLC RBRO/PJ-PE)",
                    Email = "Paul.Ionescu@ro.bosch.com"
                };

                await userManager.CreateAsync(
                    testAdminLeader, "Pas$1234");
                await userManager.AddToRoleAsync(
                    testAdminLeader, Constants.AdminRole);
                await userManager.AddToRoleAsync(
                   testAdminLeader, Constants.LeaderRole);
            }

            var adminMonica = await userManager.Users
                .Where(x => x.UserName == "rsl8blj")
                .SingleOrDefaultAsync();

            if (adminMonica == null)
            {
                adminMonica = new Associate
                {
                    UserName = "rsl8blj",
                    FirstName = "Monica",
                    LastName = "Rus",
                    Group = "PJ-PE",
                    Department = "PJ-PE",
                    Leader = testAdminLeader as Leader,
                    DisplayName = "Rus Monica (RBRO/PJ-PE)",
                    Email = "monica.rus@ro.bosch.com"
                };

                await userManager.CreateAsync(
                    adminMonica, "Pas$1234");
                await userManager.AddToRoleAsync(
                    adminMonica, Constants.AdminRole);
            }

            var cretuLiviu = await userManager.Users
                .Where(x => x.UserName == "cri2bp")
                .SingleOrDefaultAsync();

            if (cretuLiviu == null)
            {
                cretuLiviu = new Leader
                {
                    UserName = "cri2bp",
                    FirstName = "Liviu",
                    LastName = "Cretu",
                    Group = "EPS4",
                    Department = "EPS",
                    IsLeader = true,
                    Leader = testHeadOfDepartment as Leader,
                    DisplayName = "Cretu Liviu (RBRO/EPS4)",
                    Email = "Liviu.Cretu@ro.bosch.com"
                };

                await userManager.CreateAsync(
                    cretuLiviu, "Pas$1234");
                await userManager.AddToRoleAsync(
                    cretuLiviu, Constants.LeaderRole);
            }

            var laszloAlbert = await userManager.Users
                .Where(x => x.UserName == "lal2bp")
                .SingleOrDefaultAsync();

            if (laszloAlbert == null)
            {
                laszloAlbert = new Associate
                {
                    UserName = "lal2bp",
                    FirstName = "Laszlo",
                    LastName = "Albert",
                    Group = "EPS4",
                    Department = "EPS",
                    Leader = cretuLiviu as Leader,
                    DisplayName = "Albert Laszlo (RBRO/EPS4)",
                    Email = "Laszlo.Albert@ro.bosch.com"
                };

                await userManager.CreateAsync(
                    laszloAlbert, "Pas$1234");
                await userManager.AddToRoleAsync(
                    laszloAlbert, Constants.AssociateRole);
            }

            var ioanDragan = await userManager.Users
               .Where(x => x.UserName == "drc4clj")
               .SingleOrDefaultAsync();

            if (ioanDragan == null)
            {
                ioanDragan = new Leader
                {
                    UserName = "drc4clj",
                    FirstName = "Ioan-Ciprian",
                    LastName = "Dragan",
                    Group = "ESA3",
                    Department = "ESA",
                    IsLeader = true,
                    Leader = testHeadOfDepartment2 as Leader,
                    DisplayName = "Dragan Ioan-Ciprian (RBRO/ESA3)",
                    Email = "Ioan-Ciprian.Dragan@ro.bosch.com"
                };

                await userManager.CreateAsync(
                    ioanDragan, "Pas$1234");
                await userManager.AddToRoleAsync(
                    ioanDragan, Constants.LeaderRole);
            }

            var florianBoer = await userManager.Users
            .Where(x => x.UserName == "bfm4clj")
            .SingleOrDefaultAsync();

            if (florianBoer == null)
            {
                florianBoer = new Associate
                {
                    UserName = "bfm4clj",
                    FirstName = "Florian-Mircea",
                    LastName = "Boer",
                    Group = "ESA3",
                    Department = "ESA",
                    Leader = ioanDragan as Leader,
                    DisplayName = "Boer Florian-Mircea (RBRO/ESA3)",
                    Email = "Florian-Mircea.Boer@ro.bosch.com"
                };

                await userManager.CreateAsync(
                    florianBoer, "Pas$1234");
                await userManager.AddToRoleAsync(
                    florianBoer, Constants.AssociateRole);
            }

            var ionStoica = await userManager.Users
               .Where(x => x.UserName == "stc1yok")
               .SingleOrDefaultAsync(); 

            if (ionStoica == null)
            {
                ionStoica = new Leader
                {
                    UserName = "stc1yok",
                    FirstName = "Ion Corneliu",
                    LastName = "Stoica",
                    Group = "ESA1",
                    Department = "ESA",
                    IsLeader = true,
                    Leader = testHeadOfDepartment2 as Leader,
                    DisplayName = "Stoica Ion Corneliu (RBRO/ESA1)",
                    Email = "IonCorneliu.Stoica@ro.bosch.com"
                };

                await userManager.CreateAsync(
                    ionStoica, "Pas$1234");
                await userManager.AddToRoleAsync(
                    ionStoica, Constants.LeaderRole);
            }

            var mihneaPop = await userManager.Users
                .Where(x => x.UserName == "pmv4clj")
                .SingleOrDefaultAsync();

            if (mihneaPop == null)
            {
                mihneaPop = new Associate
                {
                    UserName = "pmv4clj",
                    FirstName = "Mihnea",
                    LastName = "Pop",
                    Group = "ESA1",
                    Department = "ESA",
                    Leader = ionStoica as Leader,
                    DisplayName = "Pop Mihnea (RBRO/ESA1)",
                    Email = "MihneaVlad.Pop@ro.bosch.com"
                };

                await userManager.CreateAsync(
                    mihneaPop, "Pas$1234");
                await userManager.AddToRoleAsync(
                    mihneaPop, Constants.AssociateRole);
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
