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

        <div class="Charts">
            <div class="CountryMap">
                <div class="color2 CountryMapName">
                    <ul>
                        <li>COUNTRY MAP</li>
                    </ul>
                </div>
                <div class="color1 CountryMapChart">
                      <div id="map1" style="width: 795px; height: 440px"></div>
                </div>
            </div>

            <div class="CountryChart">
                <div class="color2 CountryChartName">
                    <ul>
                        <li>COUNTRY CHART</li>
                    </ul>
                </div>
                <div class="color1 CountryChartGraph">
                    <% //TODO: Pie Chart for Countrys %>
                </div>
            </div>

            <div class="OSChart">
                <div class="color2 OSChartName">
                    <ul>
                        <li>OPERATING SYSTEM CHART</li>
                    </ul>
                </div>
                <div class="color1 OSChartGraph">
                    <% //TODO: BarGraph with OS's %>
                </div>
            </div>
        </div>

        <div class="CountGrid">
            <div class="CountryCount">
                <div class="color2 CountryCountName">
                    <ul>
                        <li>COUNTRY COUNT</li>
                    </ul>
                </div>
                <div class="color1 CountryCountGraph">
                    <div class="Grid">
                        <asp:GridView cssClass="color2" ID="grdCountry" runat="server" onrowdatabound="grdCountry_RowDataBound">
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div class="OSCount">
                <div class="color2 OSCountName">
                    <ul>
                        <li>OPERATING SYSTEM COUNT</li>
                    </ul>
                </div>
                <div class="color1 OSCountGraph">
                    <div class="Grid">
                        <asp:GridView cssClass="color2" ID="grdOS" runat="server" onrowdatabound="grdOS_RowDataBound">
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

        <div class="OnlineNowGrid">
            <div class="OnlineNow">
                <div class="color2 OnlineNowName">
                    <ul>
                        <li>ONLINE NOW</li>
                    </ul>
                </div>
                <div class="color1 OnlineNowGraph">
                    <div class="Grid">
                        <asp:GridView ID="grdOnline" runat="server" onrowdatabound="grdOnline_RowDataBound">
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>