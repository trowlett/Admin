using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;



public partial class Members_updatehandicaps : System.Web.UI.Page
{
    public MembersList ml { get; set; }

    public DateTime hcpDate;

    private string path;
    private string filename;
    Settings clubSettings;

    protected void Page_Load(object sender, EventArgs e)
    {
        clubSettings = new Settings();
        clubSettings = (Settings)Session["Settings"];
        filename = Handicap.GetFilename(clubSettings);
        if (!IsPostBack)
        {
            ShowMembers();
            lblFileName.Text = Handicap.Source(clubSettings);
            tbHcpDate.Text = Handicap.GetHandicapDate(filename).ToShortDateString();
        }
    }


    public void ShowMembers()
    {
        this.ml = MembersList.LoadMembers(clubSettings.ClubID);
        this.MembersListMainRepeater.DataSource = new MembersList[] { this.ml };
        this.MembersListMainRepeater.DataBind();

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        lblUpdateCount.Text = " ";
        lblUpdateCount.ForeColor = System.Drawing.Color.Green;
        int updateCount = 0;
        int countOfPlayers = 0;
        hcpDate = Convert.ToDateTime(tbHcpDate.Text);

        string MSGAClubID = clubSettings.ClubInfo.MSGAClubID;
        List<Handicap> hc = Handicap.LoadHandicaps(clubSettings.ClubInfo.MSGAClubID, filename, hcpDate);
        if (hc.Count > 0)
        {
            string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
            MRMISGADB db = new MRMISGADB(MRMISGADBConn);
            var members = from p in db.Players
                          where p.ClubID.Trim() == clubSettings.ClubID.Trim()
                          orderby p.MemberID, p.PlayerID
                          select p;
            if (members != null)
            {
                int i = 0;
                countOfPlayers = 0;
                foreach (var member in members)
                {
                    countOfPlayers++;
                    while (i < hc.Count)
                    {
                        string hid = hc[i].ID.Trim();
                        string mid = member.MemberID.Trim();
                        if (mid.CompareTo(hid) == 0)            //  Equal to
                        {
                            member.Hcp = hc[i].Index;
                            member.HDate = hc[i].Date;
                            updateCount++;
                            goto NextMember;
                        }
                        if (mid.CompareTo(hid) == -1)           // Greater Than
                        {
                            goto NextMember;
                        }
                        if (mid.CompareTo(hid) == 1)          //  Less Than
                        {
                            i++;                                //  next Handicap List Entry
                        }
                    }
                NextMember: ;
                }
                ShowMembers();
                lblUpdateCount.Text = string.Format("{0} of {1} Handicaps Updated", updateCount, countOfPlayers);
                lblUpdateCount.Font.Bold = true;
                lblUpdateCount.ForeColor = System.Drawing.Color.Green;
                db.SubmitChanges();
            }
        }
        ShowMembers();
    }
}
