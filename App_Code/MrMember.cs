using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MrMember
/// </summary>
public class MrMember
{
    public string clubID { get; set; }
	public int pID { get; set; }
	public string name { get; set; }
	public string lname { get; set; }
	public string fname { get; set; }
	public string hcp { get; set; }
	public string memberNumber { get; set; }
	public int gender { get; set; }
	public string title { get; set; }
	public int active { get; set; }
	public DateTime hdate { get; set; }
    public int del { get; set; }

	public bool IsFemale()
	{
		return (this.gender == 2);
	}
    public bool IsDeleted()
    {
        return (this.del == 1);
    }
    public bool IsUpdated(DateTime dt)
    {
        DateTime xdt = new DateTime(dt.Year, dt.Month, dt.Day);
        DateTime xhdate = new DateTime(hdate.Year, hdate.Month, hdate.Day);
        return (xhdate == xdt);
    }

    public string IsHandicapCurrent(DateTime beginPeriod, DateTime endPeriod)
    {
/*        MrTimeZone etz = new MrTimeZone();
        DateTime now = etz.eastTimeNow();
        int day = now.Day < 15 ? 1 : 15;
        DateTime beginPeriod = new DateTime(now.Year,now.Month,day,0,0,0);
        DateTime tempNextMonth = new DateTime(now.Year, now.Month, 1, 23,59,59);
        DateTime nextMonth = tempNextMonth.AddMonths(1);
        DateTime lastDayThisMonth = nextMonth.AddDays(-1);
        int ldm = lastDayThisMonth.Day;  // last day of month
        int lastDay = (day == 1) ? 14 : ldm;
//        DateTime endPeriod = new DateTime(now.Year,now.Month,lastDay,23,59,59);
*/
        if ((hdate >= beginPeriod) && (hdate <= endPeriod))
        {
            return "current";
        }
        else {
            return "update";
        }
    }


/*        if (this.hdate == dt)
        {
            return "Green";
        }
        else
        {
            return "Red";
        } */
    }

