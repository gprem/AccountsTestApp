﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountsTestApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (LoginForm form = new LoginForm())
            {
                form.ShowDialog(this);
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            foreach (var key in System.Configuration.ConfigurationManager.AppSettings.AllKeys)
            {
                this.tbLog.Text += string.Concat(System.Configuration.ConfigurationManager.AppSettings[key], Environment.NewLine);
            }
        }
    }
}
