<%@ Page Title="Add Event" Language="C#" MasterPageFile="~/SUP_Admin.master" AutoEventWireup="true" CodeFile="addevent.aspx.cs" Inherits="Events_addevent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

<% DateModified = "January 26, 2015"; %>

    <style type="text/css">
        .auto-style1 {
            width: 75px;
            font-weight: bold;
        }
        table {
            border-collapse: collapse;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>Add Event to Club&nbsp;
    <asp:Label ID="lblClub" runat="server" Text="XXX"></asp:Label>
        's&nbsp;Schedule
        </h2>
    <asp:Panel ID="panel_event" runat="server">
        <asp:Label ID="lblHdr" runat="server" Text="Event Information" Font-Bold="True" Font-Size="Medium"></asp:Label>
        <div class="event_info">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
            
        <table>
            <tr>
                <td class="auto-style1">Event Date<span style="color: red;">*</span>:</td>
                <td style="width: 130px;">
                    
                    <asp:TextBox ID="tbDate" runat="server" AutoPostBack="False" TabIndex="1"></asp:TextBox>
                </td>
                
                <td style="width: 50px">&nbsp;
                        <asp:RequiredFieldValidator ID="rfvDate1" runat="server" 
                            ErrorMessage="Event Date Required" 
                            ControlToValidate="tbDate" ForeColor="Red">@</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revDate" runat="server" 
                            ControlToValidate="tbDate" 
                            ErrorMessage="Date must be in the form of MM/DD/YY" 
                            ForeColor="Red" 
                            ValidationExpression="^(\d{1,2})[/|-](\d{1,2})[/|-](\d{2,4})$">@</asp:RegularExpressionValidator>

                </td>
                <td class="auto-style1">Time<span style="color: red;">*</span>:</td>
                <td>
                    <asp:TextBox ID="tbTeeTime" runat="server" TabIndex="2"></asp:TextBox>                 
                 </td>
                <td style="width: 50px">
                    &nbsp;
                        <asp:RequiredFieldValidator ID="rfvTime1" runat="server" 
                            ControlToValidate="tbTeeTime" 
                            ErrorMessage="Tee Time Required" 
                            ForeColor="Red" Visible="False">@</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revTime" runat="server" 
                            ErrorMessage="Time must be in the form of HH am or HH pm" 
                            ControlToValidate="tbTeeTime" 
                            ValidationExpression="^(\d{1,2})(:\d{2})?( [a|A|p|P][m|M])$" 
                            ForeColor="Red">@</asp:RegularExpressionValidator>

                    </td>
                <td style="font-weight: bold">Event Fee:</td>
                <td><asp:TextBox ID="tbCost" runat="server" Width="50px" 
                    TabIndex="3"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revCost" runat="server" 
                            ErrorMessage="Entry Fee must be numeric or TBD.  If not started with $, one will be inserted." 
                            ValidationExpression="^\d*(\.\d{2,})?(tbd)?(TBD)?$" 
                            ControlToValidate="tbCost" ForeColor="Red">@</asp:RegularExpressionValidator>

                    </td>
                <td>
                    <asp:CheckBox ID="cbCash" runat="server" Text="Cash Only" TextAlign="Left" TabIndex="4" Font-Bold="True" /></td>
            </tr>
            <tr>
                <td class="auto-style1"></td>
                <td class="center">m/d/y</td>
                <td></td>
                <td></td>
                <td class="center">h[:m] am/pm</td>
                <td></td>
                <td></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; #</td>
                <td></td>
            </tr>
            <tr>
                <td class="auto-style1">Player Limit:</td>
                <td><asp:TextBox ID="tbPlayerLimit" runat="server" TabIndex="5"></asp:TextBox></td>
                <td></td>
                <td class="auto-style1">Deadline Date:</td>
                <td><asp:TextBox ID="tbDeadLine" runat="server" TabIndex="6"></asp:TextBox></td>
                <td></td>
                <td class="auto-style1">Post Date:</td>
                <td><asp:TextBox ID="tbPostDate" runat="server" TabIndex="7"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="auto-style1"></td>
                <td class="center">#</td>
                <td></td>
                <td></td>
                <td class="center">m/d/y</td>
                <td></td>
                <td></td>
                <td class="center">m/d/y</td>
                <td></td>
            </tr>
            <tr>
                
                <td class="auto-style1">Special Rule:</td>
                <td><asp:TextBox ID="tbSpecialRule" runat="server" TabIndex="8"></asp:TextBox></td>
                <td></td>
                <td></td>
                <td class="auto-style1"><asp:CheckBox ID="cbGuest" runat="server" Text="Guest OK" TextAlign="Left" TabIndex="9" /></td>
                             
            </tr>
            <tr>
                <td></td>
                <td class="center">R89, R100, etc.</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            </table>
            <table>
            <tr>

                <td class="auto-style1">Event Type<span style="color: red;">*</span>:</td>
                <td style="width: 130px;">
                    <asp:DropDownList ID="ddlPlace" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPlace_SelectedIndexChanged" TabIndex="10">
                        <asp:ListItem Selected="True" Text="Select a Type" value="select"></asp:ListItem>
                        <asp:ListItem Text="Away" Value="Away"></asp:ListItem>
                        <asp:ListItem Text="Club" Value="Club"></asp:ListItem>
                        <asp:ListItem Text="Home" Value="Home"></asp:ListItem>
                        <asp:ListItem Text="MISGA" Value="MISGA"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;
                    </td>
                    <td>
                    <asp:RequiredFieldValidator ID="rfvType" runat="server" ErrorMessage="Please select a Type"
                         ControlToValidate="ddlPlace" Text="@" ForeColor="Red"
                         InitialValue="Select an Event Type"></asp:RequiredFieldValidator>
                  </td>
                <td></td>
                <td>
                    <asp:Label ID="lbEventType" runat="server" Text="" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblhostClubName" runat="server" Text="" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
        <p><span style="color: red;">*</span> denotes Required Field</p>
        
        </div>
    </asp:Panel>
    <asp:Panel ID="AwayPanel" runat="server" Visible="false">
        <asp:Label ID="Label1" runat="server" Text="Away Event Details" Font-Bold="True" Font-Size="Medium"></asp:Label>

        <div class="event_info">
            <ol>
                <li>Click Uppercase to make Away Title uppercase. </li>
                <li>Find the host club in the list below.</li>
                <li>Then click on Select Host Club to insert club name in Away Title.</li>
                <li>The Green SAVE Button will appear below.</li>
                <li>Click on the Green SAVE Button to store the event in the database.</li>
               </ol>
            <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnAwaySelect" runat="server" OnClick="btnAwaySelect_Click" Text="Select Host Club" />
                        </td>
                        <td></td>
                        <td>
                            <asp:DropDownList ID="ddlAwayHost" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="ClubName" DataValueField="ClubID" Width="250px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                <tr>
                <td class="event_lc"><asp:Label ID="lblAwayTitle" runat="server" Text="Away Title:"></asp:Label></td>
                    <td></td>
                <td><asp:TextBox ID="tbAwayTitle" width="600px" runat="server"></asp:TextBox>
                <asp:CheckBox ID="cbAwayTitleUpper" runat="server" Text="Uppercase?" OnCheckedChanged="cbAwayTitleUpper_CheckedChanged" AutoPostBack="True" /></td>
                </tr>
            </table>
             <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ClubsConnect %>" SelectCommand="SELECT [ClubID], [ClubName] FROM [Clubs] ORDER BY [ClubName]"></asp:SqlDataSource>
       </div>
    </asp:Panel>
    <asp:Panel ID="HomePanel" runat="server" Visible="false">
        <asp:Label ID="Label2" runat="server" Text="Home Event Details" Font-Bold="True" Font-Size="Medium"></asp:Label>

        <div class="event_info">
            <ol>
                <li>Click Uppercase to make Home Title uppercase. </li>
                <li>Find the visiting club in the list below.</li>
                <li>Then click on Add Visiting Club to insert club name in Home Title.</li>
                <li>The Green SAVE Button will appear below.</li>
                <li>Repeat 2 and 3 for each visiting club.</li>
                <li>When done adding clubs, click the Green SAVE Button to store the event in the database.</li>
               </ol>
            <table>
                <tr>
                    <td><asp:Button ID="btnHomeSelect" runat="server" Text="Add Visiting Club" OnClick="btnHomeSelect_Click" /></td>
                    <td></td>
                    <td><asp:DropDownList ID="ddlVisit1" runat="server" DataSourceID="SqlDataSource1" DataTextField="ClubName" DataValueField="ClubID" Width="250px">
                        <asp:ListItem Selected="True">select club</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="event_lc"><asp:Label ID="lblVisitingClubs" runat="server" Text="Home Title:"></asp:Label>&nbsp;&nbsp;</td>
                    <td></td>
                    <td><asp:TextBox ID="tbHomeTitle" runat="server" Width="600px"></asp:TextBox>
                        <asp:CheckBox ID="cbHomeTitleUpper" runat="server" Text="Uppercase?" />
                    </td>
                </tr>
         
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="MISGAPanel" runat="server" Visible="false">
        <asp:Label ID="Label3" runat="server" Text="MISGA Event Details" Font-Bold="True" Font-Size="Medium"></asp:Label>

        <div class="event_info">
            <ol>
                <li>Click Uppercase to make Event Title uppercase. </li>
                <li>Type the MISGA event title in Event Title.</li>
                <li>Find the host club in the list below.</li>
                <li>Then click on Select Host Club to add &quot;@&quot; and the host club name to the Event Title</li>
                <li>The Green SAVE Button will appear below.&nbsp; </li>
                <li>Click on it to store the event in the database.</li>
               </ol>
            <table>
                <tr>
                <td><asp:Button ID="btnMISGASelect" runat="server" Text="Select Host Club" OnClick="btnMISGASelect_Click" /></td>
                    <td></td>
                    <td>
                    <asp:DropDownList ID="ddlMISGAHost" runat="server" DataSourceID="SqlDataSource1" DataTextField="ClubName" DataValueField="ClubID" Width="250px" AutoPostBack="True"></asp:DropDownList>
                        </td>
                </tr>
                <tr>
                <td class="event_lc">Event Title:</td>
                <td></td>
                <td><asp:TextBox ID="tbMISGATitle" runat="server" Width="600px"></asp:TextBox>
                    <asp:CheckBox ID="cbMISGATitleUpper" runat="server" Text="Uppercase?" />
                </td></tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="CLUBPanel" runat="server" Visible="False">

        <asp:Label ID="Label4" runat="server" Text="Internal Club Event Details" Font-Size="Medium" Font-Bold="True"></asp:Label>

        <div class="event_info">
            <ol>
                <li>Click Uppercase to make Event Title uppercase. </li>
                <li>Type the title of the event in Event Title.</li>
                <li>Click Submit Event Title.&nbsp; </li>
                <li>The Green SAVE Button will appear below. </li>
                <li>Click it to store the event in the database.</li>
            </ol>
            <table>
                <tr>
                <td class="event_lc">Event Title:</td>
                    <td></td>
                    <td>
                    <asp:TextBox ID="tbClubTitle" runat="server" Width="600px"></asp:TextBox><asp:CheckBox ID="cbClubTitleUpper" runat="server" Text="Uppercase?" />
                    </td>

                </tr>
                <tr>
                <td><asp:Button ID="btnClubTitleDone" runat="server" Text="Submit Event Title" OnClick="btnClubTitleDone_Click" />
                </td>
                <td></td>
                <td></td>
                </tr>
            </table>

        </div>
    </asp:Panel>
    <br />
    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
    <br />
    
    <br />
    <asp:Table ID="Table1" runat="server">
        <asp:TableRow ID="buttons" runat="server">
            <asp:TableCell ID="sp5" runat="server" Width="200px">&nbsp;</asp:TableCell>
            <asp:TableCell ID="idSave" runat="server" Width="200px">
                <asp:Button ID="btnSave" runat="server" Text="SAVE" OnClick="btnSave_Click" Width="80px" />
            </asp:TableCell>
            <asp:TableCell ID="idCancel" runat="server" Width="200px">
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" Width="80px" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <div class="last_modified">
       <p>
         Page Last Modified:  <%: DateModified %>
       </p>
    </div>
</asp:Content>

