using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.SupplierManager.Factories;
using Nop.Plugin.Misc.SupplierManager.Services;
using Nop.Plugin.Misc.SupplierManager.Infrastructure;

namespace Nop.Plugin.Misc.SupplierManager.Infrastructure
{
    public class Startup : INopStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<ISupplierModelFactory, SupplierModelFactory>();
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ViewLocationExpander());
            });
        }

        public void Configure(IApplicationBuilder application)
        {
        }

        public int Order => 11;
    }
} 