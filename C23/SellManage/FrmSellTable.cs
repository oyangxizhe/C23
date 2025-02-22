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

namespace C23.SellManage
{
    public partial class FrmSellTable : Form
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dtx = new DataTable();
        C23.BaseClass.BaseOperate boperate = new C23.BaseClass.BaseOperate();
        C23.BaseClass.OperateAndValidate opAndvalidate = new C23.BaseClass.OperateAndValidate();
  
        protected int M_int_judge, i,j, look;
        protected int getdata;
        public static string[] data1 = new string[] { null, null };
        public static string[] data2 = new string[] { "", "" };
        public static string[] data3 = new string[] { "" };
        public static string[] data4 = new string[] { "" };
        string sql1 = "select distinct(SEID) from tb_selltable";
        string sql2 = "select distinct(CUID) from tb_selltable";
        string sql3 = "select distinct(B.CNAME)  AS CNAME from tb_selltable A LEFT JOIN TB_CUSTOMERINFO B ON A.CUID=B.CUID ";
        string sql4 = "select distinct(B.WNAME)  AS WNAME from tb_selltable A LEFT JOIN TB_WAREINFO B ON A.WAREID=B.WAREID ";
        string M_str_sql = @"select A.SEID AS SEID,A.SN AS SN,A.WAREID AS WAREID,C.WNAME AS WNAME,A.SECOUNT AS SECOUNT,A.MAKER AS MAKER,A.DATE AS DATE,
A.SELLUNITPRICE AS SELLUNITPRICE,A.URGENT AS URGENT,B.CNAME from tb_SellTable A 
LEFT JOIN TB_CUSTOMERINFO B ON A.CUID=B.CUID
LEFT JOIN TB_WAREINFO C ON A.WAREID=C.WAREID";
        



        public FrmSellTable()
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
            AutoCompleteStringCollection inputInfoSource1 = new AutoCompleteStringCollection();
            AutoCompleteStringCollection inputInfoSource2 = new AutoCompleteStringCollection();
            foreach (DataRow dr in dt1.Rows)
            {
             
                inputInfoSource.Add(dr[0].ToString());
            }
            this.cmb1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmb1.AutoCompleteCustomSource = inputInfoSource;

          
            foreach (DataRow dr in boperate.getdt(sql3).Rows)
            {

                inputInfoSource1.Add(dr[0].ToString());
            }
            this.cmb3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmb3.AutoCompleteCustomSource = inputInfoSource1;
            dgvStateControl();

            foreach (DataRow dr in boperate.getdt(sql4).Rows)
            {

                inputInfoSource2.Add(dr[0].ToString());
            }
            this.cmb4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmb4.AutoCompleteCustomSource = inputInfoSource2;
            dgvStateControl();
            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
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
                if ( i == 18 || i == 19 || i==22 || i==3)
                {
                    dataGridView1.Columns[i].Width = 120;

                }
                else if (i == 0 || i == 2 || i==3|| i == 11 || i == 12 || i == 13 || i == 14 || i == 16 || i == 20 || i == 21 )
                {
                    dataGridView1.Columns[i].Width = 90;

                }
                else if (i == 15 || i==17 || i==23 )
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
                dataGridView1.Columns[i].Visible = true;
                dataGridView1.Columns[i].ReadOnly = true;
            }
        }
        #endregion
        #region look
        private void ak()
        {
           
            string v1 = dtpStartDate.Value.ToString("yyyy/MM/dd 00:00:00").Replace("-", "/");
            string v2 = dtpEndDate.Value.ToString("yyyy/MM/dd 23:59:59").Replace("-", "/");
            string sql;
            if (checkBox1.Checked)
            {
                sql = @" WHERE A.SEID LIKE '%" + cmb1.Text + "%' AND A.CUID LIKE '%" + cmb2.Text + "%' AND B.CNAME LIKE '%" + cmb3.Text +
          "%' AND A.DATE BETWEEN '" + v1 + "' AND '" + v2 +
          "' AND C.WNAME LIKE '%"+cmb4.Text +"%' ORDER BY A.SEID,A.SN ASC";

            }
            else
            {
                sql = @" WHERE A.SEID LIKE '%" + cmb1.Text + "%' AND A.CUID LIKE '%" + cmb2.Text + "%' AND B.CNAME LIKE '%" + cmb3.Text +
          "%' AND C.WNAME LIKE '%" + cmb4.Text + "%' ORDER BY A.SEID,A.SN ASC";

            }
            dt = boperate.ask(M_str_sql +sql+" " , 0, 0 );
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
                dgvStateControl();
            }
            else
            {
                MessageBox.Show("没有找到相关记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dt = null;
                dataGridView1.DataSource = dt;


            }
        
           
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
            FrmSellTableT frm = new FrmSellTableT();
            FrmSellTableT.str6[0] = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            FrmSellTableT.str6[2] = Convert.ToString(dataGridView1[14, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            FrmSellTableT.str6[3] = Convert.ToString(dataGridView1[15, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            FrmSellTableT.str6[4] = Convert.ToString(dataGridView1[16, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            FrmSellTableT.str6[5] = Convert.ToString(dataGridView1[17, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            frm.ShowDialog();
        }

        private void cmb1_DropDown(object sender, EventArgs e)
        {
            cmb1.DataSource = dt1;
            cmb1.DisplayMember = "SEID";
        }
        private void cmb2_DropDown(object sender, EventArgs e)
        {
            cmb2.DataSource = boperate.getdt(sql2);
            cmb2.DisplayMember = "CUID";
        }
        private void cmb3_DropDown(object sender, EventArgs e)
        {
            cmb3.DataSource = boperate.getdt(sql3);
            cmb3.DisplayMember = "CNAME";
        }
        private void cmb4_DropDown(object sender, EventArgs e)
        {
            cmb4.DataSource = boperate.getdt(sql4);
            cmb4.DisplayMember = "WNAME";
        }
        private void btnToExcel_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                boperate.dgvtoExcel(dataGridView1, "销货单明细");
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
            string a1 = boperate.numN(12, 4, "0001", "select * from tb_SellTable", "SEID", "SE");
            if (a1 == "Exceed Limited")
            {
                MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                FrmSellTableT.str1[0] = a1;
                FrmSellTableT frm = new FrmSellTableT();
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
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

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

  

   

   
    }
}
