using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChaoYanIpc
{
    public partial class PassWord : Form
    {
        public PassWord()
        {
            InitializeComponent();
        }
        public delegate void ShowSuperUser();
        public ShowSuperUser SU;
        public ShowSuperUser InitDB;



        private void TB_PassWord_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (TB_PassWord.Text == "111")
                {
                    SU();
                    this.Close();
                }

               else if (TB_PassWord.Text == "0")
                {
                    InitDB();
                    this.Close();
                }


                else
                    MessageBox.Show("密码不对！请确认大小写", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
