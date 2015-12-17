<%@ Page Language="C#" Debug="true" MasterPageFile="~/SUP_Admin.master" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="_Test" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<p>Click the button to display a confirm box.</p>

<button onclick="myFunction()">Try it</button>

<p id="demo"></p>

<script type="text/javascript">
    function myFunction() {
        var x;
        if (confirm("Press a button!") == true) {
            x = "You pressed OK!";
        } else {
            x = "You pressed Cancel!";
        }
        document.getElementById("demo").innerHTML = x;
    }
</script>

        <asp:Button ID="btnTest" runat="server" Text="Button" onclick="btnTest_Click" />
   
        <asp:ConfirmButtonExtender ID="btnTest_ConfirmButtonExtender" runat="server" 
            ConfirmText="OK to confirm" Enabled="True" TargetControlID="btnTest">
        </asp:ConfirmButtonExtender>
   <asp:Label id="lblAck" runat="server" Text="Label"></asp:Label>
    </div>
    </asp:Content>