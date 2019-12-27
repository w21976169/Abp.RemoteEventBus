using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.RemoteEventBus.Managers;

namespace Abp.RemoteEventBus
{
    public class AbpRemoteEventBusModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpRemoteEventBusModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.RegisterIfNot<IRemoteEventBus, NullRemoteEventBus>();
        }
    }
}