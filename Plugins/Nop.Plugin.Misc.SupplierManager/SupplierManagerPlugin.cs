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

        public override async Task InstallAsync()
        {
            //Install localization resources
            await _localizationService.AddOrUpdateLocaleResourceAsync("Plugins.Misc.SupplierManager.MenuTitle", "Supplier Manager");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Plugins.Misc.SupplierManager.WelcomeMessage", "Welcome to Supplier Manager Plugin");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Plugins.Misc.SupplierManager.Added", "The supplier has been added successfully");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Plugins.Misc.SupplierManager.Updated", "The supplier has been updated successfully");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Plugins.Misc.SupplierManager.Deleted", "The supplier has been deleted successfully");
            
            await base.InstallAsync();
        }

        public override async Task UninstallAsync()
        {
            //Delete plugin resources
            await _localizationService.DeleteLocaleResourceAsync("Plugins.Misc.SupplierManager.MenuTitle");
            await _localizationService.DeleteLocaleResourceAsync("Plugins.Misc.SupplierManager.WelcomeMessage");
            await _localizationService.DeleteLocaleResourceAsync("Plugins.Misc.SupplierManager.Added");
            await _localizationService.DeleteLocaleResourceAsync("Plugins.Misc.SupplierManager.Updated");
            await _localizationService.DeleteLocaleResourceAsync("Plugins.Misc.SupplierManager.Deleted");
            
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