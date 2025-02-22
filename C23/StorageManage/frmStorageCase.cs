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
    public partial class frmStorageCase : Form
    {
        DataTable dt = new DataTable();
        C23.BaseClass.BaseOperate boperate = new C23.BaseClass.BaseOperate();

        protected int select,i;
        public frmStorageCase()
        {
            InitializeComponent();
        }
        private void frmStorageCase_Load(object sender, EventArgs e)
        {
           
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

                if (i == 0 || i==10)
                {
                    dataGridView1.Columns[i].Width = 90;
                }
                else if (i == 1)
                {
                    dataGridView1.Columns[i].Width = 200;


                }
                else if (i == 9)
                {
                    dataGridView1.Columns[i].Width = 120;

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
        private void bind()
        {
            dt = boperate.getstoragecount();
            dataGridView1.DataSource = dt;
            dgvStateControl();
            for (i = 0; i < dataGridView1.Columns.Count - 1; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
               
            }


        }

        private void condition(string sql)
        {
          
            dt = boperate.getstoragecount();
            DataRow[] dr = dt.Select(sql);
            if (dr.Length > 0)
            {
                DataTable dtu = boperate.getstoragetable();

                for (i = 0; i < dr.Length; i++)
                {
                    DataRow dr1 = dtu.NewRow();
                    dr1["品号"] = dr[i]["品号"].ToString();
                    dr1["品名"] = dr[i]["品名"].ToString();
                    dr1["套件"] = dr[i]["套件"].ToString();
                    dr1["型号"] = dr[i]["型号"].ToString();
                    dr1["细节"] = dr[i]["细节"].ToString();
                    dr1["皮种"] = dr[i]["皮种"].ToString();
                    dr1["颜色"] = dr[i]["颜色"].ToString();
                    dr1["线色"] = dr[i]["线色"].ToString();
                    dr1["海棉厚度"] = dr[i]["海棉厚度"].ToString();
                    dr1["仓库"] = dr[i]["仓库"].ToString();
                    dr1["库存数量"] = dr[i]["库存数量"].ToString();
                    if (dr[i]["可用否"].ToString() == "Y")
                    {
                        dr1["可用否"] = "可用";
                    }
                    else
                    {
                        dr1["可用否"] = "不可用";
                    }
            
      
                    dtu.Rows.Add(dr1);

                }
                DataRow dr2= dtu.NewRow();
                dr2["库存数量"] = dtu.Compute("SUM(库存数量)", "").ToString();
                dtu.Rows.Add(dr2);
                dataGridView1.DataSource = dtu;
                dgvStateControl();
         
            }
            else
            {

                MessageBox.Show("没有要查找的相关记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = null;
            }


        }
        private void dgvStorageCaseInfo_DoubleClick(object sender, EventArgs e)
        {
            if (this.dataGridView1.ReadOnly == true)
            {
                int intCurrentRowNumber = this.dataGridView1.CurrentCell.RowIndex;
                string sendSType, sendSName;

                sendSType = this.dataGridView1.Rows[intCurrentRowNumber].Cells[9].Value.ToString().Trim();
                sendSName = this.dataGridView1.Rows[intCurrentRowNumber].Cells[10].Value.ToString().Trim();

                string[] sendArray = new string[] { sendSType, sendSName };
                if (select == 1)
                {
                    C23.StorageManage.FrmMiscMateReT.data5[0] = "doubleclick";
                    C23.StorageManage.FrmMiscMateReT.data6[0] = sendArray[0];
                    C23.StorageManage.FrmMiscMateReT.data6[1] = sendArray[1];


                }

                if (select == 2)
                {
                    C23.StorageManage.FrmOutSourcingMateReT.data5[0] = "doubleclick";
                    C23.StorageManage.FrmOutSourcingMateReT.data6[0] = sendArray[0];
                    C23.StorageManage.FrmOutSourcingMateReT.data6[1] = sendArray[1];
                }


                if (select == 3)
                {
                    C23.SellManage.FrmSellTableT.data5[0] = "doubleclick";
                    C23.SellManage.FrmSellTableT.data6[0] = sendArray[0];
                    C23.SellManage.FrmSellTableT.data6[1] = sendArray[1];
                }

                this.Close();
            }

        }
        public void a1()
        {
            this.dataGridView1.ReadOnly = true;
            select = 1;
        }
        public void a2()
        {
            this.dataGridView1.ReadOnly = true;
            select = 2;
        }
        public void a3()
        {
            this.dataGridView1.ReadOnly = true;
            select = 3;
        }
        private void dgvStorageCaseInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvStorageCaseInfo_DataSourceChanged(object sender, EventArgs e)
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

        private void btnToExcel_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                boperate.dgvtoExcel(dataGridView1, "库存明细");
            }
            else
            {
                MessageBox.Show("没有数据可导出！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string t1 = textBox1.Text;
                string t2 = textBox2.Text;
                string t3 = textBox3.Text;
                condition("品号 like '%" + t1 + "%'AND 品名  like '%" + t2 + "%' AND 仓库  like '%" + t3 +
                    "%' AND 可用否  like '%" + cmbACTIVE .Text  + "%'");
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
