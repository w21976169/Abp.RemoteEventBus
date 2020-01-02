﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.RemoteEventBus.Managers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Abp.RemoteEventBus.RabbitMQ
{
    public class RabbitMQRemoteEventSubscriber : IRemoteEventSubscriber
    {
        private readonly ConcurrentDictionary<string, IModel> _dictionary;
        private readonly List<IConnection> _connectionsAcquired;
        private readonly PooledObjectFactory _factory;
        private readonly IRabbitMqEventBusOptions _rabbitMqEventBusOptions;

        private bool _disposed;

        public RabbitMQRemoteEventSubscriber(IRabbitMqEventBusOptions rabbitMqEventBusOptions)
        {
            _rabbitMqEventBusOptions = rabbitMqEventBusOptions;
            _factory = new PooledObjectFactory(rabbitMqEventBusOptions);
            _dictionary = new ConcurrentDictionary<string, IModel>();
            _connectionsAcquired = new List<IConnection>();
        }

        public void Subscribe(IEnumerable<string> topics, Action<string, string> handler)
        {
            var existsTopics = topics.ToList().Where(p => _dictionary.ContainsKey(p));
            if (existsTopics.Any())
            {
                throw new AbpException(string.Format("the topics {0} have subscribed already", string.Join(",", existsTopics)));
            }

            foreach (var topic in topics)
            {
                var connection = _factory.Create();
                _connectionsAcquired.Add(connection);
                try
                {
                    var channel = connection.CreateModel();
                    channel.ExchangeDeclare(_rabbitMqEventBusOptions.ExchangeName, "direct", true);
                    channel.QueueDeclare(_rabbitMqEventBusOptions.ClientName, true, false, false, null);
                    channel.QueueBind(_rabbitMqEventBusOptions.ClientName, _rabbitMqEventBusOptions.ExchangeName, topic);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (ch, ea) =>
                    {
                        handler(ea.RoutingKey, Encoding.UTF8.GetString(ea.Body));
                        channel.BasicAck(ea.DeliveryTag, false);
                    };
                    channel.BasicConsume(_rabbitMqEventBusOptions.ClientName, false, consumer);
                    _dictionary[topic] = channel;
                }
                finally
                {
                    _connectionsAcquired.Remove(connection);
                }
            }
        }

        public Task SubscribeAsync(IEnumerable<string> topics, Action<string, string> handler)
        {
            return Task.Factory.StartNew(() => Subscribe(topics, handler));
        }

        public void Unsubscribe(IEnumerable<string> topics)
        {
            foreach (var topic in topics)
            {
                if (_dictionary.ContainsKey(topic))
                {
                    _dictionary[topic].Close();
                    _dictionary[topic].Dispose();
                }
            }
        }

        public Task UnsubscribeAsync(IEnumerable<string> topics)
        {
            return Task.Factory.StartNew(() => Unsubscribe(topics));
        }

        public void UnsubscribeAll()
        {
            Unsubscribe(_dictionary.Select(p => p.Key));
        }

        public Task UnsubscribeAllAsync()
        {
            return Task.Factory.StartNew(UnsubscribeAll);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                UnsubscribeAll();
                foreach (var connection in _connectionsAcquired)
                {
                    _factory.Destroy(connection);
                }

                _disposed = true;
            }
        }
    }
}
