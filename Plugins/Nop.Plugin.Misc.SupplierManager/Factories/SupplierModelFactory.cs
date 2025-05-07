using Nop.Plugin.Misc.SupplierManager.Models;
using Nop.Plugin.Misc.SupplierManager.Domain;
using Nop.Plugin.Misc.SupplierManager.Services;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Web.Framework.Models.Extensions;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;


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
                }
            }

            return model;
        }

        public async Task<SupplierListModel> PrepareSupplierListModelAsync(SupplierSearchModel searchModel)
        {
            ArgumentNullException.ThrowIfNull(searchModel);

            // Get suppliers
            var suppliers = await _supplierService.GetAllSuppliersAsync(
                name: searchModel.SearchName,
                email: searchModel.SearchEmail,
                phoneNumber: searchModel.SearchPhoneNumber,
                address: searchModel.SearchAddress,
                pageIndex: searchModel.Page - 1,
                pageSize: searchModel.PageSize);

            // Prepare the grid model
            var model = await new SupplierListModel().PrepareToGridAsync(searchModel, suppliers, () =>
            {
                return suppliers.SelectAwait(async supplier =>
                {
                    // Manually create and populate a SupplierModel instead of using AutoMapper
                    var supplierModel = new SupplierModel
                    {
                        Id = supplier.Id,
                        Name = supplier.Name,
                        Email = supplier.Email,
                        PhoneNumber = supplier.PhoneNumber,
                        Address = supplier.Address
                        // Map any other properties that exist on both classes
                    };

                    // Add any additional properties that might need special handling

                    return supplierModel;
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