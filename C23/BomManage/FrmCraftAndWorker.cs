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
    public partial class FrmCraftAndWorker : Form
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();

        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dtx1 = new DataTable();
        DataTable dtx2 = new DataTable();
        C23.BaseClass.BaseOperate boperate = new C23.BaseClass.BaseOperate();
        C23.BaseClass.OperateAndValidate opAndvalidate = new C23.BaseClass.OperateAndValidate();
        protected string M_str_sql = @"select CWID AS 基价代码,CrID AS 工艺代码, Craft AS 工艺,WKID AS 工种代码,Worker as 工种,CWUnitPrice as 基价,
            Maker as 制单人,Date as 日期 from tb_CraftAndWorker";
        protected string M_str_table = "tb_CraftAndWorker";
        protected int M_int_judge, i;
        protected int getdata;
        string sql3 = "select * from tb_Craft";
        string sql4 = "select * from tb_worker";
        int k;
        public FrmCraftAndWorker()
        {
            InitializeComponent();
        }

        #region init
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCraftAndWorker));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCWUnitPrice = new System.Windows.Forms.TextBox();
            this.cmbWorker = new System.Windows.Forms.ComboBox();
            this.cmbCraft = new System.Windows.Forms.ComboBox();
            this.txtCraftAndWorkerID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtKeyWord = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.PictureBox();
            this.btnExit = new System.Windows.Forms.PictureBox();
            this.btnEdit = new System.Windows.Forms.PictureBox();
            this.btnSearch = new System.Windows.Forms.PictureBox();
            this.btnSave = new System.Windows.Forms.PictureBox();
            this.btnDel = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDel)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 216);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(943, 400);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCWUnitPrice);
            this.groupBox1.Controls.Add(this.cmbWorker);
            this.groupBox1.Controls.Add(this.cmbCraft);
            this.groupBox1.Controls.Add(this.txtCraftAndWorkerID);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(0, 127);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(943, 84);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "工种信息";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(658, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "基价";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(460, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "工种";
            // 
            // txtCWUnitPrice
            // 
            this.txtCWUnitPrice.Location = new System.Drawing.Point(693, 28);
            this.txtCWUnitPrice.Name = "txtCWUnitPrice";
            this.txtCWUnitPrice.Size = new System.Drawing.Size(100, 21);
            this.txtCWUnitPrice.TabIndex = 15;
            // 
            // cmbWorker
            // 
            this.cmbWorker.FormattingEnabled = true;
            this.cmbWorker.Location = new System.Drawing.Point(495, 28);
            this.cmbWorker.Name = "cmbWorker";
            this.cmbWorker.Size = new System.Drawing.Size(121, 20);
            this.cmbWorker.TabIndex = 14;
            this.cmbWorker.DropDown += new System.EventHandler(this.cmbWorker_DropDown);
            // 
            // cmbCraft
            // 
            this.cmbCraft.FormattingEnabled = true;
            this.cmbCraft.Location = new System.Drawing.Point(293, 28);
            this.cmbCraft.Name = "cmbCraft";
            this.cmbCraft.Size = new System.Drawing.Size(121, 20);
            this.cmbCraft.TabIndex = 13;
            this.cmbCraft.DropDown += new System.EventHandler(this.cmbCraft_DropDown);
            // 
            // txtCraftAndWorkerID
            // 
            this.txtCraftAndWorkerID.Location = new System.Drawing.Point(112, 27);
            this.txtCraftAndWorkerID.Name = "txtCraftAndWorkerID";
            this.txtCraftAndWorkerID.ReadOnly = true;
            this.txtCraftAndWorkerID.Size = new System.Drawing.Size(100, 21);
            this.txtCraftAndWorkerID.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(258, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "工艺";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "基价代码";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(857, 95);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 29;
            this.label10.Text = "退出";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(771, 95);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 28;
            this.label9.Text = "搜索";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(287, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 27;
            this.label8.Text = "删除";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtKeyWord);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.btnExit);
            this.groupBox2.Controls.Add(this.btnEdit);
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.btnDel);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(936, 121);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "菜单栏";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(200, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 26;
            this.label7.Text = "保存";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(113, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 25;
            this.label6.Text = "修改";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 24;
            this.label5.Text = "新增";
            // 
            // txtKeyWord
            // 
            this.txtKeyWord.Location = new System.Drawing.Point(496, 59);
            this.txtKeyWord.Name = "txtKeyWord";
            this.txtKeyWord.Size = new System.Drawing.Size(100, 21);
            this.txtKeyWord.TabIndex = 23;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(449, 68);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 22;
            this.label11.Text = "工艺：";
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
            // btnEdit
            // 
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.InitialImage = null;
            this.btnEdit.Location = new System.Drawing.Point(99, 20);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(60, 60);
            this.btnEdit.TabIndex = 17;
            this.btnEdit.TabStop = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
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
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.InitialImage = null;
            this.btnSave.Location = new System.Drawing.Point(186, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 60);
            this.btnSave.TabIndex = 18;
            this.btnSave.TabStop = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDel
            // 
            this.btnDel.Image = ((System.Drawing.Image)(resources.GetObject("btnDel.Image")));
            this.btnDel.InitialImage = null;
            this.btnDel.Location = new System.Drawing.Point(273, 20);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(60, 60);
            this.btnDel.TabIndex = 17;
            this.btnDel.TabStop = false;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(367, 68);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 21;
            this.label12.Text = "查询条件";
            // 
            // FrmCraftAndWorker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(942, 616);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCraftAndWorker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工艺工种工价信息维护";
            this.Load += new System.EventHandler(this.FrmCraftAndWorker_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDel)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private void FrmCraftAndWorker_Load(object sender, EventArgs e)
        {

            Bind();

        }
        #region Bind
        private void Bind()
        {
            btnSave.Enabled = false;
            this.dataGridView1.ReadOnly = true;
            dt = boperate.getdt(M_str_sql + "  order by CWID,date asc");
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


                if (i == 2 || i == 7)
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

                inputInfoSource3.Add(dr3["CRAFT"].ToString());


            }
            this.cmbCraft.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCraft.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbCraft.AutoCompleteCustomSource = inputInfoSource3;
            foreach (DataRow dr4 in dt4.Rows)
            {

                inputInfoSource4.Add(dr4["Worker"].ToString());
            }
            this.cmbWorker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbWorker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbWorker.AutoCompleteCustomSource = inputInfoSource4;

        }
        #endregion
   
        private void ClearText()
        {

            cmbCraft.Text = "";
            cmbWorker.Text = "";
            txtCWUnitPrice.Text = "";

        }
  
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
            if (M_int_judge == 0)
            {
                dt1 = boperate.getdt("select * from tb_CraftAndWorker where Craft='" + cmbCraft.Text +
                    "' and Worker='" + cmbWorker.Text + "'");
                if (dt1.Rows.Count > 0)
                {
                    MessageBox.Show("此工艺工种在系统已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);



                }
                else
                {
                    string var1 = dtx1.Rows[0]["CRID"].ToString();
                    string var2 = dtx2.Rows[0]["WKID"].ToString();
                    boperate.getcom(@"insert into tb_CraftAndWorker(CWID,CRID,CRAFT,WKID,WORKER,CWUnitPrice,Maker,Date,Year,Month,Day
             ) values('" + txtCraftAndWorkerID.Text +
                        "','" + var1 + "','" + cmbCraft.Text + "','" + var2 + "','" + cmbWorker.Text +
                        "', '" + txtCWUnitPrice.Text + "', '" + FrmLogin.M_str_name + "','" + varDate +
                        "','" + year + "','" + month + "','" + day + "')");
                    Bind();
                    ClearText();
                    txtCraftAndWorkerID.Text = "";
                }

            }
            else
            {
                dt1 = boperate.getdt("select * from tb_CraftAndWorker");
                if (dt1.Rows.Count > 0)
                {
                    boperate.getcom(@"update tb_CraftAndWorker set CWUnitPrice='" + txtCWUnitPrice.Text + "',Maker='" + FrmLogin.M_str_name +
                     "',Date='" + varDate + "' where CWID='" + Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim() + "'");
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
            txtCraftAndWorkerID.Text = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            cmbCraft.Text = Convert.ToString(dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            cmbWorker.Text = Convert.ToString(dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            txtCWUnitPrice.Text = Convert.ToString(dataGridView1[5, dataGridView1.CurrentCell.RowIndex].Value).Trim();
        }



        private void cmbCraft_DropDown(object sender, EventArgs e)
        {
            cmbCraft.DataSource = dt3;
            cmbCraft.DisplayMember = "Craft";
        }

        private void cmbWorker_DropDown(object sender, EventArgs e)
        {
            cmbWorker.DataSource = dt4;
            cmbWorker.DisplayMember = "Worker";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string a = boperate.numN(12, 4, "0001", "select * from tb_CraftAndWorker", "CWID", "CW");
            if (a == "Exceed limited")
            {
                MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                txtCraftAndWorkerID.Text = a;

            }
            cmbCraft.Enabled = true;
            cmbWorker.Enabled = true;
            btnSave.Enabled = true;
            M_int_judge = 0;
            ClearText();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            M_int_judge = 1;
            btnSave.Enabled = true;
            cmbCraft.Enabled = false;
            cmbWorker.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {


                if (cmbCraft.Text == "")
                {
                    MessageBox.Show("工艺不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (txtCraftAndWorkerID.Text == "")
                {
                    MessageBox.Show("工艺代码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (cmbWorker.Text == "")
                {
                    MessageBox.Show("工种不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (txtCWUnitPrice.Text == "")
                {
                    MessageBox.Show("基价不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                else
                {
                    if (yesno(txtCWUnitPrice.Text) == 0)
                    {
                        MessageBox.Show("基价只能输入数字！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        dtx1 = boperate.getdt("select * from tb_craft where craft='" + cmbCraft.Text + "'");
                        dtx2 = boperate.getdt("select * from tb_worker where worker='" + cmbWorker.Text + "'");
                        if (dtx1.Rows.Count > 0)
                        {
                            if (dtx2.Rows.Count > 0)
                            {
                                as1();
                            }
                            else
                            {


                                MessageBox.Show("此工种在系统中不存在！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            }

                        }
                        else
                        {

                            MessageBox.Show("此工艺在系统中不存在！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }

                    }


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
                    boperate.getcom("delete from tb_CraftAndWorker where CWID='" + Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim() + "'");
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
                if (txtKeyWord.Text == "")
                {

                }
                else
                {
                    DataSet myds = boperate.getds(M_str_sql + " where Craft like '%" + txtKeyWord.Text.Trim() + "%' order by CWID,Date asc", M_str_table);
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
