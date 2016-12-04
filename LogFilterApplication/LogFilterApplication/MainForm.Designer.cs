﻿namespace LogFilterApplication
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
            this.btnBrowseInputFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbInputFileLocation = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.tbUnknownSID = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.defaultSIDs = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBrowseInputFile
            // 
            this.btnBrowseInputFile.Location = new System.Drawing.Point(480, 26);
            this.btnBrowseInputFile.Name = "btnBrowseInputFile";
            this.btnBrowseInputFile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseInputFile.TabIndex = 0;
            this.btnBrowseInputFile.Text = "Browse";
            this.btnBrowseInputFile.UseVisualStyleBackColor = true;
            this.btnBrowseInputFile.Click += new System.EventHandler(this.btnBrowseInputFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Input Log File Location";
            // 
            // tbInputFileLocation
            // 
            this.tbInputFileLocation.Location = new System.Drawing.Point(137, 28);
            this.tbInputFileLocation.Name = "tbInputFileLocation";
            this.tbInputFileLocation.Size = new System.Drawing.Size(307, 20);
            this.tbInputFileLocation.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBrowseInputFile);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbInputFileLocation);
            this.groupBox1.Location = new System.Drawing.Point(26, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(571, 83);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose Input File";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnFilter);
            this.groupBox2.Controls.Add(this.tbUnknownSID);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.defaultSIDs);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(26, 125);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(369, 180);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter Log File";
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(234, 78);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 4;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // tbUnknownSID
            // 
            this.tbUnknownSID.Location = new System.Drawing.Point(9, 124);
            this.tbUnknownSID.Name = "tbUnknownSID";
            this.tbUnknownSID.Size = new System.Drawing.Size(168, 20);
            this.tbUnknownSID.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(9, 78);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(208, 40);
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
            this.defaultSIDs.Location = new System.Drawing.Point(71, 36);
            this.defaultSIDs.Name = "defaultSIDs";
            this.defaultSIDs.Size = new System.Drawing.Size(238, 21);
            this.defaultSIDs.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "SID";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 336);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "Log Filter Application";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
    }
}

