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
    public partial class FrmOutSourcingGodE : Form
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dtx = new DataTable();
        C23.BaseClass.BaseOperate boperate = new C23.BaseClass.BaseOperate();
        C23.BaseClass.OperateAndValidate opAndvalidate = new C23.BaseClass.OperateAndValidate();
        protected string M_str_sql = @"select A.OGID AS 入库单号,A.woid AS 工单号,C.wareid AS 品号,
C.WNAME AS 品名,C.ExternalM as 套件,C.Type as 型号,C.DETAIL AS 细节,C.Leather AS 皮种,C.COLOR AS 颜色,
C.StitchingC AS 线色,C.Thickness AS 海棉厚度,SUM(A.OGCOUNT) AS 入库数量 from tb_outsourcingGodE A
LEFT JOIN TB_WORKORDER B ON A.WOID=B.WOID AND A.SN=B.SN
LEFT JOIN TB_WAREINFO C ON C.WAREID=A.WAREID";
        protected int M_int_judge, i, look;
        protected int getdata;
        public static string[] data1 = new string[] { null, null };
        public static string[] data2 = new string[] { "", "" };
        public static string[] data3 = new string[] { "" };
        public static string[] data4 = new string[] { "" };
        string sql1 = "select distinct(woid) from tb_OutSourcingGodE";
        string sql4 = "select distinct(B.WNAME)  AS WNAME from tb_outsourcingGodE A LEFT JOIN TB_WAREINFO B ON A.WAREID=B.WAREID ";
        public FrmOutSourcingGodE()
        {
            InitializeComponent();
        }
        private void FrmOMrkGroup_Load(object sender, EventArgs e)
        {
            Bind();
        }
        #region Bind
        private void Bind()
        {
            dt1 = boperate.getdt(sql1);
            AutoCompleteStringCollection inputInfoSource = new AutoCompleteStringCollection();
            AutoCompleteStringCollection inputInfoSource4 = new AutoCompleteStringCollection();
            foreach (DataRow dr in dt1.Rows)
            {
                //cmbType.Items.Add(dr[0].ToString ());
                inputInfoSource.Add(dr[0].ToString());


            }
            this.cmb1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmb1.AutoCompleteCustomSource = inputInfoSource;

            foreach (DataRow dr in boperate.getdt(sql4).Rows)
            {
                cmb2.Items.Add(dr[0].ToString());
                inputInfoSource4.Add(dr[0].ToString());
            }
            this.cmb2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmb2.AutoCompleteCustomSource = inputInfoSource4;
            dgvStateControl();

        }
        #endregion
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
                if (i == 13 || i == 12 || i == 18)
                {
                    dataGridView1.Columns[i].Width = 120;

                }
                else if (i == 0 || i == 1 || i == 2 || i == 11 || i == 14 || i == 16 || i == 17)
                {
                    dataGridView1.Columns[i].Width = 90;

                }
                else if (i == 3)
                {
                    dataGridView1.Columns[i].Width = 200;

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
                if (i == 6)
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


        private void tsbtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定要删除该条品号信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {

                    Bind();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }


        #region override enter
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

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmOutSourcingGodET frm = new FrmOutSourcingGodET();
            FrmOutSourcingGodET.str6[0] = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            FrmOutSourcingGodET.str6[1] = Convert.ToString(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            FrmOutSourcingGodET.str6[2] = Convert.ToString(dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            FrmOutSourcingGodET.str6[3] = Convert.ToString(dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            FrmOutSourcingGodET.str6[4] = Convert.ToString(dataGridView1[11, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            frm.ShowDialog();
        }
        private void chk2_CheckedChanged(object sender, EventArgs e)
        {
            if (chk2.Checked)
            {
                dtpStartDate.Enabled = true;
                dtpEndDate.Enabled = true;
            }
            else
            {
                dtpStartDate.Enabled = false;
                dtpEndDate.Enabled = false;

            }
        }

        private void cmb1_DropDown(object sender, EventArgs e)
        {
            cmb1.DataSource = dt1;
            cmb1.DisplayMember = "WOID";
        }

        private void btnToExcel_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                boperate.dgvtoExcel(dataGridView1, "委外加工入库明细");
            }
            else
            {
                MessageBox.Show("没有数据可导出！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string a1 = boperate.numN(12, 4, "0001", "select * from tb_OutSourcingGodE", "OGID", "OG");
            if (a1 == "Exceed Limited")
            {
                MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                FrmOutSourcingGodET.str1[0] = a1;
                FrmOutSourcingGodET frm = new FrmOutSourcingGodET();
                frm.Show();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string v1 = dtpStartDate.Value.ToString("yyyy/MM/dd 00:00:00").Replace("-", "/");
                string v2 = dtpEndDate.Value.ToString("yyyy/MM/dd 23:59:59").Replace("-", "/");
                string sql;
                if (chk2.Checked)
                {
                    sql = @" WHERE A.WOID LIKE '%" + cmb1.Text + "%' AND A.DATE BETWEEN '" + v1 + "' AND '" + v2 +
              "' AND C.WNAME LIKE '%" + cmb2.Text +
              "%'   GROUP BY A.OGID,A.WOID,C.WAREID,C.WNAME,C.EXTERNALM,C.TYPE,C.DETAIL,C.LEATHER,C.COLOR,C.STITCHINGC,"
              +" C.THICKNESS ORDER BY A.OGID  ASC";

                }
                else
                {
                    sql = @" WHERE A.WOID LIKE '%" + cmb1.Text + "%'  AND C.WNAME LIKE '%" + cmb2.Text +
              "%'   GROUP BY A.OGID,A.WOID,C.WAREID,C.WNAME,C.EXTERNALM,C.TYPE,C.DETAIL,C.LEATHER,C.COLOR,C.STITCHINGC,"
              + " C.THICKNESS ORDER BY A.OGID  ASC";

                }
                dt = boperate.getdt(M_str_sql + sql);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["入库数量"] = dt.Compute("SUM(入库数量)", "").ToString();
                    dt.Rows.Add(dr);
                    dataGridView1.DataSource = dt;
                    dgvStateControl();
                }
                else
                {
                    MessageBox.Show("没有找到相关记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;

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
