using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

public partial class Recovery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnRecover_Click(object sender, EventArgs e)
    {
        if (txtEmail.Text == "")
        {
            lblFalse.Text = "Enter a Email address";
        }
        else
        {
            using (SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=Kennedy;Integrated Security=True"))
            {
                bool email;
                string active;
                string userId;
                string username;
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT count(*) FROM users WHERE Email = @Email ", conn))
                {
                    cmd.Parameters.AddWithValue("Email", txtEmail.Text.Trim());
                    email = (int) cmd.ExecuteScalar() < 1;
                }

                using (SqlCommand cmd = new SqlCommand("SELECT EmailConfirmed FROM users WHERE Email = @Email ", conn))
                {
                    cmd.Parameters.AddWithValue("Email", txtEmail.Text.Trim());
                    active = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT UserID FROM users WHERE Email = @Email ", conn))
                {
                    cmd.Parameters.AddWithValue("Email", txtEmail.Text.Trim());
                    userId = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT Username FROM users WHERE Email = @Email ", conn))
                {
                    cmd.Parameters.AddWithValue("Email", txtEmail.Text.Trim());
                    username = cmd.ExecuteScalar().ToString();
                }

                if (email)
                {
                    lblFalse.Style["color"] = "Red";
                    lblFalse.Text = "That Email address is not in use!";
                }
                else if (active == "False")
                {
                    lblFalse.Style["color"] = "Red";
                    lblFalse.Text = "That email address has not been activated yet!";
                }
                else
                {
                    string emailActivationCode = SendEmail();

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Codes VALUES (@UserID, @Username, @Code, @Type)", conn))
                    {
                        cmd.Parameters.AddWithValue("UserID", userId);
                        cmd.Parameters.AddWithValue("Username", username);
                        cmd.Parameters.AddWithValue("Code", emailActivationCode);
                        cmd.Parameters.AddWithValue("Type", "Recovery");

                        cmd.ExecuteNonQuery();
                    }
                    Response.Redirect("Login.aspx?value=Recovery");
                }
            }
        }
    }

    private string SendEmail()
    {
        string emailActivationCode = Guid.NewGuid().ToString();

        using (MailMessage mm = new MailMessage("visnet_@hotmail.com", txtEmail.Text))
        {
            mm.Subject = "VisNet Account Recovery";
            string body = "Hello " + txtEmail.Text.Trim() + ",";
            body += "<br /><br />Please click the following link to recover your email!";
            body += "<br /><a href = '" +
                    Request.Url.AbsoluteUri.Replace("Recovery.aspx",
                        "RecoverPass.aspx?ActivationCode=" + emailActivationCode) +
                    "'> Click here to recover your account.</a>";
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