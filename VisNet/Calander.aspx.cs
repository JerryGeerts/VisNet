using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Calander : System.Web.UI.Page
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
        }
    }
}