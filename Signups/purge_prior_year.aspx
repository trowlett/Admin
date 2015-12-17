<%@ Page Title="" Language="C#" MasterPageFile="~/SUP_Admin.master" AutoEventWireup="true" CodeFile="purge_prior_year.aspx.cs" Inherits="Signups_purge_prior_year" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .instructions {
            margin: 5px 20px 5px 20px;
            font-style: italic;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

<script type="text/jscript">
    function cancelClicked() {
        var label = $get('cancelMsg');
        label.innerText = 'You canceled the purge at ' + (new Date()).localeFormat("T") + '.';
        label.style.color = "red";
    }
</script>
    <asp:Panel ID="Panel1" runat="server">
    <table style="width: 100%;">
    <tr>
    <td style="width: 70%;">
        <h2>Purge Entries in the Sign Up Database</h2></td>
    <td>
        <h4 style="text-align: right;"> 
            December 5, 2015
        Update
        </h4>
    </td>
    </tr>
    </table>

    <p class="instructions">Select one of the three options below for purging entries from the Sign Up Database. The Event Date is used for selecting the entries to purge. For example, if you selected option 1, then all the entries with the date of the event before January 1 of the current year will will be purged. 
    </p>
    
    
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
        <table>
            <tr>
                <td style="width: 375px;">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server"     
                        OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" ViewStateMode="Enabled" AutoPostBack="True" Width="375px">
                        <asp:ListItem Value="1">Option 1:&nbsp; Purge Entries Prior to January 1 of </asp:ListItem>
                        <asp:ListItem Value="2">Option 2:&nbsp; Purge Entries Prior to the Specified Date of </asp:ListItem>
                        <asp:ListItem Value="3">Option 3:&nbsp; Purge ALL Entries Prior to Today </asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="width: 500px;">
                        <asp:TextBox ID="tbSelectedDate" runat="server" OnTextChanged="tbSelectedDate_TextChanged" AutoPostBack="True"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revDate" runat="server" ControlToValidate="tbSelectedDate" ErrorMessage="Date must be in the form of MM/DD/YY" ForeColor="Red" ValidationExpression="^(\d{1,2})[/|-](\d{1,2})[/|-](\d{2,4})$">@</asp:RegularExpressionValidator>
                        
                        <asp:Label ID="lblDateError" runat="server" Text="Date Error" ForeColor="Red"></asp:Label>
                        <br />
                    </td>
            </tr>
        </table>
        <br /> 
    

<!--    <h2><asp:Label ID="lblHdr1" runat="server" Text="Purge Prior Year Sign Up Entries"></asp:Label></h2> -->
    <table>
        <tr>
            <td style="width: 150px;text-align: right;"><asp:Button ID="btnDoIt" runat="server" onclick="btnDoIt_Click" Text="Do It" Width="100px" /></td>
            <td style="width: 150px;text-align: right;"><asp:Button ID="btnTryAgain" runat="server" Text="Try Again" OnClick="btnTryAgain_Click" Width="100px" Visible="False" /></td>
        </tr>
    </table>
    
    <p>
            <asp:Label ID="lblResult" runat="server" Text="Label" Visible="False"></asp:Label>
    </p>
                    <asp:ConfirmButtonExtender ID="cbe" runat="server" 
            ConfirmText="Are you sure you want to purge previous Sign Ups entries?" 
            TargetControlID="btnDoIt" onclientcancel="cancelClicked" />
    <p id="cancelMsg"><asp:Label ID="lblErrorMsg" runat="server" Text="Error" 
                    ForeColor="Red" Visible="False"></asp:Label>
             </p>


    </asp:Panel>
</asp:Content>

