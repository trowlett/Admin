using System;
using System.Collections.Generic;
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