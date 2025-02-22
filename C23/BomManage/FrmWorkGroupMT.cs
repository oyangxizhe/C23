using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace C23.BomManage
{
    public partial class FrmWorkGroupMT : Form
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dtx = new DataTable();
        DataTable dtd = new DataTable();
        DataTable dt4 = new DataTable();

        C23.BaseClass.BaseOperate boperate = new C23.BaseClass.BaseOperate();
        C23.BaseClass.OperateAndValidate opAndvalidate = new C23.BaseClass.OperateAndValidate();
        protected string M_str_sql = @"select C.WGKEY as 索引,a.WORKER as 工种,C.EMID AS 工号,B.ENAME as 员工姓名,
c.Maker as 制单人,c.Date as 日期 from tb_WorkGroupM c left join tb_worker a on C.WKID=A.WKID LEFT JOIN EMPLOYEEINFO B ON 
C.EMID=B.EMID left join tb_workgroup d on d.wgid=c.wgid";
        protected string M_str_table = "tb_WorkGroupM";
        protected int M_int_judge, i, look, vdt;
        protected int getdata;
        public static string[] data1 = new string[] { null, null };
        public static string[] data2 = new string[] { "", "" };
        public static string[] data3 = new string[] { "" };
        public static string[] data4 = new string[] { "" };
        public static string[] str1 = new string[] { "" };
        public static string[] str2 = new string[] { "", "" };

        public static int str3;
        string sql4 = "select * from tb_WorkGroup order by WGID,date asc";
        private string _WorkGroup;
        public string WorkGroup
        {
            get { return _WorkGroup; }
            set { _WorkGroup = value; }


        }
        private int _Status;
        public int Status
        {
            set { _Status = value; }
            get { return _Status; }

        }
        public FrmWorkGroupMT()
        {

            InitializeComponent();
     
        }
        #region init
        #endregion
        private void FrmWorkGroup_Load(object sender, EventArgs e)
        {

            Bind();
        }
        #region Bind
        private void Bind()
        {
            btnSave.Enabled = true;
            btnEdit.Enabled = false;
            if (Status == 1)
            {
                dt = total1();
                dataGridView1.DataSource = dt;
              
            }
            else if(WorkGroup !="")
            {
                //cmbWorkGroup.Text = str2[0];
                cmbWorkGroup.Text = WorkGroup;
                as1();
                vdt = 1;
                WorkGroup = "";
                //str2[0] = "";
                btnSave.Enabled = false;
                dtd = boperate.getdt(M_str_sql + " WHERE D.WORKGROUP='" + cmbWorkGroup.Text + "' ORDER BY C.WGID");
                if (dtd.Rows.Count > 0)
                {
                    btnDel.Enabled = true;
                    btnEdit.Enabled = true;
                 
                }
                else
                {
                    btnDel.Enabled = false;
                }
                cmbWorkGroup.Enabled = false;
            }
            else
            {
                dt = total1();
                dataGridView1.DataSource = dt;
      

            }
            dgvStateControl();
            dt4 = boperate.getdt(sql4);
          
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

                if (i == 0)
                {
                    dataGridView1.Columns[i].Width = 100;
                }

                else if (i == 5)
                {
                    dataGridView1.Columns[i].Width = 120;

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
            for (i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i == 1 || i == 2)
                {
                    dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
            for (i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i == 1 || i == 2)
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
        #region total1
        private DataTable total1()
        {
            dt = new DataTable();
            dt.Columns.Add("索引", typeof(string));
            dt.Columns.Add("工种", typeof(string));
            dt.Columns.Add("工号", typeof(string));
            dt.Columns.Add("姓名", typeof(string));
            dt.Columns.Add("制单人", typeof(string));
            dt.Columns.Add("制单日期", typeof(string));
            dt.Rows.Add();
            return dt;
        }
        #endregion
        private void as1()
        {
            dt3.Columns.Add("索引", typeof(string));
            dt3.Columns.Add("工种", typeof(string));
            dt3.Columns.Add("工号", typeof(string));
            dt3.Columns.Add("姓名", typeof(string));
            dt3.Columns.Add("制单人", typeof(string));
            dt3.Columns.Add("制单日期", typeof(string));

            //DataTable dtx1 = boperate.getdt(M_str_sql + " WHERE D.WORKGROUP='" + str2[0] + "'");
            DataTable dtx1 = boperate.getdt(M_str_sql + " WHERE D.WORKGROUP='" + WorkGroup + "'");
            for (i = 0; i < dtx1.Rows.Count; i++)
            {
                DataRow dr = dt3.NewRow();
                dr["索引"] = dtx1.Rows[i][0].ToString();
                dr["工种"] = dtx1.Rows[i][1].ToString();
                dr["工号"] = dtx1.Rows[i][2].ToString();
                dr["姓名"] = dtx1.Rows[i][3].ToString();
                dr["制单人"] = dtx1.Rows[i][4].ToString();
                dr["制单日期"] = dtx1.Rows[i][5].ToString();
                dt3.Rows.Add(dr);


            }

            dataGridView1.DataSource = dt3;


        }
        #region add
 
        #endregion
        private void ClearText()
        {
            cmbWorkGroup.Text = "";
        }
        #region save
 
        private void n1()
        {

            if (dt.Rows.Count > 0)
            {
                if (ac() == 0)
                {
                }
                else
                {
                    wf();
                }
            }


            else if (dt3.Rows.Count > 0)
            {

                if (ad() == 0)
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
                dtd = boperate.getdt(M_str_sql + " WHERE D.WORKGROUP='" + cmbWorkGroup.Text + "' ORDER BY C.WGID");
                if (dtd.Rows.Count > 0)
                {
                    MessageBox.Show("组名已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    insertdb(dt);
                }
            }
            else
            {
             
              
                dt2 = boperate.getdt("select * from tb_WorkGroupM");
                if (dt2.Rows.Count > 0)
                {
                    dtd = boperate.getdt(M_str_sql + " WHERE D.WORKGROUP='" + cmbWorkGroup.Text + "' ORDER BY C.WGID");
                    if (dt.Rows.Count >= dtd.Rows.Count)
                    {
                        boperate.getcom("delete tb_WorkGroupM WHERE WGID IN (SELECT WGID FROM TB_WORKGROUP WHERE WORKGROUP='"+cmbWorkGroup .Text +"')");
                        insertdb(dt);

                    }
                   else  if (dt3.Rows.Count >= dtd.Rows.Count)
                    {
                      
                        boperate.getcom("delete tb_WorkGroupM WHERE WGID IN (SELECT WGID FROM TB_WORKGROUP WHERE WORKGROUP='"+cmbWorkGroup .Text +"')");
                        insertdb(dt3);

                    }
                }
                else
                {

                    MessageBox.Show("无数据可以更新！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void insertdb(DataTable dtv)
        {
            string varDate = DateTime.Now.ToString();
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            for (i = 0; i < dtv.Rows.Count; i++)
            {
                string WGKEY = boperate.numN(14, 6, "000001", "select * from tb_workgroupM", "WGKEY", "WG");
                string wgkey1;
                if (WGKEY == "Exceed Limited")
                {

                    MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    wgkey1 = WGKEY;
                    DataTable dtz = boperate.getdt("select WKID from tb_worker where worker='" + dtv.Rows[i]["工种"].ToString() + "'");
                    //DataTable dtz1 = boperate.getdt("select WGID FROM TB_WORKGROUP WHERE WORKGROUOP='"+dtv .Rows [");
                    if (dtz.Rows.Count > 0)
                    {
                        string v1 = dtz.Rows[0][0].ToString();
                        DataTable dtz2 = boperate.getdt("select * from tb_workgroup where workgroup='"+cmbWorkGroup .Text +"'");
                        string v3 = dtz2.Rows[0]["WGID"].ToString();
                        boperate.getcom(@"insert into tb_WorkGroupM(WGKEY,WGID,WKID,EMID,Maker,Date,Year,Month,Day) values('" + wgkey1 +
      "','" + v3 +
      "','" + v1 + "','" + dtv.Rows[i]["工号"].ToString() +
      "','" + FrmLogin.M_str_name + "','" + varDate + "','" + year + "','" + month + "','" + day + "')");

                    }
                }

            }
            dtd = boperate.getdt(M_str_sql + " WHERE D.WORKGROUP='" + cmbWorkGroup.Text + "' ORDER BY C.WGID,C.DATE ASC");
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

        private bool tof()
        {
            
            DataTable dtz = boperate.getdt(@"select A.WGID from tb_PIECEWAGES A LEFT JOIN TB_WORKGROUP B
                    ON A.WGID=B.WGID WHERE B.WORKGROUP='" + cmbWorkGroup.Text + "'");
            if (dtz.Rows.Count > 0)
            {
            
          
                return true;
            }
            else
                return false;

        }
  



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
        private int ac()
        {
            int x = 1;

            string v1 = dt.Rows[dataGridView1.CurrentCell.RowIndex][1].ToString();
            string v2 = dt.Rows[dataGridView1.CurrentCell.RowIndex][2].ToString();
            string v3 = dt.Rows[dataGridView1.CurrentCell.RowIndex][3].ToString();

            if (v1 == "")
            {
                x = 0;
                MessageBox.Show("工种不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (v2 == "")
            {
                x = 0;
                MessageBox.Show("工号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (v3 == "")
            {
                x = 0;
                MessageBox.Show("姓名不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                DataTable dtx1 = boperate.getdt("select * from tb_Worker where worker='" + v1 + "'");
                DataTable dtx2 = boperate.getdt("select * from employeeinfo where EMID='" + v2 + "'");
                if (dtx1.Rows.Count > 0)
                {
                    if (dtx2.Rows.Count > 0)
                    {


                    }
                    else
                    {
                        x = 0;
                        MessageBox.Show("工号不存在于系统中！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                }
                else
                {
                    x = 0;
                   
                    MessageBox.Show("工种不存在于系统中！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }


            }
            return x;

        }
        #endregion
        #region ad()
        private int ad()
        {
            int x = 1;

            string v1 = dt3.Rows[dataGridView1.CurrentCell.RowIndex][1].ToString();
            string v2 = dt3.Rows[dataGridView1.CurrentCell.RowIndex][2].ToString();
            string v3 = dt3.Rows[dataGridView1.CurrentCell.RowIndex][3].ToString();

            if (v1 == "")
            {
                x = 0;
                MessageBox.Show("工种不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (v2 == "")
            {
                x = 0;
                MessageBox.Show("工号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (v3 == "")
            {
                x = 0;
                MessageBox.Show("姓名不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                DataTable dtx1 = boperate.getdt("select * from tb_Worker where worker='" + v1 + "'");
                DataTable dtx2 = boperate.getdt("select * from EMPLOYEEINFO where EMID='" + v2 + "'");
                if (dtx1.Rows.Count > 0)
                {
                    if (dtx2.Rows.Count > 0)
                    {


                    }
                    else
                    {
                        x = 0;
                        MessageBox.Show("工号不存在于系统中！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                }
                else
                {
                    x = 0;
                    MessageBox.Show(v1);
                    MessageBox.Show("工种不存在于系统中！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }


            }
            return x;

        }
        #endregion
 
        #region cellclick
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 1)
                {
                    C23.BomManage.FrmWorker frm = new C23.BomManage.FrmWorker();
                    frm.a1();
                    frm.ShowDialog();
                    if (data4[0] == "doubleclick")
                    {
                        dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value = data1[1];
                        data4[0] = "";

                    }
                }
                if (dataGridView1.CurrentCell.ColumnIndex == 2)
                {
                    C23.EmployeeManage.FrmEmployeeInfo frm = new C23.EmployeeManage.FrmEmployeeInfo();
                    frm.a1();
                    frm.ShowDialog();
                    if (data3[0] == "doubleclick")
                    {
                        dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value = data2[0];
                        dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value = data2[1];
                        data3[0] = "";
                    }
                    dataGridView1.CurrentCell = dataGridView1[5, dataGridView1.CurrentCell.RowIndex];
                }

            
   
        }
        #endregion
        #region cellenter
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridView1.Columns.Count - 1 && e.RowIndex == dataGridView1.Rows.Count - 1) //控制行、列
                {
                  
                        if (dt.Rows.Count > 0)
                        {
                            if (ac() != 0)
                            {
                                dt.Rows.Add();
                            }
                        }
                        else if (dt3.Rows.Count > 0)
                        {
                            if (ad() != 0)
                            {
                                dt3.Rows.Add();
                            }
                          
                        }

                    }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }

        }
        #endregion
        #region deleteonliy
        private void tmdelete_Click(object sender, EventArgs e)
        {
            try
            {
              
                    if (dt3.Rows.Count > 0)
                    {
                        string v1 = dt3.Rows[dataGridView1.CurrentCell.RowIndex][0].ToString();
                        boperate.getcom("delete from tb_WorkGroupM where WGKEY='" + v1 + "'");
                        dt3 = boperate.getdt(M_str_sql + " WHERE D.WORKGROUP='" + cmbWorkGroup.Text + "' ORDER BY D.WGID,C.DATE");
                        dataGridView1.DataSource = dt3;


                    }
                    else if (dt.Rows.Count > 0)
                    {
                        string v1 = dt.Rows[dataGridView1.CurrentCell.RowIndex][0].ToString();
                        boperate.getcom("delete from tb_WorkGroupM where WGKEY='" + v1 + "'");
                        dt = boperate.getdt(M_str_sql + " WHERE D.WORKGROUP='" + cmbWorkGroup.Text + "' ORDER BY D.WGID,C.DATE");
                        dataGridView1.DataSource = dt;


                    }
                    dgvStateControl();


            

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }
        #endregion
        private void cmbWorkGroup_DropDown(object sender, EventArgs e)
        {
            cmbWorkGroup.DataSource = dt4;
            cmbWorkGroup.DisplayMember = "WorkGroup";
            dataGridView1.CurrentCell = dataGridView1[1, dataGridView1.CurrentCell.RowIndex];
        }

 

        private void btnAdd_Click(object sender, EventArgs e)
        {
            cmbWorkGroup.Enabled = true;
            Bind();
            btnSave.Enabled = true;
            cmbWorkGroup.Text = "";
            M_int_judge = 0;
            ClearText();
            str3 = 0;
    
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            M_int_judge = 1;
            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string varDate = DateTime.Now.ToString();
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");

            try
            {
                if (cmbWorkGroup.Text == "")
                {
                    MessageBox.Show("组名不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    DataTable dtt1 = boperate.getdt("select * from tb_workgroup where WORKGROUP='" + cmbWorkGroup.Text + "'");
                    if (dtt1.Rows.Count > 0)
                    {
                        n1();


                    }
                    else
                    {

                        MessageBox.Show("该组名不存在系统中！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                boperate.getcom("delete tb_WorkGroupM WHERE WGID IN (SELECT WGID FROM TB_WORKGROUP WHERE WORKGROUP='" + cmbWorkGroup.Text + "')");
                cmbWorkGroup.Text = "";
                Bind();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
