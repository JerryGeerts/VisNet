<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Home" %>

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
    <form id="form1" runat="server">
        <div class="Sidebar">
            <ul>
                <li>
                    <a href="Dashboard.aspx">
                        <img id="House" src="Image/HouseIcon.png" />
                    </a>
                </li>
                <li>
                    <a href="Dashboard.aspx">
                        <img id="Logs" src="Image/Logs.png" />
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
                        <a href="<%//TODO: Add a destenation%>">Tasks Management</a>
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

        <div class="Charts">
            <div class="CountryMap">
                <div class="CountryMapName">
                    <ul>
                        <li>COUNTRY MAP</li>
                    </ul>
                </div>
                <div class="CountryMapChart">
                <% //TODO: WORLD MAP %>
                </div>
            </div>

            <div class="CountryChart">
                <div class="CountryChartName">
                    <ul>
                        <li>COUNTRY CHART</li>
                    </ul>
                </div>
                <div class="CountryChartGraph">
                    <% //TODO: Pie Chart for Countrys %>
                </div>
            </div>

            <div class="OSChart">
                <div class="OSChartName">
                    <ul>
                        <li>OPERATING SYSTEM CHART</li>
                    </ul>
                </div>
                <div class="OSChartGraph">
                    <% //TODO: BarGraph with OS's %>
                </div>
            </div>
        </div>

        <div class="CountGrid">
            <div class="CountryCount">
                <div class="CountryCountName">
                    <ul>
                        <li>COUNTRY COUNT</li>
                    </ul>
                </div>
                <div class="CountryCountGraph">
                    <% //TODO: Country With Amounts %>
                </div>
            </div>

            <div class="OSCount">
                <div class="OSCountName">
                    <ul>
                        <li>OPERATING SYSTEM COUNT</li>
                    </ul>
                </div>
                <div class="OSCountGraph">
                    <% //TODO: OS's With Amounts %>
                </div>
            </div>
        </div>

        <div class="OnlineNowGrid">
            <div class="OnlineNow">
                <div class="OnlineNowName">
                    <ul>
                        <li>ONLINE NOW</li>
                    </ul>
                </div>
                <div class="OnlineNowGraph">
                    <% //TODO: GridVieuw with ID/PCNAME/IP/CPU/GPU/INSTALDATE/LASTCON/OS/COUNTRY/REGION/HARDWAREID/VERSIONOFBOT %>
                    <asp:GridView ID="GridView1" runat="server" Height="164px" Width="1118px">
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>