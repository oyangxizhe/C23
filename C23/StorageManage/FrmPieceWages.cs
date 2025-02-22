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
    public partial class FrmPieceWages: Form
    {
        DataTable dt = new DataTable();
        C23.BaseClass.BaseOperate boperate = new C23.BaseClass.BaseOperate();
        C23.BaseClass.OperateAndValidate opAndvalidate = new C23.BaseClass.OperateAndValidate();

        string sql1 = @"SELECT A.GODEID AS 入库单号,A.SN AS 项次,A.WAREID AS 品号,F.WNAME AS 品名,F.ExternalM as 套件,
F.Type as 型号,F.DETAIL AS 细节,F.Leather AS 皮种,F.COLOR AS 颜色,F.StitchingC AS 线色,F.Thickness AS 海棉厚度,B.WORKGROUP AS 组名,
C.CRAFT AS 工艺,D.WORKER AS 工种,E.ENAME AS 姓名,A.CWUNITPRICE AS 基价,A.GECOUNT AS 
入库数量,A.PIECEWAGES AS 计件工资,A.GODEDATE AS 入库日期
FROM TB_PIECEWAGES A 
LEFT JOIN TB_WAREINFO F ON A.WAREID=F.WAREID 
LEFT JOIN TB_WORKGROUP B ON A.WGID=B.WGID
LEFT JOIN TB_CRAFT C ON C.CRID=A.CRID 
LEFT JOIN TB_WORKER D ON D.WKID=A.WKID
LEFT JOIN TB_EMPLOYEEINFO E ON E.EMPLOYEEID=A.EMPLOYEEID ";

        string sql2 = @"SELECT C.CRAFT AS 工艺,E.ENAME AS 姓名,SUM(A.PIECEWAGES) AS 工资
FROM TB_PIECEWAGES A 
LEFT JOIN TB_WAREINFO F ON A.WAREID=F.WAREID 
LEFT JOIN TB_WORKGROUP B ON A.WGID=B.WGID
LEFT JOIN TB_CRAFT C ON C.CRID=A.CRID 
LEFT JOIN TB_WORKER D ON D.WKID=A.WKID
LEFT JOIN TB_EMPLOYEEINFO E ON E.EMPLOYEEID=A.EMPLOYEEID ";

        string sql3 = @"select a.employeeid as 工号,b.ename as 姓名,sum(a.piecewages) as 工资 
from tb_piecewages a left join tb_employeeinfo b 
on a.employeeid=b.employeeid ";

        public FrmPieceWages()
        {
            InitializeComponent();
        }
        #region init
      
        #endregion


        private void FrmWorkGroup_Load(object sender, EventArgs e)
        {
            Bind();
        }
        #region Bind
        private   void Bind()
        {

       
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
                if (i == 13 || i == 12 || i==18)
                {
                    dataGridView1.Columns[i].Width = 120;

                }
                else if (i==0 || i == 2 || i==11 || i == 14 || i == 16 || i==17)
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
                if (i == 0)
                {
                    dataGridView1.Columns[i].Width = 120;
                }
        
                else
                {
                    dataGridView1.Columns[i].Width = 90;

                }
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

  
        private void ak()
        {
            //FrmPieceWagesT.str3 = 1;
            string v1 = dtpStartDate.Value.ToString("yyyy/MM/dd 00:00:00").Replace("-", "/");
            string v2 = dtpEndDate.Value.ToString("yyyy/MM/dd 23:59:59").Replace("-", "/");

            if (cmbCondition.Text == "")
            {

                MessageBox.Show("查询条件不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else  if(boperate.juagedate(dtpStartDate.Value, dtpEndDate.Value) == 1)
            {
                if (cmbCondition.Text == "入库计件工资明细")
                {
                    dt = boperate.getdt(sql1 + " WHERE A.GODEDATE BETWEEN '" + v1 + "' AND '" + v2 + "' order by A.GODEID,A.GODEDATE ASC");
                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt;
                        dgvStateControl();
                    }
                    else
                    {
                        MessageBox.Show("没有找到相关记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                   
                }
                else if (cmbCondition.Text == "工艺工号统计工资")
                {
                    dt = boperate.getdt(sql2 +
                        " WHERE A.GODEDATE BETWEEN '" + v1 + "' AND '" + v2 + "' GROUP BY C.CRAFT,E.ENAME ORDER BY C.CRAFT,E.ENAME ASC");
                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt;
                        dgvStateControl2();
                    }
                    else
                    {
                        MessageBox.Show("没有找到相关记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                }
                else if (cmbCondition.Text == "工号统计员工工资")
                {

                    dt = boperate.getdt(sql3 +
             " WHERE A.GODEDATE BETWEEN '" + v1 + "' AND '" + v2 + "' group by a.employeeid,b.ename ORDER BY A.EMPLOYEEID ASC");
                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt;
                        dgvStateControl2();
                    }
                    else
                    {
                        MessageBox.Show("没有找到相关记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
           

            }
         
        }

        private void btnToExcel_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                boperate.dgvtoExcel(dataGridView1,"工资明细");
            }
            else
            {
                MessageBox.Show("没有数据可导出！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
         
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
   



  

 