namespace BlueMoon.ClientApp
{
    partial class ParameterForm
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
            this.ctrlLoad = new System.Windows.Forms.Button();
            this.ctrlParamValues = new System.Windows.Forms.TextBox();
            this.ctrlCancel = new System.Windows.Forms.Button();
            this.ctrlOK = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel.Controls.Add(this.ctrlLoad, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.ctrlParamValues, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.ctrlCancel, 2, 1);
            this.tableLayoutPanel.Controls.Add(this.ctrlOK, 1, 1);
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
            // ctrlLoad
            // 
            this.ctrlLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ctrlLoad.Location = new System.Drawing.Point(5, 375);
            this.ctrlLoad.Margin = new System.Windows.Forms.Padding(5);
            this.ctrlLoad.Name = "ctrlLoad";
            this.ctrlLoad.Size = new System.Drawing.Size(101, 29);
            this.ctrlLoad.TabIndex = 26;
            this.ctrlLoad.Text = "&Load...";
            this.ctrlLoad.Click += new System.EventHandler(this.ctrlLoad_Click);
            // 
            // ctrlParamValues
            // 
            this.ctrlParamValues.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel.SetColumnSpan(this.ctrlParamValues, 3);
            this.ctrlParamValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlParamValues.Location = new System.Drawing.Point(8, 5);
            this.ctrlParamValues.Margin = new System.Windows.Forms.Padding(8, 5, 5, 5);
            this.ctrlParamValues.Multiline = true;
            this.ctrlParamValues.Name = "ctrlParamValues";
            this.ctrlParamValues.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ctrlParamValues.Size = new System.Drawing.Size(546, 359);
            this.ctrlParamValues.TabIndex = 23;
            this.ctrlParamValues.TabStop = false;
            this.ctrlParamValues.WordWrap = false;
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
            // ParameterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 435);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ParameterForm";
            this.Padding = new System.Windows.Forms.Padding(11, 13, 11, 13);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Parameters";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.TextBox ctrlParamValues;
        private System.Windows.Forms.Button ctrlCancel;
        private Button ctrlLoad;
        private Button ctrlOK;
    }
}
