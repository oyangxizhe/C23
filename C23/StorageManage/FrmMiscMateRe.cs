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
    public partial class FrmMiscMateRe : Form
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dtx = new DataTable();
        C23.BaseClass.BaseOperate boperate = new C23.BaseClass.BaseOperate();
        C23.BaseClass.OperateAndValidate opAndvalidate = new C23.BaseClass.OperateAndValidate();
        protected string M_str_sql = @"select MATEREID AS 领料单号,A.SN AS 项次,C.wareid AS 品号,C.WNAME AS 品名,C.ExternalM as 套件,
C.Type as 型号,C.DETAIL AS 细节,C.Leather AS 皮种,C.COLOR AS 颜色,C.StitchingC AS 线色,C.Thickness AS 海棉厚度,
D.STORAGETYPE AS 仓库,A.MRCOUNT AS 领料数量,A.MATERER AS 领料车间,A.MATEREDATE AS 领料日期,A.MAKER AS 制单人,A.DATE AS 制单日期  from tb_matere A
LEFT JOIN TB_WAREINFO C ON C.WAREID=A.WAREID
LEFT JOIN TB_STORAGEINFO D ON A.STORAGEID=D.STORAGEID";
        protected int M_int_judge, i, look;
        protected int getdata;
        public static string[] data1 = new string[] { null, null };
        public static string[] data2 = new string[] { "", "" };
        public static string[] data3 = new string[] { "" };
        public static string[] data4 = new string[] { "" };
        string sql1 = "select distinct(MateReID) from tb_MateRe";

        public FrmMiscMateRe()
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
                if (i==11 || i == 14 || i == 16)
                {
                    dataGridView1.Columns[i].Width = 120;

                }
                else if (i == 0 || i == 2 || i==12 || i == 13 || i == 15)
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

        #region look

        private void ak()
        {
            //FrmMiscMateReT.str3 = 1;
            string v1 = dtpStartDate.Value.ToString("yyyy/MM/dd 00:00:00").Replace("-", "/");
            string v2 = dtpEndDate.Value.ToString("yyyy/MM/dd 23:59:59").Replace("-", "/");
            if (cmb1.Enabled == true && dtpStartDate.Enabled == false)
            {
                if (cmb1.Text == "")
                {
                    MessageBox.Show("领料单号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dt = boperate.getdt(M_str_sql + " WHERE A.MATEREID LIKE '%" + cmb1.Text + "%' ORDER BY A.MATEREID ASC");
                    if (dt.Rows.Count > 0)
                    {

                    }
                    else
                    {

                        MessageBox.Show("没有找到相关记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dt = null;
                    }
                }

            }
            else if (cmb1.Enabled == false && dtpStartDate.Enabled == true)
            {
                if (boperate.juagedate(dtpStartDate.Value, dtpEndDate.Value) == 1)
                {
                    dt = boperate.getdt(M_str_sql + " WHERE A.DATE BETWEEN '" + v1 + "' AND '" + v2 + "' ORDER BY A.MATEREID  ASC");

                    if (dt.Rows.Count > 0)
                    {


                    }
                    else
                    {

                        MessageBox.Show("没有找到相关记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dt = null;


                    }
                }
                else
                {
                    dt = null;


                }
            }
            else if (cmb1.Enabled == true && dtpStartDate.Enabled == true)
            {

                if (cmb1.Text == "")
                {
                    MessageBox.Show("领料单号不能为空！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else if (boperate.juagedate(dtpStartDate.Value, dtpEndDate.Value) == 0)
                {
                    dt = null;


                }
                else
                {
                    dt = boperate.getdt(@M_str_sql + " WHERE A.DATE BETWEEN '" + v1 + "' AND '" + v2 +
                        "' AND  A.MATEREID LIKE '%" + cmb1.Text + "%' ORDER BY A.MATEREID ASC");
                    if (dt.Rows.Count > 0)
                    {


                    }
                    else
                    {
                        dt = null;
                        MessageBox.Show("没有找到相关记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }

                }


            }

            dataGridView1.DataSource = dt;
            dgvStateControl();
        }
        #endregion

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
            FrmMiscMateReT frm = new FrmMiscMateReT();
            FrmMiscMateReT.str6[0] = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            FrmMiscMateReT.str6[1] = Convert.ToString(dataGridView1[13, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            FrmMiscMateReT.str6[2] = Convert.ToString(dataGridView1[14, dataGridView1.CurrentCell.RowIndex].Value).Trim();

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

        private void chk1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk1.Checked)
            {
                cmb1.Enabled = true;

            }
            else
            {

                cmb1.Enabled = false;
            }
        }
        private void cmb1_DropDown(object sender, EventArgs e)
        {
            cmb1.DataSource = dt1;
            cmb1.DisplayMember = "MATEREID";
        }

        private void btnToExcel_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                boperate.dgvtoExcel(dataGridView1, "领料单明细");
            }
            else
            {
                MessageBox.Show("没有数据可导出！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
            string a1 = boperate.numN(12, 4, "0001", "select * from tb_MateRe", "MateReID", "MR");
            if (a1 == "Exceed Limited")
            {
                MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                FrmMiscMateReT.str1[0] = a1;
                FrmMiscMateReT frm = new FrmMiscMateReT();
                frm.Show();
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ak();
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
