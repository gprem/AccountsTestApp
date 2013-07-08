using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace AccountsTestApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string configInfo = DataManager.GetConfigInfo(this.comboBox1.SelectedItem.ToString());
            if (configInfo.Length == 0)
            {
                MessageBox.Show("Could not locate the configuration info for the selected account.", "AccountsTestApp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string configFilePath = Path.GetTempFileName();
            File.WriteAllText(configFilePath, configInfo);
            
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.File = configFilePath;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            //TODO: Perform authentication & other usual work

            this.Close();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            List<string> accountList = DataManager.GetAccounts();

            this.comboBox1.Items.AddRange(accountList.ToArray());

            if (comboBox1.Items.Count > 0)
                this.comboBox1.SelectedIndex = 0;
        }
    }
}
