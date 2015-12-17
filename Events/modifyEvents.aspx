<%@ Page Title="Modify the Events Database" Language="C#" MasterPageFile="~/SUP_Admin.master" AutoEventWireup="true" CodeFile="modifyEvents.aspx.cs" Inherits="Events_modifyEvents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
            <link href="../Styles/schedule.css" rel="Stylesheet" type="text/css" />
   
      <% DateModified = "January 25, 2015"; %>


            <style type="text/css">
                .auto-style1 {
                    text-align: left;
                }
            </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <h2>Modify the Events Database</h2>
    <table>
        <tr>
            <th><h3>NOTE:&nbsp;&nbsp;<br />&nbsp;</h3></th>
            <th><h3 class="auto-style1">You cannot modify an Event's Date, Tee Time, or Host Club ID.<br />To change those items you must delete the event and re-enter it.</h3></th>
        </tr>
    </table>

    <asp:Panel ID="Panel1" runat="server">
	<div class="mr_schedule">
<!--		<p>Sign up pages are available for now through&nbsp;&nbsp;		
			<span style="font-weight: bold"><%= this.displayDate.ToLongDateString()%></span>
		</p>
        -->

		<asp:Repeater runat="server" ID="ScheduleRepeater">
		<ItemTemplate>
		<table>
			<tr>
				<th class="ctrlcol">&nbsp;</th>
                <th class="ctrlcol">&nbsp;</th>
				<th class="datecol">Date</th>
				<th class="hacol">H/A</th>
                <th class="hostcol">Host Club ID</th>
				<th class="titlecol">Title / Club</th>
				<th class="costcol">Event Fee*</th>
				<th class='timecol'>Tee Time</th>
				<th class="playerlimit">Player Limit</th>
				<th class="deadlinecol">Deadline</th>
                <th class="postdatecol">Posting Date**</th>
                <th class="specialRuleCol">Special Rule</th>
                <th class="guestcol">Guest</th>
			</tr>

			<asp:Repeater ID="Repeater1" runat="server" DataSource='<%# Eval("Events") %>'>
			<ItemTemplate>
				<tr id="Tr1" class='<%# ((SysEvent)(Container.DataItem)).EType.ToLower() %>' runat="server" visible='<%# ((SysEvent)(Container.DataItem)).CanSignUp(((Events_modifyEvents)Page).displayDate) %>'>
                    <td class="ctrlcol"><a href="editevent.aspx?ID=<%# Eval("Id") %>&element=delete" title="Delete this Event">delete</a></td>
					<td class="ctrlcol"><a href="editevent.aspx?ID=<%# Eval("Id") %>&element=edit" title="Edit all the fields in the Event">edit</a></td>
					<td class='datecol'><%# ((SysEvent)Container.DataItem).EDate.ToString(formatDate) %></></td>
					<td class='hacol'><%# ((SysEvent)Container.DataItem).EType %></a></td>
                    <td class='hostcol'><%# ((SysEvent)Container.DataItem).EHostID %></a></td>
					<td class='titlecol'><%# ((SysEvent)Container.DataItem).ETitle %></a></td>
					<td class='costcol'><%# ((SysEvent)Container.DataItem).ECost %></a></td>
					<td class='timecol'><%# ((SysEvent)Container.DataItem).EDate.ToString("h:mm t").ToLower() %></a></td>
					<td class='playerlimit'><%# ((SysEvent)Container.DataItem).EPlayerLimit %></a></td>                   
					<td class='deadlinecol'><%# ((SysEvent)Container.DataItem).EDeadline.ToString(formatDate) %></a></td>
                    <td class="postdatecol"><%# ((SysEvent)Container.DataItem).EPostDate.ToString(formatDate) %></a></td>
                    <td class="specialRuleCol"><%# ((SysEvent)Container.DataItem).ESpecialRule %></td>
                    <td class="guestcol"><%# ((SysEvent)Container.DataItem).EGuest %></td>
				</tr>
			</ItemTemplate>
			</asp:Repeater>
		</table>
		<p>* Payment options are on the SIGNUP PAGE.
		<br />
        ** Posting Date is when SIGNUPS are enabled for this mixer.
		   </p> 
		</ItemTemplate>
		</asp:Repeater>
	</div>  <!-- mr_schedule -->

    </asp:Panel>
    <div class="last_modified">
       <p>
         Page Last Modified:  <%: DateModified %>
       </p>
    </div>

</asp:Content>

