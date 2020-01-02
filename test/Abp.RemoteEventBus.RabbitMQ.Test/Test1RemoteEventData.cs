using System;
using Abp.RemoteEventBus.EventDatas;

namespace Abp.RemoteEventBus.RabbitMQ.Test
{
    [Serializable]
    public class Test1RemoteEventData: RemoteEventData
    {
        public string Name { get; set; }
    }
}
