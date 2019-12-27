using System;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Castle.MicroKernel.Registration;

namespace Abp.RemoteEventBus.RabbitMQ
{
    public class RabbitMQConfiguration : IRabbitMQConfiguration, ISingletonDependency
    {
        private readonly IAbpStartupConfiguration _configuration;

        public RabbitMQConfiguration(IAbpStartupConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IRabbitMQConfiguration Configure(Action<IRabbitMqEventBusOptions> configureAction)
        {
            _configuration.IocManager.RegisterIfNot<IRabbitMqEventBusOptions, RabbitMqEventBusOptions>();

            var setting = _configuration.IocManager.Resolve<IRabbitMqEventBusOptions>();
            configureAction(setting);

            Configure(setting);

            return this;
        }


        public IRabbitMQConfiguration Configure(IRabbitMqEventBusOptions eventBusOptions)
        {
            _configuration.IocManager.IocContainer.Register(
                 Component.For<IRemoteEventPublisher>()
                    .ImplementedBy<RabbitMQRemoteEventPublisher>()
                    .DependsOn(Castle.MicroKernel.Registration.Dependency.OnValue<IRabbitMqEventBusOptions>(eventBusOptions))
                    .Named(Guid.NewGuid().ToString())
                    .LifestyleSingleton()
                    .IsDefault()
            );

            _configuration.IocManager.IocContainer.Register(
                 Component.For<IRemoteEventSubscriber>()
                    .ImplementedBy<RabbitMQRemoteEventSubscriber>()
                    .DependsOn(Castle.MicroKernel.Registration.Dependency.OnValue<IRabbitMqEventBusOptions>(eventBusOptions))
                    .Named(Guid.NewGuid().ToString())
                    .LifestyleSingleton()
                    .IsDefault()
            );

            return this;
        }
    }
}
