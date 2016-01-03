using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Members_input : System.Web.UI.Page
{
    public string club;
    Settings clubSettings;
    MembersList inputMembersList { get; set; }
    MembersList currentMembersList { get; set; }
    PlayersCollection newMembersRoster { get; set; }
    protected string _clubID;
    Settings _clubSettings;
    public const string keyPlayers = "Players";
    public int pID;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        clubSettings = new Settings();
        clubSettings = (Settings)Session["Settings"];
        club = clubSettings.ClubInfo.ClubName;
        if (!IsPostBack)
        {
            if (clubSettings.ClubID == "229") tbInputFileName.Text = "229-rwmembers.txt";
        }
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
//        pnlFileName.Visible = false;
        pnlResetID.Visible = true;
        string filename = tbInputFileName.Text;
        string fileClubID = filename.Substring(0, 3);
        _clubID = fileClubID;
        _clubSettings = new Settings();
        _clubSettings.ClubID = _clubID;
        _clubSettings.ClubInfo = ClubManager.GetSetting(_clubID);
        Session["AltClub"] = _clubSettings;
        club = _clubSettings.ClubInfo.ClubName;

        filename = Server.MapPath("~\\App_Data\\") + filename;
        inputMembersList = MembersList.LoadMembersFromTextFile(fileClubID, filename);
        Session["inputMembers"] = inputMembersList;
        int c = inputMembersList.Members.Count;
        currentMembersList = MembersList.LoadMembers(fileClubID);
        Session["CurrentMembers"] = currentMembersList;
        int n = currentMembersList.Members.Count;
        this.InputMemberRepeater.DataSource = new MembersList[] { this.inputMembersList };
        this.InputMemberRepeater.DataBind();
        this.OldMemberRepeater.DataSource = new MembersList[] { this.currentMembersList };
        this.OldMemberRepeater.DataBind();
        string x = (Param.GetParameter(_clubID,keyPlayers));
        if (x == "") x="0";
        pID = Convert.ToInt32(x);
//        pID = Convert.ToInt32(Param.GetParameter(_clubID, keyPlayers));
        if (pID > 0)
        {
            lblPlayerID.Text = string.Format("Last Player ID is = {0} &nbsp;&nbsp;&nbsp;&nbsp;", pID);
        }
        else
        {
            btnYES.Enabled = true;
            btnResetPID.Enabled = false;
        }
    }

    protected void Member_ItemCommand(Object Sender, RepeaterCommandEventArgs e)
    {
    }
    protected void btnResetPID_Click(object sender, EventArgs e)
    {
        pID = 0;
        _clubSettings = (Settings)Session["AltClub"];
        _clubID = _clubSettings.ClubID;
        if (Param.UpdateParameter(_clubID, keyPlayers, pID.ToString()))
        {
            lblResetResult.Text = "Player ID Reset Successfully.";
            pID = Convert.ToInt32(Param.GetParameter(_clubID, keyPlayers));
            lblPlayerID.Text = string.Format("Last Player ID is = {0} &nbsp;&nbsp;&nbsp;&nbsp;", pID);
            btnResetPID.Enabled = false;
            btnYES.Enabled = true;
        }
        else
        {
            lblResetResult.Text = "FATAL Error:  Could not Reset Player ID.";
        }
    }
    protected void btnYES_Click(object sender, EventArgs e)
    {
        pnlOldMembers.Visible = false;
        pnlNewRoster.Visible = true;
        _clubSettings = (Settings)Session["AltClub"];
        _clubID = _clubSettings.ClubID;
        inputMembersList = (MembersList)Session["inputMembers"];
        // 1.  Delete all members in the Players database

        int countOfPlayersDeleted = PurgePlayers(_clubID);     // purge players from database
        lblStatus.Text = string.Format("{0} Members purged from Players Database", countOfPlayersDeleted);
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);

        newMembersRoster = new PlayersCollection();
        for (int i = 0; i < inputMembersList.Members.Count; i++)
        {
            int pid = i+1;
            Players pl = new Players()
            {
                PlayerID = pid,
                ClubID = inputMembersList.Members[i].clubID,
                Name = inputMembersList.Members[i].name,
                FName = inputMembersList.Members[i].fname,
                LName = inputMembersList.Members[i].lname,
                Hcp = inputMembersList.Members[i].hcp,
                MemberID = inputMembersList.Members[i].memberNumber,
                Sex = inputMembersList.Members[i].gender,
                Title = inputMembersList.Members[i].title,
                HDate = inputMembersList.Members[i].hdate,
                Delete = inputMembersList.Members[i].del               
            };
            newMembersRoster.Roster.Add(pl);
            db.Players.InsertOnSubmit(pl);
        }
        db.SubmitChanges();
        this.NewRosterRepeater.DataSource = new PlayersCollection[] { this.newMembersRoster };
        this.NewRosterRepeater.DataBind();
        lblReloadStatus.Text = string.Format("{0} Members Reloaded to Database", newMembersRoster.Roster.Count);

        bool ok = Param.UpdateParameter(_clubID, keyPlayers, newMembersRoster.Roster.Count.ToString());

        // 2.  Take each member in the inputMembersList, change the Player ID to a new sequential number
        // 3.  Insert the member on the Players database
        // 4.  List the members with new IDs on the web page

    }
    protected int PurgePlayers(string clubID)
    {
        int purgedCount = 0;
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);
        var query = from q in db.Players
                    where (q.ClubID == clubID)
                    select q;
        if (query != null)
        {
            foreach (var item in query)
            {
                db.Players.DeleteOnSubmit(item);
                purgedCount++;
            }
            db.SubmitChanges();
        }
        return purgedCount;
    }
}