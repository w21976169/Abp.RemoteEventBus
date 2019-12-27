using System;
using System.Threading.Tasks;
using Abp.RemoteEventBus.EventDatas;

namespace Abp.RemoteEventBus.Managers
{
    public interface IRemoteEventPublisher: IDisposable
    {
        void Publish(IRemoteEventData remoteEventData);

        Task PublishAsync(IRemoteEventData remoteEventData);
    }
}
