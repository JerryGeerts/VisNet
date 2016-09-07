using System;
using System.Data.SqlClient;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string activationCode = !string.IsNullOrEmpty(Request.QueryString["ActivationCode"]) ? Request.QueryString["ActivationCode"] : Guid.Empty.ToString();
        Session["Check"] = false;

        if ((Session["Remember"] != null) && (Convert.ToBoolean(Session["Remember"]) == true))
        {
            Session["Check"] = true;
            Response.Redirect("Dashboard.aspx");
        }

        if (Request["value"] == "Registered")
        {
            lblFalse.Style["color"] = "LightGreen";
            lblFalse.Text = "Your account has been successfully registered!";
        }
        else if (Request["value"] == "Recovery")
        {
            lblFalse.Style["color"] = "LightGreen";
            lblFalse.Text = "A recovery link has been send to your email!";
        }
        else if (Request["value"] == "Recovered")
        {
            lblFalse.Style["color"] = "LightGreen";
            lblFalse.Text = "Your password has been updated!";
        }

        using (SqlConnection conn = new SqlConnection(Settings.sqlConn))
        {
            conn.Open();
            bool code;

            using (SqlCommand cmd = new SqlCommand("SELECT count(*) FROM Codes WHERE Type = @Type", conn))
            {
                cmd.Parameters.AddWithValue("Type", "EmailActivation");
                code = (int)cmd.ExecuteScalar() > 0;
            }

            if (code && activationCode != "00000000-0000-0000-0000-000000000000")
            {
                string userId;
                using (SqlCommand cmd = new SqlCommand("SELECT UserID FROM Codes WHERE Code = @ActivationCode", conn))
                {
                    cmd.Parameters.AddWithValue("@ActivationCode", activationCode);
                    userId = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("DELETE FROM Codes WHERE Code = @ActivationCode", conn))
                {
                    cmd.Parameters.AddWithValue("ActivationCode", activationCode);
                    cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand("UPDATE users SET EmailConfirmed = @True  WHERE UserID = @UserID", conn))
                {
                    cmd.Parameters.AddWithValue("True", "True");
                    cmd.Parameters.AddWithValue("UserID", userId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 1)
                    {
                        lblFalse.Style["color"] = "LightGreen";
                        lblFalse.Text = "Activation successful.";
                    }
                }
            }
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (txtUsername.Text == "" || txtPassword.Text == "")
        {
            lblFalse.Style["color"] = "Red";
            lblFalse.Text = "Your login Data was not correct!";
        }
        else
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.sqlConn))
                {
                    conn.Open();
                    string registeredHash;
                    string salt;
                    string emailConfirmed;

                    using (SqlCommand cmd = new SqlCommand("SELECT Password FROM users WHERE Username = @Username", conn))
                    {
                        cmd.Parameters.AddWithValue("Username", txtUsername.Text);
                        registeredHash = cmd.ExecuteScalar().ToString();
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT Salt FROM users WHERE Username = @Username", conn))
                    {
                        cmd.Parameters.AddWithValue("Username", txtUsername.Text);
                        salt = cmd.ExecuteScalar().ToString();
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT EmailConfirmed FROM users WHERE Username = @Username", conn))
                    {
                        cmd.Parameters.AddWithValue("Username", txtUsername.Text);
                        emailConfirmed = cmd.ExecuteScalar().ToString();
                    }

                    string hash = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text, salt);

                    if (hash == registeredHash)
                    {
                        if (emailConfirmed == "False")
                        {
                            lblFalse.Style["color"] = "Red";
                            lblFalse.Text = "Please confirm your email before trying to login!";
                        }
                        else
                        {
                            if (rbtLogged.Checked == true)
                            {
                                Session["Remember"] = true;
                            }

                            Session["Check"] = true;
                            Session["Registered"] = false;
                            Session["Username"] = txtUsername.Text;
                            Response.Redirect("Dashboard.aspx");
                        }
                    }
                    else
                    {
                        lblFalse.Style["color"] = "Red";
                        lblFalse.Text = "Your login Data was not correct!";
                    }
                }
            }
            catch
            {
                lblFalse.Style["color"] = "Red";
                lblFalse.Text = "Your login Data was not correct!";
            }
        }
    }
}