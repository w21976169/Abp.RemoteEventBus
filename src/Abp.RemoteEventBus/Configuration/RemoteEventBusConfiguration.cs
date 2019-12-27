using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Reflection;
using Abp.RemoteEventBus.Configuration;
using Abp.RemoteEventBus.Managers;
using Castle.MicroKernel.Registration;
using Castle.Core.Logging;

namespace Abp.RemoteEventBus.RabbitMQ
{
    public class RemoteEventBusConfiguration : IRemoteEventBusConfiguration, ISingletonDependency
    {
        private readonly IAbpStartupConfiguration _configuration;
        private readonly TypeFinder _typeFinder;

        public ILogger Logger { get; set; }

        public IAbpStartupConfiguration AbpStartupConfiguration
        {
            get { return _configuration; }
        }

        public RemoteEventBusConfiguration(IAbpStartupConfiguration configuration, TypeFinder typeFinder)
        {
            _configuration = configuration;
            _typeFinder = typeFinder;

            Logger = NullLogger.Instance;
        }

        public IRemoteEventBusConfiguration AutoSubscribe()
        {
            var topics = new List<string>();
            var types = _typeFinder.Find(type =>
                    type.GetInterfaces().Any(e => e.Name.Contains("IRemoteEventHandler")))
                .ToList();

            foreach (var type in types)
            {
                var iTypes = type.GetInterfaces().Where(e => e.Name.Contains("IRemoteEventHandler")).ToList();

                foreach (var iType in iTypes)
                {
                    if (iType != null)
                    {
                        var gType = iType.GetGenericArguments().FirstOrDefault();
                        if (gType != null) topics.Add(gType.Name);
                    }

                }

            }

            Logger.Info($"auto subscribe topics {string.Join(",", topics)}");

            var remoteEventBus = _configuration.IocManager.Resolve<IRemoteEventBus>();
            remoteEventBus.Subscribe(topics);

            return this;
        }
    }
}