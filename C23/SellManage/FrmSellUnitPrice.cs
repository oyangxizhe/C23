using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace C23.SellManage
{
    public partial class FrmSellUnitPrice : Form
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();

        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dtx1 = new DataTable();
        DataTable dtx2 = new DataTable();
        C23.BaseClass.BaseOperate boperate = new C23.BaseClass.BaseOperate();
        C23.BaseClass.OperateAndValidate opAndvalidate = new C23.BaseClass.OperateAndValidate();
        protected string M_str_sql = @"select A.SPID AS 销售单价代码,A.CUID AS 客户代码,B.CNAME AS 客户名称,A.LEATHER AS 皮种,A.SELLUnitPrice as 销售单价,
            A.Maker as 制单人,A.Date as 日期 from tb_SellUnitPrice A LEFT JOIN TB_CUSTOMERINFO B ON A.CUID=B.CUID";
        protected string M_str_table = "tb_SellUnitPrice";
        protected int M_int_judge, i;
        protected int getdata;
        string sql3 = "select * from tb_CUSTOMERINFO";
        string sql4 = "select * from tb_LEATHER";
        public FrmSellUnitPrice()
        {
            InitializeComponent();
        }

        private void FrmSellUnitPrice_Load(object sender, EventArgs e)
        {

            Bind();

        }
        #region Bind
        private void Bind()
        {
            btnSave.Enabled = false;
            this.dataGridView1.ReadOnly = true;
            dt = boperate.getdt(M_str_sql + "  order by A.SPID,A.date asc");
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


                if (i == 2)
                {
                    dataGridView1.Columns[i].Width = 200;

                }
                else if (i == 6)
                {
                    dataGridView1.Columns[i].Width = 120;

                }
                else if (i == 3)
                {

                    dataGridView1.Columns[i].Width = 60;
                }
                else
                {
                    dataGridView1.Columns[i].Width = 90;

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

                inputInfoSource3.Add(dr3["CNAME"].ToString());


            }
            this.cmb1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmb1.AutoCompleteCustomSource = inputInfoSource3;
            foreach (DataRow dr4 in dt4.Rows)
            {

                inputInfoSource4.Add(dr4["LEATHER"].ToString());
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
  
        private bool juage1()
        {
            bool ju = true;
            if (cmb1.Text == "")
            {
                ju = false;
                MessageBox.Show("客户名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (boperate.exists("select * from tb_CUSTOMERINFO where CNAME='" + cmb1.Text + "'") == false)
            {
                ju = false;
                MessageBox.Show("此客户名称在系统中不存在！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else if (cmb2.Text == "")
            {
                ju = false;
                MessageBox.Show("皮种不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (boperate.exists("select * from tb_leather where leather='" + cmb2.Text + "'") == false)
            {
                ju = false;
                MessageBox.Show("此皮种在系统中不存在！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else if (txt2.Text == "")
            {
                ju = false;
                MessageBox.Show("销售单价不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (boperate.yesno(txt2.Text) == 0)
            {
                ju = false;
                MessageBox.Show("销售单价只能输入数字！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return ju;

        }
        private void as1()
        {
            string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            if (M_int_judge == 0)
            {
                string v1 = boperate.getOnlyString("select * from tb_CUSTOMERINFO where CNAME='" + cmb1.Text + "'");
                dt1 = boperate.getdt("select * from tb_SellUnitPrice where CUID='" + v1 +
                    "' and LEATHER='" + cmb2.Text + "'");
                if (dt1.Rows.Count > 0)
                {
                    MessageBox.Show("此客户名称皮种在系统已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {

                    boperate.getcom(@"insert into tb_SellUnitPrice(SPID,CUID,LEATHER,SELLUnitPrice,Maker,Date,Year,Month,Day) values('" + txt1.Text +
                        "','" + v1 + "','" + cmb2.Text + "', '" + txt2.Text + "', '" + FrmLogin.UName  + "','" + varDate +
                        "','" + year + "','" + month + "','" + day + "')");
                    Bind();
                    ClearText();
                    txt1.Text = "";
                }

            }
            else
            {
                dt1 = boperate.getdt("select * from tb_SellUnitPrice");
                if (dt1.Rows.Count > 0)
                {
                    boperate.getcom(@"update tb_SellUnitPrice set SELLUnitPrice='" + txt2.Text + "',Maker='" + FrmLogin.UName  +
                     "',Date='" + varDate + "' where SPID='" + Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim() + "'");
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
            cmb1.Text = Convert.ToString(dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            cmb2.Text = Convert.ToString(dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            txt2.Text = Convert.ToString(dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value).Trim();
        }


        private void cmb1_DropDown(object sender, EventArgs e)
        {
            cmb1.DataSource = dt3;
            cmb1.DisplayMember = "CNAME";
        }

        private void cmb2_DropDown(object sender, EventArgs e)
        {
            cmb2.DataSource = dt4;
            cmb2.DisplayMember = "LEATHER";
        }

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string a = boperate.numN(12, 4, "0001", "select * from tb_SellUnitPrice", "SPID", "SP");
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

                if (juage1() == true)
                {
                    as1();
                }
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
                if (MessageBox.Show("确定要删除该条品号信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    boperate.getcom("delete from tb_SellUnitPrice where SPID='" + Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim() + "'");
                    Bind();
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
                    DataSet myds = boperate.getds(M_str_sql + " where B.CNAME like '%" + textBox1.Text + "%' order by A.SPID,A.Date asc", M_str_table);
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
