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

namespace C23.EmployeeManage
{
    public partial class FrmEmployeeInfo : Form
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
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        basec bc = new basec();
        CEMPLOYEE_INFO cemployee_info = new CEMPLOYEE_INFO();
        protected string sql = @"
SELECT 
A.EMID AS 工号,
A.ENAME AS 姓名,
A.DEPART AS 部门,
A.POSITION AS 职务,
A.PHONE AS 电话,
(SELECT ENAME FROM EMPLOYEEINFO 
WHERE EMID=A.MAKERID ) AS 制单人,
A.DATE AS 制单日期
FROM
EMPLOYEEINFO A ";
   
        protected int M_int_judge, i;
        protected int select;
        public FrmEmployeeInfo()
        {
            InitializeComponent();
        }
        #region double_click
        private void dgvEmployeeInfo_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.Enabled == true)
            {
                int indexNumber = dataGridView1.CurrentCell.RowIndex;
                string id = dataGridView1.Rows[indexNumber].Cells[0].Value.ToString().Trim();
                string sendEName = dataGridView1.Rows[indexNumber].Cells[1].Value.ToString().Trim();
                string sendDepart = dataGridView1.Rows[indexNumber].Cells[2].Value.ToString().Trim();
                string[] inputarry = new string[] { sendEName, sendDepart,id};
                if (select == 0)
                {
                    //C23.SellManage.FrmOrders.inputgetOEName[0] = inputarry[0]; 
                }
                if (select == 1)
                {
                    C23.StockManage.frmStockTable.inputgetSEName[0] = inputarry[0];
                }
                if (select == 2)
                {

                }
                if (select == 3)
                {
                    C23.PUR.frmPurOrders.getEmployeeInfo[0] = inputarry[0];
                }
                if (select == 4)
                {

                }
                if (select == 5)
                {

                }
                if (select == 6)
                {
                    C23.StockManage.frmStockTableT.inputgetSEName[0] = inputarry[0];
                }
                if (select == 7)
                {
                    C23.PUR.frmPurOrdersT.getEmployeeInfo[0] = inputarry[0];
                }
                if (select == 8)
                {

                }
                if (select == 9)
                {
                    C23.StockManage.frmReturnT.inputgetSEName[0] = inputarry[0];
                }
                if (select == 10)
                {
                    C23.StockManage.frmReturn.inputgetSEName[0] = inputarry[0];
                }
                if (select == 11)
                {
                    C23.SellManage.frmSellReT.getEmployeeInfo[0] = inputarry[0];
                }
                if (select == 12)
                {
                    C23.SellManage.frmSellRe.getEmployeeInfo[0] = inputarry[0];
                }
                if (select == 13)
                {

                }
                if (select == 14)
                {

                }
                if (select == 15)
                {
                    C23.StorageManage.FrmPWGodET.inputgetSEName[0] = inputarry[0];
                }
                if (select == 16)
                {
                    C23.UserManage.FrmUSER_INFO.EMID = inputarry[2];
                    C23.UserManage.FrmUSER_INFO.ENAME= inputarry[0];
                    C23.UserManage.FrmUSER_INFO.DEPART = inputarry[1];
                    C23.UserManage.FrmUSER_INFO.IF_DOUBLE_CLICK = true;
                }
                if (select == 17)
                {
                    C23.BomManage.FrmWorkGroupMT.data3[0] = "doubleclick";
                    C23.BomManage.FrmWorkGroupMT.data2[0] = id;
                    C23.BomManage.FrmWorkGroupMT.data2[1] = sendEName;


                }
                if (select == 18)
                {

                    C23.StorageManage.FrmOutSourcingMateReT.data3[0] = "doubleclick";
                    C23.StorageManage.FrmOutSourcingMateReT.data2[0] = sendEName;
                }
                if (select == 19)
                {
                    C23.StorageManage.FrmMiscGodET.inputgetSEName[0] = inputarry[0];
                }
                this.Close();
            }

        }
        #endregion
        #region only read
        public void dgvReadOnlyOrders()
        {
            dataGridView1.Enabled = true;
            select = 0;

        }
        public void dgvReadOnlyStock()
        {
            dataGridView1.Enabled = true;
            select = 1;
        }
        public void SellTable()
        {
            dataGridView1.Enabled = true;
            select = 2;
        }
        public void dgvReadOnlyPur()
        {
            dataGridView1.Enabled = true;
            select = 3;
        }
        public void GodE()
        {
            dataGridView1.Enabled = true;
            select = 4;
        }
        public void dgvReadOnlyOrdersT()
        {
            dataGridView1.Enabled = true;
            select = 5;

        }
        public void dgvReadOnlyStockT()
        {
            dataGridView1.Enabled = true;
            select = 6;
        }
        public void dgvReadOnlyPurT()
        {
            dataGridView1.Enabled = true;
            select = 7;
        }
        public void SellTableT()
        {
            dataGridView1.Enabled = true;
            select = 8;
        }
        public void ReturnT()
        {
            dataGridView1.Enabled = true;
            select = 9;
        }
        public void Return()
        {
            dataGridView1.Enabled = true;
            select = 10;
        }
        public void SellReT()
        {
            dataGridView1.Enabled = true;
            select = 11;
        }
        public void SellRe()
        {
            dataGridView1.Enabled = true;
            select = 12;
        }
        public void MateReT()
        {
            dataGridView1.Enabled = true;
            select = 13;
        }
        public void MateRe()
        {
            dataGridView1.Enabled = true;
            select = 14;
        }
        public void GodET()
        {
            dataGridView1.Enabled = true;
            select = 15;
        }
        public void USER_INFO_USE()
        {
            dataGridView1.Enabled = true;
            select = 16;
        }
        public void a1()
        {
            dataGridView1.Enabled = true;
            select = 17;

        }
        public void a2()
        {
            dataGridView1.Enabled = true;
            select = 18;

        }
        public void a3()
        {
            dataGridView1.Enabled = true;
            select = 19;

        }
  
        #endregion
        private void frmEmployeeInfo_Load(object sender, EventArgs e)
        {

            Bind();

        }
        private void Bind()
        {
           
            textBox1.Text = IDO;
            dt = basec.getdts(sql);
            dataGridView1.DataSource = dt;
            textBox2.Focus();
            textBox2.BackColor = Color.Yellow;
            dgvStateControl();
            hint.Location = new Point(400,100);
            hint.ForeColor = Color.Red;
            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {
                hint.Text = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
            }
            else
            {
                hint.Text = "";
            }
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
                if (i == 1)
                {
                    dataGridView1.Columns[i].Width = 70;

                }
                else if (i == 6)
                {
                    dataGridView1.Columns[i].Width = 120;

                }
                else if (i == 4)
                {
                    dataGridView1.Columns[i].Width = 90;

                }
                else
                {
                    dataGridView1.Columns[i].Width = 60;

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
        private void save()
        {

            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss");

            string varMakerID = FrmLogin.USID;
            if (!bc.exists("SELECT EMID FROM EMPLOYEEINFO WHERE EMID='" + textBox1 .Text + "'"))
            {

                basec.getcoms(@"INSERT INTO EMPLOYEEINFO(EMID,ENAME,DEPART,POSITION,MAKERID,DATE,YEAR,
                                   MONTH,PHONE) VALUES ('" + textBox1.Text + "','" + textBox2.Text +
                 "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + varMakerID + "','" + varDate +
                 "','" + year + "','" + month + "','" + textBox3.Text + "')");
                IFExecution_SUCCESS = true;

            }
            else
            {
                basec.getcoms(@"UPDATE EMPLOYEEINFO SET ENAME='" + textBox2.Text + "',DEPART='" + comboBox1.Text +
                      "',POSITION='" + comboBox2.Text + "',MAKERID='" + varMakerID +
                      "',DATE='" + varDate + "',PHONE='" + textBox3.Text + "' WHERE EMID='" + textBox1.Text + "'");
                IFExecution_SUCCESS = true;
            }
            Bind();
        }
        #endregion
        #region juage()
        private bool juage()
        {


            bool b = false;
            if (textBox2.Text == "")
            {
                b = true;

                hint.Text = "姓名不能为空！";
             
            }
     
            else if (bc.checkphone(textBox3 .Text ) == false)
            {
                b = true;
                hint.Text = "电话号码只能输入数字！";

            }
            return b;

        }
        #endregion
        public void ClearText()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
        
        }

        private void dgvEmployeeInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            textBox1 .Text  = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            textBox2.Text = Convert.ToString(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            comboBox1.Text  = Convert.ToString(dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            comboBox2 .Text = Convert.ToString(dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            textBox3.Text= Convert.ToString(dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value).Trim();


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            add();
        }
        private void add()
        {

            textBox1.Text = cemployee_info.GETID();
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
                if (ADD_OR_UPDATE == "ADD")
                {
                    add();
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {


                dt = bc.getdt(sql+" WHERE A.EMID LIKE '%"+textBox4.Text +"%' AND A.ENAME LIKE '%"+textBox5 .Text +"%'");
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
                if (MessageBox.Show("确定要删除该条员工信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {


                }
                string v1 = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
                DataTable dty = bc.getdt("select * from tb_workgroupM where Employeeid='" + v1 + "'");
                if (dty.Rows.Count > 0)
                {
                    MessageBox.Show("该员工编号已经在工作组中使用了，不允许删除，若要删除先删除工作组中该员工！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    bc.getcom("delete from EmployeeInfo where EMID='" + v1 + "'");
                    Bind();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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
    }
}
