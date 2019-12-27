# Abp.RemoteEventBus
## What’s this?

受到[Abp](https://github.com/FJQBT/ABP)事件总线的灵感而开发的一个分布式的事件总线，可以跨应用触发事件。基于发布/订阅模式，消息的传递可以通过redis，rabbitmq，kafka等实现。你还能非常容易的实现你自己的方式，通过实现指定接口。支持控制台和web应用。

A distributed event bus, inspired by the [Abp](https://github.com/FJQBT/ABP) event bus, can trigger events across applications. Based on publish / subscribe mode, the message can be passed through redis, rabbitmq, kafka and so on. You can also very easily implement your own way by implementing the specified interface. Support in console and web applications.

## How to use?

### 

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

### 



### Publish

```c#
    var eventDate = new RemoteEventData("Type_Test")
        {
            Data ={["playload"]=DateTime.Now}
        };
    remoteEventBus.Publish("Topic_Test", eventDate);

```

### Subscribe

```C#
    [RemoteEventHandler(ForType = "Type_Test", ForTopic = "Topic_Test")]
    public class RemoteEventHandler : IRemoteEventHandler, ITransientDependency
    {
        public void HandleEvent(RemoteEventArgs eventArgs)
        {
            Logger.Info("receive " + eventArgs.EventData.Data["playload"]);
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


### Demo
See [Abp.RemoteEventBus.RabbitMQ.Test](test/Abp.RemoteEventBus.RabbitMQ.Test)

## How it work?
[Here](doc/How%20it%20work.md)