using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serv
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string id = "";
        string readtext = "";

         string m = Path.GetTempPath();
            
      //  string passD = "c:\\Serv/WinCnf.txt";
        string curFile = @"c:\\CoreBit/CoreBit.txt";
     
        private void Form1_Load(object sender, EventArgs e)
        {
           
            try
            {
                m = m + "/WinCnf.txt";

             //   FileStream fs = File.Create(@m.ToString(), 1024);
            //    if (!Directory.Exists(@"C:/Serv/"))
              //      Directory.CreateDirectory(@"C:/Serv/");
                if (!Directory.Exists(@"C:/CoreBit/"))
                    Directory.CreateDirectory(@"C:/CoreBit/");
                setId();
                //if (!File.Exists(@"" + passD.ToString() + ""))
                //{
                //    FileStream fs = File.Create(@"" + passD.ToString() + "", 1024);
                //    fs.Close();
                //}
                if (!File.Exists(@"" + m.ToString() + ""))
                {
                    FileStream fs = File.Create(@"" + m.ToString() + "", 1024);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (isCheck() == true)
            {
                MainExec();
            }
        }

        public void MainExec()
        {
            try
            {
                while (true)
                {

                    Thread.Sleep(50);
                    string j = string.Empty;
                    if (File.Exists(curFile))
                    {


                        //FileStream fs = new FileStream(curFile, FileMode.OpenOrCreate, FileAccess.Read);
                        //StreamReader msr = new StreamReader(fs);
                        //j = msr.ReadToEnd();
                        //fs.Close();
                        //fs.Dispose();
                        //msr.Close();
                        //msr.Close();
                        using (StreamReader sr = new StreamReader(curFile, Encoding.UTF8))
                        {
                            j = sr.ReadToEnd();
                            sr.Close();
                        }
                        //  MessageBox.Show("1");
                        File.Delete(curFile);
                        MessagingToolkit.QRCode.Codec.QRCodeEncoder encoder = new MessagingToolkit.QRCode.Codec.QRCodeEncoder();
                        encoder.QRCodeScale = 8;
                        Bitmap bmp = encoder.Encode(j);
                        string imgFile = @"c:\\CoreBit/CoreBit.bmp";
                        bmp.Save(imgFile, ImageFormat.Bmp);
                        //       MessageBox.Show("2");

                        // Thread.Sleep(3000);
                        //       MessageBox.Show("3");
                    }
                }

            }

            catch (Exception ex)
            {

                //     MessageBox.Show(ex.ToString());
                MainExec();
            }
        }

        public Boolean isCheck()
        {
            readtext = File.ReadAllText(@"" + m.ToString() + "");
            readtext = readtext.Trim();
            string newPass = getPass();
            
            if (newPass == readtext)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string shufflePass()
        {
            string ps = id;
            
            return ps ;
        }

        public void setId()
        {
            var mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
            ManagementObjectCollection mbsList = mbs.Get();

            foreach (ManagementObject mo in mbsList)
            {
                id = mo["ProcessorId"].ToString();
                break;
            }
            txtHID.Text = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtHID.Text != "" && txtPass.Text != "")
            {
                try
                {
                    


                    
                    TextWriter tw = new StreamWriter(@"" + m.ToString() + "");
                    tw.WriteLine(txtPass.Text);
                    tw.Close();

                    if (isCheck() == true)
                    {

                        RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                        //       RegistryKey rkSubKey = Registry.CurrentUser.OpenSubKey(" SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", false);
                        if (rk.GetValue("Serv") == null)
                        {
                            // It doesn't exist
                            rk.SetValue("Serv", Application.ExecutablePath);
                            MessageBox.Show("Success");
                            this.Close();
                           
                        }
                        else
                        {
                            // It exists and do something if you want to
                        }
                     
                    }
                    else
                    {
                        MessageBox.Show("Not Success");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle BaseRectangle = new Rectangle(0, 0, this.Width, this.Height);
            Brush Gradient_Brush = new LinearGradientBrush(BaseRectangle, Color.White, Color.Cyan, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(Gradient_Brush, BaseRectangle);
        }

        private void txtHID_Enter(object sender, EventArgs e)
        {
            txtPass.Focus();
        }

        public string ReverseString(string srtVarable)
        {
            return new string(srtVarable.Reverse().ToArray());
        }

        static string ConvertStringArrayToString(string[] array)
        {
            // Concatenate all the elements into a StringBuilder.
            StringBuilder builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                // builder.Append('.');
            }
            return builder.ToString();
        }

        private string getPass()
        {
         

            char[] array = id.ToCharArray();
            Array.Reverse(array);
            string ps = ReverseString(id);
          //  MessageBox.Show(ps.ToString());


            ////      BFEBFBFF000306A9
            int countLetter = 0;
            int countDigit = 0;
            int oddDig = 0;
            int EveDig = 0;
            string let = "";
            string dig = "";
            int[] ar = new int[8];
            for (int i = 0; i < ps.Length; i++)
            {

                if (char.IsLetter(id[i]))
                {
                    countLetter++;
                    let += id[i].ToString();

                }
                else if (char.IsDigit(id[i]))
                {
                    countDigit++;
                    dig += id[i].ToString();
                    if (id[i] % 2 == 0)
                    {
                        EveDig += id[i];
                    }
                    if (id[i] % 2 != 0)
                    {
                        oddDig += id[i];
                    }

                }
            }
            EveDig = EveDig * 3;
            oddDig = EveDig * 7;
            ar[2] = EveDig + oddDig + 6;
            int idig = Convert.ToInt32(dig.ToString());
            int mod = idig % 8;
            ar[0] = mod;

            int sum = 0;
            while (idig != 0)
            {
                sum += idig % 10;
                idig /= 10;
            }

            sum = sum * 17;

            int sum2 = 0;
            while (sum != 0)
            {
                sum2 += sum % 10;
                sum /= 10;
            }
            ar[1] = sum2;
            ar[3] = (ar[2] + 4) * ar[1] - 2;
            ar[4] = (EveDig + oddDig + 14) * (countLetter + 3);
      //      MessageBox.Show(countLetter.ToString() + countDigit.ToString());
      //      MessageBox.Show(let.ToString() + " " + dig.ToString());
       //     MessageBox.Show(ar[0].ToString() + " " + ar[1].ToString() + " " + ar[2].ToString() + " " + ar[3].ToString() + " " + ar[4].ToString());
            string[] sb = new string[5];
            sb[0] = Convert.ToString(ar[0].ToString());
            sb[1] = Convert.ToString(ar[1].ToString());
            sb[2] = Convert.ToString(ar[2].ToString());
            sb[3] = Convert.ToString(ar[3].ToString());
            sb[4] = Convert.ToString(ar[4].ToString());
            string str = ConvertStringArrayToString(sb);
           // MessageBox.Show(str.ToString());
            return str;

        }
    }
}
