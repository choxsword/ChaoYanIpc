using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Configuration;
using System.Windows.Forms;

namespace ChaoYanIpc
{
    class Config
    {
       public static void UpdateConfig(string AppKey, string KeyValue)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Application.ExecutablePath + ".config");//获取当前配置文件  
            XmlNode node = doc.SelectSingleNode(@"//add[@key='" + AppKey + "']");
            XmlElement ele = (XmlElement)node;
            ele.SetAttribute("value", KeyValue);
            doc.Save(Application.ExecutablePath + ".config");
            ConfigurationManager.RefreshSection("appSettings");
            //上面句很重要,强制程序重新获取配置文件中appSettings节点中所有的值,否则更改后要到程序关闭后才更新,因为程序启动后默认不再重新获取.  
        }


        public static void Modify(string AppKey, string KeyValue)
        {
            //获取Configuration对象
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //根据Key读取元素的Value
            string name = config.AppSettings.Settings[AppKey].Value;
            //写入元素的Value
            config.AppSettings.Settings[AppKey].Value = KeyValue;
            //增加元素
            //删除元素
           // config.AppSettings.Settings.Remove(AppKey);
            //一定要记得保存，写不带参数的config.Save()也可以
            config.Save(ConfigurationSaveMode.Modified);
            //刷新，否则程序读取的还是之前的值（可能已装入内存）
            ConfigurationManager.RefreshSection("appSettings");
        }



        public static string ReadConfig(string item)
        {
            string result = "0";
            return result;
        }


    }
}
