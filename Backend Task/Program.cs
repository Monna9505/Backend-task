using System;
using System.IO;
using System.Reflection;
using Autofac;
using Backend.Services;
using log4net;
using log4net.Config;

namespace Backend
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("Pogram.cs");
        //This is your app entry point
        static void Main(string[] args)
        {
            var container = ConfigureContainer();

            //Get your application menu class
            var application = container.Resolve<ApplicationService>();

            application.Run();
        }

        private static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsSelf().AsImplementedInterfaces();

            return builder.Build();
        }
    }
}
