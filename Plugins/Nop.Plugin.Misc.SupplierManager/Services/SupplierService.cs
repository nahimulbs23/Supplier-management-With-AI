using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Nop.Core;
using Nop.Data;
using Nop.Plugin.Misc.SupplierManager.Domain;

namespace Nop.Plugin.Misc.SupplierManager.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IRepository<Supplier> _supplierRepository;

        public SupplierService(IRepository<Supplier> supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<Supplier> GetSupplierByIdAsync(int id)
        {
            return await _supplierRepository.GetByIdAsync(id);
        }

        public async Task<IPagedList<Supplier>> GetAllSuppliersAsync(
            string name = null,
            string email = null,
            string phoneNumber = null,
            string address = null,
            int pageIndex = 0,
            int pageSize = int.MaxValue)
        {
            var query = await _supplierRepository.GetAllAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(s => s.Name.Contains(name));
                if (!string.IsNullOrWhiteSpace(email))
                    query = query.Where(s => s.Email.Contains(email));
                if (!string.IsNullOrWhiteSpace(phoneNumber))
                    query = query.Where(s => s.PhoneNumber.Contains(phoneNumber));
                if (!string.IsNullOrWhiteSpace(address))
                    query = query.Where(s => s.Address.Contains(address));

                return from s in query
                       orderby s.Name
                       select s;
            });

            return await Task.FromResult(new PagedList<Supplier>(query, pageIndex, pageSize));
        }

        public async Task InsertSupplierAsync(Supplier supplier)
        {
            await _supplierRepository.InsertAsync(supplier);
        }

        public async Task UpdateSupplierAsync(Supplier supplier)
        {
            await _supplierRepository.UpdateAsync(supplier);
        }

        public async Task DeleteSupplierAsync(Supplier supplier)
        {
            await _supplierRepository.DeleteAsync(supplier);
        }
    }
} 