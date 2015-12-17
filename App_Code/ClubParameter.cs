using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClubParameter
/// </summary>
public class ClubParameter
{
    public string cClubID { get; set; }
    public string cActive { get; set; }
    public string cOrgName { get; set; }
    public string cOrgURL { get; set; }
    public string cWebSiteName { get; set; }
    public string cWebsite { get; set; }
    public string cWebMaster { get; set; }
    public string cWebMasterEmail { get; set; }
    public string cSignups { get; set; }
    public string cAccessControl { get; set; }
    public string cControlCode { get; set; }
    public int cDeadlinSpan { get; set; }
    public int cPostSpan { get; set; }
    public string cMSGAClubID { get; set; }

	public ClubParameter()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}