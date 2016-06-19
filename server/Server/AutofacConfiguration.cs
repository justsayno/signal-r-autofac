using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Server.Api;
using System.Reflection;

namespace Server
{
    public class AutofacConfiguration
    {
        public static IContainer RegisterDependencies()
        {
            // Create your builder.
            var builder = new ContainerBuilder();

            //register controllers in DI
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            

            // Register SignalR hubs and dependencies
            builder.RegisterType<Autofac.Integration.SignalR.AutofacDependencyResolver>()
                .As<Microsoft.AspNet.SignalR.IDependencyResolver>()
                .SingleInstance();

            builder.Register((context, p) =>
                    context.Resolve<Microsoft.AspNet.SignalR.IDependencyResolver>()
                        .Resolve<IConnectionManager>()
                        .GetHubContext<EventHub>()).ExternallyOwned();


            return builder.Build();
        }
    }
}
