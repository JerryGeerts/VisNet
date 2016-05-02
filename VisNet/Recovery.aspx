<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Recovery.aspx.cs" Inherits="Recovery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>VisNet</title>
    <link href="CSS/menu.css" rel="stylesheet" type="text/css"/>
</head>
<body class="Recovery">
<form id="form1" runat="server">
    <div class="RecoveryMenu">
        <asp:TextBox CssClass="RecoveryText" ID="txtEmail" runat="server" Width="300px" Height="35px" Font-Size="Large" Placeholder="Your Email"></asp:TextBox>
        <br/>
        <div id="lblFalseID" style="margin-left: auto; margin-right: auto; text-align: center; height: 20px">
            <asp:Label ID="lblFalse" runat="server" Font-Names="Arial" Font-Size="Small" ForeColor="#CC0000"></asp:Label>
        </div>
        <asp:Button CssClass="button" ID="btnRecover" runat="server" OnClick="btnRecover_Click" Text="Recover" Width="320px" Height="40px" Font-Size="Large"/>
    </div>
</form>
</body>
</html>