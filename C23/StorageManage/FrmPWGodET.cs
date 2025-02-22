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
    public partial class FrmPWGodET : Form
    {
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dtd = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();
        DataTable dt6 = new DataTable();
        C23.BaseClass.BaseOperate boperate = new C23.BaseClass.BaseOperate();
        C23.BaseClass.OperateAndValidate opAndvalidate = new C23.BaseClass.OperateAndValidate();
        protected int i, j;
        public static string[] inputTextDataWare = new string[] { null, null, null, null, null, null, null, null, null };
        public static string[] inputTextDataStorage = new string[] { "" };
        public static string[] inputTextDataLocation = new string[] { "" };
        public static string[] inputgetSEName = new string[] { "" };
        public static string[] str1 = new string[] { "" };
        public static string[] str2 = new string[] { "", "" };
        public static string[] str3 = new string[] { "" };
        public static string[] str4 = new string[] { "" };
        public static string[] str5 = new string[] { "" };
        public static string[] str6 = new string[] { "", "" };


        protected string M_str_sql = @"select A.GEKEY AS 索引,A.SN as 项次,A.GodEID as 入库单号,A.WareID as 品号,"
            + "B.WName as 品名,B.ExternalM as 套件,B.Type as 型号,B.DETAIL AS 细节,B.Leather AS 皮种,B.COLOR AS 颜色,"
            + "B.StitchingC AS 线色,B.Thickness AS 海棉厚度,D.WORKGROUP AS 工作组,E.CRAFT AS 工艺,A.GECount as 入库数量,"
            + "F.StorageType as 仓库,A.Goder as 入库员,A.GodEDate as 入库日期, A.Maker as 制单人," +
"A.Date as 制单日期 from tb_GodE A LEFT JOIN TB_WAREINFO B ON A.WAREID=B.WAREID LEFT JOIN " +
" TB_WORKGROUP D ON D.WGID=A.WGID  LEFT JOIN TB_CRAFT E ON E.CRID=A.CRID LEFT JOIN TB_Storageinfo F ON F.STORAGEID=A.STORAGEID";

        protected string M_str_sql1 = @"select A.GEKEY AS 索引,A.SN as 项次,A.WareID as 品号,B.WName as 品名,B.ExternalM as 套件,B.Type as 型号,"
+ "B.DETAIL AS 细节,B.Leather AS 皮种,B.COLOR AS 颜色,"
            + "B.StitchingC AS 线色,B.Thickness AS 海棉厚度,E.CRAFT AS 工艺,A.GECount as 入库数量,F.StorageType as 仓库,A.Goder as 入库员,A.GodEDate as 入库日期,"
+ " A.Maker as 制单人," +
"A.Date as 制单日期 from tb_GodE A LEFT JOIN TB_WAREINFO B ON A.WAREID=B.WAREID LEFT JOIN " +
" TB_WORKGROUP D ON D.WGID=A.WGID  LEFT JOIN TB_CRAFT E ON E.CRID=A.CRID LEFT JOIN TB_Storageinfo F ON F.STORAGEID=A.STORAGEID";


        string sql4 = @"select distinct(b.EName) as 组里成员 from tb_employeeinfo b left join tb_workgroupM c 
on c.employeeid=b.employeeid left join tb_workgroup d on c.wgid=D.WGID";
        string sql5;
        string sql6 = "select DISTINCT(B.WORKGROUP),B.WGID FROM TB_WORKGROUPM A  LEFT JOIN TB_WORKGROUP B ON A.WGID=B.WGID";
        string GEKEY;
        protected int M_int_judge;
        public FrmPWGodET()
        {
            InitializeComponent();
            this.cboxGoder.Items.Add("");

        }
        private void cboxAcceptor_DropDown(object sender, EventArgs e)
        {
            C23.EmployeeManage.FrmEmployeeInfo frm = new C23.EmployeeManage.FrmEmployeeInfo();
            frm.GodET();
            frm.ShowDialog();

        }

        private void cboxGoder_DropDown(object sender, EventArgs e)
        {
            C23.EmployeeManage.FrmEmployeeInfo frm = new C23.EmployeeManage.FrmEmployeeInfo();
            frm.GodET();
            frm.ShowDialog();
            getGoderData();
        }
        private void getGoderData()
        {
            this.cboxGoder.IntegralHeight = false;//使组合框不调整大小以显示其所有项
            this.cboxGoder.DroppedDown = false;//使组合框不显示其下拉部分
            this.cboxGoder.Items[0] = inputgetSEName[0];
            this.cboxGoder.SelectedIndex = 0;
            this.cboxGoder.IntegralHeight = true;//恢复默认值
        }
        private void frmGodET_Load(object sender, EventArgs e)
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
            if (txtGodEID.Text == "")
            {
                txtGodEID.Text = str1[0];
                btnSave.Enabled = true;
            }
            if (str6[0] != "")
            {

                txtGodEID.Text = str6[0];
                cmbWorkGroup.Text = str6[1];
                btnEdit.Enabled = true;
                as1();
                //vdt = 1;
                str6[0] = "";
                str6[1] = "";

                btnSave.Enabled = false;

            }
            else
            {
                dt = total1();
                dataGridView1.DataSource = dt;
                btnEdit.Enabled = false;

            }
            sql5 = sql4 + " WHERE D.WORKGROUP='" + cmbWorkGroup.Text + "' ";
            dt4 = boperate.getdt(sql6);
            dt5 = boperate.getdt(sql5);
            dataGridView2.DataSource = dt5;
            dtd = boperate.getdt(M_str_sql + " WHERE A.GODEID='" + txtGodEID.Text + "' ORDER BY A.GODEID");

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
            cmbWorkGroup.BackColor = Color.Yellow;
            int numCols1 = dataGridView1.Columns.Count;
            for (i = 0; i < numCols1; i++)
            {

                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i == 0)
                {
                    dataGridView1.Columns[i].Width = 130;

                }
                else if (i == 11 || i == 13 || i == 15 || i == 17)
                {
                    dataGridView1.Columns[i].Width = 120;

                }
                else if (i == 2 || i == 12 || i == 14 || i == 16)
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
                if (i == 2 || i == 11 || i == 12 || i == 13)
                {
                    dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
            for (i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i == 12)
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
            int numCols = dataGridView2.Columns.Count;
            for (i = 0; i < numCols; i++)
            {

                dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView2.EnableHeadersVisualStyles = false;
                dataGridView2.Columns[i].HeaderCell.Style.BackColor = Color.Lavender;

            }


            for (i = 0; i < dataGridView2.Columns.Count; i++)
            {
                dataGridView2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView2.Columns[i].DefaultCellStyle.BackColor = Color.OldLace;
                i = i + 1;
            }
            for (i = 0; i < dataGridView2.Columns.Count; i++)
            {
                dataGridView2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i == 1 || i == 2)
                {
                    dataGridView2.Columns[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
            for (i = 0; i < dataGridView2.Columns.Count; i++)
            {
                dataGridView2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView2.Columns[i].ReadOnly = true;
            }


            dataGridView2.RowHeadersVisible = false;
            //dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Lavender;




        }
        #endregion
        #region total1
        private DataTable total1()
        {
            dt = new DataTable();
            dt.Columns.Add("索引", typeof(string));
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
            dt.Columns.Add("工艺", typeof(string));
            dt.Columns.Add("入库数量", typeof(string));
            dt.Columns.Add("仓库", typeof(string));
            dt.Columns.Add("入库员", typeof(string));
            dt.Columns.Add("入库日期", typeof(string));
            dt.Columns.Add("制单人", typeof(string));
            dt.Columns.Add("制单日期", typeof(string));
            DataRow dr = dt.NewRow();
            dr["项次"] = "1";
            dt.Rows.Add(dr);
            return dt;
        }
        #endregion
        private void as1()
        {
            dt3.Columns.Add("索引", typeof(string));
            dt3.Columns.Add("项次", typeof(string));
            dt3.Columns.Add("品号", typeof(string));
            dt3.Columns.Add("品名", typeof(string));
            dt3.Columns.Add("套件", typeof(string));
            dt3.Columns.Add("型号", typeof(string));
            dt3.Columns.Add("细节", typeof(string));
            dt3.Columns.Add("皮种", typeof(string));
            dt3.Columns.Add("颜色", typeof(string));
            dt3.Columns.Add("线色", typeof(string));
            dt3.Columns.Add("海棉厚度", typeof(string));
            dt3.Columns.Add("工艺", typeof(string));
            dt3.Columns.Add("入库数量", typeof(string));
            dt3.Columns.Add("仓库", typeof(string));
            dt3.Columns.Add("入库员", typeof(string));
            dt3.Columns.Add("入库日期", typeof(string));
            dt3.Columns.Add("制单人", typeof(string));
            dt3.Columns.Add("制单日期", typeof(string));

            DataTable dtx1 = boperate.getdt(M_str_sql + " WHERE A.GODEID='" + txtGodEID.Text + "'");
            for (i = 0; i < dtx1.Rows.Count; i++)
            {
                DataRow dr = dt3.NewRow();
                dr["索引"] = dtx1.Rows[i][0].ToString();
                dr["项次"] = dtx1.Rows[i][1].ToString();
                dr["品号"] = dtx1.Rows[i][3].ToString();
                DataTable dtx2 = boperate.getdt("select * from tb_wareinfo where wareid='" + dtx1.Rows[i][3].ToString() + "'");
                dr["品名"] = dtx2.Rows[0]["WNAME"].ToString();
                dr["套件"] = dtx2.Rows[0]["ExternalM"].ToString();
                dr["型号"] = dtx2.Rows[0]["TYPE"].ToString();
                dr["细节"] = dtx2.Rows[0]["DETAIL"].ToString();
                dr["皮种"] = dtx2.Rows[0]["Leather"].ToString();
                dr["颜色"] = dtx2.Rows[0]["COLOR"].ToString();
                dr["线色"] = dtx2.Rows[0]["StitchingC"].ToString();
                dr["海棉厚度"] = dtx2.Rows[0]["Thickness"].ToString();
                dr["工艺"] = dtx1.Rows[i][13].ToString();
                dr["入库数量"] = dtx1.Rows[i][14].ToString();
                dr["仓库"] = dtx1.Rows[i][15].ToString();
                dr["入库员"] = dtx1.Rows[i][16].ToString();
                dr["入库日期"] = dtx1.Rows[i][17].ToString();
                dr["制单人"] = dtx1.Rows[i][18].ToString();
                dr["制单日期"] = dtx1.Rows[i][19].ToString();
                dt3.Rows.Add(dr);

            }
            if (dtx1.Rows.Count > 0)
            {
                dtpGodEDate.Value = Convert.ToDateTime(dtx1.Rows[0]["入库日期"].ToString());
                cboxGoder.Text = dtx1.Rows[0]["入库员"].ToString();
                dataGridView1.DataSource = dt3;
            }


        }
        private void setWareData()
        {
            dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value = inputTextDataWare[0];
            dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value = inputTextDataWare[1];
            dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value = inputTextDataWare[2];
            dataGridView1[5, dataGridView1.CurrentCell.RowIndex].Value = inputTextDataWare[3];
            dataGridView1[6, dataGridView1.CurrentCell.RowIndex].Value = inputTextDataWare[4];
            dataGridView1[7, dataGridView1.CurrentCell.RowIndex].Value = inputTextDataWare[5];
            dataGridView1[8, dataGridView1.CurrentCell.RowIndex].Value = inputTextDataWare[6];
            dataGridView1[9, dataGridView1.CurrentCell.RowIndex].Value = inputTextDataWare[7];
            dataGridView1[10, dataGridView1.CurrentCell.RowIndex].Value = inputTextDataWare[8];
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

                    boperate.getcom("delete tb_gode  where godeid='" + txtGodEID.Text + "'");
                    boperate.getcom("delete tb_piecewages where godeid='" + txtGodEID.Text + "'");
                    // boperate.getcom("delete tb_inventory where rootid='"+txtGodEID .Text +"'");
                    insertdb(dt);

                }
                else if (dt3.Rows.Count > 0)
                {
                    boperate.getcom("delete tb_gode where godeid='" + txtGodEID.Text + "'");
                    boperate.getcom("delete tb_piecewages where godeid='" + txtGodEID.Text + "'");
                    //boperate.getcom("delete tb_inventory where rootid='" + txtGodEID.Text + "'");
                    insertdb(dt3);

                }
            }
        }
        private void insertdb(DataTable dtv)
        {
            if (ad(dtv) != 0)
            {
                at(dtv);
            }

            dtd = boperate.getdt(M_str_sql1 + " WHERE A.GODEID='" + txtGodEID.Text + "' ORDER BY A.GODEID");

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
            //string RootName = "入库单";
            if (GEKEY == "Exceed Limited")
            {

                MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["品号"].ToString() != "")
                    {
                        GEKEY = boperate.numN(20, 12, "000000000001", "select * from tb_GODE", "GEKEY", "GE");
                        string varSN = dt.Rows[i]["项次"].ToString();
                        string varID = txtGodEID.Text;
                        string varWareID = dt.Rows[i]["品号"].ToString();

                        string varSpec = "";
                        string varUnit = "";
                        string v = dt.Rows[i]["工艺"].ToString();
                        decimal varGECount = decimal.Parse(dt.Rows[i]["入库数量"].ToString());
                        string varStorageType = dt.Rows[i]["仓库"].ToString();
                        string varLocationName = "";
                        string varGoder = cboxGoder.Text.Trim();
                        string varGodEDate = dtpGodEDate.Value.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
                        string varMaker = FrmLogin.M_str_name;
                        string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
                        DataTable dtw6 = boperate.getdt("select * from tb_storageinfo where storagetype='" + varStorageType + "'");
                        string v7 = dtw6.Rows[0]["STORAGEID"].ToString();

                        #region Inventory
                        // boperate.getcom("insert into tb_Inventory(SN,RootID,RootName,WareID,Spec,Unit,GECount,StorageType,LocationName,"
                        //+ " Maker,Date) values ('" + varSN + "','" + varID + "','" + RootName + "','" + varWareID + "','" + varSpec +
                        // "','" + varUnit + "','" + varGECount + "','" + varStorageType + "','" + varLocationName + "','" + varMaker +
                        //"','" + varDate + "')");
                        #endregion
                        #region piecewages
                        DataTable dtw1 = boperate.getdt("select * from tb_craft where craft='" + v + "'");
                        DataTable dtw2 = boperate.getdt("select * from tb_workgroup where workgroup='" + cmbWorkGroup.Text + "'");

                        string v1 = dtw1.Rows[0]["CRID"].ToString();
                        string v2 = dtw2.Rows[0]["WGID"].ToString();
                        DataTable dtw3 = boperate.getdt("select * from tb_workgroupM WHERE WGID='" + v2 + "'");
                        for (int j = 0; j < dtw3.Rows.Count; j++)
                        {

                            DataTable dtw4 = boperate.getdt(@"select * from tb_CraftAndWorker where CRID='" + v1 +
                                "' AND WKID='" + dtw3.Rows[j]["WKID"].ToString() + "'");
                            if (dtw4.Rows.Count > 0)
                            {
                                decimal v4 = decimal.Parse(dtw4.Rows[0]["CWUnitPrice"].ToString());
                                DataTable dtw5 = boperate.getdt(@"select COUNT(WKID) from tb_workgroupM WHERE WGID='" + v2 +
                                    "' AND WKID='" + dtw3.Rows[j]["WKID"].ToString() + "'");
                                int v5 = Convert.ToInt32(dtw5.Rows[0][0].ToString());
                                decimal v6 = (varGECount * v4) / v5;

                                string PWKEY = boperate.numN(20, 12, "000000000001", "select * from tb_PIECEWAGES", "PWKEY", "PW");

                                if (PWKEY == "Exceed Limited")
                                {

                                    MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    boperate.getcom(@"insert into tb_piecewages(PWKEY,GEKEY,GODEID,
                      SN,WAREID,WGID,CRID,WKID,EMPLOYEEID,CWUNITPRICE,GECOUNT,PIECEWAGES,GODEDATE,YEAR,MONTH,DAY)
VALUES('" + PWKEY + "','" + GEKEY + "','" + txtGodEID.Text + "','" + varSN + "','" + varWareID + "','" + v2 + "','" + v1 +
                      "','" + dtw3.Rows[j]["WKID"].ToString() + "','" + dtw3.Rows[j]["EMPLOYEEID"].ToString() +
                      "','" + v4 + "','" + varGECount + "','" + v6 + "','" + varGodEDate + "','" + year + "','" + month + "','" + day + "')");
                                }
                            }
                        }
                        #endregion
                        #region GodeT
                        string varsendValues = "('" + GEKEY + "','" + varSN + "','" + varID + "','" + year + "','" + month +
                         "','" + day + "','" + varWareID + "','" + varSpec + "','" + varUnit +
                         "','" + varGECount + "','" + v7 + "','" + varLocationName + "','" + varGodEDate +
                         "','" + v2 + "','" + v1 + "','" + varGoder + "','" + varMaker + "','" + varDate + "')";
                        boperate.getcom("insert into tb_GodE(GEKEY,SN,GodEID,Year,Month,Day,WareID,Spec,Unit,"
                        + "GECount,StorageID,LocationName,GodEDate,WGID,CRID,Goder,Maker,Date) values " + varsendValues);
                        #endregion

                    }
                }
            }
        }

        private void load()
        {

            FrmPWGodE frm = new FrmPWGodE();
            frm.Bind();
        }
        private void del()
        {
            txtGodEID.Text = "";
            cmbWorkGroup.Text = "";
            cboxGoder.Text = "";
            dataGridView1.DataSource = total1();
            dataGridView2.DataSource = null;
            dtd = boperate.getdt(M_str_sql + " WHERE A.GODEID='" + txtGodEID.Text + "' ORDER BY A.GODEID");

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
            i = dataGridView1.CurrentCell.RowIndex;
            string v1 = dt.Rows[i][2].ToString();
            string v2 = dt.Rows[i][11].ToString();
            string v3 = dt.Rows[i][12].ToString();
            string v4 = dt.Rows[i][13].ToString();
            string v5 = txtGodEID.Text;
            string v6 = cmbWorkGroup.Text;

            if (v6 == "")
            {
                x = 0;
                MessageBox.Show("工作组不能为空！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else if (exists() == false)
            {
                x = 0;
                MessageBox.Show("该工作组不存在于系统中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (v1 == "")
            {
                x = 0;
                MessageBox.Show("品号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (!boperate.exists("SELECT * FROM TB_WAREINFO WHERE WAREID='" +v1 + "' AND ACTIVE='Y'"))
            {
                x = 0;
                MessageBox.Show("品号" + v1 + "不可用或不存在于系统中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
           

            }
            else if (v2 == "")
            {
                x = 0;
                MessageBox.Show("工艺不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (v3 == "")
            {
                x = 0;
                MessageBox.Show("入库数量不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            else if (boperate.yesno(v3) == 0)
            {
                x = 0;
                MessageBox.Show("数量只能输入数字！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else if (v4 == "")
            {
                x = 0;
                MessageBox.Show("仓库不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }

            else if (v5 == "")
            {
                x = 0;
                MessageBox.Show("入库单号不能为空！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            }

            return x;

        }
        #endregion
        #region ad()
        private int ad(DataTable dt)
        {
            int x = 1;
            for (i = 0; i < dt.Rows.Count - 1; i++)
            {
                string v1 = dt.Rows[i][2].ToString();
                string v2 = dt.Rows[i][11].ToString();
                string v3 = dt.Rows[i][12].ToString();
                string v4 = dt.Rows[i][13].ToString();
                string v5 = txtGodEID.Text;
                string v6 = cmbWorkGroup.Text;

                if (v6 == "")
                {
                    x = 0;
                    MessageBox.Show("工作组不能为空！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }
                else if (exists() == false)
                {
                    x = 0;
                    MessageBox.Show("该工作组不存在于系统中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("工艺不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
                else if (v3 == "")
                {
                    x = 0;
                    MessageBox.Show("入库数量不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                }
                else if (boperate.yesno(v3) == 0)
                {
                    x = 0;
                    MessageBox.Show("数量只能输入数字！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }
                else if (v4 == "")
                {
                    x = 0;
                    MessageBox.Show("仓库不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                }

                else if (v5 == "")
                {
                    x = 0;
                    MessageBox.Show("入库单号不能为空！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                }
            }
            return x;

        }
        #endregion
        private bool exists()
        {
            DataTable dtx1 = boperate.getdt(sql4 + " where WorkGroup='" + cmbWorkGroup.Text + "'");
            if (dtx1.Rows.Count > 0)
                return true;
            else
                return false;
        }
        private void dgvGOInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 2)
            {
                C23.BomManage.frmWareInfo frm = new C23.BomManage.frmWareInfo();
                frm.GodET();
                frm.ShowDialog();
                if (str4[0] == "doubleclick")
                {
                    setWareData();
                    str4[0] = "";
                    dataGridView1.CurrentCell = dataGridView1[11, dataGridView1.CurrentCell.RowIndex];
                }


            }
            if (dataGridView1.CurrentCell.ColumnIndex == 11)
            {
                C23.BomManage.FrmCraft frm = new C23.BomManage.FrmCraft();
                frm.a1();
                frm.ShowDialog();
                if (str3[0] == "doubleclick")
                {
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[11].Value = str2[0];
                    str3[0] = "";
                    dataGridView1.CurrentCell = dataGridView1[12, dataGridView1.CurrentCell.RowIndex];
                }


            }
            if (dataGridView1.CurrentCell.ColumnIndex == 13)
            {
                C23.StorageManage.frmStorageInfo frm = new frmStorageInfo();
                frm.GodET();
                frm.ShowDialog();
                if (str5[0] == "doubleclick")
                {
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[13].Value = inputTextDataStorage[0];
                    str5[0] = "";
                    dataGridView1.CurrentCell = dataGridView1[dataGridView1.Columns.Count - 1, dataGridView1.CurrentCell.RowIndex];
                }
            }
        }
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
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

            }
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //for (i = 0; i < dataGridView1.Rows.Count - 1; i++)
            //{
            //  dataGridView1[0, i].Value = i + 1;
            // }

        }


        private void cmbWorkGroup_DropDown(object sender, EventArgs e)
        {
            cmbWorkGroup.DataSource = dt4;
            cmbWorkGroup.DisplayMember = "WorkGroup";
        }

        private void cmbWorkGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ab();
        }
        private void ab()
        {

            sql5 = sql4 + " WHERE D.WORKGROUP='" + cmbWorkGroup.Text + "' ";
            DataTable dtx = boperate.getdt(sql5);
            if (dtx.Rows.Count > 0)
            {
                dataGridView2.DataSource = dtx;
            }
            else
            {

                dataGridView2.DataSource = null;

            }
            dgvStateControl();

        }

        private void TSMI_Click(object sender, EventArgs e)
        {
            try
            {
                if (boperate.juagestoragecount(txtGodEID.Text) == false)
                {

                }
                else
                {
                    if (dt3.Rows.Count > 0)
                    {
                        string v1 = dt3.Rows[dataGridView1.CurrentCell.RowIndex][1].ToString();
                        boperate.getcom("delete from tb_gode where GODEID='" + txtGodEID.Text + "' AND SN='" + v1 + "'");
                        boperate.getcom("DELETE TB_PIECEWAGES WHERE GODEID='" + txtGodEID.Text + "' AND SN='" + v1 + "'");
                        //boperate.getcom("delete tb_inventory where rootid='" + txtGodEID.Text + "' AND SN='" + v1 + "'");
                        dt3 = boperate.getdt(M_str_sql1 + " WHERE A.GODEID='" + txtGodEID.Text + "'");
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
                        string v1 = dt.Rows[dataGridView1.CurrentCell.RowIndex][1].ToString();
                        boperate.getcom("delete from tb_gode where GODEID='" + txtGodEID.Text + "' AND SN='" + v1 + "'");
                        boperate.getcom("DELETE TB_PIECEWAGES WHERE GODEID='" + txtGodEID.Text + "' AND SN='" + v1 + "'");
                        //boperate.getcom("delete tb_inventory where rootid='" + txtGodEID.Text + "' AND SN='" + v1 + "'");
                        dt = boperate.getdt(M_str_sql1 + " WHERE A.GODEID='" + txtGodEID.Text + "'");
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("数量需输入数值！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            BomManage.FrmWorkGroupMT frm = new C23.BomManage.FrmWorkGroupMT();
            frm.WorkGroup = cmbWorkGroup.Text;
            frm.Show();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            str6[0] = "";
            string a1 = boperate.numN2(12, 4, "0001", "select * from tb_GodE where substring(GODEID,1,2)='PW' AND YEAR='" + year + "' AND MONTH='" + month + "' AND DAY='" + day + "'", "GodEID", "PW");
            if (a1 == "Exceed Limited")
            {
                MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                txtGodEID.Text = a1;

            }
            btnSave.Enabled = true;
            //M_int_judge = 0;
            cboxGoder.Text = "";
            cmbWorkGroup.Text = "";
            dataGridView2.DataSource = null;
            bind();
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
                if (boperate.juagestoragecount(txtGodEID.Text) == false)
                {

                }
                else
                {
                    n1();
                    load();
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
                if (boperate.juagestoragecount(txtGodEID.Text) == false)
                {


                }
                else
                {
                    if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        boperate.getcom("delete tb_gode WHERE godeid='" + txtGodEID.Text + "'");
                        boperate.getcom("delete tb_piecewages where godeid='" + txtGodEID.Text + "'");
                        //boperate.getcom("delete tb_inventory where rootid='" + txtGodEID.Text + "'");
                        cmbWorkGroup.Text = "";
                        del();
                        load();
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
