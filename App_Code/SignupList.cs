using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

/// <summary>
/// Summary description for SignupList
/// </summary>
public class SignupList
{

	private Collection<SignupEntry> entries = new Collection<SignupEntry>();

	public Collection<SignupEntry> Entries
	{
		get
		{
			return this.entries;
		}
	}

	public static int EntriesPurged { get { return _entriesPurged; } set { _entriesPurged = value; } }
	private static int _entriesPurged = 0;

    public int Females { get; private set; }

	public static SignupList LoadFromPlayersListDB(string clubID, string EventID)
	{
		string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
		MRMISGADB db = new MRMISGADB(MRMISGADBConn);

		SignupList target = new SignupList();
        target.Females = 0;
        var plist =
            from px in db.Players
            where px.ClubID == clubID
            select new { px.ClubID, px.PlayerID, px.Name, px.LName, px.FName, px.Sex, px.Hcp, px.Title };
		var slist =
			from pl in db.PlayersList
			join pn in plist on pl.PlayerID equals pn.PlayerID
			where pl.EventID == EventID && pl.Marked == 0
			orderby pl.TransDate
			select new { pl.TransDate, pl.EventID, pl.PlayerID, pn.Name, pn.Sex, pn.Hcp, pl.Action, pl.Carpool, pl.Marked, pl.SpecialRule, pl.GuestID };
		int seqNo = 0;

		foreach (var item in slist)
		{
			seqNo++;
			SignupEntry entry = new SignupEntry(){
				SeqNo = seqNo,
				STDate = item.TransDate,
				SeventId = item.EventID,
                SPlayerID = item.PlayerID,
				Splayer = item.Name,
				Shcp = item.Hcp,
				Saction= item.Action,
				Scarpool = item.Carpool,
				Smarked= item.Marked,
				SspecialRule = item.SpecialRule,
				SGuest = item.GuestID,
                SSelected = false,
                SDelete = false
			};
			entry.Ssex = "";
            if ((int)item.Sex == 2)
            {
                entry.Ssex = "[F]";
                target.Females++;
            }
			if (item.GuestID == 0)
			{
				entry.SGuest = 0;
			}
			else
			{
//				MRParams param = db.MRParams.FirstOrDefault(p => p.Key == keyPlayers);
				Guest guest = db.Guest.FirstOrDefault(g => g.ClubID == clubID && g.GuestID == item.GuestID);
				entry.SGuestName = guest.GuestName;
				entry.SgHcp = guest.gHcp;
				entry.SgSex = "";
				if (guest.gSex == 2) entry.SgSex = "[F]";
			}
			target.entries.Add(entry);
		}
		return target;
	}

    public static SignupList LoadPlayersListDB(string clubID)
    {
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);

        SignupList target = new SignupList();
        var slist =
            from pl in db.PlayersList
            where pl.ClubID == clubID
            orderby pl.TransDate
            select pl;
        int seqNo = 0;
        foreach (var item in slist)
        {
            seqNo++;
            SignupEntry entry = new SignupEntry()
            {
                SeqNo = seqNo,
                SClubID = item.ClubID,
                STDate = item.TransDate,
                SeventId = item.EventID,
                SPlayerID = item.PlayerID,
                Saction = item.Action,
                Scarpool = item.Carpool,
                Smarked = item.Marked,
                SspecialRule = item.SpecialRule,
                SGuest = item.GuestID,
                SSelected = false,
                SDelete = false
            };
            entry.Splayer = GetPlayerName(clubID, item.PlayerID);
            target.entries.Add(entry);
        }
        return target;
    }

    protected static string GetPlayerName(string clubID, int id)
    {
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);

        var prm = db.Players.FirstOrDefault(p => p.ClubID == clubID && p.PlayerID == id);
        if (prm == null) return "UNKNOWN";
        return prm.Name;
    }


	public static int CountPlayersActiveSignupEntries(string clubID, int PlayerID)
	{
		int entryCount = 0;
		string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
		MRMISGADB db = new MRMISGADB(MRMISGADBConn);

		var plist = 
			from p in db.PlayersList
			where (p.ClubID == clubID && p.PlayerID == PlayerID)
			select new {p.PlayerID, p.EventID, p.Marked, p.TransDate};
		if (plist != null)
		{
			foreach (var entry in plist)
			{
				if (entry.Marked == 0) entryCount++;
			}
		}

		return entryCount;
	}

    public static int CountActiveSignupsInEvent(string clubID, string EventID)
    {
        int entryCount = 0;
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);

        entryCount = db.PlayersList.Count(p => p.ClubID == clubID && p.EventID == EventID && p.Marked == 0);
/*        var plist =
            from p in db.PlayersList
            where (p.ClubID == clubID && p.EventID == EventID)
            select new { p.PlayerID, p.EventID, p.Marked, p.TransDate };
        if (plist != null)
        {
            foreach (var entry in plist)
            {
                if (entry.Marked == 0) entryCount++;
            }
        }
    */
        return entryCount;
    }

	public static void PurgeMarkedEntries(string clubID)
	{
		string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
		MRMISGADB db = new MRMISGADB(MRMISGADBConn);
		var markedEntries =
			from SignupEntries in db.PlayersList
			where SignupEntries.ClubID == clubID && SignupEntries.Marked > 0
			select SignupEntries;
		_entriesPurged = 0;
		if (markedEntries != null)
		{

			foreach (var entry in markedEntries)
			{
				db.PlayersList.DeleteOnSubmit(entry);
				_entriesPurged++;
			}
			db.SubmitChanges();
		}

	}

	public static int MarkedEntryCount(string clubID)
	{
		int mc = 0;
		string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
		MRMISGADB db = new MRMISGADB(MRMISGADBConn);
		var plist =
			from p in db.PlayersList
			where (p.ClubID == clubID && p.Marked > 0)
			select p;
		if (plist != null)
		{
			foreach (var entry in plist)
			{
				if (entry.Marked > 0) mc++;
			}
		}
		return mc;
	}

	public SignupList()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}