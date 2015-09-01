namespace Motonet.Migrations.UserContext
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Motonet.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Motonet.Models.UserContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migrations\UserContext";
        }

        protected override void Seed(Motonet.Models.UserContext context)
        {
            //  This method will be called after migrating to the latest version.

            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);
            var user = new ApplicationUser { UserName = "admin@admin.com", Email = "admin@admin.com" };

            manager.Create(user, "Admin15++"); // Change this ASAP!


            base.Seed(context);
        }
    }
}
