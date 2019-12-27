using System;
using Camc.Abp.RemoteEventBus.EventDatas;

namespace Camc.Abp.RemoteEventBus.RabbitMQ.Test
{
    [Serializable]
    public class Test1RemoteEventData: RemoteEventData
    {
        public string Name { get; set; }
    }
}
