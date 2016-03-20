using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SignupEntry
/// </summary>
public class SignupEntry
{
	public int SeqNo { get; set; }
    public string SClubID { get; set; }
	public DateTime STDate { get; set; }
	public string SeventId { get; set; }
    public int SPlayerID { get; set; }
	public string Splayer { get; set; }
	public string Ssex { get; set; }
	public string Shcp { get; set; }
    public DateTime SHDate { get; set; }
	public string Saction { get; set; }
	public string Scarpool { get; set; }
	public int Smarked { get; set; }
	public string SspecialRule { get; set; }
	public int SGuest { get; set; }
	public string SGuestName { get; set; }
	public string SgHcp { get; set; }
	public string SgSex { get; set; }
    public bool SSelected { get; set; }
    public bool SDelete { get; set; }

    public string IsHandicapCurrent()
    {
        MrTimeZone etz = new MrTimeZone();
        DateTime now = etz.eastTimeNow();
        int day = now.Day < 15 ? 1 : 15;
        DateTime begin = new DateTime(now.Year, now.Month, day, 0, 0, 0);
        int lastDay = (day == 1) ? 14 : now.AddMonths(1).AddDays(-1).Day;
        DateTime end = new DateTime(now.Year, now.Month, lastDay, 23, 59, 59);
        if ((this.SHDate >= begin) && (this.SHDate <= end))
        {
            return "current";
        }
        else {
            return "update";
        }
    }

    public SignupEntry()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}