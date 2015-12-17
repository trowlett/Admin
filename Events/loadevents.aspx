<%@ Page Title="" Language="C#" MasterPageFile="~/SUP_Admin.master" AutoEventWireup="true" CodeFile="loadevents.aspx.cs" Inherits="Events_loadevents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            font-size: medium;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>Load Events Database from the 
        &quot;<asp:Label ID="lblFN1" runat="server" Text="Label"></asp:Label>&quot; file</h2>
    <br />
    <asp:Label ID="lblFileName" runat="server" Text="FileName" style="font-size: medium">
    </asp:Label>
    <br />
<!--    <br />
    <span class="auto-style1">
    <asp:Label ID="lblPath1" runat="server" Text='Path "." = '></asp:Label>
    <asp:Label ID="lblPath1a" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:Label ID="lblPath2" runat="server" Text='Path ".." = '></asp:Label>
    <asp:Label ID="lblPath2a" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:Label ID="lblPath3" runat="server" Text='Path "~" = '></asp:Label>
    <asp:Label ID="lblPath3a" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:Label ID="lblPath4" runat="server" Text='Path "\" = '></asp:Label>
    <asp:Label ID="lblPath4a" runat="server" Text="Not Tested"></asp:Label>
    </span>
    <br />
    <br />
    -->
    <p>        <asp:Button ID="BtnLoadText" runat="server" Text="Load Events from the file" 
            onclick="BtnLoadText_Click" Visible="False" />
    </p>
    <p>       
        <asp:Label ID="lblDbLoadStatus" runat="server" Font-Size="Medium"></asp:Label>
    </p>
 </asp:Content>

