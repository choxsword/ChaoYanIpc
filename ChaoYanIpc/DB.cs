using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data;


namespace ChaoYanIpc
{
    class DB
    {
        private string Adress;
        private OleDbConnection accConnection;
        private string StrConnection;
        private OleDbDataAdapter accAdapter;
        private OleDbCommand accCommand;
        public DB()
        {
            Adress = "C:\\Users\\choxsword\\Documents\\Visual Studio 2015\\Projects\\ChaoYanIpc\\DataBase\\test.accdb";
            StrConnection = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Adress;
           accConnection= InialCon();
        }
        //连接数据库
        public OleDbConnection InialCon()
        {
            accConnection = new OleDbConnection(StrConnection);
            try { accConnection.Open(); }
            catch (OleDbException e)
            {
                MessageBox.Show("连接数据库失败");
                MessageBox.Show(e.Message);
            }
            catch (InvalidOperationException e)
            { MessageBox.Show(e.Message); }
            if (accConnection.State != ConnectionState.Open)
            {
                MessageBox.Show("导入数据库失败");
            }

            return accConnection;
        }

        #region 数据库操作
        public int Add(ScrewData Screw)
        {
            accCommand = new OleDbCommand();
            accCommand.Connection = accConnection;
            accCommand.CommandType = CommandType.Text;
            string SQLstr = "";
            SQLstr = "insert into 螺丝枪参数(日期,时间,作业轴号,作业循环号,作业阶段,扭矩Nm,转角°,扭矩率) values('";
            SQLstr += Screw.Date + "','" + Screw.Time + "','" + Screw.AxisNum + "','" + Screw.CircleNum + "','" + Screw.StepNum + "','" + Screw.Torque + "','" + Screw.Theta + "','" + Screw.Rate + "')";
            //  Debug.WriteLine(SQLstr);
            accCommand.CommandText = SQLstr;
            int result = accCommand.ExecuteNonQuery();
           // accConnection.Close();
          //  accConnection.Dispose();
            return result;
        }
        public DataTable Query(string start, string end)
        {
            accAdapter = new OleDbDataAdapter();
            accCommand = new OleDbCommand();
            accCommand.Connection = accConnection;
            accCommand.CommandType = CommandType.Text;
            DataTable accDataTable = new DataTable();
            string SQLstr = "";
           SQLstr = "SELECT 日期,时间,作业轴号,作业循环号,作业阶段,扭矩Nm,转角°,扭矩率 FROM 螺丝枪参数 ";
            // cmdString = "SELECT * FROM 螺丝枪参数 ";
            SQLstr += "WHERE "+"日期"+"=@para";
            accCommand.CommandText = SQLstr;
           // accCommand.Parameters.AddWithValue("@para1", Item);
            accCommand.Parameters.AddWithValue("@para", start);
            accAdapter.SelectCommand = accCommand;
            accAdapter.Fill(accDataTable);
           
            return accDataTable;

        }


        #endregion
        public int Operation(ScrewData Screw, string method)
        {
            int result = 0;
            //实例化数据库类
            OleDbDataAdapter accAdapter = new OleDbDataAdapter();
            DataTable accDataTable = new DataTable();
            OleDbCommand accCommand = new OleDbCommand();


            string SQLstr = "";
            if (method == "add")
            {
                SQLstr = "insert into 螺丝枪参数(日期,时间,作业轴号,作业循环号,作业阶段,扭矩Nm,转角°,扭矩率) values('";

                SQLstr += Screw.Date + "','" + Screw.Time + "','" + Screw.AxisNum + "','" + Screw.CircleNum + "','" + Screw.StepNum + "','" + Screw.Torque + "','" + Screw.Theta + "','" + Screw.Rate + "')";
                Debug.WriteLine(SQLstr);

                /*
                if (method == "delete")
                {
                    SQLstr = "delete from 人员表 where UserName='" + myUserMain.name + "' and UserAge='" + myUserMain.age + "'";
                }

                if (method== "update")
                {
                    SQLstr = "update 人员表 set UserName='" + myUserMain.name + "',UserAge='" + myUserMain.age + "' where ID='" + myUserMain.ID + "'";
                }
                */

                OleDbCommand cmd = new OleDbCommand(SQLstr, accConnection);
                result = cmd.ExecuteNonQuery();
            }

            else if (method == "query")
            {
                OleDbCommand cmd = new OleDbCommand(SQLstr, accConnection);
                cmd.ExecuteReader();
            }

            accConnection.Close();
            accConnection.Dispose();
            return result;
        }



     
    }
}
