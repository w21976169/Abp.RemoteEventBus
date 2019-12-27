namespace Abp.RemoteEventBus.RabbitMQ
{
    public interface IRabbitMqEventBusOptions
    {
        string Url { get; set; }

        string ClientName { get; set; }

        string ExchangeName { get; set; }
    }
}