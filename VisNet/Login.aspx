<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>VisNet</title>
    <link href="CSS/menu.css" rel="stylesheet" type="text/css" />
</head>
<body class="Login">
    <form id="form1" runat="server">
        <div class="color1 LoginMenu">
            <asp:TextBox CssClass="LoginText color2" ID="txtUsername" runat="server" Width="300px" Height="35px" Font-Size="Large" Placeholder="Your username"></asp:TextBox>
            <asp:TextBox CssClass="LoginText color2" ID="txtPassword" runat="server" Width="300px" Height="35px" Font-Size="Large" TextMode="Password" Placeholder="Your password"></asp:TextBox>
            <br />
            <asp:RadioButton ID="rbtLogged" runat="server" Font-Size="Small" Text="Keep me logged in" Font-Names="Arial" CssClass="color" />
            <asp:HyperLink ID="hlRegister" runat="server" Font-Size="Small" Font-Names="Arial" NavigateUrl="Register.aspx">Register here</asp:HyperLink>
            <br />
            <div id="lblFalseID" style="margin-left: auto; margin-right: auto; text-align: center; height: 20px">
                <asp:Label ID="lblFalse" runat="server" Font-Names="Arial" Font-Size="Small" ForeColor="#CC0000" CssClass="lblFalse"></asp:Label>
            </div>
            <asp:HyperLink ID="hlRecovery" runat="server" Font-Size="Small" Font-Names="Arial" NavigateUrl="Recovery.aspx">Recover your password</asp:HyperLink>
            <asp:Button CssClass="button color2" ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" Width="325px" Height="50px" Font-Size="Large" />
        </div>
    </form>
</body>
</html>