<%@ Page Title="Edit Event" Language="C#" MasterPageFile="~/SUP_Admin.master" AutoEventWireup="true" CodeFile="editevent.aspx.cs" Inherits="Events_editevent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
            <link href="../Styles/schedule.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
.right-side 
{
    width: 60%;
    float: right;
    border: 1px solid silver;
    padding: 10px 20px 10px 20px;
}
.right-side h3 { text-align: center; font-weight: bold;}

.left-side
{
    width: 25%;
    float: left;
    border: 1px solid silver;
    padding: 10px 20px 10px 20px;
}
.left-side h3 { text-align: center; font-weight: bold;}

.bottom
{
    clear: both;
    padding: 10px 20px 10px 20px;
    text-align: center;
}
</style>
   
      <% DateModified = "March 20, 2017"; %>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>
        <asp:Label ID="lblAction" runat="server" Text="Label"></asp:Label> Event ID : <asp:Label ID="lblEventID" runat="server" Text="Event ID"></asp:Label></h2>
    <h3>Element: <asp:Label ID="lblElement" runat="server" Text="Element"></asp:Label></h3>
    <asp:Panel ID="pnlEdit" runat="server" Visible="False">
    <div class="event_info" style="margin-left: 0px;">
        		<table>
			<tr>
				<th class="edatecol">Date & Time</th>
				<th class="hacol">H/A</th>
                <th class="hostcol">Host Club ID</th>
				<th class="costcol">Event Fee</th>
				<th class='timecol'>Tee Time</th>
				<th class="playerlimit">Player Limit</th>
				<th class="edeadlinecol">Deadline</th>
                <th class="epostdatecol">Posting Date</th>
                <th class="specialRuleCol">Tee Choice Options</th>
                <th class="deadlinecol">Guest</th>
			</tr>
            <tr>
                <td class="edatecol"><asp:TextBox ID="tbEditDate" runat="server" Text="date" Width="120px" ToolTip="Edit Date and Time of Event"></asp:TextBox></td>
                <td class="hacol" runat="server"><asp:TextBox ID="tbEditha" runat="server" Text="h/a" Width="45px" ToolTip="Edit Event Type:  Home; Away; Club; or MISGA"></asp:TextBox></td>
                <td class="hostcol" runat="server"><asp:TextBox ID="tbEditHost" runat="server" Text="host" Width="30px" ToolTip="Edit Host Club ID"></asp:TextBox></td>
                <td class="costcol" runat="server"><asp:TextBox ID="tbEditCost" runat="server" Text="fee" Width="50px" ToolTip="Edit Event Fee"></asp:TextBox></td>
                <td class="timecol" runat="server"><asp:TextBox ID="tbEditTime" runat="server" Text="time" Width="55px" ToolTip="Edit Event Time"></asp:TextBox></td>
                <td class="playerlimit" runat="server"><asp:TextBox ID="tbEditPlayerLimit" runat="server" Text="limit" Width="20px" ToolTip="Edit Player Limt"></asp:TextBox></td>
                <td class="edeadlinecol" runat="server"><asp:TextBox ID="tbEditDeadline" runat="server" Text="deadline" Width="120px" ToolTip="Edit Deadline Date and Time"></asp:TextBox></td>
                <td class="epostdatecol" runat="server"><asp:TextBox ID="tbEditPost" runat="server" Text="post" Width="120px" ToolTip="Edit Posting Date and Time"></asp:TextBox></td>
                <td class="specialRuleCol" runat="server"><asp:TextBox ID="tbEditSR" runat="server" Text="special rule" Width="80px" ToolTip="Edit Tee Choices if any"></asp:TextBox></td>
                <td class="deadlinecol" runat="server"><asp:TextBox ID="tbEditGuest" runat="server" Text="guest" Width="50px" ToolTip="Edit Partner for Two Man or Guest "></asp:TextBox></td>
            </tr>
            </table>
        <br />
        <table>
            <tr>
				<td class="bold">Title / Club:&nbsp;&nbsp;</td>
                <td class="titlecol" runat="server"><asp:TextBox ID="tbEditTitle" runat="server" Text="title" Width="800px" ToolTip="Event Title"></asp:TextBox></td>
            </tr>
        </table>
        <br />
        <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="Red"></asp:Label>
        <table style="margin-left: 100px;">
            <tr>
                <td style="width: 120px;">
            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" Width="80px" OnClick="btnSave_Click" />

                </td>
                <td>
                &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" OnClick="btnCancel_Click" />

                </td>
            </tr>
        </table>

    </div>
    </asp:Panel>
    <asp:Panel ID="pnlDelete" runat="server" Visible="False">
    <div class="event_info" style="margin-left: 0px;">
        		<table>
			<tr>
				<th class="datecol">Date</th>
				<th class="hacol">H/A</th>
                <th class="hostcol">Host Club ID</th>
				<th class="costcol">Event Fee</th>
				<th class='timecol'>Tee Time</th>
				<th class="playerlimit">Player Limit</th>
				<th class="deadlinecol">Deadline</th>
                <th class="postdatecol">Posting Date</th>
                <th class="specialRuleCol">Special Rule</th>
                <th class="deadlinecol">Guest</th>
			</tr>
            <tr>
                <td class="datecol"><asp:TextBox ID="tbDelDate" runat="server" Text="date" Width="120px" BorderStyle="None" Enabled="False"></asp:TextBox></td>
                <td id="Td1" class="hacol" runat="server"><asp:TextBox ID="tbDelha" runat="server" Text="h/a" Width="45px" BorderStyle="None" Enabled="False"></asp:TextBox></td>
                <td id="Td2" class="hostcol" runat="server"><asp:TextBox ID="tbDelHost" runat="server" Text="host" Width="30px" BorderStyle="None" Enabled="False"></asp:TextBox></td>
                <td id="Td3" class="costcol" runat="server"><asp:TextBox ID="tbDelCost" runat="server" Text="cost" Width="35px" BorderStyle="None" Enabled="False"></asp:TextBox></td>
                <td id="Td4" class="timecol" runat="server"><asp:TextBox ID="tbDelTime" runat="server" Text="time" Width="55px" BorderStyle="None" Enabled="False"></asp:TextBox></td>
                <td id="Td5" class="playerlimit" runat="server"><asp:TextBox ID="tbDelPlayerLimit" runat="server" Text="limit" Width="20px" BorderStyle="None" Enabled="False"></asp:TextBox></td>
                <td id="Td6" class="deadlinecol" runat="server"><asp:TextBox ID="tbDelDeadline" runat="server" Text="deadline" Width="120px" BorderStyle="None" Enabled="False"></asp:TextBox></td>
                <td id="Td7" class="postdatecol" runat="server"><asp:TextBox ID="tbDelPost" runat="server" Text="post" Width="120px" BorderStyle="None" Enabled="False"></asp:TextBox></td>
                <td id="Td8" class="specialRuleCol" runat="server"><asp:TextBox ID="tbDelSR" runat="server" Text="special rule" Width="80px" BorderStyle="None" Enabled="False"></asp:TextBox></td>
                <td id="Td9" class="deadlinecol" runat="server"><asp:TextBox ID="tbDelGuest" runat="server" Text="guest" Width="50px" BorderStyle="None" Enabled="False"></asp:TextBox></td>
            </tr>
            </table>
        <br />
        <table>
            <tr>
				<td class="bold">Title / Club:&nbsp;&nbsp;</td>
                <td id="Td10" class="titlecol" runat="server"><asp:TextBox ID="tbDelTitle" runat="server" Text="title" Width="800px" BorderColor="#CCFFFF" BorderStyle="None" Enabled="False"></asp:TextBox></td>
            </tr>
        </table>
        <br />
        <asp:Label ID="lblDelStatus" runat="server" ForeColor="Red"></asp:Label>
        <table style="margin-left: 100px;">
            <tr>
                <td style="width: 120px;">
            &nbsp;<asp:Button ID="btnDelete" runat="server" Text="Delete" Width="80px" OnClick="btnDelete_Click" />

                </td>
                <td>
                &nbsp;<asp:Button ID="btnCancelDelete" runat="server" Text="Cancel" Width="80px" OnClick="btnCancelDelete_Click" />

                </td>
            </tr>
        </table>

    </div>

    </asp:Panel>
    <asp:Panel ID="pnlError" runat="server">
        <asp:Label ID="lblError" runat="server" Text="Error Message" ForeColor="Red"></asp:Label>
        <br />
    <asp:RegularExpressionValidator ID="revCost" runat="server" 
        ErrorMessage="Entry Fee must be numeric or TBD.  If not started with $, one will be inserted." 
        ValidationExpression="^\d*(\.\d{2,})?(tbd)?(TBD)?$" 
        ControlToValidate="tbEditCost" ForeColor="Red"></asp:RegularExpressionValidator>
        <br />
    <asp:RequiredFieldValidator ID="rfvTime1" runat="server" 
        ControlToValidate="tbEditTime" 
              ErrorMessage="Tee Time Required" 
        ForeColor="Red" Visible="False"></asp:RequiredFieldValidator>

    </asp:Panel>
    <asp:Panel ID="pnlBottom" runat="server">
        <div class="last_modified">
            <p>
            Page Last Modified:  <%: DateModified %>
            </p>
        </div>
    </asp:Panel>
</asp:Content>

