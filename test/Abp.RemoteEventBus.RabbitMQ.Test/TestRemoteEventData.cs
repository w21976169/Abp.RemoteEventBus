using System;
using System.Collections.Generic;
using System.Text;
using Abp.RemoteEventBus.Impl;

namespace Abp.RemoteEventBus.RabbitMQ.Test
{
    [Serializable]
    public class TestRemoteEventData: RemoteEventData
    {
        public string Name { get; set; }
    }
}
