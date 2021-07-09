using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace WSSession2
{
    public partial class Form_Login : Form
    {
        public Form_Login()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")] // 引用dll
        public static extern int MessageBoxTimeoutA(IntPtr hWnd, string msg, string Caps, int type, int Id, int time);   //引用DLL

        private void Login_From_Load(object sender, EventArgs e)
        {

        }

        int Number = 3; //全局变量
        private readonly IDatabase database = new MySql();  // 实例化 数据库接口

        private void Btn_Login_Click(object sender, EventArgs e)
        {
            //Thread thread = new Thread(Data_Link);  // new 新的线程
            //CheckForIllegalCrossThreadCalls = true;
            //thread.IsBackground = true; //设定为后台线程
            //thread.Start(); // 开始

            string Sql_Str; // 储存sql语句
            string account, password; // 账户密码接收变量
            string pat = @"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$";  // 邮箱正则
            if (Timer.Enabled == true)
            {
                MessageBox.Show("禁止登录！");
                return;
            }

            if (Txt_Account.Text.Length > 0 && Txt_pwd.Text.Length > 0) // 检测账户密码长度
            {
                account = Txt_Account.Text.ToString(); // 接收账户字符串
                password = Txt_pwd.Text.ToString(); // 接收密码
            }
            else
            {
                MessageBox.Show("请填入账户或者密码！");
                return;
            }

            if (Regex.Replace(account, pat, string.Empty) != "") // 判断邮箱是否为合法邮箱
            {
                MessageBox.Show("邮箱有误，请重新输入。");
                Txt_Account.Text = "";
            }
            else
            {
                // 确定规则sql语句
                Sql_Str = $"SELECT name FROM users WHERE Role='Admin' and Email='{account}' and password=MD5('{password}')";
                //Data_Link();    // 连接方法
                try
                {
                    database.DataBase_link("Mysql_Session2"); // 连接数据库
                }
                catch
                {
                    MessageBox.Show("连接数据库失败！");
                    return;
                }
                database.DataBase_Inquire_Scalar(ref Sql_Str); // 查询方法
                if (Sql_Str.Length > 0)
                {
                    string Value;
                    Value = Sql_Str;
                    User.User_name = Value;
                    database.DataBase_Dispose(); // 释放 不关闭
                    database.DataBase_link("Mysql_Session2");
                    Sql_Str = $"SELECT id FROM users WHERE name = '{User.User_name}'";
                    database.DataBase_Inquire_Scalar(ref Sql_Str); // 查询方法
                    User.User_id = Sql_Str;
                    database.DataBase_Disconnect(); // 释放连接

                    MessageBox.Show($"欢迎 {User.User_name} 使用管理系统");
                    Form_Home form_Home = new Form_Home(); // 开辟Home窗体空间
                    this.Hide(); // 隐藏login窗体
                    form_Home.ShowDialog(); // 打开窗体
                    this.Close(); // 关闭login窗体
                    this.Dispose(); // 释放login窗体资源
                }
                else
                {
                    if (Number > 1) // 判断错误次数
                    {
                        MessageBox.Show($"账户或者密码错误！您还有{Number -= 1}机会");
                    }
                    else
                    {
                        MessageBox.Show($"禁止登录十分钟！");
                        Timer.Enabled = true; // 启动计时器
                        Timer.Interval = 60000; // 锁死10分钟(600000毫秒)
                        Timer.Start(); // 开始计时
                    }
                }
            }
        }

        /// <summary>
        /// 连接数据库方法
        /// </summary>
        //public void Data_Link()
        //{
        //    try
        //    {
        //        database.DataBase_link("Mysql_Session2"); // 连接数据库
        //    }
        //    catch
        //    {
        //        MessageBox.Show("连接数据库失败！");
        //        return;
        //    }
        //}

        /// <summary>
        /// 用户登录方法
        /// </summary>
        public void User_Login()
        {

        }

        private void Txt_Account_TextChanged(object sender, EventArgs e)
        {
            string account = Txt_Account.Text.ToString(); // 接收邮箱账户
            string pat = @"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$";  // 邮箱正则
            if (Regex.Replace(account, pat, string.Empty) != "") // 匹配邮箱
            {
                errorProvider_login.SetError(Txt_Account, "请输入正确的邮箱");
            }
            else
            {
                errorProvider_login.Clear();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Timer.Stop(); // 停止计时
            Timer.Enabled = false; // 关闭计时器
            Number = 3; // 重置错误计数
            MessageBoxTimeoutA((IntPtr)0, "封禁解除，错误三次后会再次封禁", "提示", 0, 0, 3000); // 弹窗会话提示       
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            this.Dispose(true); // // 释放login窗体 退出
        }

        private void Form_Login_Load(object sender, EventArgs e)
        {

        }
    }
}
