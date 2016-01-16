using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

/// <summary>
/// Summary description for MemberList
/// </summary>
public class MembersList
{
    private static readonly char[] delimiterChars = { '\t', ';' };      // tab and semicolon

	private Collection<MrMember> members = new Collection<MrMember>();

	public Collection<MrMember> Members
	{
		get
		{
			return this.members;
		}
	}

	public string FileName { get; private set; }

	public DateTime CreateTime { get; private set; }

    public static int Count {get; set;}
    private static int _count {get; set;}

	private static string _MembersName = "";
	public static string MembersName = "";
	public static MembersList LoadMembers(string clubID)
	{
		string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
		MRMISGADB db = new MRMISGADB(MRMISGADBConn);

		MembersList target = new MembersList();
		var memb =
			from p in db.Players
            where p.ClubID == clubID
			orderby p.Name
			select p;
        _count = 0;
		foreach (var item in memb)
		{
			MrMember newMember = new MrMember()
			{
                clubID = item.ClubID,
				pID = item.PlayerID,
				name = item.Name,
				fname = item.FName,
				lname = item.LName,
				gender = item.Sex,
				hcp = item.Hcp,
				memberNumber = item.MemberID,
				title = item.Title,
				hdate = item.HDate,
                del = item.Delete
			};
			newMember.active = SignupList.CountPlayersActiveSignupEntries(item.ClubID, item.PlayerID);
			target.Members.Add(newMember);
            _count++;
		}
        Count = _count;
		return target;
	}

    public static MembersList SortByID(string clubID)
    {
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);

        MembersList target = new MembersList();
        var memb =
            from p in db.Players
            where p.ClubID == clubID
            orderby p.MemberID, p.PlayerID
            select p;
        _count = 0;
        foreach (var item in memb)
        {
            MrMember newMember = new MrMember()
            {
                clubID = item.ClubID,
                pID = item.PlayerID,
                name = item.Name,
                fname = item.FName,
                lname = item.LName,
                gender = item.Sex,
                hcp = item.Hcp,
                memberNumber = item.MemberID,
                title = item.Title,
                hdate = item.HDate,
                del = item.Delete
            };
            newMember.active = SignupList.CountPlayersActiveSignupEntries(item.ClubID, item.PlayerID);
            target.Members.Add(newMember);
            _count++;
        }
        Count = _count;
        return target;
    }

	public static int DeleteMember(string clubID, int playerID)
	{
		int err = 1;                                                // set result to not deleted
		string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
		MRMISGADB db = new MRMISGADB(MRMISGADBConn);
		var pl = db.Players.Single(p => p.ClubID == clubID && p.PlayerID == playerID);
		_MembersName = pl.Name;
		if (SignupList.CountPlayersActiveSignupEntries(clubID, playerID) == 0)    // delete is active records is zero
		{
			db.Players.DeleteOnSubmit(pl);
			var deleteSignupEntries = 
				from SignupEntries in db.PlayersList
				where SignupEntries.ClubID == clubID && SignupEntries.PlayerID == playerID
				select SignupEntries;
			if (deleteSignupEntries != null)
			{
				foreach (var SignupEntry in deleteSignupEntries)
				{
					// delete all signup records for this member
					db.PlayersList.DeleteOnSubmit(SignupEntry);                  
				}
			db.SubmitChanges();
			err = 0;                                                // set result to deleted
			}

		}
		return err;
	}

    public static MembersList LoadMembersFromTextFile(string clubID, string fileName)
    {
        MembersList target = new MembersList();
        int readCount = 0;

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
                if (readCount == 0)
                {
                    readCount = 1;    // Ignore header record
                }
                else
                {
                    string[] fields = line.Split(delimiterChars);
                    if (fields.Length != 11)
                    {
                        throw new InvalidOperationException("DERP: Incorrect number of fields in " + fileName);
                    }
                    readCount++;
                    MrMember e = new MrMember()
                    {
                        clubID = clubID,
                        pID = Convert.ToInt32(fields[0]),
                        name = StripQuotes(fields[1].ToUpper()),
                        fname = fields[2],
                        lname = fields[3],
                        hcp = fields[4],
                        memberNumber = fields[5],
                        gender = Convert.ToInt32(fields[6]),
                        title = fields[7],
                        active = Convert.ToInt32(fields[8]),
                        hdate = Convert.ToDateTime(fields[9]),
                        del = Convert.ToInt32(fields[10])
                    };

                    target.Members.Add(e);
                }
            }
        }

        return target;

    }
    private static string StripQuotes(string item)
    {
        char quote = '\"';
        // Determine whether a tag begins the string.
        item = item.TrimEnd(quote);
        item = item.TrimStart(quote);

        return item;
    }

	public MembersList()
	{
        _count = 0;
		//
		// TODO: Add constructor logic here
		//
	}
}