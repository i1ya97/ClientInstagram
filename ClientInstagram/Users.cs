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
    public partial class Users : UserControl
    {
        public string token;
        public PictureBox[] pxb;
        public string id;
        public Users(string t,string idUser)
        {
            id = idUser;
            token = t;
            InitializeComponent();
        }

        private void Users_Load(object sender, EventArgs e)
        {
            string url = "https://api.instagram.com/v1/users/"+id+"?access_token=" + token;
            using (var webClient = new WebClient())
            {
                webClient.QueryString.Add("format", "json");
                var response = webClient.DownloadString(url);
                JObject o = JObject.Parse(response);
                pictureBox4.Load(o["data"].SelectToken("profile_picture").ToString());
                label10.Text = o["data"].SelectToken("username").ToString();
                label4.Text = o["data"]["counts"].SelectToken("follows").ToString();
                label1.Text = o["data"]["counts"].SelectToken("media").ToString();
                label2.Text = o["data"]["counts"].SelectToken("followed_by").ToString();
                label5.Text = o["data"].SelectToken("full_name").ToString();
                label6.Text = o["data"].SelectToken("website").ToString();
                pxb = new PictureBox[Int32.Parse(o["data"]["counts"].SelectToken("media").ToString())];
            }
            string next_url = "https://api.instagram.com/v1/users/"+id+"/media/recent/?access_token=" + token + "&count=3&MIN_ID=" + 0 + "&MAX_ID=" + 3;
            int x = 0, y = 240;
            do
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
                    catch (System.ArgumentOutOfRangeException) { break; }
                    catch (System.IndexOutOfRangeException) { break; }
                    pxb[i].SizeMode = PictureBoxSizeMode.StretchImage;
                    pxb[i].MouseUp += pictureBox1_MouseClick;
                    x += 240;
                }
                y += 240;
            } while (next_url != null);
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            string id = ((PictureBox)sender).Name;

            var myConrol = new Foto(id, token);
            this.Controls.Clear();
            this.Controls.Add(myConrol);
        }

        private void label11_MouseUp(object sender, MouseEventArgs e)
        {
            var myConrol = new UserForm(token);
            this.Controls.Clear();
            this.Controls.Add(myConrol);
        }
    }
}
