<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>VisNet</title>
    <link href="CSS/menu.css" rel="stylesheet" type="text/css" />
</head>
<body class="Register">
    <form id="form1" runat="server">
        <div class="RegisterMenu">
            <asp:TextBox CssClass="RegisterText" ID="txtUsername" runat="server" Width="300px" Height="35px" Font-Size="Large" Placeholder="Your username"></asp:TextBox>
            <asp:TextBox CssClass="RegisterText" ID="txtPassword" runat="server" Width="300px" Height="35px" Font-Size="Large" TextMode="Password" Placeholder="Your password"></asp:TextBox>
            <asp:TextBox CssClass="RegisterText" ID="txtConfirmPassword" runat="server" Width="300px" Height="35px" Font-Size="Large" TextMode="Password" Placeholder="Confirm Your password"></asp:TextBox>
            <asp:TextBox CssClass="RegisterText" ID="txtEmail" runat="server" Width="300px" Height="35px" Font-Size="Large" Placeholder="Your Email"></asp:TextBox>
            <br />
            <div id="lblFalseID" style="margin-left: auto; margin-right: auto; text-align: center; height: 20px">
                <asp:Label ID="lblFalse" runat="server" Font-Names="Arial" Font-Size="Small" ForeColor="#CC0000"></asp:Label>
            </div>
            <asp:Button CssClass="button" ID="btnLoginn" runat="server" OnClick="btnLogin_Click" Text="Register" Width="325px" Height="50px" Font-Size="Large" />
        </div>
    </form>
</body>
</html>