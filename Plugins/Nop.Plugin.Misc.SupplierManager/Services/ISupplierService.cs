using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Plugin.Misc.SupplierManager.Domain;

namespace Nop.Plugin.Misc.SupplierManager.Services
{
    public interface ISupplierService
    {
        Task<Supplier> GetSupplierByIdAsync(int id);
        Task<IPagedList<Supplier>> GetAllSuppliersAsync(
            string name = null,
            string email = null,
            string phoneNumber = null,
            string address = null,
            int pageIndex = 0,
            int pageSize = int.MaxValue);
        Task InsertSupplierAsync(Supplier supplier);
        Task UpdateSupplierAsync(Supplier supplier);
        Task DeleteSupplierAsync(Supplier supplier);
    }
} 