using System;
using System.CodeDom;
using System.Web.Http;
using System.Xml.XPath;
using Swashbuckle.Application;
using System.Reflection;
using System.Web;

namespace ActiveDirManager
{
  /// <summary>
  /// Represent Swagger configuration.
  /// </summary>
  public class SwaggerConfig
  {
    /// <summary>
    /// Configures Swagger API 
    /// </summary>
    /// <param name="configuration">Instance of <see cref="HttpConfiguration"/>.</param>
    public static void Configure(HttpConfiguration configuration)
    {
      configuration
          .EnableSwagger(c =>
          {
            c.RootUrl(req => new Uri(req.RequestUri, HttpContext.Current.Request.ApplicationPath ?? string.Empty).ToString());
            c.SingleApiVersion("v1", "ActiveDirManager.Api");
            c.PrettyPrint();
            c.IncludeXmlComments(() => new XPathDocument(GetXmlDocumentationPath()));
          })
          .EnableSwaggerUi(c => { });
    }

    private static string GetXmlDocumentationPath()
    {
      var path =
          string.Format("{0}bin\\{1}.xml", AppDomain.CurrentDomain.BaseDirectory, Assembly.GetExecutingAssembly().GetName().Name);

      return path;
    }
  }
}