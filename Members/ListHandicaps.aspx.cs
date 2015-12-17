using System;
using System.Collections.Generic;
using System.Linq;


public partial class Members_ListHandicaps : System.Web.UI.Page
{
    public List<Handicap> HcpList { get; set; }

    public string club = "";
    private string filename = "";
    Settings clubSettings;
    protected void Page_Load(object sender, EventArgs e)
    {
        clubSettings = new Settings();
        clubSettings = (Settings)Session["Settings"];
        club = clubSettings.ClubInfo.ClubName;
        lblInstructions.Text = string.Format("Click Submit Button to list all {0} handicaps.", club);
        string hs = Handicap.Source(clubSettings);
        lblFileName.Text = hs;

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DateTime hcpDate= Handicap.GetHandicapDate(filename);

        List<Handicap> hcps = Handicap.LoadHandicaps(clubSettings.ClubInfo.MSGAClubID, filename, hcpDate);

        var hl = from h in hcps
                 orderby h.Name
                 select new { h.ID, h.Name, h.Index, h.Date };
        HcpList = new List<Handicap>();
        foreach (var item in hl)
        {
            Handicap h = new Handicap
            {
                ID = item.ID,
                Name = item.Name,
                Index = item.Index,
                Date = item.Date
            };
            HcpList.Add(h);
        }
        lblHandicapCount.Text = string.Format(" {0} Members Listed", HcpList.Count);
        this.HandicapListMainRepeater.DataSource = HcpList;
        this.HandicapListMainRepeater.DataBind();

        btnSubmit.Enabled = false;
        btnSubmit.Visible = false;
        lblInstructions.Visible = false;
/*
        foreach (var item in hl)
        {
            seq = string.Format("{0,5:F0}: ",i+1);
            txt = " ";
            for (int j = 0; j < seq.Length; j++)
            {
                if (seq[j] == ' ') 
                {
                    txt = txt + "&nbsp;";
                }
                else
                {
                    txt = txt + seq[j];
                }
            }
            txt = txt + string.Format("{0,8} - {1} - {2} - {3}", item.ID, item.Name, item.Date.ToShortDateString(), item.Index);
            ltrHcps.Text = ltrHcps.Text + txt+"<br />";
            i++;
        }
 * */
    }
}