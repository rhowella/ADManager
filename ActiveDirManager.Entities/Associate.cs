using ActiveDirManager.Core.Attributes;
using System;

namespace ActiveDirManager.Entities
{
  public class Associate
  {

    [DataNames("AssociateId", "Associate Id")]
    public int AssociateId { get; set; }
    
    [DataNames("FirstName", "First Name")]
    public string FirstName { get; set; }

    [DataNames("LastName", "Last Name")]
    public string LastName { get; set; }

    [DataNames("PreferredName", "Preferred Name")]
    public string PreferredName { get; set; }

    [DataNames("PersonalMobile", "Personal Mobile")]
    public string PersonalMobile { get; set; }

    [DataNames("Email", "Email")]
    public string Email { get; set; }

    [DataNames("HireDate", "Hire Date")]
    public DateTime? HireDate { get; set; }

    [DataNames("RehireDate", "Rehire Date")]
    public DateTime? RehireDate { get; set; }

    [DataNames("TerminationDate", "Termination Date")]
    public DateTime? TerminationDate { get; set; }

    [DataNames("CreatedBy")]
    public string CreatedBy { get; set; }

    [DataNames("CreatedDate")]
    public DateTime? CreatedDate { get; set; }

    [DataNames("UpdatedBy")]
    public string UpdatedBy { get; set; }

    [DataNames("UpdatedDate")]
    public DateTime? UpdatedDate { get; set; }


    [DataNames("ReportsToId", "Reports To Associate Id")]
    public int? ReportsToId { get; set; }


    [DataNames("BusinessUnitId", "Business Unit Id")]
    public int? BusinessUnitId { get; set; }
     
   //public BusinessUnit BusinessUnit { get; set; }


    [DataNames("DepartmentId", "Department Id")]
    public int? DepartmentId { get; set; }
     
    //public Department Department { get; set; }


    [DataNames("JobtitleId", "JobtitleId")]
    public int? JobtitleId { get; set; }
     
    //public JobTitle JobTitle { get; set; }
     

  }
}
