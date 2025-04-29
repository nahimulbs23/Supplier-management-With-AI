using FluentMigrator;
using Nop.Data.Migrations;

namespace Nop.Plugin.Misc.SupplierManager.Infrastructure.Migrations
{
    [NopMigration("2024/03/20 12:00:00", "Supplier Manager plugin: Create supplier table")]
    public class CreateSupplierTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("Supplier")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(400).NotNullable()
                .WithColumn("Email").AsString(255).NotNullable()
                .WithColumn("PhoneNumber").AsString(50).NotNullable()
                .WithColumn("Address").AsString(1000).NotNullable();
        }
    }
} 