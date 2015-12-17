<%@ Page Title="" Language="C#" MasterPageFile="~/SUP_Admin.master" AutoEventWireup="true" CodeFile="usermain.aspx.cs" Inherits="User_usermain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

    <style type="text/css">
        .cpTitle {
            text-align: right;
            width: 200px;
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

        <h2>Maintain Users having access to MISGA-SignUp</h2>
    <asp:Panel ID="MainPanel1" runat="server">
                <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:MRMISGADBConnect %>' 
                    DeleteCommand="DELETE FROM Users WHERE (UserID = @UserID)" 
                    InsertCommand="INSERT INTO Users(UserID, Password, Name, ClubID, Email, Phone, Role, LoginCount, RegisteredDate, ChangeDate, LastLogin) VALUES (@UserID, @Password, @Name, @ClubID, @Email, @Phone, @Role, @LoginCount, @RegisteredDate, @ChangeDate, @LastLogin)" 
                    SelectCommand="SELECT Users.* FROM Users" 
                    UpdateCommand="UPDATE Users SET Password = @password, Name = @Name, ClubID = @ClubID, Email = @Email, Phone = @Phone, Role = @Role, ChangeDate = @ChangeDate WHERE (UserID = @UserID)">
            <DeleteParameters>
                <asp:Parameter Name="UserID"></asp:Parameter>
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="UserID"></asp:Parameter>
                <asp:Parameter Name="Password"></asp:Parameter>
                <asp:Parameter Name="Name"></asp:Parameter>
                <asp:Parameter Name="ClubID"></asp:Parameter>
                <asp:Parameter Name="Email"></asp:Parameter>
                <asp:Parameter Name="Phone"></asp:Parameter>
                <asp:Parameter Name="Role"></asp:Parameter>
                <asp:Parameter Name="LoginCount"></asp:Parameter>
                <asp:Parameter Name="RegisteredDate"></asp:Parameter>
                <asp:Parameter Name="ChangeDate"></asp:Parameter>
                <asp:Parameter Name="LastLogin"></asp:Parameter>
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="password"></asp:Parameter>
                <asp:Parameter Name="Name"></asp:Parameter>
                <asp:Parameter Name="ClubID"></asp:Parameter>
                <asp:Parameter Name="Email"></asp:Parameter>
                <asp:Parameter Name="Phone"></asp:Parameter>
                <asp:Parameter Name="Role"></asp:Parameter>
                <asp:Parameter Name="ChangeDate"></asp:Parameter>
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:Label ID="MessageLabel" runat="server" Text="Label"></asp:Label>

    <div class="user_div">
        <asp:Panel ID="GridPanel" runat="server" Visible="False">
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="UserID" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="ClubID" HeaderText="Club ID" SortExpression="ClubID">
                    <FooterStyle HorizontalAlign="Left" Width="60px" />
                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="UserID" HeaderText="User ID" ReadOnly="True" SortExpression="UserID">
                    <HeaderStyle HorizontalAlign="Left" Width="100px" Wrap="True" />
                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                    <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" />
                    <asp:BoundField DataField="LoginCount" HeaderText="Log Ins" SortExpression="LoginCount" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <asp:Panel ID="FormPanel" runat="server">
        <asp:FormView ID="FormView1" runat="server" DataSourceID="SqlDataSource1" DataKeyNames="UserID" AllowPaging="True"
                            ForeColor="Black" Width="800px" EditRowStyle-Wrap="False" EmptyDataRowStyle-Wrap="False" 
                InsertRowStyle-Wrap="False" RowStyle-Wrap="False" BackColor="#CCCCCC" 
                BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellSpacing="2" GridLines="Both" OnItemUpdating="FormView1_ItemUpdating" OnItemDeleting="FormView1_ItemDeleting" OnItemInserting="FormView1_ItemInserting" OnModeChanged="FormView1_ModeChanged"
                >
         <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />

            <EditItemTemplate>
                <table>
                    <tr>
                        <td class="cpTitle"> UserID:  &nbsp;</td>
                        <td class="cpBody">
                            <asp:TextBox Text='<%# Eval("UserID") %>' runat="server" width="400px" ID="UserIDLabel1" Enabled="false" /><br /></td>
                    </tr>
                    <tr>
                        <td class="cpTitle">Password:  &nbsp;</td>
                    
                        <td class="cpBody">
                      <asp:TextBox Text='<%# Bind("Password") %>' runat="server" width="400px" ID="PasswordTextBox" Enabled="true" /><br />
                        </td>
                    </tr>
                <tr>
                    <td class="cpTitle">Name:  &nbsp;</td>
                    <td class="cpBody">
                        <asp:TextBox Text='<%# Bind("Name") %>' runat="server" width="400px" ID="NameTextBox" /><br />
                    </td>
                </tr>
                    <tr>
                        <td class="cpTitle">ClubID:  &nbsp;</td>
                        <td class="cpBody">
                            <asp:TextBox Text='<%# Bind("ClubID") %>' runat="server" width="400px" ID="ClubIDTextBox" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">Email:  &nbsp;</td>
                        <td class="cpBody">
                            <asp:TextBox Text='<%# Bind("Email") %>' runat="server" width="400px" ID="EmailTextBox" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">Phone:  &nbsp;</td>
                        <td class="cpBody">
                            <asp:TextBox Text='<%# Bind("Phone") %>' runat="server" width="400px" ID="PhoneTextBox" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">Role:  &nbsp;</td>
                        <td class="cpBody">
                            <asp:TextBox Text='<%# Bind("Role") %>' runat="server" width="400px" ID="RoleTextBox" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">LoginCount:  &nbsp;</td>
                        <td class="cpBody">
                            <asp:TextBox Text='<%# Bind("LoginCount") %>' runat="server" width="400px" ID="LoginCountTextBox" Enabled="False" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">RegisteredDate:  &nbsp;</td>
                        <td class="cpBody">
                            <asp:TextBox Text='<%# Bind("RegisteredDate") %>' runat="server" width="400px" ID="RegisteredDateTextBox" Enabled="False" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">ChangeDate:  &nbsp;</td>
                        <td class="cpBody">
                            <asp:TextBox Text='<%# Bind("ChangeDate") %>' runat="server" width="400px" ID="ChangeDateTextBox" Enabled="False" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">Last Login: &nbsp;</td>
                        <td class="cpBody">
                            <asp:TextBox Text='<%# Bind("LastLogin") %>' runat="server" width="400px" ID="LastLoginTextBox" Enabled="False" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button runat="server" Text="Update" CommandName="Update" ID="UpdateButton" CausesValidation="True" />&nbsp;
                        </td>
                        <td align="center">
                            <asp:Button runat="server" Text="Cancel" CommandName="Cancel" ID="UpdateCancelButton" CausesValidation="False" />
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
                        <td class="cpTitle">UserID: &nbsp;</td>
                        <td class="cpBody">
                            <asp:TextBox Text='<%# Bind("UserID") %>' runat="server" Width="400px" ID="UserIDTextBox" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">Password: &nbsp;</td>
                        <td class="cpBody">
                            <asp:TextBox Text='<%# Bind("Password") %>' runat="server" Width="400px" ID="PasswordTextBox" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">Name: &nbsp;</td>
                        <td class="cpBody">
                            <asp:TextBox Text='<%# Bind("Name") %>' runat="server" Width="400px" ID="NameTextBox" /><br />

                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">Club ID: &nbsp;</td>
                        <td class="cpBody">
                            <asp:TextBox Text='<%# Bind("ClubID") %>' runat="server" Width="400px" ID="ClubIDTextBox" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">Email: &nbsp;</td>
                        <td class="cpBody">
                            <asp:TextBox Text='<%# Bind("Email") %>' runat="server" Width="400px" ID="EmailTextBox" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">Phone: &nbsp;</td>
                        <td class="cpBody">
                            <asp:TextBox Text='<%# Bind("Phone") %>' runat="server" Width="400px" ID="PhoneTextBox" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">Role: &nbsp;</td>
                        <td class="cpBody">
                            <asp:TextBox Text='<%# Bind("Role") %>' runat="server" Width="400px" ID="RoleTextBox" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">Login Count: &nbsp;</td>
                        <td class="cpBody">
                            <asp:TextBox Text='<%# Bind("LoginCount") %>' runat="server" Width="400px" ID="LoginCountTextBox" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">Regisrered Date: &nbsp;</td>
                        <td class="cpBody">
                            <asp:TextBox Text='<%# Bind("RegisteredDate") %>' runat="server" Width="400px" ID="RegisteredDateTextBox" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">Change Date: &nbsp;</td>
                        <td class="cpBody">
                            <asp:TextBox Text='<%# Bind("ChangeDate") %>' runat="server" Width="400px" ID="ChangeDateTextBox" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">Last Login: &nbsp;</td>
                        <td class="cpBody">
                            <asp:TextBox Text='<%# Bind("LastLogin") %>' runat="server" Width="400px" ID="LastLoginTextBox" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                        <asp:Button runat="server" Text="Insert" CommandName="Insert" ID="InsertButton" CausesValidation="True" />&nbsp;
                        </td>
                        <td align="center">
                                <asp:Button runat="server" Text="Cancel" CommandName="Cancel" ID="InsertCancelButton" CausesValidation="False" />
                        </td>
                    </tr>
                    </table>
            </InsertItemTemplate>
            <ItemTemplate>
                <table>
                    <tr>
                        <td class="cpTitle">User ID: &nbsp;</td>
                        <td><asp:Label Text='<%# Eval("UserID") %>' runat="server" Width="400px" ID="UserIDLabel" /><br />
                              </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">Password: &nbsp;</td>
                        <td class="cpBody">
                            <asp:Label Text='<%# Bind("Password") %>' runat="server" Width="400px" ID="PasswordLabel" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">Name: &nbsp;</td>
                        <td class="cpBody">
                            <asp:Label Text='<%# Bind("Name") %>' runat="server" ID="NameLabel" Width="400px" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">Club ID: &nbsp;</td>
                        <td class="cpBody">
                            <asp:Label Text='<%# Bind("ClubID") %>' runat="server" Width="400px" ID="ClubIDLabel" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">Email: &nbsp;</td>
                        <td class="cpBody">
                            <asp:Label Text='<%# Bind("Email") %>' runat="server" Width="400px" ID="EmailLabel" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">Phone: &nbsp;</td>
                        <td class="cpBody">
                            <asp:Label Text='<%# Bind("Phone") %>' runat="server" Width="400px" ID="PhoneLabel" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">Role: &nbsp;</td>
                        <td class="cpBody">
                            <asp:Label Text='<%# Bind("Role") %>' runat="server" Width="400px" ID="RoleLabel" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">Login Count: &nbsp;</td>
                        <td class="cpBody">
                            <asp:Label Text='<%# Bind("LoginCount") %>' runat="server" Width="400px" ID="LoginCountLabel" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">Registered Date: &nbsp;</td>
                        <td class="cpBody">
                            <asp:Label Text='<%# Bind("RegisteredDate") %>' runat="server" Width="400px" ID="RegisteredDateLabel" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">Change Date: &nbsp;</td>
                        <td class="cpBody">
                            <asp:Label Text='<%# Bind("ChangeDate") %>' runat="server" Width="400px" ID="ChangeDateLabel" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cpTitle">Last Login: &nbsp;</td>
                        <td class="cpBody">
                            <asp:Label Text='<%# Bind("LastLogin") %>' runat="server" Width="400px" ID="LastLoginLabel" /><br />
                        </td>
                    </tr>
                </table>
                <hr />
                <table>
                    <tr>
                        <td style="width: 100px;align-content: center;"><asp:Button ID="LinkButton1" runat="server" CausesValidation="True" CommandName="New" Text="New" /></td>
                        <td style="width: 100px;align-content: center;"><asp:Button runat="server" Text="Edit" CommandName="Edit" ID="EditButton" CausesValidation="False" /></td>
                        <td style="width: 100px;align-content: center;"><asp:Button runat="server" Text="Delete" CommandName="Delete" ID="DeleteButton" CausesValidation="False" Enabled="False" /></td>
                        <td style="width: 100px;align-content: center;"><asp:Button ID="CancelButton" runat="server" CausesValidation="True" CommandName="Cancel" Text="Cancel" /></td>
                    </tr>
                </table>
            </ItemTemplate>
            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" Wrap="False" />
                <PagerTemplate>
                  <table>
                    <tr>
                      <td style="width: 100px;"><asp:Button ID="FirstButton" CommandName="Page" CommandArgument="First" Text="<< First" RunAt="server"/></td>
                      <td style="width: 100px;"><asp:Button ID="PrevButton"  CommandName="Page" CommandArgument="Prev"  Text="< Prev"  RunAt="server"/></td>
                        <td style="width: 100px; align-content: center"><%= FormView1.PageIndex + 1 %> of <%= FormView1.PageCount %></td>
                      <td style="width: 100px;"><asp:Button ID="NextButton"  CommandName="Page" CommandArgument="Next"  Text="Next >"  RunAt="server"/></td>
                      <td style="width: 100px;"><asp:Button ID="LastButton"  CommandName="Page" CommandArgument="Last"  Text="Last >>" RunAt="server"/></td>
                    </tr>
                  </table>
                </PagerTemplate>

                <RowStyle BackColor="White" />

        </asp:FormView>
        </asp:Panel>
    </div>
    </asp:Panel>

</asp:Content>

