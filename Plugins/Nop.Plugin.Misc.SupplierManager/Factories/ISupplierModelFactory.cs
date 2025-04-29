using Nop.Plugin.Misc.SupplierManager.Models;
using Nop.Plugin.Misc.SupplierManager.Domain;

namespace Nop.Plugin.Misc.SupplierManager.Factories
{
    public interface ISupplierModelFactory
    {
        Task<SupplierModel> PrepareSupplierModelAsync(SupplierModel model, Supplier supplier, bool excludeProperties = false);
        Task<SupplierListModel> PrepareSupplierListModelAsync(SupplierSearchModel searchModel);
        Task<SupplierSearchModel> PrepareSupplierSearchModelAsync(SupplierSearchModel searchModel);
    }
} 