using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abp.RemoteEventBus
{
    public interface IRemoteEventPublisher: IDisposable
    {
        void Publish(IRemoteEventData remoteEventData);

        Task PublishAsync(IRemoteEventData remoteEventData);
    }
}
