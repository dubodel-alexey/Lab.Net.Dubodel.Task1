using System.Collections.Generic;
using task3.Infrastructure.Authentication;

namespace task3.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<task3.Infrastructure.Authentication.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            Role role1 = new Role { RoleName = "Admin" };
            Role role2 = new Role { RoleName = "User" };

            User user1 = new User { Username = "admin", Email = "admin@ymail.com",  Password = "123456",  Roles = new List<Role>() };
            User user2 = new User { Username = "user1", Email = "user1@ymail.com",  Password = "123456",  Roles = new List<Role>() };
            user1.Roles.Add(role1);
            user2.Roles.Add(role2);
            context.Users.AddOrUpdate(user1);
            context.Users.AddOrUpdate(user2);
        }
    }
}
