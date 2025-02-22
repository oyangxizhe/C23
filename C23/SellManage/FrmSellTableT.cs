using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace C23.SellManage
{
    public partial class FrmSellTableT : Form
    {
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dtd = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();
        DataTable dt6 = new DataTable();
        DataTable dtx2 = new DataTable();
        C23.BaseClass.BaseOperate boperate = new C23.BaseClass.BaseOperate();
        C23.BaseClass.OperateAndValidate opAndvalidate = new C23.BaseClass.OperateAndValidate();
        protected int i, j;
        public static string[] inputTextDataWare = new string[] { null, null, null, null, null, null, null, null, null };
        public static string[] data1 = new string[] { "" };
        public static string[] data2 = new string[] { "", "", "", "" };
        public static string[] data5 = new string[] { "" };
        public static string[] data6 = new string[] { "", "" };
        public static string[] data7 = new string[] { "" };
        public static string[] data8 = new string[] { "", "" };
        public static string[] inputTextDataLocation = new string[] { "" };
        public static string[] str1 = new string[] { "" };
        public static string[] str2 = new string[] { "", "" };
        public static string[] str4 = new string[] { "" };
        public static string[] str6 = new string[] { "", "", "", "", "", "", "" };
        public static string[] data3 = new string[] { "" };
        public static string[] data4 = new string[] { "" };
        public static string[] inputgetOEName = new string[] { "" };
        string sql = "SELECT * FROM TB_SELLTABLE ";

        string SEKEY, MRKEY;
        protected int M_int_judge, t;
        string[] a = new string[] { "", "加急" };
        public FrmSellTableT()
        {
            InitializeComponent();

        }
        private void FrmSellTableT_Load(object sender, EventArgs e)
        {

            try
            {
                bind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }
        private void cboxAcceptor_DropDown(object sender, EventArgs e)
        {
            C23.EmployeeManage.FrmEmployeeInfo frm = new C23.EmployeeManage.FrmEmployeeInfo();
            frm.GodET();
            frm.ShowDialog();

        }
        private void getGoderData()
        {
            this.cmb1.IntegralHeight = false;//使组合框不调整大小以显示其所有项
            this.cmb1.DroppedDown = false;//使组合框不显示其下拉部分
            this.cmb1.Items[0] = data2[0];
            this.cmb1.SelectedIndex = 0;
            this.cmb1.IntegralHeight = true;//恢复默认值
        }

        #region bind
        private void bind()
        {

            if (txt1.Text == "")
            {
                txt1.Text = str1[0];
    
            }
            if (str6[0] != "")
            {

                txt1.Text = str6[0];
                cmb1.Text = str6[2];
                txt3.Text = str6[3];
                txt4.Text = str6[4];
                txt5.Text = str6[5];
       
                dt3 = total1(txt1.Text);
                str6[0] = "";
                dtp1.Enabled = false;
                dtp2.Enabled = false;
     
                cmb1.Enabled = false;
                dataGridView1.DataSource = dt3;
                t = 1;

            }
            else
            {
                dt3 = total1(txt1.Text);
                if (cmb1.Text == "")
                {
                    dataGridView1.DataSource = null;
                }
                else if (dt3.Rows.Count > 0)
                {

                    dataGridView1.DataSource = dt3;
                    ask1(dt3);
                }
                else
                {
                    dt3 = total1();
                    dataGridView1.DataSource = dt3;
                    txtNoTax.Text = "";
                    if (t == 1)
                    {
                        txt1.Text = "";
                        cmb1.Text = "";
                        dtp2.Text = "";
                        dtp1.Text = "";
                        txt3.Text = "";
                        txt4.Text = "";
                        txt5.Text = "";
                    }
                }

            }

            dtd = total1(txt1.Text);
            if (dtd.Rows.Count > 0)
            {
                btnDel.Enabled = true;
       
                TSMI.Enabled = true;
                btnDel.Enabled = true;
         
            }
            else
            {
                btnDel.Enabled = false;
                TSMI.Enabled = false;
   
                btnDel.Enabled = false;
            }
            dgvStateControl();
        }
        #endregion
        #region dgvStateControl
        private void dgvStateControl()
        {
            int i;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Lavender;

            cmb1.BackColor = Color.Yellow;

            int numCols1 = dataGridView1.Columns.Count;
            for (i = 0; i < numCols1; i++)
            {

                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i == 17)
                {
                    dataGridView1.Columns[i].Width = 120;

                }
                else if (i == 1 || i == 11 || i == 13 || i == 14 || i == 15 || i == 16)
                {
                    dataGridView1.Columns[i].Width = 100;

                }
                else if (i == 12)
                {
                    dataGridView1.Columns[i].Width = 100;

                }
                else if (i == 2)
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
                if (i == 1 || i == 11 || i == 12 || i == 14)
                {
                    dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
            for (i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i == 10 || i == 11 || i == 14)
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

        #region total
        private DataTable total()
        {
            dt = new DataTable();

            dt.Columns.Add("项次", typeof(string));
            dt.Columns.Add("品号", typeof(string));
            dt.Columns.Add("品名", typeof(string));
            dt.Columns.Add("套件", typeof(string));
            dt.Columns.Add("型号", typeof(string));
            dt.Columns.Add("细节", typeof(string));
            dt.Columns.Add("皮种", typeof(string));
            dt.Columns.Add("颜色", typeof(string));
            dt.Columns.Add("线色", typeof(string));
            dt.Columns.Add("海棉厚度", typeof(string));
            dt.Columns.Add("加急否", typeof(string));
            dt.Columns.Add("销售单价", typeof(decimal));
            dt.Columns.Add("仓库", typeof(string));
            dt.Columns.Add("库存数量", typeof(decimal));
            dt.Columns.Add("销货数量", typeof(decimal));
            dt.Columns.Add("金额", typeof(decimal));
            dt.Columns.Add("制单人", typeof(string));
            dt.Columns.Add("制单日期", typeof(string));
            return dt;
        }
        #endregion
        #region total1
        private DataTable total1()
        {
            DataTable dtt2 = total();
            DataRow dr = dtt2.NewRow();
            dr["项次"] = "1";
            dtt2.Rows.Add(dr);
            return dtt2;
        }
        #endregion
        #region total1
        private DataTable total1(string v1)
        {
            DataTable dtt = total();
            DataTable dtx1 = boperate.getdt(sql + " WHERE SEID='" + v1 + "'");
            if (dtx1.Rows.Count > 0)
            {
                for (i = 0; i < dtx1.Rows.Count; i++)
                {
                    DataRow dr = dtt.NewRow();

                    dr["项次"] = dtx1.Rows[i]["SN"].ToString();
                    dr["品号"] = dtx1.Rows[i]["WAREID"].ToString();
                    dtx2 = boperate.getdt("select * from tb_wareinfo where wareid='" + dtx1.Rows[i]["WAREID"].ToString() + "'");
                    dr["品名"] = dtx2.Rows[0]["WNAME"].ToString();
                    dr["套件"] = dtx2.Rows[0]["ExternalM"].ToString();
                    dr["型号"] = dtx2.Rows[0]["TYPE"].ToString();
                    dr["细节"] = dtx2.Rows[0]["DETAIL"].ToString();
                    dr["皮种"] = dtx2.Rows[0]["Leather"].ToString();
                    dr["颜色"] = dtx2.Rows[0]["COLOR"].ToString();
                    dr["线色"] = dtx2.Rows[0]["StitchingC"].ToString();
                    dr["海棉厚度"] = dtx2.Rows[0]["Thickness"].ToString();
                    dr["销售单价"] = dtx1.Rows[i]["SELLUNITPRICE"].ToString();
                    dr["销货数量"] = dtx1.Rows[i]["SECOUNT"].ToString();
                    dr["加急否"] = dtx1.Rows[i]["URGENT"].ToString();
                    dr["制单人"] = dtx1.Rows[i]["MAKER"].ToString();
                    dr["制单日期"] = dtx1.Rows[i]["DATE"].ToString();
                    dr["金额"] = decimal.Parse(dtx1.Rows[i]["SELLUNITPRICE"].ToString()) * decimal.Parse(dtx1.Rows[i]["SECOUNT"].ToString());
                    DataTable dtx5 = boperate.getmaxstoragecount(dtx1.Rows[i]["WAREID"].ToString());
                    if (dtx5.Rows.Count > 0)
                    {
                        dr["仓库"] = dtx5.Rows[0]["仓库"].ToString();
                        dr["库存数量"] = dtx5.Rows[0]["库存数量"].ToString();
                    }


                    dtt.Rows.Add(dr);

                }
                DataRow dr1 = dtt.NewRow();
                int b = Convert.ToInt32(dtt.Rows[dtt.Rows.Count - 1]["项次"].ToString());
                dr1["项次"] = Convert.ToString(b + 1);
                dtt.Rows.Add(dr1);
                dtp1.Value = Convert.ToDateTime(dtx1.Rows[0]["ORDERDATE"].ToString());
                dtp2.Value = Convert.ToDateTime(dtx1.Rows[0]["DELIVERYDATE"].ToString());
                ask1(dtt);
            }
            return dtt;
        }
        #endregion
   

        private void btnAdd_Click(object sender, EventArgs e)
        {
            str6[0] = "";
            string a1 = boperate.numN(12, 4, "0001", "select * from tb_SellTable", "SEID", "SE");
            if (a1 == "Exceed Limited")
            {
                MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                txt1.Text = a1;

            }
  
            cmb1.Text = "";
            txt3.Text = "";
            txt4.Text = "";
            txt5.Text = "";
            cmb1.Enabled = true;
  
        }
        private void n1()
        {
            wf();
        }
        private void wf()
        {

            if (M_int_judge == 0)
            {

                insertdb(dt3);
            }
            else
            {
                if (dtd.Rows.Count > 0)
                {

                    boperate.getcom("delete tb_SellTable where SEID='" + txt1.Text + "'");
                    boperate.getcom("delete tb_MATERE where SEID='" + txt1.Text + "'");
                    insertdb(dt3);
                }
            }
        }
        private void insertdb(DataTable dtv)
        {
            at(dtv);
            dtd = total1(txt1.Text);

            if (dtd.Rows.Count > 0)
            {
                dt3 = dtd;
                dataGridView1.DataSource = dt3;
            }

            dgvStateControl();

        }
        #region at
        private void at(DataTable dt)
        {

            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            if (SEKEY == "Exceed Limited")
            {

                MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                i = dataGridView1.CurrentCell.RowIndex;
                if (dt.Rows[i]["品号"].ToString() != "")
                {
                    SEKEY = boperate.numN(20, 12, "000000000001", "select * from tb_SellTable", "SEKEY", "SE");
                    MRKEY = boperate.numN(20, 12, "000000000001", "select * from tb_MATERE", "MRKEY", "MR");
                    string varID = txt1.Text;
                    string v2 = cmb1.Text;
                    string varSN = dt.Rows[i]["项次"].ToString();
                    string v6 = dt.Rows[i]["品号"].ToString();

                    string v7 = dt.Rows[i]["销售单价"].ToString();
                    string v11 = dt.Rows[i]["销货数量"].ToString();
                    string v12 = dt.Rows[i]["仓库"].ToString();
                    string v13 = boperate.getstorageid(v12);
                    string v9 = dtp1.Value.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
                    string v10 = dtp2.Value.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
                    string v15 = dt.Rows[i]["加急否"].ToString();
                    string varMaker = FrmLogin.M_str_name;
                    string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
                    dt.Rows[i]["金额"] = decimal.Parse(v11) * decimal.Parse(dt.Rows[i]["销售单价"].ToString());
                    #region SellTable

                    boperate.getcom(@"insert into tb_SellTable(SEKEY,SEID,SN,WareID,SECOUNT,SELLUNITPRICE,ORDERDATE," +
                        "DELIVERYDATE,URGENT,CUID,MAKER,DATE, Year,Month,Day) values ('" + SEKEY + "','" + varID + "','" + varSN + "','" + v6 +
                    "','" + v11 + "','" + v7 + "','" + v9 + "','" + v10 + "','" + v15 + "','" + v2 +
                    "','" + varMaker + "','" + varDate + "','" + year + "','" + month +
                    "','" + day + "')");
                    #endregion

                    #region matere
                    boperate.getcom(@"insert into tb_matere(MRKEY,MateReID,SN,WAREID,MRCOUNT,STORAGEID,YEAR,MONTH,DAY,DATE) VALUES ('" + MRKEY + "','" + varID +
                        "','" + varSN + "','" + v6 + "','" + v11 + "','" + v13 + "','" + year + "','" + month + "','" + day + "','" + varDate + "')");
                    #endregion
                }
            }
        }
        #endregion
        #region del

        #endregion
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
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(dt.Rows.Count.ToString());
        }
        #region ac()
        private bool ac(DataTable dt)
        {
            bool bx = true;
            i = dataGridView1.CurrentCell.RowIndex;
            string v1 = dt.Rows[i][12].ToString();
            string v3 = dt.Rows[i][14].ToString();
            string v5 = txt1.Text;
            string v6 = dt.Rows[i]["加急否"].ToString();
            if (txt1.Text == "")
            {

                MessageBox.Show("销货单号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bx = false;
            }
            else if (dt.Rows[i]["品号"].ToString() == "")
            {
                MessageBox.Show("品号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bx = false;
            }
            else if (!boperate.exists("SELECT * FROM TB_WAREINFO WHERE WAREID='" + dt.Rows[i]["品号"].ToString() + "' AND ACTIVE='Y'"))
            {

                MessageBox.Show("品号" + dt.Rows[i]["品号"].ToString() + "不可用或不存在于系统中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bx = false;

            }
            else if (boperate.juageValueLimits(a, v6) == false)
            {


                MessageBox.Show("加急栏位只能输入加急或是留空不输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bx = false;

            }
            else if (dt.Rows[i]["销售单价"].ToString() == "")
            {
                MessageBox.Show("销售单价不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bx = false;
            }
            else if (v1 == "")
            {
                MessageBox.Show("仓库不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bx = false;
            }
            else if (v3 == "")
            {

                MessageBox.Show("本次销货量不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bx = false;
            }
            else if (boperate.yesno(v3) == 0)
            {

                MessageBox.Show("本次销货量只能输入数字且大于0！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bx = false;

            }
            else if (decimal.Parse(v3) <= 0)
            {

                MessageBox.Show("本次销货量需大于0！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bx = false;
            }
            else if (decimal.Parse(v3) > decimal.Parse(dt.Rows[i]["库存数量"].ToString()))
            {
                MessageBox.Show("本次销货数量不能大于该仓库的库存数量！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bx = false;


            }
            else
            {
                string k1 = boperate.CheckingWareidAndStorage(dt3.Rows[i]["品号"].ToString(), dt3.Rows[i]["仓库"].ToString());

                if (k1 != dt.Rows[i]["库存数量"].ToString())
                {
                    MessageBox.Show("选择的库存品号与此项次销货品号不一致！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bx = false;
                }
            }
            return bx;
        }
        #endregion
        #region dgv-cellclick
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int y1 = dataGridView1.CurrentCell.RowIndex;
            if (dataGridView1.CurrentCell.ColumnIndex == 1)
            {
                C23.BomManage.frmWareInfo frm = new C23.BomManage.frmWareInfo();
                frm.a6();
                frm.ShowDialog();
                if (str4[0] == "doubleclick")
                {
                    setWareData();
                    str4[0] = "";

                    string sqlx = "select Sellunitprice from tb_sellunitprice where cuid='" + cmb1.Text + "' and leather='" + dt3.Rows[y1]["皮种"].ToString() + "'";
                    if (boperate.getOnlyString(sqlx) != "")
                    {
                        dt3.Rows[y1]["销售单价"] = boperate.getOnlyString(sqlx);

                    }
                    else
                    {
                        dt3.Rows[y1]["销售单价"] = DBNull.Value;
                    }
                    dataGridView1.CurrentCell = dataGridView1[14, dataGridView1.CurrentCell.RowIndex];
                }
                DataTable dtx5 = boperate.getmaxstoragecount(dt3.Rows[y1]["品号"].ToString());
                if (dtx5.Rows.Count > 0)
                {
                    dt3.Rows[y1]["仓库"] = dtx5.Rows[0]["仓库"].ToString();
                    dt3.Rows[y1]["库存数量"] = dtx5.Rows[0]["库存数量"].ToString();
                }
                else
                {
                    dt3.Rows[y1]["仓库"] = "";
                    dt3.Rows[y1]["库存数量"] = DBNull.Value;

                }
            }
            if (dataGridView1.CurrentCell.ColumnIndex == 12)
            {

                C23.StorageManage.frmStorageCase frm = new C23.StorageManage.frmStorageCase();
                frm.a3();
                frm.ShowDialog();
                if (data5[0] == "doubleclick")
                {
                    dataGridView1[12, dataGridView1.CurrentCell.RowIndex].Value = data6[0];
                    dataGridView1[13, dataGridView1.CurrentCell.RowIndex].Value = data6[1];
                    data5[0] = "";
                }

            }
        }
        #endregion
        private void setWareData()
        {
            dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value = inputTextDataWare[0];
            dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value = inputTextDataWare[1];
            dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value = inputTextDataWare[2];
            dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value = inputTextDataWare[3];
            dataGridView1[5, dataGridView1.CurrentCell.RowIndex].Value = inputTextDataWare[4];
            dataGridView1[6, dataGridView1.CurrentCell.RowIndex].Value = inputTextDataWare[5];
            dataGridView1[7, dataGridView1.CurrentCell.RowIndex].Value = inputTextDataWare[6];
            dataGridView1[8, dataGridView1.CurrentCell.RowIndex].Value = inputTextDataWare[7];
            dataGridView1[9, dataGridView1.CurrentCell.RowIndex].Value = inputTextDataWare[8];
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            M_int_judge = 1;

        }

        private void cmb1_DropDown(object sender, EventArgs e)
        {

            C23.SellManage.FrmCustomerInfo frm = new FrmCustomerInfo();
            frm.a2();
            frm.ShowDialog();
            this.cmb1.IntegralHeight = false;//使组合框不调整大小以显示其所有项
            this.cmb1.DroppedDown = false;//使组合框不显示其下拉部分
            if (data1[0] == "doubleclick")
            {
                cmb1.Text = data2[0];
                txt3.Text = data2[1];
                txt4.Text = data2[2];
                txt5.Text = data2[3];

            }
            data1[0] = "";
            this.cmb1.IntegralHeight = true;//恢复默认值
            bind();

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

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int a = dataGridView1.CurrentCell.ColumnIndex;

            try
            {
                if (t == 1)
                {
                    MessageBox.Show("查询状态不能做销货，需销货请新增销货单！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (a == 16 && dataGridView1.CurrentCell.RowIndex == dataGridView1.Rows.Count - 1)
                    {
                        if (dt3.Rows.Count > 0)
                        {
                            if (ac(dt3) == true)
                            {
                                n1();
                                ask1(dt3);
                                del();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
        }
        private void del()
        {

            dtd = total1(txt1.Text);
            if (dtd.Rows.Count > 0)
            {
                btnDel.Enabled = true;
            
                TSMI.Enabled = true;

            }
            else
            {
                btnDel.Enabled = false;
                TSMI.Enabled = false;
    
            }
        }
        private void ask1(DataTable dt)
        {
            string v5 = dt.Compute("sum(销货数量)", "").ToString();
            txtNoTax.Text = string.Format("{0:F2}", Convert.ToDouble(v5));


        }
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("价格或数量只能输入数字！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                C23.ReportManage.FrmCRSellTable frm = new C23.ReportManage.FrmCRSellTable();
                C23.ReportManage.FrmCRSellTable.Array[0] = txt1.Text;
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
        }

        private void btnExcelPrint_Click(object sender, EventArgs e)
        {
           
            try
            {
                string sql = @"select SEID,wareid,SUM(SECOUNT) from tb_SellTable  ";
                DataTable dtn = boperate.ask(sql + " WHERE SEID='" + txt1.Text + "' GROUP BY SEID,WAREID ORDER BY SEID ASC", 1, 1);
                if (dtn.Rows.Count > 0)
                {
                    string v1 = @"D:\PrintModelForSellTable.xls";
                    if (File.Exists(v1))
                    {
                        boperate.ExcelPrint(dtn, "销货单", v1);
                    }
                    else
                    {
                        MessageBox.Show("指定路径不存在打印模版！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                else
                {
                    MessageBox.Show("无数据可打印！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
        }

        private void TSMI_Click(object sender, EventArgs e)
        {

            try
            {
                if (dt3.Rows.Count > 0)
                {
                    string v1 = dt3.Rows[dataGridView1.CurrentCell.RowIndex][0].ToString();
                    boperate.getcom("delete from tb_SellTable where SEID='" + txt1.Text + "' AND SN='" + v1 + "'");
                    boperate.getcom("delete from tb_MATERE where MATEREID='" + txt1.Text + "' AND SN='" + v1 + "'");

                    if (dt3.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt3;
                    }

                }
                bind();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {

                if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    boperate.getcom("delete tb_SellTable where SEID='" + txt1.Text + "'");
                    boperate.getcom("delete tb_MATERE where MateReID='" + txt1.Text + "'");
                    bind();
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
