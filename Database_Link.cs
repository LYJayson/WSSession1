using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSSession1
{
    /// <summary>
    /// 数据库接口
    /// </summary>
    public interface IDatabase
    {
        void DataBase_link(); // 连接数据库

        void DataBase_increase(); // 向数据库增加

        void DataBase_increase(ref string value); // 向数据库增加

        void DataBase_delete(); // 删除

        void DataBase_modify(); // 修改

        void DataBase_modify(ref string value); // 修改

        void DataBase_Inquire(ref string value); // 基本查询

        void DataBase_Inquire(out DataSet Ds,ref string Sql_Str); // dataGridView

        void DataBase_Disconnect(); // 中断连接
    }
}
