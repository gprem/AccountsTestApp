using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace CloudAccountsUtility
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            this.btnRemove.Enabled = false;
            this.btnViewConfig.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (AddAccountForm form = new AddAccountForm())
            {
                if (form.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
                    return;

                //TODO: Validate if account name is unique & config file exists
                using (AccountsEntities ctx = new AccountsEntities())
                {                  
                    XmlDocument doc = new XmlDocument();
                    doc.Load(form.ConfigurationFilePath);
                    
                    Account acc = new Account();
                    acc.Name = form.AccountName;
                    acc.ConfigurationInfo = doc.InnerXml;
                    acc.Enabled = true;
                    acc.DefaultPlantID = form.DefaultPlantID;
                    ctx.Accounts.Add(acc);

                    ctx.SaveChanges();
                }
            }

            Form1_Load(this, EventArgs.Empty);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.lvAccounts.SelectedItems.Count == 0)
                return;

            if (MessageBox.Show("Are you sure you want to delete the selected accounts?", "Remove Account", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) 
                != System.Windows.Forms.DialogResult.Yes)
                return;

            using (AccountsEntities ctx = new AccountsEntities())
            {
                foreach (ListViewItem selLvi in this.lvAccounts.SelectedItems)
                {
                    int curID = (int)selLvi.Tag;
                    var selAccount = ctx.Accounts.Where(a => a.Id == curID).FirstOrDefault();

                    ctx.Accounts.Remove(selAccount);
                }

                ctx.SaveChanges();
            }

            Form1_Load(this, EventArgs.Empty);
        }

        private void btnViewConfig_Click(object sender, EventArgs e)
        {

        }

        private void lvAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnRemove.Enabled = (this.lvAccounts.SelectedItems.Count > 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.lvAccounts.Items.Clear();
            
            //Querying with LINQ to Entities 
            using (AccountsEntities ctx = new AccountsEntities())
            {
                foreach (var account in ctx.Accounts)
                {
                    ListViewItem lvi = new ListViewItem(account.Name);
                    lvi.SubItems.Add(account.Enabled.ToString());
                    lvi.Tag = account.Id;
                    this.lvAccounts.Items.Add(lvi);
                }
            }
        }
    }
}
