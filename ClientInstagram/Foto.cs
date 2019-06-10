using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json.Linq;

namespace ClientInstagram
{
    public partial class Foto : UserControl
    {
        public string id;
        public Label[] labelsr;
        public string token;
        private bool flag = false;
        public Foto(string idz,string t)
        {
            token = t;
            id = idz;
            InitializeComponent();
        }

        private void Foto_Load(object sender, EventArgs e)
        {
            string url = "https://api.instagram.com/v1/media/" + id + "?access_token="+token;
            using (var webClient = new WebClient())
            {
                webClient.QueryString.Add("format", "json");
                var response = webClient.DownloadString(url);
                JObject o = JObject.Parse(response);
                pictureBox1.Load(o["data"]["images"]["standard_resolution"].SelectToken("url").ToString());
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                name.Text = o["data"]["user"].SelectToken("username").ToString();
                pictureBox3.Load(o["data"]["user"].SelectToken("profile_picture").ToString());
                pictureBox3.Name = o["data"]["user"].SelectToken("id").ToString();
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                label3.Text = o["data"]["likes"].SelectToken("count").ToString();
                labelsr = new Label[2 * Int32.Parse(o["data"]["comments"].SelectToken("count").ToString())];
            }
            url = "https://api.instagram.com/v1/media/" + id + "/comments?access_token="+token;
            using (var webClient = new WebClient())
            {
                webClient.QueryString.Add("format", "json");
                var response = webClient.DownloadString(url);
                JObject o = JObject.Parse(response);
                int x = 557, y=130;
                for (int i = 0; i < labelsr.Length; i++)
                {
                    labelsr[i] = new Label();
                    labelsr[i].AutoSize = true;
                    labelsr[i].Location = new System.Drawing.Point(x, y);
                    labelsr[i].Name = "" + i;
                    labelsr[i].Size = new System.Drawing.Size(20, 35);
                    if (i % 2 == 0)
                    {
                        labelsr[i].Text = o["data"][i / 2]["from"].SelectToken("username").ToString() +": ";
                        x += 70;
                    }else
                    {
                        labelsr[i].Text = o["data"][i / 2].SelectToken("text").ToString();
                        y += 30;
                        x = 557;
                    }
                    this.Controls.Add(labelsr[i]);

                }
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            var myConrol = new UserForm(token);
            this.Controls.Clear();
            this.Controls.Add(myConrol);
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            var myConrol = new Users(token,pictureBox3.Name);
            this.Controls.Clear();
            this.Controls.Add(myConrol);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (flag == true)
            {
                int x = Int32.Parse(label3.Text);
                label3.Text = (x - 1).ToString();
                flag = false;
            }
            else
            {
                int x = Int32.Parse(label3.Text);
                label3.Text = (x + 1).ToString();
                flag = true;
            }
        }
    }
}
