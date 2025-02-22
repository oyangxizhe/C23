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
    public partial class FrmUSER_INFO : Form
    {
        DataTable dt = new DataTable();
        private string _IDO;
        public string IDO
        {
            set { _IDO = value; }
            get { return _IDO; }

        }
        private string _ADD_OR_UPDATE;
        public string ADD_OR_UPDATE
        {
            set { _ADD_OR_UPDATE = value; }
            get { return _ADD_OR_UPDATE; }
        }
        private static string _EMID;
        public static string EMID
        {
            set { _EMID = value; }
            get { return _EMID; }

        }
        private static string _ENAME;
        public  static string ENAME
        {
            set { _ENAME = value; }
            get { return _ENAME; }

        }
        private static string _DEPART;
        public static string DEPART
        {
            set { _DEPART = value; }
            get { return _DEPART; }

        }
        private bool _IFExecutionSUCCESS;
        public  bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        private static bool _IF_DOUBLE_CLICK;
        public static bool IF_DOUBLE_CLICK
        {
            set { _IF_DOUBLE_CLICK = value; }
            get { return _IF_DOUBLE_CLICK; }

        }
        basec bc = new basec();
        CEMPLOYEE_INFO cemployee_info = new CEMPLOYEE_INFO();
        CUSER cuser = new CUSER();
         string sql = @"
SELECT
A.USID AS 用户编号,
A.UNAME AS 用户名,
A.EMID AS 员工编号,
B.ENAME AS 姓名,
(SELECT ENAME FROM EMPLOYEEINFO  WHERE EMID=A.MAKERID) AS 制单人,
A.DATE AS 制单日期 
FROM   USERINFO  A 
LEFT JOIN EMPLOYEEINFO B ON A.EMID=B.EMID
";

        protected int M_int_judge, i;
        protected int select;
        public FrmUSER_INFO()
        {
            InitializeComponent();
        }
        private void FrmUSER_INFO_Load(object sender, EventArgs e)
        {
            Bind();

        }
        private void Bind()
        {
            dt = basec.getdts(sql);
            dataGridView1.DataSource = dt;
            textBox1.Text = IDO;
          
            comboBox1.Focus();
            textBox2.BackColor = Color.Yellow;
            comboBox1.BackColor = Color.Yellow;
            textBox3.BackColor = Color.Yellow;
            dgvStateControl();
            hint.ForeColor= Color.Red;
            hint.Location = new Point(400,100);
            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {
                hint.Text = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
            }
            else
            {
                hint.Text  = "";
            }
            LENAME.Text = "";
            textBox2.Focus();
            textBox3.PasswordChar = '*';
            IF_DOUBLE_CLICK = false;
        }
        #region dgvStateControl
        private void dgvStateControl()
        {
            int i;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Lavender;
            int numCols1 = dataGridView1.Columns.Count;
            for (i = 0; i < numCols1; i++)
            {

                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i == 0)
                {
                    dataGridView1.Columns[i].Width = 80;

                }
                else if (i == 5)
                {
                    dataGridView1.Columns[i].Width = 120;

                }
           
                else
                {
                    dataGridView1.Columns[i].Width = 80;

                }
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.Columns[i].HeaderCell.Style.BackColor = Color.Lavender;

            }
            for (i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.OldLace;
                i = i + 1;
            }
            for (i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[i].ReadOnly = true;

            }
        }
        #endregion

        #region save
        protected void save()
        {
            hint.Text = "";
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss");

            string varMakerID = FrmLogin.USID;
            string v2 = bc.getOnlyString("SELECT UNAME FROM USERINFO WHERE  USID='" + textBox1.Text + "'");
            Byte[] B = bc.GetMD5(textBox3.Text );
            if (juage())
            {

            }
            else if (!bc.exists("SELECT USID FROM USERINFO WHERE USID='" + textBox1.Text + "'"))
            {
                if (bc.exists("select * from USERINFO where Uname='" + textBox2.Text + "'"))
                {

                    hint .Text = "该用户名已经存在了！";
                

                }
                else
                {


                    //建立SQL Server链接
                    SqlConnection Con = bc.getcon();
                    String SqlCmd = "INSERT INTO USERINFO (USID,UNAME,EMID,PWD,"
+ "DATE,Year,Month,Day) valueS (@USID,@UName,@EMID,@PWD,@DATE,@YEAR,@MONTH,@DAY)";
                    SqlCommand CmdObj = new SqlCommand(SqlCmd, Con);
                    CmdObj.Parameters.Add("@USID", SqlDbType.VarChar, 20).Value = textBox1.Text;
                    CmdObj.Parameters.Add("@UName", SqlDbType.VarChar, 20).Value = textBox2.Text;
                    CmdObj.Parameters.Add("@EMID", SqlDbType.VarChar, 20).Value = comboBox1.Text;
                    CmdObj.Parameters.Add("@PWD", SqlDbType.Binary, 50).Value = B;
                    CmdObj.Parameters.Add("@MAKEID", SqlDbType.VarChar, 20).Value = varMakerID;
                    CmdObj.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
                    CmdObj.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
                    CmdObj.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
                    CmdObj.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
                    Con.Open();
                    CmdObj.ExecuteNonQuery();
                    
                    Con.Close();
                    IFExecution_SUCCESS = true;
                

                }
            }
            else if (v2 != textBox2.Text)
            {
                if (bc.exists("select * from USERINFO where Uname='" + textBox2.Text + "'"))
                {

                  hint .Text= "该用户名已经存在了！";

                }
                else
                {
                    string sql = @"UPDATE USERINFO SET UNAME=@UNAME,
EMID=@EMID,
PWD=@PWD,
DATE=@DATE,
MAKERID=@MAKERID 
WHERE USID='" + textBox1.Text + "'";
                    SqlConnection con = bc.getcon();
                    SqlCommand sqlcom = new SqlCommand(sql, con);
                    sqlcom.Parameters.Add("@UNAME", SqlDbType.VarChar, 20).Value = textBox2.Text;
                    sqlcom.Parameters.Add("@EMID", SqlDbType.VarChar, 20).Value = comboBox1.Text;
                    sqlcom.Parameters.Add("@PWD", SqlDbType.Binary, 50).Value = B;
                    sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = varMakerID;
                    sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
                    con.Open();
                    sqlcom.ExecuteNonQuery();
                    con.Close();
                    IFExecution_SUCCESS = true;

                }

            }
            else
            {
                string sql = @"UPDATE USERINFO SET UNAME=@UNAME,
EMID=@EMID,
PWD=@PWD,
DATE=@DATE,
MAKERID=@MAKERID 
WHERE USID='" + textBox1.Text + "'";
                SqlConnection con = bc.getcon();
                SqlCommand sqlcom = new SqlCommand(sql, con);
                sqlcom.Parameters.Add("@UNAME", SqlDbType.VarChar, 20).Value = textBox2.Text;
                sqlcom.Parameters.Add("@EMID", SqlDbType.VarChar, 20).Value = comboBox1.Text;
                sqlcom.Parameters.Add("@PWD", SqlDbType.Binary, 50).Value = B;
                sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = varMakerID;
                sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
                con.Open();
                sqlcom.ExecuteNonQuery();
                con.Close();
                IFExecution_SUCCESS = true;

            }

          
        }
        #endregion
        #region juage()
        private bool juage()
        {

            string pwd = textBox3.Text;
            bool b = false;
            if (textBox2.Text  == "")
            {
                b = true;
                hint.Text = "用户名不能为空！";
            

            }
            else if (!bc.exists("SELECT * FROM EMPLOYEEINFO WHERE EMID='" + comboBox1.Text + "'"))
            {

                b = true;
                hint.Text = "员工编号在系统中不存在！";

            }
            else if (pwd == "")
            {
                b = true;
                hint.Text = "密码不能为空！";
       

            }
            else if (bc.checkEmail(pwd) == false)
            {
                b = true;
                hint.Text = "密码只能输入数字字母的组合";


            }
            else if (pwd.Length < 6)
            {
                b = true;
                hint.Text = "密码长度需大于6位！";
              

            }
            else if (!bc.checkNumber(pwd))
            {
                b = true;
                hint.Text = "密码需是数字与字母的组合！";

            }
            else if (!bc.checkLetter(pwd))
            {
                b = true;
                hint.Text = "密码需是数字与字母的组合！";

            }
            return b;

        }
        #endregion
        public void ClearText()
        {
            textBox2.Text = "";
            comboBox1.Text = "";
            LENAME.Text = "";
            textBox3.Text = "";
    
        
        }
        public void EditRight()
        {
            dataGridView1.Enabled = true;


        }
        private void dgvUSER_INFO_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            textBox1 .Text  = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            textBox2.Text = Convert.ToString(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            comboBox1.Text  = Convert.ToString(dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value).Trim();

         


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            add();
        }
        private void add()
        {

            textBox1.Text = cuser.GETID();
            ClearText();

        }
      

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            if (juage())
            {

            }
            else
            {
                save();
                if (IFExecution_SUCCESS)
                {
                    Bind();
                    add();
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {


                dt = bc.getdt(sql+" WHERE A.USID LIKE '%"+textBox4.Text +"%' AND A.UNAME LIKE '%"+textBox5 .Text +"%'");
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;

                }
                else
                {

                    MessageBox.Show("没有找到相关信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                }
                dgvStateControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
        
            try
            {

                if (MessageBox.Show("确定要删除该条用户信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {

                    string v1 = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();

                    bc.getcom("delete from USERINFO where USID='" + v1 + "'");
                    basec.getcoms("delete RightList where USID='" + v1 + "'");
                    Bind();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

   
        #region override enter
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && ((!(ActiveControl is System.Windows.Forms.TextBox) ||
                !((System.Windows.Forms.TextBox)ActiveControl).AcceptsReturn)))
            {

                if (dataGridView1.CurrentCell.ColumnIndex == 7 &&
                    dataGridView1["借方原币金额", dataGridView1.CurrentCell.RowIndex].Value.ToString() != null)
                {

                    SendKeys.SendWait("{Tab}");
                    SendKeys.SendWait("{Tab}");
                }
                else if (dataGridView1.CurrentCell.ColumnIndex == 9)
                {
                    SendKeys.SendWait("{Tab}");
                    SendKeys.SendWait("{Tab}");
                    SendKeys.SendWait("{Tab}");
                }
                else
                {

                    SendKeys.SendWait("{Tab}");
                }
                return true;
            }
            if (keyData == (Keys.Enter | Keys.Shift))
            {
                SendKeys.SendWait("+{Tab}");

                return true;
            }
            if (keyData == (Keys.F7))
            {

                dataGridView1.Focus();

                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

  

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            C23.EmployeeManage.FrmEmployeeInfo frm = new C23.EmployeeManage.FrmEmployeeInfo();
            frm.USER_INFO_USE();
            frm.ShowDialog();
            this.comboBox1.IntegralHeight = false;//使组合框不调整大小以显示其所有项
            this.comboBox1.DroppedDown = false;//使组合框不显示其下拉部分
            this.comboBox1.IntegralHeight = true;//恢复默认值
            if (IF_DOUBLE_CLICK)
            {
                comboBox1.Text = EMID;
                LENAME.Text = ENAME;
            }
            textBox3.Focus();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            textBox2.Text = Convert.ToString(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            comboBox1.Text = Convert.ToString(dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            LENAME.Text = Convert.ToString(dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value).Trim();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.Enabled == true)
            {
                int indexNumber = dataGridView1.CurrentCell.RowIndex;
                string sendUserID, sendUName, sendDepart;
                sendUserID = dataGridView1.Rows[indexNumber].Cells[0].Value.ToString().Trim();
                sendUName = dataGridView1.Rows[indexNumber].Cells[1].Value.ToString().Trim();
             
                string ename = sendDepart = dataGridView1.Rows[indexNumber].Cells["姓名"].Value.ToString().Trim();
                string[] inputarry = new string[] { sendUserID, sendUName,ename  };
                C23.UserManage.FrmEditRight .UNAME  = inputarry[1];
                C23.UserManage.FrmEditRight .ENAME  = inputarry[2];
                FrmEditRight.IF_DOUBLE_CLICK = true;
                this.Close();
            }
        }

    
  

     

   

    
    }
}
