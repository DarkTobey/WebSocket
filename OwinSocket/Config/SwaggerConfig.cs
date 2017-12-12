using System.Web.Http;
using WebActivatorEx;
using OwinSocket;
using Swashbuckle.Application;
using System.Linq;
using System.IO;

namespace OwinSocket.Config
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.EnableSwagger("docs/{apiVersion}", c =>
            {
                c.Schemes(new[] { "http", "https" });
                c.SingleApiVersion("v1", "").Description("");
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.DescribeAllEnumsAsStrings();
                c.IgnoreObsoleteProperties();
                c.UseFullTypeNameInSchemaIds();

                // 传统mvc的xml文件的目录
                //var searchFolder = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin");

                // owin的xml文件的目录
                var searchFolder = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "");

                var xmlPath = Directory.EnumerateFiles(searchFolder, "*.xml", SearchOption.AllDirectories);
                foreach (var xml in xmlPath)
                {
                    c.IncludeXmlComments(xml);
                }

            }).EnableSwaggerUi("sandbox/{*assetPath}");
        }
    }
}
