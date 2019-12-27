using System;

namespace Camc.Abp.RemoteEventBus.RabbitMQ
{
    public interface IRabbitMQConfiguration
    {
        IRabbitMQConfiguration Configure(Action<IRabbitMqEventBusOptions> configureAction);

        IRabbitMQConfiguration Configure(IRabbitMqEventBusOptions eventBusOptions);
    }
}
