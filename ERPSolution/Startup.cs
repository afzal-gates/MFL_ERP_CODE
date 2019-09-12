using ERPSolution;
using ERPSolution.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using Hangfire;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Hangfire.SqlServer;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Web.Http.Filters;
using Hangfire.Dashboard;
using FluentScheduler;


[assembly: OwinStartup(typeof(ERPSolution.Startup))]
namespace ERPSolution
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
            HttpConfiguration config = new HttpConfiguration();
            //config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(
            //               new IsoDateTimeConverter());

            // Create Json.Net formatter serializing DateTime using the ISO 8601 format
            config.Filters.Add(new ExternalReqAuthorizeAttribute());

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            serializerSettings.Converters.Add(new IsoDateTimeConverter());
            config.EnableCors();
 
            //GlobalConfiguration.Configuration.Formatters[0] =  new  JsonNetFormatter(serializerSettings);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            WebApiConfig.Register(config);
            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.MapSignalR();
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }

    }
}