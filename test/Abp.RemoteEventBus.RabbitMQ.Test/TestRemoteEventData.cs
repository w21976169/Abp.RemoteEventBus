using System;
using Abp.RemoteEventBus.EventDatas;

namespace Abp.RemoteEventBus.RabbitMQ.Test
{
    [Serializable]
    public class TestRemoteEventData: RemoteEventData
    {
        public string Name { get; set; }
    }
}
