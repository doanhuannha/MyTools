using BlueMoon.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BlueMoon.Common.ObjectExtension;

namespace CodeGen
{
    public partial class MainForm : Form
    {
        
        class DefTable
        {
            [ColumnName("TABLE_NAME")]
            public string TableName { get; set; }
        }
        private void LoadTemplateFileList(string templateFolder)
        {
            var files = Directory.GetFiles(templateFolder, "*.tpl");
            ctrlTemplateList.DataSource = files.Select(f => new { text = new FileInfo(f).Name, value = f }).ToList();
            ctrlTemplateList.SelectedIndex = 0;
        }
        public MainForm()
        {
            InitializeComponent();
        }
        const string USER_SETTINGS_CONNSTRING = "RecentConnString",
            USER_SETTINGS_TPLFOLDER = "RecentTplFolder",
            USER_SETTINGS_OUTFOLDER = "RecentOutFolder";
        private void MainForm_Load(object sender, EventArgs e)
        {
            ctrlConnString.Text = UserSettings.Default[USER_SETTINGS_CONNSTRING] as string;
            ctrlOutFolderBrowse.Text = UserSettings.Default[USER_SETTINGS_OUTFOLDER] as string;
            string templateFolder = UserSettings.Default[USER_SETTINGS_TPLFOLDER] as string;
            if(!string.IsNullOrEmpty(templateFolder)) LoadTemplateFileList(templateFolder);

        }
        private async void btLoadTables_Click(object sender, EventArgs e)
        {
            btLoadTables.Enabled = false;
            ctrlTableList.Items.Clear();
            string connStr = ctrlConnString.Text.Trim();
            DataAcess db = new DataAcess(connStr);
            var tbs = await db.ExecuteTextCommandAsync<DefTable>("SELECT [TABLE_NAME] FROM [INFORMATION_SCHEMA].[TABLES] (NOLOCK) WHERE [TABLE_TYPE] = 'BASE TABLE' ORDER BY [TABLE_NAME]");
            tbs.ForEach(p => { ctrlTableList.Items.Add(p.TableName); });
            ctrlTableList.Tag = connStr;
            UserSettings.Default[USER_SETTINGS_CONNSTRING] = connStr;
            UserSettings.Default.Save();
            btLoadTables.Enabled = true;
        }

        private void ctrlTplFolderBrowse_Click(object sender, EventArgs e)
        {

            var result = folderBrowserDialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                string templateFolder = folderBrowserDialog.SelectedPath;
                LoadTemplateFileList(templateFolder);

                UserSettings.Default[USER_SETTINGS_TPLFOLDER] = templateFolder;
                UserSettings.Default.Save();
            }
        }

        private void btOutputFolderBrowse_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ctrlOutFolderBrowse.Text)) folderBrowserDialog.SelectedPath = ctrlOutFolderBrowse.Text;
            var result = folderBrowserDialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                ctrlOutFolderBrowse.Text = folderBrowserDialog.SelectedPath;

                UserSettings.Default[USER_SETTINGS_OUTFOLDER] = ctrlOutFolderBrowse.Text;
                UserSettings.Default.Save();
            }
        }

        private async void btGenerate_Click(object sender, EventArgs e)
        {
            btGenerate.Enabled = false;
            string connStr = ctrlTableList.Tag as string;
            string[] tableNames = ctrlTableList.CheckedItems.Cast<string>().ToArray();
            await new CodeGenerator().GenerateFromMSSQL(connStr, ctrlTemplateList.SelectedValue.ToString(), ctrlOutFolderBrowse.Text, tableNames);
            ShowMessage("Generating is complete");
            btGenerate.Enabled = true;
        }

        private void ShowMessage(string msg)
        {
            Form form = new Form();
            form.Text = "Message";
            form.ClientSize = new Size(180, 80) ;
            form.StartPosition = FormStartPosition.CenterParent;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.ControlBox = false;
            
            Label label = new Label();
            label.Text = msg;
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.TopCenter;
            Button bt = new Button();
            bt.Text = "OK";
            bt.Dock = DockStyle.Bottom;
            bt.Click += (s, a) =>
            {
                form.Close();
            };
            form.Controls.Add(bt);
            form.Controls.Add(label);
            
            form.ShowDialog(this);
            form.Dispose();
        }

        
    }
}
