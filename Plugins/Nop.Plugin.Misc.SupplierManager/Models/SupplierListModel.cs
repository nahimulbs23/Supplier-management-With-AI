using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.SupplierManager.Models
{
    public record SupplierListModel : BasePagedListModel<SupplierModel>
    {
        public SupplierSearchModel SearchModel { get; set; } = new();
    }
} 