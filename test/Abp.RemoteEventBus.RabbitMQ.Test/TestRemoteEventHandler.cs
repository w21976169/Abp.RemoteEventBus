using Abp.Dependency;
using Camc.Abp.RemoteEventBus.Handlers;

namespace Camc.Abp.RemoteEventBus.RabbitMQ.Test
{
    public class TestRemoteEventHandler : IRemoteEventHandler<TestRemoteEventData>,
        IRemoteEventHandler<Test1RemoteEventData>, ITransientDependency
    {
        public void HandleEvent(TestRemoteEventData eventData)
        {
            // TODO： 处理类型为 TestRemoteEventData 消息
        }

        public void HandleEvent(Test1RemoteEventData eventData)
        {
            // TODO： 处理类型为  Test1RemoteEventData 消息
        }
    }
}