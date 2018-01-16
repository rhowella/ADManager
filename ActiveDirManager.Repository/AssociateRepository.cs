using ActiveDirManager.Entities;
using Dapper;
using System;
using System.Collections.Generic;

namespace ActiveDirManager.Repository
{
  public interface IAssociateRepository
  {
    Associate GetAssociate(int Id);
    List<Associate> GetAssociateList();
    bool Insert(Associate associate);
    bool Update(Associate associate);
    bool Save(List<Associate> associate);
  }

  public class AssociateRepository : BaseRepository, IAssociateRepository
  {
    public Associate GetAssociate(int Id)
    {
      throw new NotImplementedException();
    }

    public List<Associate> GetAssociateList()
    {
      throw new NotImplementedException();
    }

    public bool Insert(Associate associate)
    {
      int rowsAffected = _db.Execute(String.Format(@"INSERT INTO[dbo].[Associate]
    ([AssociateId]
          ,[FirstName]
          ,[LastName]
          ,[PreferredName]
          ,[ReportsToId]
          ,[BusinessUnitId]
          ,[JobTitleId]
          ,[PersonalMobile]
          ,[Email]
          ,[HireDate]
          ,[RehireDate]
          ,[TerminationDate]
          ,[CreatedDate]
          ,[CreatedBy]
          ,[UpdatedDate]
          ,[UpdatedBy]
          ,[DepartmentId])
    VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16})
      ",associate.AssociateId
      ,associate.FirstName
      ,associate.LastName
      ,associate.PreferredName
      ,associate.ReportsToId
      , associate.BusinessUnitId
      , associate.JobtitleId
      , associate.PersonalMobile
      , associate.Email
      , associate.HireDate
      , associate.RehireDate
      , associate.TerminationDate
      , DateTime.Now
      , "system"
      , DateTime.Now
      , "system"
      , associate.DepartmentId));

      if (rowsAffected > 0)
      {
        return true;
      }
      return false;
    }
    public bool Save(List<Associate> associateList)
    { 
      if (_db.State == System.Data.ConnectionState.Closed)
        _db.Open();

      var trans = _db.BeginTransaction();
      try
      {
        foreach (var associate in associateList)
        {
          DynamicParameters parameter = new DynamicParameters();
          parameter.Add("@AssociateId", associate.AssociateId);
          parameter.Add("@FirstName", associate.FirstName);
          parameter.Add("@LastName", associate.LastName);
          parameter.Add("@PreferredName", associate.PreferredName);
          parameter.Add("@ReportsToId", associate.ReportsToId);
          parameter.Add("@BusinessUnitId", associate.BusinessUnitId);
          parameter.Add("@JobtitleId", associate.JobtitleId);
          parameter.Add("@PersonalMobile", associate.PersonalMobile);
          parameter.Add("@Email", associate.Email);
          parameter.Add("@HireDate", associate.HireDate);
          parameter.Add("@RehireDate", associate.RehireDate);
          parameter.Add("@TerminationDate", associate.TerminationDate);
          parameter.Add("@CreatedDate", associate.CreatedDate);
          parameter.Add("@UpdatedDate", associate.UpdatedDate);
          parameter.Add("@DepartmentId", associate.DepartmentId);
          parameter.Add("@CreatedBy", "system");
          parameter.Add("@UpdatedBy", "system");
          _db.Execute("UPSERT_ASSOCIATE",parameter, transaction: trans, commandType: System.Data.CommandType.StoredProcedure);
        }
      trans.Commit();
        _db.Close();
      }
      catch (Exception ex)
      {        
        trans.Rollback();
        return false;
      }
      return true;
    }

      public bool Update(Associate associate)
    {      
      int rowsAffected = _db.Execute(String.Format(@"UPDATE [dbo].[Associate]
      (
          SET [FirstName] = {1}
          ,[LastName] = {2}
          ,[PreferredName] = {3}
          ,[ReportsToId] = {4}
          ,[BusinessUnitId] = {5}
          ,[JobTitleId] = {6}
          ,[PersonalMobile] = {7}
          ,[Email] = {8}
          ,[HireDate] = {9}
          ,[RehireDate] = {10} 
          ,[TerminationDate] = {11}
          ,[UpdatedDate] = {12}
          ,[UpdatedBy] = {13}
          ,[DepartmentId]  = {14}
    WHERE [AssociateId] = {0} )
      ", associate.AssociateId
      , associate.FirstName
      , associate.LastName
      , associate.PreferredName
      , associate.ReportsToId
      , associate.BusinessUnitId
      , associate.JobtitleId
      , associate.PersonalMobile
      , associate.Email
      , associate.HireDate
      , associate.RehireDate
      , associate.TerminationDate
      , DateTime.Now
      , "system"
      , associate.DepartmentId));

      if (rowsAffected > 0)
      {
        return true;
      }
      return false;
    }
  }
}
  