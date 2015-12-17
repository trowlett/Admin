<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Account_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Login for MISGA Sign Up Database Administration and Maintenance</title>
         <style type="text/css">
        div.details {
            margin-bottom: 20px;
        }
        div {
            margin-top: 5px;
        }
        label {
            width: 90px;
            display: inline-block;
        }
        button {
            margin: 10px 10px 0 0;
        }
    </style>   

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>MISGA Sign Up Administration<br />Log In</h2>
    <div>

<table>
   <tr>
      <td>User ID:</td>
      <td>
          <asp:TextBox ID="tbUserName" runat="server"></asp:TextBox>
<!--          <input id="txtUserName" type="text" runat="server" />
    -->
      </td>
      <td>
         
          <ASP:RequiredFieldValidator ControlToValidate="tbUserName"
           Display="Static" ErrorMessage="*" runat="server" 
           ID="vUserName" ForeColor="Red" /></td>
   </tr>
   <tr>
      <td>Password:</td>
      <td><asp:TextBox id="tbUserPass" runat="server" TextMode="Password" /></td>
      <td><ASP:RequiredFieldValidator ControlToValidate="tbUserPass"
          Display="Static" ErrorMessage="*" runat="server" 
          ID="vUserPass" ForeColor="Red" />
      </td>
   </tr>
   <tr>
      <td>
          <asp:Label ID="lblPersistenCookie" runat="server" Text="Persistent Cookie:" Visible="False"></asp:Label></td>
      <td><ASP:CheckBox id="chkPersistCookie" runat="server" autopostback="false" Visible="False" /></td>
      <td></td>
   </tr>
</table>
        
        <asp:Button ID="btnLogon" runat="server" Text="Log In" OnClick="btnLogon_Click" />
<!-- <input type="submit" value="Logon" runat="server" id="cmdLogin" /> -->
        <p></p>
<asp:Label id="lblMsg" ForeColor="red"  runat="server" Font-Names="Verdana" Font-Size="Medium" />
    
    </div>
    
    </div>
        <p style="text-align: left;font-size: small;">Revision Date:  <span style="font-weight: bold;">
            December 10, 2015
        </span></p>
    </form>
</body>
</html>
