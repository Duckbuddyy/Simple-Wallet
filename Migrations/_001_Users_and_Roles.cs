using FluentMigrator;

namespace WebTabanliProje.Migrations
{
    [Migration(2)]
    public class _001_Users_and_Roles : Migration
    {
        public override void Down()
        {
            Delete.Table("Users");
            Delete.Table("Roles");
            Delete.Table("Roles_Users");
        }

        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("UserName").AsString(128)
                .WithColumn("Email").AsString(256)
                .WithColumn("UserPassword_hash").AsString(128);
            Create.Table("Roles")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("RoleName").AsString(128);
            Create.Table("Roles_Users")
                .WithColumn("User_Id").AsInt32().ForeignKey("Users", "Id").OnDelete(System.Data.Rule.Cascade)
                .WithColumn("Role_Id").AsInt32().ForeignKey("Roles", "Id").OnDelete(System.Data.Rule.Cascade);
        }
    }
}