using Microsoft.Extensions.Configuration;

namespace Mindr.OpenIDConnect
{
    public static class ConfigurationExtention
    {
        public static void SetDevelopmentAppsettings(this IConfigurationBuilder configurationManager, string environment)
        {
#if DEBUG_LOCAL
            configurationManager.AddJsonFile($"appsettings.{environment}_Local.json", optional: true, reloadOnChange: true);
#elif DEBUG_TEST
        configurationManager.AddJsonFile($"appsettings.{environment}_Test.json", optional: true, reloadOnChange: true);
#endif
        }

    }
}
