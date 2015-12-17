using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Account_Login : System.Web.UI.Page
{
    Users usr;
//    Settings clubSettings;


    protected void Page_Load(object sender, EventArgs e)
    {
        usr = new Users();
        tbUserName.Focus();


    }

    protected string GetUser()
    {
        return Context.User.Identity.Name;
    }

    protected bool GetRequestStatus()
    {
        return Request.IsAuthenticated;

    }
 //   protected void btnLogin_Click(object sender, EventArgs e)
//    {

//    }

    private bool ValidateUser(string userName, string passWord)
    {

        string lookupPassword = null;

        string sdbc = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(sdbc);

        // Check for invalid userName.
        // userName must not be null and must be between 1 and 15 characters.
        if ((null == userName) || (0 == userName.Length) || (userName.Length > 15))
        {
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of userName failed.");
            return false;
        }

        // Check for invalid passWord.
        // passWord must not be null and must be between 1 and 25 characters.
        if ((null == passWord) || (0 == passWord.Length) || (passWord.Length > 25))
        {
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of passWord failed.");
            return false;
        }

        try
        {

            var cs = db.Users.FirstOrDefault(c => c.UserID.ToLower() == userName.ToLower());
            if (cs != null)
            {
                lookupPassword = cs.Password;
                usr.UserID = cs.UserID;
                usr.Name = cs.Name;
                usr.ClubID = cs.ClubID;
                usr.Email = cs.Email;
                usr.Phone = cs.Phone;
                usr.role = cs.role;
                usr.LoginCount = cs.LoginCount;
            }

        }
        catch (Exception ex)
        {
            // Add error handling here for debugging.
            // This error message should not be sent back to the caller.
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }

        // If no password found, return false.
        if (null == lookupPassword)
        {
            // You could write failed login attempts here to event log for additional security.
            return false;
        }

        // Compare lookupPassword and input passWord, using a case-sensitive comparison.
        if (0 == string.Compare(lookupPassword, passWord, false))
        {
            MrTimeZone tz = new MrTimeZone();
            
            var cs = db.Users.FirstOrDefault(c => c.UserID == userName);
            cs.LoginCount++;
            cs.LastLogin = tz.eastTimeNow();
            db.SubmitChanges();
           return true;
        }
        return false;


    }

    private void cmdLogin_ServerClick(object sender, System.EventArgs e)
    {
        if (ValidateUser(tbUserName.Text, tbUserPass.Text))
        {
            FormsAuthenticationTicket tkt;
            string cookiestr;
            HttpCookie ck;
            string roleLevel = "1";         // set role ;eve; to Lowest or Guest
            if (usr.role.Trim().Substring(0, 4) == "gues") roleLevel = "1";
            if (usr.role.Trim().Substring(0, 4) == "user") roleLevel = "2";
            if (usr.role.Trim().Substring(0, 4) == "admi") roleLevel = "3";
            if (usr.role.Trim().Substring(0, 4) == "mast") roleLevel = "4";
            tkt = new FormsAuthenticationTicket(1, usr.UserID, DateTime.Now,
                DateTime.Now.AddMinutes(30), chkPersistCookie.Checked, roleLevel);
            cookiestr = FormsAuthentication.Encrypt(tkt);
            ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
            if (chkPersistCookie.Checked)
                ck.Expires = tkt.Expiration;
            ck.Path = FormsAuthentication.FormsCookiePath;
            Response.Cookies.Add(ck);
            Settings cs = new Settings();
            cs.ClubID = usr.ClubID;
            cs.ClubInfo = ClubManager.GetSetting(cs.ClubID);
            Session["Settings"] = cs;
            string strRedirect;
            strRedirect = Request["ReturnUrl"];
            if (strRedirect == null)
                strRedirect = "default.aspx";
            Response.Redirect(strRedirect, true);
        }
        else
        {
            //            Response.Redirect("logon.aspx", true);
            lblMsg.Text = "Invalid User ID or Password.  Try again.";
        }
    }
    protected void btnLogon_Click(object sender, EventArgs e)
    {
        cmdLogin_ServerClick(sender, e);
    }
}