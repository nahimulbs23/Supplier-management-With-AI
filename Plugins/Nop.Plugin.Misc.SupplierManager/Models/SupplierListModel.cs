using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.SupplierManager.Models
{
    public record SupplierListModel : BasePagedListModel<SupplierModel>
    {
        public IList<SupplierModel> Suppliers { get; set; } = new List<SupplierModel>();
    }
} 