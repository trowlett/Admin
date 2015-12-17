<%@ Page Title="List Handicaps" Language="C#" MasterPageFile="~/SUP_Admin.master" AutoEventWireup="true" CodeFile="ListHandicaps.aspx.cs" Inherits="Members_ListHandicaps" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .hid {
            width: 100px;
            text-align: center;
        }
        .hname {
            width: 300px;
            text-align: left;
        }
        .hindex {
            width: 50px;
            text-align: right;
        }
        .hdate {
            width: 100px;
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
        <table style="width: 100%;">
    <tr>
    <td style="width: 70%;">
            <h2>List Handicaps for <%= club %></h2>
        </td>
    <td>
        <h4 style="text-align: right;"> 
            December 10, 2015
        Update
        </h4></td></tr></table>

            <p>
            <asp:Label ID="lblFileName" runat="server" Text=""></asp:Label>
                <br />
            <asp:Label ID="lblInstructions" runat="server" 
                 Text="Click Submit Button to list all <%= club %> handicaps."></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />

        </p>
    <asp:Label ID="lblHandicapCount" runat="server" Text=""></asp:Label>

   	<asp:Panel ID="Panel1" runat="server">
	<div id="members_handicaps">
		<asp:Repeater ID="HandicapListMainRepeater" runat="server">

		<HeaderTemplate>
		<table>
        <thead>
		    <tr>
		        <th class="hid">MSGA Network ID</th>
		        <th class="hname" style="text-align: center;">Name</th>
		        <th class="hindex">Index</th>
		        <th class="hdate">Date</th>
		    </tr>
        </thead>
        <tbody>
        </HeaderTemplate>
		<ItemTemplate>
			<tr style="background-color: silver;">
			<td class="hid"><%# Eval("ID") %> </td>
			<td class="hname"><%# Eval("Name") %> </td>
			<td class="hindex"><%# Eval("Index") %></td>
			<td class="hdate"><%# Eval("Date", "{0:d}") %></td>
			</tr>
		</ItemTemplate>
            <AlternatingItemTemplate>
                <tr style="background-color: ghostwhite;">
			<td class="hid"><%# Eval("ID") %> </td>
			<td class="hname"><%# Eval("Name") %> </td>
			<td class="hindex"><%# Eval("Index") %></td>
			<td class="hdate"><%# Eval("Date", "{0:d}") %></td>
                </tr>
            </AlternatingItemTemplate>
        <FooterTemplate>
                </tbody>
		    </table>
        </FooterTemplate>
		</asp:Repeater>
		</div>
		<br /><br /><br />
	</asp:Panel> 


</asp:Content>

