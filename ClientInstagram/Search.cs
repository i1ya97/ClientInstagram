using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json.Linq;

namespace ClientInstagram
{
    public partial class Search : UserControl
    {
        public string token;
        public PictureBox[] pxb = new PictureBox[10];
        public Search(string t)
        {
            token = t;
            InitializeComponent();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            string next_url = "https://api.instagram.com/v1/tags/"+textBox1.Text+"/media/recent?access_token=" + token;
            int x = 0, y = 240;
            /*do
            {
                var webClient = new WebClient();
                webClient.QueryString.Add("format", "json");
                var response = webClient.DownloadString(next_url);
                JObject o = JObject.Parse(response);
                if (o["pagination"].SelectToken("next_url") == null)
                {
                    next_url = null;
                }
                else
                {
                    next_url = o["pagination"].SelectToken("next_url").ToString();
                }
                x = 0;
                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        pxb[i] = new PictureBox();
                        pxb[i].Location = new System.Drawing.Point(x, y);
                        pxb[i].Name = o["data"][i].SelectToken("id").ToString();
                        pxb[i].Size = new System.Drawing.Size(237, 237);
                        Controls.Add(pxb[i]);
                        pxb[i].Load(o["data"][i]["images"]["low_resolution"].SelectToken("url").ToString());
                    }
                    catch (System.ArgumentOutOfRangeException) { }
                    pxb[i].SizeMode = PictureBoxSizeMode.StretchImage;
                    pxb[i].MouseUp += pictureBox1_MouseClick;
                    x += 240;
                }
                y += 240;
            } while (next_url != null);*/
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            string id = ((PictureBox)sender).Name;

            var myConrol = new Foto(id, token);
            this.Controls.Clear();
            this.Controls.Add(myConrol);
        }

        private void label11_MouseDown(object sender, MouseEventArgs e)
        {
            var myConrol = new UserForm(token);
            this.Controls.Clear();
            this.Controls.Add(myConrol);
        }
    }
}
