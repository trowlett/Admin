using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

/// <summary>
/// Summary description for MrSchedule
/// </summary>
public class MrSchedule
{
    private static readonly char[] delimiterChars = { ';' };

    private static DateTime MinDate;
    private const string keyMinDate = "MinDate";

    private Collection<SysEvent> events = new Collection<SysEvent>();

    public Collection<SysEvent> Events
    {
        get
        {
            return this.events;
        }
    }

    public string FileName { get; private set; }

    public DateTime CreateTime { get; private set; }
    public string ErrorMsg { get; set; }

    public static MrSchedule LoadFromCsv(string clubID, string fileName)
    {
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);
        MRParams item = db.MRParams.FirstOrDefault(p => p.ClubID == clubID && p.Key == keyMinDate);
        if ((item == null))
        {
            MinDate = new DateTime(2000, 1, 1, 1, 0, 0);
        }
        else
        {
            MinDate = Convert.ToDateTime(item.Value);
        }
        
        MrSchedule target = new MrSchedule();
        target.ErrorMsg = "";
        target.FileName = fileName;
        target.CreateTime = System.IO.File.GetLastWriteTime(fileName);
        string[] lines = System.IO.File.ReadAllLines(fileName);
        foreach (String line in lines)
        {
            if (line.Trim() == "")
            {
                // Ignore this line
            }
            else if (line.Substring(0, 1) == "/")
            {
                // Ignore comment line
            }
            else
            {
                string[] fields = line.Split(delimiterChars);
                if (fields.Length != 12)
//                if (fields.Length != 11)
                {
                    throw new InvalidOperationException("DERP: Incorrect number of fields in "+fileName);
                }

                SysEvent e = new SysEvent()
                {
                    EClubID = fields[0].Substring(0,3),
                    Id = fields[0],
                    EDate = Convert.ToDateTime(fields[1]),
                    EType = fields[2],
                    ETitle = fields[3],
                    ECost = fields[4],
//                    ETime = Convert.ToDateTime(fields[1]).ToString("h:mm t"),
                    EDeadline = Convert.ToDateTime(fields[6]),
                    EHostPhone = fields[7],
                    ESpecialRule = fields[9],
                    EGuest = fields[10],
                    EHostID = fields[0].Substring(11, 3),
                    ECreationDate = target.CreateTime
                };
                if (string.IsNullOrEmpty(fields[11]))
                {
                    e.EPostDate = MinDate;
                }
                else
                {
                    e.EPostDate = Convert.ToDateTime(fields[11]);
                }

                if (fields[8] == "")
                {
                    e.EPlayerLimit = 6;
                }
                else
                {
                    e.EPlayerLimit = Convert.ToInt32(fields[8]);
                }

                target.Events.Add(e);
                Events ev = db.Events.FirstOrDefault(p =>  ((p.ClubID == e.EClubID) && (p.EventID == e.Id)));
                if (ev == null)
                {
                    Events newEvent = new Events()
                    {
                        ClubID = e.EClubID,
                        EventID = e.Id,
                        Date = e.EDate,
                        Type = e.EType,
                        Title = e.ETitle,
                        Cost = e.ECost,
//                        Time = e.ETime,
                        Deadline = e.EDeadline,
                        HostID = e.EHostID,
                        SpecialRule = e.ESpecialRule,
                        PlayerLimit = e.EPlayerLimit,
                        Guest = e.EGuest,
                        HostPhone = e.EHostPhone,
                        PostDate = e.EPostDate,
                        CreationDate = e.ECreationDate
                    };
                    db.Events.InsertOnSubmit(newEvent);
                }
                else
                {
                    ev.ClubID = e.EClubID;
                    ev.Date = e.EDate;
                    ev.Type = e.EType;
                    ev.Title = e.ETitle;
                    ev.Cost = e.ECost;
//                    ev.Time = e.ETime;
                    ev.Deadline = e.EDeadline;
                    ev.HostID = e.EHostID;
                    ev.Guest = e.EGuest;
                    ev.PlayerLimit = e.EPlayerLimit;
                    ev.SpecialRule = e.ESpecialRule;
                    ev.HostPhone = e.EHostPhone;
                    ev.PostDate = e.EPostDate;
                    ev.CreationDate = e.ECreationDate;
                }
                db.SubmitChanges();
            }
        }

        return target;
    }

    public static MrSchedule LoadFromDB(string clubID)
    {
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);

        MrSchedule DBEntries = new MrSchedule();
        var query = from q in db.Events
                    where q.ClubID == clubID
                    orderby q.Date
                    select q;

        foreach (var item in query)
        {
            SysEvent dataEvent = new SysEvent()
            {
                EClubID = item.ClubID,
                Id = item.EventID,
                EDate = item.Date,
                EType = item.Type,
                EHostID = item.HostID,
                ETitle = item.Title,
                ECost = item.Cost,
//                ETime = item.Time,
                EDeadline = item.Deadline,
                ESpecialRule = item.SpecialRule,
                EGuest = item.Guest,
                EHostPhone = item.HostPhone,
                EPlayerLimit = item.PlayerLimit,
                EPostDate = item.PostDate,
                ECreationDate = item.CreationDate
            };


            DBEntries.Events.Add(dataEvent);
        }
        int entryCount = DBEntries.Events.Count;

        return DBEntries;
    }

    public static bool IsClosed(DateTime deadline)
    {
        MrTimeZone etz = new MrTimeZone();
        return (etz.eastTimeNow() >= deadline);
    }
    

}