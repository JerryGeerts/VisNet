<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Calender.aspx.cs" Inherits="Calander" %>

<html>
<head runat="server">
    <link href="CSS/menu.css" rel="stylesheet" />
    <title>VisNet Dashboard</title>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <script src="JS/jquery.easypiechart.min.js"></script>
    <meta charset="utf-8" />
    <link href='CSS/fullcalendar.print.css' rel='stylesheet' media='print' />
    <link href="CSS/fullcalendar.min.css" rel="stylesheet" />
    <script src="JS/moment.min.js"></script>
    <script src="JS/fullcalendar.min.js"></script>

    <script>

        $(function () {
            var $chart = $('.chart');
            $chart.easyPieChart({
                onStep: function (from, to, percent) {
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
        jQuery(function () {
            var $ = jQuery;
            var proc = {<%=procentSyntax%> }
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
    	<script>

    	    $(document).ready(function () {

    	        $('#calendar').fullCalendar({
    	            header: {
    	                left: 'prev',
    	                center: 'title',
    	                right: 'next'
    	            },
                    height:1540,
    	            defaultDate: '2016-09-12',
    	            navLinks: true, // can click day/week names to navigate views
    	            eventLimit: true, // allow "more" link when too many events
			events: [
				{
				    title: 'All Day Event',
				    start: '2016-09-01'
				},
			]
		});
		
	});

</script>
    <style>
        .fc-state-highlight{
    background-color:black;
}
    </style>
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

       <hr class="SecondLine" />

        <div id='calendar'></div>
        <div class="bottomborder"></div>
    </form>
</body>
</html>