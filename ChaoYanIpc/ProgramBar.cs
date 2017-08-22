using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ChaoYanIpc
{
    public partial class ProgramBar : Form
    {
        public ProgramBar()
        {
            InitializeComponent();
        }
        public void SetText(string label_text)
        {
            Lab_Disp.Text = label_text;
        }

    }





}
