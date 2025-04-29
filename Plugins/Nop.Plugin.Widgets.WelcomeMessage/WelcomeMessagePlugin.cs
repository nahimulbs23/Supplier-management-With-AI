using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;
using Nop.Plugin.Widgets.WelcomeMessage.Components;
using Nop.Plugin.Widgets.WelcomeMessage.Models;

namespace Nop.Plugin.Widgets.WelcomeMessage
{
    public class WelcomeMessagePlugin : BasePlugin, IWidgetPlugin
    {
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;

        public WelcomeMessagePlugin(ISettingService settingService, IWebHelper webHelper)
        {
            _settingService = settingService;
            _webHelper = webHelper;
        }

        public bool HideInWidgetList => false;

        public Type GetWidgetViewComponent(string widgetZone)
        {
            return typeof(WidgetsWelcomeMessageViewComponent);
        }

        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageTop });
        }

        public override async Task InstallAsync()
        {
            await _settingService.SaveSettingAsync(new WelcomeMessageSettings
            {
                Enabled = true,
                Message = "Welcome to our store!"
            });

            await base.InstallAsync();
        }

        public override async Task UninstallAsync()
        {
            await _settingService.DeleteSettingAsync<WelcomeMessageSettings>();
            await base.UninstallAsync();
        }
    }
} 