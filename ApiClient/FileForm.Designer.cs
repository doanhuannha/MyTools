namespace BlueMoon.ClientApp
{
    partial class FileForm
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
            this.ctrlCancel = new System.Windows.Forms.Button();
            this.ctrlOK = new System.Windows.Forms.Button();
            this.ctrlBrowse = new System.Windows.Forms.Button();
            this.ctrlSelectedFile = new System.Windows.Forms.TextBox();
            this.ctrlBase64Mode = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel.Controls.Add(this.ctrlCancel, 2, 1);
            this.tableLayoutPanel.Controls.Add(this.ctrlOK, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.ctrlBrowse, 2, 0);
            this.tableLayoutPanel.Controls.Add(this.ctrlSelectedFile, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.ctrlBase64Mode, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(11, 13);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(508, 84);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // ctrlCancel
            // 
            this.ctrlCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ctrlCancel.Location = new System.Drawing.Point(393, 50);
            this.ctrlCancel.Margin = new System.Windows.Forms.Padding(5);
            this.ctrlCancel.Name = "ctrlCancel";
            this.ctrlCancel.Size = new System.Drawing.Size(110, 29);
            this.ctrlCancel.TabIndex = 24;
            this.ctrlCancel.Text = "&Cancel";
            // 
            // ctrlOK
            // 
            this.ctrlOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ctrlOK.Location = new System.Drawing.Point(273, 50);
            this.ctrlOK.Margin = new System.Windows.Forms.Padding(5);
            this.ctrlOK.Name = "ctrlOK";
            this.ctrlOK.Size = new System.Drawing.Size(110, 29);
            this.ctrlOK.TabIndex = 25;
            this.ctrlOK.Text = "&Select";
            // 
            // ctrlBrowse
            // 
            this.ctrlBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlBrowse.Location = new System.Drawing.Point(393, 5);
            this.ctrlBrowse.Margin = new System.Windows.Forms.Padding(5);
            this.ctrlBrowse.Name = "ctrlBrowse";
            this.ctrlBrowse.Size = new System.Drawing.Size(110, 29);
            this.ctrlBrowse.TabIndex = 28;
            this.ctrlBrowse.Text = "&Browse";
            this.ctrlBrowse.Click += new System.EventHandler(this.ctrlBrowse_Click);
            // 
            // ctrlSelectedFile
            // 
            this.tableLayoutPanel.SetColumnSpan(this.ctrlSelectedFile, 2);
            this.ctrlSelectedFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlSelectedFile.Location = new System.Drawing.Point(3, 3);
            this.ctrlSelectedFile.Name = "ctrlSelectedFile";
            this.ctrlSelectedFile.Size = new System.Drawing.Size(382, 27);
            this.ctrlSelectedFile.TabIndex = 29;
            // 
            // ctrlBase64Mode
            // 
            this.ctrlBase64Mode.AutoSize = true;
            this.ctrlBase64Mode.Location = new System.Drawing.Point(3, 47);
            this.ctrlBase64Mode.Name = "ctrlBase64Mode";
            this.ctrlBase64Mode.Size = new System.Drawing.Size(151, 24);
            this.ctrlBase64Mode.TabIndex = 30;
            this.ctrlBase64Mode.Text = "Convert to base64";
            this.ctrlBase64Mode.UseVisualStyleBackColor = true;
            // 
            // FileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 110);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileForm";
            this.Padding = new System.Windows.Forms.Padding(11, 13, 11, 13);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select a file";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button ctrlCancel;
        private Button ctrlOK;
        private Button ctrlBrowse;
        private TextBox ctrlSelectedFile;
        private CheckBox ctrlBase64Mode;
    }
}
