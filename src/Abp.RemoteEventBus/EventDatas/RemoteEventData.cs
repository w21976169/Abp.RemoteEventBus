using System;
using Abp.Events.Bus;

namespace Camc.Abp.RemoteEventBus.EventDatas
{
    [Serializable]
    public class RemoteEventData : EventData, IRemoteEventData
    {

    }
}
