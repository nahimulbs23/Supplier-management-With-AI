using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.WelcomeMessage.Models
{
    public class WelcomeMessageSettings : ISettings
    {
        public bool Enabled { get; set; }
        public string Message { get; set; }
    }
} 