using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Members_input : System.Web.UI.Page
{
    public string club;
    Settings clubSettings;
    MembersList newMembersList { get; set; }
    MembersList currentMembersList { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        clubSettings = new Settings();
        clubSettings = (Settings)Session["Settings"];
        club = clubSettings.ClubInfo.ClubName;
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string filename = tbInputFileName.Text;
        string fileClubID = filename.Substring(0, 3);
        filename = Server.MapPath("~\\App_Data\\") + filename;
        newMembersList = MembersList.LoadMembersFromTextFile(fileClubID, filename);
        int c = newMembersList.Members.Count;
        currentMembersList = MembersList.LoadMembers(fileClubID);
        int n = currentMembersList.Members.Count;
        this.NewMemberRepeater.DataSource = new MembersList[] { this.newMembersList };
        this.NewMemberRepeater.DataBind();
        this.OldMemberRepeater.DataSource = new MembersList[] { this.currentMembersList };
        this.OldMemberRepeater.DataBind();


    }

    protected void Member_ItemCommand(Object Sender, RepeaterCommandEventArgs e)
    {
    }
}