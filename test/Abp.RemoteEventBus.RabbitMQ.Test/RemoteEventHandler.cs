using Abp.Dependency;
using Abp.RemoteEventBus.Handlers;
using Castle.Core.Logging;

namespace Abp.RemoteEventBus.RabbitMQ.Test
{
    public class RemoteEventHandler : IRemoteEventHandler<TestRemoteEventData>, IRemoteEventHandler<Test1RemoteEventData>, ITransientDependency
    {
        public void HandleEvent(TestRemoteEventData eventData)
        {

        }

        public void HandleEvent(Test1RemoteEventData eventData)
        {

        }
    }
}