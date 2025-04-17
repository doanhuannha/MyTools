using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace BlueMoon.ClientApp
{
    partial class FileForm : Form
    {
        public FileForm()
        {
            InitializeComponent();
        }

        public (string filePath, string mode) ShowDialogX(IWin32Window owner)
        {
            var r = this.ShowDialog(owner);
            if (r == DialogResult.OK) return (ctrlSelectedFile.Text, ctrlBase64Mode.Checked ? "base64" : "raw");
            else return default;
        }

        private void ctrlBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            var result = dialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                ctrlSelectedFile.Text = dialog.FileName;
            }
        }
    }
}
