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
    public partial class FrmMiscMateReT : Form
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
        public static string[] data1 = new string[] { null, null, null, null };
        public static string[] data5 = new string[] { "" };
        public static string[] data6 = new string[] { "", "" };
        public static string[] data7 = new string[] { "" };
        public static string[] data8 = new string[] { "", "" };
        public static string[] inputTextDataLocation = new string[] { "" };
        public static string[] str1 = new string[] { "" };
        public static string[] str2 = new string[] { "", "" };
        public static string[] str4 = new string[] { "" };
        public static string[] str6 = new string[] { "", "", "", "", "", "", "" };
        public static string[] data2 = new string[] { "" };
        public static string[] data3 = new string[] { "" };
        public static string[] data4 = new string[] { "" };
        public static string[] inputgetOEName = new string[] { "" };

        protected string M_str_sql = @"select  MATEREID AS 领料单号,A.SN AS 项次,C.wareid AS 品号,C.WNAME AS 品名,C.ExternalM as 套件,
C.Type as 型号,C.DETAIL AS 细节,C.Leather AS 皮种,C.COLOR AS 颜色,C.StitchingC AS 线色,C.Thickness AS 海棉厚度,
D.STORAGETYPE AS 仓库,A.MRCOUNT AS 领料数量,A.MAKER AS 制单人,A.DATE AS 制单日期  from tb_matere A
LEFT JOIN TB_WAREINFO C ON C.WAREID=A.WAREID
LEFT JOIN TB_STORAGEINFO D ON A.STORAGEID=D.STORAGEID";
        string MRKEY;
        protected int M_int_judge, t, t1;
        public FrmMiscMateReT()
        {
            InitializeComponent();

        }
        private void FrmMiscMateReT_Load(object sender, EventArgs e)
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
                dtp1.Text = str6[2];
                cmb1.Text = str6[1];
        
                dt3 = as1(txt1.Text);
                str6[0] = "";
                str6[1] = "";
                cmb1.Enabled = false;

                dataGridView1.DataSource = dt3;
                t = 1;
                t1 = 1;
            }
            else
            {
                dt3 = as1();
                dataGridView1.DataSource = dt3;
            }
            dtd = boperate.getdt(M_str_sql);
            if (dtd.Rows.Count > 0)
            {
                btnDel.Enabled = true;

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

            int numCols1 = dataGridView1.Columns.Count;
            for (i = 0; i < numCols1; i++)
            {

                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i == 10 || i == 14)
                {
                    dataGridView1.Columns[i].Width = 120;

                }
                else if (i == 1 || i == 11 || i == 12 || i == 13)
                {
                    dataGridView1.Columns[i].Width = 90;

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
                if (i == 10 || i == 12)
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

            }
        }
        #endregion
        #region as1
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
            dtt.Columns.Add("仓库", typeof(string));
            dtt.Columns.Add("库存数量", typeof(string));
            dtt.Columns.Add("领料数量", typeof(string));
            dtt.Columns.Add("制单人", typeof(string));
            dtt.Columns.Add("制单日期", typeof(string));
            DataRow dr = dtt.NewRow();
            dr["项次"] = "1";
            dtt.Rows.Add(dr);
            return dtt;
        }
        #endregion
        #region as1
        private DataTable as1(string v1)
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
            dtt.Columns.Add("仓库", typeof(string));
            dtt.Columns.Add("库存数量", typeof(string));
            dtt.Columns.Add("领料数量", typeof(string));
            dtt.Columns.Add("制单人", typeof(string));
            dtt.Columns.Add("制单日期", typeof(string));

            DataTable dtx1 = boperate.getdt(M_str_sql + " WHERE A.MATEREID='" + v1 + "'");
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
                    dr["仓库"] = dtx1.Rows[i][11].ToString();
                    DataTable dtx5 = boperate.getmaxstoragecount(dtx1.Rows[i][2].ToString());
                    if (dtx5.Rows.Count > 0)
                    {

                        dr["库存数量"] = dtx5.Rows[0]["库存数量"].ToString();
                    }
                    dr["领料数量"] = dtx1.Rows[i][12].ToString();
                    dr["制单人"] = dtx1.Rows[i][13].ToString();
                    dr["制单日期"] = dtx1.Rows[i][14].ToString();
                    dtt.Rows.Add(dr);

                }
            }

            return dtt;
        }
        #endregion

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

                    boperate.getcom("delete tb_MateRe where MateReID='" + txt1.Text + "'");
                    insertdb(dt3);

                }

            }

        }
        private void insertdb(DataTable dtv)
        {

            at(dtv);
            dtd = as1(txt1.Text);
            if (dt3.Rows.Count > 0)
            {
                dt3 = dtd;
            }
            if (dtd.Rows.Count > 0)
            {
                btnDel.Enabled = true;
            }
            else
            {
                btnDel.Enabled = false;

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
            if (MRKEY == "Exceed Limited")
            {

                MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                i = dataGridView1.CurrentCell.RowIndex;

                if (dt.Rows[i]["品号"].ToString() != "")
                {

                    MRKEY = boperate.numN(20, 12, "000000000001", "select * from tb_MATERE", "MRKEY", "MR");
                    string varSN = dt.Rows[i]["项次"].ToString();
                    string varID = txt1.Text;
                    string v1 = cmb1.Text;
                    string v2 = cmb1.Text;
                    string v3 = dtp1.Value.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
                    string v5 = dt.Rows[i]["品号"].ToString();
                    decimal v6 = decimal.Parse(dt.Rows[i]["领料数量"].ToString());
                    string v10 = dt.Rows[i]["仓库"].ToString();
                    string v12 = boperate.getstorageid(v10);
                    string varMaker = FrmLogin.M_str_name;
                    string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");

                    #region matere
                    boperate.getcom(@"insert into tb_matere(MRKEY,MateReID,SN,WAREID,MRCOUNT,STORAGEID,MATERER,MATEREDATE,
Maker,YEAR,MONTH,DAY,DATE) VALUES ('" + MRKEY + "','" + varID +
                        "','" + varSN + "','" + v5 + "','" + v6 + "','" + v12 +
                        "','" + v1 + "','" + v3 + "','" + varMaker + "','" + year + "','" + month + "','" + day + "','" + varDate + "')");
                    #endregion


                }
            }
        }
        #endregion
        private void del()
        {
            txt1.Text = "";
            cmb1.Text = "";
            dtp1.Text = "";

            dtd = as1();
            dt3 = as1();
            t1 = 1;
            dataGridView1.DataSource = dt3;
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
            int k = 1;
            i = dataGridView1.CurrentCell.RowIndex;
            int x = dataGridView1.CurrentCell.RowIndex;
            string v1 = dt.Rows[i]["品号"].ToString();
            string v2 = dt.Rows[i][10].ToString();
            string v3 = dt.Rows[i]["库存数量"].ToString();
            string v4 = dt.Rows[i][12].ToString();
            string v5 = txt1.Text;
            string v6 = dt.Rows[i]["仓库"].ToString();
            string[] a = new string[] { "大巴车间", "小车车间" };
            if (txt1.Text == "")
            {
                k = 0;

                MessageBox.Show("领料单号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            else if (cmb1.Text == "")
            {
                k = 0;
                MessageBox.Show("领料车间不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (boperate.juageValueLimits(a, cmb1.Text) == false)
            {
                k = 0;
                MessageBox.Show("领料车间需为大巴车间或是小车车间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


            else if (v1 == "")
            {
                k = 0;
                MessageBox.Show("品号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (!boperate.exists("SELECT * FROM TB_WAREINFO WHERE WAREID='" + v1 + "' AND ACTIVE='Y'"))
            {
                k= 0;
                MessageBox.Show("品号" + v1 + "不可用或不存在于系统中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (v2 == "")
            {
                k = 0;
                MessageBox.Show("仓库不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (v4 == "")
            {
                k = 0;
                MessageBox.Show("领用量不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (boperate.yesno(v4) == 0)
            {
                k = 0;
                MessageBox.Show("领用量只能输入数字且大于0！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (decimal.Parse(v4) <= 0)
            {
                k = 0;
                MessageBox.Show("领用量需大于0！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            else if (decimal.Parse(v4) > decimal.Parse(v3))
            {
                k = 0;
                MessageBox.Show("领用量不能大于库存数量！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            else
            {
                string k1 = boperate.CheckingWareidAndStorage(v1, dt.Rows[x]["仓库"].ToString());

                if (k1 != v3)
                {
                    k = 0;
                    MessageBox.Show("选择的库存品号与此项次领料品号不一致！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }


            }
            return k;
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
            //M_int_judge = 1;
            //btnSave.Enabled = true;

        }
        private void ak()
        {

            if (dataGridView1.CurrentCell.ColumnIndex == 12 && dataGridView1.CurrentCell.RowIndex == dataGridView1.Rows.Count - 1) //控制行、列
            {
                int n = dataGridView1.CurrentCell.RowIndex;
                if (dt3.Rows.Count > 0)
                {
                    if (dt3.Rows[n]["品号"].ToString() != "")
                    {

                        DataTable dtx5 = boperate.getmaxstoragecount(dt3.Rows[n]["品号"].ToString());
                        if (dtx5.Rows.Count > 0)
                        {
                            dt3.Rows[n]["仓库"] = dtx5.Rows[0]["仓库"].ToString();
                            dt3.Rows[n]["库存数量"] = dtx5.Rows[0]["库存数量"].ToString();
                        }
                        else
                        {
                            dt3.Rows[n]["仓库"] = "";
                            dt3.Rows[n]["库存数量"] = "";
                        }
                    }

                }

            }
        }
        private void TSMI_Click(object sender, EventArgs e)
        {
            t1 = 1;
            try
            {

                if (dt3.Rows.Count > 0)
                {
                    string v1 = dt3.Rows[dataGridView1.CurrentCell.RowIndex][0].ToString();
                    boperate.getcom("delete from tb_MateRe where MATEREID='" + txt1.Text + "' AND SN='" + v1 + "'");
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
                    boperate.getcom("delete from tb_MateRe where MATEREID='" + txt1.Text + "' AND SN='" + v1 + "'");
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 1)
            {
                C23.BomManage.frmWareInfo frm = new C23.BomManage.frmWareInfo();
                frm.a4();
                frm.ShowDialog();
                if (str4[0] == "doubleclick")
                {
                    setWareData();
                    str4[0] = "";
                    string a1 = dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                    DataTable dtx5 = boperate.getmaxstoragecount(a1);
                    if (dtx5.Rows.Count > 0)
                    {
                        dataGridView1[10, dataGridView1.CurrentCell.RowIndex].Value = dtx5.Rows[0]["仓库"].ToString();
                        dataGridView1[11, dataGridView1.CurrentCell.RowIndex].Value = dtx5.Rows[0]["库存数量"].ToString();
                    }
             
                    dataGridView1.CurrentCell = dataGridView1[12, dataGridView1.CurrentCell.RowIndex];
                }
            }
            if (dataGridView1.CurrentCell.ColumnIndex == 10)
            {
                C23.StorageManage.frmStorageCase frm = new frmStorageCase();
                frm.a1();
                frm.ShowDialog();
                if (data5[0] == "doubleclick")
                {
                    dataGridView1[10, dataGridView1.CurrentCell.RowIndex].Value = data6[0];
                    dataGridView1[11, dataGridView1.CurrentCell.RowIndex].Value = data6[1];
                    data5[0] = "";
                }
            }
        }
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            ak();
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell.ColumnIndex == 14 && dataGridView1.CurrentCell.RowIndex == dataGridView1.Rows.Count - 1)
                {
                    if (dt3.Rows.Count > 0)
                    {
                        if (ac(dt3) != 0)
                        {
                            if (t1 == 1)
                            {

                                t1 = 0;
                            }
                            else
                            {
                                n1();


                            }
                            DataRow dr3 = dt3.NewRow();
                            int b1 = Convert.ToInt32(dt3.Rows[dt3.Rows.Count - 1]["项次"].ToString());
                            dr3["项次"] = Convert.ToString(b1 + 1);
                            dt3.Rows.Add(dr3);
                            dataGridView1.DataSource = dt3;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

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
            string a1 = boperate.numN(12, 4, "0001", "select * from tb_MateRe", "MateReID", "MR");
            if (a1 == "Exceed Limited")
            {
                MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                txt1.Text = a1;

            }
            cmb1.Text = "";
            cmb1.Enabled = true;
            //M_int_judge = 0;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {

                if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    boperate.getcom("delete tb_MateRe WHERE MateReID='" + txt1.Text + "'");
                    del();
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
