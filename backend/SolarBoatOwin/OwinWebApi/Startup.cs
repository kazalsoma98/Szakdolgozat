using Microsoft.Owin.Cors;
using Owin;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http;

namespace OwinWebApi
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            CorsPolicy corsPolicy = new CorsPolicy()
            {
                AllowAnyHeader = true,
                AllowAnyMethod = true,
                SupportsCredentials = true
            };
            string frontendhost = ConfigurationManager.AppSettings["frontendhost"];
            corsPolicy.Origins.Add(frontendhost);
            

            CorsPolicyProvider policyProvider = new CorsPolicyProvider()
            {
                PolicyResolver = (context) => Task.FromResult(corsPolicy)
            };
            CorsOptions corsOptions = new CorsOptions()
            {
                PolicyProvider = policyProvider
            };

            appBuilder.UseCors(corsOptions);
            appBuilder.UseWebApi(config);
        }
    }
}