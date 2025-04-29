using Nop.Plugin.Misc.SupplierManager.Models;
using Nop.Plugin.Misc.SupplierManager.Domain;
using Nop.Plugin.Misc.SupplierManager.Services;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Misc.SupplierManager.Factories
{
    public class SupplierModelFactory : ISupplierModelFactory
    {
        private readonly ISupplierService _supplierService;
        private readonly ILocalizationService _localizationService;
        private readonly IPictureService _pictureService;

        public SupplierModelFactory(
            ISupplierService supplierService,
            ILocalizationService localizationService,
            IPictureService pictureService)
        {
            _supplierService = supplierService;
            _localizationService = localizationService;
            _pictureService = pictureService;
        }

        public async Task<SupplierModel> PrepareSupplierModelAsync(SupplierModel model, Supplier supplier, bool excludeProperties = false)
        {
            if (supplier != null)
            {
                if (model == null)
                {
                    model = new SupplierModel();
                }

                if (!excludeProperties)
                {
                    model.Id = supplier.Id;
                    model.Name = supplier.Name;
                    model.Email = supplier.Email;
                    model.PhoneNumber = supplier.PhoneNumber;
                    model.Address = supplier.Address;
                    model.Description = supplier.Description;
                    model.IsActive = supplier.IsActive;
                    model.DisplayOrder = supplier.DisplayOrder;
                }
            }

            return model;
        }

        public async Task<SupplierListModel> PrepareSupplierListModelAsync(SupplierSearchModel searchModel)
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync(
                name: searchModel.SearchName,
                pageIndex: searchModel.Page - 1,
                pageSize: searchModel.PageSize);

            var model = new SupplierListModel().PrepareToGrid(searchModel, suppliers, () =>
            {
                return suppliers.Select(supplier =>
                {
                    var supplierModel = new SupplierModel();
                    return PrepareSupplierModelAsync(supplierModel, supplier).Result;
                });
            });

            return model;
        }

        public async Task<SupplierSearchModel> PrepareSupplierSearchModelAsync(SupplierSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();

            return searchModel;
        }
    }
} 