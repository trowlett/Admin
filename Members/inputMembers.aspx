<%@ Page Title="" Language="C#" MasterPageFile="~/SUP_Admin.master" AutoEventWireup="true" CodeFile="inputMembers.aspx.cs" Inherits="Members_input" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
        <link href="../Styles/memberslist.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        #left {
            width: 50%;
            float: left;
            border-right: 1px solid black;
        }
        #right {
            width: 47%;
            float: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
        <table style="width: 100%;">
    <tr>
    <td style="width: 70%;">
            <h2>Players Update for <%= club %></h2>
        </td>
    <td>
        <h4 style="text-align: right;"> 
            December 30, 2015
        Update
        </h4></td></tr></table>
    <asp:Panel ID="pnlFileName" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Enter Input File Name:  "></asp:Label>
        <asp:TextBox ID="tbInputFileName" runat="server" Text="232-mrmembers.txt"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Load Members" />
    </asp:Panel>
    <div style="clear: both;"></div>
    <div id="right">
        <asp:Panel ID="pnlNewMembers" runat="server" Width="100%" HorizontalAlign="Left">
        <asp:Label ID="Label3" runat="server" Text="New Members"></asp:Label>
            &nbsp;from Input File<br />
            <asp:Repeater ID="NewMemberRepeater" runat="server">
        	   <ItemTemplate>
		        <table>
		        <tr>
		        <th class="pid">Player ID</th>
		        <th class="mname">Name</th>
                <th class="memberID">MSGA ID</th>
                <th class="hcp">Hcp</th>
                <th class="mtitle">Title</th>

		  </tr>
			<asp:Repeater id="MembersListRepeater" runat="server" DataSource='<%# Eval("Members") %>' OnItemCommand="Member_ItemCommand">


			<ItemTemplate>
				<tr class="<%# ((MrMember)Container.DataItem).IsHandicapCurrent() %>">
					<td class="pid"><%# ((MrMember)Container.DataItem).pID %></td>
					<td class="mname"><%# ((MrMember)Container.DataItem).name %></td>
                    <td class="memberID"><%# ((MrMember)Container.DataItem).memberNumber.Trim() %></td>
                    <td class="hcp"><%# ((MrMember)Container.DataItem).hcp %></td>
                    <td class="mtitle"><%# ((MrMember)Container.DataItem).title.Trim() %></td>

				</tr>
			</ItemTemplate>

			</asp:Repeater>


			</table>
			</ItemTemplate>

        </asp:Repeater>
    </asp:Panel>
        </div>    
    <div id="left">
        <asp:Panel ID="pnlOldMembers" runat="server" Width="100%" HorizontalAlign="Left">
        <asp:Label ID="Label2" runat="server" Text="Old Members"></asp:Label>
        &nbsp;from Database<br />
            <asp:Repeater ID="OldMemberRepeater" runat="server">
        	   <ItemTemplate>
		        <table>
		        <tr>
		        <th class="pid">Player ID</th>
		        <th class="mname">Name</th>
                <th class="memberID">MSGA ID</th>
                <th class="hcp">Hcp</th>
                <th class="mtitle">Title</th>

		  </tr>
			<asp:Repeater id="MembersListRepeater" runat="server" DataSource='<%# Eval("Members") %>' OnItemCommand="Member_ItemCommand">


			<ItemTemplate>
				<tr class="<%# ((MrMember)Container.DataItem).IsHandicapCurrent() %>">
					<td class="pid"><%# ((MrMember)Container.DataItem).pID %></td>
					<td class="mname"><%# ((MrMember)Container.DataItem).name %></td>
                    <td class="memberID"><%# ((MrMember)Container.DataItem).memberNumber.Trim() %></td>
                    <td class="hcp"><%# ((MrMember)Container.DataItem).hcp %></td>
                    <td class="mtitle"><%# ((MrMember)Container.DataItem).title.Trim() %></td>

				</tr>
			</ItemTemplate>

			</asp:Repeater>


			</table>
			</ItemTemplate>

            </asp:Repeater>
            <br />

    </asp:Panel>
    
        </div>
    <div style="clear: both;"></div>

</asp:Content>

