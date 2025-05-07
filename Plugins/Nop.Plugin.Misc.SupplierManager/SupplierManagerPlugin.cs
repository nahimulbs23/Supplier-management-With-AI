using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Menu;
using Nop.Services.Events;
using Nop.Web.Framework.Events;
using static Nop.Services.Security.StandardPermission;

namespace Nop.Plugin.Misc.SupplierManager
{
    public class SupplierManagerPlugin : BasePlugin, IMiscPlugin
    {
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;

        public SupplierManagerPlugin(
            ILocalizationService localizationService,
            ISettingService settingService,
            IWebHelper webHelper)
        {
            _localizationService = localizationService;
            _settingService = settingService;
            _webHelper = webHelper;
        }

        private async Task ManageLocalizationResourcesAsync(bool isInstall)
        {
            var resources = new Dictionary<string, string>
            {
                // Menu and Navigation
                ["Admin.Suppliers"] = "Suppliers",
                ["Plugins.Misc.SupplierManager.MenuTitle"] = "Supplier Manager",
                ["Plugins.Misc.SupplierManager.WelcomeMessage"] = "Welcome to Supplier Manager Plugin",
                ["Plugins.Misc.SupplierManager.Suppliers"] = "Suppliers",
                ["Plugins.Misc.SupplierManager.AddNew"] = "Add new supplier",
                ["Plugins.Misc.SupplierManager.Edit"] = "Edit supplier",
                ["Plugins.Misc.SupplierManager.BackToList"] = "Back to supplier list",
                ["Plugins.Misc.SupplierManager.SupplierInfo"] = "Supplier Information",

                // Success Messages
                ["Plugins.Misc.SupplierManager.Added"] = "The supplier has been added successfully",
                ["Plugins.Misc.SupplierManager.Updated"] = "The supplier has been updated successfully",
                ["Plugins.Misc.SupplierManager.Deleted"] = "The supplier has been deleted successfully",

                // Field Labels
                ["Plugins.Misc.SupplierManager.Fields.Name"] = "Name",
                ["Plugins.Misc.SupplierManager.Fields.Email"] = "Email",
                ["Plugins.Misc.SupplierManager.Fields.PhoneNumber"] = "Phone number",
                ["Plugins.Misc.SupplierManager.Fields.Address"] = "Address",

                // Search Field Labels
                ["Plugins.Misc.SupplierManager.Fields.SearchName"] = "Search by name",
                ["Plugins.Misc.SupplierManager.Fields.SearchEmail"] = "Search by email",
                ["Plugins.Misc.SupplierManager.Fields.SearchPhoneNumber"] = "Search by phone number",
                ["Plugins.Misc.SupplierManager.Fields.SearchAddress"] = "Search by address",

                // Field Hints
                ["Plugins.Misc.SupplierManager.Fields.Name.Hint"] = "Enter the full name of the supplier.",
                ["Plugins.Misc.SupplierManager.Fields.Email.Hint"] = "Enter a valid email address for the supplier. This will be used for communication.",
                ["Plugins.Misc.SupplierManager.Fields.PhoneNumber.Hint"] = "Enter the supplier's contact phone number. Include country code if applicable.",
                ["Plugins.Misc.SupplierManager.Fields.Address.Hint"] = "Enter the complete address of the supplier including street, city, state/province, and postal code.",

                // Validation Messages
                ["Plugins.Misc.SupplierManager.Fields.Name.Required"] = "Name is required",
                ["Plugins.Misc.SupplierManager.Fields.Email.Required"] = "Email is required",
                ["Plugins.Misc.SupplierManager.Fields.Email.WrongEmail"] = "Wrong email",
                ["Plugins.Misc.SupplierManager.Fields.PhoneNumber.Required"] = "Phone number is required",
                ["Plugins.Misc.SupplierManager.Fields.Address.Required"] = "Address is required",

                // Card Titles and Headers
                ["Plugins.Misc.SupplierManager.Card.SupplierInfo"] = "Supplier Information",
                ["Plugins.Misc.SupplierManager.Card.Search"] = "Search Suppliers",
                ["Plugins.Misc.SupplierManager.Card.List"] = "Supplier List",

                // Search Field Hints
                ["Plugins.Misc.SupplierManager.Fields.SearchName.Hint"] = "Search by supplier name. You can enter partial name.",
                ["Plugins.Misc.SupplierManager.Fields.SearchEmail.Hint"] = "Search by supplier email address.",
                ["Plugins.Misc.SupplierManager.Fields.SearchPhoneNumber.Hint"] = "Search by supplier phone number.",
                ["Plugins.Misc.SupplierManager.Fields.SearchAddress.Hint"] = "Search by supplier address. You can enter partial address."
            };

            foreach (var resource in resources)
            {
                if (isInstall)
                    await _localizationService.AddOrUpdateLocaleResourceAsync(resource.Key, resource.Value);
                else
                    await _localizationService.DeleteLocaleResourceAsync(resource.Key);
            }
        }

        public override async Task InstallAsync()
        {
            //Install localization resources
            await ManageLocalizationResourcesAsync(true);
            
            await base.InstallAsync();
        }

        public override async Task UninstallAsync()
        {
            //Delete plugin resources
            await ManageLocalizationResourcesAsync(false);
            
            await base.UninstallAsync();
        }

        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/SupplierManager/Configure";
        }
    }

    public class EventConsumer : IConsumer<AdminMenuCreatedEvent>
    {
        public Task HandleEventAsync(AdminMenuCreatedEvent eventMessage)
        {
            eventMessage.RootMenuItem.InsertBefore("Local plugins",
                new AdminMenuItem
                {
                    SystemName = "Misc.SupplierManager",
                    Title = "Supplier Manager",
                    Url = eventMessage.GetMenuItemUrl("SupplierManager", "List"),
                    IconClass = "far fa-dot-circle",
                    Visible = true,
                });

            return Task.CompletedTask;
        }
    }
} 