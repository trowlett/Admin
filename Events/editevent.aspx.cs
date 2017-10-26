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
    private bool newEventDataOK = true;

    private string eventID;
    private string element;
    private string action;
    private string oldEventID;
    private string newEventID;
    private string errMsg;
    private const string nl = "<br />";
    SysEvent oldEvent;
    SysEvent NewEvent;

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
//        pnlError.Visible = false;
        lblError.ForeColor = Color.Red;
        lblError.Text = "";
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
//            pnlError.Visible = true;
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
                lblError.Text = msg + "  Cannot delete the event until all Sign Ups are canceled." + nl;
//                pnlError.Visible = true;
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
        oldEvent = new SysEvent(ev);
        Session["OldEvent"] = oldEvent;
        tbEditDate.Text = ev.Date.ToString(formatDate);
        tbEditDate.Enabled = true;
        tbEditHost.Text = ev.HostID;
        tbEditHost.Enabled = true;
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
        oldEvent = (SysEvent)Session["OldEvent"];
//        bool saveOK;
//        bool newEventDataOK = true;
        errMsg = "";
        SysEvent nev = new SysEvent();
        nev.EClubID = club.ClubID;
//        nev.ECreationDate = etz.eastTimeNow();
        DateTime newDate;
        if (Verify.Date(tbEditDate.Text, out newDate))
        {
            nev.EDate = newDate;
            tbEditDate.Text = nev.EDate.ToString(formatDate);
//            tbEditTime.Text = nev.EDate.ToShortTimeString();
        }
        else
        {
            errMsg = "- Event Date & Time format error" + nl;
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
            errMsg += "- Invalid Host ID" + nl; ;
            newEventDataOK = false;
        }
        if (hostClubID != oldEvent.EHostID)
        {
            // check title for changing, and
            // check Type for correctness
        }

        if (Verify.EventType(tbEditha.Text))
        {
            nev.EType = tbEditha.Text.Trim();
        }
        else
        {
            newEventDataOK = false;
            errMsg += "- Invalid h/a field -  must be Home, Away, Club. MISGA or MiSGA" + nl;
        }
        if (Verify.Date(tbEditDeadline.Text, out newDate))
        {
            nev.EDeadline = newDate;
            tbEditDeadline.Text = nev.EDeadline.ToString(formatDate);
        }
        else
        {
            newEventDataOK = false;
            errMsg += "- Deadline Date & Time format error" +nl;
        }
        if (Verify.Date(tbEditPost.Text, out newDate))
        {
            nev.EPostDate = newDate;
            tbEditPost.Text = nev.EPostDate.ToString(formatDate);
        }
        else
        {
            newEventDataOK = false;
            errMsg += "- Post Date & Time format error." + nl;
        }
        nev.EPlayerLimit = Verify.PlayerLimit(tbEditPlayerLimit.Text);
        tbEditPlayerLimit.Text = nev.EPlayerLimit.ToString("##0");
        nev.ETitle = tbEditTitle.Text;
        nev.ECost = Verify.Cost(tbEditCost.Text);
        tbEditCost.Text = nev.ECost;
        nev.EGuest = tbEditGuest.Text;
        nev.ESpecialRule = tbEditSR.Text;
        nev.EHostPhone = oldEvent.EHostPhone;
        newEventID = nev.EClubID + nev.EDate.ToString("yyMMddHH") + hostClubID;
        nev.Id = newEventID;
        if (newEventDataOK)                         // Is the information for the new Event OK?
        {
            if (nev.Id == oldEvent.Id)              // Yes Info OK.  Has there been a change to the Event ID?
            {
                SaveChangedEvent(nev);   // Put updated event on database.
                lblError.Text = string.Format("Event {0} successfully updated.", nev.Id);
            }
            else
            {
                // Event data is good and there is a change in the Event ID
                // Event ID has changed.  Need to replace old Event with the new one making sure all signup entries 
                // for the old Event are now associated with the new Event plus some other validations for the new Event
                //
                // has Host ID changed?  If so, make sure Title has changed.  
                if (oldEvent.EHostID != nev.EHostID)
                {
                    if (oldEvent.ETitle == nev.ETitle)
                    {
                        // just notify that the event ID has changed but the TITLE remains the same.
                        //  This is not an error but need to notify the user.
                        //
                        errMsg += "+ Title is likely to change since Host Club has changed" + nl;
                    }
                    if (oldEvent.EType.ToUpper() == "HOME")
                    {
                        if (nev.EType.ToUpper() == "HOME")
                        {
                            errMsg += "+ Home Host ID changed to another course but Event Type stayed as Home Event" + nl;
                        }
                    }
                }
                // Validate Event Type with a change in HostID also because it might have changed from Away to Home.
                // add new Event
                if (SysEvent.EventAddToDB(nev))
                {
                   errMsg += string.Format("+ New Event {0} added to Database Succcessfully.{1}",nev.Id, nl);
                    lblEventID.Text = nev.Id;
                    // change signup list for old event to new event
                    int SignUpsChanged = SignupList.ChangeListToNewEvent(oldEvent, nev);
                    errMsg += string.Format("+ {0} Signup Enties changed to new Event.{1}", SignUpsChanged,nl);
                    // delete old event
                    club = new Settings();
                    club = (Settings)Session["Settings"];
                    string sdbc = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
                    MRMISGADB db = new MRMISGADB(sdbc);
                    //
                    //  Delete selected Event in SQL database
                    //
                    var ev = db.Events.FirstOrDefault(ex => ex.ClubID == club.ClubID && ex.EventID == oldEvent.Id);
                    db.Events.DeleteOnSubmit(ev);
                    db.SubmitChanges();
                    errMsg += string.Format("+ Event ID: {0} successfully deleted.", eventID);


                }
                else
                {
                    errMsg += "- New Event already on database. Please try again." + nl;
                    newEventDataOK = false;
                }

            }

        }
        if (!newEventDataOK)
        {

        }
        lblStatus.Text =  errMsg +nl;
        btnCancel.Text = "Back";
        btnCancel.ToolTip = "Click to go back to Modify Events Page.";
        btnSave.Visible = false;


    }
    protected bool UpdateEventDatabase(SysEvent newEventData, SysEvent oldEventData)
    {
        bool result = true;

        
        return result;
    }

    protected void SaveChangedEvent(SysEvent newEv)
    {
        Settings club = new Settings();
        club = (Settings)Session["Settings"];
        MrTimeZone etz = new MrTimeZone();
        string sdbc = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(sdbc);
;
        var ev = db.Events.FirstOrDefault(et => et.ClubID == club.ClubID && et.EventID == newEv.Id);

        ev.CreationDate = newEv.ECreationDate;
        ev.Date = newEv.EDate;
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