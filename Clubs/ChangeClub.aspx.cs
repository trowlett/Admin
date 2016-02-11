using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Clubs_ChangeClub : System.Web.UI.Page
{
    public string club;
    Settings clubSettings;

    protected void Page_Load(object sender, EventArgs e)
    {
        clubSettings = new Settings();
        clubSettings = (Settings)Session["Settings"];
        club = string.Format("{0}: {1}", clubSettings.ClubID, clubSettings.ClubInfo.ClubName);

        if (!IsPostBack)
        {
            LoadClubDDL();
        }
    }
    protected void LoadClubDDL()
    {
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);
        var query = (from cs in db.ClubSettings
                     where ((cs.ClubID != "") && (cs.Active.ToUpper().Trim() == "YES"))
                     orderby cs.OrgName
                     select cs);
        ddlClub.Items.Clear();
        if (query != null)
        {
            ListItem x = new ListItem();
            x.Text = " - select - ";
            x.Value = "";
            ddlClub.Items.Add(x);
            foreach (var item in query)
            {
                ListItem li = new ListItem();
                li.Text = item.OrgName;
                li.Value = item.ClubID.Trim();
                ddlClub.Items.Add(li);
            }
            ListItem addClub = new ListItem();
            addClub.Text = " - Add Club - ";
            addClub.Value = "add";
            ddlClub.Items.Add(addClub);
        }
        else
        {
            MessageLabel.Text = "NO Clubs Settings Information Available.<br />";
            ddlClub.Enabled = false;
            ddlClub.Visible = false;
        }
    }
    protected void ddlClub_IndexChanged(object sender, EventArgs e)
    {
        string clubid = ddlClub.SelectedValue.ToString().Trim();
        if (clubid != "")
        {
            Settings cs = new Settings();
            cs.ClubID = clubid;
            cs.ClubInfo = ClubManager.GetSetting(cs.ClubID);
            Session["Settings"] = cs;
            string strRedirect = "ChangeClub.aspx";
            Response.Redirect(strRedirect, true);

        }
        else
        {
            MessageLabel.Text = "Must Select a Club<br />";
        }
    }
}