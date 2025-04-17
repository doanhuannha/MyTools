using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueMoon.ClientApp
{
    partial class PreviewForm : Form
    {
        public PreviewForm()
        {
            InitializeComponent();
            ctrlContent.MaxLength = int.MaxValue;
        }

        public DialogResult ShowDialog(string content, IWin32Window owner, bool isValid)
        {
            ctrlContent.Text = content;
            ctrlOK.Enabled = isValid;
            return this.ShowDialog(owner);
        }
    }
}
