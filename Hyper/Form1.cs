using System;
using System.IO;
using System.Media;
using System.Windows.Forms;
using static CBDN_CS_Library.GMailSMTPeMailServiceProvider;

namespace Hyper
{
    public partial class Form1 : Form // <summary>
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string PARAM_NAME;
        public string PARAM_EMAIL;
        public string CONFIG_PARAM_EMAIL_TYPE;
        public string PARAM_PASSWD;

        public string PARAM_TOWHO;
        public string PARAM_REC_EMAIL;
        public string PARAM_SUBJECT;
        public string DATA_MESSAGE;
        public string HTML;

        public int ms = 0;
        public int s = 0;
        public int m = 0;
        public int h = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            Timer timer1 = new Timer();

            timer1.Tick += new EventHandler(Timr);
            timer1.Interval = 900; // in miliseconds, every two weeks
            timer1.Start();

            HTML = "<!Doctype html> \r\n \r\n <html> \r\n <head> \r\n <style>\r\n </style> \r\n <script> \r\n </script> \r\n </head> \r\n \r\n <body> \r\n </body> \r\n <footer> \r\n </footer> \r\n  </html>";

            try
            {
                PARAM_EMAIL = Properties.Settings.Default.email;

                PARAM_NAME = Properties.Settings.Default.name;

                PARAM_PASSWD = Properties.Settings.Default.password;

                string TYPE_SETTING = Properties.Settings.Default.type;

                if (TYPE_SETTING.Equals("HTML"))
                {
                    CONFIG_PARAM_EMAIL_TYPE = "HTML";
                }
                else
                {
                    CONFIG_PARAM_EMAIL_TYPE = "PlainText";
                }

                    textBox3.Text = PARAM_EMAIL;
                    textBox1.Text = PARAM_NAME;
                    textBox2.Text = PARAM_PASSWD;

                using (StreamReader t = new StreamReader("type.txt"))
                {
                    string TYPE = CONFIG_PARAM_EMAIL_TYPE;

                    if (TYPE.Equals("HTML"))
                    {
                        radioButton1.Select();
                        temp1 = "Html";
                    }
                    else
                    {
                        radioButton2.Select();
                        temp1 = "text";
                    }
                }

                if (!temp1.Equals("text"))
                {
                    textBox6.Text = HTML;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Send1();
        }

        private bool ish;
        private string temp1;

        private void Send1()
        {
            InitializeString();
            ReadSettings();

            if (CONFIG_PARAM_EMAIL_TYPE.Equals("HTML"))
            {
                ish = true;
            }
            else
            {
                ish = false;
            }

            string didSend = GetDidSend();

            if (!didSend.Equals("Success"))
            {
                MessageBox.Show("Unknown Error", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SystemSounds.Beep.Play();
                SystemSounds.Exclamation.Play();
                MessageBox.Show("Message sent.");
            }
        }

        private string GetDidSend()
        {
            return Send(
                            from: PARAM_EMAIL,
                            from_name: PARAM_NAME,
                            to_name: PARAM_TOWHO,
                            to: PARAM_REC_EMAIL,
                            password: PARAM_PASSWD,
                            message_subject: PARAM_SUBJECT,
                            message_body: DATA_MESSAGE,
                            isHTML: ish
                      );
        }

        private void InitializeString()
        {
            PARAM_TOWHO = textBox4.Text;
            PARAM_REC_EMAIL = textBox5.Text;
            PARAM_SUBJECT = textBox7.Text;
            DATA_MESSAGE = textBox6.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InitializeSettings();
        }

        private void InitializeSettings()
        {
            Properties.Settings.Default.name = textBox1.Text;
            Properties.Settings.Default.email = textBox3.Text;
            Properties.Settings.Default.password = textBox2.Text;

            if (radioButton1.Checked == true)
            {
                Properties.Settings.Default.type = "HTML";
            }
            else
            {
                Properties.Settings.Default.type = "PlainText";
            }
            Properties.Settings.Default.Save();
        }

        private void ReadSettings()
        {
            PARAM_EMAIL = Properties.Settings.Default.email;

            PARAM_NAME = Properties.Settings.Default.name;

            PARAM_PASSWD = Properties.Settings.Default.password;

            string TYPE_SETTING = Properties.Settings.Default.type;

            if (TYPE_SETTING.Equals("HTML"))
            {
                CONFIG_PARAM_EMAIL_TYPE = "HTML";
            }
            else
            {
                CONFIG_PARAM_EMAIL_TYPE = "PlainText";
            }
        }

        #region Timr

        public void Timr(object sender, EventArgs e)
        {
            s++;
            seconds.Text = s.ToString();

            if (s.Equals(60))
            {
                s = 0;
                m++;
                minutes.Text = m.ToString();
            }
            if (m.Equals(60))
            {
                m = 0;
                h++;
                hours.Text = h.ToString();
            }
        }

        #endregion Timr

        private void button3_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("about:blank");

            if (webBrowser1.Document != null)

            {

                webBrowser1.Document.Write(string.Empty);

            }

            webBrowser1.DocumentText = textBox6.Text;
        }
    }
}