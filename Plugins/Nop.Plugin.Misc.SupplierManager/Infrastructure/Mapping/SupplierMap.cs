using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.SupplierManager.Domain;

namespace Nop.Plugin.Misc.SupplierManager.Infrastructure.Mapping
{
    public class SupplierMap : NopEntityBuilder<Supplier>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(Supplier.Name)).AsString(400).NotNullable()
                .WithColumn(nameof(Supplier.Email)).AsString(255).NotNullable()
                .WithColumn(nameof(Supplier.PhoneNumber)).AsString(50).NotNullable()
                .WithColumn(nameof(Supplier.Address)).AsString(1000).NotNullable();
        }
    }
} 