using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace C23.WorkOrderManage
{
    public partial class FrmVoucherProterties : Form
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        C23.BaseClass.BaseOperate boperate = new C23.BaseClass.BaseOperate();
        C23.BaseClass.OperateAndValidate opAndvalidate = new C23.BaseClass.OperateAndValidate();
        protected string M_str_sql = "select VPID AS 单据性质代码,VoucherProperties as 单据性质,Maker as 制单人,Date as 日期 from tb_VoucherProperties";
        protected string M_str_table = "tb_VoucherProperties";
        protected int M_int_judge, i;
        protected int getdata;

        public FrmVoucherProterties()
        {
            InitializeComponent();
        }
        #region init

        #endregion


        private void FrmVoucherProterties_Load(object sender, EventArgs e)
        {

            Bind();

        }
        #region Bind
        private void Bind()
        {
            btnSave.Enabled = false;
            this.dataGridView1.ReadOnly = true;
            dt = boperate.getdt(M_str_sql + "  order by VPID,date asc");
            dataGridView1.DataSource = dt;
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
        #endregion
        #region dgvStateControl
        private void dgvStateControl()
        {
            int i;
            int numCols = dataGridView1.Columns.Count;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Lavender;
            for (i = 0; i < numCols; i++)
            {

                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i == 3)
                {
                    dataGridView1.Columns[i].Width = 120;

                }
                else
                {
                    dataGridView1.Columns[i].Width = 90;

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
        }
        #endregion


 
        private void ClearText()
        {
            txtName.Text = "";


        }
        #region save
        private void save()
        {

            string varDate = DateTime.Now.ToString();
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            if (txtName.Text == "")
            {
                MessageBox.Show("单据性质不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (M_int_judge == 0)
                {
                    dt1 = boperate.getdt("select VoucherProperties from tb_VoucherProperties where VoucherProperties='" + txtName.Text + "'");
                    if (dt1.Rows.Count > 0)
                    {
                        MessageBox.Show("单据性质已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);



                    }
                    else if (txtID.Text == "")
                    {

                        MessageBox.Show("单据性质代码不能为空！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        boperate.getcom("insert into tb_VoucherProperties(VPID,VoucherProperties,Maker,Date,Year,Month,Day) values('" + txtID.Text +
                            "','" + txtName.Text.Trim() + "','" + FrmLogin.M_str_name + "','" + varDate +
                            "','" + year + "','" + month + "','" + day + "')");
                        Bind();

                    }

                }
                else
                {
                    dt2 = boperate.getdt("select VoucherProperties from tb_VoucherProperties");
                    if (dt2.Rows.Count > 0)
                    {
                        boperate.getcom(@"update tb_VoucherProperties set VoucherProperties='" + txtName.Text + "',Maker='" + FrmLogin.M_str_name +
                         "',Date='" + varDate +
                         "' where VoucherProperties='" + Convert.ToString(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value).Trim() + "'");
                        Bind();
                    }
                    else
                    {

                        MessageBox.Show("无数据可以更新！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }


                }



            }

        }
        #endregion

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            txtName.Text = Convert.ToString(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value).Trim();
        }


        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.dataGridView1.ReadOnly == true) //判断如果是在进货单中生成的窗体则不响应DataGrid的双击事件
            {
                int intCurrentRowNumber = this.dataGridView1.CurrentCell.RowIndex;
                string v1, v2;
                v1 = this.dataGridView1.Rows[intCurrentRowNumber].Cells[0].Value.ToString().Trim();
                v2 = this.dataGridView1.Rows[intCurrentRowNumber].Cells[1].Value.ToString().Trim();


                if (getdata == 0)
                {
                    C23.BomManage.FrmWorkGroupMT.data4[0] = "doubleclick";
                    C23.BomManage.FrmWorkGroupMT.data1[0] = v1;
                    C23.BomManage.FrmWorkGroupMT.data1[1] = v2;
                }

                this.Close();
            }
        }
        public void a1()
        {
            this.dataGridView1.ReadOnly = true;
            getdata = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string a1 = boperate.numN(12, 4, "0001", "select * from tb_VoucherProperties", "VPID", "VP");
            if (a1 == "Exceed Limited")
            {
                MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                txtID.Text = a1;

            }
            btnSave.Enabled = true;
            txtName.Text = "";
            M_int_judge = 0;
            ClearText();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            M_int_judge = 1;
            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {

                string v1 = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
                string v2 = Convert.ToString(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value).Trim();
                DataTable dty = boperate.getdt("select * from tb_WorkOrder where vpid='" + v1 + "'");
                if (dty.Rows.Count > 0)
                {
                    MessageBox.Show("该单据性质已经在工单中使用了，不允许删除！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (MessageBox.Show("确定要删除该条品号信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        boperate.getcom("delete from tb_VoucherProperties where VoucherProperties='" + v2 + "'");
                        Bind();
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
            try
            {
                if (textBox1.Text == "")
                {

                }
                else
                {
                    DataSet myds = boperate.getds(M_str_sql + " where VoucherProperties like '%" + textBox1.Text + "%' order by VPID,Date asc", M_str_table);
                    if (myds.Tables[0].Rows.Count > 0)
                        dataGridView1.DataSource = myds.Tables[0];
                    else
                        MessageBox.Show("没有要查找的相关记录！");

                }
                dgvStateControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
