using Abp.Configuration.Startup;

namespace Abp.RemoteEventBus.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IRemoteEventBusConfiguration RemoteEventBus(this IModuleConfigurations configuration)
        {
            return configuration.AbpConfiguration.GetOrCreate("Modules.Abp.RemoteEventBus", () => configuration.AbpConfiguration.IocManager.Resolve<IRemoteEventBusConfiguration>());
        }
    }
}
