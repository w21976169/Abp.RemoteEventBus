using System;
using Commons.Pool;
using RabbitMQ.Client;

namespace Abp.RemoteEventBus.RabbitMQ
{
    public class PooledObjectFactory : IPooledObjectFactory<IConnection>
    {
        private ConnectionFactory _connectionFactory;

        public PooledObjectFactory(IRabbitMqEventBusOptions rabbitMqEventBusOptions)
        {
            Check.NotNullOrWhiteSpace(rabbitMqEventBusOptions.Url, "Url");
            _connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(rabbitMqEventBusOptions.Url),
                AutomaticRecoveryEnabled = true
            };
        }

        public IConnection Create()
        {
            return _connectionFactory.CreateConnection();
        }

        public void Destroy(IConnection obj)
        {
            obj.Dispose();
        }
    }
}
