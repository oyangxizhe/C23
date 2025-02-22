using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using XizheC;

namespace C23
{
    public partial class FrmLogin : Form
    {
        public static string USID;//记录登录用户编号
        public static string UName;//记录登录用户名
        public static string M_str_pwd;//记录登录用户密码
        public static string M_str_Depart;//记录登录用户的部门
        public static string EMID;
        public static string ENAME;//记录登录用户姓名
        public static string M_str_name;
      
        public byte[] PWD;
        basec bc = new basec();
        CUSER cuser = new CUSER();
        public FrmLogin()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
       
        private void frmLogin_Load(object sender, EventArgs e)
        {
            
            dt = bc.getdt("SELECT UNAME FROM USERINFO");
            foreach (DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["UNAME"].ToString());
            }
            hint.Text = "";
            hint.ForeColor = Color.Red;
            textBox1.PasswordChar = '*';
            if (bc.exists("SELECT UNAME FROM USERINFO WHERE UNAME='admin'"))
            {
                comboBox1.Text = "admin";

            }
            btnLogin.Size = new Size(115, 21);
            btnLogin.FlatStyle = FlatStyle.Flat;/*使BUTTON 采用IMG做底图*/
            btnLogin.FlatAppearance.BorderSize = 0;/*去掉底图黑线*/
            textBox1.Focus();
        }

        private void cboxUName_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #region 
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter &&
             (
             (
              !(ActiveControl is System.Windows.Forms.TextBox) ||
              !((System.Windows.Forms.TextBox)ActiveControl).AcceptsReturn)
             )
             )
            {
                SendKeys.SendWait("{Tab}");
                return true;
            }
            if (keyData == (Keys.Enter | Keys.Shift))
            {
                SendKeys.SendWait("+{Tab}");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
    
        private void login()
        {
               if (cuser.JUAGE_LOGIN_IF_SUCCESS(comboBox1 .Text ,textBox1 .Text ))
                {
                 
                    M_str_Depart = cuser.DEPART;
                    M_str_name = comboBox1.Text;
                    ENAME = cuser.ENAME;
                    UName = comboBox1.Text;
                    EMID = cuser.EMID;
                    USID = cuser.USID;
                    FrmMain frm = new FrmMain();
                    this.Hide();
                    frm.Show();
                
                 
                }
                else
                {

                    hint.Text = "密码不正确，请重新输入！";
                }
        }

        #region juage()
        private bool juage()
        {

            string uname = comboBox1.Text;
            string pwd = textBox1.Text;
            bool b = false;
            if (uname == "")
            {
                b = true;
                hint.Text = "用户名不能为空！";

            }
            else if (!bc.exists ("SELECT * FROM USERINFO WHERE UNAME='"+uname+"'"))
            {
                b = true;
                hint.Text = "用户名不存在！";
            }
            else if (pwd== "")
            {
                b = true;
                hint.Text = "密码不能为空！";

            }
            return b;

        }
        #endregion

        private void btnLogin_Enter(object sender, EventArgs e)
        {
            /*USID = "US13110001";
            UName = "admin";
            M_str_name = "admin";
            FrmMain frm = new FrmMain();
            this.Hide();
            frm.ShowDialog();*/
            if (juage())
            {
            }
            else
            {
                login();

            }
          
            textBox1.Focus();/*执行BTNLOGIN 事件时将FOCUS移到其它控件避免选中时出现底框*/
        
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
           if (juage())
            {
            }
            else
            {
                login();

            }

         
        }
   
    }
}
