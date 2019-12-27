namespace Abp.RemoteEventBus.Handlers
{
    public interface IRemoteEventHandler<in TRemoteEventData>
    {
        void HandleEvent(TRemoteEventData eventData);
    }
}