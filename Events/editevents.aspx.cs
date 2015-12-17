using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Schedule_Default : System.Web.UI.Page
{
    public string clubid { get; set; }
    Settings clubSettings;

    protected void Page_Load(object sender, EventArgs e)
    {
        clubSettings = new Settings();
        clubSettings = (Settings)Session["Settings"];
        clubid = clubSettings.ClubID;
        SqlDataSource2.SelectParameters["ClubID"].DefaultValue = clubid;    // Set Sql Select Parameter to current club id
        lblClubName.Text = clubSettings.ClubInfo.ClubName;
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}