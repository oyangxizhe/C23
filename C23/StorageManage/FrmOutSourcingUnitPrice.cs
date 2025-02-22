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
    public partial class FrmOutSourcingUnitPrice : Form
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();

        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dtx1 = new DataTable();
        DataTable dtx2 = new DataTable();
        C23.BaseClass.BaseOperate boperate = new C23.BaseClass.BaseOperate();
        C23.BaseClass.OperateAndValidate opAndvalidate = new C23.BaseClass.OperateAndValidate();
        protected string M_str_sql = @"select A.OSID AS 加工费单价代码,A.WareID AS 品号,B.WNAME AS 品名,A.STORAGEID AS 仓库代码,C.STORAGETYPE AS  仓库,A.OSUnitPrice as 加工费单价,
            A.Maker as 制单人,A.Date as 日期 from tb_OutSourcingUnitPrice A
LEFT JOIN TB_WAREINFO B ON A.WAREID=B.WAREID
LEFT JOIN TB_STORAGEINFO C ON A.STORAGEID=C.STORAGEID";
        protected string M_str_table = "tb_OutSourcingUnitPrice";
        protected int M_int_judge, i;
        protected int getdata;
        string sql3 = "select * from tb_WareInfo";
        string sql4 = "select * from tb_StorageInfo";
        int k;
        public FrmOutSourcingUnitPrice()
        {
            InitializeComponent();
        }



        private void FrmOutSourcingUnitPrice_Load(object sender, EventArgs e)
        {

            Bind();

        }
        #region Bind
        private void Bind()
        {
            btnSave.Enabled = false;
            this.dataGridView1.ReadOnly = true;
            dt = boperate.getdt(M_str_sql + "  order by A.OSID,A.date asc");
            dataGridView1.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                btnDel.Enabled = true;
                btnEdit.Enabled = true;
            }
            else
            {
                btnDel.Enabled = false;
                btnEdit.Enabled = false;
            }

            for (i = 0; i < dataGridView1.Columns.Count - 1; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dgvStateControl();
            abc();
        }
        #endregion
        #region dgvStateControl
        private void dgvStateControl()
        {
            int i;
            int numCols = dataGridView1.Columns.Count;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Lavender;
            for (i = 0; i < numCols; i++)
            {

                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;


                if (i == 7 || i == 2)
                {
                    dataGridView1.Columns[i].Width = 150;

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
        }
        #endregion

        #region abc
        private void abc()
        {

            dt3 = boperate.getdt(sql3);
            dt4 = boperate.getdt(sql4);

            AutoCompleteStringCollection inputInfoSource3 = new AutoCompleteStringCollection();
            AutoCompleteStringCollection inputInfoSource4 = new AutoCompleteStringCollection();
            foreach (DataRow dr3 in dt3.Rows)
            {

                inputInfoSource3.Add(dr3["WAREID"].ToString());


            }
            this.cmb1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmb1.AutoCompleteCustomSource = inputInfoSource3;
            foreach (DataRow dr4 in dt4.Rows)
            {

                inputInfoSource4.Add(dr4["STORAGETYPE"].ToString());
            }
            this.cmb2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmb2.AutoCompleteCustomSource = inputInfoSource4;

        }
        #endregion
 
        private void ClearText()
        {

            cmb1.Text = "";
            cmb2.Text = "";
            txt2.Text = "";

        }

        #region save
        private void save()
        {



            if (cmb1.Text == "")
            {
                MessageBox.Show("品号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txt1.Text == "")
            {
                MessageBox.Show("加工费单价代码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (cmb2.Text == "")
            {
                MessageBox.Show("仓库不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txt2.Text == "")
            {
                MessageBox.Show("加工费单价不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            else
            {
                if (yesno(txt2.Text) == 0)
                {
                    MessageBox.Show("加工费单价只能输入数字！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    dtx1 = boperate.getdt("select * from tb_WareINFO where WAREID='" + cmb1.Text + "' AND ACTIVE='Y'");
                    dtx2 = boperate.getdt("select * from tb_STORAGEINFO where STORAGETYPE='" + cmb2.Text + "'");
                    if (dtx1.Rows.Count > 0)
                    {
                        if (dtx2.Rows.Count > 0)
                        {
                            as1();
                        }
                        else
                        {


                            MessageBox.Show("此仓库在系统中不存在！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }

                    }
                    else
                    {

                        MessageBox.Show("此品号在系统中不存在或不可用！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }

                }


            }
        }
        #endregion
        private int yesno(string vars)
        {

            int i;
            for (i = 0; i < vars.Length; i++)
            {
                int p = Convert.ToInt32(vars[i]);
                if (p >= 48 && p <= 57 || p == 46)
                {
                    k = 1;
                }
                else
                {
                    k = 0; break;
                }

            }

            return k;

        }
        private void as1()
        {
            string varDate = DateTime.Now.ToString();
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            string v1 = boperate.getstorageid(cmb2.Text);
            if (M_int_judge == 0)
            {

                dt1 = boperate.getdt("select * from tb_OutSourcingUnitPrice where WAREID='" + cmb1.Text +
                    "' and STORAGEID='" + v1 + "'");
                if (dt1.Rows.Count > 0)
                {
                    MessageBox.Show("此品号仓库在系统已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);



                }
                else
                {
                    string var1 = dtx1.Rows[0]["WAREID"].ToString();
                    string var2 = dtx2.Rows[0]["STORAGEID"].ToString();
                    boperate.getcom(@"insert into tb_OutSourcingUnitPrice(OSID,WAREID,STORAGEID,OSUnitPrice,Maker,Date,Year,Month,Day
             ) values('" + txt1.Text +
                        "','" + var1 + "','" + var2 + "', '" + txt2.Text + "', '" + FrmLogin.M_str_name + "','" + varDate +
                        "','" + year + "','" + month + "','" + day + "')");
                    Bind();
                    ClearText();
                    txt1.Text = "";
                }

            }
            else
            {
                dt1 = boperate.getdt("select * from tb_OutSourcingUnitPrice");
                if (dt1.Rows.Count > 0)
                {
                    boperate.getcom(@"update tb_OutSourcingUnitPrice set OSUnitPrice='" + txt2.Text + "',Maker='" + FrmLogin.M_str_name +
                     "',Date='" + varDate + "' where OSID='" + Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim() + "'");
                    Bind();
                }
                else
                {

                    MessageBox.Show("无数据可以更新！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }



        }
 

  
    

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt1.Text = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            cmb1.Text = Convert.ToString(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            cmb2.Text = Convert.ToString(dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            txt2.Text = Convert.ToString(dataGridView1[5, dataGridView1.CurrentCell.RowIndex].Value).Trim();
        }

  
        private void cmb1_DropDown(object sender, EventArgs e)
        {
            cmb1.DataSource = dt3;
            cmb1.DisplayMember = "WareID";
        }

        private void cmb2_DropDown(object sender, EventArgs e)
        {
            cmb2.DataSource = dt4;
            cmb2.DisplayMember = "STORAGEType";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string a = boperate.numN(12, 4, "0001", "select * from tb_OutSourcingUnitPrice", "OSID", "OS");
            if (a == "Exceed limited")
            {
                MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                txt1.Text = a;

            }
            cmb1.Enabled = true;
            cmb2.Enabled = true;
            btnSave.Enabled = true;
            M_int_judge = 0;
            ClearText();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            M_int_judge = 1;
            btnSave.Enabled = true;
            cmb1.Enabled = false;
            cmb2.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                string v1 = Convert.ToString(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value).Trim();
                string v2 = Convert.ToString(dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value).Trim();
                if (boperate.exists("SELECT * FROM TB_OUTSOURCINGGODE WHERE WOID IN (SELECT DISTINCT(WOID) FROM TB_OUTSOURCINGMATERE WHERE WAREID='" + v1 + "' AND STORAGEID='" + v2 + "') ") == true)
                {

                    MessageBox.Show("此委外加工单价在委外加工入库单中已经存在，不允许删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {

                    if (MessageBox.Show("确定要删除该条品号信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        boperate.getcom("delete from tb_OutSourcingUnitPrice where OSID='" + Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim() + "'");
                        Bind();
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {

                }
                else
                {
                    DataSet myds = boperate.getds(M_str_sql + " where wAREID like '%" + textBox1.Text + "%' order by A.WARID,A.Date asc", M_str_table);
                    if (myds.Tables[0].Rows.Count > 0)
                        dataGridView1.DataSource = myds.Tables[0];
                    else
                        MessageBox.Show("没有要查找的相关记录！");

                }
                dgvStateControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
