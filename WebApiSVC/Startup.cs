using System.Web.Http;
using Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin;

namespace WebApiSVC
{
    public static class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public static void ConfigureApp(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);

            //add routings for static files:
            var fileSystem = new PhysicalFileSystem(@".\wwwroot");
            var options = new FileServerOptions
            {
                EnableDefaultFiles = true,
                FileSystem = fileSystem,
                RequestPath = PathString.Empty
            };
            options.DefaultFilesOptions.DefaultFileNames = new[] { "index.html" };
            options.StaticFileOptions.FileSystem = fileSystem;
            options.StaticFileOptions.ServeUnknownFileTypes = true;
            options.EnableDirectoryBrowsing = true;

            config.MapHttpAttributeRoutes();
            appBuilder.UseFileServer(options);
        }
    }
}
