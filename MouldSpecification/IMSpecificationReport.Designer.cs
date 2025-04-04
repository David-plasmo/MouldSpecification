﻿using System.Windows.Forms;

namespace MouldSpecification
{
    partial class IMSpecificationReport
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnShowReport = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cboProductCode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboCustomer = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnShowReport);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.cboProductCode);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.cboCustomer);
            this.splitContainer1.Size = new System.Drawing.Size(1653, 916);
            this.splitContainer1.SplitterDistance = 66;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnShowReport
            // 
            this.btnShowReport.Location = new System.Drawing.Point(1054, 13);
            this.btnShowReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnShowReport.Name = "btnShowReport";
            this.btnShowReport.Size = new System.Drawing.Size(108, 43);
            this.btnShowReport.TabIndex = 4;
            this.btnShowReport.Text = "Show Report";
            this.btnShowReport.UseVisualStyleBackColor = true;
            this.btnShowReport.Click += new System.EventHandler(this.btnShowReport_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(486, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Code";
            // 
            // cboProductCode
            // 
            this.cboProductCode.FormattingEnabled = true;
            this.cboProductCode.Location = new System.Drawing.Point(556, 19);
            this.cboProductCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboProductCode.Name = "cboProductCode";
            this.cboProductCode.Size = new System.Drawing.Size(287, 33);
            this.cboProductCode.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Customer";
            // 
            // cboCustomer
            // 
            this.cboCustomer.FormattingEnabled = true;
            this.cboCustomer.Location = new System.Drawing.Point(158, 19);
            this.cboCustomer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Size = new System.Drawing.Size(298, 33);
            this.cboCustomer.TabIndex = 0;
            // 
            // IMSpecificationReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1653, 916);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "IMSpecificationReport";
            this.Text = "IMSpecificationReport";
            this.Load += new System.EventHandler(this.IMSpecificationReport_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer splitContainer1;
        private Button btnShowReport;
        private Label label2;
        private ComboBox cboProductCode;
        private Label label1;
        private ComboBox cboCustomer;
    }
}