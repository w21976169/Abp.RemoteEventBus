using System;
using Abp.Events.Bus;

namespace Abp.RemoteEventBus.EventDatas
{
    [Serializable]
    public class RemoteEventData : EventData, IRemoteEventData
    {

    }
}
