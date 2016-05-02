using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

public partial class Register : System.Web.UI.Page
{
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (txtUsername.Text == "")
        {
            lblFalse.Text = "Enter a Username";
        }
        else if (txtPassword.Text == "")
        {
            lblFalse.Text = "Enter A Valid Password";
        }
        else if (txtConfirmPassword.Text == "")
        {
            lblFalse.Text = "Enter your Password Again To Confirm";
        }
        else if (txtEmail.Text == "")
        {
            lblFalse.Text = "Enter A Valid Email";
        }
        else
        {
            using (SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=Kennedy;Integrated Security=True"))
            {
                conn.Open();
                bool username = false;
                bool email = false;

                using (SqlCommand cmd = new SqlCommand("SELECT count(*) FROM users WHERE Username = @Username ", conn))
                {
                    cmd.Parameters.AddWithValue("Username", txtUsername.Text.Trim());
                    username = (int)cmd.ExecuteScalar() > 0;
                }

                using (SqlCommand cmd = new SqlCommand("SELECT count(*) FROM users WHERE Email = @Email ", conn))
                {
                    cmd.Parameters.AddWithValue("Email", txtEmail.Text);
                    email = (int)cmd.ExecuteScalar() > 0;
                }

                if (username)
                {
                    lblFalse.Text = "That username already exists!";
                }
                else if (email)
                {
                    lblFalse.Text = "That Email already exists!";
                }
                else if (txtPassword.Text == txtConfirmPassword.Text)
                {
                    string salt = BCrypt.Net.BCrypt.GenerateSalt();
                    string hash = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text, salt);
                    string emailActivationCode = SendEmail();
                    string userId;

                    using (SqlCommand cmd = new SqlCommand("INSERT into users values (@Username, @Password, @Salt, @Email, @EmailConfirmed, @Rank, @Rank#)", conn))
                    {
                        cmd.Parameters.AddWithValue("Username", txtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("Password", hash);
                        cmd.Parameters.AddWithValue("Salt", salt);
                        cmd.Parameters.AddWithValue("Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("EmailConfirmed", "False");
                        cmd.Parameters.AddWithValue("Rank", "None");
                        cmd.Parameters.AddWithValue("Rank#", "0");

                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT UserID FROM users WHERE Username = @Username", conn))
                    {
                        cmd.Parameters.AddWithValue("Username", txtUsername.Text);

                        userId = cmd.ExecuteScalar().ToString();
                    }

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Codes VALUES (@UserID, @Username, @Code, @Type)", conn))
                    {
                        cmd.Parameters.AddWithValue("UserID", userId);
                        cmd.Parameters.AddWithValue("Username", txtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("Code", emailActivationCode);
                        cmd.Parameters.AddWithValue("Type", "EmailActivation");

                        cmd.ExecuteNonQuery();
                    }
                    Response.Redirect("Login.aspx?value=Registered");
                }
                else
                {
                    lblFalse.Text = "The passwords you entered were not the same!";
                }
            }
        }
    }

    private string SendEmail()
    {
        string emailActivationCode = Guid.NewGuid().ToString();

        using (MailMessage mm = new MailMessage("visnet_@hotmail.com", txtEmail.Text))
        {
            mm.Subject = "VisNet Account Activation";
            string body = "Hello " + txtUsername.Text.Trim() + ",";
            body += "<br /><br />Please click the following link to activate your account";
            body += "<br /><a href = '" +
                    Request.Url.AbsoluteUri.Replace("Register.aspx", "Login.aspx?ActivationCode=" + emailActivationCode) +
                    "'> Click here to activate your account.</a>";
            body += "<br /><br />Thanks";
            mm.Body = body;
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.live.com");
            smtp.EnableSsl = true;
            NetworkCredential networkCred = new NetworkCredential("visnet_@hotmail.com", "ThisIsATestEmail");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = networkCred;
            smtp.Port = 587;
            smtp.Send(mm);
        }
        return emailActivationCode;
    }
}