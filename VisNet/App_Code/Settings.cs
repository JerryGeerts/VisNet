using System;
using System.Data.SqlClient;

public class Settings
{
    public static string sqlConn = "Data Source=localhost;Initial Catalog=visnet;Trusted_Connection=True;";

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