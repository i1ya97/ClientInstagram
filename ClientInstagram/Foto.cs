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
    public partial class Foto : UserControl
    {
        public string id;
        public Foto(string idz)
        {
            id = idz;
            InitializeComponent();
        }

        private void Foto_Load(object sender, EventArgs e)
        {
            string url = "https://api.instagram.com/v1/media/" + id + "?access_token=7576783719.88d0cc9.dcdf9f7e511143b78a5e112a0bf594f0";
            using (var webClient = new WebClient())
            {
                webClient.QueryString.Add("format", "json");
                var response = webClient.DownloadString(url);
                JObject o = JObject.Parse(response);
                pictureBox1.Load(o["data"]["images"]["standard_resolution"].SelectToken("url").ToString());
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                name.Text = o["data"]["user"].SelectToken("username").ToString();
                pictureBox3.Load(o["data"]["user"].SelectToken("profile_picture").ToString());
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                label3.Text = o["data"]["likes"].SelectToken("count").ToString();
            }
            url = "https://api.instagram.com/v1/media/" + id + "/comments?access_token=7576783719.88d0cc9.dcdf9f7e511143b78a5e112a0bf594f0";
            using (var webClient = new WebClient())
            {
                webClient.QueryString.Add("format", "json");
                var response = webClient.DownloadString(url);
                JObject o = JObject.Parse(response);

            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            var myConrol = new UserForm();
            this.Controls.Clear();
            this.Controls.Add(myConrol);
        }
    }
}
