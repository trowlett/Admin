<%@ Page Title="" Language="C#" MasterPageFile="~/SUP_Admin.master" AutoEventWireup="true" CodeFile="ClubxMain.aspx.cs" Inherits="Clubs_ClubsMain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .cpTitle {
            text-align: right;
            width: 250px;
        }
        .cpBody {
            width: 800px;
        }
        .cpBody {
            width: 128px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>Maintain Club Settings</h2>
    <asp:Panel ID="Panel1" runat="server">
        <div>
            <asp:Label ID="MessageLabel" runat="server" Text="Label"></asp:Label><br />
            <asp:DropDownList ID="ddlClub" runat="server" style="height: 22px" AutoPostBack="True" OnSelectedIndexChanged="ddlClub_IndexChanged"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT ClubID, OrgName, WebSiteName FROM ClubSettings ORDER BY WebSiteName"></asp:SqlDataSource>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlViewForm" runat="server">
        <div>
            <asp:SqlDataSource ID="SqlClubParameters" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                SelectCommand="SELECT ClubID, Active, OrgName, OrgURL, WebSiteName, Website, WebMaster, WebMasterEmail, Signups, AccessControl, ControlCode, DeadlineSpan, PostSpan, MSGAClubID FROM ClubSettings WHERE (ClubID = @xClub) ORDER BY ClubID" 
                DeleteCommand="DELETE FROM ClubSettings WHERE (ClubID = @ClubID)" 
                InsertCommand="INSERT INTO ClubSettings(ClubID, Active, OrgName, OrgURL, WebSiteName, Website, WebMaster, WebMasterEmail, Signups, AccessControl, ControlCode, DeadlineSpan, PostSpan, MSGAClubID) VALUES (@ClubID, @Active, @OrgName, @OrgURL, @WebSiteName, @Website, @WebMaster, @WebMasterEmail, @Signups, @AccessControl, @ControlCode, @DeadlineSpan, @PostSpan, @MSGAClubID)" 
                UpdateCommand="UPDATE ClubSettings SET ClubID = @ClubID, Active = @Active, OrgName = @OrgName, OrgURL = @OrgURL, WebSiteName = @WebSIteName, Website = @Website, WebMaster = @WebMaster, WebMasterEmail = @WebMasterEmail, Signups = @Signups, AccessControl = @AccessControl, ControlCode = @ControlCode, DeadlineSpan = @DeadlineSpan, PostSpan = @PostSpan, MSGAClubID = @MSGAClubID WHERE (ClubID = @ClubID)">
                <DeleteParameters>
                    <asp:Parameter Name="ClubID" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="ClubID" />
                    <asp:Parameter Name="Active" />
                    <asp:Parameter Name="OrgName" />
                    <asp:Parameter Name="OrgURL" />
                    <asp:Parameter Name="WebSiteName" />
                    <asp:Parameter Name="Website" />
                    <asp:Parameter Name="WebMaster" />
                    <asp:Parameter Name="WebMasterEmail" />
                    <asp:Parameter Name="Signups" />
                    <asp:Parameter Name="AccessControl" />
                    <asp:Parameter Name="ControlCode" />
                    <asp:Parameter Name="DeadlineSpan" />
                    <asp:Parameter Name="PostSpan" />
                    <asp:Parameter Name="MSGAClubID" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter DefaultValue="438" Name="xClub" Type="String" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="ClubID" />
                    <asp:Parameter Name="Active" />
                    <asp:Parameter Name="OrgName" />
                    <asp:Parameter Name="OrgURL" />
                    <asp:Parameter Name="WebSIteName" />
                    <asp:Parameter Name="Website" />
                    <asp:Parameter Name="WebMaster" />
                    <asp:Parameter Name="WebMasterEmail" />
                    <asp:Parameter Name="Signups" />
                    <asp:Parameter Name="AccessControl" />
                    <asp:Parameter Name="ControlCode" />
                    <asp:Parameter Name="DeadlineSpan" />
                    <asp:Parameter Name="PostSpan" />
                    <asp:Parameter Name="MSGAClubID" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:FormView ID="FormView1" runat="server" 
                AllowPaging="True" CellPadding="4" 
                DataKeyNames="ClubID" DataSourceID="SqlClubParameters" 
                ForeColor="Black" Width="800px" EditRowStyle-Wrap="False" EmptyDataRowStyle-Wrap="False" 
                InsertRowStyle-Wrap="False" RowStyle-Wrap="False" BackColor="#CCCCCC" 
                BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellSpacing="2" GridLines="Both" OnItemDeleting="FormView1_ItemDeleting" OnItemInserting="FormView1_ItemInserting" OnItemUpdating="FormView1_ItemUpdating" OnModeChanged="FormView1_ModeChanged" OnPageIndexChanging="FormView1_PageIndexChanging">
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <EditItemTemplate>
                    <table>
                        <tr>
                            <td class="cpTitle"><b>Club ID (3 digits): &nbsp;</b></td>
                            <td class="cpBody">
                                <asp:Label ID="Label1" runat="server" width="400px" text='<%# Eval("ClubID") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Active (YES or NO): &nbsp;</b></td>
                            <td class="cpBody">
                                <asp:TextBox ID="ActiveTextBox" runat="server" width="400px" text='<%# Bind("Active") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Organization Name: &nbsp;</b></td>
                            <td class="cpBody">
                                <asp:TextBox ID="OrgNameTextBox" runat="server" width="400px" text='<%# Bind("OrgName") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Organziation URL: &nbsp;</b></td>
                            <td class="cpBody">
                                <asp:TextBox ID="OrgURLTextBox" runat="server" width="400px" text='<%# Bind("OrgURL") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Website Name: &nbsp;</b></td>
                            <td class="cpBody">
                                <asp:TextBox ID="WebSiteNameTextBox" runat="server" width="400px" text='<%# Bind("WebSiteName") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Website URL: &nbsp;</b></td>
                            <td class="cpBody">
                                <asp:TextBox ID="WebsiteTextBox" runat="server" width="400px" text='<%# Bind("Website") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>WebMaster Name: &nbsp;</b></td>
                            <td class="cpBody">
                                <asp:TextBox ID="WebMasterTextBox" runat="server" width="400px" text='<%# Bind("WebMaster") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>WebMaster Email: &nbsp;</b></td>
                            <td class="cpBody">
                                <asp:TextBox ID="WebMasterEmailTextBox" runat="server" width="400px" text='<%# Bind("WebMasterEmail") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Signups (Enabled or Disabled): &nbsp;</b></td>
                            <td class="cpBody">
                                <asp:TextBox ID="SignupsTextBox" runat="server" width="400px" text='<%# Bind("Signups") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Access Control (on or off): &nbsp;</b></td>
                            <td class="cpBody">
                                <asp:TextBox ID="AccessControlTextBox" runat="server" width="400px" text='<%# Bind("AccessControl") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Access Control Code: &nbsp;</b></td>
                            <td class="cpBody">
                                <asp:TextBox ID="ControlCodeTextBox" runat="server" width="400px" text='<%# Bind("ControlCode") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Deadline Span (default 4): &nbsp;</b></td>
                            <td class="cpBody">
                                <asp:TextBox ID="DeadlineSpanTextBox" runat="server" width="400px" text='<%# Bind("DeadlineSpan") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Post Date Span (default 45): &nbsp;</b></td>
                            <td class="cpBody">
                                <asp:TextBox ID="PostSpanTextBox" runat="server" width="400px" text='<%# Bind("PostSpan") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>MSGA Club ID: &nbsp;</b></td>
                            <td class="cpBody">
                                <asp:TextBox ID="MSGAClubIDTextBox" runat="server" Width="400px" Text='<%# Bind("MSGAClubID") %>' />
                            </td> 
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
                            </td>
                            <td align="center" class="cpBody">&nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </EditItemTemplate>

                <EmptyDataRowStyle Wrap="False" />
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle ForeColor="White" BackColor="Black" />
                <InsertItemTemplate>
                    <table>
                        <tr>
                            <td class="cpTitle"><b>Club ID (3 digits): &nbsp;</b></td>
                            <td><asp:TextBox ID="ClubIDTextBox" runat="server" width="400px" text='<%# Bind("ClubID") %>' /></td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Active (YES or NO): &nbsp;</b></td>
                            <td><asp:TextBox ID="ActiveTextBox" runat="server" width="400px" text='<%# Bind("Active") %>' /></td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Organization Name: &nbsp;</b></td>
                            <td><asp:TextBox ID="OrgNameTextBox" runat="server" width="400px" text='<%# Bind("OrgName") %>' /></td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Organization URL: &nbsp;</b></td>
                            <td><asp:TextBox ID="OrgURLTextBox" runat="server" width="400px" text='<%# Bind("OrgURL") %>' /></td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Website Name: &nbsp;</b></td>
                            <td><asp:TextBox ID="WebSiteNameTextBox" runat="server" width="400px" text='<%# Bind("WebSiteName") %>' /></td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Website URL: &nbsp;</b></td>
                            <td><asp:TextBox ID="WebsiteTextBox" runat="server" width="400px" text='<%# Bind("Website") %>' /></td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>WebMaster Name: &nbsp;</b></td>
                            <td><asp:TextBox ID="WebMasterTextBox" runat="server" width="400px" text='<%# Bind("WebMaster") %>' /></td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>WebMaster Email: &nbsp;</b></td>
                            <td><asp:TextBox ID="WebMasterEmailTextBox" runat="server" width="400px" text='<%# Bind("WebMasterEmail") %>' /></td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Signups (Enabled or Disabled): &nbsp;</b></td>
                            <td><asp:TextBox ID="SignupsTextBox" runat="server" width="400px" text='<%# Bind("Signups") %>' /></td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Access Control (on or off): &nbsp;</b></td>
                            <td><asp:TextBox ID="AccessControlTextBox" runat="server" width="400px" text='<%# Bind("AccessControl") %>' /></td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Access Control Code: &nbsp;</b></td>
                            <td><asp:TextBox ID="ControlCodeTextBox" runat="server" width="400px" text='<%# Bind("ControlCode") %>' /></td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Deadline Span (default 4): &nbsp;</b></td>
                            <td><asp:TextBox ID="DeadlineSpanTextBox" runat="server" width="400px" text='<%# Bind("DeadlineSpan") %>' /></td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Post Date Span (default 45): &nbsp;</b></td>
                            <td><asp:TextBox ID="PostSpanTextBox" runat="server" width="400px" text='<%# Bind("PostSpan") %>' /></td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>MSGA Club ID: &nbsp;</b></td>
                            <td><asp:TextBox ID="MSGAClubIDTextBox" runat="server" Width="400px" Text='<%# Bind("MSGAClubID") %>' />
                            </td> 
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center"><asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Save" /></td>
                            <td align="center"><asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" /></td>
                        </tr>              
                    </table>                    
                </InsertItemTemplate>
                <InsertRowStyle Wrap="False" />
                <ItemTemplate>
                    <table>
                        <tr>
                            <td class="cpTitle"><b>Club ID (3 digits): &nbsp;</b></td>
                            <td><asp:Label ID="Label1" runat="server" width="400px" text='<%# Eval("ClubID") %>' /></td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Active (YES or NO): &nbsp;</b></td>
                            <td><asp:Label ID="ActiveLabel" runat="server" width="400px" text='<%# Bind("Active") %>' /></td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Organization Name: &nbsp;</b></td>
                            <td><asp:Label ID="OrgNameLabel" runat="server" width="400px" text='<%# Bind("OrgName") %>' /></td>                    
                       </tr>
                        <tr>
                            <td class="cpTitle"><b>Organization URL: &nbsp;</b></td>
                            <td><asp:Label ID="OrgURLLabel" runat="server" width="400px" text='<%# Bind("OrgURL", "{0}") %>' /></td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Website Name: &nbsp;</b></td>
                            <td><asp:Label ID="WebSiteNameLabel" runat="server" width="400px" text='<%# Bind("WebSiteName") %>' /></td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Website URL: &nbsp;</b></td>
                            <td><asp:Label ID="WebsiteLabel" runat="server" width="400px" text='<%# Bind("Website") %>' /></td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>WebMaster Name: &nbsp;</b></td>
                            <td><asp:Label ID="WebMasterLabel" runat="server" width="400px" text='<%# Bind("WebMaster") %>' /></td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>WebMaster Email: &nbsp;</b></td>
                            <td><asp:Label ID="WebMasterEmailLabel" runat="server" width="400px" text='<%# Bind("WebMasterEmail") %>' /></td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Signups (Enabled or Disabled): &nbsp;</b></td>
                            <td><asp:Label ID="SignupsLabel" runat="server" width="400px" text='<%# Bind("Signups") %>' /></td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Access Control (on or off): &nbsp;</b></td>
                            <td><asp:Label ID="AccessControlLabel" runat="server" width="400px" text='<%# Bind("AccessControl") %>' /></td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Access Control Code: &nbsp;</b></td>
                            <td><asp:Label ID="ControlCodeLabel" runat="server" width="400px" text='<%# Bind("ControlCode") %>' /></td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Deadline Span (default 4): &nbsp;</b></td>
                            <td><asp:Label ID="DeadlineSpanLabel" runat="server" width="400px" text='<%# Bind("DeadlineSpan") %>' /></td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>Post Date Span (default 45): &nbsp;</b></td>
                            <td><asp:Label ID="PostSpanLabel" runat="server" width="400px" text='<%# Bind("PostSpan") %>' /></td>
                        </tr>
                        <tr>
                            <td class="cpTitle"><b>MSGA Club ID : &nbsp</b></td>
                            <td><asp:Label ID="MSGAClubIDLabel" runat="server" width="400px" text='<%# Bind("MSGAClubID") %>' /></td>
                        </tr>
                        </table>
                    <hr />
                    <table>
                        <tr>
                            <td style="width: 100px;align-content: center;"><asp:LinkButton ID="NewButton" runat="server" CausesValidation="True" CommandName="New" Text="New" /></td>
                            <td style="width: 100px;align-content: center;"><asp:LinkButton ID="EditButton" runat="server" CausesValidation="True" CommandName="Edit" Text="Edit" Enabled="true" /></td>
                            <td style="width: 100px;align-content: center;"><asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="True" CommandName="Delete" Text="Delete" Enabled="<%# this.CanDeleteClubSettings %>" /></td>
                            <td style="width: 100px;align-content: center;"><asp:LinkButton ID="CancelButton" runat="server" CausesValidation="True" CommandName="Cancel" Text="Cancel" /></td>
                        </tr>
                    </table>
                </ItemTemplate>
                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" Wrap="False" />
                <PagerTemplate>
                  <table>
                    <tr>
                      <td style="width: 100px;"><asp:LinkButton ID="FirstButton" CommandName="Page" CommandArgument="First" Text="<< First" RunAt="server"/></td>
                      <td style="width: 100px;"><asp:LinkButton ID="PrevButton"  CommandName="Page" CommandArgument="Prev"  Text="< Prev"  RunAt="server"/></td>
                      <td style="width: 100px;"><asp:LinkButton ID="NextButton"  CommandName="Page" CommandArgument="Next"  Text="Next >"  RunAt="server"/></td>
                      <td style="width: 100px;"><asp:LinkButton ID="LastButton"  CommandName="Page" CommandArgument="Last"  Text="Last >>" RunAt="server"/></td>
                    </tr>
                  </table>
                </PagerTemplate>

                <RowStyle BackColor="White" />
            </asp:FormView>
        </div>
    </asp:Panel>
</asp:Content>

