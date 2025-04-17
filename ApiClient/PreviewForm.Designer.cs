namespace BlueMoon.ClientApp
{
    partial class PreviewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ctrlOK = new System.Windows.Forms.Button();
            this.ctrlContent = new System.Windows.Forms.TextBox();
            this.ctrlCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel.Controls.Add(this.ctrlOK, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.ctrlContent, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.ctrlCancel, 1, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(11, 13);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(559, 409);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // ctrlOK
            // 
            this.ctrlOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ctrlOK.Location = new System.Drawing.Point(333, 375);
            this.ctrlOK.Margin = new System.Windows.Forms.Padding(5);
            this.ctrlOK.Name = "ctrlOK";
            this.ctrlOK.Size = new System.Drawing.Size(101, 29);
            this.ctrlOK.TabIndex = 25;
            this.ctrlOK.Text = "&Submit";
            // 
            // ctrlContent
            // 
            this.ctrlContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel.SetColumnSpan(this.ctrlContent, 2);
            this.ctrlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlContent.Location = new System.Drawing.Point(8, 5);
            this.ctrlContent.Margin = new System.Windows.Forms.Padding(8, 5, 5, 5);
            this.ctrlContent.Multiline = true;
            this.ctrlContent.Name = "ctrlContent";
            this.ctrlContent.ReadOnly = true;
            this.ctrlContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ctrlContent.Size = new System.Drawing.Size(546, 359);
            this.ctrlContent.TabIndex = 23;
            this.ctrlContent.TabStop = false;
            this.ctrlContent.WordWrap = false;
            // 
            // ctrlCancel
            // 
            this.ctrlCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ctrlCancel.Location = new System.Drawing.Point(453, 375);
            this.ctrlCancel.Margin = new System.Windows.Forms.Padding(5);
            this.ctrlCancel.Name = "ctrlCancel";
            this.ctrlCancel.Size = new System.Drawing.Size(101, 29);
            this.ctrlCancel.TabIndex = 24;
            this.ctrlCancel.Text = "&Cancel";
            // 
            // PreviewForm
            // 
            this.AcceptButton = this.ctrlCancel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 435);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreviewForm";
            this.Padding = new System.Windows.Forms.Padding(11, 13, 11, 13);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preview";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.TextBox ctrlContent;
        private System.Windows.Forms.Button ctrlCancel;
        private Button ctrlOK;
    }
}
