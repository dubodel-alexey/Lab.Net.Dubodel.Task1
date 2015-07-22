namespace task3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserRoleFieldRename : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropPrimaryKey("dbo.Roles");
            DropPrimaryKey("dbo.Users");
            DropColumn("dbo.Roles", "RoleId");
            DropColumn("dbo.Users", "UserId");
            AddColumn("dbo.Roles", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Users", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Roles", "Id");
            AddPrimaryKey("dbo.Users", "Id");
            AddForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserRoles", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "UserId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Roles", "RoleId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropPrimaryKey("dbo.Users");
            DropPrimaryKey("dbo.Roles");
            DropColumn("dbo.Users", "Id");
            DropColumn("dbo.Roles", "Id");
            AddPrimaryKey("dbo.Users", "UserId");
            AddPrimaryKey("dbo.Roles", "RoleId");
            AddForeignKey("dbo.UserRoles", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles", "RoleId", cascadeDelete: true);
        }
    }
}
