namespace LogFilterApplication
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnBrowseInputFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbInputFileLocation = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbFileType = new System.Windows.Forms.ComboBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.tbUnknownSID = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.defaultSIDs = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnBrowseDataSource = new System.Windows.Forms.Button();
            this.tbDataSourceLocation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBrowseInputFile
            // 
            this.btnBrowseInputFile.Location = new System.Drawing.Point(640, 32);
            this.btnBrowseInputFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBrowseInputFile.Name = "btnBrowseInputFile";
            this.btnBrowseInputFile.Size = new System.Drawing.Size(100, 28);
            this.btnBrowseInputFile.TabIndex = 0;
            this.btnBrowseInputFile.Text = "Browse";
            this.btnBrowseInputFile.UseVisualStyleBackColor = true;
            this.btnBrowseInputFile.Click += new System.EventHandler(this.btnBrowseInputFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Input Log File Location";
            // 
            // tbInputFileLocation
            // 
            this.tbInputFileLocation.Location = new System.Drawing.Point(183, 34);
            this.tbInputFileLocation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbInputFileLocation.Name = "tbInputFileLocation";
            this.tbInputFileLocation.Size = new System.Drawing.Size(408, 22);
            this.tbInputFileLocation.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBrowseInputFile);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbInputFileLocation);
            this.groupBox1.Location = new System.Drawing.Point(35, 160);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(761, 102);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose Input File";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cbFileType);
            this.groupBox2.Controls.Add(this.btnFilter);
            this.groupBox2.Controls.Add(this.tbUnknownSID);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.defaultSIDs);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(35, 305);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(761, 222);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter Log File";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(528, 48);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Output file type";
            // 
            // cbFileType
            // 
            this.cbFileType.FormattingEnabled = true;
            this.cbFileType.Items.AddRange(new object[] {
            ".xls",
            ".csv"});
            this.cbFileType.Location = new System.Drawing.Point(653, 44);
            this.cbFileType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbFileType.Name = "cbFileType";
            this.cbFileType.Size = new System.Drawing.Size(85, 24);
            this.cbFileType.TabIndex = 5;
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(369, 96);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(100, 28);
            this.btnFilter.TabIndex = 4;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // tbUnknownSID
            // 
            this.tbUnknownSID.Location = new System.Drawing.Point(12, 153);
            this.tbUnknownSID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbUnknownSID.Name = "tbUnknownSID";
            this.tbUnknownSID.Size = new System.Drawing.Size(276, 22);
            this.tbUnknownSID.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(12, 96);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(277, 49);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "Please enter the SID you would like to find in this box if it is not listed";
            // 
            // defaultSIDs
            // 
            this.defaultSIDs.FormattingEnabled = true;
            this.defaultSIDs.Items.AddRange(new object[] {
            "5d7.0",
            "186.16",
            "1f8.40",
            "430.38",
            "7ec.623203.24",
            "7ec.623204.24",
            "7ec.622004.24",
            "42e.44",
            "7ec.622005.24"});
            this.defaultSIDs.Location = new System.Drawing.Point(95, 44);
            this.defaultSIDs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.defaultSIDs.Name = "defaultSIDs";
            this.defaultSIDs.Size = new System.Drawing.Size(373, 24);
            this.defaultSIDs.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 48);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "SID";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnBrowseDataSource);
            this.groupBox3.Controls.Add(this.tbDataSourceLocation);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(35, 15);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(761, 97);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Choose Data Source";
            // 
            // btnBrowseDataSource
            // 
            this.btnBrowseDataSource.Location = new System.Drawing.Point(636, 34);
            this.btnBrowseDataSource.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBrowseDataSource.Name = "btnBrowseDataSource";
            this.btnBrowseDataSource.Size = new System.Drawing.Size(100, 28);
            this.btnBrowseDataSource.TabIndex = 2;
            this.btnBrowseDataSource.Text = "Browse";
            this.btnBrowseDataSource.UseVisualStyleBackColor = true;
            this.btnBrowseDataSource.Click += new System.EventHandler(this.btnBrowseDataSource_Click);
            // 
            // tbDataSourceLocation
            // 
            this.tbDataSourceLocation.Location = new System.Drawing.Point(183, 37);
            this.tbDataSourceLocation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbDataSourceLocation.Name = "tbDataSourceLocation";
            this.tbDataSourceLocation.Size = new System.Drawing.Size(408, 22);
            this.tbDataSourceLocation.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 41);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Data Source Location";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 567);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "Log Filter Application";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBrowseInputFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbInputFileLocation;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.TextBox tbUnknownSID;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox defaultSIDs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnBrowseDataSource;
        private System.Windows.Forms.TextBox tbDataSourceLocation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbFileType;
    }
}

