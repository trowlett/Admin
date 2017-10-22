<%@ Page Title="" Language="C#" Debug="true" MasterPageFile="~/SUP_Admin.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Maintenance_Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        #welcome {
            font-size: medium;
            color: darkslategrey;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <table style="width: 100%;">
    <tr>
    <td style="width: 70%;">
        <h2>Site Maintenace Main Page</h2></td>
    <td>
        <h4 style="text-align: right;"> 
            June 7, 2016
        Update
        </h4></td></tr></table>
    <div id="welcome">
        <p><strong>Welcome to the MISGA Sign Up Database maintenance.    
           </strong>    
           </p>
        <p>Select from the menu items above, what you would like to do?  
           </p>
        <asp:Panel ID="minUserPanel" runat="server">
        <ul>
            <li>
                <strong>Signup List</strong> -&nbsp; Prepare Players Lists to email. 
            </li>
            <li>
                <strong>About</strong> -&nbsp; Contact information if you have questions, comments, suggestions or problems. 
            </li>
        </ul>

        </asp:Panel>
        <asp:Panel ID="userPanel" runat="server">
        <ul>
            <li>
                <strong>Signup List</strong> -&nbsp; Prepare Players Lists to email. 
            </li>
            <li>
                <strong>Members</strong> -&nbsp; Change the information about a member, and update the handicaps. 
            </li>
            <li>
                <strong>About</strong> -&nbsp; Contact information if you have questions, comments, suggestions or problems. 
            </li>
        </ul>
        </asp:Panel>
        <asp:Panel ID="adminPanel" runat="server" Visible="False">
        <ul>
            <li>
                <strong>Signup List</strong> -&nbsp; Prepare Players Lists to email, or edit the Signup Database. 
            </li>
            <li>
                <strong>Events</strong> -&nbsp; Add, Edit, or Delete an event from ther Events Database, and load the Schedule Text file. 
            </li>
            <li>
                <strong>Members</strong> -&nbsp; Change the information about a member, and update the handicaps. 
            </li>
            <li>
                <strong>Parameters</strong> - &nbsp;Change the parameters for the system. 
            </li>
            <li>
                <strong>About</strong> -&nbsp; Contact information if you have questions, comments, suggestions or problems. 
            </li>
        </ul>
        </asp:Panel>
    </div>
    <div class="gohome">
        <p>
            <asp:Button ID="btnHome" runat="server" Text="Site Url" OnClick="btnHome_Click" />
             
        </p>
    </div>
    <br />
    <br />
    
    <br />

    </asp:Content>

