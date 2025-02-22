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
    public partial class FrmMiscGodE: Form
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dtx = new DataTable();

        C23.BaseClass.BaseOperate boperate = new C23.BaseClass.BaseOperate();
        C23.BaseClass.OperateAndValidate opAndvalidate = new C23.BaseClass.OperateAndValidate();
        protected string M_str_sql = @"select A.GEKEY AS 索引,A.SN as 项次,A.GodEID as 入库单号,A.WareID as 品号,"
            +"B.WName as 品名,B.ExternalM as 套件,B.Type as 型号,B.DETAIL AS 细节,B.Leather AS 皮种,B.COLOR AS 颜色,"
            +"B.StitchingC AS 线色,B.Thickness AS 海棉厚度,A.GECount as 入库数量,"
            +"F.StorageType as 仓库,A.Goder as 入库员,A.GodEDate as 入库日期, A.Maker as 制单人," +
"A.Date as 制单日期 from tb_GodE A LEFT JOIN TB_WAREINFO B ON A.WAREID=B.WAREID  LEFT JOIN TB_Storageinfo F ON F.STORAGEID=A.STORAGEID";

        protected string M_str_table = "tb_GodE";
        protected int M_int_judge, i,look;
        protected int getdata;
        public static string[] data1 = new string[] { null, null};
        public static string[] data2 = new string[] { "","" };
        public static string[] data3 = new string[] { "" };
        public static string[] data4 = new string[] { "" };
        string sql1 = "select distinct(godeid) from tb_gode WHERE SUBSTRING(GODEID,1,2)='GE' ";
        string sql4 = "select distinct(B.WNAME)  AS WNAME from tb_GODE A LEFT JOIN TB_WAREINFO B ON A.WAREID=B.WAREID WHERE SUBSTRING(A.GODEID,1,2)='GE'";
        public FrmMiscGodE()
        {
            InitializeComponent();
        }
        #region init
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMiscGodE));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmb2 = new System.Windows.Forms.ComboBox();
            this.btnToExcel = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.PictureBox();
            this.btnExit = new System.Windows.Forms.PictureBox();
            this.btnSearch = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(3, 217);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1046, 370);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cmb2);
            this.groupBox1.Controls.Add(this.btnToExcel);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmb1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpEndDate);
            this.groupBox1.Controls.Add(this.dtpStartDate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 127);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1046, 84);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(309, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 20;
            this.label6.Text = "品名";
            // 
            // cmb2
            // 
            this.cmb2.FormattingEnabled = true;
            this.cmb2.Location = new System.Drawing.Point(344, 20);
            this.cmb2.Name = "cmb2";
            this.cmb2.Size = new System.Drawing.Size(207, 20);
            this.cmb2.TabIndex = 19;
            // 
            // btnToExcel
            // 
            this.btnToExcel.FlatAppearance.BorderSize = 0;
            this.btnToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToExcel.Font = new System.Drawing.Font("宋体", 9F);
            this.btnToExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnToExcel.Image")));
            this.btnToExcel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnToExcel.Location = new System.Drawing.Point(841, 13);
            this.btnToExcel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnToExcel.Name = "btnToExcel";
            this.btnToExcel.Size = new System.Drawing.Size(50, 64);
            this.btnToExcel.TabIndex = 11;
            this.btnToExcel.Text = "导出";
            this.btnToExcel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnToExcel.UseVisualStyleBackColor = false;
            this.btnToExcel.Click += new System.EventHandler(this.btnToExcel_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(57, 57);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.chk2_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(78, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "入库单号";
            // 
            // cmb1
            // 
            this.cmb1.Location = new System.Drawing.Point(137, 20);
            this.cmb1.Name = "cmb1";
            this.cmb1.Size = new System.Drawing.Size(121, 20);
            this.cmb1.TabIndex = 7;
            this.cmb1.DropDown += new System.EventHandler(this.cmbGodEID_DropDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(296, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "~";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "yyyy/MM/dd";
            this.dtpEndDate.Enabled = false;
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(344, 50);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(121, 21);
            this.dtpEndDate.TabIndex = 5;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "yyyy/MM/dd";
            this.dtpStartDate.Enabled = false;
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(137, 50);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(121, 21);
            this.dtpStartDate.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(78, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "日期期间";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(857, 95);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 29;
            this.label11.Text = "退出";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(771, 95);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 28;
            this.label12.Text = "搜索";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.btnExit);
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1046, 121);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "菜单栏";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(28, 95);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(29, 12);
            this.label17.TabIndex = 24;
            this.label17.Text = "新增";
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.InitialImage = null;
            this.btnAdd.Location = new System.Drawing.Point(12, 20);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 60);
            this.btnAdd.TabIndex = 16;
            this.btnAdd.TabStop = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.InitialImage = null;
            this.btnExit.Location = new System.Drawing.Point(843, 20);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(60, 60);
            this.btnExit.TabIndex = 19;
            this.btnExit.TabStop = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.InitialImage = null;
            this.btnSearch.Location = new System.Drawing.Point(757, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(60, 60);
            this.btnSearch.TabIndex = 18;
            this.btnSearch.TabStop = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // FrmMiscGodE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1052, 616);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FrmMiscGodE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "杂项入库作业";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmWorkGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion


        private void FrmWorkGroup_Load(object sender, EventArgs e)
        {
            Bind();
        }
        #region Bind
        public   void Bind()
        {
            dt1 = boperate.getdt(sql1);
            AutoCompleteStringCollection inputInfoSource = new AutoCompleteStringCollection();
            AutoCompleteStringCollection inputInfoSource2 = new AutoCompleteStringCollection();
            foreach (DataRow dr in dt1.Rows)
            {
                //cmbType.Items.Add(dr[0].ToString ());
                inputInfoSource.Add(dr[0].ToString());


            }
            this.cmb1 .AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb1 .AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmb1 .AutoCompleteCustomSource = inputInfoSource;

            foreach (DataRow dr in boperate.getdt(sql4).Rows)
            {
                cmb2.Items.Add(dr[0].ToString ());
                inputInfoSource2.Add(dr[0].ToString());
            }
            this.cmb2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmb2.AutoCompleteCustomSource = inputInfoSource2;

            dgvStateControl();
       
        }
        #endregion
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
                if (i == 0)
                {
                    dataGridView1.Columns[i].Width = 130;

                }
               else  if (i==17 || i==15)
                {
                    dataGridView1.Columns[i].Width = 120;

                }
               else  if (i == 2 || i==3 || i==12 || i==14 || i==15 || i==16 ||  i==13)
                {
                    dataGridView1.Columns[i].Width = 90;

                }
                else if (i == 4 )
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
                if (i == 6)
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
 
        private void tsbtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定要删除该条品号信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    boperate.getcom("delete from tb_FrmPurchaseInventoryEwhere WorkGroupM='" + Convert.ToString(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value).Trim() + "'");
                    Bind();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }
        #region look
 
       private  void ak()
        {
       
            string v1 = dtpStartDate.Value.ToString("yyyy/MM/dd 00:00:00").Replace("-", "/");
            string v2 = dtpEndDate.Value.ToString("yyyy/MM/dd 23:59:59").Replace("-", "/");
            string sql;
            if (checkBox1.Checked)
            {
                sql = @" WHERE A.GODEID LIKE '%" + cmb1.Text + "%' AND  SUBSTRING(A.GODEID,1,2)='GE'  AND A.DATE BETWEEN '" + v1 + "' AND '" + v2 +
          "' AND B.WNAME LIKE '%" + cmb2.Text + "%' ORDER BY A.GODEID,A.SN ASC";

            }
            else
            {
                sql = @" WHERE A.GODEID LIKE '%" + cmb1.Text + "%' AND  SUBSTRING(A.GODEID,1,2)='GE' AND B.WNAME LIKE '%" + cmb2.Text + "%' ORDER BY A.GODEID,A.SN ASC";

            }
            dt = boperate.getdt(M_str_sql + sql);
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
                DataRow dr = dt.NewRow();
                dr["入库数量"] = dt.Compute("SUM(入库数量)", "").ToString();
                dt.Rows.Add(dr);
                dgvStateControl();
            }
            else
            {
                MessageBox.Show("没有找到相关记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dt = null;
                dataGridView1.DataSource = dt;


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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbGodEID_DropDown(object sender, EventArgs e)
        {
            cmb1.DataSource = dt1;
            cmb1.DisplayMember = "GODEID";
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmMiscGodET frm = new FrmMiscGodET();
            FrmMiscGodET.str6[0] = Convert.ToString(dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            FrmMiscGodET.str6[1] = Convert.ToString(dataGridView1[12, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            frm.ShowDialog();
        }

        private void chk2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dtpStartDate.Enabled = true;
                dtpEndDate.Enabled = true;
            }
            else
            {
                dtpStartDate.Enabled = false;
                dtpEndDate.Enabled = false;

            }
        }


        private void btnToExcel_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                boperate.dgvtoExcel(dataGridView1, "采购入库异动作业明细");
            }
            else
            {
                MessageBox.Show("没有数据可导出！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            string a1 = boperate.numN2(12, 4, "0001", "select * from tb_GodE where substring(GODEID,1,2)='GE' AND YEAR='" + year + "' AND MONTH='" + month + "' AND DAY='" + day + "'", "GodEID", "GE");
            if (a1 == "Exceed Limited")
            {
                MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                FrmMiscGodET.str1[0] = a1;
                FrmMiscGodET frm = new FrmMiscGodET();
                frm.Show();

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ak();
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
