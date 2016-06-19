using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System.Web.Http;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(Server.Startup))]
namespace Server
{
    public class Startup
    {
        public static IContainer Container { get; set; }
        public void Configuration(IAppBuilder app)
        {
            var config = GlobalConfiguration.Configuration;

            // configure IoC container
            Container = AutofacConfiguration.RegisterDependencies();

            //set dependency resolver from WebAPI and MVC
            config.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
            DependencyResolver.SetResolver(new Autofac.Integration.Mvc.AutofacDependencyResolver(Container));

            //register Autofac Middleware
            app.UseAutofacMiddleware(Container);

            app.Map("/signalr", a =>
            {
                a.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    Resolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(Container)
                };
                a.RunSignalR(hubConfiguration);
            });

            // This server will be accessed by clients from other domains, so
            //  we open up CORS
            //
            app.UseCors(CorsOptions.AllowAll);

            // Build up the WebAPI middleware
            //
            var httpConfig = new HttpConfiguration();
            httpConfig.MapHttpAttributeRoutes();
            app.UseWebApi(httpConfig);
        }

    }
}
