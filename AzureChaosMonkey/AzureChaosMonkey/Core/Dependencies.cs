using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AzureChaosMonkey.Infrastructure.Logging;
using AzureChaosMonkey.Infrastructure.TimeProvider;
using AzureChaosMonkey.Services.Manager;
using IContainer = Autofac.IContainer;

namespace AzureChaosMonkey.Core
{
    public class Dependencies
    {
        public static IContainer Register(ApplicationArguments args)        
        {
            var builder = new ContainerBuilder();   

            builder.RegisterType<ChaosLogger>().As<IChaosLogger>().WithParameter("logFileName", args.Settings.LogFileName).SingleInstance();
            builder.RegisterType<TimeProvider>().As<ITimeProvider>().SingleInstance();  
            builder.RegisterType<ChaosManager>().As<IChaosManager>().WithParameter("settings", args.Settings);  

            return builder.Build();
        }
    }
}
