using System.Text;
using System.Threading.Tasks;
using Camc.Abp.RemoteEventBus.EventDatas;
using Camc.Abp.RemoteEventBus.Managers;
using Commons.Pool;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Camc.Abp.RemoteEventBus.RabbitMQ
{
    public class RabbitMQRemoteEventPublisher : IRemoteEventPublisher
    {
        private readonly IRabbitMqEventBusOptions _rabbitMqEventBusOptions;

        private readonly IObjectPool<IConnection> _connectionPool;


        private bool _disposed;

        public RabbitMQRemoteEventPublisher(
            IPoolManager poolManager, 
            IRabbitMqEventBusOptions rabbitMqEventBusOptions
        )
        {
            _rabbitMqEventBusOptions = rabbitMqEventBusOptions;

            _connectionPool = poolManager.NewPool<IConnection>()
                .WithFactory(new PooledObjectFactory(rabbitMqEventBusOptions))
                                    .Instance();
        }

        public void Publish(IRemoteEventData remoteEventData)
        {
            var routingKey = remoteEventData.GetType().FullName;

            var connection = _connectionPool.Acquire();
            try
            {
                var channel = connection.CreateModel();
                channel.ExchangeDeclare(_rabbitMqEventBusOptions.ExchangeName, "direct", true);
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(remoteEventData));
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                channel.BasicPublish(_rabbitMqEventBusOptions.ExchangeName, routingKey, properties, body);
            }
            finally
            {
                _connectionPool.Return(connection);
            }
        }

        public Task PublishAsync(IRemoteEventData remoteEventData)
        {
            return Task.Factory.StartNew(() =>
            {
                Publish(remoteEventData);
            });
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _connectionPool.Dispose();

                _disposed = true;
            }
        }
    }
}
