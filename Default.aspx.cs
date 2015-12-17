using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Maintenance_Default : System.Web.UI.Page
{
//    private string clubID;

    protected string OrgURL { get; set; }
    public Settings clubSettings { get; set; }
    protected string txtUserName;


    protected void Page_Load(object sender, EventArgs e)
    {
//        if (Request.QueryString["CLUB"] == null)
//        {
//            string url = "GetClubID.aspx";                          // set up to ask for club ID
//            Server.Transfer("~/Account/Login.aspx");              // Transfer to implement login when not commented out
//            Server.Transfer(url);                                   // Transfer to ask for Club ID
//        }
//        clubSettings = new Settings();
//        clubID = Request.QueryString["CLUB"].Trim();
//        clubSettings.ClubID = clubID;
//        string x = clubSettings.ClubID;
 //       clubSettings.ClubInfo = ClubManager.GetSetting(clubSettings.ClubID);
 //       Session["Settings"] = clubSettings;
        //
        // Below is the code for using LOG ON access
        //
        clubSettings = (Settings)Session["Settings"];
        if (clubSettings == null)
        {
            Server.Transfer("Login.aspx");
        }
        btnHome.Text = "Go To:  "+clubSettings.ClubInfo.WebSite;
//        hlOrgURL.NavigateUrl = "http://"+clubSettings.ClubInfo.WebSite;
//        hlOrgURL.Text = clubSettings.ClubInfo.OrgName;
        HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

        // Decrypts the FormsAuthenticationTicket that is held in the cookie's .Value property.
        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

        // The "authTicket" variable now contains your original, custom FormsAuthenticationTicket,
        // complete with User-specific custom data.  You can then check that the FormsAuthenticationTicket's
        // .Name property is for the correct user, and perform the relevant functions with the ticket.
        //
        if (this.clubSettings != null)
        {
            OrgURL = clubSettings.ClubInfo.MISGAURL;
//            literalOrg.Text = clubSettings.ClubInfo.ClubName + "<br />";
//            NavigationMenu.Items[0].NavigateUrl = "~/Default.aspx?CLUB=" + clubSettings.ClubID;
//            SetupMenu(NavigationMenu, authTicket.UserData);
            minUserPanel.Visible = false;
            userPanel.Visible = false;
            adminPanel.Visible = false;
            if (authTicket.UserData == "1")
            {
                minUserPanel.Visible = true;
            }
            if (authTicket.UserData == "2")
            {
                userPanel.Visible = true;
            }
            if ((authTicket.UserData == "3") || (authTicket.UserData == "4"))
            {
                adminPanel.Visible = true;
            }
        }
    }

    protected void btnHome_Click(object sender, EventArgs e)
    {
        clubSettings = (Settings)Session["Settings"];
        FormsAuthentication.SignOut();
        string url = "http://" + clubSettings.ClubInfo.WebSite;
        Response.Redirect(url);
    }
}