using Microsoft.Extensions.DependencyInjection;

namespace TestHalcyonApi
{
    internal class AppConfig
    {
        public AppConfig()
        {
        }

        public string BaseUrl { get; set; } = HalcyonConventionOptions.HostVariable;
    }
}