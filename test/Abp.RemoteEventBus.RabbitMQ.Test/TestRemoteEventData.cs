using System;
using Camc.Abp.RemoteEventBus.EventDatas;

namespace Camc.Abp.RemoteEventBus.RabbitMQ.Test
{
    [Serializable]
    public class TestRemoteEventData: RemoteEventData
    {
        public string Name { get; set; }
    }
}
