using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Events_editevent : System.Web.UI.Page
{
    public MrSchedule Schedule { get; set; }
    public DateTime displayDate;
    Settings clubSettings;
    public string DateModified { get; set; }

    public int ActiveSignups = 0;

    private string eventID;
    private string element;
    private string action;

    private const string formatDate = "M/d/yy h:mm tt";

    protected void Page_Load(object sender, EventArgs e)
    {
        eventID = Request.QueryString["ID"];
        element = Request.QueryString["element"];
//        Session["EVENTID"] = eventID;
        clubSettings = new Settings();
        clubSettings = (Settings)Session["Settings"];
        lblEventID.Text = eventID;
        lblElement.Text = element;
        pnlError.Visible = false;
        lblError.ForeColor = Color.Red;
        action = "Edit";
        lblAction.Text = action;
        lblStatus.Text = "";
        if (element == "delete")
        {
            if (!IsPostBack)
            {
                action = "Delete";
                ShowEventDelete(clubSettings, eventID);
                // btnDelete.Enabled = true;
                btnDelete.ToolTip = "Click to delete this event from the database.";
                btnCancelDelete.Text = "Cancel";
                btnCancelDelete.ToolTip = "Click here to cancel delete.";
                // pnlDelete.Visible = true;
            }
        }
        else
        {
            if (!IsPostBack)
            {
                ShowEventEdit(clubSettings, eventID);
                btnSave.ForeColor = Color.White;
                btnSave.BackColor = Color.Green;
                btnSave.Visible = true;
                btnSave.ToolTip = "Click to save your changes in the Events Database";
                btnCancel.Text = "Cancel";
                btnCancel.ToolTip = "Click here to cancel any changes you made to the above items.";
                pnlEdit.Visible = true;
            }
        }
//        SignupDates sd = new SignupDates();
//        displayDate = sd.getDisplayDate(clubSettings.ClubID);
//        displayDate = new DateTime(2013, 11, 2);
//        load_schedule();
        

    }

    protected void ShowEventDelete(Settings club, string eventID)
    {
        btnDelete.Visible = false;
        string sdbc = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(sdbc);

        var ev = db.Events.FirstOrDefault(x => x.ClubID == club.ClubID && x.EventID == eventID);

        if (ev == null)
        {
            string msg = "Derp:  Event: " + eventID + " not in database!  Try again.";
            lblError.Text = msg;
            pnlError.Visible = true;
            return;
        }
        else
        {
            pnlDelete.Visible = true;
            tbDelDate.Text = ev.Date.ToString(formatDate);
            tbDelHost.Text = ev.HostID;
            tbDelTime.Text = ev.Date.ToShortTimeString();
            tbDelTitle.Text = ev.Title.Trim();
            tbDelha.Text = ev.Type.Trim();
            tbDelCost.Text = ev.Cost.Trim();
            tbDelDeadline.Text = ev.Deadline.ToString(formatDate).Trim();
            tbDelPlayerLimit.Text = ev.PlayerLimit.ToString().Trim();
            tbDelPost.Text = ev.PostDate.ToString(formatDate).Trim();
            tbDelSR.Text = ev.SpecialRule.Trim();
            tbDelGuest.Text = ev.Guest.Trim();

            ActiveSignups = SignupList.CountActiveSignupsInEvent(clubSettings.ClubID, eventID);

            Session["ActiveSignups"] = ActiveSignups.ToString();
            if (ActiveSignups > 0)
            {
                string msg = String.Format("Event {0} has {1} ", eventID, ActiveSignups);
                if (ActiveSignups == 1)
                {
                    msg = msg + "person signed up for it.";
                }
                else
                {
                    msg = msg + "people signed up for it.";
                }
                lblError.Text = msg + "  Cannot delete the event until all Sign Ups are canceled.";
                pnlError.Visible = true;
                return;
            }
            btnDelete.Visible = true;
            btnDelete.BackColor = Color.Green;
            btnDelete.ForeColor = Color.White;
        }

    }

    protected void DeleteEvent(Settings club, string eventID)
    {

    }

    protected void ShowEventEdit(Settings club, string eventID)
    {
        string sdbc = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(sdbc);

        var ev = db.Events.FirstOrDefault(e => e.ClubID == club.ClubID && e.EventID == eventID);

        if (ev == null)
        {
            string msg = "Derp:  Event: " + eventID + " not in database!";
            throw new InvalidOperationException(msg);
        }
        tbEditDate.Text = ev.Date.ToString(formatDate);
        tbEditDate.Enabled = false;
        tbEditDate.ToolTip = "Cannot edit date. Delete Event and add event with new date.";
        tbEditHost.Text = ev.HostID;
        tbEditHost.Enabled = false;
        tbEditHost.ToolTip = "Cannot edit Host ID.  Delete event and add new event with correct Host ID.";
        tbEditTime.Text = ev.Date.ToShortTimeString();
        tbEditTime.Enabled = false;
        tbEditTime.ToolTip = "Cannot edit Time.  Delete event and add new event with correct time.";
        tbEditTitle.Text = ev.Title.Trim();
        tbEditha.Text = ev.Type.Trim();
        tbEditCost.Text = ev.Cost.Trim();
        tbEditDeadline.Text = ev.Deadline.ToString(formatDate).Trim();
        tbEditPlayerLimit.Text = ev.PlayerLimit.ToString().Trim();
        tbEditPost.Text = ev.PostDate.ToString(formatDate).Trim();
        tbEditSR.Text = ev.SpecialRule.Trim();
        tbEditGuest.Text = ev.Guest.Trim();
        

    }

    protected void EditEvent(Settings club, string eventID)
    {
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
//        eventID = (string)Session["EVENTID"];
        Settings club = new Settings();
        club = (Settings)Session["Settings"];
        string sdbc = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(sdbc);
        //
        // To Do:   Data in Textboxes need to be validated
        //
        //  Update selected Event in SQL database
        //

        var ev = db.Events.FirstOrDefault(et => et.ClubID == club.ClubID && et.EventID == eventID);
        MrTimeZone etz = new MrTimeZone();
        ev.CreationDate = etz.eastTimeNow();
        ev.Title = tbEditTitle.Text;
        ev.Type = tbEditha.Text;
        ev.Cost = tbEditCost.Text;
        ev.Deadline = Convert.ToDateTime(tbEditDeadline.Text);
        ev.Guest = tbEditGuest.Text;
        ev.PlayerLimit = Convert.ToInt32(tbEditPlayerLimit.Text);
        ev.PostDate = Convert.ToDateTime(tbEditPost.Text);
        ev.SpecialRule = tbEditSR.Text;
        db.SubmitChanges();
        lblStatus.Text = string.Format("Event {0} successfully updated.",eventID);
        btnCancel.Text = "Back";
        btnCancel.ToolTip = "Click to go back to Modufy Events Page.";
        btnSave.Visible = false;


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("modifyEvents.aspx");
    }
    protected void btnCancelDelete_Click(object sender, EventArgs e)
    {
        Response.Redirect("modifyEvents.aspx");
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Settings club = new Settings();
        club = (Settings)Session["Settings"];
        string sdbc = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(sdbc);
        //
        //  Delete selected Event in SQL database
        //
        var ev = db.Events.FirstOrDefault(ex => ex.ClubID == club.ClubID && ex.EventID == eventID);
        db.Events.DeleteOnSubmit(ev);
        db.SubmitChanges();
        lblDelStatus.Text = string.Format("Event ID: {0} successfully deleted.", eventID);
        btnCancelDelete.Text = "Back";
        btnCancelDelete.ToolTip = "Click to go back to Modufy Events Page.";
        btnDelete.Visible = false;
    }
}