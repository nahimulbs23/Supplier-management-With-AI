using Nop.Core;

namespace Nop.Plugin.Misc.SupplierManager.Domain
{
    public class Supplier : BaseEntity
    {
        /// <summary>
        /// Gets or sets the supplier name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the supplier email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the supplier phone number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the supplier address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the supplier description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the supplier is active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }
    }
} 