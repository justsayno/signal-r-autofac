using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Autofac.Integration.SignalR;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Server.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Server
{
    public static class AutofacConfiguration
    {
        public static IContainer RegisterDependencies()
        {
            // Create your builder.
            var builder = new ContainerBuilder();

            //register controllers in DI
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register SignalR hubs
            builder.RegisterHubs(Assembly.GetExecutingAssembly());

            return builder.Build();
        }
    }
}