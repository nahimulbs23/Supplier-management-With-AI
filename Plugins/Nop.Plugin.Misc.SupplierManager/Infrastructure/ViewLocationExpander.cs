using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;
using System.Linq;
using Nop.Web.Framework.Infrastructure.Extensions;

namespace Nop.Plugin.Misc.SupplierManager.Infrastructure
{
    public class ViewLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            // No values need to be populated
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context.AreaName == "Admin")
            {
                viewLocations = new[]
                {
                    $"/Plugins/Misc.SupplierManager/Views/{{1}}/{{0}}.cshtml",
                    $"/Plugins/Misc.SupplierManager/Views/{{0}}.cshtml"
                }.Concat(viewLocations);
            }
            else
            {
                viewLocations = new[]
                {
                    $"/Plugins/Misc.SupplierManager/Views/{{1}}/{{0}}.cshtml",
                    $"/Plugins/Misc.SupplierManager/Views/{{0}}.cshtml"
                }.Concat(viewLocations);
            }

            return viewLocations;
        }
    }
} 