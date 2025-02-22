using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace C23.SellManage
{
    public partial class FrmCustomerInfo : Form
    {
        DataTable dt = new DataTable();
        C23.BaseClass.BaseOperate boperate = new C23.BaseClass.BaseOperate();
        C23.BaseClass.OperateAndValidate opAndvalidate = new C23.BaseClass.OperateAndValidate();
        protected string M_str_sql = "select CUID as 客户代码,CName as 客户名称,"
            + "Phone as 联系电话,Fax as 传真号码,PostCode as 邮政编码,Address as 联系地址,Email as Email,taxrate as 税率,Remark as 备注,"
            + "Maker as 制单人,Date as 日期 from tb_CustomerInfo";
        protected string M_str_table = "tb_CustomerInfo";
        protected int M_int_judge, i;
        protected int select;
        public FrmCustomerInfo()
        {
            InitializeComponent();
        }
        private void dgvClientInfo_DoubleClick(object sender, EventArgs e)
        {
            int intCurrentRowNumber = this.dataGridView1.CurrentCell.RowIndex;
            string s1 = this.dataGridView1.Rows[intCurrentRowNumber].Cells[0].Value.ToString().Trim();
            string s2 = this.dataGridView1.Rows[intCurrentRowNumber].Cells[1].Value.ToString().Trim();
            string s3 = this.dataGridView1.Rows[intCurrentRowNumber].Cells[2].Value.ToString().Trim();
            string s4 = this.dataGridView1.Rows[intCurrentRowNumber].Cells[5].Value.ToString().Trim();
            if (select == 0)
            {

                C23.SellManage.FrmOrderT.data1[0] = "doubleclick";
                C23.SellManage.FrmOrderT.data2[0] = s1;
                C23.SellManage.FrmOrderT.data2[1] = s2;
                C23.SellManage.FrmOrderT.data2[2] = s3;
                C23.SellManage.FrmOrderT.data2[3] = s4;

            }
            if (select == 1)
            {

                C23.SellManage.FrmSellTableT.data1[0] = "doubleclick";
                C23.SellManage.FrmSellTableT.data2[0] = s1;
                C23.SellManage.FrmSellTableT.data2[1] = s2;
                C23.SellManage.FrmSellTableT.data2[2] = s3;
                C23.SellManage.FrmSellTableT.data2[3] = s4;

            }
            this.Close();
        }
        private void FrmCustomerInfo_Load(object sender, EventArgs e)
        {
            bind();
        }
        public void a1()
        {
            dataGridView1.ReadOnly = true;
            select = 0;
        }
        public void a2()
        {
            dataGridView1.ReadOnly = true;
            select = 1;
        }

        private void bind()
        {
            if (textBox1.Text == "" && textBox2.Text == "")
            {
                dt = boperate.getdt(M_str_sql);
                dataGridView1.DataSource = dt;
            }
            else if (textBox1.Text != "" && textBox2.Text == "")
            {
                DataSet myds = boperate.getds(M_str_sql + " where CUID like '%" + textBox1.Text + "%'", M_str_table);
                if (myds.Tables[0].Rows.Count > 0)
                    dataGridView1.DataSource = myds.Tables[0];
                else
                    MessageBox.Show("没有要查找的相关记录！");
            }
            else if (textBox1.Text == "" && textBox2.Text != "")
            {
                DataSet myds = boperate.getds(M_str_sql + " where CName like '%" + textBox2.Text + "%'", M_str_table);
                if (myds.Tables[0].Rows.Count > 0)
                    dataGridView1.DataSource = myds.Tables[0];
                else
                    MessageBox.Show("没有要查找的相关记录！");
            }
            else if (textBox1.Text != "" && textBox2.Text != "")
            {
                DataSet myds = boperate.getds(M_str_sql + " where CUID like '%" + textBox1.Text + "%' AND CNAME LIKE '%" + textBox2.Text +
                    "%'", M_str_table);
                if (myds.Tables[0].Rows.Count > 0)
                    dataGridView1.DataSource = myds.Tables[0];
                else
                    MessageBox.Show("没有要查找的相关记录！");
            }
            txtCName.Enabled = false;
            if (dt.Rows.Count > 0)
            {
                btnDel.Enabled = true;
            }
            else
            {
                btnDel.Enabled = false;
            }
            for (i = 0; i < dataGridView1.Columns.Count - 1; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

            }
            dgvStateControl();


        }
        #region dgvStateControl
        private void dgvStateControl()
        {
            int i;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Lavender;
            txtCName.BackColor = Color.Yellow;
            txtCPhone.BackColor = Color.Yellow;
            txtCAddress.BackColor = Color.Yellow;
            int numCols1 = dataGridView1.Columns.Count;
            for (i = 0; i < numCols1; i++)
            {

                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i == 0 || i == 2 || i == 3 | i == 4 || i == 7 || i == 8)
                {
                    dataGridView1.Columns[i].Width = 90;

                }
                if (i == 9)
                {
                    dataGridView1.Columns[i].Width = 120;

                }
                if (i == 1 || i == 5)
                {
                    dataGridView1.Columns[i].Width = 200;

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
                if (i == 9 || i == 11)
                {
                    //dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
            for (i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i == 11)
                {
                    dataGridView1.Columns[i].ReadOnly = false;
                }
                else
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }

            }
        }
        #endregion
 
        #region at
        private void at()
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            string varmaker = FrmLogin.M_str_name;
            string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
            if (M_int_judge == 0)
            {
                if (boperate.exists("select * from tb_customerinfo where cname='" + txtCName.Text + "'"))
                {

                    MessageBox.Show("该客户名称已经存在了！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else if (juage1() == true)
                {

                    boperate.getcom("insert into tb_CUSTOMERInfo(CUID,CName,"
              + "Year,Month,Day,Phone,Fax,PostCode,Address,Email,TaxRate,Date,Maker) values('" + txt1.Text.Trim()
              + "','" + txtCName.Text.Trim() + "','" + year + "','" + month + "','" + day + "','" + txtCPhone.Text.Trim() + "','" + txtCFax.Text.Trim()
              + "','" + txtCPostCode.Text.Trim() + "','" + txtCAddress.Text.Trim() + "','" + txtCEmail.Text.Trim()
              + "','" + txt8.Text + "','" + varDate
              + "','" + varmaker + "')");
                    bind();
                    btnSave.Enabled = false;
                    btnEdit.Enabled = true;
                    txtCName.Enabled = false;

                }

            }
            if (M_int_judge == 1)
            {
                if (juage1() == true)
                {
                    boperate.getcom("update tb_CustomerInfo set CName='" + txtCName.Text.Trim()
                        + "',Phone='" + txtCPhone.Text.Trim() + "',Fax='" + txtCFax.Text.Trim()
                        + "',PostCode='" + txtCPostCode.Text.Trim() + "',Address='" + txtCAddress.Text.Trim()
                        + "',Email='" + txtCEmail.Text.Trim() + "',Taxrate='" + txt8.Text + "',Maker='" + varmaker + 
                        "' where CUID='" + txt1.Text.Trim() + "'");
                    bind();
                    btnSave.Enabled = false;
                    btnEdit.Enabled = true;
                    txtCName.Enabled = false;
                }

            }


        }
        #endregion
        private bool juage1()
        {

            bool ju = true;
            if (txtCName.Text == "")
            {
                ju = false;
                MessageBox.Show("客户名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtCPhone.Text == "")
            {
                ju = false;
                MessageBox.Show("联系电话不能为空！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else if (boperate.checkphone(txtCPhone.Text) == false)
            {
                ju = false;
                MessageBox.Show("电话号码只能输入数字！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

            else if (boperate.checkphone(txtCFax.Text) == false)
            {
                ju = false;
                MessageBox.Show("传真号码只能输入数字！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

            else if (boperate.checkphone(txtCPostCode.Text) == false)
            {
                ju = false;
                MessageBox.Show("邮编只能输入数字！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else if (txtCAddress.Text == "")
            {
                ju = false;
                MessageBox.Show("送货地址不能为空！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else if (boperate.checkEmail(txtCEmail.Text) == false)
            {
                ju = false;
                MessageBox.Show("邮箱地址只能输入数字字母的组合！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else if (boperate.yesno(txt8.Text) == 0)
            {
                ju = false;
                MessageBox.Show("税率只能输入数值！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            }
            return ju;

        }
    
        private void dgvClientInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt1.Text = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            txtCName.Text = Convert.ToString(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            txtCPhone.Text = Convert.ToString(dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            txtCFax.Text = Convert.ToString(dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            txtCPostCode.Text = Convert.ToString(dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            txtCAddress.Text = Convert.ToString(dataGridView1[5, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            txtCEmail.Text = Convert.ToString(dataGridView1[6, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            txt8.Text = Convert.ToString(dataGridView1[7, dataGridView1.CurrentCell.RowIndex].Value).Trim();
          
        }

        public void ClearText()
        {
            txtCName.Text = "";
            txtCPhone.Text = "";
            txtCFax.Text = "";
            txtCPostCode.Text = "";
            txtCAddress.Text = "";
            txtCEmail.Text = "";
            txt8.Text = "";
  
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                bind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string a1 = boperate.numN(12, 4, "0001", "select * from tb_CustomerInfo", "CUID", "CU");
            if (a1 == "Exceed Limited")
            {
                MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                txt1.Text = a1;

            }
            btnSave.Enabled = true;
            btnEdit.Enabled = false;
            txtCName.Enabled = true;
            M_int_judge = 0;
            ClearText();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            M_int_judge = 1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                at();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);


            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            string a1 = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            try
            {
                if (boperate.exists("select * from tb_order where cuid='" + a1 + "'") == true)
                {
                    MessageBox.Show("该客户信息已经在订单中存在不允许删除！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {

                    if (MessageBox.Show("确定要删除该客户信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        boperate.getcom("delete from tb_CUSTOMERInfo where CUID='" + a1 + "'");
                        bind();
                    }

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
    }
}
