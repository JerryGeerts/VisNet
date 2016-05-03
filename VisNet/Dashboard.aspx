﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>VisNet Dashboard</title>
    <link href="CSS/menu.css" rel="stylesheet" />
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
                    <a href="Dashboard.aspx"></a><% //TODO: Place a Weird folder image here%>
                </li>
                <li>
                    <a href="Dashboard.aspx"></a><% //TODO: Place a Calander image here%>
                </li>
                <li>
                    <a href="Dashboard.aspx"></a><% //TODO: Place a Folder image here%>
                </li>
                <li>
                    <a href="Dashboard.aspx"></a><% //TODO: Place a Folder image here%>
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
                    <li></li>
                    <li></li>
                    <li>Connections Today</li>
                    <li><% //TODO: Cricle graph with % amount of bots online today %></li>
                    <li>Connections This Week</li>
                    <li><% //TODO: Cricle graph with % amount of bots online this Week %></li>
                    <li>Connections This Month</li>
                    <li><% //TODO: Cricle graph with % amount of bots online this Month %></li>
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

        <hr class="FirstLine"/>

        <div class="Summary">
            <div class="OnnlineNow">
                <asp:Label CssClass="Number" ID="lblOnnlineNow" runat="server" Text="0"></asp:Label>
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
                <% //TODO: Country With Amounts %>
            </div>

            <div class="OSCount">
                <% //TODO: OS's With Amounts %>
            </div>
        </div>

        <div class="OnnlineNowGrid">
            <div class="OnnlineNow">
                <% //TODO: GridVieuw with ID/PCNAME/IP/CPU/GPU/INSTALDATE/LASTCON/OS/COUNTRY/REGION/HARDWAREID/VERSIONOFBOT %>
            </div>
        </div>
    </form>
</body>
</html>