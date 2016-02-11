<%@ Page Title="" Language="C#" MasterPageFile="~/SUP_Admin.master" AutoEventWireup="true" CodeFile="ChangeClub.aspx.cs" Inherits="Clubs_ChangeClub" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
        <table style="width: 100%;">
    <tr>
    <td style="width: 70%;">
            <h2>Change Club</h2>
        </td>
    <td>
        <h4 style="text-align: right;"> 
            January 20, 2016
        Update
        </h4></td></tr>
            <tr>
                <td><h3>Current Club = <span class="bold"><%= club %></span></h3></td>
                <td></td>
            </tr>
        </table>
    <asp:Label ID="Label1" runat="server" Text="Select the Club to change to:  "></asp:Label>
     <asp:DropDownList ID="ddlClub" runat="server" style="height: 22px" AutoPostBack="True" OnSelectedIndexChanged="ddlClub_IndexChanged"></asp:DropDownList>
     <br />
    <asp:Label ID="MessageLabel" runat="server" Text=""></asp:Label><br />

</asp:Content>

