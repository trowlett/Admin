using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Clubs_ClubsMain : System.Web.UI.Page
{
    public ClubParameterCollection cpc { get; set; }
    private Settings clubSettings;
    private string roleLevel = "";



    protected void Page_Load(object sender, EventArgs e)
    {
        clubSettings = (Settings)Session["Settings"];
        MessageLabel.Text = "";

        clubSettings = new Settings();
        this.clubSettings = (Settings)Session["Settings"];
        HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

        // Decrypts the FormsAuthenticationTicket that is held in the cookie's .Value property.
        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

        // The "authTicket" variable now contains your original, custom FormsAuthenticationTicket,
        // complete with User-specific custom data.  You can then check that the FormsAuthenticationTicket's
        // .Name property is for the correct user, and perform the relevant functions with the ticket.
        //
        roleLevel = authTicket.UserData.Trim();
//        roleLevel = "3";                          // setup test condition for "CanDeleteClubSettings"

        if (!IsPostBack)
        {
            LoadClubDDL();
        }
        
        pnlViewForm.Visible = false;
    }
    protected bool CanDeleteClubSettings
    {
        get
        {
            return this.roleLevel == "4";
        }
    }
    protected void LoadClubDDL()
    {
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);
        var query = (from cs in db.ClubSettings
                     where (cs.ClubID != "")
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

    protected void FormView1_ModeChanged(object sender, EventArgs e)
    {
        MessageLabel.Text = "";
        ddlClub.SelectedIndex = 0;
    }
    protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        pnlViewForm.Visible = true;
        ddlClub.Visible = false;
        ddlClub.Enabled = false;
        // 
        // Check that iten is not already on file
        // make sure all fields have data in them
        //
        MessageLabel.Text = "";
        if (e.Values["ClubID"].Equals(""))
        {
            MessageLabel.Text += "Enter a Club ID.<br />";
            e.Cancel = true;
            return;
        }
        string newClubID = e.Values["ClubID"].ToString().Trim();
        string sdbc = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(sdbc);
        var cs = db.ClubSettings.FirstOrDefault(c => c.ClubID.Trim() == newClubID);
        if (cs != null)
        {
            MessageLabel.Text += string.Format("Club ID {0} already om file. Try again.",newClubID);
            e.Cancel = true;
            return;
        }
        if (e.Values["DeadlineSpan"].Equals("")) e.Values["DeadlineSpan"] = "4";
        if (e.Values["PostSpan"].Equals("")) e.Values["PostSpan"] = "45";
        if (e.Values["MSGAClubID"].Equals("")) e.Values["MSGAClubID"] = " ";

        foreach (DictionaryEntry entry in e.Values)
        {
            if (entry.Value.Equals(""))
            {
                // Use the Cancel property to cancel the 
                // insert operation.
                e.Cancel = true;

                MessageLabel.Text += "<br />Please enter a value for the " +
                  entry.Key.ToString() + " field.<br/>";

            }
        }

    }
    protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
//        if (e.Values["MSGAClubID"].Equals("")) e.Values["MSGAClubID"] = " ";
        string cid = e.Keys["ClubID"].ToString().Trim();

    }
    protected void FormView1_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        string cid = e.Keys["ClubID"].ToString().Trim();
        if (roleLevel == "4")                           // Level 4 (Master) is only level permitted to delete Club data
        {
            string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
            MRMISGADB db = new MRMISGADB(MRMISGADBConn);
            var clubToDelete = db.ClubSettings.FirstOrDefault(c => c.ClubID.Trim() == cid);
            if (clubToDelete != null)
            {
                db.ClubSettings.DeleteOnSubmit(clubToDelete);
                db.SubmitChanges();
                MessageLabel.Text += string.Format("<br />Club {0} Successfully Deleted.<br />", cid);
            }
            else
            {
                MessageLabel.Text = string.Format("<br /> Club {0} not in Club Settings Database.<br />", cid);
            }
            e.Cancel = true;
        }
        else
        {
            MessageLabel.Text += string.Format("<br />Your are not Authorized to delete Club {0}<br />", cid);
            e.Cancel = true;
        }
        ddlClub.SelectedIndex = 0;

    }

    protected void ddlClub_IndexChanged(object sender, EventArgs e)
    {
        string clubid = ddlClub.SelectedValue.ToString().Trim();
        if (clubid != "")
        {
            if (clubid == "add")
            {
                pnlViewForm.Visible = true;
            }
            else
            {

                SqlClubParameters.SelectParameters["xClub"].DefaultValue = clubid;
                FormView1.DataBind();
                pnlViewForm.Visible = true;
            }
        }
        else
        {
            MessageLabel.Text = "Must Select a Club<br />";
        }
    }

    protected void FormView1_PageIndexChanging(object sender, FormViewPageEventArgs e)
    {

    }
}