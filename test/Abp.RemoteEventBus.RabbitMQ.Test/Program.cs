using System;
using System.Threading.Tasks;
using Abp.Castle.Logging.Log4Net;
using Abp.RemoteEventBus.Managers;
using Castle.Facilities.Logging;

namespace Abp.RemoteEventBus.RabbitMQ.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var bootstrapper = AbpBootstrapper.Create<RabbitMQTestModule>();

            //bootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(f =>
            //    f.UseAbpLog4Net().WithConfig("log4net.config"));

            bootstrapper.Initialize();
            
            var remoteEventBus=bootstrapper.IocManager.Resolve<IRemoteEventBus>();

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    var eventDate = new Test1RemoteEventData()
                    {
                        Name = "Wynnyo"
                    };
                    remoteEventBus.Publish(eventDate);

                    Task.Delay(30000).Wait();
                }
            });

            Console.WriteLine("Any key exit");
            Console.ReadKey();
        }
    }
}