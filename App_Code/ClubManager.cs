using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

/// <summary>
/// Summary description for ClubManager



public class ClubManager
{

    private Collection<ClubInfo> clubCollection = new Collection<ClubInfo>();
    public Collection<ClubInfo> ClubCollection
    {
        get { return this.clubCollection; }
    }


    public static ClubInfo ci { get; set; }

    public static ClubInfo GetSetting(string clubid)
    {
        string sdbc = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB csdb = new MRMISGADB(sdbc);

        var cs = csdb.ClubSettings.FirstOrDefault(c => c.ClubID == clubid);

        string ClubsConnect = ConfigurationManager.ConnectionStrings["ClubsConnect"].ToString();
        MISGACLUBS db = new MISGACLUBS(ClubsConnect);

        var q = db.Clubs.FirstOrDefault(p => p.ClubID == clubid);

        ci = new ClubInfo
        {
            ClubID = clubid,
            ClubName= q.ClubName,
            MISGAURL=q.MISGAURL,
            ProName = q.ProName,
            ProEmail = q.ProEmail,
            ProPhone = q.ProPhone,
            ProFax = q.ProFax,
            RepName = q.RepName,
            RepEmail =q.RepEmail,
            RepPhone = q.RepPhone,
            PayOpt= q.PayOpt,
            Refresh = q.Refresh,
            SRule= q.SRule,
            OtherRule= q.OtherRule,
            Misc = q.Misc,
            slope = q.slope,
            OrgName= cs.OrgName,
            OrgURL = cs.OrgURL,
            WebSiteName = cs.WebSiteName,
            WebSite = cs.Website,
            WebMaster = cs.WebMaster,
            WebMasterEmail = cs.WebMasterEmail,
            Signups = cs.Signups,
            AccessControl = cs.AccessControl,
            ControlCode = cs.ControlCode,
            DeadlineSpan = cs.DeadlineSpan,
            PostSpan = cs.PostSpan,
            MSGAClubID= cs.MSGAClubID
        };
        return ci;
    }
    public static ClubManager GetClubs()
    {
        ClubManager target = new ClubManager();
        string sdbc = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB csdb = new MRMISGADB(sdbc);

        var cs = 
            from c in csdb.ClubSettings
            where c.Active == "YES"
            orderby c.OrgName
            select new { c.ClubID };
        if (cs != null)
        {
            foreach (var clb in cs)
            {
                ClubInfo ci = ClubManager.GetSetting(clb.ClubID);
//                ClubParameter cd = new ClubParameter();
//                cd = LoadClubParameter(clb.ClubID);
                target.clubCollection.Add(ci);
            }
        }
        return target;
    }


	public ClubManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}