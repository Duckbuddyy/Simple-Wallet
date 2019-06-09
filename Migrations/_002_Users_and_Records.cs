using FluentMigrator;

namespace WebTabanliProje.Migrations
{
    [Migration(6)]
    public class _002_Users_and_Records : Migration
    {
        public override void Down()
        {
            Delete.Table("Categories");
            Delete.Table("Records");
            Delete.Table("Users_Records");
        }

        public override void Up()
        {
            Create.Table("Categories")
                .WithColumn("Category_Id").AsInt32().PrimaryKey().NotNullable().Indexed()
                .WithColumn("Category_Name").AsString(256).NotNullable();

            Create.Table("Records")
                .WithColumn("Record_Id").AsInt32().PrimaryKey().NotNullable().Indexed()
                .WithColumn("Amount").AsFloat().NotNullable()
                .WithColumn("Type").AsBoolean().NotNullable()
                .WithColumn("Note").AsString(256).Nullable()
                .WithColumn("Category").AsInt32();

            Create.Table("Users_Records")
                .WithColumn("User_Id").AsInt32()
                .WithColumn("Record_Id").AsInt32();
        }
    }
}