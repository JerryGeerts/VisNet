using System;
using System.Data.SqlClient;

public partial class RecoverPass : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        bool Code;
        string UserID;
        string activationCode = !string.IsNullOrEmpty(Request.QueryString["ActivationCode"]) ? Request.QueryString["ActivationCode"] : Guid.Empty.ToString();
        string Salt = BCrypt.Net.BCrypt.GenerateSalt();
        string Hash = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text, Salt);

        using (SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=Kennedy;Integrated Security=True"))
        {
            conn.Open();

            using (SqlCommand cmd = new SqlCommand("SELECT count(*) FROM Codes WHERE Type = @Type", conn))
            {
                cmd.Parameters.AddWithValue("Type", "Recovery");

                Code = (int)cmd.ExecuteScalar() > 0;
            }

            if (Code && activationCode != "00000000-0000-0000-0000-000000000000")
            {
                using (SqlCommand cmd = new SqlCommand("SELECT UserID FROM Codes WHERE Code = @ActivationCode", conn))
                {
                    cmd.Parameters.AddWithValue("@ActivationCode", activationCode);

                    UserID = cmd.ExecuteScalar().ToString();
                }

                if (txtPassword.Text == txtConfirmPassword.Text)
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE users SET Password = @Pass, Salt = @Salt  WHERE UserID = @UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("Pass", Hash);
                        cmd.Parameters.AddWithValue("Salt", Salt);
                        cmd.Parameters.AddWithValue("UserID", UserID);

                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Codes WHERE Code = @ActivationCode", conn))
                    {
                        cmd.Parameters.AddWithValue("ActivationCode", activationCode);

                        cmd.ExecuteNonQuery();
                    }
                    Response.Redirect("Login.aspx?value=Recovered");
                }
                else
                {
                    lblFalse.Style["color"] = "Red";
                    lblFalse.Text = "The passwords you entered are not the same!";
                }
            }
            else
            {
                lblFalse.Style["color"] = "Red";
                lblFalse.Text = "Your Activation Code is not valid!";
            }
        }
    }
}