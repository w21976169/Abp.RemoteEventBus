using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.RemoteEventBus.Configuration;

namespace Abp.RemoteEventBus.RabbitMQ.Test
{
    [DependsOn(typeof(AbpRemoteEventBusRabbitMQModule))]
    public class RabbitMQTestModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RabbitMQTestModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            Configuration.Modules.RemoteEventBus().UseRabbitMQ().Configure(setting =>
            {
                setting.Url = "amqp://guest:guest@114.116.213.181:5672/";
                setting.ClientName = "ZTTest";
            });

            Configuration.Modules.RemoteEventBus().AutoSubscribe();
        }
    }
}