using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace C23.BomManage
{
    public partial class FrmExternalM : Form
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        C23.BaseClass.BaseOperate boperate = new C23.BaseClass.BaseOperate();
        C23.BaseClass.OperateAndValidate opAndvalidate = new C23.BaseClass.OperateAndValidate();
        protected string M_str_sql = "select ExternalM as 套件,Maker as 制单人,Date as 日期 from tb_ExternalM ";
        protected string M_str_table = "tb_ExternalM ";
        protected int M_int_judge, i;
        protected int getdata;

        public FrmExternalM()
        {
            InitializeComponent();
        }
        #region Bind
        private void Bind()
        {
            btnSave.Enabled = false;
            this.dataGridView1.ReadOnly = true;

            dt = boperate.getdt(M_str_sql + " order by date asc");
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
                if (i == 2)
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
            txtExternalM.Text = "";


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtExternalM.Text = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
        }

        private void FrmExternalM_Load(object sender, EventArgs e)
        {
            Bind();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            txtExternalM.Text = "";
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
                string varDate = DateTime.Now.ToString();

                if (txtExternalM.Text == "")
                {
                    MessageBox.Show("套件不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (M_int_judge == 0)
                    {
                        dt1 = boperate.getdt("select ExternalM from tb_ExternalM where  ExternalM='" + txtExternalM.Text + "'");
                        if (dt1.Rows.Count > 0)
                        {
                            MessageBox.Show("套件已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);



                        }
                        else
                        {
                            boperate.getcom("insert into tb_ExternalM(ExternalM,Maker,Date) values('"
                                               + txtExternalM.Text.Trim() + "','" + FrmLogin.M_str_name + "','" + varDate + "')");
                            Bind();

                        }

                    }
                    else
                    {
                        dt2 = boperate.getdt("select  ExternalM from tb_ExternalM");
                        if (dt2.Rows.Count > 0)
                        {
                            boperate.getcom(@"update tb_ExternalM set  ExternalM='" + txtExternalM.Text + "',Maker='" + FrmLogin.M_str_name +
                             "',Date='" + varDate +
                             "' where ExternalM='" + Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim() + "'");
                            Bind();
                        }
                        else
                        {

                            MessageBox.Show("无数据可以更新！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }


                    }



                }
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
                if (MessageBox.Show("确定要删除该条品号信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    boperate.getcom("delete from tb_ExternalM where  ExternalM='" + Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim() + "'");
                    Bind();
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
                if (txtKeyWord.Text == "")
                {

                }
                else
                {
                    DataSet myds = boperate.getds(M_str_sql + " where  ExternalM like '%" + txtKeyWord.Text.Trim() + "%' order by Date asc", M_str_table);
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
