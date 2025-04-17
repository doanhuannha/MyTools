using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueMoon.ClientApp
{
    partial class ParameterForm : Form
    {
        public ParameterForm()
        {
            InitializeComponent();
        }

        public string ShowDialogX(IWin32Window owner, string sample)
        {
            ctrlParamValues.Text = sample;
            var r = this.ShowDialog(owner);
            if (r == DialogResult.OK) return ctrlParamValues.Text;
            else return null;
        }

        private void ctrlLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            var result = dialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {

                ctrlParamValues.Text = File.ReadAllText(dialog.FileName);
            }
        }
    }
}
