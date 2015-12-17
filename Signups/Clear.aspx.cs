using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Signups_Clear : System.Web.UI.Page
{
    Settings clubSettings;

    protected void Page_Load(object sender, EventArgs e)
    {
        clubSettings = new Settings();
        clubSettings = (Settings)Session["Settings"];
        lblMarkedCount.Text = String.Format("There are {0:##,##0} marked records in the Signup (PlayersList) Database.", SignupList.MarkedEntryCount(clubSettings.ClubID));
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        SignupList.PurgeMarkedEntries(clubSettings.ClubID);
        lblPurgeCount.Text = "Entries purged = " + SignupList.EntriesPurged.ToString("##,##0");
    }
}