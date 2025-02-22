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
    public partial class FrmWorkOrderT : Form
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
        public static string[] str6 = new string[] { "", "" };
        public static string[] str7 = new string[] { "" };
        public static string[] str8 = new string[] { "", "", "", "" };
        protected string M_str_sql = @"select A.WOID ,A.SN ,A.WareID ,B.WNAME,C.VOUCHERPROPERTIES AS 单据性质,A.WORKORDERCOUNT AS 
工单数量,A.DETWAREID,A.UNITDOSAGE,A.DOSAGE,A.MAKER,A.DATE FROM TB_WORKORDER A 
LEFT JOIN TB_WAREINFO B ON A.WAREID=B.WAREID
LEFT JOIN TB_VOUCHERPROPERTIES C ON C.VPID=A.VPID";

        protected string M_str_sql1 = @"select A.SN AS 项次,A.DETWAREID as 品号,B.WName as 品名,B.ExternalM as 套件,B.Type as 型号
,A.UNITDOSAGE AS 组成用量,A.DOSAGE AS 用量,A.MAKER AS 制单人,A.DATE AS 制单日期 FROM TB_WORKORDER A 
LEFT JOIN TB_WAREINFO B ON A.WAREID=B.WAREID
LEFT JOIN TB_VOUCHERPROPERTIES C ON C.VPID=A.VPID";

        string sql4 = @"SELECT * FROM TB_VOUCHERPROPERTIES ORDER BY VPID ASC";
        string sql5 = @"SELECT * FROM TB_WAREINFO";

        string WOKEY;
        protected int M_int_judge;
        public FrmWorkOrderT()
        {
            InitializeComponent();
            this.cmb2.Items.Add("");

        }
        private void cboxAcceptor_DropDown(object sender, EventArgs e)
        {
            C23.EmployeeManage.FrmEmployeeInfo frm = new C23.EmployeeManage.FrmEmployeeInfo();
            frm.GodET();
            frm.ShowDialog();

        }
        private void getGoderData()
        {
            this.cmb2.IntegralHeight = false;//使组合框不调整大小以显示其所有项
            this.cmb2.DroppedDown = false;//使组合框不显示其下拉部分
            this.cmb2.Items[0] = inputgetSEName[0];
            this.cmb2.SelectedIndex = 0;
            this.cmb2.IntegralHeight = true;//恢复默认值
        }
        private void FrmWorkOrderT_Load(object sender, EventArgs e)
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
                cmb1.Enabled = false;
                cmb2.Enabled = false;
                txt3.Enabled = false;
                dataGridView1.DataSource = dt3;
                cmb2.Items[0] = "";
            }
            else
            {
                dt = total1();
                dataGridView1.DataSource = dt;
                btnEdit.Enabled = false;
                cmb2.Text = "";
            }

            dt4 = boperate.getdt(sql4);
            dt5 = boperate.getdt(sql5 + " WHERE SUBSTRING(WAREID,1,1)='9' ORDER BY WAREID ASC");
            AutoCompleteStringCollection inputInfoSource = new AutoCompleteStringCollection();
            AutoCompleteStringCollection inputInfoSource4 = new AutoCompleteStringCollection();
            foreach (DataRow dr in dt5.Rows)
            {
                //cmbType.Items.Add(dr[0].ToString ());
                inputInfoSource.Add(dr[0].ToString());


            }
            this.cmb2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmb2.AutoCompleteCustomSource = inputInfoSource;

            dtd = boperate.getdt(M_str_sql + " WHERE A.WOID='" + txt1.Text + "' ORDER BY A.WOID");

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



        }
        #endregion
        #region dgvStateControl
        private void dgvStateControl()
        {
            int i;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Lavender;
            cmb1.BackColor = Color.Yellow;
            cmb2.BackColor = Color.Yellow;
            txt3.BackColor = Color.Yellow;
            int numCols1 = dataGridView1.Columns.Count;
            for (i = 0; i < numCols1; i++)
            {

                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (i == 13)
                {
                    dataGridView1.Columns[i].Width = 120;

                }
                else if (i == 2)
                {
                    dataGridView1.Columns[i].Width = 200;

                }
                else if (i == 1 || i == 11)
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
                if (i == 1 || i == 10)
                {
                    dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
            for (i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i == 10)
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
        #region total1
        private DataTable total1()
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
            dt.Columns.Add("组成用量", typeof(string));
            dt.Columns.Add("用量", typeof(string));
            dt.Columns.Add("制单人", typeof(string));
            dt.Columns.Add("制单日期", typeof(string));
            DataRow dr = dt.NewRow();
            dr["项次"] = "1";
            dt.Rows.Add(dr);
            return dt;
        }
        #endregion
        private DataTable as1()
        {
            DataTable dtt = new DataTable();

            dtt.Columns.Add("项次", typeof(string));
            dtt.Columns.Add("品号", typeof(string));
            dtt.Columns.Add("品名", typeof(string));
            dtt.Columns.Add("套件", typeof(string));
            dtt.Columns.Add("型号", typeof(string));
            dtt.Columns.Add("细节", typeof(string));
            dtt.Columns.Add("皮种", typeof(string));
            dtt.Columns.Add("颜色", typeof(string));
            dtt.Columns.Add("线色", typeof(string));
            dtt.Columns.Add("海棉厚度", typeof(string));
            dtt.Columns.Add("组成用量", typeof(string));
            dtt.Columns.Add("用量", typeof(string));
            dtt.Columns.Add("制单人", typeof(string));
            dtt.Columns.Add("制单日期", typeof(string));
            DataTable dtx1 = boperate.getdt(M_str_sql + " WHERE A.WOID='" + txt1.Text + "'");
            if (dtx1.Rows.Count > 0)
            {
                for (i = 0; i < dtx1.Rows.Count; i++)
                {
                    DataRow dr = dtt.NewRow();

                    dr["项次"] = dtx1.Rows[i][1].ToString();
                    dr["品号"] = dtx1.Rows[i][6].ToString();
                    dtx2 = boperate.getdt("select * from tb_wareinfo where wareid='" + dtx1.Rows[i][6].ToString() + "'");
                    dr["品名"] = dtx2.Rows[0]["WNAME"].ToString();
                    dr["套件"] = dtx2.Rows[0]["ExternalM"].ToString();
                    dr["型号"] = dtx2.Rows[0]["TYPE"].ToString();
                    dr["细节"] = dtx2.Rows[0]["DETAIL"].ToString();
                    dr["皮种"] = dtx2.Rows[0]["Leather"].ToString();
                    dr["颜色"] = dtx2.Rows[0]["COLOR"].ToString();
                    dr["线色"] = dtx2.Rows[0]["StitchingC"].ToString();
                    dr["海棉厚度"] = dtx2.Rows[0]["Thickness"].ToString();
                    dr["组成用量"] = dtx1.Rows[i][7].ToString();
                    dr["用量"] = dtx1.Rows[i][8].ToString();
                    dr["制单人"] = dtx1.Rows[i][9].ToString();
                    dr["制单日期"] = dtx1.Rows[i][10].ToString();
                    dtt.Rows.Add(dr);

                }
                txt1.Text = dtx1.Rows[0][0].ToString();
                cmb1.Text = dtx1.Rows[0][4].ToString();
                cmb2.Text = dtx1.Rows[0][2].ToString();
                txt2.Text = dtx1.Rows[0][3].ToString();
                txt3.Text = dtx1.Rows[0][5].ToString();



            }


            return dtt;
        }
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

        private void n1()
        {

            if (dt.Rows.Count > 0)
            {
                if (ac(dt) == 0)
                {
                }
                else
                {
                    wf();
                }
            }


            else if (dt3.Rows.Count > 0)
            {

                if (ac(dt3) == 0)
                {

                }
                else
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

                    boperate.getcom("delete tb_WorkOrder where woid='" + txt1.Text + "'");
                    insertdb(dt);

                }
                else if (dt3.Rows.Count > 0)
                {
                    boperate.getcom("delete tb_WorkOrder where woid='" + txt1.Text + "'");
                    insertdb(dt3);

                }
            }
        }
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
        private void at(DataTable dt)
        {

            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            if (WOKEY == "Exceed Limited")
            {

                MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["品号"].ToString() != "")
                    {
                        WOKEY = boperate.numN(20, 12, "000000000001", "select * from tb_WORKORDER", "WOKEY", "WO");
                        string varSN = dt.Rows[i]["项次"].ToString();
                        string varID = txt1.Text;
                        string varWareID = cmb2.Text;
                        decimal v1 = decimal.Parse(txt3.Text);
                        string v5 = dt.Rows[i]["品号"].ToString();
                        decimal v6 = decimal.Parse(dt.Rows[i]["组成用量"].ToString());
                        decimal v7 = v1 * v6;
                        string v8 = cmb1.Text;
                        dtx3 = boperate.getdt("select * from tb_voucherproperties where voucherproperties='" + v8 + "'");
                        string v9 = dtx3.Rows[0]["VPID"].ToString();
                        string varMaker = FrmLogin.M_str_name;
                        string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
                        #region WORKORDER

                        boperate.getcom(@"insert into tb_WORKORDER(WOKEY,SN,WOID,VPID,WareID,WORKORDERCOUNT,
                        DETWAREID,UNITDOSAGE,DOSAGE,MAKER,DATE,Year,Month,Day) values ('" + WOKEY + "','" + varSN + "','" + varID +
                         "','" + v9 + "','" + varWareID + "','" + v1 + "','" + v5 + "','" + v6 + "','" + v7 + "','" + varMaker +
                         "','" + varDate + "','" + year + "','" + month + "','" + day + "')");
                        #endregion

                    }
                }
            }
        }

        private void del()
        {
            txt1.Text = "";
            txt2.Text = "";
            txt3.Text = "";
            cmb1.Text = "";
            cmb2.Text = "";

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
 
        #region ac()
        private int ac(DataTable dt)
        {
            int x = 1;
            string v1 = dt.Rows[dataGridView1.CurrentCell.RowIndex][2].ToString();
            string v2 = dt.Rows[dataGridView1.CurrentCell.RowIndex][10].ToString();
            string v3 = dt.Rows[dataGridView1.CurrentCell.RowIndex][11].ToString();
            string v6 = dt.Rows[dataGridView1.CurrentCell.RowIndex][1].ToString();
            string v5 = txt1.Text;
            if (txt1.Text == "")
            {
                x = 0;
                MessageBox.Show("工单号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (cmb1.Text == "")
            {
                x = 0;
                MessageBox.Show("单据性质不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else if (boperate.exists("select * from tb_voucherproperties where voucherproperties='" + cmb1.Text + "'") == false)
            {
                x = 0;
                MessageBox.Show("该单据性质不存在于系统中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (cmb2.Text == "")
            {
                x = 0;
                MessageBox.Show("单头品号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        
            else if (boperate.exists("select * from tb_wareinfo where wareid='" + cmb2.Text + "' AND ACTIVE='Y'") == false)
            {
                x = 0;
                MessageBox.Show("该单头品号不存在于系统中或不可用！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (txt3.Text == "")
            {
                x = 0;
                MessageBox.Show("工单数量不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (boperate.yesno(txt3.Text) == 0)
            {
                x = 0;
                MessageBox.Show("工单数量只能输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (v6 == "")
            {
                x = 0;
                MessageBox.Show("单身品号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (!boperate.exists("select * from tb_wareinfo where wareid='" + v6 + "' AND ACTIVE='Y'"))
            {
                x = 0;
                MessageBox.Show("该单身品号"+v6+"不存在于系统中或不可用！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (v2 == "")
            {
                x = 0;
                MessageBox.Show("组成用量不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (boperate.yesno(v2) == 0)
            {
                x = 0;
                MessageBox.Show("组成用量只能输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (v3 == "")
            {
                x = 0;
                MessageBox.Show("用量不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (boperate.yesno(v3) == 0)
            {
                x = 0;
                MessageBox.Show("用量只能输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            return x;

        }
        #endregion
        #region ac1
        private int ac1()
        {
            int x = 1;


            string v5 = txt1.Text;
            if (txt1.Text == "")
            {
                x = 0;
                MessageBox.Show("工单号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (cmb1.Text == "")
            {
                x = 0;
                MessageBox.Show("单据性质不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else if (boperate.exists("select * from tb_voucherproperties where voucherproperties='" + cmb1.Text + "'") == false)
            {
                x = 0;
                MessageBox.Show("该单据性质不存在于系统中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (cmb2.Text == "")
            {
                x = 0;
                MessageBox.Show("单头品号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (boperate.exists("select * from tb_wareinfo where wareid='" + cmb2.Text + "'") == false)
            {
                x = 0;
                MessageBox.Show("该单头品号不存在于系统中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (txt3.Text == "")
            {
                x = 0;
                MessageBox.Show("工单数量不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            return x;

        }
        #endregion
        private void dgvGOInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (ac1() == 1)
            {
                if (dataGridView1.CurrentCell.ColumnIndex == 1)
                {
                    C23.BomManage.frmWareInfo frm = new C23.BomManage.frmWareInfo();
                    frm.a1();
                    frm.ShowDialog();
                    if (str4[0] == "doubleclick")
                    {
                        setWareData();
                        str4[0] = "";
                        dataGridView1.CurrentCell = dataGridView1[10, dataGridView1.CurrentCell.RowIndex];
                    }


                }
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridView1.Columns.Count - 1 && e.RowIndex == dataGridView1.Rows.Count - 1) //控制行、列
                {

                    if (dt.Rows.Count > 0)
                    {
                        if (ac(dt) != 0)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.NewRow();
                                int b = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["项次"].ToString());
                                dr["项次"] = Convert.ToString(b + 1);
                                dt.Rows.Add(dr);
                            }

                        }
                    }
                    else if (dt3.Rows.Count > 0)
                    {
                        if (ac(dt3) != 0)
                        {
                            DataRow dr3 = dt3.NewRow();
                            int b1 = Convert.ToInt32(dt3.Rows[dt3.Rows.Count - 1]["项次"].ToString());
                            dr3["项次"] = Convert.ToString(b1 + 1);
                            dt3.Rows.Add(dr3);
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {

                        decimal v1 = decimal.Parse(dt.Rows[dataGridView1.CurrentCell.RowIndex][10].ToString());
                        decimal v2 = decimal.Parse(txt3.Text);
                        dt.Rows[dataGridView1.CurrentCell.RowIndex][11] = Convert.ToString(v1 * v2);
                    }
                    if (dt3.Rows.Count > 0)
                    {

                        decimal v1 = decimal.Parse(dt3.Rows[dataGridView1.CurrentCell.RowIndex][10].ToString());
                        decimal v2 = decimal.Parse(txt3.Text);
                        dt3.Rows[dataGridView1.CurrentCell.RowIndex][11] = Convert.ToString(v1 * v2);
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {


                if (txt3.Text != "")
                {
                    if (e.ColumnIndex == 10 && e.RowIndex == dataGridView1.CurrentCell.RowIndex) //控制行、列
                    {
                        if (dt.Rows.Count > 0)
                        {

                            decimal v1 = decimal.Parse(dt.Rows[dataGridView1.CurrentCell.RowIndex][10].ToString());
                            decimal v2 = decimal.Parse(txt3.Text);
                            dt.Rows[dataGridView1.CurrentCell.RowIndex][11] = Convert.ToString(v1 * v2);
                        }
                        if (dt3.Rows.Count > 0)
                        {

                            decimal v1 = decimal.Parse(dt3.Rows[dataGridView1.CurrentCell.RowIndex][10].ToString());
                            decimal v2 = decimal.Parse(txt3.Text);
                            dt3.Rows[dataGridView1.CurrentCell.RowIndex][11] = Convert.ToString(v1 * v2);
                        }
                        dataGridView1.CurrentCell = dataGridView1[13, dataGridView1.CurrentCell.RowIndex];
                    }
                }
          


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);


            }
          

        }
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //for (i = 0; i < dataGridView1.Rows.Count - 1; i++)
            //{
            //  dataGridView1[0, i].Value = i + 1;
            // }

        }
        private void TSMI_Click(object sender, EventArgs e)
        {
            try
            {
                dtnn = boperate.getdt("select * from tb_outsourcingmatere where woid='" + txt1.Text + "'");
                if (dtnn.Rows.Count > 0)
                {
                    MessageBox.Show("此工单已经用领料记录不允许编辑或是删除！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {

                    if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        if (dt3.Rows.Count > 0)
                        {
                            string v1 = dt3.Rows[dataGridView1.CurrentCell.RowIndex][0].ToString();
                            boperate.getcom("delete from tb_WORKORDER where WOID='" + txt1.Text + "' AND SN='" + v1 + "'");
                            dt3 = as1();
                            if (dt3.Rows.Count > 0)
                            {
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
                            boperate.getcom("delete from tb_WORKORDER where WOID='" + txt1.Text + "' AND SN='" + v1 + "'");
                            dt = as1();
                            if (dt.Rows.Count > 0)
                            {
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }

        private void cmb1_DropDown(object sender, EventArgs e)
        {

            cmb1.DataSource = dt4;
            cmb1.DisplayMember = "VOUCHERPROPERTIES";
        }

        private void cmb2_DropDown(object sender, EventArgs e)
        {
            C23.BomManage.frmWareInfo frm = new C23.BomManage.frmWareInfo();
            frm.a2();
            frm.ShowDialog();
            this.cmb2.IntegralHeight = false;//使组合框不调整大小以显示其所有项
            this.cmb2.DroppedDown = false;//使组合框不显示其下拉部分
            cmb2.Text = str8[0];
            txt2.Text = str8[1];
            str7[0] = "";
            this.cmb2.IntegralHeight = true;
        }

        private void cmb3_DropDown(object sender, EventArgs e)
        {

        }

        private void cmb2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmb2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmb2.Text != "")
            {
                dt6 = boperate.getdt(sql5 + " WHERE WAREID='" + cmb2.Text + "'");
                if (dt6.Rows.Count > 0)
                {
                    txt2.Text = dt6.Rows[0]["wname"].ToString();

                }
            }
        }

        private void bn_RefreshItems(object sender, EventArgs e)
        {

        }

        private void txt3_TextChanged(object sender, EventArgs e)
        {
            if (boperate.yesno(txt3.Text) == 0)
            {
                MessageBox.Show("工单数量只能输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt3.Text = "";

            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("数量只能输入数字！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            str6[0] = "";
            string a1 = boperate.numN(12, 4, "0001", "select * from tb_WorkOrder", "WOID", "WO");
            if (a1 == "Exceed Limited")
            {
                MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                txt1.Text = a1;

            }
            btnSave.Enabled = true;
            txt2.Text = "";
            txt3.Text = "";
            cmb1.Text = "";

            cmb2.Text = "";
            cmb1.Enabled = true;
            cmb2.Enabled = true;
            txt3.Enabled = true;
            //M_int_judge = 0;
            btnEdit.Enabled = false;
            dataGridView1.DataSource = total1();

            //bind();
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
                dtnn = boperate.getdt("select * from tb_outsourcingmatere where woid='" + txt1.Text + "'");
                if (dtnn.Rows.Count > 0)
                {
                    MessageBox.Show("此工单已经用领料记录不允许编辑或是删除！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                dtnn = boperate.getdt("select * from tb_outsourcingmatere where woid='" + txt1.Text + "'");
                if (dtnn.Rows.Count > 0)
                {
                    MessageBox.Show("此工单已经用领料记录不允许编辑或是删除！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {

                    if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        boperate.getcom("delete tb_WORKORDER WHERE WOID='" + txt1.Text + "'");
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
