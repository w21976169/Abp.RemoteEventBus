using System;
using Abp.RemoteEventBus.EventDatas;

namespace Abp.RemoteEventBus.Messages
{
    [Serializable]
    public class TestRemoteEventData: RemoteEventData
    {
        public string Name { get; set; }
    }
}
