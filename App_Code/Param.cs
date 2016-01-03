using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Param
/// </summary>
public class Param
{
    public string ClubID { get; set; }
	public string Key { get; set; }
	public string Value { get; set; }
	public DateTime ChangeDate { get; set; }

    private string _ClubID = "";
	private string _Key = "";
	private string _Value = "";
	private DateTime _ChangeDate;

    public static string GetParameter(string ClubID, string Key)
    {
        string value = "";
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);
		MRParams param = db.MRParams.FirstOrDefault(p => ((p.ClubID == ClubID) && (p.Key == Key)));
        if (param != null)
        {
            value = param.Value;
        }
        return value;
    }

    public static bool UpdateParameter(string ClubID, string Key, string Value)
    {
        bool ok = false;
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);
        MrTimeZone tz = new MrTimeZone();
		MRParams param = db.MRParams.FirstOrDefault(p => ((p.ClubID == ClubID) && (p.Key == Key)));
        if (param != null)
        {
            param.Value = Value;
            param.ChangeDate = tz.eastTimeNow();
            ok = true;
        }
        else
        {
            MRParams addParam = new MRParams()
            {
                ClubID = ClubID,
                Key = Key,
                Value = Value,
                ChangeDate = tz.eastTimeNow()
            };
            db.MRParams.InsertOnSubmit(addParam);
            ok = true;
        }
        db.SubmitChanges();
        return ok;
    }
	public Param()
	{
		//
		// TODO: Add constructor logic here
		//
        ClubID = _ClubID;
		Key = _Key;
		Value = _Value;
		_ChangeDate = new MrTimeZone().eastTimeNow();
		ChangeDate = _ChangeDate;
	}
}