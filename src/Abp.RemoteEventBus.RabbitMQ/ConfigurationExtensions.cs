using System;
using Abp.Dependency;
using Camc.Abp.RemoteEventBus.Configuration;
using Camc.Abp.RemoteEventBus.Managers;
using Castle.MicroKernel.Registration;

namespace Camc.Abp.RemoteEventBus.RabbitMQ
{
    public static class ConfigurationExtensions
    {
        public static IRabbitMQConfiguration UseRabbitMQ(this IRemoteEventBusConfiguration configuration)
        {
            var iocManager = configuration.AbpStartupConfiguration.IocManager;
            
            iocManager.IocContainer.Register(
                Component.For<IRemoteEventPublisher>().ImplementedBy<RabbitMQRemoteEventPublisher>()
                    .LifestyleSingleton().IsDefault()
            );
            iocManager.IocContainer.Register(
                Component.For<IRemoteEventSubscriber>().ImplementedBy<RabbitMQRemoteEventSubscriber>()
                    .LifestyleSingleton().IsDefault()
            );
            iocManager.IocContainer.Register(
                Component.For<IRemoteEventBus>()
                    .ImplementedBy<Camc.Abp.RemoteEventBus.Managers.RemoteEventBus>()
                    .Named(Guid.NewGuid().ToString())
                    .LifestyleSingleton()
                    .IsDefault()
            );

            iocManager.RegisterIfNot<IRabbitMQConfiguration, RabbitMQConfiguration>();

            return iocManager.Resolve<IRabbitMQConfiguration>();
        }
    }
}