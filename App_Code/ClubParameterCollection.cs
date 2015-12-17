using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

/// <summary>
/// Summary description for ClubParameterCollection
/// </summary>
public class ClubParameterCollection
{
    private Collection<ClubParameter> clubParameters = new Collection<ClubParameter>();

    public Collection<ClubParameter> ClubParameters
    {
        get
        {
            return this.clubParameters;
        }
    }

    public static ClubParameterCollection LoadClubsParameters()
    {

        ClubParameterCollection target = new ClubParameterCollection();
        string sdbc = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB csdb = new MRMISGADB(sdbc);
        var cd =
            from c in csdb.ClubSettings
            orderby c.ClubID
            select new { c.ClubID, c.Active, c.OrgName, c.OrgURL, c.WebSiteName, c.Website, c.WebMaster, c.WebMasterEmail, c.Signups, c.AccessControl, c.ControlCode, c.DeadlineSpan, c.PostSpan, c.MSGAClubID };


        foreach (var item in cd)
        {

            ClubParameter cp = new ClubParameter();
            if (cd != null)
            {
                cp.cClubID = item.ClubID;
                cp.cActive = item.Active;
                cp.cOrgName = item.OrgName;
                cp.cOrgURL = item.OrgURL;
                cp.cWebSiteName = item.WebSiteName;
                cp.cWebsite = item.Website;
                cp.cWebMaster = item.WebMaster;
                cp.cWebMasterEmail = item.WebMasterEmail;
                cp.cSignups = item.Signups;
                cp.cAccessControl = item.AccessControl;
                cp.cControlCode = item.ControlCode;
                cp.cDeadlinSpan = item.DeadlineSpan;
                cp.cPostSpan = item.PostSpan;
                cp.cMSGAClubID = item.MSGAClubID;
            }
            else
            {
                cp.cClubID = item.ClubID;
                cp.cActive = "NO";
                cp.cOrgName = "No club on file for this Club ID";
                cp.cOrgURL = "";
                cp.cWebSiteName = "";
                cp.cWebsite = "http://demo.misga-signup.org";
                cp.cWebMaster = "";
                cp.cWebMasterEmail = "";
                cp.cSignups = "Disabled";
                cp.cAccessControl = "on";
                cp.cControlCode = "DEMO";
                cp.cDeadlinSpan = 4;
                cp.cPostSpan = 45;
                cp.cMSGAClubID = "";
            }
            target.clubParameters.Add(cp);
        }
        return target;
    }

	public ClubParameterCollection()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}