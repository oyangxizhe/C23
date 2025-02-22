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
    public partial class FrmOutSourcingGodET : Form
    {
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dtd = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();
        DataTable dt6 = new DataTable();
        DataTable dtx2 = new DataTable();
        DataTable dt7 = new DataTable();
        C23.BaseClass.BaseOperate boperate = new C23.BaseClass.BaseOperate();
        C23.BaseClass.OperateAndValidate opAndvalidate = new C23.BaseClass.OperateAndValidate();
        protected int i, j;
        public static string[] inputTextDataWare = new string[] { null, null, null, null };
        public static string[] data1 = new string[] { null, null, null, null };
        public static string[] data5= new string[] { "" };
        public static string[] data6 = new string[] { "","" };
        public static string[] data7 = new string[] { "" };
        public static string[] data8 = new string[] { "","" };
        public static string[] inputTextDataLocation = new string[] { "" };
        public static string[] str1 = new string[] { "" };
        public static string[] str2 = new string[] { "", "" };
        public static string[] str4 = new string[] { "" };
        public static string[] str6 = new string[] { "", "","","","",""};
        public static string[] data2 = new string[] { "" };
        public static string[] data3 = new string[] { "" };
        public static string[] data4 = new string[] { "" };

        protected string M_str_sql = @"select distinct(A.OGID) AS 入库单号,A.woid AS 工单号,C.wareid AS 品号,C.WNAME AS 品名,B.WORKORDERCOUNT AS
工单数量 from tb_outsourcingGodE A
LEFT JOIN TB_WORKORDER B ON A.WOID=B.WOID
LEFT JOIN TB_WAREINFO C ON C.WAREID=A.WAREID";

        protected string M_str_sql1 = @"select A.WOID as 工单号,A.SN AS 项次,A.WareID as 品号,B.WName as 品名,B.ExternalM as 套件,B.Type as 型号,
C.VOUCHERPROPERTIES AS 单据性质,A.WORKORDERCOUNT AS 
工单数量,A.DETWAREID,A.UNITDOSAGE,A.DOSAGE,A.MAKER,A.DATE FROM TB_WORKORDER A 
LEFT JOIN TB_WAREINFO B ON A.WAREID=B.WAREID
LEFT JOIN TB_VOUCHERPROPERTIES C ON C.VPID=A.VPID";

        string  GEKEY,MRKEY,OGKEY;
        protected int M_int_judge, t;
        public FrmOutSourcingGodET()
        {
            InitializeComponent();
 
        }
        private void FrmOutSourcingGodET_Load(object sender, EventArgs e)
        {
            bind();
            try
            {
              
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
                cmb1.Text = str6[1];
                txt2.Text = str6[2];
                txt3.Text = str6[3];
                txt4.Text = boperate.getOnlyString("SELECT WORKORDERCOUNT FROM TB_WORKORDER WHERE WOID='"+cmb1 .Text +"'");
           
                dt3 = as1(cmb1.Text,txt1 .Text );
                str6[0] = "";
                str6[1] = "";
              
                cmb1.Enabled = false;
                dataGridView1.DataSource = dt3;
                t = 1;
            }
            else
            {
               
                if (cmb1.Text == "")
                {
                    dataGridView1.DataSource = null;
                }
                else
                {
                   dt3 = as1(cmb1.Text);

                   dataGridView1.DataSource = dt3;
                }

            }
            dtd = boperate.getdt(M_str_sql + " WHERE A.WOID='" + txt1.Text + "' ORDER BY A.WOID");
            if (dtd.Rows.Count > 0)
            {
             
            }
            else
            {
             
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
                if (i==7 || i==14)
                {
                    dataGridView1.Columns[i].Width = 120;

                }
                else if (i==8 || i == 9 || i == 10 || i == 11 || i==12|| i == 13 )
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
                if (i == 9 || i==11)
                {
                    dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
            for (i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i ==11)
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
        #region as1
        private DataTable as1(string v1)
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add("套件", typeof(string));
            dtt.Columns.Add("型号", typeof(string));
            dtt.Columns.Add("细节", typeof(string));
            dtt.Columns.Add("皮种", typeof(string));
            dtt.Columns.Add("颜色", typeof(string));
            dtt.Columns.Add("线色", typeof(string));
            dtt.Columns.Add("海棉厚度", typeof(string));
            dtt.Columns.Add("工单累计入库数量",typeof(string));
            dtt.Columns.Add("未入库数量", typeof(string));
            dtt.Columns.Add("仓库", typeof(string));
            dtt.Columns.Add("委外加工单价", typeof(decimal));
            dtt.Columns.Add("本次入库数量", typeof(decimal));
            dtt.Columns.Add("本次加工费", typeof(decimal));
            dtt.Columns.Add("制单人", typeof(string));
            dtt.Columns.Add("制单日期", typeof(DateTime));
            DataRow dr = dtt.NewRow();
            if (txt2 .Text  != "")
            {
                dtx2 = boperate.getdt("select * from tb_wareinfo where wareid='" + txt2.Text + "'");
            }
            if (dtx2.Rows.Count > 0)
            {

                dr["套件"] = dtx2.Rows[0]["ExternalM"].ToString();
                dr["型号"] = dtx2.Rows[0]["TYPE"].ToString();
                dr["细节"] = dtx2.Rows[0]["DETAIL"].ToString();
                dr["皮种"] = dtx2.Rows[0]["Leather"].ToString();
                dr["颜色"] = dtx2.Rows[0]["COLOR"].ToString();
                dr["线色"] = dtx2.Rows[0]["StitchingC"].ToString();
                dr["海棉厚度"] = dtx2.Rows[0]["Thickness"].ToString();
            }
            dr["工单累计入库数量"] = 0;
            dtt.Rows.Add(dr);
            DataTable dtx4 = boperate.getdt("SELECT WOID,WAREID,SUM(OGCOUNT) FROM TB_OUTSOURCINGGODE GROUP BY WOID,WAREID");
            if (dtx4.Rows.Count > 0)
            {
                for (j = 0; j < dtt.Rows.Count; j++)
                {
                    for (i = 0; i < dtx4.Rows.Count; i++)
                    {
                        if (cmb1 .Text == dtx4.Rows[i]["WOID"].ToString())
                        {
                            dtt.Rows[j]["工单累计入库数量"] = dtx4.Rows[i][2].ToString();
                            break;
                        }
                    }
                }
              
            }
            DataTable dtx5 = boperate.getmaxstoragecount(txt2.Text );
            if (dtx5.Rows.Count > 0)
            {
                dtt.Rows [0]["仓库"] = dtx5.Rows[0]["仓库"].ToString();

            }
            if(boperate .exists ("select * from tb_outsourcingMateRe where woid='"+cmb1.Text +"'")==true)
            {
                string n1= boperate.getOnlyString ("select DISTINCT(STORAGEID) from tb_outsourcingMateRe where woid='" + cmb1.Text + "'");
                string n2 = boperate.getOnlyString(@"select OSUNITPRICE from TB_OUTSOURCINGUNITPRICE WHERE WAREID='"+txt2.Text +
                    "' AND STORAGEID='"+n1+"' ");
                if (n2 != "")
                {
                    dtt.Rows[0]["委外加工单价"] = decimal.Parse(n2);

                }
            }
            decimal a,b;
            a=decimal .Parse (txt4.Text );
            b=decimal .Parse (dtt.Rows [0]["工单累计入库数量"].ToString ());
            dtt.Rows[0]["未入库数量"] = a - b;
            dtt.Rows[0]["本次入库数量"] = dtt.Rows[0]["未入库数量"].ToString();
            return dtt;
        }
        #endregion
        #region as1
        private DataTable as1(string v1,string v2,string  v3)
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add("工单号", typeof(string));
            dtt.Columns.Add("项次", typeof(string));
            dtt.Columns.Add("品号", typeof(string));
            dtt.Columns.Add("品名", typeof(string));
            dtt.Columns.Add("套件", typeof(string));
            dtt.Columns.Add("型号", typeof(string));
            dtt.Columns.Add("组成用量", typeof(decimal));
            dtt.Columns.Add("用量", typeof(decimal));
            dtt.Columns.Add("累计已领用量", typeof(decimal));
            dtt.Columns.Add("未领用量", typeof(decimal), "用量-累计已领用量");
            dtt.Columns.Add("转出仓库", typeof(string));
            dtt.Columns.Add("转出仓库库存数量", typeof(string));
            dtt.Columns.Add("转入仓库", typeof(string));
            dtt.Columns.Add("本次领用量", typeof(string));
            dtt.Columns.Add("制单人", typeof(string));
            dtt.Columns.Add("制单日期", typeof(DateTime));

            DataTable dtx1 = boperate.getdt(M_str_sql1 + " WHERE A.WOID='" + v1 + "'");
            if (dtx1.Rows.Count > 0)
            {
                for (i = 0; i < dtx1.Rows.Count; i++)
                {
                    DataRow dr = dtt.NewRow();
                    dr["工单号"] = dtx1.Rows[i][0].ToString();
                    dr["项次"] = dtx1.Rows[i][1].ToString();
                    dr["品号"] = dtx1.Rows[i][8].ToString();
                    dtx2 = boperate.getdt("select * from tb_wareinfo where wareid='" + dtx1.Rows[i][8].ToString() + "'");
                    dr["品名"] = dtx2.Rows[0]["WNAME"].ToString();
                    dr["型号"] = dtx2.Rows[0]["TYPE"].ToString();
                    dr["组成用量"] = dtx1.Rows[i][9].ToString();
                    dr["用量"] = dtx1.Rows[i][10].ToString();
                    dr["累计已领用量"] = 0;
                    DataTable dtx5 = boperate.getmaxstoragecount(dtx1.Rows[i][8].ToString());
                    if (dtx5.Rows.Count > 0)
                    {
                        dr["转出仓库"] = dtx5.Rows[0]["仓库"].ToString();
                        dr["转出仓库库存数量"] = dtx5.Rows[0]["库存数量"].ToString();
                    }
                    dtt.Rows.Add(dr);

                }
            }
            if (v2 == "A")
            {
                for (i = 0; i < dtt.Rows.Count; i++)
                {
                    decimal v4 = decimal.Parse(v3);
                    dtt.Rows[i]["累计已领用量"] = v4 * decimal.Parse(dtt.Rows[i]["组成用量"].ToString());

                }
            }
            else
            {
                DataTable dtx4 = boperate.getdt("SELECT WOID,SN,WAREID,SUM(OMCOUNT) FROM TB_OUTSOURCINGMATERE GROUP BY WOID,SN,WAREID");
                if (dtx4.Rows.Count > 0)
                {
                    for (i = 0; i < dtx4.Rows.Count; i++)
                    {
                        for (j = 0; j < dtt.Rows.Count; j++)
                        {
                            if (dtt.Rows[j]["工单号"].ToString() == dtx4.Rows[i]["WOID"].ToString() && dtt.Rows[j]["项次"].ToString() == dtx4.Rows[i]["SN"].ToString())
                            {
                                dtt.Rows[j]["累计已领用量"] = dtx4.Rows[i][3].ToString();
                                break;
                            }
                        }
                    }
                }
            }
            return dtt;
        }
        #endregion
        #region as1
        private DataTable as1(string v1, string v2)
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add("套件", typeof(string));
            dtt.Columns.Add("型号", typeof(string));
            dtt.Columns.Add("细节", typeof(string));
            dtt.Columns.Add("皮种", typeof(string));
            dtt.Columns.Add("颜色", typeof(string));
            dtt.Columns.Add("线色", typeof(string));
            dtt.Columns.Add("海棉厚度", typeof(string));
            dtt.Columns.Add("工单累计入库数量", typeof(string));
            dtt.Columns.Add("未入库数量", typeof(string));
            dtt.Columns.Add("仓库", typeof(string));
            dtt.Columns.Add("委外加工单价", typeof(decimal));
            dtt.Columns.Add("本次入库数量", typeof(decimal));
            dtt.Columns.Add("本次加工费", typeof(decimal));
            dtt.Columns.Add("制单人", typeof(string));
            dtt.Columns.Add("制单日期", typeof(DateTime));
            DataRow dr = dtt.NewRow();
            if (txt2 .Text != "")
            {
                dtx2 = boperate.getdt("select * from tb_wareinfo where wareid='" +txt2.Text + "'");
            }
            if (dtx2.Rows.Count > 0)
            {
               
                dr["套件"] = dtx2.Rows[0]["ExternalM"].ToString();
                dr["型号"] = dtx2.Rows[0]["TYPE"].ToString();
                dr["细节"] = dtx2.Rows[0]["DETAIL"].ToString();
                dr["皮种"] = dtx2.Rows[0]["Leather"].ToString();
                dr["颜色"] = dtx2.Rows[0]["COLOR"].ToString();
                dr["线色"] = dtx2.Rows[0]["StitchingC"].ToString();
                dr["海棉厚度"] = dtx2.Rows[0]["Thickness"].ToString();
            }
                dr["工单累计入库数量"] = 0;
            
            dtt.Rows.Add(dr);
            DataTable dtx4 = boperate.getdt("SELECT WOID,WAREID,SUM(OGCOUNT) FROM TB_OUTSOURCINGGODE GROUP BY WOID,WAREID");
            if (dtx4.Rows.Count > 0)
            {
                for (j = 0; j < dtt.Rows.Count; j++)
                {
                    for (i = 0; i < dtx4.Rows.Count; i++)
                    {
                        if (cmb1.Text == dtx4.Rows[i]["WOID"].ToString())
                        {
                            dtt.Rows[j]["工单累计入库数量"] = dtx4.Rows[i][2].ToString();
                            break;
                        }
                    }
                }
            }
            DataTable dtx5 = boperate.getmaxstoragecount(txt2.Text);
            if (dtx5.Rows.Count > 0)
            {
                dtt.Rows[0]["仓库"] = dtx5.Rows[0]["仓库"].ToString();

            }
            if (boperate.exists("select * from tb_outsourcingMateRe where woid='" + cmb1.Text + "'") == true)
            {
                string n1 = boperate.getOnlyString("select DISTINCT(STORAGEID) from tb_outsourcingMateRe where woid='" + cmb1.Text + "'");
                string n2 = boperate.getOnlyString(@"select OSUNITPRICE from TB_OUTSOURCINGUNITPRICE WHERE WAREID='" + txt2.Text +
                    "' AND STORAGEID='" + n1 + "' ");
                if (n2 != "")
                {
                    dtt.Rows[0]["委外加工单价"] = decimal.Parse(n2);

                }
            }
            decimal a, b;
            a = decimal.Parse(txt4.Text);
            b = decimal.Parse(dtt.Rows[0]["工单累计入库数量"].ToString());
            dtt.Rows[0]["未入库数量"] = a - b;
            dtt.Rows[0]["本次入库数量"] = dtt.Rows[0]["未入库数量"].ToString();
            DataTable dtx6 = boperate.getdt(@"select OGID,WOID,SUM(OGCOUNT) FROM TB_OUTSOURCINGGODE WHERE OGID='" + v2 + "' AND WOID='" + v1 +
                "'GROUP BY OGID,WOID ORDER BY OGID,WOID");
            if (dtx6.Rows.Count > 0)
            {
                for (i = 0; i < dtx6.Rows.Count; i++)
                {
                    for (j = 0; j < dtt.Rows.Count; j++)
                    {
                        if (cmb1.Text == dtx6.Rows[i]["WOID"].ToString()  && v2 == dtx6.Rows[i]["OGID"].ToString())
                        {
                            dtt.Rows[j]["本次入库数量"] = dtx6.Rows[i][2].ToString();
                            break;
                        }
                    }
                }
            }
            return dtt;
        }
        #endregion
        private bool juageMateReAndGodeECount()
        {
            bool  s=true ;
            string s1 = dt3.Rows[0]["本次入库数量"].ToString();
             dt7 = as1(cmb1.Text, "A", s1);
            DataTable dtnn3 = as1(cmb1.Text, "", "");
        
            for (int j = 0; j < dt7.Rows.Count; j++)
            {
                decimal s2=decimal .Parse (dt7.Rows [j]["累计已领用量"].ToString ());
                decimal s3=decimal .Parse (dtnn3.Rows [j]["累计已领用量"].ToString ());
                string s4=dt7.Rows [j]["工单号"].ToString ();
                string s5=dt7.Rows [j]["项次"].ToString();
                string s6=dt7.Rows [j]["品号"].ToString ();
                if (s3<s2)
                {
            
           MessageBox.Show("工单号"+s4+"+ 项次"+s5+"+品号"+s6+"材料领用量"+s3.ToString ()+"小于入库量需要的领用量"+s2.ToString (), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
           s = false;
           break;

                }
            }
         return s;
        }
        private void n1()
        {

            if (ac(dt3) == true)
            {
                wf();
            }
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

                    boperate.getcom("delete tb_OutSourcingGodE where OGid='" + txt1.Text + "'");
                    insertdb(dt3);
                }
            }
        }
        private void insertdb(DataTable dtv)
        {
            at(dtv);
            dtd = as1(cmb1 .Text  );

            if (dtd.Rows.Count > 0)
            {
                dt3 = dtd;
                dataGridView1.DataSource = dt3;
            }
         

            if (dtd.Rows.Count > 0)
            {
            
            }
            else
            {
       

            }
    
  
            dgvStateControl();
       
        }
        private void at(DataTable dt)
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            if (OGKEY == "Exceed Limited")
            {

                MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
              
                if (dt.Rows[0]["工单累计入库数量"].ToString() != "")
                    {
                        GEKEY = boperate.numN(20, 12, "000000000001", "select * from tb_GODE", "GEKEY", "GE");
                        OGKEY = boperate.numN(20, 12, "000000000001", "select * from tb_OUTSOURCINGGODE", "OGKEY", "OG");
                        string varID = txt1.Text;
                        string varSN = "";
                        string varWareID = txt2.Text;
                        string v2 = cmb1.Text;
                        decimal v6 = decimal.Parse(dt.Rows[0]["本次入库数量"].ToString());
                        string v10 = dt.Rows[0]["仓库"].ToString();
                        string v12 = boperate.getstorageid(v10);
                        string v13 = boperate.getOnlyString("select distinct(storageid) from tb_OUTSOURCINGMATERE where woid='" + cmb1.Text + "'");
                        string v14 = dt.Rows[0]["委外加工单价"].ToString();
                        string v15 = dt.Rows[0]["本次加工费"].ToString();
                        string v16 = boperate.getOnlyString("select storagetype from tb_storageinfo where storageid='" + v13 + "'");
                        decimal v17 = decimal.Parse(v14);
                        decimal v18 = v6 * v17;
                        string varMaker = FrmLogin.M_str_name;
                        string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
                        #region OutSourcingGodE

                        boperate.getcom(@"insert into tb_OutSourcingGodE(OGKEY,OGID,WOID,WareID,OSSTORAGEID,OSSTORAGETYPE,STORAGEID,
OSUNITPRICE,OGCOUNT,PROCESSCOST,MAKER,DATE,Year,Month,Day) values ('" +OGKEY + "','" + varID+ "','" + v2 + 
            "','" + varWareID + "','"+v13+"','"+v16+"','"+v12+"','"+v14+"','" + v6 + "','"+v18+"','" + varMaker +
                         "','" + varDate + "','" + year + "','" + month +
                        "','" + day + "')");
                        #endregion
                        #region gode
                        boperate.getcom(@"insert into tb_gode(GEKEY,godeid,sn,wareid,gecount,storageid,GODEDATE,DATE,Year,MONTH,DAY) values ('" + GEKEY +
                        "','"+varID +"','"+varSN +
                         "','" + varWareID +"','" + v6 + "','" + v12 + "','" + varDate + "','" + varDate + "','" + year + "','" + month + "','" + day + "')");
                        #endregion
                        #region MATERE

                       
                        for (i = 0; i < dt7.Rows.Count; i++)
                        {
                            MRKEY = boperate.numN(20, 12, "000000000001", "select * from tb_MATERE", "MRKEY", "MR");

                            boperate.getcom(@"insert into tb_MATERE(MRKEY,MATEREID,SN,WAREID,MRCOUNT,STORAGEID,DATE,YEAR,MONTH,DAY) VALUES ('"+MRKEY +
                                "','"+varID+
                              "','"+dt7.Rows [i]["项次"].ToString () +"','"+dt7.Rows[i]["品号"].ToString ()+"','"+dt7.Rows [i]["累计已领用量"].ToString ()+
                              "','"+v13+"','"+varDate +"','"+year +"','"+month +"','"+day +"')");
                        }
                        #endregion

                    
                    }
                }
        }

        private void del()
        {
            txt1.Text = "";
            txt3.Text = "";
            cmb1.Text = "";
 
            dtd = as1(txt1.Text );
            if (dtd.Rows.Count > 0)
            {
       
   
                TSMI.Enabled = true;
            }
            else
            {
       
                TSMI.Enabled = false;
         
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
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(dt.Rows.Count.ToString());
        }
        #region ac()
        private bool  ac(DataTable dt)
        {
            bool s = true;
            i = dataGridView1.CurrentCell.RowIndex;
                string v1 = dt.Rows[i][9].ToString();
                string v3 = dt.Rows[i][10].ToString();
                string v4 = dt.Rows[i][11].ToString();
                string v5 = txt1.Text;
                if (txt1.Text == "")
                {
                    s = false;
                    MessageBox.Show("入库单号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (cmb1.Text == "")
                {
                    s = false;
                    MessageBox.Show("工单号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (boperate.exists("select * from tb_workorder where woid='" + cmb1.Text + "'") == false)
                {
                    s = false;
                    MessageBox.Show("该工单号不存在于系统中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (boperate.exists("select distinct(storageid) from tb_OUTSOURCINGMATERE  where woid='" + cmb1.Text + "'") == false)
                {
                    s = false;
                    MessageBox.Show("该工单号不存在委外加工领料单！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (boperate.yesno(v3) == 0)
                {
                    s = false;
                    MessageBox.Show("加工单价只能输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }  
                else if (v3 == "")
                {
                    s = false;
                    MessageBox.Show("委外加工单价不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (boperate.yesno(v4) == 0)
                {
                    s = false;
                    MessageBox.Show("入库数量只能输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (v4 == "")
                {
                    s = false;
                    MessageBox.Show("本次入库数量不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (decimal.Parse(v4) > decimal.Parse(dt.Rows[i]["未入库数量"].ToString()))
                {
                    s = false;
                    MessageBox.Show("本次入库数量不能大于未入库数量！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
          
                else if (juageMateReAndGodeECount() == false)
                {
                    s = false;

                }
                else if (v1 == "")
                {
                    s = false;
                    MessageBox.Show("仓库不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                return s;
        }
        #endregion
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 9)
            {
               
                C23.StorageManage.frmStorageInfo frm = new frmStorageInfo();
                frm.a3();
                frm.ShowDialog();
                if (data7[0] == "doubleclick")
                {
                    dataGridView1[9, dataGridView1.CurrentCell.RowIndex].Value = data8 [0];
                    data7[0] = "";
                }
                dataGridView1.CurrentCell = dataGridView1[11, dataGridView1.CurrentCell.RowIndex];
            }
        }
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //for (i = 0; i < dataGridView1.Rows.Count - 1; i++)
            //{
            //  dataGridView1[0, i].Value = i + 1;
            // }

        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            string varMaker = FrmLogin.M_str_name;
            string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/"); ;
        }
        private void tsbtnEdit_Click(object sender, EventArgs e)
        {
            M_int_judge = 1;
       
        }
        private void TSMI_Click(object sender, EventArgs e)
        {
            try
            {

                if (dt3.Rows.Count > 0)
                {
                    string v1 = dt3.Rows[dataGridView1.CurrentCell.RowIndex][0].ToString();
                    boperate.getcom("delete from tb_OutSourcingGodE where OGID='" + txt1.Text + "'");
                    boperate.getcom("delete from tb_MateRe where MatereID='" + txt1.Text + "' ");
                    boperate.getcom("delete from tb_GODE where GODEID='" + txt1.Text + "' ");
                    dt3 = as1(txt1 .Text );
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
                    boperate.getcom("delete from tb_OutSourcingGodE where OGID='" + txt1.Text + "'");
                    boperate.getcom("delete from tb_MateRe where MatereID='" + txt1.Text + "' ");
                    boperate.getcom("delete from tb_GODE where GODEID='" + txt1.Text + "' ");
                    dt = as1(txt1.Text );
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }
        private void cmb1_DropDown(object sender, EventArgs e)
        {
            C23.WorkOrderManage.FrmWorkOrder frm = new C23.WorkOrderManage.FrmWorkOrder();
            frm.a3();
            frm.ShowDialog();
           
                this.cmb1.IntegralHeight = false;//使组合框不调整大小以显示其所有项
                this.cmb1.DroppedDown = false;//使组合框不显示其下拉部分
                cmb1.Text = data1[0];
                txt2.Text = data1[1];
                txt3.Text = data1[2];
                txt4.Text = data1[3];
                this.cmb1.IntegralHeight = true;//恢复默认值
                data4[0] = "";
                bind();
        }
        private void cmb4_DropDown(object sender, EventArgs e)
        {
            C23.EmployeeManage.FrmEmployeeInfo frm = new C23.EmployeeManage.FrmEmployeeInfo();
            frm.a2();
            frm.ShowDialog();
            if (data3[0] == "doubleclick")
            {
             
                data3[0] = "";
            }
        }
        #region save
        private void tsbtnSave_Click(object sender, EventArgs e)
        {
     
       
        }
        #endregion
        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            try
            {
                if (t == 1)
                {
                    MessageBox.Show("查询状态不能做入库，需入库请新增委外加工入库单！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    int a = dataGridView1.CurrentCell.ColumnIndex;
                    if (a == 13)
                    {
                        if (dt3.Rows.Count > 0)
                        {

                            n1();
                            dataGridView1.CurrentCell = dataGridView1[13, dataGridView1.CurrentCell.RowIndex];
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

            MessageBox.Show("单价数量只能输入数字！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void button1_Click_2(object sender, EventArgs e)
        {
            MessageBox.Show(dt3.Rows[0]["本次加工费"].ToString());
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
               
                if (e.ColumnIndex == 4 && e.RowIndex == dataGridView1.CurrentCell.RowIndex) //控制行、列
                {
                    if (ac(dt3) == true)
                    {
                        if (dt3.Rows.Count > 0)
                        {

                            decimal v1 = decimal.Parse(dt3.Rows[dataGridView1.CurrentCell.RowIndex][3].ToString());
                            decimal v2 = decimal.Parse(dt3.Rows[dataGridView1.CurrentCell.RowIndex][4].ToString());
                            dt3.Rows[dataGridView1.CurrentCell.RowIndex][5] = Convert.ToString(v1 * v2);
                        }
                        //dataGridView1.CurrentCell = dataGridView1[8, dataGridView1.CurrentCell.RowIndex];
                    }
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
            str6[0] = "";
            string a1 = boperate.numN(12, 4, "0001", "select * from tb_OutSourcingGodE", "OGID", "OG");
            if (a1 == "Exceed Limited")
            {
                MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                txt1.Text = a1;
            }

            txt3.Text = "";
            cmb1.Text = "";
            cmb1.Enabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {

                if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    dela();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
        }
        private void dela()
        {

            if (dt3.Rows.Count > 0)
            {
                string v1 = dt3.Rows[dataGridView1.CurrentCell.RowIndex][0].ToString();
                boperate.getcom("delete from tb_OutSourcingGodE where OGID='" + txt1.Text + "'");
                boperate.getcom("delete from tb_MateRe where MatereID='" + txt1.Text + "' ");
                boperate.getcom("delete from tb_GODE where GODEID='" + txt1.Text + "' ");
                dt3 = as1(txt1.Text);
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
                boperate.getcom("delete from tb_OutSourcingGodE where OGID='" + txt1.Text + "'");
                boperate.getcom("delete from tb_MateRe where MatereID='" + txt1.Text + "' ");
                boperate.getcom("delete from tb_GODE where GODEID='" + txt1.Text + "' ");
                dt = as1(txt1.Text);
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
