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
  

    public partial class Form_DateSelect : Form
    {
        public Form_DateSelect()
        {
            InitializeComponent();
            DTP_Start.Format = DateTimePickerFormat.Custom; //设置为显示格式为自定义
            DTP_Start.CustomFormat = "yyyy/MM/dd"; //设置显示格式
            DTP_End.Format = DateTimePickerFormat.Custom; //设置为显示格式为自定义
            DTP_End.CustomFormat = "yyyy/MM/dd"; //设置显示格式
            TP_Start.Value = DateTime.Parse("0:00:00");
           TP_End.Value = DateTime.Parse("23:59:59");
            
        }

        //members
        //public string Date_Begin;
        //public string Date_End;
        public DateTime Date_Begin;
        public DateTime Date_End;
        public DateTime Time_Begin;
        public DateTime Time_End;
 

        public EventArg arg;
        public delegate int ShowTable(Form_DateSelect sender,DateTime[] DateTime);//定义委托类
        public event ShowTable CloseForm;
        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void Btn_DateConfirm_Click(object sender, EventArgs e)
        {
            //  Date_Begin = DTP_Start.Text;
            //Date_End = DTP_End.Text;
            Date_Begin = DTP_Start.Value;
            Date_End = DTP_End.Value;
            Time_Begin = TP_Start.Value;
            Time_End = TP_End.Value;
            DateTime[] DT_Array = { Date_Begin, Date_End, Time_Begin, Time_End };
            Debug.WriteLine(Date_Begin);
            PanForWait.BringToFront();
            Application.DoEvents();
           // Debug.WriteLine(Date_Begin);
            CloseForm(this,DT_Array);
        }

        public void HidePanForWait()
        {
            PanForWait.Hide();
        }



        public void wait()
        {
            PanForWait.BringToFront();
            Timer_Wait.Interval = 1000;
            Label_Wait.Visible = true;
            Timer_Wait.Enabled = true;

        }


        private void DTP_Start_ValueChanged(object sender, EventArgs e)
        {
           //DTP_End.Value = DTP_Start.Value; 
        }

        private void Timer_Wait_Tick(object sender, EventArgs e)
        {

            if (Label_Wait.Visible == true)
                Label_Wait.Visible = false;
            else
                Label_Wait.Visible = true;
        }

        private void Form_DateSelect_FormClosing(object sender, FormClosingEventArgs e)
        {
            Timer_Wait.Enabled = false; 
        }

        private void TP_Start_ValueChanged(object sender, EventArgs e)
        {

        }
    }
    public class EventArg
    {
        public EventArg(string _arg1, string _arg2)
        {
            arg1 = _arg1;
            arg2 = _arg2;

        }
        public string arg1;
        public string arg2;
    }
}
