using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Events_modifyEvents : System.Web.UI.Page
{
    public MrSchedule Schedule { get; set; }
    public DateTime displayDate;
    Settings clubSettings;
    public string DateModified { get; set; }

    public const string formatDate = "M/d/yy";

    private DateTime highDate; 

    protected void Page_Load(object sender, EventArgs e)
    {
        highDate = new DateTime(2030,12,31,23,59,59);

        clubSettings = new Settings();
        clubSettings = (Settings)Session["Settings"];
        SignupDates sd = new SignupDates();
        displayDate = sd.getDisplayDate(clubSettings.ClubID);
        displayDate = highDate;
//        displayDate = new DateTime(2013, 11, 2);
        load_schedule();

    }

    protected void load_schedule()
    {
        this.Schedule = MrSchedule.LoadFromDB(clubSettings.ClubID);
        this.ScheduleRepeater.DataSource = new MrSchedule[] { this.Schedule };
        this.ScheduleRepeater.DataBind();
    }

}