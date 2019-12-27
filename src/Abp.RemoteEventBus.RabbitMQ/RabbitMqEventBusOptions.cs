using Abp.Dependency;

namespace Camc.Abp.RemoteEventBus.RabbitMQ
{
    public class RabbitMqEventBusOptions : IRabbitMqEventBusOptions, ITransientDependency
    {
        public string Url { get; set; }
        public string ClientName { get; set; }
        public string ExchangeName { get; set; }

        public RabbitMqEventBusOptions()
        {
            ExchangeName = "RemoteEventBusMessages";
        }
    }
}
