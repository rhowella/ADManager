using ActiveDirManager.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ActiveDirManager.Web.Controllers
{
  public class HomeController : Controller
  {
    private string _baseApi;

    public HomeController()
    {
      _baseApi = ConfigurationManager.AppSettings.Get("ActiveDirManager.API_BaseUrl");
    }

    public ActionResult Index()
    { 
      return View(new DataTable());
    } 

    [HttpPost]
    public async Task<ActionResult> Index(HttpPostedFileBase file)
    {
      if (file != null && file.ContentLength > 0)
      {
        var fileName =$"{DateTime.UtcNow.ToString("yyyyMMddHHmmss")}_{Path.GetFileName(file.FileName)}";
        var path = ConfigurationManager.AppSettings.Get("ActiveDirManager_Temp_Path");
        file.SaveAs(Path.Combine(path,fileName));

        var importFile = new ImportFile() { FilePath = path, FileName = fileName };
        HttpClient client = new HttpClient();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage response = client.PostAsJsonAsync($"{_baseApi}api/import/excel", importFile).Result;
        string dataResult = "";
        using (HttpContent content = response.Content)
        {
          // ... Read the string.
          Task<string> result = content.ReadAsStringAsync();
          dataResult = result.Result;
        }
        var table = JsonConvert.DeserializeObject<DataTable>(dataResult);

        return View(table);
      }
      return View(new DataTable());
    }

    [HttpPost]
    public async Task<ActionResult> Sync(int Id)
    {
      HttpClient client = new HttpClient();

      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      HttpResponseMessage response = client.PostAsJsonAsync($"{_baseApi}api/import/excel", Id).Result;
      string dataResult = "";
      using (HttpContent content = response.Content)
      {
        // ... Read the string.
        Task<string> result = content.ReadAsStringAsync();
        dataResult = result.Result;
      }
      var table = JsonConvert.DeserializeObject<DataTable>(dataResult);


      return View("Index", table);
    }

    }
}