using Nop.Web.Framework.Models;
using System.Collections.Generic;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.SupplierManager.Models
{
    public record SupplierSearchModel : BaseSearchModel
    {
        public SupplierSearchModel()
        {
            Suppliers = new List<SupplierModel>();
        }

        [NopResourceDisplayName("Plugins.Misc.SupplierManager.Fields.SearchName")]
        public string SearchName { get; set; }

        [NopResourceDisplayName("Plugins.Misc.SupplierManager.Fields.SearchEmail")]
        public string SearchEmail { get; set; }

        [NopResourceDisplayName("Plugins.Misc.SupplierManager.Fields.SearchPhoneNumber")]
        public string SearchPhoneNumber { get; set; }

        [NopResourceDisplayName("Plugins.Misc.SupplierManager.Fields.SearchAddress")]
        public string SearchAddress { get; set; }

        public IList<SupplierModel> Suppliers { get; set; }
    }
} 