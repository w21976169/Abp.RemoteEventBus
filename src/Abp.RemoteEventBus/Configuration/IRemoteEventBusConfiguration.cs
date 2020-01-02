using Abp.Configuration.Startup;

namespace Abp.RemoteEventBus.Configuration
{
    public interface IRemoteEventBusConfiguration
    {
        IAbpStartupConfiguration AbpStartupConfiguration { get; }

        IRemoteEventBusConfiguration AutoSubscribe();
    }
}