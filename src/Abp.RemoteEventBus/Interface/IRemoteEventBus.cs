using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.RemoteEventBus
{
    public interface IRemoteEventBus : IDisposable
    {
        void MessageHandle(string topic, string message);

        void Publish(IRemoteEventData eventData);

        Task PublishAsync(IRemoteEventData eventData);

        void Subscribe(string topic);

        void Subscribe(IEnumerable<string> topics);

        Task SubscribeAsync(string topic);

        Task SubscribeAsync(IEnumerable<string> topics);

        void Unsubscribe(string topic);

        void Unsubscribe(IEnumerable<string> topics);

        Task UnsubscribeAsync(string topic);

        Task UnsubscribeAsync(IEnumerable<string> topics);

        void UnsubscribeAll();

        Task UnsubscribeAllAsync();
    }
}