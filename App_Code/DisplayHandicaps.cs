using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for DisplayHandicaps
/// </summary>
public class DisplayHandicaps
{


    public static string[] List()
    {
        var client = new MSGAServiceReference.MemberServiceClient();
        MSGAServiceReference.Credentials mscred = new MSGAServiceReference.Credentials();
        MSGAServiceReference.FetchMembersByClubRequest msreq = new MSGAServiceReference.FetchMembersByClubRequest();
        MSGAServiceReference.FetchMembersResponse msresp = new MSGAServiceReference.FetchMembersResponse();

        mscred.Username = "MisgaService";
        mscred.Password = "M1sga$3rv1c3";
        mscred.Source = "MISGA";

        msreq.Credentials = mscred;
        msreq.SourceClubId = "3849";
        msreq.FromDate = new DateTime(2010, 1, 27);
        msreq.ActiveOnly = true;

        msresp = client.FetchMembersByClub(msreq);

        int numberOfMembers = msresp.MemberList.Count();        // List size minus 1 because CLUB Info at List[0].  Not a member
                                                                //        Response.Write(string.Format("number of members = {0}<br />\n", numberOfMembers));
        string[] hcpList = new string[numberOfMembers];
        for (var i = 1; i < (numberOfMembers); i++)
        {
            string active = (msresp.MemberList[i].IsActive) ? "        " : "Inactive";
            string name = msresp.MemberList[i].LastName.ToUpper() + ", " + msresp.MemberList[i].FirstName.ToUpper() + " [" + msresp.MemberList[i].FriendlyId + "] " + active;
            string msg = string.Format("{2} - {0}; {1} {3}", msresp.MemberList[i].NetworkId, name, i, msresp.MemberList[i].HandicapInfo[0].DisplayHandicap);
            msg = msg + "<br/>\n";
            hcpList[i] = msg;
        }
        return hcpList;
    }
    public DisplayHandicaps()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}