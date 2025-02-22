using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using XizheC;
namespace C23.UserManage
{
    public partial class FrmEditRight : Form
    {
        DataTable dt = new DataTable();
        basec bc = new basec();
        private static string _UNAME;
        public static string UNAME
        {
            set { _UNAME = value; }
            get { return _UNAME; }

        }
        private static string _ENAME;
        public static string ENAME
        {
            set { _ENAME = value; }
            get { return _ENAME; }

        }
        private static bool _IF_DOUBLE_CLICK;
        public static bool IF_DOUBLE_CLICK
        {
            set { _IF_DOUBLE_CLICK = value; }
            get { return _IF_DOUBLE_CLICK; }

        }
        protected string sql = @"
SELECT
A.UNAME AS 用户名,
B.ENAME AS 姓名,
C.NODE_NAME AS 作业名称,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=C.MAKERID) AS 制单人,
C.DATE AS 制单日期 
FROM  
USERINFO  A 
LEFT JOIN EMPLOYEEINFO B ON A.EMID=B.EMID 
LEFT JOIN RIGHTLIST C ON A.USID=C.USID
";

        protected int M_int_judge, i,j;
        public bool blInitial = true;
        public static string[] getUserInfo = new string[] { "", "", "" };
        public FrmEditRight()
        {
            InitializeComponent();
            //this.cboxUserID.Items.Add("");
        }
        private void frmEditRight_Load(object sender, EventArgs e)
        {
            Bind();
    
        }


        #region bind
        private void Bind()
        {
            string a=bc.getOnlyString("SELECT UNAME FROM USERINFO WHERE USID='" + FrmLogin.USID + "'");
       
            try
            {
                hint.ForeColor = Color.Red;
                hint.Location = new Point(400, 100);
                hint.Text = "";
                dt = bc.getdt(sql+" WHERE A.UNAME='"+a+"'");
                dataGridView1.DataSource = dt;
                dgvStateControl();
                LENAME.Text = dt.Rows[0]["姓名"].ToString();
                comboBox1.Text = a;
                Assignment(dt);
                IF_DOUBLE_CLICK = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
        #region bind1
        private void Bind1()
        {
            try
            {
                hint.ForeColor = Color.Red;
                hint.Location = new Point(400, 100);
                hint.Text = "";
                dt = bc.getdt(sql + " WHERE A.UNAME='" + comboBox1.Text + "'");
                dataGridView1.DataSource = dt;
                dgvStateControl();
                LENAME.Text = "";
                Assignment(dt);
                IF_DOUBLE_CLICK = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
        protected void Assignment(DataTable dt)
        { 
            #region Assignment
            for (int j = 0; j < checkedListBox1.Items.Count; j++)
                checkedListBox1.SetItemChecked(j, false);
            for (int j = 0; j < checkedListBox2.Items.Count; j++)
                checkedListBox2.SetItemChecked(j, false);
          
            for (j = 0; j < dt.Rows.Count; j++)
            {
                bool b = false;
                string vargetNodeContext = dt.Rows[j]["作业名称"].ToString();
           
                for (i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (vargetNodeContext == checkedListBox1.Items[i].ToString())
                    {
                        this.checkedListBox1.SetItemChecked(i, true);
                        b = true;
                        break;
                       
                    }

                }
                if (!b)
                {
                    for (i = 0; i < checkedListBox2.Items.Count; i++)
                    {
                        //MessageBox.Show(vargetNodeContext + "," + checkedListBox2.Items[i].ToString());
                        if (vargetNodeContext == checkedListBox2.Items[i].ToString())
                        {
                            this.checkedListBox2.SetItemChecked(i, true);
                            break;
                        }

                    }
                }
            }
            #endregion
        }
             
        #region dgvStateControl
        private void dgvStateControl()
        {
            int i;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Lavender;
            int numCols1 = dataGridView1.Columns.Count;
            for (i = 0; i < numCols1; i++)
            {

                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i == 2)
                {
                    dataGridView1.Columns[i].Width = 130;

                }
                else if (i == 4)
                {
                    dataGridView1.Columns[i].Width = 120;

                }

                else
                {
                    dataGridView1.Columns[i].Width = 70;

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
                dataGridView1.Columns[i].ReadOnly = true;

            }
        }
        #endregion
 
 
        public void ClearText()
        {
            

        }
   
        #region Select
        private void Select_all_CheckedChanged(object sender, EventArgs e)
        {
            if (Select_all.Checked)
            {
                for (int j = 0; j < checkedListBox1.Items.Count; j++)
                    checkedListBox1.SetItemChecked(j, true);
                for (int j = 0; j < checkedListBox2.Items.Count; j++)
                    checkedListBox2.SetItemChecked(j, true);

            }
            else
            {
                for (int j = 0; j < checkedListBox1.Items.Count; j++)
                    checkedListBox1.SetItemChecked(j, false);
                for (int j = 0; j < checkedListBox2.Items.Count; j++)
                    checkedListBox2.SetItemChecked(j, false);

            }

        }
        #endregion
        #region chbInverse
        private void chbInverse_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
                else
                {
                    checkedListBox1.SetItemChecked(i, true);
                }
            }
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if (checkedListBox2.GetItemChecked(i))
                {
                    checkedListBox2.SetItemChecked(i, false);
                }
                else
                {
                    checkedListBox2.SetItemChecked(i, true);
                }
            }
        }
        #endregion
        private void dgvERInfo_MouseUp(object sender, MouseEventArgs e)
        {
         
        
            

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LENAME.Text = "";
            for (int j = 0; j < checkedListBox1.Items.Count; j++)
                checkedListBox1.SetItemChecked(j, false);
            for (int j = 0; j < checkedListBox2.Items.Count; j++)
                checkedListBox2.SetItemChecked(j, false);
            Select_all.Checked = false;
            chbInverse.Checked = false;
            dataGridView1.DataSource = null;
            search();
        }
        private void search()
        {
            try
            {


                dt = bc.getdt(sql + " WHERE  A.UNAME LIKE '%" + comboBox1.Text + "%'");
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;

                }
                else
                {

                    MessageBox.Show("没有找到相关信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                }
                dgvStateControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void Clear()
        {

            comboBox1.Text = "";
            LENAME.Text = "";
            for (int j = 0; j < checkedListBox1.Items.Count; j++)
                checkedListBox1.SetItemChecked(j, false);
            for (int j = 0; j < checkedListBox2.Items.Count; j++)
                checkedListBox2.SetItemChecked(j, false);
            Select_all.Checked = false;
            chbInverse.Checked = false;
            dataGridView1.DataSource = null;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!juage1())
            {

            }
            else
            {
                save();
            }
            try
            {
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
   
        }
        private void save()
        {


            int varNodeID, varParentNodeID;
            string varNodeContext, varURL;
            string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            string USID = bc.getOnlyString("SELECT USID FROM USERINFO WHERE  UNAME='" +comboBox1 .Text  + "'");
                 bc.getcom("DELETE RIGHTLIST WHERE USID='" +USID + "'");
                 for (i = 0; i < checkedListBox1.Items.Count; i++)
                 {
                     if (checkedListBox1.GetItemChecked(i))
                     {


                         string v1 = checkedListBox1.Items[i].ToString();
                        
                         dt = basec.getdts("select * from RightName where NODE_NAME='" + v1 + "'");
                         if (dt.Rows.Count > 0)
                         {
                             varNodeID = Convert.ToInt32(dt.Rows[0]["NodeID"].ToString());
                             varNodeContext = dt.Rows[0]["Node_NAME"].ToString();
                             varParentNodeID = Convert.ToInt32(dt.Rows[0]["Parent_NodeID"].ToString());
                             varURL = dt.Rows[0]["URL"].ToString();

                             if (!bc.exists("select * from RightList where USID='" +USID   + "' and NodeID='" + varNodeID + "'"))
                             {
                                 string tempSendStrSQL = "insert RightList (USID,NodeID,Node_name,"
                                    + "Parent_NodeID,URL,MakerID,Date) values ('" +USID + "','" + varNodeID + "','" + v1 +
                                    "','" + varParentNodeID + "','" + varURL + "','" + FrmLogin.USID +
                                    "','" + varDate + "' )";
                                 basec.getcoms(tempSendStrSQL);
                             }
                         }
                     }
                 }
                 for (i = 0; i < checkedListBox2.Items.Count; i++)
                 {
                     if (checkedListBox2.GetItemChecked(i))
                     {


                         string v1 = checkedListBox2.Items[i].ToString();
                    
                         dt = basec.getdts("select * from RightName where NODE_NAME='" + v1 + "'");
                         if (dt.Rows.Count > 0)
                         {
                             varNodeID = Convert.ToInt32(dt.Rows[0]["NodeID"].ToString());
                             varNodeContext = dt.Rows[0]["Node_NAME"].ToString();
                             varParentNodeID = Convert.ToInt32(dt.Rows[0]["Parent_NodeID"].ToString());
                             varURL = dt.Rows[0]["URL"].ToString();

                             if (!bc.exists("select * from RightList where USID='" + USID + "' and NodeID='" + varNodeID + "'"))
                             {
                                 string tempSendStrSQL = "insert RightList (USID,NodeID,Node_name,"
                                    + "Parent_NodeID,URL,MakerID,Date) values ('" + USID + "','" + varNodeID + "','" + v1 +
                                    "','" + varParentNodeID + "','" + varURL + "','" + FrmLogin.USID +
                                    "','" + varDate + "' )";
                                 basec.getcoms(tempSendStrSQL);
                             }
                         }
                     }
                 }
                 Bind1();

             
             

        }

        #region juage1()
        private bool juage1()
        {

            bool ju = true;
            if (comboBox1 .Text == "")
            {
                ju = false;
                hint.Text = "用户名不能为空！";

            }
            else if (!bc.exists("SELECT * FROM USERINFO WHERE UNAME='" + comboBox1 .Text + "'"))
            {
                ju = false;
                hint.Text = "用户名在系统中不存在！";

            }

            return ju;

        }
        #endregion
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string v1 = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
      
            dt = bc.getdt(sql + " WHERE A.UNAME='"+v1+"'");
            if (dt.Rows.Count > 0)
            {
                Assignment(dt);
              
            }
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            C23.UserManage.FrmUSER_INFO FRM = new FrmUSER_INFO();
            FRM.EditRight();
            FRM.ShowDialog();
            this.comboBox1.IntegralHeight = false;//使组合框不调整大小以显示其所有项
            this.comboBox1.DroppedDown = false;//使组合框不显示其下拉部分
            this.comboBox1.IntegralHeight = true;//恢复默认值
            if (IF_DOUBLE_CLICK)
            {
                comboBox1.Text = UNAME;
                LENAME.Text = ENAME;
                search();

                dt = bc.getdt(sql + " WHERE  A.UNAME ='" + comboBox1.Text + "'");
                Assignment(dt);
               
            }
       

        }

    }
}
