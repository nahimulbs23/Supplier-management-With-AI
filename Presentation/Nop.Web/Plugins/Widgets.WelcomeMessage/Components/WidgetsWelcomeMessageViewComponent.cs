using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.WelcomeMessage.Components
{
    [ViewComponent(Name = "WidgetsWelcomeMessage")]
    public class WidgetsWelcomeMessageViewComponent : NopViewComponent
    {
        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            return View("~/Plugins/Widgets.WelcomeMessage/Views/PublicInfo.cshtml", "Welcome to our store!");
        }
    }
} 