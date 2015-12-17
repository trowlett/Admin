using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_usermain : System.Web.UI.Page
{
    public const string formatDate = "M/d/yy";
    public DateTime ChangeDateNow { get; set; }


    protected void Page_Load(object sender, EventArgs e)
    {
        ChangeDateNow = DateTime.Now;
        MessageLabel.Text = "";
    }

    protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        string changeDate = ChangeDateNow.ToString();
        e.NewValues["ChangeDate"] = changeDate;
    }
    protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        MessageLabel.Text = "";
        if (e.Values["UserID"].ToString().Equals(""))
        {
            MessageLabel.Text += "Please enter a User ID<br />";
            e.Cancel = true;
        }
        string newUserID = e.Values["UserID"].ToString().Trim();
        //
        // look for user id already used
        // if so, error message and cancel operation
        //
        string sdbc = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(sdbc);
        var cs = db.Users.FirstOrDefault(c => c.UserID.ToLower().Trim() == newUserID.ToLower());
        if (cs != null)
        {
            MessageLabel.Text += string.Format("User ID [{0}] already used. Try another.",newUserID);
            e.Cancel = true;
            return;
        }
        if (e.Values["Password"].Equals(""))
        {
            MessageLabel.Text += "Please enter a Password<br />";
            e.Cancel = true;
        }
        if (e.Values["ClubID"].Equals(""))
        {
            MessageLabel.Text += "Please enter a Club ID<br />";
            e.Cancel = true;
        }
        if (e.Values["Role"].Equals(""))
        {
            MessageLabel.Text += "Role field not entered.  'users' inserted<br />";
            e.Values["Role"] = "users";
        }
        if (e.Values["Name"].Equals("")) e.Values["Name"] = " ";
        if (e.Values["Phone"].Equals("")) e.Values["Phone"] = " ";
        if (e.Values["Email"].Equals("")) e.Values["Email"] = " ";
/*
        foreach (DictionaryEntry entry in e.Values)
        {
            if (entry.Value.Equals(""))
            {
                // Use the Cancel property to cancel the 
                // insert operation.
                e.Cancel = true;

                MessageLabel.Text += "Please enter a value for the " +
                  entry.Key.ToString() + " field.<br/>";

            }
        }
  * */
        string now = DateTime.Now.ToString();
        e.Values["RegisteredDate"] = now;
        e.Values["ChangeDate"] = now;
        e.Values["LastLogin"] = now;
        e.Values["LoginCount"] = "0";
    }
    protected void FormView1_ModeChanged(object sender, EventArgs e)
    {
        MessageLabel.Text = "";
    }
    protected void FormView1_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {
        String keyValue = e.Keys["UserID"].ToString();
        String clubID = e.Values["ClubID"].ToString() +
          ":  " + e.Values["Name"].ToString();
        string title = e.Values["Role"].ToString().Trim();
        if (title.Equals("admins"))
        {
            MessageLabel.Text = "You cannot delete record " +
              e.RowIndex.ToString() + ". " + clubID +
              " (User ID " + keyValue.ToString() +
              ") Role is 'admins'.";

            e.Cancel = true;
        }
        else
        {
            MessageLabel.Text +=
                string.Format("{0}. {1} (Used ID {2}) is DELETED!", e.RowIndex.ToString(), clubID, keyValue.ToString());
        }
    }
}