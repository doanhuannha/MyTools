namespace BlueMoon.ClientApp
{
    partial class Main
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            layoutButtons = new FlowLayoutPanel();
            ctrlSubmit = new Button();
            ctrlMultipleRun = new Button();
            layoutRequest = new TableLayoutPanel();
            ctrlRequestData = new TextBox();
            ctrlGridParams = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Browse = new DataGridViewButtonColumn();
            ctrlMethod = new Label();
            ctrlURL = new TextBox();
            label4 = new Label();
            ctrlResponseInfo = new Label();
            label8 = new Label();
            label7 = new Label();
            ctrlEnv = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            ctrlApi = new ComboBox();
            label3 = new Label();
            ctrlDesc = new Label();
            label5 = new Label();
            ctrlResponseData = new TextBox();
            layoutMain = new TableLayoutPanel();
            layoutButtons.SuspendLayout();
            layoutRequest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ctrlGridParams).BeginInit();
            layoutMain.SuspendLayout();
            SuspendLayout();
            // 
            // layoutButtons
            // 
            layoutButtons.Controls.Add(ctrlSubmit);
            layoutButtons.Controls.Add(ctrlMultipleRun);
            layoutButtons.Dock = DockStyle.Fill;
            layoutButtons.FlowDirection = FlowDirection.RightToLeft;
            layoutButtons.Location = new Point(88, 320);
            layoutButtons.Margin = new Padding(0);
            layoutButtons.Name = "layoutButtons";
            layoutButtons.Size = new Size(811, 30);
            layoutButtons.TabIndex = 16;
            // 
            // ctrlSubmit
            // 
            ctrlSubmit.Anchor = AnchorStyles.None;
            ctrlSubmit.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ctrlSubmit.Location = new Point(728, 2);
            ctrlSubmit.Margin = new Padding(3, 2, 3, 2);
            ctrlSubmit.Name = "ctrlSubmit";
            ctrlSubmit.Size = new Size(80, 26);
            ctrlSubmit.TabIndex = 12;
            ctrlSubmit.Text = "&Preview";
            ctrlSubmit.UseVisualStyleBackColor = true;
            ctrlSubmit.Click += Submit_Click;
            // 
            // ctrlMultipleRun
            // 
            ctrlMultipleRun.Anchor = AnchorStyles.None;
            ctrlMultipleRun.Location = new Point(586, 2);
            ctrlMultipleRun.Margin = new Padding(3, 2, 3, 2);
            ctrlMultipleRun.Name = "ctrlMultipleRun";
            ctrlMultipleRun.Size = new Size(136, 26);
            ctrlMultipleRun.TabIndex = 13;
            ctrlMultipleRun.Text = "&Multiple requests";
            ctrlMultipleRun.UseVisualStyleBackColor = true;
            ctrlMultipleRun.Click += MultipleRun_Click;
            // 
            // layoutRequest
            // 
            layoutRequest.ColumnCount = 2;
            layoutRequest.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.66666F));
            layoutRequest.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            layoutRequest.Controls.Add(ctrlRequestData, 0, 0);
            layoutRequest.Controls.Add(ctrlGridParams, 1, 0);
            layoutRequest.Dock = DockStyle.Fill;
            layoutRequest.Location = new Point(91, 167);
            layoutRequest.Margin = new Padding(3, 2, 3, 2);
            layoutRequest.Name = "layoutRequest";
            layoutRequest.RowCount = 1;
            layoutRequest.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutRequest.Size = new Size(805, 151);
            layoutRequest.TabIndex = 15;
            // 
            // ctrlRequestData
            // 
            ctrlRequestData.BorderStyle = BorderStyle.FixedSingle;
            ctrlRequestData.Dock = DockStyle.Fill;
            ctrlRequestData.Location = new Point(3, 2);
            ctrlRequestData.Margin = new Padding(3, 2, 3, 2);
            ctrlRequestData.Multiline = true;
            ctrlRequestData.Name = "ctrlRequestData";
            ctrlRequestData.ScrollBars = ScrollBars.Vertical;
            ctrlRequestData.Size = new Size(530, 147);
            ctrlRequestData.TabIndex = 9;
            // 
            // ctrlGridParams
            // 
            ctrlGridParams.AllowUserToAddRows = false;
            ctrlGridParams.AllowUserToDeleteRows = false;
            ctrlGridParams.AllowUserToResizeRows = false;
            ctrlGridParams.BackgroundColor = SystemColors.Control;
            ctrlGridParams.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ctrlGridParams.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Browse });
            ctrlGridParams.Dock = DockStyle.Fill;
            ctrlGridParams.Location = new Point(539, 2);
            ctrlGridParams.Margin = new Padding(3, 2, 3, 2);
            ctrlGridParams.Name = "ctrlGridParams";
            ctrlGridParams.RowHeadersVisible = false;
            ctrlGridParams.RowHeadersWidth = 51;
            ctrlGridParams.RowTemplate.Height = 29;
            ctrlGridParams.Size = new Size(263, 147);
            ctrlGridParams.TabIndex = 17;
            ctrlGridParams.CellContentClick += ctrlGridParams_CellContentClick;
            // 
            // Column1
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(224, 224, 224);
            Column1.DefaultCellStyle = dataGridViewCellStyle1;
            Column1.Frozen = true;
            Column1.HeaderText = "Parameter";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 125;
            // 
            // Column2
            // 
            Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column2.HeaderText = "Value";
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            // 
            // Browse
            // 
            Browse.HeaderText = "";
            Browse.MinimumWidth = 6;
            Browse.Name = "Browse";
            Browse.Width = 40;
            // 
            // ctrlMethod
            // 
            ctrlMethod.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            ctrlMethod.AutoSize = true;
            ctrlMethod.Location = new Point(91, 135);
            ctrlMethod.Name = "ctrlMethod";
            ctrlMethod.Size = new Size(124, 30);
            ctrlMethod.TabIndex = 7;
            ctrlMethod.Text = "POST application/json";
            ctrlMethod.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ctrlURL
            // 
            ctrlURL.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            ctrlURL.BorderStyle = BorderStyle.None;
            ctrlURL.Location = new Point(91, 112);
            ctrlURL.Margin = new Padding(3, 2, 3, 2);
            ctrlURL.Name = "ctrlURL";
            ctrlURL.ReadOnly = true;
            ctrlURL.Size = new Size(805, 16);
            ctrlURL.TabIndex = 8;
            ctrlURL.Text = "[URL]";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(57, 105);
            label4.Name = "label4";
            label4.Size = new Size(28, 30);
            label4.TabIndex = 14;
            label4.Text = "URL";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ctrlResponseInfo
            // 
            ctrlResponseInfo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            ctrlResponseInfo.AutoSize = true;
            ctrlResponseInfo.Location = new Point(91, 505);
            ctrlResponseInfo.Name = "ctrlResponseInfo";
            ctrlResponseInfo.Size = new Size(16, 30);
            ctrlResponseInfo.TabIndex = 13;
            ctrlResponseInfo.Text = "...";
            ctrlResponseInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Location = new Point(28, 350);
            label8.Name = "label8";
            label8.Size = new Size(57, 155);
            label8.TabIndex = 11;
            label8.Text = "Response";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Location = new Point(36, 165);
            label7.Name = "label7";
            label7.Size = new Size(49, 155);
            label7.TabIndex = 8;
            label7.Text = "Request";
            // 
            // ctrlEnv
            // 
            ctrlEnv.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            ctrlEnv.DropDownStyle = ComboBoxStyle.DropDownList;
            ctrlEnv.FormattingEnabled = true;
            ctrlEnv.Location = new Point(91, 18);
            ctrlEnv.Margin = new Padding(3, 2, 3, 2);
            ctrlEnv.Name = "ctrlEnv";
            ctrlEnv.Size = new Size(805, 23);
            ctrlEnv.TabIndex = 1;
            ctrlEnv.SelectedIndexChanged += ctrlEnv_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(17, 15);
            label1.Name = "label1";
            label1.Size = new Size(68, 30);
            label1.TabIndex = 0;
            label1.Text = "Enviroment";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(60, 45);
            label2.Name = "label2";
            label2.Size = new Size(25, 30);
            label2.TabIndex = 2;
            label2.Text = "API";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ctrlApi
            // 
            ctrlApi.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            ctrlApi.DropDownStyle = ComboBoxStyle.DropDownList;
            ctrlApi.FormattingEnabled = true;
            ctrlApi.Location = new Point(91, 48);
            ctrlApi.Margin = new Padding(3, 2, 3, 2);
            ctrlApi.Name = "ctrlApi";
            ctrlApi.Size = new Size(805, 23);
            ctrlApi.TabIndex = 3;
            ctrlApi.SelectedIndexChanged += ctrlApi_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(18, 75);
            label3.Name = "label3";
            label3.Size = new Size(67, 30);
            label3.TabIndex = 4;
            label3.Text = "Description";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ctrlDesc
            // 
            ctrlDesc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            ctrlDesc.AutoSize = true;
            ctrlDesc.Location = new Point(91, 75);
            ctrlDesc.Name = "ctrlDesc";
            ctrlDesc.Size = new Size(155, 30);
            ctrlDesc.TabIndex = 5;
            ctrlDesc.Text = "[Some note for selected api]";
            ctrlDesc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(36, 135);
            label5.Name = "label5";
            label5.Size = new Size(49, 30);
            label5.TabIndex = 6;
            label5.Text = "Method";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ctrlResponseData
            // 
            ctrlResponseData.BorderStyle = BorderStyle.FixedSingle;
            ctrlResponseData.Dock = DockStyle.Fill;
            ctrlResponseData.Location = new Point(91, 352);
            ctrlResponseData.Margin = new Padding(3, 2, 3, 2);
            ctrlResponseData.Multiline = true;
            ctrlResponseData.Name = "ctrlResponseData";
            ctrlResponseData.ReadOnly = true;
            ctrlResponseData.ScrollBars = ScrollBars.Vertical;
            ctrlResponseData.Size = new Size(805, 151);
            ctrlResponseData.TabIndex = 10;
            // 
            // layoutMain
            // 
            layoutMain.ColumnCount = 3;
            layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 88F));
            layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 9F));
            layoutMain.Controls.Add(ctrlResponseData, 1, 8);
            layoutMain.Controls.Add(label5, 0, 5);
            layoutMain.Controls.Add(ctrlDesc, 1, 3);
            layoutMain.Controls.Add(label3, 0, 3);
            layoutMain.Controls.Add(ctrlApi, 1, 2);
            layoutMain.Controls.Add(label2, 0, 2);
            layoutMain.Controls.Add(label1, 0, 1);
            layoutMain.Controls.Add(label7, 0, 6);
            layoutMain.Controls.Add(label8, 0, 8);
            layoutMain.Controls.Add(ctrlResponseInfo, 1, 9);
            layoutMain.Controls.Add(label4, 0, 4);
            layoutMain.Controls.Add(ctrlURL, 1, 4);
            layoutMain.Controls.Add(ctrlMethod, 1, 5);
            layoutMain.Controls.Add(layoutRequest, 1, 6);
            layoutMain.Controls.Add(layoutButtons, 1, 7);
            layoutMain.Controls.Add(ctrlEnv, 1, 1);
            layoutMain.Dock = DockStyle.Fill;
            layoutMain.Location = new Point(0, 0);
            layoutMain.Margin = new Padding(3, 2, 3, 2);
            layoutMain.Name = "layoutMain";
            layoutMain.RowCount = 11;
            layoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            layoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            layoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            layoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            layoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            layoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            layoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            layoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            layoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 8F));
            layoutMain.Size = new Size(908, 543);
            layoutMain.TabIndex = 0;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(908, 543);
            Controls.Add(layoutMain);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "Main";
            Text = "API Client";
            Load += Main_Load;
            layoutButtons.ResumeLayout(false);
            layoutRequest.ResumeLayout(false);
            layoutRequest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ctrlGridParams).EndInit();
            layoutMain.ResumeLayout(false);
            layoutMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel layoutButtons;
        private Button ctrlSubmit;
        private TableLayoutPanel layoutRequest;
        private TextBox ctrlRequestData;
        private Label ctrlMethod;
        private TextBox ctrlURL;
        private Label label4;
        private Label ctrlResponseInfo;
        private Label label8;
        private Label label7;
        private ComboBox ctrlEnv;
        private Label label1;
        private Label label2;
        private ComboBox ctrlApi;
        private Label label3;
        private Label ctrlDesc;
        private Label label5;
        private TextBox ctrlResponseData;
        private TableLayoutPanel layoutMain;
        private DataGridView ctrlGridParams;
        private Button ctrlMultipleRun;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewButtonColumn Browse;
    }
}