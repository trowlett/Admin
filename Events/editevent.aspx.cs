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
    private string oldEventID;
    private string newEventID;

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
//            tbDelTime.Text = ev.Date.ToShortTimeString();
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
//        tbEditDate.Text = ev.Date.ToString("MM/dd/yyyy");
        tbEditDate.Enabled = true;
//        tbEditDate.ToolTip = "Change date.";
        tbEditHost.Text = ev.HostID;
        tbEditHost.Enabled = true;
//        tbEditHost.ToolTip = "Change Host ID.";
//        tbEditTime.Text = ev.Date.ToShortTimeString();
//        tbEditTime.Enabled = false;
//        tbEditTime.ToolTip = "Change Time.";
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
        MrTimeZone etz = new MrTimeZone();
        //
        // To Do:   Data in Textboxes needs to be validated
        //
        oldEventID = eventID;
        bool saveOK;
        bool newEventDataOK = true;
        string errMsg = "";
        SysEvent nev = new SysEvent();
        nev.EClubID = club.ClubID;
        nev.ECreationDate = etz.eastTimeNow();
        DateTime newDate;
        if (Verify.Date(tbEditDate.Text, out newDate))
        {
            nev.EDate = newDate;
            tbEditDate.Text = nev.EDate.ToString(formatDate);
//            tbEditTime.Text = nev.EDate.ToShortTimeString();
        }
        else
        {
            errMsg = "Event Date & Time format error";
            newEventDataOK = false;
        }

        string hostClubID = tbEditHost.Text.Trim();
        if (Verify.HostClubID(hostClubID))
        {
            nev.EHostID = hostClubID;
            tbEditHost.Text = hostClubID;
        }
        else
        {
            errMsg += "; Invalid Host ID";
            newEventDataOK = false;
        }

        if (Verify.EventType(tbEditha.Text))
        {
            nev.EType = tbEditha.Text.Trim();
        }
        else
        {
            newEventDataOK = false;
            errMsg += "; Invalid h/a field -  must be Home, Away, Club. MISGA or MiSGA";
        }
        if (Verify.Date(tbEditDeadline.Text, out newDate))
        {
            nev.EDeadline = newDate;
            tbEditDeadline.Text = nev.EDeadline.ToString(formatDate);
        }
        else
        {
            newEventDataOK = false;
            errMsg += "; Deadline Date & Time format error";
        }
        if (Verify.Date(tbEditPost.Text, out newDate))
        {
            nev.EPostDate = newDate;
            tbEditPost.Text = nev.EPostDate.ToString(formatDate);
        }
        else
        {
            newEventDataOK = false;
            errMsg += "; Post Date & Time format error.";
        }
        nev.EPlayerLimit = Verify.PlayerLimit(tbEditPlayerLimit.Text);
        tbEditPlayerLimit.Text = nev.EPlayerLimit.ToString("##0");
/*        int pl = 0;
        if (!int.TryParse(tbEditPlayerLimit.Text, out pl))
        {
            pl = 60;
            tbEditPlayerLimit.Text = "60";
        }
        nev.EPlayerLimit = pl;
        */
        nev.ETitle = tbEditTitle.Text;
        nev.ECost = Verify.Cost(tbEditCost.Text);
        tbEditCost.Text = nev.ECost;
        nev.EGuest = tbEditGuest.Text;
        nev.ESpecialRule = tbEditSR.Text;

        if (newEventDataOK)
        {
            newEventID = nev.EClubID + nev.EDate.ToString("yyMMddHH") + hostClubID;
            nev.Id = newEventID;
            saveOK = UpdateEventDatabase(nev, oldEventID);
            lblStatus.Text = string.Format("Event {0} successfully updated.", eventID);
        }
        else
        {
            lblStatus.Text = errMsg;
        }
        btnCancel.Text = "Back";
        btnCancel.ToolTip = "Click to go back to Modify Events Page.";
        btnSave.Visible = false;


    }
    protected bool UpdateEventDatabase(SysEvent newEventData, string oldEventID)
    {
        bool result = true;

        if (newEventData.Id == oldEventID)
        {
            // if same Event ID, then uppdate Old Event with changes
            SaveChangedEvent(newEventData, oldEventID);
        }
        else
        {
            // has Host I changed?  If so, make sure Title has changed.  
            // Validate Event Type with a change in HostID also because it might have changed from Away to Home.
            // add new Event
            // change signup list for old event to new event
            // delete old event
        }
        return result;
    }

    protected void SaveChangedEvent(SysEvent newEv, string oldID)
    {
        Settings club = new Settings();
        club = (Settings)Session["Settings"];
        MrTimeZone etz = new MrTimeZone();
        string sdbc = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(sdbc);
;
        var ev = db.Events.FirstOrDefault(et => et.ClubID == club.ClubID && et.EventID == oldID);

        ev.CreationDate = newEv.ECreationDate;
        ev.Title = newEv.ETitle;
        ev.Type = newEv.EType;
        ev.Cost = newEv.ECost;
        ev.PlayerLimit = newEv.EPlayerLimit;
        ev.Deadline = newEv.EDeadline;
        ev.Guest = newEv.EGuest;
        ev.PostDate = newEv.EPostDate;
        ev.SpecialRule = newEv.ESpecialRule;
        db.SubmitChanges();
        return;
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