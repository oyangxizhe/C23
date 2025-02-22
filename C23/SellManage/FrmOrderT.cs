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
using System.IO;
using System.Reflection;


namespace C23.SellManage
{
    public partial class FrmOrderT : Form
    {
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dtd = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();
        DataTable dt6 = new DataTable();
        DataTable dtx2 = new DataTable();
        DataTable dtx3 = new DataTable();
        DataTable dtnn = new DataTable();
        C23.BaseClass.BaseOperate boperate = new C23.BaseClass.BaseOperate();
        C23.BaseClass.OperateAndValidate opAndvalidate = new C23.BaseClass.OperateAndValidate();
        protected int i, j;
        public static string[] inputTextDataWare = new string[] { null, null, null, null, null, null, null, null, null };
        public static string[] inputTextDataStorage = new string[] { "" };
        public static string[] inputTextDataLocation = new string[] { "" };
        public static string[] inputgetSEName = new string[] { "" };
        public static string[] str1 = new string[] { "" };
        public static string[] str2 = new string[] { "", "" };
        public static string[] str4 = new string[] { "" };
        public static string[] str6 = new string[] { "", "", "", "", "" };
        public static string[] str7 = new string[] { "" };
        public static string[] str8 = new string[] { "", "", "", "" };
        public static string[] data1 = new string[] { "" };
        public static string[] data2 = new string[] { "", "", "", "" };
        string[] a = new string[] { "", "加急" };
        protected string M_str_sql = @"select A.ORID as 订单号, A.SN as 项次,A.WareID as 品号,B.WNAME AS 品名,B.ExternalM as 套件,
            B.Type as 型号,B.DETAIL AS 细节,B.Leather AS 皮种,B.COLOR AS 颜色,B.StitchingC AS 线色,B.Thickness AS 海棉厚度,"
        + "A.OCount as 订单数量 ,A.SellUnitPrice as 销售单价 ,A.DiscountRate as 折扣率,A.TaxRate as 税率,"
        + "A.CuID as 客户代码,C.CName as 客户名称,A.URGENT AS 加急否,A.ORDERDATE AS 订货日期,A.DELIVERYDATE AS 交货日期,A.Maker as 制单人,"
        + " A.Date as 制单日期 from tb_Order A "
        + " LEFT JOIN TB_WAREINFO B ON A.WAREID=B.WAREID"
        + " LEFT JOIN TB_CUSTOMERINFO C ON A.CUID=C.CUID";

        protected int M_int_judge, t;
        string ORKEY;
        public FrmOrderT()
        {
            InitializeComponent();


        }
        private void FrmOrderT_Load(object sender, EventArgs e)
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

        #region bind
        private void bind()
        {
            if (txt1.Text == "")
            {
                txt1.Text = str1[0];
                btnSave.Enabled = true;
            }
            if (str6[0] != "")
            {

                txt1.Text = str6[0];

                btnEdit.Enabled = true;
                dt3 = as1();
                str6[0] = "";
                btnSave.Enabled = false;

                dataGridView1.DataSource = dt3;

            }
            else
            {
                dt = total1();
                dataGridView1.DataSource = dt;
                btnEdit.Enabled = false;

            }


            dtd = boperate.getdt(M_str_sql + " WHERE A.ORID='" + txt1.Text + "' ORDER BY A.ORID");

            if (dtd.Rows.Count > 0)
            {
                btnDel.Enabled = true;
                btnEdit.Enabled = true;
            }
            else
            {
                btnDel.Enabled = false;
            }
            dgvStateControl();
            txt2.Enabled = false;
            txt3.Enabled = false;
            txt4.Enabled = false;
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

                if (i == 18 || i == 19 || i == 21)
                {
                    dataGridView1.Columns[i].Width = 120;

                }
                else if (i == 2)
                {
                    dataGridView1.Columns[i].Width = 200;

                }
                else if (i == 1 || i == 10 || i == 12 || i == 15 || i == 16 || i == 17 || i == 20)
                {
                    dataGridView1.Columns[i].Width = 90;
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
                if (i == 1 || i == 10 || i == 12 || i == 13 || i == 14)
                {
                    dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
                if (i == 15)
                {
                    dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.GreenYellow;

                }
            }
            for (i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i == 10 || i == 11 || i == 12 || i == 14 || i == 13)
                {
                    dataGridView1.Columns[i].ReadOnly = false;
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
            dt.Columns.Add("订单数量", typeof(decimal));
            dt.Columns.Add("加急否", typeof(string));
            dt.Columns.Add("销售单价", typeof(decimal));
            dt.Columns.Add("折扣率", typeof(decimal));
            dt.Columns.Add("税率", typeof(decimal));
            dt.Columns.Add("未税金额", typeof(decimal));
            dt.Columns.Add("税额", typeof(decimal));
            dt.Columns.Add("含税金额", typeof(decimal));
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
            dr["折扣率"] = 1;
            dtt2.Rows.Add(dr);
            return dtt2;
        }
        #endregion
        #region as1()
        private DataTable as1()
        {
            DataTable dtt = total();
            DataTable dtx1 = boperate.getdt(M_str_sql + " WHERE A.ORID='" + txt1.Text + "'");
            if (dtx1.Rows.Count > 0)
            {

                for (i = 0; i < dtx1.Rows.Count; i++)
                {
                    DataRow dr = dtt.NewRow();
                    dr["项次"] = dtx1.Rows[i][1].ToString();
                    dr["品号"] = dtx1.Rows[i][2].ToString();
                    dtx2 = boperate.getdt("select * from tb_wareinfo where wareid='" + dtx1.Rows[i][2].ToString() + "'");
                    dr["品名"] = dtx2.Rows[0]["WNAME"].ToString();
                    dr["套件"] = dtx2.Rows[0]["ExternalM"].ToString();
                    dr["型号"] = dtx2.Rows[0]["TYPE"].ToString();
                    dr["细节"] = dtx2.Rows[0]["DETAIL"].ToString();
                    dr["皮种"] = dtx2.Rows[0]["Leather"].ToString();
                    dr["颜色"] = dtx2.Rows[0]["COLOR"].ToString();
                    dr["线色"] = dtx2.Rows[0]["StitchingC"].ToString();
                    dr["海棉厚度"] = dtx2.Rows[0]["Thickness"].ToString();
                    dr["订单数量"] = dtx1.Rows[i][11].ToString();
                    dr["加急否"] = dtx1.Rows[i][17].ToString();
                    dr["销售单价"] = dtx1.Rows[i][12].ToString();
                    dr["折扣率"] = dtx1.Rows[i][13].ToString();
                    dr["税率"] = dtx1.Rows[i][14].ToString();

                    DataTable dtx5 = boperate.getdt(@" select orid,sn,wareid,ocount*sellunitprice*discountrate as 未税金额,
ocount*sellunitprice*discountrate*taxrate/100 as 税额,ocount*sellunitprice*discountrate*(1+taxrate/100)  AS 含税金额 from tb_order
where orid='" + txt1.Text + "' order by orid,sn");

                    dr["未税金额"] = dtx5.Rows[i][3].ToString();
                    dr["税额"] = dtx5.Rows[i][4].ToString();
                    dr["含税金额"] = dtx5.Rows[i][5].ToString();
                    dr["制单人"] = dtx1.Rows[i][20].ToString();
                    dr["制单日期"] = dtx1.Rows[i][21].ToString();

                    dtt.Rows.Add(dr);

                }
                DataRow dr1 = dtt.NewRow();
                int b = Convert.ToInt32(dtt.Rows[dtt.Rows.Count - 1]["项次"].ToString());
                dr1["项次"] = Convert.ToString(b + 1);
                dr1["折扣率"] = decimal.Parse(dtt.Rows[dtt.Rows.Count - 1]["折扣率"].ToString());
                dr1["税率"] = decimal.Parse(dtt.Rows[dtt.Rows.Count - 1]["税率"].ToString());
                dtt.Rows.Add(dr1);

                txt1.Text = dtx1.Rows[0][0].ToString();
                cmb1.Text = dtx1.Rows[0][15].ToString();
                dtx3 = boperate.getdt("select * from tb_customerinfo where cuid='" + cmb1.Text + "'");
                txt2.Text = dtx3.Rows[0]["CNAME"].ToString();
                txt3.Text = dtx3.Rows[0]["PHONE"].ToString();
                txt4.Text = dtx3.Rows[0]["ADDRESS"].ToString();

                DataTable dtx4 = boperate.getdt(@"SELECT ORID,SUM(ocount*sellunitprice*discountrate),SUM(ocount*sellunitprice*discountrate*
taxrate/100),SUM(ocount*sellunitprice*discountrate*(1+taxrate/100)) 
FROM TB_ORDER WHERE ORID='" + txt1.Text + "' GROUP BY ORID ORDER BY ORID");

                string v5 = dtx4.Rows[0][1].ToString();
                string v6 = dtx4.Rows[0][2].ToString();
                string v7 = dtx4.Rows[0][3].ToString();

                txtNoTax.Text = string.Format("{0:F2}", Convert.ToDouble(v5));
                txtTax.Text = string.Format("{0:F2}", Convert.ToDouble(v6));
                txtTotalCount.Text = string.Format("{0:F2}", Convert.ToDouble(v7));

                dtp1.Text = dtx1.Rows[0][18].ToString();
                dtp2.Text = dtx1.Rows[0][19].ToString();
            }

            return dtt;
        }
        #endregion
        #region save

        #endregion

        private void n1()
        {

            if (dt.Rows.Count > 0)
            {
                if (ac1(dt) != 0)
                {
                    wf();

                }
            }


            else if (dt3.Rows.Count > 0)
            {

                if (ac1(dt3) != 0)
                {

                    wf();

                }
            }
        }
        private void wf()
        {

            if (M_int_judge == 0)
            {

                insertdb(dt);
            }
            else
            {
                if (dt.Rows.Count > 0)
                {

                    boperate.getcom("delete tb_Order where ORid='" + txt1.Text + "'");
                    insertdb(dt);

                }
                else if (dt3.Rows.Count > 0)
                {
                    boperate.getcom("delete tb_Order where ORid='" + txt1.Text + "'");
                    insertdb(dt3);

                }
            }
        }
        #region insertdb
        private void insertdb(DataTable dtv)
        {
            at(dtv);
            dtd = as1();
            if (dt.Rows.Count > 0)
            {
                dt = dtd;
                dataGridView1.DataSource = dt;
            }
            if (dt3.Rows.Count > 0)
            {

                dt3 = dtd;
                dataGridView1.DataSource = dt3;

            }

            if (dtd.Rows.Count > 0)
            {
                btnDel.Enabled = true;
            }
            else
            {
                btnDel.Enabled = false;

            }
            btnSave.Enabled = false;
            btnEdit.Enabled = true;
            dgvStateControl();
        }
        #endregion

        #region at
        private void at(DataTable dt)
        {

            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            if (ORKEY == "Exceed Limited")
            {

                MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                for (i = 0; i < dt.Rows.Count - 1; i++)
                {
                    ORKEY = boperate.numN(20, 12, "000000000001", "select * from tb_Order", "ORKEY", "OR");
                    string varSN = dt.Rows[i]["项次"].ToString();
                    string varID = txt1.Text;
                    string v1 = cmb1.Text;
                    string varWareid = dt.Rows[i]["品号"].ToString();


                    string v2 = dt.Rows[i]["订单数量"].ToString();
                    string v3 = dt.Rows[i]["销售单价"].ToString();
                    string v4 = dt.Rows[i]["折扣率"].ToString();
                    string v5 = dt.Rows[i]["税率"].ToString();
                    string v6 = dt.Rows[i]["加急否"].ToString();
                    string v7 = dtp1.Value.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
                    string v8 = dtp2.Value.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
                    string varMaker = FrmLogin.M_str_name;
                    string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
                    #region Order
                    boperate.getcom(@"insert into tb_order(ORKEY,ORID,SN,WAREID,OCOUNT,SELLUNITPRICE,DISCOUNTRATE,TAXRATE,
CUID,URGENT,ORDERDATE,DELIVERYDATE,MAKER,DATE,YEAR,MONTH,DAY) VALUES ('" + ORKEY + "','" + varID + "','" + varSN + "','" + varWareid + "','" + v2 +
                    "','" + v3 + "','" + v4 + "','" + v5 + "','" + v1 + "','" + v6 + "','" + v7 + "','" + v8 + "','" + varMaker +
                    "','" + varDate + "','" + year + "','" + month + "','" + day + "')");


                    #endregion
                }


            }
        }
        #endregion

        #region btndel

        #endregion

        #region del
        private void del()
        {
            txt1.Text = "";
            txt2.Text = "";
            cmb1.Text = "";
            txt3.Text = "";
            txt4.Text = "";
            txtTotalCount.Text = "";
            txtTax.Text = "";
            txtNoTax.Text = "";
            dataGridView1.DataSource = total1();
            dtd = as1();
            if (dtd.Rows.Count > 0)
            {
                btnDel.Enabled = true;
                btnEdit.Enabled = true;
                TSMI.Enabled = true;

            }
            else
            {
                btnDel.Enabled = false;
                TSMI.Enabled = false;
                btnEdit.Enabled = false;
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

        #region ac()
        private int ac(DataTable dt)
        {
            int x = 1;
            string v1 = dt.Rows[dataGridView1.CurrentCell.RowIndex][1].ToString();
            string v2 = dt.Rows[dataGridView1.CurrentCell.RowIndex][10].ToString();
            string v3 = dt.Rows[dataGridView1.CurrentCell.RowIndex][12].ToString();
            string v4 = dt.Rows[dataGridView1.CurrentCell.RowIndex][13].ToString();
            string v5 = dt.Rows[dataGridView1.CurrentCell.RowIndex][14].ToString();
            string v6 = dt.Rows[dataGridView1.CurrentCell.RowIndex][11].ToString();

            if (txt1.Text == "")
            {
                x = 0;
                MessageBox.Show("订单号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (cmb1.Text == "")
            {
                x = 0;
                MessageBox.Show("客户代码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else if (boperate.exists("select * from tb_customerinfo where cuid='" + cmb1.Text + "'") == false)
            {
                x = 0;
                MessageBox.Show("该客户代码不存在于系统中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (v1 == "")
            {
                x = 0;
                MessageBox.Show("品号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (!boperate.exists("SELECT * FROM TB_WAREINFO WHERE WAREID='" + v1 + "' AND ACTIVE='Y'"))
            {
                x = 0;
                MessageBox.Show("品号" + v1 + "不可用或不存在于系统中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
         
            }
            else if (v2 == "")
            {
                x = 0;
                MessageBox.Show("订单数量不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (boperate.yesno(v2) == 0)
            {
                x = 0;
                MessageBox.Show("数量只能输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (boperate.juageValueLimits(a, v6) == false)
            {
                x = 0;

                MessageBox.Show("加急栏位只能输入加急或是留空不输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (v3 == "")
            {
                x = 0;
                MessageBox.Show("销售单价不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (boperate.yesno(v3) == 0)
            {
                x = 0;
                MessageBox.Show("单价只能输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (v4 == "")
            {
                x = 0;
                MessageBox.Show("折扣率不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (boperate.yesno(v4) == 0)
            {
                x = 0;
                MessageBox.Show("折扣率只能输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (v5 == "")
            {
                x = 0;
                MessageBox.Show("税率不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (boperate.yesno(v5) == 0)
            {
                x = 0;
                MessageBox.Show("税率只能输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            return x;

        }
        #endregion
        #region ac1()
        private int ac1(DataTable dt)
        {
            int x = 1;
            for (int k = 0; k < dt.Rows.Count - 1; k++)
            {
                string v1 = dt.Rows[k][1].ToString();
                string v2 = dt.Rows[k][10].ToString();
                string v3 = dt.Rows[k][12].ToString();
                string v4 = dt.Rows[k][13].ToString();
                string v5 = dt.Rows[k][14].ToString();
                string v6 = dt.Rows[k][11].ToString();

                if (txt1.Text == "")
                {
                    x = 0;
                    MessageBox.Show("订单号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
                else if (cmb1.Text == "")
                {
                    x = 0;
                    MessageBox.Show("客户代码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }

                else if (boperate.exists("select * from tb_customerinfo where cuid='" + cmb1.Text + "'") == false)
                {
                    x = 0;
                    MessageBox.Show("该客户代码不存在于系统中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                }
                else if (v1 == "")
                {
                    x = 0;
                    MessageBox.Show("品号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
          
                else if (v2 == "")
                {
                    x = 0;
                    MessageBox.Show("订单数量不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
                else if (boperate.yesno(v2) == 0)
                {
                    x = 0;
                    MessageBox.Show("数量只能输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                }
                else if (boperate.juageValueLimits(a, v6) == false)
                {
                    x = 0;
                    MessageBox.Show("加急栏位只能输入加急或是留空不输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                }
                else if (v3 == "")
                {
                    x = 0;
                    MessageBox.Show("销售单价不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                }
                else if (boperate.yesno(v3) == 0)
                {
                    x = 0;
                    MessageBox.Show("单价只能输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                }
                else if (v4 == "")
                {
                    x = 0;
                    MessageBox.Show("折扣率不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
                else if (boperate.yesno(v4) == 0)
                {
                    x = 0;
                    MessageBox.Show("折扣率只能输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                }
                else if (v5 == "")
                {
                    x = 0;
                    MessageBox.Show("税率不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                }
                else if (boperate.yesno(v5) == 0)
                {
                    x = 0;
                    MessageBox.Show("税率只能输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                }
            }
            return x;

        }
        #endregion

        #region dgvcellclick
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.CurrentCell.ColumnIndex == 1)
            {
                C23.BomManage.frmWareInfo frm = new C23.BomManage.frmWareInfo();
                frm.a5();
                frm.ShowDialog();
                if (str4[0] == "doubleclick")
                {
                    setWareData();
                    str4[0] = "";
                    int y1 = dataGridView1.CurrentCell.RowIndex;
                    string sqlx = "select Sellunitprice from tb_sellunitprice where cuid='" + cmb1.Text + "' and leather='" + dt.Rows[y1]["皮种"].ToString() + "'";
                    if (boperate.getOnlyString(sqlx) != "")
                    {
                        dt.Rows[y1]["销售单价"] = boperate.getOnlyString(sqlx);

                    }
                    else
                    {
                        dt.Rows[y1]["销售单价"] = DBNull.Value;
                    }
                    dataGridView1.CurrentCell = dataGridView1[10, dataGridView1.CurrentCell.RowIndex];
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
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                asw();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }

        }
        #region asw
        private void asw()
        {

            if (dataGridView1.CurrentCell.ColumnIndex == 15)
            {
                if (dt.Rows.Count > 0)
                {
                    if (ac(dt) != 0)
                    {
                        if (dataGridView1.CurrentCell.RowIndex == dataGridView1.Rows.Count - 1)
                        {

                            if (dt.Rows.Count > 0)
                            {

                                DataRow dr = dt.NewRow();
                                int b = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["项次"].ToString());
                                dr["项次"] = Convert.ToString(b + 1);
                                dr["折扣率"] = decimal.Parse(dt.Rows[dt.Rows.Count - 1]["折扣率"].ToString());
                                dr["税率"] = decimal.Parse(dt.Rows[dt.Rows.Count - 1]["税率"].ToString());
                                dt.Rows.Add(dr);
                            }

                        }

                        ask();
                    }
                }
            }
        }
        #endregion
        private void ask()
        {

            int n = dataGridView1.CurrentCell.RowIndex;
            decimal v1 = decimal.Parse(dt.Rows[dataGridView1.CurrentCell.RowIndex]["订单数量"].ToString());
            decimal v2 = decimal.Parse(dt.Rows[dataGridView1.CurrentCell.RowIndex]["销售单价"].ToString());
            decimal v3 = decimal.Parse(dt.Rows[dataGridView1.CurrentCell.RowIndex]["折扣率"].ToString());
            decimal v4 = decimal.Parse(dt.Rows[dataGridView1.CurrentCell.RowIndex]["税率"].ToString());
            dt.Rows[n]["未税金额"] = v1 * v2 * v3;
            dt.Rows[n]["税额"] = v1 * v2 * v3 * v4 / 100;
            dt.Rows[n]["含税金额"] = v1 * v2 * v3 * (1 + v4 / 100);
            ask1();


        }
        private void ask1()
        {
            string v5 = dt.Compute("sum(未税金额)", "").ToString();
            string v6 = dt.Compute("sum(税额)", "").ToString();
            string v7 = dt.Compute("sum(含税金额)", "").ToString();
            txtNoTax.Text = string.Format("{0:F2}", Convert.ToDouble(v5));
            txtTax.Text = string.Format("{0:F2}", Convert.ToDouble(v6));
            txtTotalCount.Text = string.Format("{0:F2}", Convert.ToDouble(v7));
        }
        #region tsmi
        private void TSMI_Click(object sender, EventArgs e)
        {

            dtnn = boperate.getdt("select * from tb_selltable where orid='" + txt1.Text + "'");
            if (dtnn.Rows.Count > 0)
            {
                MessageBox.Show("此订单已有销货记录不允许编辑或是删除！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (dt3.Rows.Count > 0)
                    {
                        string v1 = dt3.Rows[dataGridView1.CurrentCell.RowIndex][0].ToString();
                        boperate.getcom("delete from tb_Order where ORID='" + txt1.Text + "' AND SN='" + v1 + "'");
                        dt3 = as1();
                        if (dt3.Rows.Count > 0)
                        {
                            t = 1;
                            ask1();
                            dataGridView1.DataSource = dt3;
                        }
                        else
                        {
                            del();
                        }
                    }
                    else if (dt.Rows.Count > 0)
                    {
                        string v1 = dt.Rows[dataGridView1.CurrentCell.RowIndex][0].ToString();
                        boperate.getcom("delete from tb_Order where ORID='" + txt1.Text + "' AND SN='" + v1 + "'");
                        dt = as1();
                        if (dt.Rows.Count > 0)
                        {
                            t = 1;
                            ask1();
                            dataGridView1.DataSource = dt;
                        }
                        else
                        {
                            del();
                        }
                    }
                }
            }
        }
        #endregion
        #region cmb1_dropdown
        private void cmb1_DropDown(object sender, EventArgs e)
        {
            C23.SellManage.FrmCustomerInfo frm = new FrmCustomerInfo();
            frm.a1();
            frm.ShowDialog();
            this.cmb1.IntegralHeight = false;//使组合框不调整大小以显示其所有项
            this.cmb1.DroppedDown = false;//使组合框不显示其下拉部分
            if (data1[0] == "doubleclick")
            {
                cmb1.Text = data2[0];
                txt2.Text = data2[1];
                txt3.Text = data2[2];
                txt4.Text = data2[3];
            }
            data1[0] = "";
            this.cmb1.IntegralHeight = true;

            string v1;
            dataGridView1.CurrentCell = dataGridView1[1, dataGridView1.CurrentCell.RowIndex];


            if (dt.Rows.Count > 0)
            {

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    if (boperate.getOnlyString("select taxrate from tb_customerinfo where cuid='" + cmb1.Text + "'") != "")
                    {
                        v1 = boperate.getOnlyString("select taxrate from tb_customerinfo where cuid='" + cmb1.Text + "'");

                    }
                    else
                    {
                        v1 = Convert.ToString(17);

                    }
                    dt.Rows[i]["税率"] = v1;
                    string sqlx = "select Sellunitprice from tb_sellunitprice where cuid='" + cmb1.Text + "' and leather='" + dt.Rows[i]["皮种"].ToString() + "'";
                    if (boperate.getOnlyString(sqlx) != "")
                    {


                        dt.Rows[i]["销售单价"] = boperate.getOnlyString(sqlx);
                    }
                    else
                    {
                        dt.Rows[i]["销售单价"] = DBNull.Value;

                    }
                }

            }

        }
        #endregion
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

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("数量只能输入数字！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                C23.ReportManage.FrmCROrder frm = new C23.ReportManage.FrmCROrder();
                C23.ReportManage.FrmCROrder.Array[0] = txt1.Text;
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
                DataTable dtn = boperate.PrintOrder(" WHERE ORID='" + txt1.Text + "'");
                if (dtn.Rows.Count > 0)
                {
                    string v1 = @"D:\PrintModelForOrder.xls";
                    if (File.Exists(v1))
                    {
                        boperate.ExcelPrint(dtn, "订单", v1);
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            str6[0] = "";
            string a1 = boperate.numN(12, 4, "0001", "select * from tb_Order", "ORID", "OR");
            if (a1 == "Exceed Limited")
            {
                MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                txt1.Text = a1;

            }
            btnSave.Enabled = true;
            txt3.Text = "";
            txt4.Text = "";
            txt2.Text = "";
            cmb1.Text = "";
            cmb1.Enabled = true;
            btnEdit.Enabled = false;
            dataGridView1.DataSource = total1();
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
                //bn.Validate();
                dtnn = boperate.getdt("select * from tb_SELLTABLE  where ORID='" + txt1.Text + "'");
                if (dtnn.Rows.Count > 0)
                {
                    MessageBox.Show("此订单已有销货记录不允许编辑或是删除！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {

                    n1();

                }

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
                dtnn = boperate.getdt("select * from tb_selltable  where orid='" + txt1.Text + "'");
                if (dtnn.Rows.Count > 0)
                {
                    MessageBox.Show("此订单有销货记录不允许编辑或是删除！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {

                    if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        boperate.getcom("delete tb_Order WHERE orID='" + txt1.Text + "'");
                        del();
                    }
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
