namespace Moses_BugTracker.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Moses_BugTracker.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Configuration;

    internal sealed class Configuration : DbMigrationsConfiguration<Moses_BugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Moses_BugTracker.Models.ApplicationDbContext db)
        {
            var roleManager = new RoleManager<IdentityRole>(
             new RoleStore<IdentityRole>(db));

            if (!db.Roles.Any(rr => rr.Name == "ADMINISTRATOR"))
            {
                roleManager.Create(new IdentityRole { Name = "ADMINISTRATOR" });
            }

            if (!db.Roles.Any(rr => rr.Name == "PROJECTMANAGER"))
            {
                roleManager.Create(new IdentityRole { Name = "PROJECTMANAGER" });
            }

            if (!db.Roles.Any(rr => rr.Name == "DEVELOPER"))
            {
                roleManager.Create(new IdentityRole { Name = "DEVELOPER" });
            }

            if (!db.Roles.Any(rr => rr.Name == "SUBMITTER"))
            {
                roleManager.Create(new IdentityRole { Name = "SUBMITTER" });
            }

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(db));

            if (!db.Users.Any(u => u.Email == "DemoAdministrator@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoAdministrator@Mailinator.com",
                    Email = "DemoAdministrator@Mailinator.com",
                    FirstName = "Demo",
                    LastName = "Administrator",
                    DisplayName = "The Administrator",
                    AvatarPath = "/Avatars/default.png"
                }, "Abc&123!");

                var adminId = userManager.FindByEmail("DemoAdministrator@Mailinator.com").Id;
                userManager.AddToRole(adminId, "ADMINISTRATOR");
            }

            if (!db.Users.Any(u => u.Email == "DemoProjectManager@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoProjectManager@Mailinator.com",
                    Email = "DemoProjectManager@Mailinator.com",
                    FirstName = "Demo",
                    LastName = "ProjectManager",
                    DisplayName = "The PM",
                    AvatarPath = "/Avatars/default.png"
                }, "Abc&123!");

                var pmId = userManager.FindByEmail("DemoProjectManager@Mailinator.com").Id;
                userManager.AddToRole(pmId, "PROJECTMANAGER");
            }

            if (!db.Users.Any(u => u.Email == "DemoDeveloper@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoDeveloper@Mailinator.com",
                    Email = "DemoDeveloper@Mailinator.com",
                    FirstName = "Demo",
                    LastName = "Developer",
                    DisplayName = "The DEV",
                    AvatarPath = "/Avatars/default.png"
                }, "Abc&123!");

                var devId = userManager.FindByEmail("DemoDeveloper@Mailinator.com").Id;
                userManager.AddToRole(devId, "DEVELOPER");
            }

            if (!db.Users.Any(u => u.Email == "DemoSubmitter@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoSubmitter@Mailinator.com",
                    Email = "DemoSubmitter@Mailinator.com",
                    FirstName = "Demo",
                    LastName = "Submitter",
                    DisplayName = "The SUBMITTER",
                    AvatarPath = "/Avatars/default.png"
                }, "Abc&123!");

                var subId = userManager.FindByEmail("DemoSubmitter@Mailinator.com").Id;
                userManager.AddToRole(subId, "SUBMITTER");
            }            
        }
    }
}
