using System;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Check"] = false;
        Session["Remember"] = false;
        Response.Redirect("Login.aspx");
    }
}