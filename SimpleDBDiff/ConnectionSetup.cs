using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimpleDBDiff
{
    public partial class ConnectionSetup : Form
    {
        public ConnectionSetup()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if( !string.IsNullOrEmpty(txtServerIP.Text ))
            {
                dbHelper db = new dbHelper();
                bool status = false;
                if( chkUserWndAuth.Checked)
                    status = db.SetupConnection(txtServerIP.Text);
                else
                    status = db.SetupConnection(txtServerIP.Text, txtUsername.Text, txtPassword.Text);
                if (status)
                {
                    Properties.Settings.Default.Server = txtServerIP.Text;
                    Properties.Settings.Default.UseWndAuth = chkUserWndAuth.Checked;
                    if (!chkUserWndAuth.Checked)
                    {
                        Properties.Settings.Default.Username = txtUsername.Text;
                        Properties.Settings.Default.Password = txtPassword.Text;
                    }
                    Properties.Settings.Default.Save();

                    //everything fine close it
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Fail to connect to DB Server");
                }
            }
        }

        private void ConnectionSetup_Load(object sender, EventArgs e)
        {
            txtServerIP.Text = Properties.Settings.Default.Server;
            txtUsername.Text = Properties.Settings.Default.Username;
            txtPassword.Text = Properties.Settings.Default.Password;
            chkUserWndAuth.Checked = Properties.Settings.Default.UseWndAuth;
        }

         
    }
}
