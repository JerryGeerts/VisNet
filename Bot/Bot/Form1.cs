using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Bot
{
    public partial class Form1 : Form
    {
        int STask;
        int amount;
        string date;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            test();
        }

        private void test()
        {
            int CTask = 2;
            int BotID = 2;
            using (SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=Kennedy;Integrated Security=True"))
            {
                conn.Open();
                do
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT CONVERT(datetime,GETDATE())", conn))
                    {
                        date = cmd.ExecuteScalar().ToString();
                    }

                    using (SqlCommand cmd = new SqlCommand("UPDATE bots set LastConn = @LastConn where BotID = @BotID", conn))
                    {
                        cmd.Parameters.AddWithValue("BotID", BotID);
                        cmd.Parameters.AddWithValue("LastConn", date);
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT CTask from bots where BotID = @BotID;", conn))
                    {
                        cmd.Parameters.AddWithValue("BotID", BotID);
                        STask = Convert.ToInt32(cmd.ExecuteScalar());
                        if (CTask > STask && STask != 0)
                        {
                            using (SqlCommand cmd2 = new SqlCommand("UPDATE bots set Ctask = @Ctask where BotID = @BotID;", conn))
                            {
                                STask++;
                                cmd2.Parameters.AddWithValue("BotID", BotID);
                                cmd2.Parameters.AddWithValue("Ctask", STask);
                                cmd2.ExecuteNonQuery();
                                MessageBox.Show("hello its me" + STask);
                            }
                        }
                        else { }
                    }
                    System.Threading.Thread.Sleep(1000);
                } while (true);
            }
        }
    }
}

