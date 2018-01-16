using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.DirectoryServices;
using System.Data;
using System.Configuration;
using System.Data.OleDb;
using ActiveDirManager.Repository;
using ActiveDirManager.Core.Mapping;
using ActiveDirManager.Entities;

namespace ActiveDirManager.Business
{
  public interface IImportManager { 
    DataTable ImportToTable(string FilePath, bool isHDR = true);
    //void CreateBulkADUsersFromCSVFile();
  }

  public class ImportManager : IImportManager
  {
    private IAssociateRepository _associateRepository;

    public ImportManager()
    {
      _associateRepository = new AssociateRepository();
    }

    public DataTable ImportToTable(string FilePath, bool isHDR = true)
    {

      string conStr = "";
      string Extension = Path.GetExtension(FilePath);

      switch (Extension)
      {

        case ".xls": //Excel 97-03  
          conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
          break;

        case ".xlsx": //Excel 07  
          conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
          break;
      }

      conStr = String.Format(conStr, FilePath, isHDR);
      OleDbConnection connExcel = new OleDbConnection(conStr);
      OleDbCommand cmdExcel = new OleDbCommand();
      OleDbDataAdapter oda = new OleDbDataAdapter();
      DataTable dt = new DataTable();
      cmdExcel.Connection = connExcel;

      //Get the name of First Sheet  
      connExcel.Open();
      DataTable dtExcelSchema;
      dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
      string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
      connExcel.Close();

      //Read Data from First Sheet  
      connExcel.Open();
      cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
      oda.SelectCommand = cmdExcel;
      oda.Fill(dt);
      connExcel.Close();

      SaveImportData(dt);
      return dt;

    }

    public bool SaveImportData(DataTable importTable)
    {

      DataNamesMapper<Associate> mapper = new DataNamesMapper<Associate>();
      List<Associate> persons = mapper.Map(importTable).ToList();

      //TODO: Save list to database
      _associateRepository.Save(persons);
      return true;
    }
    
    public bool ImportToActveDirectory(DataTable importTable)
    {


      return true;
    }



    private void CreatBulkADUsersFromCSVFile(DataTable importTable)
    {
      // reading column fields 
      //string[] colFields = csvReader.ReadFields();
      //int index_Name = colFields.ToList().IndexOf("Name");
      //int index_samaccountName = colFields.ToList().IndexOf("samAccountName");
      //int index_ParentOU = colFields.ToList().IndexOf("ParentOU");

      //foreach(DataRow row in importTable.Rows)
      //{
      //  DirectoryEntry ouEntry = new DirectoryEntry("LDAP://" + csvData[index_ParentOU]);
      //  try
      //  {
      //    DirectoryEntry user = ouEntry.Children.Add("CN=" + csvData[index_Name], "user");

      //    user.Properties["samAccountName"].Value = csvData[index_samaccountName];
      //    user.CommitChanges();
      //    ouEntry.CommitChanges();

      //    int old_UAC = (int)user.Properties["userAccountControl"][0];
      //    // AD user account disable flag
      //    int Flag_Acc_Disable = 2;
      //    // To enable an ad user account, we need to clear the disable bit/flag:
      //    user.Properties["userAccountControl"][0] = (old_UAC & ~Flag_Acc_Disable);
      //    user.CommitChanges();

      //    user.Invoke("SetPassword", new object[] { "MyP@$$w0rd" });
      //    user.CommitChanges();
      //  }
      //  catch (Exception ex)
      //  {
      //    Console.WriteLine(ex.Message);
      //  }
      //}
    }

  }
}
