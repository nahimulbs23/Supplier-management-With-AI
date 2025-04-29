using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.SupplierManager.Models
{
    public record SupplierModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Plugins.Misc.SupplierManager.Fields.Name")]
        [Required(ErrorMessage = "Plugins.Misc.SupplierManager.Fields.Name.Required")]
        public string Name { get; set; }

        [NopResourceDisplayName("Plugins.Misc.SupplierManager.Fields.Email")]
        [Required(ErrorMessage = "Plugins.Misc.SupplierManager.Fields.Email.Required")]
        [EmailAddress(ErrorMessage = "Plugins.Misc.SupplierManager.Fields.Email.WrongEmail")]
        public string Email { get; set; }

        [NopResourceDisplayName("Plugins.Misc.SupplierManager.Fields.PhoneNumber")]
        [Required(ErrorMessage = "Plugins.Misc.SupplierManager.Fields.PhoneNumber.Required")]
        public string PhoneNumber { get; set; }

        [NopResourceDisplayName("Plugins.Misc.SupplierManager.Fields.Address")]
        [Required(ErrorMessage = "Plugins.Misc.SupplierManager.Fields.Address.Required")]
        public string Address { get; set; }

        [NopResourceDisplayName("Plugins.Misc.SupplierManager.Fields.Description")]
        public string Description { get; set; }

        [NopResourceDisplayName("Plugins.Misc.SupplierManager.Fields.IsActive")]
        public bool IsActive { get; set; }

        [NopResourceDisplayName("Plugins.Misc.SupplierManager.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }
    }
} 