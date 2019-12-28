# Camc.Abp.RemoteEventBus
## 起因

由于公司在使用 abp 做中台服务(还没有使用 abp vnext )，需要 远程消息;

本项目是在 参考了 [wuyi6216/Abp.RemoteEventBus](https://github.com/wuyi6216/Abp.RemoteEventBus) 和 [Volo.Abp.EventBus.RabbitMQ](https://github.com/abpframework/abp/tree/dev/framework/src/Volo.Abp.EventBus.RabbitMQ) 改进的;

## 特别感谢



@[Wuyi6216](https://github.com/wuyi6216)

## 安装

注：我这里暂时只发布了 4.4.0 版本，对应 abp 4.4.0，需要其他版本可以直接在源码修改，或者联系我

	- qq：21976169
	- wechat：wqj21976169
	- tel：17695712131

```
Install-Package Camc.Abp.RemoteEventBus -Version 4.4.0
```

```
Install-Package Camc.Abp.RemoteEventBus.RabbitMQ -Version 4.4.0
```

## 使用

### RemoteEventData

```c#
[Serializable]
public class TestRemoteEventData: RemoteEventData
{
	public string Name { get; set; }
}

[Serializable]
public class Test1RemoteEventData: RemoteEventData
{
	public string Name { get; set; }
}
```

### RemoteEventHandler

```c#
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
```

### Configuration

```c#
[DependsOn(typeof(CamcAbpRemoteEventBusRabbitMQModule))]
public class RabbitMQTestModule : AbpModule
{
	public override void Initialize()
    {
       IocManager.RegisterAssemblyByConvention(typeof(RabbitMQTestModule).GetAssembly());
    }
    
    public override void PostInitialize()
    {
        Configuration.Modules.RemoteEventBus().UseRabbitMQ().Configure(setting =>
        {
            setting.Url = "amqp://guest:guest@127.0.0.1:5672/";
            setting.ClientName = "ZTTest";
        });

        Configuration.Modules.RemoteEventBus().AutoSubscribe();
    }
}
```

### Publish

```c#
// TODO:  依赖注入 _remoteEventBus(构造方法注入, 属性注入,IocManager解析)

_remoteEventBus.Publish(new TestRemoteEventData(){ Name = "TestMessage" });
_remoteEventBus.Publish(new Test1RemoteEventData(){ Name = "Test1Message" });

```

### Subscribe

```C#
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
```

### Demo
See [Camc.Abp.RemoteEventBus.RabbitMQ.Test](test/Abp.RemoteEventBus.RabbitMQ.Test)
