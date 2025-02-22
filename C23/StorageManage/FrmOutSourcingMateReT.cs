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
    public partial class FrmOutSourcingMateReT : Form
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
        public static string[] str6 = new string[] { "", "","","","","",""};
        public static string[] data2 = new string[] { "" };
        public static string[] data3 = new string[] { "" };
        public static string[] data4 = new string[] { "" };
        public static string[] inputgetOEName = new string[] { "" };
        protected string M_str_sql = @"select A.WOID as 工单号,A.SN AS 项次,A.WareID as 品号,B.WName as 品名,B.ExternalM as 套件,B.Type as 型号,
C.VOUCHERPROPERTIES AS 单据性质,A.WORKORDERCOUNT AS 
工单数量,A.
DETWAREID,A.UNITDOSAGE,A.DOSAGE,A.MAKER,A.DATE FROM TB_WORKORDER A 
LEFT JOIN TB_WAREINFO B ON A.WAREID=B.WAREID
LEFT JOIN TB_VOUCHERPROPERTIES C ON C.VPID=A.VPID";


        protected string M_str_sql2 = @"select distinct(A.omid) AS 领料单号,A.woid AS 工单号,C.wareid AS 品号,C.WNAME AS 品名,B.WORKORDERCOUNT AS
工单数量 ,D.STORAGETYPE AS 委外厂仓库 from tb_outsourcingmatere A
LEFT JOIN TB_WORKORDER B ON A.WOID=B.WOID
LEFT JOIN TB_WAREINFO C ON C.WAREID=A.WAREID
LEFT JOIN TB_STORAGEINFO D ON A.STORAGEID=D.STORAGEID";
        string OMKEY, GEKEY,MRKEY;
        protected int M_int_judge, t;
        public FrmOutSourcingMateReT()
        {
            InitializeComponent();
    
        }
        private void FrmOutSourcingMateReT_Load(object sender, EventArgs e)
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
                cmb1.Text = str6[1];
                txt2.Text = str6[2];
                txt3.Text = str6[3];
                txt4.Text = str6[4];
                cmb4.Text = str6[5];
                dt3 = as1(cmb1.Text,txt1.Text );
                str6[0] = "";
                str6[1] = "";
                cmb1.Enabled = false;
                cmb4.Enabled = false;
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
            dtd = boperate.getdt("SELECT * FROM TB_OUTSOURCINGMATERE WHERE OMID='"+txt1.Text +"'");
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
            cmb4.BackColor = Color.Yellow;
            cmb1.BackColor = Color.Yellow;

            int numCols1 = dataGridView1.Columns.Count;
            for (i = 0; i < numCols1; i++)
            {

                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i == 12 || i == 15 || i == 18)
                {
                    dataGridView1.Columns[i].Width = 120;

                }
                else if (i == 0 || i == 2 || i==11 || i==12 || i==13 || i==14 || i==16 || i==17 )
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
                if (i == 14 || i==16)
                {
                    dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
            for (i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i == 16)
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
            dtt.Columns.Add("工单号", typeof(string));
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
            dtt.Columns.Add("用量", typeof(decimal));
            dtt.Columns.Add("累计已领用量", typeof(decimal));
            dtt.Columns.Add("未领用量", typeof(decimal),"用量-累计已领用量");
            dtt.Columns.Add("转出仓库", typeof(string));
            dtt.Columns.Add("转出仓库库存数量", typeof(string));
            dtt.Columns.Add("本次领用量", typeof(string));
            dtt.Columns.Add("制单人", typeof(string));
            dtt.Columns.Add("制单日期", typeof(DateTime));
            DataTable dtx1 = boperate.getdt(M_str_sql + " WHERE A.WOID='" + v1 + "'");
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
                    dr["套件"] = dtx2.Rows[0]["ExternalM"].ToString();
                    dr["型号"] = dtx2.Rows[0]["TYPE"].ToString();
                    dr["细节"] = dtx2.Rows[0]["DETAIL"].ToString();
                    dr["皮种"] = dtx2.Rows[0]["Leather"].ToString();
                    dr["颜色"] = dtx2.Rows[0]["COLOR"].ToString();
                    dr["线色"] = dtx2.Rows[0]["StitchingC"].ToString();
                    dr["海棉厚度"] = dtx2.Rows[0]["Thickness"].ToString();
                    dr["用量"] = dtx1.Rows[i][10].ToString();
                    dr["累计已领用量"] = 0;
                    DataTable dtx5 = boperate.getmaxstoragecount(dtx1.Rows[i][8].ToString());
                    if(dtx5.Rows .Count >0)
                    {
                        dr["转出仓库"] = dtx5.Rows[0]["仓库"].ToString();
                        dr["转出仓库库存数量"] = dtx5.Rows[0]["库存数量"].ToString();
                    }
                    dtt.Rows.Add(dr);

                }
            }
            DataTable dtx4 = boperate.getdt("SELECT WOID,SN,DETWAREID,SUM(OMCOUNT) FROM TB_OUTSOURCINGMATERE GROUP BY WOID,SN,DETWAREID");
            if (dtx4.Rows.Count > 0)
            {
                for (i = 0; i < dtx4.Rows.Count; i++)
                {
                    for(j=0;j<dtt.Rows .Count ;j++)
                    {
                        if (dtt.Rows[j]["工单号"].ToString() == dtx4.Rows[i]["WOID"].ToString() && dtt.Rows[j]["项次"].ToString() == dtx4.Rows[i]["SN"].ToString())
                        {
                            dtt.Rows[j]["累计已领用量"] = dtx4.Rows[i][3].ToString();
                            break;
                        }
                    }
                }
            }
            
            for (i = 0; i < dtt.Rows.Count; i++)
            {
                dtt.Rows[i]["本次领用量"] = dtt.Rows[i]["未领用量"].ToString();


            }
            return dtt;
        }
        #endregion
        #region as1
        private DataTable as1(string v1,string v2)
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add("工单号", typeof(string));
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
            dtt.Columns.Add("用量", typeof(decimal));
            dtt.Columns.Add("累计已领用量", typeof(decimal));
            dtt.Columns.Add("未领用量", typeof(decimal), "用量-累计已领用量");
            dtt.Columns.Add("转出仓库", typeof(string));
            dtt.Columns.Add("转出仓库库存数量", typeof(string));
            dtt.Columns.Add("本次领用量", typeof(string));
            dtt.Columns.Add("制单人", typeof(string));
            dtt.Columns.Add("制单日期", typeof(DateTime));

            DataTable dtx1 = boperate.getdt(M_str_sql + " WHERE A.WOID='" + v1 + "'");
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
                    dr["套件"] = dtx2.Rows[0]["ExternalM"].ToString();
                    dr["型号"] = dtx2.Rows[0]["TYPE"].ToString();
                    dr["细节"] = dtx2.Rows[0]["DETAIL"].ToString();
                    dr["皮种"] = dtx2.Rows[0]["Leather"].ToString();
                    dr["颜色"] = dtx2.Rows[0]["COLOR"].ToString();
                    dr["线色"] = dtx2.Rows[0]["StitchingC"].ToString();
                    dr["海棉厚度"] = dtx2.Rows[0]["Thickness"].ToString();
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
            DataTable dtx4 = boperate.getdt("SELECT WOID,SN,DETWAREID,SUM(OMCOUNT) FROM TB_OUTSOURCINGMATERE GROUP BY WOID,SN,DETWAREID");
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

            DataTable dtx6 = boperate.getdt(@"select OMID,WOID,SN,SUM(OMCOUNT) FROM TB_OUTSOURCINGMATERE WHERE OMID='"+v2+"' AND WOID='"+v1+
                "'GROUP BY OMID,WOID,SN ORDER BY OMID,WOID,SN");
            if (dtx6.Rows.Count > 0)
            {
                for (i = 0; i < dtx6.Rows.Count; i++)
                {
                    for (j = 0; j < dtt.Rows.Count; j++)
                    {
                        if (dtt.Rows[j]["工单号"].ToString() == dtx6.Rows[i]["WOID"].ToString() && dtt.Rows[j]["项次"].ToString() == dtx6.Rows[i]["SN"].ToString() && v2==dtx6.Rows [i]["OMID"].ToString ())
                        {
                            dtt.Rows[j]["本次领用量"] = dtx6.Rows[i][3].ToString();
                            break;
                        }
                    }
                }


            }
            return dtt;
        }
        #endregion

        private void n1()
        {

            ac(dt3);

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

                    boperate.getcom("delete tb_OutSourcingMateRe where OMid='" + txt1.Text + "'");
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
            dgvStateControl();
       
        }
        private void at(DataTable dt)
        {

            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            if (OMKEY == "Exceed Limited")
            {

                MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                i = dataGridView1.CurrentCell.RowIndex;
                 if (dt.Rows[i]["品号"].ToString() != "")
                    {
                        OMKEY = boperate.numN(20, 12, "000000000001", "select * from tb_OutSourcingMateRe", "OMKEY", "OM");
                        GEKEY = boperate.numN(20, 12, "000000000001", "select * from tb_GODE", "GEKEY", "GE");
                        MRKEY = boperate.numN(20, 12, "000000000001", "select * from tb_MATERE", "MRKEY", "MR");
                        string varSN = dt.Rows[i]["项次"].ToString();
                        string varID = txt1.Text;
                        string varWareID = txt2.Text;
                        string v2 = cmb1.Text;
                      
                        string v5 = dt.Rows[i]["品号"].ToString();
                        decimal v6 = decimal.Parse(dt.Rows[i]["本次领用量"].ToString());
                        string v10 = dt.Rows[i]["转出仓库"].ToString();
                        string v11 = boperate.getstorageid(cmb4.Text);
                        string v12 = boperate.getstorageid(v10);
                   
                        string varMaker = FrmLogin.M_str_name;
                        string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");

                        #region OutSourcingMateRe

                        boperate.getcom(@"insert into tb_OutSourcingMateRe(OMKEY,OMID,WOID,SN,WareID,OMCOUNT,DETWAREID,STORAGEID,MAKER,DATE,
Year,Month,Day) values ('" +OMKEY + "','" + varID+ "','" + v2 +
                         "','"+varSN +"','" + varWareID + "','" + v6 + "','" + v5 + "','"+v11+"','" + varMaker +
                         "','" + varDate + "','" + year + "','" + month +
                        "','" + day + "')");
                        #endregion

                        #region matere
                        boperate.getcom(@"insert into tb_matere(MRKEY,MateReID,SN,WAREID,MRCOUNT,STORAGEID,YEAR,MONTH,DAY,DATE) VALUES ('"+MRKEY +"','"+varID+
                            "','"+varSN +"','"+v5+"','"+v6+"','"+v12+"','"+year +"','"+month +"','"+day +"','"+varDate +"')");
                        #endregion

                        #region gode
                        boperate.getcom(@"insert into tb_gode(GEKEY,godeid,sn,wareid,gecount,storageid,GODEDATE,Year,MONTH,DAY,DATE) values ('"+GEKEY +
                            "','"+varID +"','"+varSN + "','"+v5+"','"+v6+"','"+v11+"','"+varDate +"','"+year +"','"+month +"','"+day +"','"+varDate +"')");
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
            bind();
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
        private void  ac(DataTable dt)
        {
            i = dataGridView1.CurrentCell.RowIndex;
                string v1 = dt.Rows[i][14].ToString();
                string v2 = cmb4.Text;
                string v3 = dt.Rows[i][16].ToString();
                //string v4 = dt.Rows[i][1].ToString();
                string v5 = txt1.Text;
                string v6,v7;
                if (txt1.Text == "")
                {
                  
                    MessageBox.Show("领料单号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (cmb1.Text == "")
                {
                    MessageBox.Show("工单号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (boperate.exists("select * from tb_workorder where woid='" + cmb1.Text + "'") == false)
                {
                   
                    MessageBox.Show("该工单号不存在于系统中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (v2 == "")
                {

                    MessageBox.Show("转入仓库不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (boperate.exists("select * from tb_STORAGEINFO where STORAGETYPE='" + cmb4.Text + "'") == false)
                {

                    MessageBox.Show("转入仓库不存在于系统中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (boperate.yesno(v3) == 0)
                {

                    MessageBox.Show("本次领用量只能输入数字且大于0！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (decimal .Parse (v3)<=0)
                {

                    MessageBox.Show("本次领用量需大于0！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
         
                else if (v3 == "")
                {

                    MessageBox.Show("本次领用量不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
             
                   else  if (v1 == "")
                    {
                        MessageBox.Show("转出仓库不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
          
                else if (decimal.Parse(v3) > decimal.Parse(dt.Rows[i]["未领用量"].ToString()))
                    {

                        MessageBox.Show("本次领用量不能大于未领料数量！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                else if (decimal.Parse(v3) > decimal.Parse(dt.Rows[i]["转出仓库库存数量"].ToString()))
                    {
                        MessageBox.Show("本次领用量不能大于该仓库的库存数量！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
         
                    else
                    {
                        DataTable dtnn1 = boperate.getdt("select * from tb_outsourcingmatere where woid='"+cmb1.Text +"'");
                        if (dtnn1.Rows.Count > 0)
                        {
                            v6 = dtnn1.Rows[0]["STORAGEID"].ToString();
                            v7 = boperate.getstorageid(cmb4.Text);
                            if (v6!=v7)
                            {
                                MessageBox.Show("同委外工单下的领料单委外仓库需一致！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                string k1 = boperate.CheckingWareidAndStorage(dt3.Rows[i]["品号"].ToString(), dt3.Rows[i]["转出仓库"].ToString());
                                if (k1!=dt.Rows [i]["转出仓库库存数量"].ToString())
                                {
                                    MessageBox.Show("选择的库存品号与此项次领料品号不一致！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                else
                                {
                                    wf();
                                }

                            }

                        }
                        else
                        {
                            string k1 = boperate.CheckingWareidAndStorage(dt3.Rows[i]["品号"].ToString(), dt3.Rows[i]["转出仓库"].ToString());
                            if (k1 != dt.Rows[i]["转出仓库库存数量"].ToString())
                            {
                                MessageBox.Show("选择的库存品号与此项次领料品号不一致！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            else
                            {
                                wf();
                            }


                        }
                    

                    }
        }
        #endregion
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 14)
            {
               
                C23.StorageManage.frmStorageCase frm = new frmStorageCase();
                frm.a2();
                frm.ShowDialog();
                if (data5[0] == "doubleclick")
                {
                    dataGridView1[14, dataGridView1.CurrentCell.RowIndex].Value = data6[0];
                    dataGridView1[15, dataGridView1.CurrentCell.RowIndex].Value = data6[1];
                    data5[0] = "";
                }
                dataGridView1.CurrentCell = dataGridView1[16, dataGridView1.CurrentCell.RowIndex];
            }
        }
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //for (i = 0; i < dataGridView1.Rows.Count - 1; i++)
            //{
            //  dataGridView1[0, i].Value = i + 1;
            // }

        }
        private void tsbtnEdit_Click(object sender, EventArgs e)
        {
            M_int_judge = 1;
    
        }
        private void TSMI_Click(object sender, EventArgs e)
        {
  
           
            try
            {
                TSMIO();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }
        private void TSMIO()
        {
                if (boperate.exists("SELECT * FROM TB_OUTSOURCINGgode WHERE WOID='" + cmb1.Text + "'"))
                {

                    MessageBox.Show("该工单号已经有委外入库信息，不允许删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (dt3.Rows.Count > 0)
                {
                    string v1 = dt3.Rows[dataGridView1.CurrentCell.RowIndex][1].ToString();
                    boperate.getcom("delete from tb_OutSourcingMateRe where OMID='" + txt1.Text + "' AND SN='" + v1 + "'");
                    boperate.getcom("delete from tb_MateRe where MatereID='" + txt1.Text + "' AND SN='" + v1 + "'");
                    boperate.getcom("delete from tb_GODE where GODEID='" + txt1.Text + "' AND SN='" + v1 + "'");
                    dt3 = as1(cmb1.Text);
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
                    boperate.getcom("delete from tb_OutSourcingMateRe where OMID='" + txt1.Text + "' AND SN='" + v1 + "'");
                    boperate.getcom("delete from tb_MateRe where MatereID='" + txt1.Text + "' AND SN='" + v1 + "'");
                    boperate.getcom("delete from tb_GODE where GODEID='" + txt1.Text + "' AND SN='" + v1 + "'");
                    dt = as1(cmb1.Text);
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
        private void cmb1_DropDown(object sender, EventArgs e)
        {
            C23.WorkOrderManage.FrmWorkOrder frm = new C23.WorkOrderManage.FrmWorkOrder();
            frm.a2();
            frm.ShowDialog();
                this.cmb1.IntegralHeight = false;//使组合框不调整大小以显示其所有项
                this.cmb1.DroppedDown = false ;//使组合框不显示其下拉部分
                cmb1.Text= data1[0];
                txt2.Text = data1[1];
                txt3.Text = data1[2];
                txt4.Text = data1[3];
                //this.cmb1.SelectedIndex = 0;
                this.cmb1.IntegralHeight = true;//恢复默认值
                data4[0] = "";
                string w = boperate.getOnlyString("select STORAGEID FROM TB_OUTSOURCINGMATERE WHERE WOID='"+cmb1.Text +"'");
                if (w != "")
                {
                    cmb4.Text = boperate.getOnlyString("select storagetype from tb_storageinfo where storageid='"+w+"'");
                }
                bind();
         
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
                    MessageBox.Show("查询状态不能做领用，需领用请新增委外加工领料单！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    int a = dataGridView1.CurrentCell.ColumnIndex;
                    if (a == 18)
                    {
                        if (dt3.Rows.Count > 0)
                        {

                            n1();
                            dataGridView1.CurrentCell = dataGridView1[18, dataGridView1.CurrentCell.RowIndex];
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
        }
        private void cmb4_DropDown(object sender, EventArgs e)
        {
            C23.StorageManage.frmStorageInfo frm = new frmStorageInfo();
            frm.a2();
            frm.ShowDialog();
            a();
        }
        private void a()
        {
                this.cmb4.IntegralHeight = false;//使组合框不调整大小以显示其所有项
                this.cmb4.DroppedDown = false;//使组合框不显示其下拉部分
                cmb4.Text = data8[0];
                this.cmb4.IntegralHeight = true;//恢复默认值
                data7[0] = ""; 
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

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("数量只能输入数字！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            str6[0] = "";
            string a1 = boperate.numN(12, 4, "0001", "select * from tb_OutSourcingMateRe", "OMID", "OM");
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
            //M_int_judge = 0;
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

            if (boperate.exists("SELECT * FROM TB_OUTSOURCINGgode WHERE WOID='" + cmb1.Text + "'"))
            {

                MessageBox.Show("该工单号已经有委外入库信息，不允许删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dt3.Rows.Count > 0)
            {
                string v1 = dt3.Rows[dataGridView1.CurrentCell.RowIndex][0].ToString();
                boperate.getcom("delete from tb_OutSourcingMateRe where OMID='" + txt1.Text + "' ");
                boperate.getcom("delete from tb_MateRe where MatereID='" + txt1.Text + "'");
                boperate.getcom("delete from tb_GODE where GODEID='" + txt1.Text + "' ");
                dt3 = as1(cmb1 .Text );
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
                boperate.getcom("delete from tb_OutSourcingMateRe where OMID='" + txt1.Text + "' ");
                boperate.getcom("delete from tb_MateRe where MatereID='" + txt1.Text + "'");
                boperate.getcom("delete from tb_GODE where GODEID='" + txt1.Text + "' ");
                dt = as1(cmb1 .Text );
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
