using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ChaoYanIpc
{
    public partial class Register : Form
    {

        public Register()
        {
            InitializeComponent();
            this.TB_MachineCode.Text = softReg.GetMNum();

        }
        private SoftReg softReg = new SoftReg();


        private void Btn_ConfirmReg_Click(object sender, EventArgs e)
        {
            try
            {
                if (TB_RegisterCode.Text == softReg.GetRNum())
                {
                    MessageBox.Show("注册成功！重启软件后生效！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RegistryKey retkey = Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("mySoftWare").CreateSubKey("Register.INI").CreateSubKey(TB_RegisterCode.Text);
                    retkey.SetValue("UserName", "Rsoft");
                    this.Close();
                }

                else
                {
                    MessageBox.Show("注册码错误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TB_RegisterCode.SelectAll();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
