using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MySql.Data.MySqlClient;

namespace WSSession1
{
    internal class Mysql : IDatabase
    {
        public MySqlConnection MySqlConnection;

        // 无参构造
        public Mysql()
        {
        }
        // 有参构造
        //public Mysql(string Sql_Str)
        //{
        //    Sql_str = Sql_Str;
        //}

        /// <summary>
        /// Sql_str_1
        /// </summary>
        //private static string _sql_str;
        //public static string Sql_str
        //{
        //    get { return _sql_str; }
        //    set { _sql_str = value; }
        //}

        // 实现数据库接口 - 删除
        void IDatabase.DataBase_delete()
        {
            throw new NotImplementedException();
        }

        // 实现数据库接口 - 增加
        void IDatabase.DataBase_increase()
        {

        }

        void IDatabase.DataBase_increase(ref string Sql_str)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(Sql_str, MySqlConnection);
            Sql_str = Convert.ToString(mySqlCommand.ExecuteNonQuery()); // 插入true 返回执行数，false返回空值
        }

        // 实现数据库接口 - 查询
        void IDatabase.DataBase_Inquire(ref string Sql_str)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(Sql_str, MySqlConnection);
            Sql_str = Convert.ToString(mySqlCommand.ExecuteScalar());  // 查询true 返回对应值，false返回空值
        }

        void IDatabase.DataBase_Inquire(out DataSet Ds, ref string Sql_Str)
        {
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(Sql_Str, MySqlConnection);
            Ds = new DataSet();
            mySqlDataAdapter.Fill(Ds, "userdata");
        }

        // 实现数据库接口 - 连接
        void IDatabase.DataBase_link()
        {
            string SQL_ConnectStr = ""; // 储存mysql连接串
            XmlDocument xmlDocument = new XmlDocument(); // 开辟xml
            xmlDocument.Load("../../App.config"); // 获取app.config
            XmlNodeList xmlNodeList = xmlDocument.SelectNodes("configuration/connectionStrings/add"); // 获取数据库连接串相应节点

            foreach (XmlNode item in xmlNodeList) // 遍历获取
            {
                if (item.Attributes["name"].Value == "mysql") // foreach限制只获取mysql的连接串
                {
                    SQL_ConnectStr = item.Attributes["connectionString"].Value;  // 连接串赋值
                }
            }
            MySqlConnection = new MySqlConnection(SQL_ConnectStr);
            if (MySqlConnection != null)
            {
                MySqlConnection.Open();//开启SQL查询通道
                Console.WriteLine("数据通道打开");
            }
            else
            {
                Console.WriteLine("数据通道打开失败!");
                throw new ArgumentOutOfRangeException("MySqlConnection", "数据库连接失败未知错误");
            }
        }

        // 实现数据库接口 - 修改
        void IDatabase.DataBase_modify()
        {

        }

        void IDatabase.DataBase_modify(ref string Sql_str)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(Sql_str, MySqlConnection);
            Sql_str = mySqlCommand.ExecuteNonQuery().ToString();
        }

        // 中断连接MySQL
        void IDatabase.DataBase_Disconnect()
        {
            MySqlConnection.Close();
        }

    }
}
