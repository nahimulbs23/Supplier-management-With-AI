using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.SupplierManager.Models
{
    public record SupplierSearchModel : BaseSearchModel
    {
        public string SearchName { get; set; }
        public string SearchEmail { get; set; }
        public string SearchPhoneNumber { get; set; }
        public string SearchAddress { get; set; }
    }
} 