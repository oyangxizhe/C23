using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace C23.StorageManage
{
    public partial class FrmOSProcessCost: Form
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        C23.BaseClass.BaseOperate boperate = new C23.BaseClass.BaseOperate();
        C23.BaseClass.OperateAndValidate opAndvalidate = new C23.BaseClass.OperateAndValidate();

        string sql1 = @"
SELECT A.OSSTORAGEID AS 委外厂仓库代码,B.STORAGETYPE AS 委外厂仓库,SUM(A.PROCESSCOST) AS 加工费 
FROM TB_OUTSOURCINGGODE A
LEFT JOIN TB_STORAGEINFO B ON A.OSSTORAGEID=B.STORAGEID";

        string sql2 = @"select A.OGID AS 委外入库单号 ,A.WOID AS 工单号,A.WAREID AS 品号,
C.WNAME AS 品名,C.ExternalM as 套件,C.Type as 型号,C.DETAIL AS 细节,C.Leather AS 皮种,C.COLOR AS 颜色,
C.StitChingC AS 线色,C.ThiCkness AS 海棉厚度,
A.OSSTORAGETYPE AS  委外厂仓库,B.STORAGETYPE AS 入库仓库,A.OSUNITPRICE AS 委外加工单价,
A.OGCOUNT AS 委外入库数量,A.PROCESSCOST AS 委外加工费, A.MAKER AS 制单人,A.DATE AS 制单日期 
from tb_outsourcinggode A LEFT JOIN TB_STORAGEINFO B ON A.STORAGEID=B.STORAGEID
LEFT JOIN TB_WAREINFO C ON A.WAREID=C.WAREID";

        string sql3="select DISTINCT(OSSTORAGETYPE) FROM TB_OUTSOURCINGGODE";
        public FrmOSProcessCost()
        {
            InitializeComponent();
        }
        #region init
      
        #endregion


        private void FrmWorkGroup_Load(object sender, EventArgs e)
        {
            dt1 = boperate.getdt(sql3);
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


                if (i==11 || i==12 || i == 17)
                {
                    dataGridView1.Columns[i].Width = 120;

                }
                else if (i == 0 || i == 1 || i == 2 || i == 14 || i == 13 || i == 15 || i==16)
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
        #region dgvStateControl
        private void dgvStateControl2()
        {
            int i;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Lavender;
            int numCols1 = dataGridView1.Columns.Count;
            for (i = 0; i < numCols1; i++)
            {

                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

            

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
        #region search
        private void search()
        {
            string v1 = dtpStartDate.Value.ToString("yyyy/MM/dd 00:00:00").Replace("-", "/");
            string v2 = dtpEndDate.Value.ToString("yyyy/MM/dd 23:59:59").Replace("-", "/");
            if (cmb1.Text == "")
            {
                MessageBox.Show("查询内容不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (cmb2.Enabled == false && dtpStartDate.Enabled == false)
            {
                MessageBox.Show("需选中查询条件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {

                if (cmb1.Text == "查加工费统计")
                {
                    string s1 = sql1 + " WHERE B.STORAGETYPE LIKE '%" + cmb2.Text + "%' GROUP BY A.OSSTORAGEID,B.STORAGETYPE ORDER BY A.OSSTORAGEID";
                    string s2 = sql1 + " WHERE A.DATE BETWEEN '" + v1 + "' AND '" + v2 + "' GROUP BY A.OSSTORAGEID,B.STORAGETYPE ORDER BY A.OSSTORAGEID";
                    string s3 = @sql1 + " WHERE A.DATE BETWEEN '" + v1 + "' AND '" + v2 +
                    "' AND  B.STORAGETYPE LIKE '%" + cmb2.Text + "%' GROUP BY A.OSSTORAGEID,B.STORAGETYPE ORDER BY A.OSSTORAGEID";

                    ak(s1, s2, s3);
                }
                else if (cmb1.Text == "查加工费明细")
                {
                    string s4 = sql2 + " WHERE A.OSSTORAGETYPE LIKE '%" + cmb2.Text + "%'  ORDER BY A.OSSTORAGEID,A.DATE";
                    string s5 = sql2 + " WHERE A.DATE BETWEEN '" + v1 + "' AND '" + v2 + "'  ORDER BY A.OSSTORAGEID,A.DATE";
                    string s6 = @sql2 + " WHERE A.DATE BETWEEN '" + v1 + "' AND '" + v2 +
                    "' AND  A.OSSTORAGETYPE LIKE '%" + cmb2.Text + "%' ORDER BY A.OSSTORAGEID,A.DATE";
                    ak(s4, s5, s6);

                }

            }
        }
        #endregion
        private void ak(string s1,string s2,string s3)
        {
           
            if (cmb2.Enabled == true && dtpStartDate.Enabled == false)
            {
               
                if (cmb2.Text == "")
                {
                    MessageBox.Show("委外仓库不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dt = boperate.getdt(s1);
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
            else if (cmb2.Enabled == false && dtpStartDate.Enabled == true)
            {
                if (boperate.juagedate(dtpStartDate.Value, dtpEndDate.Value) == 1)
                {
                    dt = boperate.getdt(s2);

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
            else if (cmb2.Enabled == true && dtpStartDate.Enabled == true)
            {

                if (cmb2.Text == "")
                {
                    MessageBox.Show("委外仓库不能为空！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else if (boperate.juagedate(dtpStartDate.Value, dtpEndDate.Value) == 0)
                {
                    dt = null;


                }
                else
                {
                    dt = boperate.getdt(s3);
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
        private void btnToExcel_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                boperate.dgvtoExcel(dataGridView1,"委外加工费明细");
            }
            else
            {
                MessageBox.Show("没有数据可导出！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
         
        }

    
        private void chk1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk1.Checked)
            {
                cmb2.Enabled = true;

            }
            else
            {

                cmb2.Enabled = false;
            }
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmb2_DropDown(object sender, EventArgs e)
        {
            cmb2.DataSource = dt1;
            cmb2.DisplayMember = "OSSTORAGETYPE";
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            int i;
            for (i = 0; i < dataGridView1.Columns.Count; i++)
            {
                if (dataGridView1.Columns[i].ValueType.ToString() == "System.Decimal")
                {
                    dataGridView1.Columns[i].DefaultCellStyle.Format = "N";
                    dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                }

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                search();
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
   



  

 