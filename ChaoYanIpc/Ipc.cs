using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;
using System.Net;
using System.ServiceProcess;
using System.Threading;
using System.Xml;
using System.Runtime.InteropServices;
using System.Configuration;
using System.IO;
using Microsoft.Win32;
using System.Text;



namespace ChaoYanIpc
{

    public partial class Ipc : Form
    {
        //Variables

        private SerialPort port;
        private SerialPort port_left;
        private string message;
        private string message_left;

        [DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        private const int AW_HOR_POSITIVE = 0x0001;//从左向右显示
        private const int AW_HOR_NEGATIVE = 0x0002;//从右向左显示
        private const int AW_VER_POSITIVE = 0x0004;//从上到下显示
        private const int AW_VER_NEGATIVE = 0x0008;//从下到上显示
        private const int AW_CENTER = 0x0010;//从中间向四周
        private const int AW_HIDE = 0x10000;
        private const int AW_ACTIVATE = 0x20000;//普通显示
        private const int AW_SLIDE = 0x40000;
        private const int AW_BLEND = 0x80000;//透明渐变显示


        private string[] data = { "2017/07/1", "11:05:02", "2", "1", "1", "5", "180", "0.5" };
        // private DB DataBase;
        private SqlDB sqlDataBase;
        private ScrewData SD;
        private CancellationTokenSource cts = new CancellationTokenSource();
        Thread Thread_InputData;
        private DateTime Current = new DateTime();
        private DateTime BackTime;
        #region 构造函数
        private delegate void Dele_vv_Form();
        Dele_vv_Form DeleEntity_Text;
        private SoftReg softReg = new SoftReg();

        public Ipc()
        {
            InitializeComponent();
            // CheckForIllegalCrossThreadCalls = false;

            // DataBase = new DB();
            sqlDataBase = new SqlDB();
            sqlDataBase.SqlIsError += new SqlDB.SqlError(ShowSqlButton);
            // sqlDataBase.InialCon();
            SD = new ScrewData();
            Thread_InputData = new Thread(InputData);
            InitialSettings();
            //Application.DoEvents();
        }

        private void ShowSqlButton()
        {
            PanForSet.BringToFront();
            Btn_StartServer.Visible = true;
            Btn_StartServer.BackColor = Color.Red;
        }


        #endregion

        #region 按键响应
        private void Btn_StartMonitor_Click(object sender, EventArgs e)
        {

            if (port.IsOpen)   //需要关闭串口
            {
                
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("您确定要停止监控？将不再接收钻枪数据", "提示", messButton, MessageBoxIcon.Question);

                if (dr == DialogResult.OK)
                {
                    port_left.Close();
                    port.Close();
                    Application.DoEvents();
                    sqlDataBase.Disconn();
                    Btn_StartMonitor.Text = "开启监控";
                    Btn_StartMonitor.BackColor = Color.Red;
                }

            }
            else
            {

              
                if (sqlDataBase.InialCon())
                { Btn_StartMonitor.Text = "停止监控";
                    Btn_StartMonitor.BackColor = Color.Transparent;
                    port.Open();
                    port_left.Open();
                }
                else
                { MessageBox.Show("连接数据库失败！请联系技术人员处理", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }


            }


        }

        private void Btn_ClearTable_Click(object sender, EventArgs e)
        {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("您确定要清空数据？\r(提示：此操作仅清空显示屏，不会影响数据存储)", "提示", messButton, MessageBoxIcon.Question);

            if (dr == DialogResult.OK)
            {
                DGV.Rows.Clear();
                Label_Amount.Text = (DGV.Rows.Count - 1).ToString();
            }

        }

        #endregion

        #region 启动函数区域
        private void InitialSettings()
        {
            string[] PortNames = SerialPort.GetPortNames();

            foreach (string s in PortNames)
            {//串口名称添加到cbxPortName下拉框上
                //一般计算机上是有COM1和COM2串口的，如果没有自己在cbxPortName下拉框里写COM1 和 COM2的字符串(如：this.cbxPortName.Items.Add("COM2"))
                SelectPort.Items.Add(s);
                SelectPort_Left.Items.Add(s);
            }
            port = new SerialPort();
            port_left = new SerialPort();
            if (PortNames.Length > 0)//判断串口为空
            {

                // Debug.WriteLine(PortNames.Length);

                int cnt = 0;
                port.PortName = ConfigurationManager.AppSettings["COM_RIGHT"];

                port.BaudRate = 19200;
                port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
                port.DataBits = 8;
                port.StopBits = StopBits.One;
                port.Parity = Parity.None;

                port_left.PortName = ConfigurationManager.AppSettings["COM_LEFT"];

                port_left.BaudRate = 19200;
                port_left.DataReceived += new SerialDataReceivedEventHandler(port_left_DataReceived);
                port_left.DataBits = 8;
                port_left.StopBits = StopBits.One;
                port_left.Parity = Parity.None;
                foreach (string items in SelectPort.Items)
                {
                    if (items == port.PortName)
                    {
                        SelectPort.SelectedIndex = cnt;
                    }
                    cnt++;
                }
                cnt = 0;
                foreach (string items in SelectPort_Left.Items)
                {
                    if (items == port_left.PortName)
                    {
                        SelectPort_Left.SelectedIndex = cnt;
                    }
                    cnt++;

                }



                //Debug.WriteLine(SelectPort.SelectedText);
                //  port.PortName = "COM1";
            }

            else
            {
                MessageBox.Show("没有找到串口", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //初始化端口




            //数据库操作
            label_BackUpPath.Text = ConfigurationManager.AppSettings["BackUpPath"];
            //控件
            CB_SetBackUp.Checked = true;
            CB_AutoStart.Checked = true;
            Combo_BackPeriod.SelectedIndex = 1;
            Timer_BackUp.Interval = 1000;


            //重新设置区域
            PanForDisp.Left = (this.Width - PanForDisp.Width) / 2;
            PanForDisp.Top = (this.Height - PanForDisp.Height) / 2;


            PanForSet.Left = (this.Width - PanForSet.Width) / 2;
            PanForSet.Top = (this.Height - PanForSet.Height) / 2;

            PanForHelp.Left = (this.Width - PanForHelp.Width) / 2;
            PanForHelp.Top = (this.Height - PanForHelp.Height) / 2;

            PanForQuery.Left = (this.Width - PanForQuery.Width) / 2;
            PanForQuery.Top = (this.Height - PanForQuery.Height) / 2;


        }
        #endregion

        #region 菜单栏

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PanForSet.BringToFront();

        }

        private void 数据显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PanForDisp.BringToFront();
        }
        private void Ipc_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("您确定要退出软件吗？（所有数据已自动保存）", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                e.Cancel = true;
                // Application.Exit();
            }


        }



        #endregion

        #region 串口通信
        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {


            //Debug.WriteLine("收到数据");
            string currentline = "";
            //循环接收串口中的数据
            Debug.WriteLine(Thread.CurrentThread.ManagedThreadId+"start");

            while (port.BytesToRead > 0)
            {
                char ch = (char)port.ReadByte();
                currentline += ch.ToString();
            }

            char StartChar = currentline[0];
            char EndChar = currentline[currentline.Length - 1];

            if (StartChar == '\r' && EndChar == '\n')
            {
                message += currentline;
                message += "1";
                GenerateData(message);
                message = "";
            }
            else if (StartChar == '\r')
            {
                message = "";
                message += currentline;

            }

            else if (EndChar == '\n')
            {

                message += currentline;
                message += "1";

                GenerateData(message);
            }
            else
            {
                message += currentline;


            }

            //CallDGV(messages[1]);
            //AddText();
            /*
          Byte[] Buf = new Byte[25];
          port.Read(Buf, 0, port.BytesToRead);
          ASCIIEncoding encoding = new ASCIIEncoding();
          currentline = encoding.GetString(Buf);
          */

            // currentline = ((SerialPort)sender).ReadExisting();

            //   Debug.WriteLine(DateTime.Now.ToString());

            //######在这里对接收到的数据进行显示

            //让本行再次恢复颜色
            Debug.WriteLine(Thread.CurrentThread.ManagedThreadId + "end");
        }

        private void GenerateData(string message)
        {
            label14.BackColor = Color.Yellow;
            ScrewData sd = new ScrewData();
            sd.Date = DateTime.Now.ToShortDateString();
            sd.Time = DateTime.Now.ToLongTimeString();
            sd.ReadValue = message.Substring(1, 4);
            sd.AxisNum = message.Substring(6, 2);
            sd.CircleNum = message.Substring(9, 2);
            sd.StepNum = message.Substring(12, 2);
            sd.Torque = message.Substring(34, 6);
            sd.Theta = message.Substring(42, 6);
            sd.Rate = message.Substring(50, 6);
            sd.Remark = message.Substring(58, 6);
            sd.Result = message.Substring(66, 3);
            sd.MachineNum = message[70].ToString();
            string[] data = { sd.Date, sd.Time, sd.MachineNum, sd.Type, sd.ReadValue, sd.StepNum, sd.Torque, sd.Theta, sd.IsOK, sd.Result };

            CallDGV(data);
            sqlDataBase.Add(sd);
            label14.BackColor = Color.Transparent;
        }





        private void port_left_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {


            //Debug.WriteLine("收到数据");
            string currentline_left = "";
            //循环接收串口中的数据


            while (port_left.BytesToRead > 0)
            {
                char ch = (char)port_left.ReadByte();
                currentline_left += ch.ToString();
            }
            char StartChar = currentline_left[0];
            char EndChar = currentline_left[currentline_left.Length - 1];
            if (StartChar == '\r' && EndChar == '\n')
            {
                message_left += currentline_left;
                message_left += "2";
                GenerateData(message_left);
                message_left = "";
            }

            else if (StartChar == '\r')
            {
                message_left = "";
                message_left += currentline_left;
            }

            else if (EndChar == '\n')
            {

                message_left += currentline_left;
                message_left += "2";
                GenerateData(message_left);
            }
            else
            {
                message_left += currentline_left;

            }

        }

        #endregion

        #region 跨线程调用Winform

        private delegate void InvokeDelegate();
        private delegate void DGV_Delegate(string[] data);

        private void AddText()
        {
            //使用Invoke代理的方式调用ModifyLabelText方法
            if (DGV.InvokeRequired)
            {
                InvokeDelegate invokeDelegate = new InvokeDelegate(AddText);
                Invoke(invokeDelegate);
            }

            else
            {
                DGV.Rows.Add(message);
                DGV.FirstDisplayedScrollingRowIndex = DGV.Rows.Count - 2;

                DGV.Rows[DGV.Rows.Count - 2].DefaultCellStyle.BackColor = Color.LightGreen;
                if (DGV.Rows.Count > 2)
                { DGV.Rows[DGV.Rows.Count - 3].DefaultCellStyle.BackColor = Color.White; }
            }
        }
        private void CallDGV(string[] data)
        {

            //使用Invoke代理的方式调用ModifyLabelText方法
            if (DGV.InvokeRequired)
            {
                DGV_Delegate dgv_dele = new DGV_Delegate(CallDGV);
                try
                {
                    this.Invoke(dgv_dele, (object)data);
                }
                catch (Exception e)
                { MessageBox.Show(e.Message); }
            }

            else
            {

                DGV.Rows.Add(data);
                DGV.FirstDisplayedScrollingRowIndex = DGV.Rows.Count - 2;

                Label_Amount.Text = (DGV.Rows.Count - 1).ToString();
                if (data[8] == "NOK")
                {
                    DGV.Rows[DGV.Rows.Count - 2].DefaultCellStyle.BackColor = Color.Red;
                }
                else
                {
                    // DGV.Rows[DGV.Rows.Count - 2].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                /*
                if (DGV.Rows.Count > 2)
                {
                    if (DGV.Rows[DGV.Rows.Count - 3].Cells[9].Value.ToString() != "YES")
                    {
                        DGV.Rows[DGV.Rows.Count - 3].DefaultCellStyle.BackColor = Color.White;
                    }
                }
                */
            }
        }

        //to be continued....
        //将数据格式转成SrewData格式，调用DB.Add即可



        #endregion


        //查询相关信息

        private void StartTimers()
        {
            // T_Refresh.Enabled = true; 
        }



        private void PanForDisp_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Ipc_Load(object sender, EventArgs e)
        {
            //注册

            RegistryKey retkey = Registry.CurrentUser.OpenSubKey("SOFTWARE", true).CreateSubKey("mySoftWare").CreateSubKey("Register.INI");
            foreach (string strRNum in retkey.GetSubKeyNames())
            {
                if (strRNum == softReg.GetRNum())
                {
                    Btn_StartMonitor_Click(null, null);
                    return;
                }
            }

            MessageBox.Show("此软件未注册!");

            Register RegisterForm = new Register();
            RegisterForm.ShowDialog();
            retkey = Registry.CurrentUser.OpenSubKey("SOFTWARE", true).CreateSubKey("mySoftWare").CreateSubKey("Register.INI");
            foreach (string strRNum in retkey.GetSubKeyNames())
            {
                if (strRNum == softReg.GetRNum())
                {
                    return;
                }
            }

            Environment.Exit(0);



            /*
               int animatetype = 4;
               Random a = new Random();
               int dwFlags = (int)a.Next(animatetype);
               int time = 300;
               switch (dwFlags)
               {

                   case 1://从上到下显示
                       AnimateWindow(Handle, time, AW_VER_POSITIVE);
                       break;
                   case 2://从下到上显示
                       AnimateWindow(Handle, time, AW_VER_NEGATIVE);
                       break;
                  default://透明渐变显示
                       AnimateWindow(Handle, time, AW_BLEND);
                       break;
                   case 3://从中间向四周
                       AnimateWindow(Handle, time, AW_CENTER);
                       break;
               }
               */

        }

        private void Pbox_Brand_Click(object sender, EventArgs e)
        {

        }

        private void 查看历史数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PanForQuery.BringToFront();
        }

        private void 螺丝枪参数BindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();

        }

        private int SearchDB(Form_DateSelect sender, DateTime[] DT_Array)
        {
            DGV_Query.DataSource = null;

            System.Data.DataTable table = sqlDataBase.Query(DT_Array);
            int MessageAmount = table.Rows.Count;
            if (MessageAmount < 1)
            {
                sender.HidePanForWait();
                MessageBox.Show("没有找到相关数据，请确认日期是否有误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //sender.wait();
                sender.Close();
                DGV_Query.DataSource = table;
                foreach (DataGridViewRow dgvr in DGV_Query.Rows)//遍历所有行
                {
                    if (dgvr.Cells[8].Value != null)
                    {

                        if (dgvr.Cells[8].Value.ToString().StartsWith("NOK"))
                        {
                            //Debug.WriteLine(dgvr.Cells[8].Value.ToString());
                            dgvr.DefaultCellStyle.BackColor = Color.Red;
                        }
                    }
                }

                // DGV_Query.AutoResizeColumns();
                MessageBox.Show("成功查询到" + MessageAmount + "条数据");
            }
            return MessageAmount;
        }




        private void Btn_LoadData_Click(object sender, EventArgs e)
        {
            if (sqlDataBase.InialCon())
            {
                Form_DateSelect FormDS = new Form_DateSelect();
                FormDS.CloseForm += new Form_DateSelect.ShowTable(SearchDB);
                FormDS.ShowDialog();
                sqlDataBase.Disconn();
            }
            else
            {
                MessageBox.Show("查询时连接数据库失败！请联系技术人员处理", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }





        private void Btn_Input_Click(object sender, EventArgs e)
        {
            Btn_Input.Enabled = false;
            if (Btn_Input.Text == "插入数据")
            {
                Btn_Input.Text = "停止";
                sqlDataBase.InialCon();
                Btn_Input.Enabled = true;
                cts = new CancellationTokenSource();
                SD = ScrewData.GenerateSD(data);
                Thread_InputData = new Thread(InputData);
                Thread_InputData.Start();

            }
            else
            {
                Btn_Input.Text = "插入数据";
                cts.Cancel();
                Application.DoEvents();
                sqlDataBase.Disconn();

                //if (sqlDataBase.sqlConnection.State == ConnectionState.Open)
                // { }
                // else { sqlDataBase.InialCon(); }
                DeleEntity_Text = new Dele_vv_Form(CloseSql);
                Thread thread_CloseSql = new Thread(CloseSql);
                thread_CloseSql.Start();
            }
        }

        private void CloseSql()

        {
            string text = sqlDataBase.Amount().ToString();

            if (this.sqlDataAmount.InvokeRequired)
            { this.Invoke(DeleEntity_Text); }
            else
            {
                sqlDataAmount.Text = text;
                Btn_Input.Enabled = true;
                Application.DoEvents();
            }
        }



        private void InputData()
        {
            while (!cts.Token.IsCancellationRequested)
            {
                sqlDataBase.Add(SD);
            }

        }


        private void Btn_Export_Click(object sender, EventArgs e)
        {
            if (DGV_Query.Rows.Count > 1e+5)
            { MessageBox.Show("数据量超过10万条，不建议直接导出，请缩短查询范围"); }
            else
            {
                sqlDataBase.InialCon();
                ExportToExcel excel = new ExportToExcel();
                excel.OutputAsExcelFile(DGV_Query);
                sqlDataBase.Disconn();
            }
        }



        private void 帮助ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PanForHelp.BringToFront();
        }


        private void ShowPanForSU()
        {
            Btn_InitDB.Visible = true;
            PanForSU.Visible = true;
            Btn_Test.Text = "结束调试";
            Application.DoEvents();
            OpenSqlConn();
            sqlDataAmount.Text = sqlDataBase.Amount().ToString();
            sqlDataBase.Disconn();
            SelectPort.Enabled = true;
            SelectPort_Left.Enabled = true;
            // Application.DoEvents();
        }

        private void Btn_Test_Click(object sender, EventArgs e)
        {


            if (Btn_Test.Text == "软件调试")
            {

                PassWord pb = new PassWord();
                pb.SU += new PassWord.ShowSuperUser(ShowPanForSU);
                pb.InitDB += new PassWord.ShowSuperUser(ShowBtn_InitDB);
                pb.ShowDialog();
            }

            else
            {
                Btn_Test.Text = "软件调试";
                PanForSU.Visible = false;
                SelectPort.Enabled = false;
                SelectPort_Left.Enabled = false;
                Btn_InitDB.Visible = false;
            }
        }
        private void ShowBtn_InitDB()
        {
           
            Btn_InitDB.BackColor = Color.Red;
            Btn_InitDB.Visible = true;
        }

        private void ClearSqlDB_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("此操作会清空全部数据！请确认是否继续", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                OpenSqlConn();
                sqlDataBase.ClearSql();

                sqlDataAmount.Text = sqlDataBase.Amount().ToString();
                sqlDataBase.Disconn();

            }

        }

        private void Btn_StartServer_Click(object sender, EventArgs e)
        {

            string myServiceName = "MSSQL$SQLEXPRESS";
            string status; //service status (For example, Running or Stopped)

            //  Console.WriteLine("Service: " + myServiceName);

            //display service status: For example, Running, Stopped, or Paused
            ServiceController mySC = new ServiceController(myServiceName);
            mySC.MachineName = Dns.GetHostName();

            try
            {
                status = mySC.Status.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Service not found. It is probably not installed. [exception=" + ex.Message + "]");
                return;
            }
            //display service status: For example, Running, Stopped, or Paused
            //  Console.WriteLine("Service status : " + status);

            //if service is Stopped or StopPending, you can run it with the following code.

            if (mySC.Status.Equals(ServiceControllerStatus.Stopped) | mySC.Status.Equals(ServiceControllerStatus.StopPending))
            {
                try
                {
                    mySC.Start();
                    mySC.WaitForStatus(ServiceControllerStatus.Running);
                    MessageBox.Show("The service is now " + mySC.Status.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("shit" + ex.Message);
                }

            }

            return;

        }

        private void SqlBackUp_Click(object sender, EventArgs e)
        {


            if (sqlDataBase.BackUp())
                MessageBox.Show("备份数据成功!");
            else
            {
                MessageBox.Show("备份失败！");

            }

        }

        private void Btn_RestoreSql_Click(object sender, EventArgs e)
        {


            if (sqlDataBase.RestoreSql())
            {
                OpenSqlConn();
                sqlDataAmount.Text = sqlDataBase.Amount().ToString();
                sqlDataBase.Disconn();
                MessageBox.Show("备份数据成功!");
            }
            else
            {
                MessageBox.Show("没有成功进行备份！");

            }
            Btn_InitDB.Visible = false;
        }

        private void 数据库备份ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Thread SelfBack = new Thread(SelfBackUp);
            SelfBack.Start();
        }

        private void SelfBackUp()
        {
            if (sqlDataBase.BackUp())
                MessageBox.Show("备份数据成功!");
            else
            {
                MessageBox.Show("没有成功进行备份！");

            }

        }


        private void OpenSqlConn()
        {
            if (sqlDataBase.sqlConnection.State == ConnectionState.Open)
            { }
            else
            { sqlDataBase.InialCon(); }

        }
        private void CloseSqlConn()
        {
            if (sqlDataBase.sqlConnection.State == ConnectionState.Open)
            { sqlDataBase.Disconn(); }
            else
            { }

        }

        private void Btn_SelectPath_Click(object sender, EventArgs e)
        {

        }

        private void Btn_OpenPath_Click(object sender, EventArgs e)
        {
            string path = ConfigurationManager.AppSettings["BackUpPath"];
            Process.Start(path);
        }

        private void PanForSet_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CB_SetBackUp_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_SetBackUp.Checked == true)
            {
                Combo_BackPeriod.Enabled = true;
                Timer_BackUp.Enabled = true;
                TB_Date.Enabled = true;
                TB_RecentBackTime.Enabled = true;
            }
            else
            {
                Combo_BackPeriod.Enabled = false;
                Timer_BackUp.Enabled = false;
                TB_Date.Enabled = false;
                TB_RecentBackTime.Enabled = false;
            }
        }

        private void Combo_BackPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (Combo_BackPeriod.SelectedIndex)
            {
                case 0: sqlDataBase.BackUpPeriod = Period.Month; break;
                case 1: sqlDataBase.BackUpPeriod = Period.Season; break;
                case 2: sqlDataBase.BackUpPeriod = Period.HalfYear; break;
                case 3: sqlDataBase.BackUpPeriod = Period.Year; break;
            }

        }

        private void Timer_BackUp_Tick(object sender, EventArgs e)
        {
            string BackUpTime = ConfigurationManager.AppSettings["LastBackUpTime"];
            BackTime = Convert.ToDateTime(BackUpTime);
            Current = DateTime.Today;
            TB_RecentBackTime.Text = BackUpTime;

            TimeSpan span = Current.Subtract(BackTime);
            int rest = (int)(sqlDataBase.BackUpTimeSpan - span.TotalDays);
            if (rest <= 0)
            {
                Timer_BackUp.Enabled = false;
                TB_Date.Text = "正在备份";
                TB_Date.BackColor = DefaultBackColor;
                Thread StartBackUp = new Thread(AutoBackUp);
                StartBackUp.Start();
                Debug.WriteLine("开始自动备份");
            }
            else
            {
                TB_Date.Text = rest.ToString() + "天";
                TB_Date.BackColor = DefaultBackColor;
            }



        }
        private void AutoBackUp()
        {

            string BackUpTime = Current.ToShortDateString();

            if (sqlDataBase.AutoBackUp() == true)
            {
                Config.Modify("LastBackUpTime", BackUpTime);

                //Debug.WriteLine("备份成功");
                Dele_vv_Form EnableTimer = new Dele_vv_Form(EnableBackTimer);
                this.Invoke(EnableTimer);
            }
            else
            {
                Dele_vv_Form backupError = new Dele_vv_Form(TimerError);
                this.Invoke(backupError);
            }

        }
        private void TimerError()
        {
            TB_Date.Text = "备份失败!";
            TB_Date.BackColor = Color.Red;
            MessageBox.Show("自动备份失败！请联系技术人员修复");
        }
        private void EnableBackTimer()
        {
            this.Timer_BackUp.Enabled = true;
        }

        private void CB_AutoStart_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_AutoStart.Checked) //设置开机自启动  
            {

                string path = Application.ExecutablePath;

                RegistryKey RKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                RKey.SetValue("ChaoYanIpc", path);
                RKey.Close();
            }
            else //取消开机自启动  
            {
                string path = Application.ExecutablePath;
                RegistryKey RKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                RKey.DeleteValue("ChaoYanIpc", false);
                RKey.Close();
            }
        }

        private void label_BackUpPath_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Register_Click(object sender, EventArgs e)
        {

            Register RegisterForm = new Register();
            RegisterForm.ShowDialog();


        }

        private void SelectPort_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                port.PortName = SelectPort.SelectedItem.ToString();
                Config.Modify("COM_RIGHT", port.PortName);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

            if (port_left.PortName == port.PortName)
            { MessageBox.Show("两个控制器端口不能相同！请重新设置", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void SelectPort_left_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                port_left.PortName = SelectPort_Left.SelectedItem.ToString();
                Config.Modify("COM_LEFT", port_left.PortName);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

            if (port_left.PortName == port.PortName)
            { MessageBox.Show("两个控制器端口不能相同！请重新设置", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }


        }

        private void DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SelectPort_MouseClick(object sender, MouseEventArgs e)
        {
            if (port.IsOpen)
            {
                MessageBox.Show("请先关闭数据监控后再设置端口！");
                PanForDisp.BringToFront();
            }
        }

        private void SelectPort_Left_MouseClick(object sender, MouseEventArgs e)
        {
            if (port.IsOpen)
            {
                MessageBox.Show("请先关闭数据监控后再设置端口！");
                PanForDisp.BringToFront();
            }
        }

        private void Btn_Reset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("此操作将会恢复软件原始的设置", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                string ComLeft = ConfigurationManager.AppSettings["COM_LEFT1"];
                Config.Modify("COM_LEFT", ComLeft);


                string ComRight = ConfigurationManager.AppSettings["COM_RIGHT1"];
                Config.Modify("COM_RIGHT", ComRight);

                int cnt = 0;
                port.PortName = ConfigurationManager.AppSettings["COM_RIGHT"];

                port_left.PortName = ConfigurationManager.AppSettings["COM_LEFT"];
                foreach (string items in SelectPort.Items)
                {
                    if (items == port.PortName)
                    {
                        SelectPort.SelectedIndex = cnt;
                    }
                    cnt++;
                }
                cnt = 0;
                foreach (string items in SelectPort_Left.Items)
                {
                    if (items == port_left.PortName)
                    {
                        SelectPort_Left.SelectedIndex = cnt;
                    }
                    cnt++;

                }



                string Path = ConfigurationManager.AppSettings["BackUpPath1"];
                Config.Modify("BackUpPath", Path);
                label_BackUpPath.Text = ConfigurationManager.AppSettings["BackUpPath"];

                CB_AutoStart.Checked = true;
                CB_SetBackUp.Checked = true;
                Combo_BackPeriod.SelectedIndex = 1;

                string Type = ConfigurationManager.AppSettings["Type01backup"];
                Config.Modify("Type01", Type);

                Type = ConfigurationManager.AppSettings["Type02backup"];
                Config.Modify("Type02", Type);

                Type = ConfigurationManager.AppSettings["Type03backup"];
                Config.Modify("Type03", Type);

                Type = ConfigurationManager.AppSettings["Type04backup"];
                Config.Modify("Type04", Type);

                Type = ConfigurationManager.AppSettings["Type05backup"];
                Config.Modify("Type05", Type);

                Type = ConfigurationManager.AppSettings["Type06backup"];
                Config.Modify("Type06", Type);

                Type = ConfigurationManager.AppSettings["Type07backup"];
                Config.Modify("Type07", Type);

                Type = ConfigurationManager.AppSettings["Type08backup"];
                Config.Modify("Type08", Type);


                if (Combo_TypeNum.SelectedIndex == 0)
                { TB_TypeDef.Text = ConfigurationManager.AppSettings["Type01backup"]; }
                else
                    Combo_TypeNum.SelectedIndex = 0;
            }
        }

        private void Btn_SetPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择保存路径";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                Config.Modify("BackUpPath", foldPath);
            }
            label_BackUpPath.Text = ConfigurationManager.AppSettings["BackUpPath"];
        }

        private void 数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PanForQuery.BringToFront();
        }

        private void 手动备份数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread SelfBack = new Thread(SelfBackUp);
            SelfBack.Start();
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void TB_TypeDef_TextChanged(object sender, EventArgs e)
        {

            switch (Combo_TypeNum.SelectedItem.ToString())
            {
                case "01": Config.Modify("Type01", TB_TypeDef.Text); break;
                case "02": Config.Modify("Type02", TB_TypeDef.Text); break;
                case "03": Config.Modify("Type03", TB_TypeDef.Text); break;
                case "04": Config.Modify("Type04", TB_TypeDef.Text); break;
                case "05": Config.Modify("Type05", TB_TypeDef.Text); break;
                case "06": Config.Modify("Type06", TB_TypeDef.Text); break;
                case "07": Config.Modify("Type07", TB_TypeDef.Text); break;
                case "08": Config.Modify("Type08", TB_TypeDef.Text); break;
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void Combo_TypeNumChanged(object sender, EventArgs e)
        {
            switch (Combo_TypeNum.SelectedItem.ToString())
            {
                case "01": TB_TypeDef.Text = ConfigurationManager.AppSettings["Type01"]; break;
                case "02": TB_TypeDef.Text = ConfigurationManager.AppSettings["Type02"]; break;
                case "03": TB_TypeDef.Text = ConfigurationManager.AppSettings["Type03"]; break;
                case "04": TB_TypeDef.Text = ConfigurationManager.AppSettings["Type04"]; break;
                case "05": TB_TypeDef.Text = ConfigurationManager.AppSettings["Type05"]; break;
                case "06": TB_TypeDef.Text = ConfigurationManager.AppSettings["Type06"]; break;
                case "07": TB_TypeDef.Text = ConfigurationManager.AppSettings["Type07"]; break;
                case "08": TB_TypeDef.Text = ConfigurationManager.AppSettings["Type08"]; break;
            }



        }

        private void DGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==9)
            {
                if(this.DGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value==null)
                { return; }
                string content = this.DGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                string tip1 = "";
                string tip2 = "";
             
          if(content[0]=='A')
                {
                    MessageBox.Show("作业结果：正常");
                    return;
                }
                
                        
           else if(content[0]=='R')
                {
                    switch(content[1])
                    {
                        case 'T': tip1= "错误提示:\r1.作业循环结束时的最终扭矩大于最大扭矩容限\r"; break;
                        case 't':tip1= "错误提示:\r1.作业循环结束时的最终扭矩小于最小扭矩容限\r"; break;
                        case 'a':tip1 = "错误提示:\r1.作业循环结束时的最终转角小于最小转角容限\r"; break;
                        case 'A': tip1 = "错误提示:\r1.作业循环结束时的最终转角大于最大转角容限\r"; break;
                    }
                    switch (content[2])
                    {
                       
                        case 't': tip2 = "2.循环停止，是因为该阶段或循环设置的时间已结束，而不是目标参数的原因。必须对编程设置的运行时长进行修改，以适应作业应用的需要"; break;

                    }
                    if (tip1 + tip2 == "")
                    {
                        MessageBox.Show("具体错误信息请查阅法雷奥控制器手册");
                        return;
                    }

                    MessageBox.Show(tip1 + tip2);


                }
            


            }
        }

        private void DGV_Query_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 9)
            {
                if (this.DGV_Query.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                { return; }
                string content = this.DGV_Query.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                string tip1 = "";
                string tip2 = "";

                if (content[0] == 'A')
                {
                    MessageBox.Show("作业结果：正常");
                    return;
                }


                else if (content[0] == 'R')
                {
                    switch (content[1])
                    {
                        case 'T': tip1 = "错误提示:\r1.作业循环结束时的最终扭矩大于最大扭矩容限\r"; break;
                        case 't': tip1 = "错误提示:\r1.作业循环结束时的最终扭矩小于最小扭矩容限\r"; break;
                        case 'a': tip1 = "错误提示:\r1.作业循环结束时的最终转角小于最小转角容限\r"; break;
                        case 'A': tip1 = "错误提示:\r1.作业循环结束时的最终转角大于最大转角容限\r"; break;
                    }
                    switch (content[2])
                    {

                        case 't': tip2 = "2.循环停止，是因为该阶段或循环设置的时间已结束，而不是目标参数的原因。必须对编程设置的运行时长进行修改，以适应作业应用的需要"; break;

                    }
                    if(tip1+tip2=="")
                    {
                        MessageBox.Show("具体错误信息请查阅法雷奥控制器手册");
                        return;
                    }
           
                    MessageBox.Show(tip1 + tip2);


                }



            }
        }

        private void DGV_Resize(object sender, EventArgs e)
        {
                  
            DGV.Left = (this.Width - DGV.Width) / 2;


    }

        private void Ipc_Resize(object sender, EventArgs e)
        {
            PanForDisp.Left = (this.Width - PanForDisp.Width) / 2;
            PanForDisp.Top = (this.Height - PanForDisp.Height) / 2;


            PanForSet.Left = (this.Width - PanForSet.Width) / 2;
            PanForSet.Top = (this.Height - PanForSet.Height) / 2;

            PanForHelp.Left = (this.Width - PanForHelp.Width) / 2;
            PanForHelp.Top = (this.Height - PanForHelp.Height) / 2;

            PanForQuery.Left = (this.Width - PanForQuery.Width) / 2;
            PanForQuery.Top = (this.Height - PanForQuery.Height) / 2;
        }
    }

}



