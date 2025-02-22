using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace C23.StorageManage
{
    public partial class frmStorageInfo : Form
    {
        DataTable dt = new DataTable();
        C23.BaseClass.BaseOperate boperate = new C23.BaseClass.BaseOperate();
        C23.BaseClass.OperateAndValidate opAndvalidate = new C23.BaseClass.OperateAndValidate();
        protected string M_str_sql = "select StorageID as 仓库编号,StorageType as 仓库类型,Maker as 制单人,Date as 日期 from tb_StorageInfo";
        protected string M_str_table = "tb_StorageInfo";
        protected int M_int_judge, i;
        protected int select;


        public frmStorageInfo()
        {
            InitializeComponent();
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

                if (i == 0 || i == 2)
                {
                    dataGridView1.Columns[i].Width = 90;
                }

                else if (i == 1 || i == 3)
                {
                    dataGridView1.Columns[i].Width = 120;

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
                if (i == 5)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }
                else
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }
                if (i == 0)
                {
                    dataGridView1.Columns[i].Visible = true;

                }
            }
        }
        #endregion
        private void dgvStorageInfo_DoubleClick(object sender, EventArgs e)
        {
            if (this.dataGridView1.ReadOnly == true)
            {
                int intCurrentRowNumber = this.dataGridView1.CurrentCell.RowIndex;
                string sendSType;

                sendSType = this.dataGridView1.Rows[intCurrentRowNumber].Cells[1].Value.ToString().Trim();

                string[] sendArray = new string[] { sendSType };
                if (select == 0)
                {
                    C23.StockManage.frmStockTable.inputTextDataStorage[0] = sendArray[0];
                }

                if (select == 1)
                {
                    C23.StockManage.frmReturn.inputTextDataStorage[0] = sendArray[0];
                }
                if (select == 2)
                {
                    C23.SellManage.frmSellReT.getStorageTypeInfo[0] = sendArray[0];
                }
                if (select == 3)
                {
                    C23.SellManage.frmSellRe.getStorageTypeInfo[0] = sendArray[0];
                }
                if (select == 4)
                {

                }
                if (select == 5)
                {
                    C23.StockManage.frmStockTableT.inputTextDataStorage[0] = sendArray[0];
                }
                if (select == 6)
                {
                    C23.StockManage.frmReturnT.inputTextDataStorage[0] = sendArray[0];
                }
                if (select == 7)
                {
                    C23.StorageManage.FrmPWGodET.str5[0] = "doubleclick";
                    C23.StorageManage.FrmPWGodET.inputTextDataStorage[0] = sendArray[0];
                }
                if (select == 8)
                {
                    C23.StorageManage.FrmOutSourcingMateReT.data7[0] = "doubleclick";
                    C23.StorageManage.FrmOutSourcingMateReT.data8[0] = sendArray[0];


                }
                if (select == 9)
                {
                    C23.StorageManage.FrmOutSourcingGodET.data7[0] = "doubleclick";
                    C23.StorageManage.FrmOutSourcingGodET.data8[0] = sendArray[0];

                }
                if (select == 10)
                {
                    C23.StorageManage.FrmMiscGodET.str5[0] = "doubleclick";
                    C23.StorageManage.FrmMiscGodET.inputTextDataStorage[0] = sendArray[0];

                }
                this.Close();
            }
        }
        public void dgvStockTable()
        {
            this.dataGridView1.ReadOnly = true;
            select = 0;
        }

        public void dgvReturn()
        {
            this.dataGridView1.ReadOnly = true;
            select = 1;
        }
        public void SellReT()
        {
            this.dataGridView1.ReadOnly = true;
            select = 2;
        }
        public void SellRe()
        {
            this.dataGridView1.ReadOnly = true;
            select = 3;
        }
        public void GodE()
        {
            this.dataGridView1.ReadOnly = true;
            select = 4;
        }
        public void dgvStockTableT()
        {
            this.dataGridView1.ReadOnly = true;
            select = 5;
        }
        public void dgvReturnT()
        {
            this.dataGridView1.ReadOnly = true;
            select = 6;
        }
        public void GodET()
        {
            this.dataGridView1.ReadOnly = true;
            select = 7;
        }
        public void a2()
        {
            this.dataGridView1.ReadOnly = true;
            select = 8;

        }
        public void a3()
        {
            this.dataGridView1.ReadOnly = true;
            select = 9;

        }
        public void a4()
        {
            this.dataGridView1.ReadOnly = true;
            select = 10;

        }
        private void frmStorageInfo_Load(object sender, EventArgs e)
        {
            Bind();
        }
        #region bind
        private void Bind()
        {
            try
            {
                if (textBox1.Text == "" && textBox2.Text == "")
                {
                    dt = boperate.getdt(M_str_sql);
                    dataGridView1.DataSource = dt;
                }
                else if (textBox1.Text != "" && textBox2.Text == "")
                {
                    DataSet myds = boperate.getds(M_str_sql + " where StorageID like '%" + textBox1.Text + "%'", M_str_table);
                    if (myds.Tables[0].Rows.Count > 0)
                        dataGridView1.DataSource = myds.Tables[0];
                    else
                        MessageBox.Show("没有要查找的相关记录！");
                }
                else if (textBox1.Text == "" && textBox2.Text != "")
                {
                    DataSet myds = boperate.getds(M_str_sql + " where StorageType like '%" + textBox2.Text + "%'", M_str_table);
                    if (myds.Tables[0].Rows.Count > 0)
                        dataGridView1.DataSource = myds.Tables[0];
                    else
                        MessageBox.Show("没有要查找的相关记录！");
                }
                else if (textBox1.Text != "" && textBox2.Text != "")
                {
                    DataSet myds = boperate.getds(M_str_sql + " where StorageID like '%" + textBox1.Text + "%' AND StorageType LIKE '%" + textBox2.Text +
                        "%'", M_str_table);
                    if (myds.Tables[0].Rows.Count > 0)
                        dataGridView1.DataSource = myds.Tables[0];
                    else
                        MessageBox.Show("没有要查找的相关记录！");
                }

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        private void dgvStorageInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtStorageID.Text = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            txtStorageType.Text = Convert.ToString(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value).Trim();

        }
        public void ClearText()
        {
            txtStorageType.Text = "";
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            for (i = 0; i < dataGridView1.Columns.Count; i++)
            {
                if (dataGridView1.Columns[i].ValueType.ToString() == "System.Decimal")
                {
                    dataGridView1.Columns[i].DefaultCellStyle.Format = "N";
                    dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                }

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            opAndvalidate.num("select Year,Month,StorageID from tb_StorageInfo", "select * from tb_StorageInfo where Year='" + year +
                "' and Month='" + month + "'", "tb_StorageInfo", "StorageID", "SR", "0001", txtStorageID);
            btnSave.Enabled = true;
            txtStorageType.Text = "";
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
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");

            SqlDataReader sqlread = boperate.getread("select StorageType from tb_StorageInfo where StorageType='" + txtStorageType.Text.Trim() + "'");
            sqlread.Read();
            if (M_int_judge == 0)
            {
                if (txtStorageType.Text == "")
                {
                    MessageBox.Show("仓库类型不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);



                }
                else if (sqlread.HasRows)
                {
                    MessageBox.Show("该仓库类型已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtStorageType.Text = "";
                    txtStorageType.Focus();
                }
                else
                {
                    boperate.getcom("insert into tb_StorageInfo(StorageID,Year,Month,StorageType,Maker,Date) values('"
                        + txtStorageID.Text.Trim() + "','" + year + "','" + month + "','" + txtStorageType.Text.Trim() +
                        "','" + FrmLogin.M_str_name + "','" + varDate + "')");
                    frmStorageInfo_Load(sender, e);
                    //MessageBox.Show("仓库信息添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = false;
                }
            }
            sqlread.Close();
            if (M_int_judge == 1)
            {
                if (txtStorageType.Text == "")
                {
                    MessageBox.Show("仓库类型不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    boperate.getcom("update tb_StorageInfo set StorageID='" + txtStorageID.Text.Trim()
                        + "',StorageType='" + txtStorageType.Text.Trim() +
                         "' ,Maker='" + FrmLogin.M_str_name + "',Date='" + varDate + "' where StorageID='" + txtStorageID.Text.Trim() + "'");
                    frmStorageInfo_Load(sender, e);
                    //MessageBox.Show("仓库信息修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = false;
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                string v1 = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
                DataTable dt1 = boperate.getdt("select * from tb_gode where storageID='" + v1 + "'");
                DataTable dt2 = boperate.getdt("select * from tb_matere where StorageID='" + v1 + "'");

                if (dt1.Rows.Count > 0)
                {
                    MessageBox.Show("此仓库已有入库信息，不允许删除！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dt2.Rows.Count > 0)
                {

                    MessageBox.Show("此仓库已有领料信息，不允许删除！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {

                    if (MessageBox.Show("确定要删除该条仓库信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        boperate.getcom("delete from tb_StorageInfo where StorageID='" + v1 + "' ");
                        frmStorageInfo_Load(sender, e);
                        //MessageBox.Show("删除数据成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Bind();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
