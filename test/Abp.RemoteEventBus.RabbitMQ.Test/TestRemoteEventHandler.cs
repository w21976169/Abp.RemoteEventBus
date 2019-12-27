using Abp.Dependency;
using Camc.Abp.RemoteEventBus.Handlers;

namespace Camc.Abp.RemoteEventBus.RabbitMQ.Test
{
    public class TestRemoteEventHandler : IRemoteEventHandler<TestRemoteEventData>,
        IRemoteEventHandler<Test1RemoteEventData>, ITransientDependency
    {
        public void HandleEvent(TestRemoteEventData eventData)
        {
        }

        public void HandleEvent(Test1RemoteEventData eventData)
        {
        }
    }
}