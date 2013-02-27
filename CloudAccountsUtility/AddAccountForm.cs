using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudAccountsUtility
{
    public partial class AddAccountForm : Form
    {
        public AddAccountForm()
        {
            InitializeComponent();

            this.btnOK.Enabled = false;
        }


        public string AccountName
        {
            get { return this.tbName.Text.Trim(); }
        }

        public string ConfigurationFilePath
        {
            get { return this.tbConfigFilePath.Text.Trim(); }
        }

        public string DefaultPlantID
        {
            get { return this.tbDefaultPlantID.Text.Trim(); }
        }


        private void textBox_TextChanged(object sender, EventArgs e)
        {
            this.btnOK.Enabled = (this.tbName.Text.Trim().Length > 0) && (this.tbConfigFilePath.Text.Trim().Length > 0);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
                return;

            this.tbConfigFilePath.Text = this.openFileDialog1.FileName;
        }
    }
}
