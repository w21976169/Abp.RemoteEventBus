using Abp.Configuration.Startup;

namespace Camc.Abp.RemoteEventBus.Configuration
{
    public interface IRemoteEventBusConfiguration
    {
        IAbpStartupConfiguration AbpStartupConfiguration { get; }

        IRemoteEventBusConfiguration AutoSubscribe();
    }
}