using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            if (cb.SelectedIndex == -1 && cb.Items.Count > 0)
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
            tvTables.Nodes.Clear();
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

            //DoDBCompareExt(dbA, dbB);
            DoDBCompare(dbA, dbB);


        }

        private void DoDBCompareExt(string dbA, string dbB)
        {
            string[] TablesA, TablesB;
            TablesA = db.GetTables(dbA);
            TablesB = db.GetTables(dbB);
            bool first = true;
            foreach (string tbA in TablesA)
            {
                if (tbA.Contains("$")) continue;
                if (TablesB.Contains(tbA))
                {
                    db.CompareTablesExt(dbA, dbB, tbA);
                    //DataTable dt = db.CompareTables(dbA, dbB, tbA);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    dt.TableName = tbA;
                    //    AddNode(tbA, dt.Rows.Count, dt);
                    //    if (first)
                    //    {
                    //        dataGridView1.DataSource = dt;
                    //        first = false;
                    //    }
                    //    //break;
                    //}
                }
            }

        }

        private void DoDBCompare(string dbA, string dbB)
        {
            string[] TablesA, TablesB;
            TablesA = db.GetTables(dbA);
            TablesB = db.GetTables(dbB);
            bool first = true;
            foreach (string tbA in TablesA)
            {
                //if (tbA.Contains("$")) continue;
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

        private TreeNode AddNode(string name, int count, DataTable dataTable)
        {
            TreeNode tn = tvTables.Nodes.Add(name, name + "[" + count.ToString() + "]");
            tn.Tag = dataTable;
            return tn;
        }

        private void tvTables_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if( tvTables.SelectedNode==null) return;

            radioButtonAB.Checked = true;
            dataGridView1.DataSource = tvTables.SelectedNode.Tag;
            DoCMPHighlight();

            //this part for external diff useless slow
            //if( tvTables.SelectedNode.Level == 1)
            //{
            //    if( tvTables.SelectedNode.Text.StartsWith("Diff") )
            //    {
            //        DataTable tbl = db.GetItems(((TableDiffResult)tvTables.SelectedNode.Parent.Tag).Diff.ToArray(),
            //            ((TableDiffResult)tvTables.SelectedNode.Parent.Tag).Key, tvTables.SelectedNode.Parent.Name);
            //        dataGridView1.DataSource = tbl;
            //    }
            //}

        }

        private void AutoResizeColumns()
        {
            dataGridView1.AutoResizeColumns();
            foreach (DataGridViewColumn dataGridViewColumn in dataGridView1.Columns)
            {
                if (dataGridViewColumn.Width > 300)
                    dataGridViewColumn.Width = 300;
            }
        }

        private void DoCMPHighlight()
        {
            dataGridView1.SuspendLayout();
            
            for (int i = 0; i < dataGridView1.Rows.Count-1; i ++)
            {
                if(  (string)dataGridView1.Rows[i].Cells[0].Value  == "A")
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = CA;
                if ((string)dataGridView1.Rows[i].Cells[0].Value == "B")
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = CB;
                if ((((string)dataGridView1.Rows[i].Cells[0].Value  == "A" &&
                    (string)dataGridView1.Rows[i+1].Cells[0].Value  == "B") ||
                    ((string)dataGridView1.Rows[i].Cells[0].Value == "B" &&
                    (string)dataGridView1.Rows[i + 1].Cells[0].Value == "A")) &&
                    dataGridView1.Rows[i].Cells[1].Value.ToString() ==
                    dataGridView1.Rows[i + 1].Cells[1].Value.ToString())
                {
                    for (int j = 2; j < dataGridView1.ColumnCount; j++ )
                    {
                        if( (dataGridView1.Rows[i].Cells[j].Value!=null && 
                            dataGridView1.Rows[i+1].Cells[j].Value!=null &&
                            dataGridView1.Rows[i].Cells[j].Value.ToString() !=
                            dataGridView1.Rows[i+1].Cells[j].Value.ToString() ) ||
                           (dataGridView1.Rows[i].Cells[j].Value != null &&
                            dataGridView1.Rows[i+1].Cells[j].Value == null)||
                            (dataGridView1.Rows[i].Cells[j].Value == null &&
                            dataGridView1.Rows[i+1].Cells[j].Value != null))
                        {
                            dataGridView1.Rows[i].Cells[j].Style.BackColor = CD;
                            dataGridView1.Rows[i+1].Cells[j].Style.BackColor = CD;
                        }
                    }
                     
                    i++;
                }
            }
            AutoResizeColumns();
            dataGridView1.ResumeLayout();
        }

        Color CA = Color.LightGray;
        Color CB = Color.PaleTurquoise;
        Color CD = Color.PaleVioletRed;
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.CellStyle.BackColor == CD) return;
            if( (string)dataGridView1.Rows[e.RowIndex].Cells[0].Value == "A" )
                e.CellStyle.BackColor = CA;
            else if( (string)dataGridView1.Rows[e.RowIndex].Cells[0].Value == "B" )
                e.CellStyle.BackColor = CB;
        }

        private void comboBoxB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxA.SelectedIndex == -1 ||
                comboBoxB.SelectedIndex == -1)
            {
                
                return;
            }

            string dbA, dbB;
            dbA = comboBoxA.SelectedItem.ToString();
            dbB = comboBoxB.SelectedItem.ToString();

            Properties.Settings.Default.DBA = dbA;
            Properties.Settings.Default.DBB = dbB;
            Properties.Settings.Default.Save();

            string[] tablesA = db.GetTables(dbA);
            string[] tablesB = db.GetTables(dbB);
            List<string> ret = new List<string>();
            for (int i = 0; i < tablesA.Length; i++)
            {
                if( tablesB.Contains( tablesA[i] ))
                    ret.Add(tablesA[i]);
            }
            comboBoxTbl.Sorted = true;
            comboBoxTbl.Items.AddRange(ret.ToArray());

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBoxA.SelectedIndex == -1 ||
                comboBoxB.SelectedIndex == -1)
            {
                MessageBox.Show("Select both db first");
                return;
            }
            if (comboBoxTbl.SelectedIndex == -1  )
            {
                MessageBox.Show("Select the table first");
                return;
            }

             string dbA, dbB;
            dbA = comboBoxA.SelectedItem.ToString();
            dbB = comboBoxB.SelectedItem.ToString();

            Properties.Settings.Default.DBA = dbA;
            Properties.Settings.Default.DBB = dbB;
            Properties.Settings.Default.Save();
            string tblname = comboBoxTbl.SelectedItem.ToString();
            //TableDiffResult tbl = db.CompareTablesExt(dbA, dbB, tblname);
            //AddDiffNodes(tblname, tbl);


            DataTable dt = db.CompareTables(dbA, dbB, tblname);
            CheckNRemoveTNode(tblname);
            if (dt.Rows.Count > 0)
            {
                dt.TableName = tblname;
                
                tvTables.SelectedNode= AddNode(tblname, dt.Rows.Count, dt);
                
                dataGridView1.DataSource = dt;
            }
            else
            {
                SetStatusText("Done", "Table " + tblname + ": No different", true);
                
            }
        }
        
        private void SetStatusText(string text1, string text2 , bool autoClear)
        {
            tsl1.Text = text1;
            tsl2.Text = text2;
            if( autoClear)
            timer1.Start();
        }

        void CheckNRemoveTNode(string findstr)
        {
            TreeNode[] tnc = tvTables.Nodes.Find(findstr, false);
            if (tnc.Length > 0)
                tvTables.Nodes.RemoveByKey(findstr);
        }
        #region Disabled External Diff Routines
        private void AddDiffNodes(string tblname, TableDiffResult difftbl)
        {
            CheckNRemoveTNode(tblname);

            TreeNode basetn = tvTables.Nodes.Add(tblname, tblname);
            if (difftbl.Diff.Count > 0)
                basetn.Nodes.Add("Diff [" + difftbl.Diff.Count.ToString()+"]");
            if (difftbl.Src.Count > 0)
                basetn.Nodes.Add("AOnly [" + difftbl.Src.Count.ToString() + "]");
            if (difftbl.Dest.Count > 0)
                basetn.Nodes.Add("BOnly [" + difftbl.Dest.Count.ToString() + "]");
            basetn.Tag = difftbl;
        }
        #endregion
        private void btnGenerate_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            tsl1.Text = "Ready";
            tsl2.Text = "";

        }

        private void btnDiffCells_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count != 2) return;
            string t1=null, t2=null;
            try
            {
                t1 = Path.GetTempFileName();
                t2 = Path.GetTempFileName();

                

                File.WriteAllText(t1, dataGridView1.SelectedCells[0].Value.ToString());
                File.WriteAllText(t2, dataGridView1.SelectedCells[1].Value.ToString());
                string param = string.Format(" \"{0}\" \"{1}\" ", t1, t2);
                if (dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString() == "B")
                    param = string.Format(" \"{0}\" \"{1}\" ", t2, t1);
                //Common.RunExtCmd( @"C:\Program Files\Kdiff3\kdiff3.exe", param);
                Common.RunExtCmd(@"C:\Program Files\WinMerge\WinMergeU.exe" , param);
            }
            finally
            {
                if(t1!= null && File.Exists(t1))
                    File.Delete(t1);
                if (t2 != null && File.Exists(t2))
                    File.Delete(t2);
            }
        }

        private void radioButtonA_CheckedChanged(object sender, EventArgs e)
        {
            DataTable t = (DataTable)dataGridView1.DataSource;
            t.DefaultView.RowFilter = "DB='A'";
        }

        private void radioButtonB_CheckedChanged(object sender, EventArgs e)
        {
            DataTable t = (DataTable)dataGridView1.DataSource;
            t.DefaultView.RowFilter = "DB='B'";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            DataTable t = (DataTable)dataGridView1.DataSource;
            t.DefaultView.RowFilter = "";
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            int cnt = e.RowIndex + 1;
            dataGridView1.Rows[e.RowIndex].HeaderCell.Value = cnt;


            //dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for(int i =0 ; i<e.RowCount; i++)
                dataGridView1.Rows[e.RowIndex + i].HeaderCell.Value =( e.RowIndex + 1+i).ToString();
        }
    }
}
