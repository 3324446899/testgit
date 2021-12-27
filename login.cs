using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;

namespace WindowsFormsApp1
{
    public partial class login : Form
    {
        string username, password;
        public login()//初始化
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //测试用户是否存在
            string conString = "DSN=ssm-mysql32;UID=root;PWD=888qwe"; //ODBC的数据源名
            OdbcConnection con = null;
            con = new OdbcConnection(conString);
            //验证用户是否存在------------------------------------------------------
            //打开连接
            con.Open();
            string P_Str_SqlStr = "call user_in_or_not('" + username + "');";//调用存储过程
            OdbcCommand com = new OdbcCommand(P_Str_SqlStr, con);
            int flag = Convert.ToInt32(com.ExecuteScalar());
            //关闭连接
            con.Close();
            //---------------------------------------------------------------------------

            try
            {             
                //连接
                string conString2 = "DSN=ssm-mysql32;UID=" + username + ";PWD=" + password; //ODBC的数据源名
                con = new OdbcConnection(conString2);
                con.Open();


                //转到管理员面板
                if (flag==0 || username=="root")
                {
                    manager form2 = new manager();
                    form2.username = username;
                    form2.password = password;
                    form2.Show();
                }

                //转到学生面板
                else if (flag == 1)
                {
                    student student = new student();
                    student.username = username;
                    student.password = password;
                    student.Show();
                }

                //转到老师面板
                else if (flag == 2)
                {
                    teacher teacher = new teacher();
                    teacher.username = username;
                    teacher.password = password;
                    teacher.Show();
                }
                
                label3.Text = "数据库连接成功！";
                error.Text = "当前用户名：" + username;
            }
            catch (Exception e1)
            {
                //Console.WriteLine(e1.Message);
                //label3.Text = e1.Message;
                error.Text = "登录失败，请输入正确的用户名（密码）！";
            }
            finally
            {
                //Console.ReadLine();
                con.Close();
            }
        }

        public void textBox2_TextChanged(object sender, EventArgs e)
        {
            password = textBox2.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            username = textBox1.Text;
        }
    }
}
