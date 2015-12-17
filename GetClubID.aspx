<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetClubID.aspx.cs" Inherits="GetClubID" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>MISGA-SignUp Administration and Maintenance</h2>
        </div>
    <div>
        <asp:Label ID="Label1" runat="server" Text="Enter Club ID:"></asp:Label>
        <asp:TextBox ID="tbClubID" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
    </div>
    </form>
</body>
</html>
