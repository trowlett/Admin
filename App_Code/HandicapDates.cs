using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HandicapDates
/// </summary>
public class HandicapDates
{
    public DateTime BeginDate { get; set; }
    public DateTime EndDate {  get; set; } 

    public DateTime CalcBeginPeriodDate()
    {
        MrTimeZone etz = new MrTimeZone();
        DateTime now = etz.eastTimeNow();
        int day = now.Day < 15 ? 1 : 15;
        DateTime beginPeriod = new DateTime(now.Year, now.Month, day, 0, 0, 0);
        DateTime tempNextMonth = new DateTime(now.Year, now.Month, 1, 23, 59, 59);
        DateTime nextMonth = tempNextMonth.AddMonths(1);
        DateTime lastDayThisMonth = nextMonth.AddDays(-1);
        int ldm = lastDayThisMonth.Day;  // last day of month
        int lastDay = (day == 1) ? 14 : ldm;
        DateTime endPeriod = new DateTime(now.Year, now.Month, lastDay, 23, 59, 59);
        BeginDate = beginPeriod;
        EndDate = endPeriod;

        return beginPeriod; ;

    }

    public DateTime CalcEndPeriodDate()
    {
        MrTimeZone etz = new MrTimeZone();
        DateTime now = etz.eastTimeNow();
        int day = now.Day < 15 ? 1 : 15;
        DateTime beginPeriod = new DateTime(now.Year, now.Month, day, 0, 0, 0);
        DateTime tempNextMonth = new DateTime(now.Year, now.Month, 1, 23, 59, 59);
        DateTime nextMonth = tempNextMonth.AddMonths(1);
        DateTime lastDayThisMonth = nextMonth.AddDays(-1);
        int ldm = lastDayThisMonth.Day;  // last day of month
        int lastDay = (day == 1) ? 14 : ldm;
        DateTime endPeriod = new DateTime(now.Year, now.Month, lastDay, 23, 59, 59);

        return endPeriod;
    }

    public HandicapDates()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}