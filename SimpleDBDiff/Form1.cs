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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnA_Click(object sender, EventArgs e)
        {
            ConnectionSetup dlg = new ConnectionSetup();
            if (dlg.ShowDialog() == DialogResult.OK)
                RefreshDBList();
        }

        private dbHelper db;
        private bool InitDB()
        {
            db = new dbHelper();
            if (Properties.Settings.Default.UseWndAuth)
                return db.SetupConnection(Properties.Settings.Default.Server);
            
            return db.SetupConnection(Properties.Settings.Default.Server,
                Properties.Settings.Default.Username, Properties.Settings.Default.Password);
        }
        private void RefreshDBList()
        {
            if (InitDB())
            {
                string[] dblist = db.GetDatabases();
                comboBoxA.Items.AddRange(dblist);
                comboBoxB.Items.AddRange(dblist);
            }
        }

        void SelectDBCombo(ComboBox cb , string db)
        {
            if (!string.IsNullOrEmpty(db))
                cb.SelectedIndex = cb.FindStringExact(db);
            if (cb.SelectedIndex == -1)
                cb.SelectedIndex = 0;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshDBList();

            SelectDBCombo(comboBoxA, Properties.Settings.Default.DBA);
            SelectDBCombo(comboBoxB, Properties.Settings.Default.DBB);
         }

        private void btnDIFF_Click(object sender, EventArgs e)
        {
            if( comboBoxA.SelectedIndex == -1 || 
                comboBoxB.SelectedIndex == -1 )
            {
                MessageBox.Show("Select both db first");
                return;
            }

            string dbA, dbB;
            dbA = comboBoxA.SelectedItem.ToString();
            dbB = comboBoxB.SelectedItem.ToString();

            Properties.Settings.Default.DBA = dbA;
            Properties.Settings.Default.DBB = dbB;
            Properties.Settings.Default.Save();

            DoDBCompare(dbA, dbB);


        }

        private void DoDBCompare(string dbA, string dbB)
        {
            string[] TablesA, TablesB;
            TablesA = db.GetTables(dbA);
            TablesB = db.GetTables(dbB);
            bool first = true;
            foreach (string tbA in TablesA)
            {
                if (TablesB.Contains(tbA))
                {
                    DataTable dt = db.CompareTables(dbA, dbB, tbA);
                    if (dt.Rows.Count > 0)
                    {
                        dt.TableName = tbA;
                        AddNode(tbA, dt.Rows.Count, dt);
                        if (first)
                        {
                            dataGridView1.DataSource = dt;
                            first = false;
                        }
                        //break;
                    }
                }
            }
           
        }

        private void AddNode(string name, int count, DataTable dataTable)
        {
            tvTables.Nodes.Add(name, name + "[" + count.ToString() + "]").Tag = dataTable;

        }

        private void tvTables_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if( tvTables.SelectedNode!=null)
            dataGridView1.DataSource = tvTables.SelectedNode.Tag;
        }
    }
}
