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
namespace C23.UserManage
{
    public partial class FrmEditPwd : Form
    {
        C23.BaseClass.BaseOperate boperate = new C23.BaseClass.BaseOperate();
        protected string M_str_sql = @"select A.USID AS USID,A.UNAME AS UNAME,A.EMID AS EMID,B.ENAME AS ENAME,A.PWD AS PWD,
(SELECT ENAME FROM EMPLOYEEINFO  WHERE EMID=A.MAKERID) AS MAKER,A.DATE AS DATE from   USERINFO  A LEFT JOIN EMPLOYEEINFO B ON A.EMID=B.EMID";
        basec bc = new basec();
        CUSER cuser = new CUSER();
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        public FrmEditPwd()
        {
            InitializeComponent();
        }

        private void FrmEditPwd_Load(object sender, EventArgs e)
        {
           
            Bind();
        }
        #region bind()
        private void Bind()
        {

            textBox1.Text = FrmLogin.M_str_name;
            textBox2.BackColor = Color.Yellow;
            textBox3.BackColor = Color.Yellow;
            textBox4.BackColor = Color.Yellow;
            hint.ForeColor = Color.Red;
            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {
                hint.Text = "密码修改成功！";
            }
            else
            {
                hint.Text = "";
            }
            textBox2.Focus();
            textBox2.PasswordChar = '*';
            textBox3.PasswordChar = '*';
            textBox4.PasswordChar = '*';
        }
        #endregion
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                save();
            }
            catch (Exception)
            {


            }
        }
        #region save
        protected void save()
        {
            hint.Text = "";
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString();
            string varMakerID = FrmLogin.USID;
            Byte[] B = bc.GetMD5(textBox3 .Text );
            if (!juage1())
            {

            }

            else
            {


                string sql = @"UPDATE USERINFO SET 

PWD=@PWD,
MAKERID=@MAKERID,
DATE=@DATE WHERE UNAME='" + FrmLogin .UName + "'";
                SqlConnection con = bc.getcon();
                SqlCommand sqlcom = new SqlCommand(sql, con);
                sqlcom.Parameters.Add("@PWD", SqlDbType.Binary, 50).Value = B;
                sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = varMakerID;
                sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
                con.Open();
                sqlcom.ExecuteNonQuery();
                con.Close();
                IFExecution_SUCCESS = true;
                Bind();

            }

            
        }
        #endregion
        #region juage1()
        private bool juage1()
        {
            Byte[] B = bc.GetMD5(textBox3.Text);
            bool ju = true;
           if (!cuser.JUAGE_LOGIN_IF_SUCCESS(textBox1.Text, textBox2.Text))
            {

                  ju = false;
                  hint.Text = "原密码输入不正确！";
            }
           else if(textBox3.Text=="")
           {
               ju = false;
               hint.Text = "新密码不能为空！";

           }
            else if (bc.checkEmail(textBox3.Text) == false)
            {
                ju = false;
                hint.Text = "密码只能输入数字字母的组合！";

            }
            else if (textBox3.Text.Length < 6)
            {
                ju = false;
                hint.Text = "密码长度需大于6位！";

            }
            else if (!bc.checkNumber(textBox3.Text))
            {
                ju = false;
                hint.Text = "密码需是数字与字母的组合！";

            }
            else if (!bc.checkLetter(textBox3.Text))
            {
                ju = false;
                hint.Text = "密码需是数字与字母的组合！";

            }
          
            else if (textBox3.Text != textBox4.Text)
            {
                ju = false;
                hint.Text = "两次的密码不一致！";

            }
            return ju;
        }
        #endregion
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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
    
    

     
    }
}
