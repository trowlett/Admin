using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GetClubID : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        tbClubID.Focus();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string clubID = tbClubID.Text.Trim();
        if (clubID == "") clubID = "999";
        string url = "Default.aspx?CLUB=" + clubID;
        
       Response.Redirect(url);
    }
}