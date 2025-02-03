namespace MouldSpecification
{
    partial class ImportAccessIM
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
            this.btnOK = new System.Windows.Forms.Button();
            this.checkedTaskList = new System.Windows.Forms.CheckedListBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(607, 651);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(210, 58);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "Return";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.UseWaitCursor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // checkedTaskList
            // 
            this.checkedTaskList.FormattingEnabled = true;
            this.checkedTaskList.Location = new System.Drawing.Point(30, 67);
            this.checkedTaskList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkedTaskList.Name = "checkedTaskList";
            this.checkedTaskList.Size = new System.Drawing.Size(711, 484);
            this.checkedTaskList.TabIndex = 1;
            this.checkedTaskList.UseWaitCursor = true;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(26, 667);
            this.lblResult.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(0, 29);
            this.lblResult.TabIndex = 2;
            this.lblResult.UseWaitCursor = true;
            // 
            // ImportAccessIM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 867);
            this.ControlBox = false;
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.checkedTaskList);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "ImportAccessIM";
            this.Text = "Import Injection Moulding Specifications";
            this.UseWaitCursor = true;
            this.Shown += new System.EventHandler(this.ImportAccessIM_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckedListBox checkedTaskList;
        private System.Windows.Forms.Label lblResult;
    }
}