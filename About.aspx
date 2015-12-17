<%@ Page Title="About Us" Language="C#" MasterPageFile="~/SUP_Admin.master" AutoEventWireup="true"
    CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        About 
        <asp:Label ID="lblClubName" runat="server" Text="Label Club Name"></asp:Label>
        &nbsp;Administration Web Site
    </h2>
    <p>
        Web site is used to maintain the database for the 
        <asp:Literal ID="LiteralOrg" runat="server" Text="Club Organization"></asp:Literal>&nbsp;Web Site 
        <asp:Button ID="btnHome" runat="server" Text="Go To:" OnClick="btnHome_Click" />
        </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        Questions or Comments?&nbsp; Contact:&nbsp; Tom Rowlett at 
            <a href="mailto:tom.rowlett@misga-signup.org">tom.rowlett@misga-signup.org</a> or (301) 473-4785.</p>
    <%# System.Configuration.ConfigurationManager.AppSettings["OrgURL"].ToString() %>
</asp:Content>
