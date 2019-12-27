using Abp.Modules;
using Abp.Reflection.Extensions;
using Commons.Pool;

namespace Camc.Abp.RemoteEventBus.RabbitMQ
{
    [DependsOn(typeof(CamcAbpRemoteEventBusModule))]
    public class CamcAbpRemoteEventBusRabbitMQModule: AbpModule
    {
        public override void Initialize()
        {
            IocManager.Register<IPoolManager, PoolManager>();
            IocManager.RegisterAssemblyByConvention(typeof(CamcAbpRemoteEventBusRabbitMQModule).GetAssembly());
        }
    }
}