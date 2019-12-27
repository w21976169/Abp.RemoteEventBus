using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.RemoteEventBus.RabbitMQ
{
    public interface IRabbitMQConfiguration
    {
        IRabbitMQConfiguration Configure(Action<IRabbitMqEventBusOptions> configureAction);

        IRabbitMQConfiguration Configure(IRabbitMqEventBusOptions eventBusOptions);
    }
}
