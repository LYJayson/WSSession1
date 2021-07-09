using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;

namespace WSSession2
{
    class MySql : IDatabase
    {
        public MySqlConnection MySqlConnection;

        // 无参构造
        public MySql()
        {
        }
        // 有参构造
        //public MySql(string Sql_Str)
        //{
        //    Sql_str = Sql_Str;
        //}


        // 实现数据库接口 - 删除
        void IDatabase.DataBase_delete()
        {

        }

        // 实现数据库接口 - 删除
        void IDatabase.DataBase_delete(ref string Sql_str)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(Sql_str, MySqlConnection);
            Sql_str = mySqlCommand.ExecuteNonQuery().ToString(); // 插入true 返回执行数，false返回空值
        }

        // 实现数据库接口 - 增加
        void IDatabase.DataBase_increase()
        {

        }

        void IDatabase.DataBase_increase(ref string Sql_str)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(Sql_str, MySqlConnection);
            Sql_str = mySqlCommand.ExecuteNonQuery().ToString(); // 插入true 返回执行数，false返回空值
        }

        // 实现数据库接口 - 查询
        void IDatabase.DataBase_Inquire_Scalar(ref string Sql_str)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(Sql_str, MySqlConnection);
            Sql_str = Convert.ToString(mySqlCommand.ExecuteScalar());  // 查询true 返回对应值只返回单个，false返回空值
        }

        void IDatabase.DataBase_Inquire_Reader(ref string Sql_str, ref List<object> values, ref string Column)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(Sql_str, MySqlConnection);
            MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();  // 查询true 返回对应值可以多个值，false返回空值

            while (mySqlDataReader.Read())
            {
                values.Add(mySqlDataReader[$"{Column}"].ToString());
            }
        }

        void IDatabase.DataBase_Inquire_DataSet(out DataSet Ds, ref string Sql_Str)
        {
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(Sql_Str, MySqlConnection);
            Ds = new DataSet();
            mySqlDataAdapter.Fill(Ds, "userdata");
        }

        /// <summary>
        /// 实现数据库接口 - 连接
        /// </summary>
        /// <param name="name"></param>
        void IDatabase.DataBase_link(string name)
        {
            string SQL_ConnectStr = ""; // 储存mysql连接串
            XmlDocument xmlDocument = new XmlDocument(); // 开辟xml
            xmlDocument.Load("../../App.config"); // 获取app.config
            XmlNodeList xmlNodeList = xmlDocument.SelectNodes("configuration/connectionStrings/add"); // 获取数据库连接串相应节点

            foreach (XmlNode item in xmlNodeList) // 遍历获取
            {
                if (item.Attributes["name"].Value == name) // foreach限制只获取mysql的连接串
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

        /// <summary>
        /// 实现修改
        /// </summary>
        /// <param name="Sql_str"></param>
        void IDatabase.DataBase_modify(ref string Sql_str)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(Sql_str, MySqlConnection);
            Sql_str = mySqlCommand.ExecuteNonQuery().ToString();  // true 返回执行条数,false 返回0
        }

        /// <summary>
        /// 中断连接MySQL
        /// </summary>
        void IDatabase.DataBase_Disconnect()
        {
            MySqlConnection.Close();
            MySqlConnection.Dispose();
        }

        /// <summary>
        /// 释放
        /// </summary>
        void IDatabase.DataBase_Dispose()
        {
            MySqlConnection.Dispose();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        void IDatabase.DataBase_Close()
        {
            MySqlConnection.Close();
        }


    }
}
