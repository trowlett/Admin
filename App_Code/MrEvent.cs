using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SysEvent
/// </summary>
public class SysEvent
{
    public string EClubID { get; set; }
    public string Id { get; set; }
    public DateTime EDate { get; set; }
    public string EType { get; set; }
    public string EHostID { get; set; }
    public string ETitle { get; set; }
    public string ECost { get; set; }
    public int EPlayerLimit { get; set; }
    public DateTime EDeadline { get; set; }
    public DateTime EPostDate { get; set; }
    public string EHostPhone { get; set; }
    public string ESpecialRule { get; set; }
    public string EGuest { get; set; }
    public DateTime ECreationDate { get; set; }

    public bool CanSignUp(DateTime lastDate)
    {
        return this.EType != "MISGA" && this.EDate <= lastDate;
    }

    public static bool EventAddToDB(SysEvent se)
    {
        bool status = false;
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);
        Events ev = db.Events.FirstOrDefault(p => ((p.ClubID == se.EClubID) && (p.EventID == se.Id)));
        if (ev == null)
        {
            Events newEvent = new Events()
            {
                ClubID = se.EClubID,
                EventID = se.Id,
                Date = se.EDate,
                Type = se.EType,
                Title = se.ETitle,
                Cost = se.ECost,
                //                        Time = e.ETime,
                Deadline = se.EDeadline,
                HostID = se.EHostID,
                SpecialRule = se.ESpecialRule,
                PlayerLimit = se.EPlayerLimit,
                Guest = se.EGuest,
                HostPhone = se.EHostPhone,
                PostDate = se.EPostDate,
                CreationDate = se.ECreationDate
            };
            db.Events.InsertOnSubmit(newEvent);
            db.SubmitChanges();
            status = true;
        }
        else
        {
            status = false;
        }

        return status;
    }
    public static bool DeleteEvent(string EvID)
    {
        bool result = false;

        return result;
    }


    public SysEvent(Events Event)
    {
        ECreationDate = new MrTimeZone().eastTimeNow();
        EClubID = Event.ClubID;
        Id = Event.EventID;
        ECost = Event.Cost;
        EDate = Event.Date;
        EDeadline = Event.Deadline;
        EGuest = Event.Guest;
        EHostID = Event.HostID;
        EHostPhone = Event.HostPhone;
        EPlayerLimit = Event.PlayerLimit;
        EPostDate = Event.PostDate;
        ESpecialRule = Event.SpecialRule;
        ETitle = Event.Title;
        EType = Event.Type;
    }
    public SysEvent()
    {
        ECreationDate = new MrTimeZone().eastTimeNow();
    }
}