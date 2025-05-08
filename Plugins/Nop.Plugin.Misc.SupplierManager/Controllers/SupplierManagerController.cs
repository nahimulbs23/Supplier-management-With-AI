using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Web.Areas.Admin.Controllers;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Plugin.Misc.SupplierManager.Models;
using Nop.Plugin.Misc.SupplierManager.Services;
using Nop.Web.Framework.Models.Extensions;
using Nop.Plugin.Misc.SupplierManager.Domain;
using Nop.Plugin.Misc.SupplierManager.Factories;
using Nop.Core.Domain.Common;

namespace Nop.Plugin.Misc.SupplierManager.Controllers
{
    [AuthorizeAdmin]
    [Area("Admin")]
    public class SupplierManagerController : BasePluginController
    {
        private readonly ISupplierService _supplierService;
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly ISupplierModelFactory _supplierModelFactory;

        public SupplierManagerController(
            ISupplierService supplierService,
            ILocalizationService localizationService,
            INotificationService notificationService,
            ISupplierModelFactory supplierModelFactory)
        {
            _supplierService = supplierService;
            _localizationService = localizationService;
            _notificationService = notificationService;
            _supplierModelFactory = supplierModelFactory;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public async Task<IActionResult> List()
        {
            var searchModel = await _supplierModelFactory.PrepareSupplierSearchModelAsync(new SupplierSearchModel());
            return View(searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> List(SupplierSearchModel searchModel)
        {
            var model = await _supplierModelFactory.PrepareSupplierListModelAsync(searchModel);
            return Json(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = await _supplierModelFactory.PrepareSupplierModelAsync(null, null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<IActionResult> Create(SupplierModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var supplier = new Supplier
                {
                    Name = model.Name,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address
                };

                await _supplierService.InsertSupplierAsync(supplier);

                _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = supplier.Id });
            }

            //If we got this far, something failed, redisplay form
            model = await _supplierModelFactory.PrepareSupplierModelAsync(model, null, true);
            return View("Create", model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            if (supplier == null)
                return RedirectToAction("List");

            var model = await _supplierModelFactory.PrepareSupplierModelAsync(null, supplier);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<IActionResult> Edit(SupplierModel model, bool continueEditing)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(model.Id);
            if (supplier == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                supplier.Name = model.Name;
                supplier.Email = model.Email;
                supplier.PhoneNumber = model.PhoneNumber;
                supplier.Address = model.Address;

                await _supplierService.UpdateSupplierAsync(supplier);

                _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = supplier.Id });
            }

            //If we got this far, something failed, redisplay form
            model = await _supplierModelFactory.PrepareSupplierModelAsync(model, supplier, true);
            return View("Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            if (supplier == null)
                return RedirectToAction("List");

            await _supplierService.DeleteSupplierAsync(supplier);

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Plugins.Misc.SupplierManager.Deleted"));

            return RedirectToAction("List");
        }
    }
} 