using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;

/// <summary>
/// Summary description for Verify
///     to provide methods to valify input elements for adding an event to the SignUp System
/// </summary>
public class Verify
{
    public static bool Date(string date , out DateTime Value)
    {
        // accepts a string to be checked for a valid date in the form of m/d/y or m/d
        //  if year is omitted, assume it is the current year
        //
        // date written = 4/15/17
        //
        DateTime dateValue;
        string dateString = date;
        bool result = true;
        if (DateTime.TryParse(dateString, out dateValue))
        {
            Value = dateValue;
            result = true;         // return true if date is verified as a good date
        }
        else
        {
            Value = dateValue;
            result = false;                      // return false if date is invalid
        }
        return result;
    }

    public static bool HostClubID(string hostID)
    {
        bool result = false;
        string clubdb = ConfigurationManager.ConnectionStrings["ClubsConnect"].ToString();
        MRMISGADB db = new MRMISGADB(clubdb);
        var cdata = db.Clubs.FirstOrDefault(ct => ct.ClubID.Trim() == hostID);
        result = !(cdata == null);
        if (!(cdata == null))
            result = true;
        return result;
    }

    public static bool EventType(string eventType)
    {
        bool result = false;
        string et = eventType.Trim();
        if (et == "Home") result = true;
        if (et == "MISGA") result = true;
        if (et == "Away") result = true;
        if (et == "Club") result = true;
        if (et == "MiSGA") result = true;

        return result;
    }

    public static string Cost(string amount)
    {
        string _cost = "";
        if (amount.Length == 0)
        {
            amount = "tbd";
            _cost = "tbd";
        }
        else
        {
            if (amount.ToUpper() == "TBD")
            {
                _cost = amount;
            }
            else
            {
                _cost = (amount.Substring(0, 1).Equals("$")) ? amount.Trim() : "$" + amount;
            }
        }
        return _cost;
    }

    public static int PlayerLimit(string _pl)
    {
        int pl = 0;
        if (!int.TryParse(_pl, out pl))
        {
            pl = 60;
        }
        return pl;
    }

    public Verify()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}