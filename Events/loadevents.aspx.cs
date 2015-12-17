using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Events_loadevents : System.Web.UI.Page
{
    public DateTime LastDate { get; set; }
    public MrSchedule Schedule { get; set; }
    private string filename;
    private int countOfEvents;
    public string scheduleDate;
    public string textFileName { get; set; }
    private string PathPrefix;
    Settings clubSettings;


    protected void load_schedule()
    {
        this.Schedule = MrSchedule.LoadFromCsv(clubSettings.ClubID, filename);
        this.scheduleDate = this.Schedule.CreateTime.ToLongDateString();
        countOfEvents = this.Schedule.Events.Count;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        clubSettings = new Settings();
        clubSettings = (Settings)Session["Settings"];
        string tempPath = Server.MapPath("..");
        int posLast = tempPath.LastIndexOf("\\");
        PathPrefix = tempPath.Substring(0, posLast) + "\\" + clubSettings.ClubInfo.WebSite;
        textFileName = clubSettings.ClubID.Trim() + "-schedule.txt";
        filename = PathPrefix + "\\App_Data\\" + textFileName;
        filename = Server.MapPath("~\\App_Data\\") + textFileName;
//        filename = Server.MapPath("~\\App_Data\\schedule.txt");
        string[] fn = filename.Split('\\');
        string newPath = "..";
        for (int i = 3; i > 0; i--)
        {
            newPath = newPath + "\\" + fn[fn.Length - i];
        }

        lblFileName.Text = string.Format("From FileName = {0}", filename);
        lblFileName.Text = string.Format("From FileName = {0}", newPath);
        BtnLoadText.Text = "Load Events from: " + textFileName;
        BtnLoadText.Text = string.Format("Load Events from:  {0}", newPath);
        lblFN1.Text = textFileName;
        if (System.IO.File.Exists(filename))
        {
            BtnLoadText.Visible = true;
            lblDbLoadStatus.Text = "";
        }
        else{
            BtnLoadText.Visible = false;
            lblDbLoadStatus.Text = "File not found.";
            lblDbLoadStatus.ForeColor = System.Drawing.Color.Red;
        }
//        lblPath1a.Text = Server.MapPath(".");
//        lblPath2a.Text = Server.MapPath("..");
//        lblPath3a.Text = Server.MapPath("~");
//        lblPath4a.Text = Server.MapPath("//");
        if (IsPostBack)
        {
            //  MrResources mr = new MrResources();
            //  path = Server.MapPath(mr.Root);
            if (BtnLoadText.Visible)
            {
            BtnLoadText.Enabled = true;
            lblDbLoadStatus.Text = "";
            lblDbLoadStatus.ForeColor = System.Drawing.Color.DarkSlateGray;
            }
        }

    }

    protected void BtnLoadText_Click(object sender, EventArgs e)
    {
        load_schedule();
        lblDbLoadStatus.Text = string.Format("{0} Events now in the database.  File date:  {1}",countOfEvents, scheduleDate);
        BtnLoadText.Enabled = false;
        DataBind();
        SystemParameters.Update(clubSettings.ClubID,SystemParameters.ScheduleDate, scheduleDate);
    }
}