using ActiveDirManager.Business;
using ActiveDirManager.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace ActiveDirManager.API.Controllers
{
  /// <summary>
  /// CSV Import Controller
  /// </summary>
  [RoutePrefix("api/import")]
  public class ImportController : ApiController
  {

    private IImportManager _importManager;

    /// <summary>
    /// Initialize Controller
    /// </summary>
    /// <param name="importManager"></param>
    public ImportController(IImportManager importManager)
    {
      _importManager = importManager;
    }
    
    /// <summary>
    /// Import File
    /// </summary>
    /// <param name="importFile"></param>
    /// <returns></returns>
    [HttpPost, Route("excel")] 
    public async Task<IHttpActionResult> ImportExcelAsync(ImportFile importFile)
    {
      try
      {
        var returnValue =_importManager.ImportToTable(Path.Combine( $@"{importFile.FilePath}",$@"{importFile.FileName}"));
        return Ok(returnValue);
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }
    }
  }
}
