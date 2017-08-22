using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using System.Configuration;
using System.IO;
using System.Threading;

namespace ChaoYanIpc
{
    public enum Period { Month, Season, HalfYear, Year };
    public class SqlDB
    {
        private string Adress;
        public SqlConnection sqlConnection;
        private string StrConnection;
        private SqlDataAdapter accAdapter;
        private SqlCommand accCommand;
        public delegate void SqlError();
        public SqlError SqlIsError;
        private Period _BackUpPeriod;
        public double BackUpTimeSpan = 90;
        private string ComputerName;
        public SqlDB()
        {
            ComputerName = Environment.MachineName;
            StrConnection = "Server=" + ComputerName + "\\SQLEXPRESS;" + "initial catalog=ChaoYanIpc;" + "Integrated Security = SSPI";
            sqlConnection = new SqlConnection(StrConnection);
            BackUpPeriod = Period.Season;
        }
        //连接数据库
        public Period BackUpPeriod
        {
            get
            {
                return _BackUpPeriod;
            }

            set
            {
                _BackUpPeriod = value;
                switch (value)
                {
                    case Period.Month: BackUpTimeSpan = 30;break;
                    case Period.Season: BackUpTimeSpan = 90; break;
                    case Period.HalfYear: BackUpTimeSpan = 180; break;
                    case Period.Year: BackUpTimeSpan = 365; break;
                }
            }
        }
        public bool InialCon()
        {
            bool IsOK = false;
            if (sqlConnection.State !=ConnectionState.Open)
            {
                try
                {
                    sqlConnection.Open();
                    IsOK = true;
                    Debug.WriteLine("已开启");
                }
                catch (SqlException e)
                {
                    // MessageBox.Show("连接SQL数据库失败,请在设置中启动数据库！");
                    MessageBox.Show(e.Message);
                    //SqlIsError();
                }

                catch (InvalidOperationException e)
                { MessageBox.Show(e.Message); }

            }
            else
            { IsOK = true; }
            return IsOK;
            //return sqlConnection;
        }
        public void Disconn()
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                try
                {
                    sqlConnection.Close();
                    Debug.WriteLine("已关闭");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }





        private void CloseSql()
        {
           

        }

        #region 数据库操作
        public int Add(ScrewData Screw)
        {
            int result = 0;
            accCommand = new SqlCommand();
            accCommand.Connection = sqlConnection;
            accCommand.CommandType = CommandType.Text;
            /*
            DateTime date =Convert.ToDateTime( Screw.Date);
            DateTime time = Convert.ToDateTime(Screw.Time);
            Screw.Date = date.ToShortDateString();
            Screw.Time = time.ToShortTimeString();
            */
            string SQLstr = "";
            SQLstr = "insert into DefaultTable(日期,时间,控制器,型号,数量,作业阶段,扭矩,转角,拧紧结果,报告) values('";
            SQLstr += Screw.Date + "','" +Screw.Time+"','"+ Screw.MachineNum + "','" + Screw.Type + "','" + Screw.ReadValue + "','" + Screw.StepNum + "','" + Screw.Torque + "','" + Screw.Theta +  "','" + Screw.IsOK + "','" + Screw.Result + "')";
            //Debug.WriteLine(SQLstr);
            accCommand.CommandText = SQLstr;
            if (sqlConnection.State != ConnectionState.Open)
            { InialCon(); }
                try
                {
                    result = accCommand.ExecuteNonQuery();
                    // Debug.WriteLine(DateTime.Now);
                }
                catch (Exception e)
                { MessageBox.Show("卧槽"+e.Message); }
                accCommand.Dispose();
            return result;
        }

        public DataTable Query(DateTime[] DT_Array)
        {
            accAdapter = new SqlDataAdapter();
            accCommand = new SqlCommand();
            accCommand.Connection = sqlConnection;
            accCommand.CommandType = CommandType.Text;
            DataTable accDataTable = new DataTable();
            string SQLstr = "";

            string Date_Start = DT_Array[0].ToShortDateString();
            string Date_End = DT_Array[1].ToShortDateString();
            string Time_Start = DT_Array[2].ToShortTimeString();
            string Time_End = DT_Array[3].ToShortTimeString();

            SQLstr = "SELECT 日期,时间,控制器,型号,数量,作业阶段,扭矩,转角,拧紧结果,报告 FROM DefaultTable ";
            // cmdString = "SELECT * FROM 螺丝枪参数 ";
            //  SQLstr += "WHERE " + "日期" + "=@para";
            SQLstr += string.Format("WHERE 日期>='{0}' and 日期<='{1}' and 时间>='{2}' and 时间<='{3}'", Date_Start, Date_End, Time_Start, Time_End);
            accCommand.CommandText = SQLstr;
            // accCommand.Parameters.AddWithValue("@para1", Item);
            // accCommand.Parameters.AddWithValue("@para", start);
            accAdapter.SelectCommand = accCommand;
            accAdapter.Fill(accDataTable);
            return accDataTable;
        }

        public Int64 Amount()
        {
            Int64 amount = 101010;
            string SQLstr = "";
            SQLstr = "SELECT count(*) FROM DefaultTable ";
           string StrConnection1 = "Server=" + ComputerName + "\\SQLEXPRESS;" + "initial catalog=ChaoYanIpc;" + "Integrated Security = SSPI";
          SqlConnection  sqlConnection1 = new SqlConnection(StrConnection1);
            try
            {
                sqlConnection1.Open();

            }
            catch (SqlException e)
            {
                MessageBox.Show("读取数据量时连接SQL数据库失败,请在设置中启动数据库！");
            }


            if (sqlConnection1.State == ConnectionState.Open)
            {

                SqlCommand cmd = new SqlCommand(SQLstr, sqlConnection1);
                cmd.CommandTimeout = 60 * 1000;
                try
                {
                    amount = Convert.ToInt64(cmd.ExecuteScalar());
                    cmd.Dispose();
                }
                catch (SqlException e)
                {
                    MessageBox.Show("shit" + e.Message);
                    cmd.Dispose();

                }
                sqlConnection1.Close();

            }

            return amount;
        }

        public void ClearSql()
        {
            accCommand = new SqlCommand();
            String SQLstr = "delete FROM DefaultTable ";
            accCommand.Connection = sqlConnection;
            accCommand.CommandType = CommandType.Text;
            accCommand.CommandText = SQLstr;
            accCommand.CommandTimeout = 10 * 60000;
            try
            { int result = accCommand.ExecuteNonQuery(); }
            catch (Exception e)
            { MessageBox.Show(e.Message); }
        }
        public bool BackUp()
        {
            bool IsBackedUp = false;
            string FilePath = ConfigurationManager.AppSettings["BackUpPath"];
            if (Directory.Exists(FilePath) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(FilePath);
            }

            /*
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择备份路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = dialog.SelectedPath;
            }
            */

            if (MessageBox.Show("建议使用自动备份！您确认要手动备份数据库？会备份到默认路径，\r如需更改默认备份路径请到设置中进行更改", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                SqlCommand acc = new SqlCommand();
                SqlConnection sqlConnForBackUp = new SqlConnection(StrConnection);

                try { sqlConnForBackUp.Open(); }

                catch (SqlException e)
                {
                    MessageBox.Show("备份数据库连接-" + e.Message);
                    //MessageBox.Show(e.Message);
                    return IsBackedUp;
                }

                string SQLstr = "BACKUP DATABASE " + "ChaoYanIpc" + " TO DISK = '" + FilePath + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".bak'";
                acc.Connection = sqlConnForBackUp;
                acc.CommandType = CommandType.Text;
                acc.CommandText = SQLstr;
                ProgramBar PB = new ProgramBar();
                PB.SetText("正在保存数据库，请稍候后...");
                PB.Show();
                Application.DoEvents();
                try
                {
                    int result = acc.ExecuteNonQuery();
                    IsBackedUp = true;
                    string BackUpTime = DateTime.Today.ToShortDateString();
                        Config.Modify("LastBackUpTime", BackUpTime);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                PB.Close();
                Application.DoEvents();
            }

          

            return IsBackedUp;

        }
        public bool AutoBackUp()
        {

            bool IsBackedUp = false;
            string FilePath = ConfigurationManager.AppSettings["BackUpPath"];
            if (Directory.Exists(FilePath) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(FilePath);
            }

                SqlCommand acc = new SqlCommand();
                SqlConnection sqlConnForBackUp = new SqlConnection(StrConnection);

                try { sqlConnForBackUp.Open(); }

                catch (SqlException e)
                {
                    MessageBox.Show("自动备份数据库连接失效" + e.Message);
                    //MessageBox.Show(e.Message);
                    return IsBackedUp;
                }

                string SQLstr = "BACKUP DATABASE " + "ChaoYanIpc" + " TO DISK = '" + FilePath + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".bak'";
                acc.Connection = sqlConnForBackUp;
                acc.CommandType = CommandType.Text;
                acc.CommandText = SQLstr;

                try
                {
                    int result = acc.ExecuteNonQuery();
                    IsBackedUp = true;
                }
                catch (Exception e)
                {
                    MessageBox.Show("自动备份失败,请选择手动备份"+e.Message);
                }
                Application.DoEvents();
      return IsBackedUp;

        }
        public bool RestoreSql()
        {
            ProgramBar PB = new ProgramBar();
       
           // sqlConnection.Dispose();
            string FilePath;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "BAK备份文件(*.bak)|*.bak";

            bool IsRestored = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                PB.SetText("初始化中,请耐心等待");
                PB.Show();
                Application.DoEvents();
                FilePath = dialog.FileName;
                string constr = "Server=" + ComputerName + "\\SQLEXPRESS;" + "initial catalog=master;" + "Integrated Security = SSPI";

                SqlConnection con = new SqlConnection(constr);
                accCommand = new SqlCommand();
                accCommand.CommandTimeout = 5 * 60 * 1000;
                PB.SetText("还原前将自动备份当前数据库...");
                Application.DoEvents();
                if (!AutoBackUp())
                {
                    if (MessageBox.Show("自动备份数据库异常，是否继续恢复？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    {
                        PB.Close();
                        return IsRestored;
                    }
                }


                    try
                    { con.Open(); }
                    catch (Exception e)
                    {
                        MessageBox.Show("开启失败" + e.Message);
                        PB.Close();
                        return IsRestored;
                    }

                    accCommand.Connection = con;
                    string database = "ChaoYanIpc";
                    string BACKUP = String.Format("USE MASTER RESTORE DATABASE {0} FROM DISK = '{1}'", database, FilePath);


                    PB.SetText("正在重新连接数据库...");
                    Application.DoEvents();

                    string ALTER = "ALTER DATABASE " + database + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
                    accCommand.CommandText = ALTER;
                    try
                    {
                        accCommand.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        // MessageBox.Show(e.Message);

                        accCommand.CommandText = BACKUP;

                        PB.SetText("开始还原,请耐心等待2分钟左右...");
                        Application.DoEvents();
                        try
                        {
                            accCommand.ExecuteNonQuery();
                            IsRestored = true;
                            PB.Close();
                            return IsRestored;

                        }
                        catch (Exception error)
                        {

                            PB.Close();
                            MessageBox.Show(error.Message);
                            return IsRestored;
                        }
                    }


                    accCommand.CommandText = BACKUP;

                    PB.SetText("开始还原,请耐心等待2分钟左右...");
                    Application.DoEvents();
                    try
                    {
                        accCommand.ExecuteNonQuery();
                        IsRestored = true;

                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                        PB.Close();
                        return IsRestored;
                    }
                    PB.Close();
                    return IsRestored;
                

            }
            return IsRestored;
           
        }
        #endregion


    }


}

