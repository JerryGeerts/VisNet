<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecoverPass.aspx.cs" Inherits="RecoverPass" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>VisNet</title>
    <link href="CSS/menu.css" rel="stylesheet" type="text/css"/>
</head>
<body class="RecoverPass">
<form id="form1" runat="server">
    <div class="RecoverPassMenu">
        <asp:TextBox CssClass="LoginText" ID="txtPassword" runat="server" Width="300px" Height="35px" Font-Size="Large" TextMode="Password" Placeholder="Enter your new password"></asp:TextBox>
        <asp:TextBox CssClass="LoginText" ID="txtConfirmPassword" runat="server" Width="300px" Height="35px" Font-Size="Large" TextMode="Password" Placeholder="Confirm your new password"></asp:TextBox>
        <div id="lblFalseID" style="margin-left: auto; margin-right: auto; text-align: center; height: 20px">
            <asp:Label ID="lblFalse" runat="server" Font-Names="Arial" Font-Size="Small" ForeColor="#CC0000" CssClass="lblFalse"></asp:Label>
        </div>
        <asp:Button CssClass="button" ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" Width="325px" Height="50px" Font-Size="Large"/>
    </div>
</form>
</body>
</html>