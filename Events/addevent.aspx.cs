using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Events_addevent : System.Web.UI.Page
{
    public string DateModified { get; set; }
    private string clubID;
    private string clubName;
    private string hostClubID;
    private string hostClubName;
    private string visitingClubID;
    private string visitingClubName;
    private string EventID;
    Settings clubSettings;
    private DateTime Deadline;
    private DateTime PostDate;
    private DateTime EventDate;
    private string EventType;
    private string EventTitle;

    private SysEvent Event;
    private SysEvent newEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        clubSettings = new Settings();
        clubSettings = (Settings)Session["Settings"];
        clubID = clubSettings.ClubID;
        clubName = clubSettings.ClubInfo.ClubName;
        lblClub.Text = clubName;
        EventID = clubID;
        newEvent = new SysEvent();
        newEvent.EClubID = clubID;
        if (IsPostBack)
        {
            newEvent = (SysEvent)Session["Event"];
        }
        else
        {
            btnSave.Enabled = false;
            btnSave.Visible = false;
            Color bc = btnSave.BackColor;
            Color fc = btnSave.ForeColor;
            btnSave.BackColor = System.Drawing.Color.Firebrick;
            btnSave.BackColor = Color.Empty;
            btnSave.ForeColor = Color.White;
            btnCancel.Width = 80;
            btnCancel.Text = "Cancel";            
            Session["Event"] = newEvent;
        }



    }
    protected void ddlPlace_SelectedIndexChanged(object sender, EventArgs e)
    {
        string place = ddlPlace.SelectedItem.Value;
        lbEventType.Text = place;
        ddlPlace.Enabled = false;
        AwayPanel.Visible = false;
        HomePanel.Visible = false;
        MISGAPanel.Visible = false;
        CLUBPanel.Visible = false;
        if (place == "Away")
        {
            AwayPanel.Visible = true;
        }
        if (place == "Home")
        {
            HomePanel.Visible = true;
        }
        if (place == "MISGA")
        {
            MISGAPanel.Visible = true;
        }
        if (place == "Club")
        {
            CLUBPanel.Visible = true;
        }
        if (place == "select")
        {
            lbEventType.Text = "";
            AwayPanel.Visible = false;
            HomePanel.Visible = false;
            MISGAPanel.Visible = false;
            ddlPlace.Enabled = true;
        }
        newEvent.EType = lbEventType.Text;
        newEvent.ETitle = EventTitle;
        Session["Event"] = newEvent;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //
        // Validate all input fields and complete fields that were npt input
        //
        // Then check that not entry exists for this date and time and host club
        //
        // create new event and write to database
        //
        newEvent = (SysEvent)Session["Event"];
        EventType = lbEventType.Text.ToUpper().Trim();
        EventType = newEvent.EType;
        if (EventType == "Home") 
            EventTitle = cbHomeTitleUpper.Checked? tbHomeTitle.Text.ToUpper(): tbHomeTitle.Text;
        if (EventType == "MISGA") 
            EventTitle = cbMISGATitleUpper.Checked? tbMISGATitle.Text.ToUpper():tbMISGATitle.Text;
        if (EventType == "Away") 
            EventTitle = cbAwayTitleUpper.Checked? lblhostClubName.Text.ToUpper():lblhostClubName.Text;
        if (EventType == "Club") 
            EventTitle = cbClubTitleUpper.Checked? tbClubTitle.Text.ToUpper(): tbClubTitle.Text;
        newEvent.ETitle = EventTitle;
        ValidateAddEvent();
        if (EventAddedToDB(Event))
        {
            lblStatus.Text = "Event added successfully";
            btnSave.Visible = false;
            btnCancel.Text = "Add Another";// Event added succesfully
            btnCancel.Width = 120;
        }
        else
        {
            lblStatus.Text = "Event is in the database already.  Please correct a try again.";
            
            // Event not added
        }
        fillInHomeInfo();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Server.Transfer("addevent.aspx");
    }
    protected void btnAwaySelect_Click(object sender, EventArgs e)
    {
        hostClubID = ddlAwayHost.SelectedValue.Trim();
        hostClubName = ddlAwayHost.SelectedItem.Text.Trim();
        if (cbAwayTitleUpper.Checked)
        {
            hostClubName = hostClubName.ToUpper();
        }
        tbAwayTitle.Text = hostClubName;
        lblhostClubName.Text = hostClubName;
        newEvent.EHostID = hostClubID;
        EventTitle = hostClubName;
        EventTitle = tbAwayTitle.Text;
        newEvent.ETitle = EventTitle;
        Session["Event"] = newEvent;
        btnSave.BackColor = Color.Green;
        btnSave.Visible = true;
        btnSave.Enabled = true;

    }
    protected void btnHomeSelect_Click(object sender, EventArgs e)
    {
        visitingClubID = ddlVisit1.SelectedValue.Trim();
        visitingClubName = ddlVisit1.SelectedItem.Text.Trim();
        hostClubID = clubID;
        if (tbHomeTitle.Text == "")
        {
            tbHomeTitle.Text += visitingClubName;
        }
        else
        {
            tbHomeTitle.Text += ", " + visitingClubName;
        }
        if (cbHomeTitleUpper.Checked)
        {
            string temp = tbHomeTitle.Text.ToUpper();
            tbHomeTitle.Text = temp;
        }
        newEvent.EHostID = hostClubID;
        newEvent.ETitle = tbHomeTitle.Text;
        btnSave.Visible = true;
        btnSave.BackColor = Color.Green;
        btnSave.Enabled = true;
    }
    protected void fillInHomeInfo()
    {
    }
    protected void ValidateAddEvent()
    {
        newEvent = (SysEvent)Session["Event"];
        hostClubID = newEvent.EHostID;
        Event = new SysEvent();
        MrTimeZone etz = new MrTimeZone();
        Event.ECreationDate = etz.eastTimeNow();
        string date = tbDate.Text;
        
/*        if (DateTime.TryParse(date, out evDate))
        {
        }
        else{
            // bad input of date
        }
 * */
        string time = tbTeeTime.Text.ToUpper();
        string dateandtime = date+" "+time;
        if (DateTime.TryParse(dateandtime, out EventDate))
        {
        }
        else{
            // Bad date and time input
        }
        EventDate = Convert.ToDateTime(dateandtime);

        int y = EventDate.Year;
        int m = EventDate.Month;
        int d = EventDate.Day;
        int h = EventDate.Hour;

        EventID = clubID + EventDate.ToString("yyMMddHH")+hostClubID;
//      Set up Dealine Date:  if input is blank set Deadline to Event Date - Days specified in Settings
        DateTime tdd;       // use a temporary Deadline date
        if (tbDeadLine.Text.Trim() != "")
        {
            tdd = DateTime.Parse(tbDeadLine.Text);
        }
        else
        {
            int span = (clubSettings.ClubInfo.DeadlineSpan > 0)?clubSettings.ClubInfo.DeadlineSpan: 4;
            tdd = EventDate.AddDays(-span);
        }
        Deadline = new DateTime(tdd.Year, tdd.Month, tdd.Day, 12, 0, 0);  // set Deadline Date time to NOON
//      If post Date input is empty, set Post Date to Event Date minus days specified in Settings
        DateTime tpd =  new DateTime(EventDate.Year, 1, 1, 0, 0, 0);           //  use temporary Post Date
        if (tbPostDate.Text.Trim() != "")
        {
            tpd = DateTime.Parse(tbPostDate.Text);
        }
        else
        {
            int span = clubSettings.ClubInfo.PostSpan;
            if (span != 0)
            {
                tpd = EventDate.AddDays(-span);
            }
        }
        PostDate = new DateTime(tpd.Year, tpd.Month, tpd.Day, 9, 0, 0);

        Event.EClubID = clubSettings.ClubID;
        Event.Id = EventID;
        Event.EDate = EventDate;
//        Event.ETime = EventDate.ToString("h:mm tt");
        Event.EType = newEvent.EType;
        Event.EHostID = hostClubID;
        Event.ETitle = newEvent.ETitle;
        string tCost = tbCost.Text.Trim();
        if (tCost.Length == 0)
        {
            tCost = "tbd";
            Event.ECost= "tbd";
        }
        else
        {
            if (tCost.ToUpper() == "TBD")
            {
                Event.ECost = tCost;
            }
            else
            {
                Event.ECost = (tCost.Substring(0, 1).Equals("$")) ? tCost.Trim() : "$" + tCost;
            }
        }
        Event.ECost = (cbCash.Checked) ? Event.ECost + "*" : Event.ECost;
        if (tbPlayerLimit.Text == "")
        {
            Event.EPlayerLimit = 0;
        }
        else
        {
            Event.EPlayerLimit = Convert.ToInt32(tbPlayerLimit.Text);
        }
        Event.EDeadline = Deadline;
        Event.EPostDate = PostDate;
        Event.EHostPhone = clubSettings.ClubInfo.ProPhone;
        Event.ESpecialRule = tbSpecialRule.Text.Trim();
        Event.EGuest = (cbGuest.Checked) ? "Guest" : "";
            
    }
    protected string GetHomeDate()
    {
        string d = "";
//        d = window.prompt("Welcome?", "Enter your name here.");
//        document.write("Welcome " + theResponse + ".<BR>");
        return d;
    }
    protected void btnMISGASelect_Click(object sender, EventArgs e)
    {
        hostClubID = ddlMISGAHost.SelectedValue.Trim();
        hostClubName = ddlMISGAHost.SelectedItem.Text.Trim();
        EventTitle = tbMISGATitle.Text + " @ "+hostClubName;
        if (cbMISGATitleUpper.Checked)
        {
            EventTitle = EventTitle.ToUpper();
        }
        tbMISGATitle.Text = EventTitle;
        newEvent.EHostID = hostClubID;
        newEvent.ETitle = EventTitle;
        Session["Event"] = newEvent;
        btnSave.BackColor = Color.Green;
        btnSave.Visible = true;
        btnSave.Enabled = true;

    }

    protected void processClubEvent()
    {
        EventTitle = tbClubTitle.Text;
        if (cbClubTitleUpper.Checked)
        {
            EventTitle = EventTitle.ToUpper();
            tbClubTitle.Text = EventTitle;
        }
        hostClubID = clubID;
        newEvent.EHostID = hostClubID;
        newEvent.ETitle = EventTitle;
        Session["Event"] = newEvent;
        btnSave.BackColor = Color.Green;
        btnSave.Visible = true;
        btnSave.Enabled = true;
    }

    protected bool EventAddedToDB(SysEvent se)
    {
        bool status = false;
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);
        Events ev = db.Events.FirstOrDefault(p => ((p.ClubID == se.EClubID) && (p.EventID == se.Id)));
        if (ev == null)
        {
            Events newEvent = new Events()
            {
                ClubID = se.EClubID,
                EventID = se.Id,
                Date = se.EDate,
                Type = se.EType,
                Title = se.ETitle,
                Cost = se.ECost,
                //                        Time = e.ETime,
                Deadline = se.EDeadline,
                HostID = se.EHostID,
                SpecialRule = se.ESpecialRule,
                PlayerLimit = se.EPlayerLimit,
                Guest = se.EGuest,
                HostPhone = se.EHostPhone,
                PostDate = se.EPostDate,
                CreationDate = se.ECreationDate
            };
            db.Events.InsertOnSubmit(newEvent);
            db.SubmitChanges();
            status = true;
        }
        else
        {
            status = false;
        }

        return status; 
    }
    protected void btnClubTitleDone_Click(object sender, EventArgs e)
    {
        processClubEvent();
    }
    protected void cbAwayTitleUpper_CheckedChanged(object sender, EventArgs e)
    {
        newEvent = (SysEvent)Session["Event"];
        if (cbAwayTitleUpper.Checked)
        {
            tbAwayTitle.Text = tbAwayTitle.Text.ToUpper();
            EventTitle = tbAwayTitle.Text;
            newEvent.ETitle = EventTitle;
            Session["Event"] = newEvent;
        }
    }
}