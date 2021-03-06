namespace BeerTracker.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.DataModels;
    using Models.DataModels.Enums;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BeerTracker.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var adminRole = UserRoles.Administrator.ToString();
            var regularUserRole = UserRoles.RegularUser.ToString();
            var partnerRole = UserRoles.Partner.ToString();

            if (!context.Roles.Any(r => r.Name == regularUserRole))
            {
                CreateRole(context, regularUserRole);
            }
            if (!context.Roles.Any(r => r.Name == partnerRole))
            {
                CreateRole(context, partnerRole);
            }

            if (!context.Roles.Any(r => r.Name == adminRole))
            {
                CreateRole(context, adminRole);
            }

            if (!context.Users.Any())
            {
                var adminEmail = "admin@admin.com";
                var adminUsername = adminRole;
                var adminFullName = "System Administrator";
                var adminPass = adminEmail;

                CreateAdminUser(context, adminEmail, adminFullName, adminUsername, adminPass, adminRole);
            }
        }

        private void CreateRole(ApplicationDbContext context, string role)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var roleCreateResult = roleManager.Create(new IdentityRole(role));

            if (!roleCreateResult.Succeeded)
            {
                throw new Exception(string.Join(";", roleCreateResult.Errors));
            }

        }

        private void CreateAdminUser(ApplicationDbContext context, string adminEmail, string adminFullName, string adminUsername, string adminPass, string adminRole)
        {
            var adminUser = new User
            {
                UserName = adminUsername,
                Email = adminEmail,
                FullName = adminFullName,
                IsActive = true
            };

            var userStore = new UserStore<User>(context);

            var userManager = new UserManager<User>(userStore);

            userManager.PasswordValidator = new PasswordValidator
            {
                RequireDigit = false,
                RequiredLength = 1,
                RequireLowercase = false,
                RequireNonLetterOrDigit = false,
                RequireUppercase = false
            };

            var userCreateResult = userManager.Create(adminUser, adminPass);

            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join(";", userCreateResult.Errors));
            }

            this.AddUserInRole(userManager, adminUser.Id, adminRole);
        }

        private void AddUserInRole(UserManager<User> userManager, string adminUserId, string adminRole)
        {
            var addAdminToRoleResult = userManager.AddToRole(adminUserId, adminRole);

            if (!addAdminToRoleResult.Succeeded)
            {
                throw new Exception(string.Join(";", addAdminToRoleResult.Errors));
            }
        }
    }
}
