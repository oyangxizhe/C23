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
    public partial class FrmCraft : Form
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        C23.BaseClass.BaseOperate boperate = new C23.BaseClass.BaseOperate();
        C23.BaseClass.OperateAndValidate opAndvalidate = new C23.BaseClass.OperateAndValidate();
        protected string M_str_sql = "select CRID AS 工艺代码,Craft as 工艺,Maker as 制单人,Date as 日期 from tb_Craft";
        protected string M_str_table = "tb_Craft";
        protected int M_int_judge, i;
        protected int getdata;

        public FrmCraft()
        {
            InitializeComponent();
        }
        #region init
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCraft));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCraft = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCraftID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKeyWord = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.PictureBox();
            this.btnDel = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.PictureBox();
            this.btnEdit = new System.Windows.Forms.PictureBox();
            this.btnSave = new System.Windows.Forms.PictureBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.groupBox1.Controls.Add(this.txtCraft);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCraftID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(0, 127);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(943, 84);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "工艺种类信息";
            // 
            // txtCraft
            // 
            this.txtCraft.Location = new System.Drawing.Point(286, 21);
            this.txtCraft.Name = "txtCraft";
            this.txtCraft.Size = new System.Drawing.Size(192, 21);
            this.txtCraft.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(227, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "工艺种类";
            // 
            // txtCraftID
            // 
            this.txtCraftID.Location = new System.Drawing.Point(105, 21);
            this.txtCraftID.Name = "txtCraftID";
            this.txtCraftID.ReadOnly = true;
            this.txtCraftID.Size = new System.Drawing.Size(100, 21);
            this.txtCraftID.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "工艺代码";
            // 
            // txtKeyWord
            // 
            this.txtKeyWord.Location = new System.Drawing.Point(496, 61);
            this.txtKeyWord.Name = "txtKeyWord";
            this.txtKeyWord.Size = new System.Drawing.Size(100, 21);
            this.txtKeyWord.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(449, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 22;
            this.label4.Text = "工艺：";
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.InitialImage = null;
            this.btnSearch.Location = new System.Drawing.Point(757, 22);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(60, 60);
            this.btnSearch.TabIndex = 18;
            this.btnSearch.TabStop = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnDel
            // 
            this.btnDel.Image = ((System.Drawing.Image)(resources.GetObject("btnDel.Image")));
            this.btnDel.InitialImage = null;
            this.btnDel.Location = new System.Drawing.Point(273, 22);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(60, 60);
            this.btnDel.TabIndex = 17;
            this.btnDel.TabStop = false;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(367, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 21;
            this.label3.Text = "查询条件";
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.InitialImage = null;
            this.btnExit.Location = new System.Drawing.Point(843, 22);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(60, 60);
            this.btnExit.TabIndex = 19;
            this.btnExit.TabStop = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
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
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.btnExit);
            this.groupBox2.Controls.Add(this.btnEdit);
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.btnDel);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(936, 121);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "菜单栏";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(857, 97);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 29;
            this.label10.Text = "退出";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(771, 97);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 28;
            this.label9.Text = "搜索";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(287, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 27;
            this.label8.Text = "删除";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(200, 97);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 26;
            this.label7.Text = "保存";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(113, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 25;
            this.label6.Text = "修改";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 24;
            this.label5.Text = "新增";
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.InitialImage = null;
            this.btnAdd.Location = new System.Drawing.Point(12, 22);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 60);
            this.btnAdd.TabIndex = 16;
            this.btnAdd.TabStop = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.InitialImage = null;
            this.btnEdit.Location = new System.Drawing.Point(99, 22);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(60, 60);
            this.btnEdit.TabIndex = 17;
            this.btnEdit.TabStop = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.InitialImage = null;
            this.btnSave.Location = new System.Drawing.Point(186, 22);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 60);
            this.btnSave.TabIndex = 18;
            this.btnSave.TabStop = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            this.dataGridView1.TabIndex = 25;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // FrmCraft
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(942, 616);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCraft";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工艺种类信息维护";
            this.Load += new System.EventHandler(this.FrmCraft_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        public class MyButton : System.Windows.Forms.Button
        {
            public bool Selectable
            {
                get { return this.GetStyle(System.Windows.Forms.ControlStyles.FixedHeight); }
                set { this.SetStyle(System.Windows.Forms.ControlStyles.Selectable, value); }
            }
        }

        private void FrmCraft_Load(object sender, EventArgs e)
        {

            Bind();
          
          
        }
        #region Bind
        private void Bind()
        {
            btnSave.Enabled = false;
            this.dataGridView1.ReadOnly = true;
            dt = boperate.getdt(M_str_sql + "  order by crid,date asc");
            dataGridView1.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                btnDel.Enabled = true;
            }
            else
            {
                btnDel.Enabled = false;
            }

            for (i = 0; i < dataGridView1.Columns.Count - 1; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dgvStateControl();
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

                if (i == 0 || i == 2)
                {
                    dataGridView1.Columns[i].Width = 90;

                }
                else
                {
                    dataGridView1.Columns[i].Width = 120;

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

        private void ClearText()
        {
            txtCraft.Text = "";


        }
 
        public void a1()
        {
            this.dataGridView1.ReadOnly = true;
            getdata = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string a1 = boperate.numN(12, 4, "0001", "select * from tb_Craft", "CRID", "CR");
            if (a1 == "Exceed Limited")
            {
                MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                txtCraftID.Text = a1;

            }
            btnSave.Enabled = true;
            txtCraft.Text = "";
            M_int_judge = 0;
            ClearText();
        }

 

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string varDate = DateTime.Now.ToString();
                string year, month, day;
                year = DateTime.Now.ToString("yy");
                month = DateTime.Now.ToString("MM");
                day = DateTime.Now.ToString("dd");
                if (txtCraft.Text == "")
                {
                    MessageBox.Show("工艺种类不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (txtCraftID.Text == "")
                {
                    MessageBox.Show("工艺代码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    if (M_int_judge == 0)
                    {
                        dt1 = boperate.getdt("select Craft from tb_Craft where Craft='" + txtCraft.Text + "'");
                        if (dt1.Rows.Count > 0)
                        {
                            MessageBox.Show("工艺种类已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);



                        }
                        else
                        {
                            boperate.getcom("insert into tb_Craft(CRID,Craft,Maker,Date,Year,Month,Day) values('" + txtCraftID.Text + "','"
                                               + txtCraft.Text.Trim() + "','" + FrmLogin.M_str_name + "','" + varDate + "','" + year +
                                               "','" + month + "','" + day + "')");
                            Bind();

                        }

                    }
                    else
                    {
                        dt2 = boperate.getdt("select Craft from tb_Craft");
                        if (dt2.Rows.Count > 0)
                        {
                            boperate.getcom(@"update tb_Craft set Craft='" + txtCraft.Text + "',Maker='" + FrmLogin.M_str_name +
                             "',Date='" + varDate + "' where Craft='" + Convert.ToString(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value).Trim() + "'");
                            Bind();
                        }
                        else
                        {

                            MessageBox.Show("无数据可以更新！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    boperate.getcom("delete from tb_Craft where Craft='" + Convert.ToString(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value).Trim() + "'");
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
                    DataSet myds = boperate.getds(M_str_sql + " where Craft like '%" + txtKeyWord.Text.Trim() + "%' order by Date asc", M_str_table);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCraftID.Text = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            txtCraft.Text = Convert.ToString(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value).Trim();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.dataGridView1.ReadOnly == true)
            {
                int intCurrentRowNumber = this.dataGridView1.CurrentCell.RowIndex;
                string v1;
                v1 = this.dataGridView1.Rows[intCurrentRowNumber].Cells[1].Value.ToString().Trim();
                string[] sendArray = new string[] { v1 };
                if (getdata == 0)
                {
                    C23.StorageManage.FrmPWGodET.str3[0] = "doubleclick";
                    C23.StorageManage.FrmPWGodET.str2[0] = sendArray[0];
                }
                this.Close();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            M_int_judge = 1;
            btnSave.Enabled = true;
        }

 

    

    }
}
