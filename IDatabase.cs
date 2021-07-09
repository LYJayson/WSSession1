using System.Collections.Generic;
using System.Data;

namespace WSSession2
{
    /// <summary>
    /// 数据库接口
    /// </summary>
    interface IDatabase
    {
        /// <summary>
        /// 连接用户数据库
        /// </summary>
        /// <param name="value"></param>
        void DataBase_link(string value);

        /// <summary>
        /// 向数据库增加
        /// </summary>
        void DataBase_increase();

        /// <summary>
        /// 向数据库增加
        /// </summary>
        /// <param name="value"></param>
        void DataBase_increase(ref string value);

        /// <summary>
        /// 删除
        /// </summary>
        void DataBase_delete();

        /// <summary>
        /// 删除
        /// </summary>
        void DataBase_delete(ref string value);

        /// <summary>
        /// 修改
        /// </summary>
        void DataBase_modify();

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="value"></param>
        void DataBase_modify(ref string value);

        /// <summary>
        /// 基本查询
        /// </summary>
        /// <param name="value"></param>
        void DataBase_Inquire_Scalar(ref string value);

        /// <summary>
        /// 基本查询
        /// </summary>
        /// <param name="value"></param>
        /// <param name="values"></param>
        void DataBase_Inquire_Reader(ref string value, ref List<object> values, ref string Column);

        /// <summary>
        /// dataGridView
        /// </summary>
        /// <param name="Ds"></param>
        /// <param name="Sql_Str"></param>
        void DataBase_Inquire_DataSet(out DataSet Ds, ref string Sql_Str);

        /// <summary>
        /// 中断连接
        /// </summary>
        void DataBase_Disconnect();

        /// <summary>
        /// 释放
        /// </summary>
        void DataBase_Dispose();

        /// <summary>
        /// 关闭
        /// </summary>
        void DataBase_Close();


    }
}
