﻿using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Events.Bus;
using Abp.Reflection;
using Abp.RemoteEventBus.Handlers;
using Abp.RemoteEventBus.Impl;
using Newtonsoft.Json;

namespace Abp.RemoteEventBus
{
    public class RemoteEventBus : IRemoteEventBus
    {
        public ILogger Logger { get; set; }

        private readonly IEventBus _eventBus;
        private readonly IRemoteEventPublisher _publisher;
        private readonly IRemoteEventSubscriber _subscriber;
        private readonly IRemoteEventTopicSelector _topicSelector;
        private readonly IRemoteEventSerializer _remoteEventSerializer;
        private readonly IIocResolver _iocResolver;
        private readonly TypeFinder _typeFinder;

        private bool _disposed;

        public RemoteEventBus(
            IEventBus eventBus,
            IRemoteEventPublisher publisher,
            IRemoteEventSubscriber subscriber,
            IRemoteEventTopicSelector topicSelector,
            IRemoteEventSerializer remoteEventSerializer, IIocResolver iocResolver, TypeFinder typeFinder)
        {
            _eventBus = eventBus;
            _publisher = publisher;
            _subscriber = subscriber;
            _topicSelector = topicSelector;
            _remoteEventSerializer = remoteEventSerializer;
            _iocResolver = iocResolver;
            _typeFinder = typeFinder;

            Logger = NullLogger.Instance;
        }


        public void Publish(IRemoteEventData eventData)
        {
            _publisher.Publish(eventData);
        }

        public async Task PublishAsync(IRemoteEventData eventData)
        {
            await _publisher.PublishAsync(eventData);
            await Task.FromResult(0);
        }

        public void Subscribe(string topic)
        {
            Subscribe(new[] {topic});
        }

        public Task SubscribeAsync(string topic)
        {
            return SubscribeAsync(new[] {topic});
        }

        public void Subscribe(IEnumerable<string> topics)
        {
            _subscriber.Subscribe(topics, MessageHandle);
            Logger.Debug($"Subscribed topics {string.Join(",", topics)}");
        }

        public Task SubscribeAsync(IEnumerable<string> topics)
        {
            return _subscriber.SubscribeAsync(topics, MessageHandle)
                .ContinueWith((s) => Logger.Debug($"Subscribed topics {string.Join(",", topics)}"));
        }

        public void Unsubscribe(string topic)
        {
            Unsubscribe(new[] {topic});
        }

        public Task UnsubscribeAsync(string topic)
        {
            return UnsubscribeAsync(new[] {topic});
        }

        public void Unsubscribe(IEnumerable<string> topics)
        {
            _subscriber.Unsubscribe(topics);
            Logger.Debug($"Unsubscribed topics {string.Join(",", topics)}");
        }

        public Task UnsubscribeAsync(IEnumerable<string> topics)
        {
            return _subscriber.UnsubscribeAsync(topics)
                .ContinueWith((s) => Logger.Debug($"Unsubscribed topics {string.Join(",", topics)}"));
        }

        public virtual void MessageHandle(string topic, string message)
        {
            // TODO: 触发对应的 Handler
            var dataType = _typeFinder.Find(type => type.Name == topic).FirstOrDefault();

            var eventData = JsonConvert.DeserializeObject(message, dataType);

            var handleType = _typeFinder.Find(type => type.GetInterfaces().Any(e =>
                    e.Name.Contains("IRemoteEventHandler") && e.GetGenericArguments().Any(e1 => e1.Name == topic)))
                .FirstOrDefault();

            var handler = _iocResolver.Resolve(handleType);

            var handlerMethod = handleType.GetMethods().FirstOrDefault(e =>
                e.Name == "HandleEvent" && e.GetParameters().Length == 1 &&
                e.GetParameters().First().ParameterType.Name == topic);

            handlerMethod.Invoke(handler, new object[] {eventData});
        }

        public void UnsubscribeAll()
        {
            _subscriber.UnsubscribeAll();
            Logger.Debug($"Unsubscribed all topics");
        }

        public Task UnsubscribeAllAsync()
        {
            return _subscriber.UnsubscribeAllAsync()
                .ContinueWith((s) => Logger.Debug($"Unsubscribes all topics"));
            ;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                _subscriber?.Dispose();
                _publisher?.Dispose();

                _disposed = true;
            }
        }
    }
}