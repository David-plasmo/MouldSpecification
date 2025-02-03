namespace IMSpecification
{
    partial class ReportPrompt
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
            this.label2 = new System.Windows.Forms.Label();
            this.cboProduct = new System.Windows.Forms.ComboBox();
            this.btnShowReport = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnAssemblyFS = new System.Windows.Forms.Button();
            this.btnPackagingFS = new System.Windows.Forms.Button();
            this.btnPackagingIM = new System.Windows.Forms.Button();
            this.btnQCInstructionIM = new System.Windows.Forms.Button();
            this.btnIMCostCalculation = new System.Windows.Forms.Button();
            this.btnIMPriceDifference = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Product";
            // 
            // cboProduct
            // 
            this.cboProduct.FormattingEnabled = true;
            this.cboProduct.Location = new System.Drawing.Point(118, 12);
            this.cboProduct.Name = "cboProduct";
            this.cboProduct.Size = new System.Drawing.Size(583, 21);
            this.cboProduct.TabIndex = 2;
            // 
            // btnShowReport
            // 
            this.btnShowReport.Location = new System.Drawing.Point(716, 338);
            this.btnShowReport.Name = "btnShowReport";
            this.btnShowReport.Size = new System.Drawing.Size(108, 35);
            this.btnShowReport.TabIndex = 4;
            this.btnShowReport.Text = "Show Report";
            this.btnShowReport.UseVisualStyleBackColor = true;
            this.btnShowReport.Visible = false;
            this.btnShowReport.Click += new System.EventHandler(this.btnShowReport_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(32, 338);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 35);
            this.button1.TabIndex = 5;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAssemblyFS
            // 
            this.btnAssemblyFS.Enabled = false;
            this.btnAssemblyFS.Location = new System.Drawing.Point(118, 122);
            this.btnAssemblyFS.Name = "btnAssemblyFS";
            this.btnAssemblyFS.Size = new System.Drawing.Size(278, 29);
            this.btnAssemblyFS.TabIndex = 8;
            this.btnAssemblyFS.Text = "Product Assembly Sheet\r\n";
            this.btnAssemblyFS.UseVisualStyleBackColor = true;
            this.btnAssemblyFS.Click += new System.EventHandler(this.btnAssemblyFS_Click);
            // 
            // btnPackagingFS
            // 
            this.btnPackagingFS.Enabled = false;
            this.btnPackagingFS.Location = new System.Drawing.Point(118, 170);
            this.btnPackagingFS.Name = "btnPackagingFS";
            this.btnPackagingFS.Size = new System.Drawing.Size(278, 29);
            this.btnPackagingFS.TabIndex = 9;
            this.btnPackagingFS.Text = "Product Packaging Sheet";
            this.btnPackagingFS.UseVisualStyleBackColor = true;
            this.btnPackagingFS.Click += new System.EventHandler(this.btnPackagingFS_Click);
            // 
            // btnPackagingIM
            // 
            this.btnPackagingIM.Enabled = false;
            this.btnPackagingIM.Location = new System.Drawing.Point(423, 122);
            this.btnPackagingIM.Name = "btnPackagingIM";
            this.btnPackagingIM.Size = new System.Drawing.Size(278, 29);
            this.btnPackagingIM.TabIndex = 10;
            this.btnPackagingIM.Text = "Product Specification Sheet";
            this.btnPackagingIM.UseVisualStyleBackColor = true;
            this.btnPackagingIM.Click += new System.EventHandler(this.btnPackagingIM_Click);
            // 
            // btnQCInstructionIM
            // 
            this.btnQCInstructionIM.Enabled = false;
            this.btnQCInstructionIM.Location = new System.Drawing.Point(423, 170);
            this.btnQCInstructionIM.Name = "btnQCInstructionIM";
            this.btnQCInstructionIM.Size = new System.Drawing.Size(278, 29);
            this.btnQCInstructionIM.TabIndex = 14;
            this.btnQCInstructionIM.Text = "QC Instruction - Injection Moulding";
            this.btnQCInstructionIM.UseVisualStyleBackColor = true;
            this.btnQCInstructionIM.Click += new System.EventHandler(this.btnQCInstructionIM_Click);
            // 
            // btnIMCostCalculation
            // 
            this.btnIMCostCalculation.Enabled = false;
            this.btnIMCostCalculation.Location = new System.Drawing.Point(423, 76);
            this.btnIMCostCalculation.Name = "btnIMCostCalculation";
            this.btnIMCostCalculation.Size = new System.Drawing.Size(278, 29);
            this.btnIMCostCalculation.TabIndex = 15;
            this.btnIMCostCalculation.Text = "Injection Mould Product Cost Calculation";
            this.btnIMCostCalculation.UseVisualStyleBackColor = true;
            this.btnIMCostCalculation.Click += new System.EventHandler(this.btnIMCostCalculation_Click);
            // 
            // btnIMPriceDifference
            // 
            this.btnIMPriceDifference.Location = new System.Drawing.Point(118, 76);
            this.btnIMPriceDifference.Name = "btnIMPriceDifference";
            this.btnIMPriceDifference.Size = new System.Drawing.Size(278, 29);
            this.btnIMPriceDifference.TabIndex = 16;
            this.btnIMPriceDifference.Text = "Injection Mould Price Difference";
            this.btnIMPriceDifference.UseVisualStyleBackColor = true;
            this.btnIMPriceDifference.Click += new System.EventHandler(this.btnIMPriceDifference_Click);
            // 
            // ReportPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 399);
            this.Controls.Add(this.btnIMPriceDifference);
            this.Controls.Add(this.btnIMCostCalculation);
            this.Controls.Add(this.btnQCInstructionIM);
            this.Controls.Add(this.btnPackagingIM);
            this.Controls.Add(this.btnPackagingFS);
            this.Controls.Add(this.btnAssemblyFS);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnShowReport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboProduct);
            this.Name = "ReportPrompt";
            this.Text = "Report Prompt";
            this.Load += new System.EventHandler(this.EMCostReportPrompt_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboProduct;
        private System.Windows.Forms.Button btnShowReport;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnAssemblyFS;
        private System.Windows.Forms.Button btnPackagingFS;
        private System.Windows.Forms.Button btnPackagingIM;
        private System.Windows.Forms.Button btnQCInstructionIM;
        private System.Windows.Forms.Button btnIMCostCalculation;
        private System.Windows.Forms.Button btnIMPriceDifference;
    }
}