using System;
using Abp.Events.Bus;

namespace Abp.RemoteEventBus.Impl
{
    [Serializable]
    public class RemoteEventData : EventData, IRemoteEventData
    {

    }
}
