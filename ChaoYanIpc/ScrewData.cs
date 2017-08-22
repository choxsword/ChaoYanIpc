using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Configuration;

namespace ChaoYanIpc
{
    public class ScrewData
    {
        private string _Date = null;
        private string _Time = null;
        private string _AxisNum = null;
        private string _CircleNum = null;
        private string _StepNum = null;
        private string _Torque = null;
        private string _Theta = null;
        private string _Rate = null;
        private string _IsOK = null;
        private string _Type = null;
        private string _Remark = null;//备用属性
        private string _ReadValue = null;
        private string _Result = null;
        private string _MachineNum = null;

        #region 属性

        public string Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        public string Time
        {
            get { return _Time; }
            set { _Time = value; }
        }
        public string AxisNum
        {
            get { return _AxisNum; }
            set { _AxisNum = value; }
        }
        public string MachineNum
        {
            get { return _MachineNum; }
            set {
                _MachineNum = value;
            }
        }

        public string CircleNum
        {
            get { return _CircleNum; }
            set {
                switch(value)
                {
                    case "01":_CircleNum= ConfigurationManager.AppSettings["Type01"];break;
                    case "02": _CircleNum = ConfigurationManager.AppSettings["Type02"]; break;
                    case "03": _CircleNum = ConfigurationManager.AppSettings["Type03"]; break;
                    case "04": _CircleNum = ConfigurationManager.AppSettings["Type04"]; break;
                    case "05": _CircleNum = ConfigurationManager.AppSettings["Type05"]; break;
                    case "06": _CircleNum = ConfigurationManager.AppSettings["Type06"]; break;
                    case "07": _CircleNum = ConfigurationManager.AppSettings["Type07"]; break;
                    case "08": _CircleNum = ConfigurationManager.AppSettings["Type08"]; break;
                }
                _Type = _CircleNum;
            }
        }
        public string StepNum
        {
            get { return _StepNum; }
            set { _StepNum = value; }
        }
        public string Result
        {
            get { return _Result; }
            set
            {
                _Result = value;

                if (value.StartsWith("A"))
                {
                    _IsOK = "OK";

                }
                else if (value.StartsWith("R"))
                {
                    _IsOK = "NOK";
                }
            }
        }
        public string Torque
        {
            get { return _Torque; }
            set { _Torque = value; }
        }
        public string Theta
        {
            get { return _Theta; }
            set { _Theta = value; }
        }
        public string Rate
        {
            get { return _Rate; }
            set { _Rate = value; }
        }


        public string IsOK
        {
            get { return _IsOK; }
            set { _IsOK = value; }
        }

        public string Type
        {
            get { return _Type; }
            set
            { _Type = value; }
        }
        public string Remark
        {
            get { return _Remark; }
            set
            { _Remark = value; }
        }
        public string ReadValue
        {
            get { return _ReadValue; }
            set { _ReadValue = value; }
        }

        #endregion

        #region 方法

        //由数组生成ScrewData 数据
        public static ScrewData GenerateSD(string[] input)
        {
            ScrewData data = new ScrewData();


            data.Date = input[0];
            data.Time = input[1];
            data.AxisNum = input[2];
            data.CircleNum = input[3];
            data.StepNum = input[4];
            data.Torque = input[5];
            data.Theta = input[6];
            data.Rate = input[7];
            return data;
        }
        #endregion

    }
}
