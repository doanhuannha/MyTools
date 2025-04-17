
namespace CodeGen
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrlConnString = new System.Windows.Forms.TextBox();
            this.btLoadTables = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlTableList = new System.Windows.Forms.CheckedListBox();
            this.btGenerate = new System.Windows.Forms.Button();
            this.ctrlTemplateList = new System.Windows.Forms.ComboBox();
            this.ctrlTplFolderBrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ctrlOutFolderBrowse = new System.Windows.Forms.TextBox();
            this.btOutputFolderBrowse = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlConnString
            // 
            this.ctrlConnString.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlConnString.Location = new System.Drawing.Point(174, 31);
            this.ctrlConnString.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ctrlConnString.Name = "ctrlConnString";
            this.ctrlConnString.Size = new System.Drawing.Size(497, 27);
            this.ctrlConnString.TabIndex = 0;
            this.ctrlConnString.Text = "Data Source=W1CVDS1128.1DC.COM;Initial Catalog=LW_Fiserv_API;Integrated Security=" +
    "SSPI";
            // 
            // btLoadTables
            // 
            this.btLoadTables.Location = new System.Drawing.Point(677, 31);
            this.btLoadTables.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btLoadTables.Name = "btLoadTables";
            this.btLoadTables.Size = new System.Drawing.Size(86, 29);
            this.btLoadTables.TabIndex = 1;
            this.btLoadTables.Text = "Load tables";
            this.btLoadTables.UseVisualStyleBackColor = true;
            this.btLoadTables.Click += new System.EventHandler(this.btLoadTables_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 37);
            this.label1.TabIndex = 2;
            this.label1.Text = "Connection string";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctrlTableList
            // 
            this.ctrlTableList.CheckOnClick = true;
            this.ctrlTableList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlTableList.FormattingEnabled = true;
            this.ctrlTableList.Location = new System.Drawing.Point(174, 68);
            this.ctrlTableList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ctrlTableList.Name = "ctrlTableList";
            this.ctrlTableList.Size = new System.Drawing.Size(497, 177);
            this.ctrlTableList.TabIndex = 3;
            // 
            // btGenerate
            // 
            this.btGenerate.Location = new System.Drawing.Point(677, 327);
            this.btGenerate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btGenerate.Name = "btGenerate";
            this.btGenerate.Size = new System.Drawing.Size(86, 29);
            this.btGenerate.TabIndex = 4;
            this.btGenerate.Text = "Generate";
            this.btGenerate.UseVisualStyleBackColor = true;
            this.btGenerate.Click += new System.EventHandler(this.btGenerate_Click);
            // 
            // ctrlTemplateList
            // 
            this.ctrlTemplateList.DisplayMember = "text";
            this.ctrlTemplateList.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlTemplateList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctrlTemplateList.FormattingEnabled = true;
            this.ctrlTemplateList.Location = new System.Drawing.Point(174, 253);
            this.ctrlTemplateList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ctrlTemplateList.Name = "ctrlTemplateList";
            this.ctrlTemplateList.Size = new System.Drawing.Size(497, 28);
            this.ctrlTemplateList.TabIndex = 5;
            this.ctrlTemplateList.ValueMember = "value";
            // 
            // ctrlTplFolderBrowse
            // 
            this.ctrlTplFolderBrowse.Location = new System.Drawing.Point(677, 253);
            this.ctrlTplFolderBrowse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ctrlTplFolderBrowse.Name = "ctrlTplFolderBrowse";
            this.ctrlTplFolderBrowse.Size = new System.Drawing.Size(86, 29);
            this.ctrlTplFolderBrowse.TabIndex = 6;
            this.ctrlTplFolderBrowse.Text = "Browse...";
            this.ctrlTplFolderBrowse.UseVisualStyleBackColor = true;
            this.ctrlTplFolderBrowse.Click += new System.EventHandler(this.ctrlTplFolderBrowse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 249);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 37);
            this.label2.TabIndex = 7;
            this.label2.Text = "Template";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 286);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 37);
            this.label3.TabIndex = 9;
            this.label3.Text = "Output folder";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctrlOutFolderBrowse
            // 
            this.ctrlOutFolderBrowse.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlOutFolderBrowse.Location = new System.Drawing.Point(174, 290);
            this.ctrlOutFolderBrowse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ctrlOutFolderBrowse.Name = "ctrlOutFolderBrowse";
            this.ctrlOutFolderBrowse.Size = new System.Drawing.Size(497, 27);
            this.ctrlOutFolderBrowse.TabIndex = 8;
            // 
            // btOutputFolderBrowse
            // 
            this.btOutputFolderBrowse.Location = new System.Drawing.Point(677, 290);
            this.btOutputFolderBrowse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btOutputFolderBrowse.Name = "btOutputFolderBrowse";
            this.btOutputFolderBrowse.Size = new System.Drawing.Size(86, 29);
            this.btOutputFolderBrowse.TabIndex = 10;
            this.btOutputFolderBrowse.Text = "Browse...";
            this.btOutputFolderBrowse.UseVisualStyleBackColor = true;
            this.btOutputFolderBrowse.Click += new System.EventHandler(this.btOutputFolderBrowse_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 171F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 137F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btGenerate, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.btOutputFolderBrowse, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.ctrlConnString, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.ctrlOutFolderBrowse, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btLoadTables, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.ctrlTableList, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.ctrlTemplateList, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.ctrlTplFolderBrowse, 2, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(811, 360);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.CommonStartup;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 360);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "Code generator";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox ctrlConnString;
        private System.Windows.Forms.Button btLoadTables;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox ctrlTableList;
        private System.Windows.Forms.Button btGenerate;
        private System.Windows.Forms.ComboBox ctrlTemplateList;
        private System.Windows.Forms.Button ctrlTplFolderBrowse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ctrlOutFolderBrowse;
        private System.Windows.Forms.Button btOutputFolderBrowse;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}

