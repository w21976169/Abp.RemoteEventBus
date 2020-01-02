using Abp.Dependency;

namespace Abp.RemoteEventBus.RabbitMQ
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
