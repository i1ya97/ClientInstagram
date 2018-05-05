using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClientInstagram
{
    public partial class Form2 : Form
    {
        public string token;
        public Form2(string t)
        {
            token = t;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            var myConrol = new UserForm(token);
            panel1.Controls.Add(myConrol);
        }
    }
}
