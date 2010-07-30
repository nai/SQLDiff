namespace SimpleDBDiff
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnConnA = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBoxTbl = new System.Windows.Forms.ComboBox();
            this.comboBoxB = new System.Windows.Forms.ComboBox();
            this.comboBoxA = new System.Windows.Forms.ComboBox();
            this.btnDIFF = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tvTables = new System.Windows.Forms.TreeView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsl1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsl2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnDiffCells = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButtonA = new System.Windows.Forms.RadioButton();
            this.radioButtonB = new System.Windows.Forms.RadioButton();
            this.radioButtonAB = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(191, 106);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.Size = new System.Drawing.Size(471, 275);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            // 
            // btnConnA
            // 
            this.btnConnA.Location = new System.Drawing.Point(6, 16);
            this.btnConnA.Name = "btnConnA";
            this.btnConnA.Size = new System.Drawing.Size(83, 28);
            this.btnConnA.TabIndex = 1;
            this.btnConnA.Text = "Connection";
            this.btnConnA.UseVisualStyleBackColor = true;
            this.btnConnA.Click += new System.EventHandler(this.btnConnA_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGenerate);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.comboBoxB);
            this.groupBox1.Controls.Add(this.comboBoxA);
            this.groupBox1.Controls.Add(this.btnDIFF);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnConnA);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(521, 88);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Setup";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.comboBoxTbl);
            this.groupBox2.Location = new System.Drawing.Point(131, 43);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(316, 39);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tables";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(234, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 26);
            this.button1.TabIndex = 8;
            this.button1.Text = "Compare";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBoxTbl
            // 
            this.comboBoxTbl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTbl.FormattingEnabled = true;
            this.comboBoxTbl.Location = new System.Drawing.Point(36, 15);
            this.comboBoxTbl.Name = "comboBoxTbl";
            this.comboBoxTbl.Size = new System.Drawing.Size(192, 21);
            this.comboBoxTbl.TabIndex = 6;
            // 
            // comboBoxB
            // 
            this.comboBoxB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxB.FormattingEnabled = true;
            this.comboBoxB.Location = new System.Drawing.Point(292, 16);
            this.comboBoxB.Name = "comboBoxB";
            this.comboBoxB.Size = new System.Drawing.Size(155, 21);
            this.comboBoxB.TabIndex = 5;
            this.comboBoxB.SelectedIndexChanged += new System.EventHandler(this.comboBoxB_SelectedIndexChanged);
            // 
            // comboBoxA
            // 
            this.comboBoxA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxA.FormattingEnabled = true;
            this.comboBoxA.Location = new System.Drawing.Point(114, 16);
            this.comboBoxA.Name = "comboBoxA";
            this.comboBoxA.Size = new System.Drawing.Size(152, 21);
            this.comboBoxA.TabIndex = 4;
            this.comboBoxA.SelectedIndexChanged += new System.EventHandler(this.comboBoxB_SelectedIndexChanged);
            // 
            // btnDIFF
            // 
            this.btnDIFF.Location = new System.Drawing.Point(453, 13);
            this.btnDIFF.Name = "btnDIFF";
            this.btnDIFF.Size = new System.Drawing.Size(65, 31);
            this.btnDIFF.TabIndex = 4;
            this.btnDIFF.Text = "Compare";
            this.btnDIFF.UseVisualStyleBackColor = true;
            this.btnDIFF.Click += new System.EventHandler(this.btnDIFF_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(272, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "B";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "A";
            // 
            // tvTables
            // 
            this.tvTables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tvTables.HideSelection = false;
            this.tvTables.Location = new System.Drawing.Point(12, 106);
            this.tvTables.Name = "tvTables";
            this.tvTables.Size = new System.Drawing.Size(173, 275);
            this.tvTables.TabIndex = 5;
            this.tvTables.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvTables_AfterSelect);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsl1,
            this.tsl2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 384);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(674, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsl1
            // 
            this.tsl1.AutoSize = false;
            this.tsl1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tsl1.Name = "tsl1";
            this.tsl1.Size = new System.Drawing.Size(50, 17);
            this.tsl1.Text = "Ready";
            this.tsl1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsl2
            // 
            this.tsl2.Name = "tsl2";
            this.tsl2.Size = new System.Drawing.Size(609, 17);
            this.tsl2.Spring = true;
            this.tsl2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(453, 50);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(65, 31);
            this.btnGenerate.TabIndex = 7;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 7000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnDiffCells
            // 
            this.btnDiffCells.Location = new System.Drawing.Point(537, 15);
            this.btnDiffCells.Name = "btnDiffCells";
            this.btnDiffCells.Size = new System.Drawing.Size(75, 34);
            this.btnDiffCells.TabIndex = 7;
            this.btnDiffCells.Text = "Diff Cells";
            this.btnDiffCells.UseVisualStyleBackColor = true;
            this.btnDiffCells.Click += new System.EventHandler(this.btnDiffCells_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButtonAB);
            this.groupBox3.Controls.Add(this.radioButtonB);
            this.groupBox3.Controls.Add(this.radioButtonA);
            this.groupBox3.Location = new System.Drawing.Point(539, 55);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(123, 45);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Filter";
            // 
            // radioButtonA
            // 
            this.radioButtonA.AutoSize = true;
            this.radioButtonA.Location = new System.Drawing.Point(6, 21);
            this.radioButtonA.Name = "radioButtonA";
            this.radioButtonA.Size = new System.Drawing.Size(32, 17);
            this.radioButtonA.TabIndex = 0;
            this.radioButtonA.Text = "A";
            this.radioButtonA.UseVisualStyleBackColor = true;
            this.radioButtonA.CheckedChanged += new System.EventHandler(this.radioButtonA_CheckedChanged);
            // 
            // radioButtonB
            // 
            this.radioButtonB.AutoSize = true;
            this.radioButtonB.Location = new System.Drawing.Point(37, 21);
            this.radioButtonB.Name = "radioButtonB";
            this.radioButtonB.Size = new System.Drawing.Size(32, 17);
            this.radioButtonB.TabIndex = 1;
            this.radioButtonB.Text = "B";
            this.radioButtonB.UseVisualStyleBackColor = true;
            this.radioButtonB.CheckedChanged += new System.EventHandler(this.radioButtonB_CheckedChanged);
            // 
            // radioButtonAB
            // 
            this.radioButtonAB.AutoSize = true;
            this.radioButtonAB.Checked = true;
            this.radioButtonAB.Location = new System.Drawing.Point(71, 21);
            this.radioButtonAB.Name = "radioButtonAB";
            this.radioButtonAB.Size = new System.Drawing.Size(47, 17);
            this.radioButtonAB.TabIndex = 2;
            this.radioButtonAB.TabStop = true;
            this.radioButtonAB.Text = "Both";
            this.radioButtonAB.UseVisualStyleBackColor = true;
            this.radioButtonAB.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 406);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnDiffCells);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tvTables);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnConnA;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxB;
        private System.Windows.Forms.ComboBox comboBoxA;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDIFF;
        private System.Windows.Forms.TreeView tvTables;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsl1;
        private System.Windows.Forms.ToolStripStatusLabel tsl2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBoxTbl;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnDiffCells;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButtonB;
        private System.Windows.Forms.RadioButton radioButtonA;
        private System.Windows.Forms.RadioButton radioButtonAB;
    }
}

