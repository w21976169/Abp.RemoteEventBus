using System;
using System.Threading.Tasks;
using Camc.Abp.RemoteEventBus.EventDatas;

namespace Camc.Abp.RemoteEventBus.Managers
{
    public interface IRemoteEventPublisher: IDisposable
    {
        void Publish(IRemoteEventData remoteEventData);

        Task PublishAsync(IRemoteEventData remoteEventData);
    }
}
