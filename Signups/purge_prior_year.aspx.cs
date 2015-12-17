using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Signups_purge_prior_year : System.Web.UI.Page
{
    public DateTime currDate;
    public DateTime currYearBegin;
    public DateTime selectedDate;
    public DateTime purgeDate;
    public string purgeType;
    public int purgedEntries;
    public int currYear;
    Settings clubSettings;


    protected void Page_Load(object sender, EventArgs e)
    {
        clubSettings = new Settings();
        clubSettings = (Settings)Session["Settings"];

        MrTimeZone etz = new MrTimeZone();
        currDate = etz.eastTimeNow();
        currYearBegin = new DateTime (currDate.Year, 1, 1, 0, 0, 1);
        currYear = currDate.Year;

        if (!IsPostBack)
        {
            string priorYear = string.Format("Option 1: &nbsp;Purge Entries Prior to January 1 of {0}", currYear);
            RadioButtonList1.Items[0].Text = priorYear;
            RadioButtonList1.Items[1].Text = "Option 2:&nbsp; Purge Entries Prior to the Specified Date of ";
            RadioButtonList1.Items[2].Text = "Option 3:&nbsp; Purge ALL Entries Prior to Today ";
            tbSelectedDate.Text = DateTime.Now.ToShortDateString();
            lblDateError.Visible = false;
        }
        if (IsPostBack)
        {
            selectedDate = getDate(tbSelectedDate.Text);
            purgeDate = selectedDate;
        }
        int itemcount = RadioButtonList1.Items.Count;

    }
    protected string purgeEntries(string purgeType, DateTime priorToDate)
    {
        DateTime wkDate = new DateTime(priorToDate.Year,priorToDate.Month, priorToDate.Day, 0, 0, 0);
        string rslt = "No Entries Purged";
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);
        int countOfEntries;
        countOfEntries = 0;

//            wkDate = new DateTime(priorToDate.Year, 1, 1, 0, 0, 0);
            var sup = from pl in db.PlayersList
                      where pl.ClubID.Trim() == clubSettings.ClubID.Trim()
                      orderby pl.EventID
                      select pl;

            
        foreach (PlayersList item in sup)
            {
                int y = Convert.ToInt32(item.EventID.Substring(3, 2)) + 2000;
                int m = Convert.ToInt32(item.EventID.Substring(5, 2));
                int d = Convert.ToInt32(item.EventID.Substring(7, 2));
                DateTime fd = new DateTime(y, m, d, 0, 0, 0);
                if (fd < wkDate)
                {
                    db.PlayersList.DeleteOnSubmit(item);
                    countOfEntries++;
                }
            }
            db.SubmitChanges();
            rslt = string.Format("{0} Sign Up Entries Purged that were prior to date {1}.",countOfEntries,wkDate.ToShortDateString());
            return rslt;
;
    }

    protected void btnDoIt_Click(object sender, EventArgs e)
    {
        purgeDate = new DateTime();
        purgeType = RadioButtonList1.SelectedValue;
        if (purgeType == "1")
        {
            purgeDate = currYearBegin;
            
        }
        if (purgeType == "2")
        {
            selectedDate = getDate(tbSelectedDate.Text);
            purgeDate = selectedDate;
            
        }
        if (purgeType == "3")
        {
            purgeDate = DateTime.Now;
            
        }

//        lblResult.Text = string.Format("{0} Signup Entries Deleted", purgeEntries(purgeType, purgeDate));
        lblResult.Text = purgeEntries(purgeType, purgeDate);
        btnDoIt.Visible = false;
        lblResult.Visible = true;
        lblErrorMsg.Visible = false;
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int x = RadioButtonList1.SelectedIndex;
        purgeType = RadioButtonList1.SelectedValue;
        if (purgeType == "1")
        {
            purgeDate = currYearBegin;
            cbe.ConfirmText = string.Format("Are you sure you want to purge Sign Up entries prior to {0}?", currYear);
        }
        if (purgeType == "2")
        {
            purgeDate = getDate(tbSelectedDate.Text);
            cbe.ConfirmText = string.Format("Are you sure you want to purge Sign Up entries prior to {0}?", purgeDate.ToShortDateString());
        }
        if (purgeType == "3")
        {
            purgeDate = DateTime.Now;
            cbe.ConfirmText = "Are you sure you want to purge all the entries in the Sign Up database?";
        }
    }
    protected DateTime getDate(string date)
    {
        DateTime result = new DateTime(2000,1,1);
        
        if (date != "")
        {
            if (DateTime.TryParse(date, out result))
            {
                return result;
            }
            else
            {
                lblDateError.Text = String.Format("Unable to parse date {0}.", date);
                lblDateError.Visible = true;
                btnDoIt.Visible = false;
                btnTryAgain.Visible = true;
            }

        }
        return result;
    }
    protected void tbSelectedDate_TextChanged(object sender, EventArgs e)
    {
        string date = tbSelectedDate.Text;
        if (DateTime.TryParse(date, out selectedDate))
        {
            selectedDate = Convert.ToDateTime(date);
            purgeDate = Convert.ToDateTime(date);
            cbe.ConfirmText = string.Format("Are you sure you want to purge Sign Up entries prior to {0}?", purgeDate.ToShortDateString());
            RadioButtonList1.AutoPostBack = false;
        }
        else
        {
            lblDateError.Text = String.Format("Invalid date entered {0}.", date);
            lblDateError.Visible = true;
            btnDoIt.Visible = false;
            btnTryAgain.Visible = true;
            RadioButtonList1.AutoPostBack = true;
        }

    }
    protected void btnTryAgain_Click(object sender, EventArgs e)
    {
        Server.Transfer("purge_prior_year.aspx");
    }
}
