using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
using XizheC;

namespace C23.BomManage
{
    public partial class frmWareInfo : Form
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();
        DataTable dt6 = new DataTable();
        DataTable dt7 = new DataTable();
        DataTable dta = new DataTable();
        DataTable dtx4 = new DataTable();
        string sql1 = "select type from tb_type ";
        string sql2 = "select ExternalM from tb_ExternalM ";
        string sql3 = "select Leather from tb_Leather";
        string sql4 = "select Color from tb_color";
        string sql5 = "select StitchingC from tb_StitchingC ";
        string sql6 = "select Detail from tb_detail ";
        string sql7 = "select Thickness from tb_Thickness ";
        C23.BaseClass.BaseOperate boperate = new C23.BaseClass.BaseOperate();
        C23.BaseClass.OperateAndValidate opAndvalidate = new C23.BaseClass.OperateAndValidate();
        protected string M_str_sql = @"select WareID as 品号,WName as 品名,ExternalM as 套件,Type as 型号,Detail as 细节,
Leather as 皮种,Color as 颜色,StitchingC as 线色,Thickness as 海棉厚度,
CASE WHEN 
ACTIVE='Y'
THEN '可用'
ELSE 
'不可用'
END  AS 可用否,Maker as 制单人,Date as 日期 from tb_WareInfo  ";
        protected string M_str_table = "tb_WareInfo";
        protected int M_int_judge, i;
        string a11;
        basec bc = new basec();
        protected int getdata;

        public frmWareInfo()
        {
            InitializeComponent();

        }
        private string _Type;
        public string Type
        {
            set { _Type = value; }
            get { return _Type; }

        }
        private string _ExternalM;
        public string ExternalM
        {
            set { _ExternalM = value; }
            get { return _ExternalM; }

        }
        private string _Leather;
        public string Leather
        {
            set { _Leather = value; }
            get { return _Leather; }

        }
        private string _PColor;
        public string PColor
        {
            set { _PColor = value; }
            get { return _PColor; }


        }
        private string _StitchingC;
        public string StitchingC
        {
            get { return _StitchingC; }
            set { _StitchingC = value; }


        }
        private string _Detail;
        public string Detail
        {
            set { _Detail = value; }
            get { return _Detail; }



        }
        private string _Thickness;
        public string Thickness
        {
            set { _Thickness = value; }
            get { return _Thickness; }

        }
        #region DoubleClick
        private void dgvWareInfo_DoubleClick(object sender, EventArgs e)
        {
            if (this.dgvWareInfo.ReadOnly == true) //判断如果是在进货单中生成的窗体则不响应DataGrid的双击事件
            {
                int intCurrentRowNumber = this.dgvWareInfo.CurrentCell.RowIndex;
                string sendWareID, sendWareName, sendWSpec, sendWUnit, v5, v6, v7, v8, v9;
                sendWareID = this.dgvWareInfo.Rows[intCurrentRowNumber].Cells[0].Value.ToString().Trim();
                sendWareName = this.dgvWareInfo.Rows[intCurrentRowNumber].Cells[1].Value.ToString().Trim();
                sendWSpec = this.dgvWareInfo.Rows[intCurrentRowNumber].Cells[2].Value.ToString().Trim();
                sendWUnit = this.dgvWareInfo.Rows[intCurrentRowNumber].Cells[3].Value.ToString().Trim();
                v5 = this.dgvWareInfo.Rows[intCurrentRowNumber].Cells[4].Value.ToString().Trim();
                v6 = this.dgvWareInfo.Rows[intCurrentRowNumber].Cells[5].Value.ToString().Trim();
                v7 = this.dgvWareInfo.Rows[intCurrentRowNumber].Cells[6].Value.ToString().Trim();
                v8 = this.dgvWareInfo.Rows[intCurrentRowNumber].Cells[7].Value.ToString().Trim();
                v9 = this.dgvWareInfo.Rows[intCurrentRowNumber].Cells[8].Value.ToString().Trim();
                string[] sendArray = new string[] { sendWareID, sendWareName, sendWSpec, sendWUnit, v5, v6, v7, v8, v9 };
                if (getdata == 0)
                {
                    C23.StockManage.frmStockTable.inputTextDataWare[0] = sendArray[0];
                    C23.StockManage.frmStockTable.inputTextDataWare[1] = sendArray[1];
                    C23.StockManage.frmStockTable.inputTextDataWare[2] = sendArray[2];
                    C23.StockManage.frmStockTable.inputTextDataWare[3] = sendArray[3];
                }
                if (getdata == 1)
                {
                    //C23.SellManage.FrmOrders.inputdgvWare[0] = sendArray[0];
                    //C23.SellManage.FrmOrders.inputdgvWare[1] = sendArray[1];
                    //C23.SellManage.FrmOrders.inputdgvWare[2] = sendArray[2];
                    //C23.SellManage.FrmOrders.inputdgvWare[3] = sendArray[3];

                }
                if (getdata == 2)
                {

                }
                if (getdata == 3)
                {
                    C23.PUR.frmPurOrders.inputTextDataWare[0] = sendArray[0];
                    C23.PUR.frmPurOrders.inputTextDataWare[1] = sendArray[1];
                    C23.PUR.frmPurOrders.inputTextDataWare[2] = sendArray[2];
                    C23.PUR.frmPurOrders.inputTextDataWare[3] = sendArray[3];
                }
                if (getdata == 4)
                {
                    C23.StockManage.frmReturn.inputTextDataWare[0] = sendArray[0];
                    C23.StockManage.frmReturn.inputTextDataWare[1] = sendArray[1];
                    C23.StockManage.frmReturn.inputTextDataWare[2] = sendArray[2];
                    C23.StockManage.frmReturn.inputTextDataWare[3] = sendArray[3];
                }
                if (getdata == 5)
                {
                    C23.SellManage.frmSellRe.inputTextDataWare[0] = sendArray[0];
                    C23.SellManage.frmSellRe.inputTextDataWare[1] = sendArray[1];
                    C23.SellManage.frmSellRe.inputTextDataWare[2] = sendArray[2];
                    C23.SellManage.frmSellRe.inputTextDataWare[3] = sendArray[3];
                }
                if (getdata == 6)
                {

                }
                if (getdata == 7)
                {
                    C23.StorageManage.FrmMiscMateReT.inputTextDataWare[0] = sendArray[0];
                    C23.StorageManage.FrmMiscMateReT.inputTextDataWare[1] = sendArray[1];
                    C23.StorageManage.FrmMiscMateReT.inputTextDataWare[2] = sendArray[2];
                    C23.StorageManage.FrmMiscMateReT.inputTextDataWare[3] = sendArray[3];
                }
                if (getdata == 8)
                {


                }
                if (getdata == 9)
                {
                    C23.StockManage.frmStockTableT.inputTextDataWare[0] = sendArray[0];
                    C23.StockManage.frmStockTableT.inputTextDataWare[1] = sendArray[1];
                    C23.StockManage.frmStockTableT.inputTextDataWare[2] = sendArray[2];
                    C23.StockManage.frmStockTableT.inputTextDataWare[3] = sendArray[3];

                }
                if (getdata == 10)
                {
                    C23.PUR.frmPurOrdersT.inputTextDataWare[0] = sendArray[0];
                    C23.PUR.frmPurOrdersT.inputTextDataWare[1] = sendArray[1];
                    C23.PUR.frmPurOrdersT.inputTextDataWare[2] = sendArray[2];
                    C23.PUR.frmPurOrdersT.inputTextDataWare[3] = sendArray[3];
                }
                if (getdata == 11)
                {
                    C23.StockManage.frmReturnT.inputTextDataWare[0] = sendArray[0];
                    C23.StockManage.frmReturnT.inputTextDataWare[1] = sendArray[1];
                    C23.StockManage.frmReturnT.inputTextDataWare[2] = sendArray[2];
                    C23.StockManage.frmReturnT.inputTextDataWare[3] = sendArray[3];
                }
                if (getdata == 12)
                {

                }
                if (getdata == 13)
                {
                    C23.SellManage.frmSellReT.inputTextDataWare[0] = sendArray[0];
                    C23.SellManage.frmSellReT.inputTextDataWare[1] = sendArray[1];
                    C23.SellManage.frmSellReT.inputTextDataWare[2] = sendArray[2];
                    C23.SellManage.frmSellReT.inputTextDataWare[3] = sendArray[3];
                }
                if (getdata == 14)
                {

                }
                if (getdata == 15)
                {
                    C23.StorageManage.FrmPWGodET.str4[0] = "doubleclick";
                    C23.StorageManage.FrmPWGodET.inputTextDataWare[0] = sendArray[0];
                    C23.StorageManage.FrmPWGodET.inputTextDataWare[1] = sendArray[1];
                    C23.StorageManage.FrmPWGodET.inputTextDataWare[2] = sendArray[2];
                    C23.StorageManage.FrmPWGodET.inputTextDataWare[3] = sendArray[3];
                    C23.StorageManage.FrmPWGodET.inputTextDataWare[4] = sendArray[4];
                    C23.StorageManage.FrmPWGodET.inputTextDataWare[5] = sendArray[5];
                    C23.StorageManage.FrmPWGodET.inputTextDataWare[6] = sendArray[6];
                    C23.StorageManage.FrmPWGodET.inputTextDataWare[7] = sendArray[7];
                    C23.StorageManage.FrmPWGodET.inputTextDataWare[8] = sendArray[8];

                }
                if (getdata == 16)
                {
                    C23.WorkOrderManage.FrmWorkOrderT.str4[0] = "doubleclick";
                    C23.WorkOrderManage.FrmWorkOrderT.inputTextDataWare[0] = sendArray[0];
                    C23.WorkOrderManage.FrmWorkOrderT.inputTextDataWare[1] = sendArray[1];
                    C23.WorkOrderManage.FrmWorkOrderT.inputTextDataWare[2] = sendArray[2];
                    C23.WorkOrderManage.FrmWorkOrderT.inputTextDataWare[3] = sendArray[3];
                    C23.WorkOrderManage.FrmWorkOrderT.inputTextDataWare[4] = sendArray[4];
                    C23.WorkOrderManage.FrmWorkOrderT.inputTextDataWare[5] = sendArray[5];
                    C23.WorkOrderManage.FrmWorkOrderT.inputTextDataWare[6] = sendArray[6];
                    C23.WorkOrderManage.FrmWorkOrderT.inputTextDataWare[7] = sendArray[7];
                    C23.WorkOrderManage.FrmWorkOrderT.inputTextDataWare[8] = sendArray[8];
                }
                if (getdata == 17)
                {
                    C23.WorkOrderManage.FrmWorkOrderT.str7[0] = "doubleclick";
                    C23.WorkOrderManage.FrmWorkOrderT.str8[0] = sendArray[0];
                    C23.WorkOrderManage.FrmWorkOrderT.str8[1] = sendArray[1];
                    C23.WorkOrderManage.FrmWorkOrderT.str8[2] = sendArray[2];
                    C23.WorkOrderManage.FrmWorkOrderT.str8[3] = sendArray[3];
                }
                if (getdata == 18)
                {
                    C23.StorageManage.FrmMiscGodET.str4[0] = "doubleclick";
                    C23.StorageManage.FrmMiscGodET.inputTextDataWare[0] = sendArray[0];
                    C23.StorageManage.FrmMiscGodET.inputTextDataWare[1] = sendArray[1];
                    C23.StorageManage.FrmMiscGodET.inputTextDataWare[2] = sendArray[2];
                    C23.StorageManage.FrmMiscGodET.inputTextDataWare[3] = sendArray[3];
                    C23.StorageManage.FrmMiscGodET.inputTextDataWare[4] = sendArray[4];
                    C23.StorageManage.FrmMiscGodET.inputTextDataWare[5] = sendArray[5];
                    C23.StorageManage.FrmMiscGodET.inputTextDataWare[6] = sendArray[6];
                    C23.StorageManage.FrmMiscGodET.inputTextDataWare[7] = sendArray[7];
                    C23.StorageManage.FrmMiscGodET.inputTextDataWare[8] = sendArray[8];


                }
                if (getdata == 19)
                {
                    C23.StorageManage.FrmMiscMateReT.str4[0] = "doubleclick";
                    C23.StorageManage.FrmMiscMateReT.inputTextDataWare[0] = sendArray[0];
                    C23.StorageManage.FrmMiscMateReT.inputTextDataWare[1] = sendArray[1];
                    C23.StorageManage.FrmMiscMateReT.inputTextDataWare[2] = sendArray[2];
                    C23.StorageManage.FrmMiscMateReT.inputTextDataWare[3] = sendArray[3];
                    C23.StorageManage.FrmMiscMateReT.inputTextDataWare[4] = sendArray[4];
                    C23.StorageManage.FrmMiscMateReT.inputTextDataWare[5] = sendArray[5];
                    C23.StorageManage.FrmMiscMateReT.inputTextDataWare[6] = sendArray[6];
                    C23.StorageManage.FrmMiscMateReT.inputTextDataWare[7] = sendArray[7];
                    C23.StorageManage.FrmMiscMateReT.inputTextDataWare[8] = sendArray[8];
                }
                if (getdata == 20)
                {
                    C23.SellManage.FrmOrderT.str4[0] = "doubleclick";
                    C23.SellManage.FrmOrderT.inputTextDataWare[0] = sendArray[0];
                    C23.SellManage.FrmOrderT.inputTextDataWare[1] = sendArray[1];
                    C23.SellManage.FrmOrderT.inputTextDataWare[2] = sendArray[2];
                    C23.SellManage.FrmOrderT.inputTextDataWare[3] = sendArray[3];
                    C23.SellManage.FrmOrderT.inputTextDataWare[4] = sendArray[4];
                    C23.SellManage.FrmOrderT.inputTextDataWare[5] = sendArray[5];
                    C23.SellManage.FrmOrderT.inputTextDataWare[6] = sendArray[6];
                    C23.SellManage.FrmOrderT.inputTextDataWare[7] = sendArray[7];
                    C23.SellManage.FrmOrderT.inputTextDataWare[8] = sendArray[8];
                }
                if (getdata == 21)
                {
                    C23.SellManage.FrmSellTableT.str4[0] = "doubleclick";
                    C23.SellManage.FrmSellTableT.inputTextDataWare[0] = sendArray[0];
                    C23.SellManage.FrmSellTableT.inputTextDataWare[1] = sendArray[1];
                    C23.SellManage.FrmSellTableT.inputTextDataWare[2] = sendArray[2];
                    C23.SellManage.FrmSellTableT.inputTextDataWare[3] = sendArray[3];
                    C23.SellManage.FrmSellTableT.inputTextDataWare[4] = sendArray[4];
                    C23.SellManage.FrmSellTableT.inputTextDataWare[5] = sendArray[5];
                    C23.SellManage.FrmSellTableT.inputTextDataWare[6] = sendArray[6];
                    C23.SellManage.FrmSellTableT.inputTextDataWare[7] = sendArray[7];
                    C23.SellManage.FrmSellTableT.inputTextDataWare[8] = sendArray[8];
                }
                this.Close();
            }
        }
        #endregion
        #region dgvReadOnly
        public void GodET()
        {
            this.dgvWareInfo.ReadOnly = true;
            getdata = 15;
        }
        public void a1()
        {
            this.dgvWareInfo.ReadOnly = true;
            getdata = 16;

        }
        public void a2()
        {
            this.dgvWareInfo.ReadOnly = true;
            getdata = 17;

        }
        public void a3()
        {
            this.dgvWareInfo.ReadOnly = true;
            getdata = 18;

        }
        public void a4()
        {
            this.dgvWareInfo.ReadOnly = true;
            getdata = 19;

        }
        public void a5()
        {
            this.dgvWareInfo.ReadOnly = true;
            getdata = 20;

        }
        public void a6()
        {
            this.dgvWareInfo.ReadOnly = true;
            getdata = 21;

        }
        #endregion
        private void frmWareInfo_Load(object sender, EventArgs e)
        {
            Bind();
        }

        #region Bind
        private void Bind()
        {
           
            this.dgvWareInfo.ReadOnly = true;
            dt = boperate.getdt(M_str_sql + "  WHERE ACTIVE='Y' order by wareid,date asc");
            dgvWareInfo.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                btnDel.Enabled = true;
            }
            else
            {
                btnDel.Enabled = false;
            }

            for (i = 0; i < dgvWareInfo.Columns.Count - 1; i++)
            {
                dgvWareInfo.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dgvStateControl();
            abc();

   
         

        }
        #endregion

        #region dgvStateControl
        public void dgvStateControl()
        {
            int i;
            int numCols = dgvWareInfo.Columns.Count;
            dgvWareInfo.RowHeadersDefaultCellStyle.BackColor = Color.Lavender;
            for (i = 0; i < numCols; i++)
            {

                dgvWareInfo.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvWareInfo.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (i == 1)
                {
                    dgvWareInfo.Columns[i].Width = 200;

                }
                else if (i == 0 || i == 9 || i==10)
                {
                    dgvWareInfo.Columns[i].Width = 80;

                }
                else if (i == 11)
                {
                    dgvWareInfo.Columns[i].Width = 120;

                }
                else
                {
                    dgvWareInfo.Columns[i].Width = 60;

                }
                dgvWareInfo.EnableHeadersVisualStyles = false;
                dgvWareInfo.Columns[i].HeaderCell.Style.BackColor = Color.Lavender;

            }

            for (i = 0; i < dgvWareInfo.Columns.Count; i++)
            {
                dgvWareInfo.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvWareInfo.Columns[i].DefaultCellStyle.BackColor = Color.OldLace;
                i = i + 1;
            }

        }
        #endregion
        #region abc
        private void abc()
        {
            dt1 = boperate.getdt(sql1);
            dt2 = boperate.getdt(sql2);
            dt3 = boperate.getdt(sql3);
            dt4 = boperate.getdt(sql4);
            dt5 = boperate.getdt(sql5);
            dt6 = boperate.getdt(sql6);
            dt7 = boperate.getdt(sql7);
            AutoCompleteStringCollection inputInfoSource = new AutoCompleteStringCollection();
            AutoCompleteStringCollection inputInfoSource4 = new AutoCompleteStringCollection();
            foreach (DataRow dr in dt1.Rows)
            {
                //cmbType.Items.Add(dr[0].ToString ());
                inputInfoSource.Add(dr[0].ToString());


            }
            this.cmbType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbType.AutoCompleteCustomSource = inputInfoSource;
            foreach (DataRow dr4 in dt4.Rows)
            {
                //cmbType.Items.Add(dr[0].ToString ());
                inputInfoSource4.Add(dr4[0].ToString());


            }
            this.cmbColor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbColor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbColor.AutoCompleteCustomSource = inputInfoSource4;

        }
        #endregion

        private void ClearText()
        {
            cmbMaterial.Text = "";
            txtWareID.Text = "";
            txtWName.Text = "";
            cmbType.Text = "";
            cmbExternalM.Text = "";
            cmbLeather.Text = "";
            cmbColor.Text = "";
            cmbStitchingC.Text = "";
            cmbDetail.Text = "";
            cmbThickness.Text = "";
        }


        #region save
  
        #endregion
        #region ac()
        private void ac()
        {
        
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
  
            string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
            string[] xw = new string[] { "原材料", "半成品", "成品" };
             if (boperate.juageValueLimits(xw, cmbMaterial.Text) == false)
            {
                MessageBox.Show("物料属性需为成品,原材料或半成品！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           else  if (ac1() == false)
            {



            }
           else  if (!boperate.exists("SELECT * FROM TB_WAREINFO WHERE WAREID='" + txtWareID.Text + "'"))
            {

                if (ab1())
                {
              
                MessageBox.Show("您输入的7个属性值及品名已经存在于系统中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    if (cmbMaterial.Text == "原材料")
                    {
                        string x1 = "select * from tb_wareinfo where substring(wareid,1,1)='5' AND YEAR='" + year +
                                "' AND  MONTH='" + month + "' AND DAY='" + day + "'";
                        at(x1, "5");
                    }
                    else if (cmbMaterial.Text == "半成品")
                    {
                        string x1 = "select * from tb_wareinfo where substring(wareid,1,1)='8' AND YEAR='" + year +
                                "' AND  MONTH='" + month + "' AND DAY='" + day + "'";
                        at(x1, "8");
                    }
                    else if (cmbMaterial.Text == "成品")
                    {
                        string x1 = "select * from tb_wareinfo where substring(wareid,1,1)='9' AND YEAR='" + year +
                                "' AND  MONTH='" + month + "' AND DAY='" + day + "'";
                        at(x1, "9");
                    }
                    Bind();
                }
            }
    
       


            
        }
        #endregion
        #region ac1()
        private bool ac1()
        {
            bool x1 = true;
            if (txtWName.Text == "")
            {
                x1 = false;
                MessageBox.Show("品名不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            else if (boperate.juageValueLimits("select ExternalM from tb_ExternalM", cmbExternalM.Text) == false)
            {
                x1 = false;
                MessageBox.Show("您输入的属性值 " + cmbExternalM.Text + " 不为空或不存在于系统中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (boperate.juageValueLimits("select Type from tb_Type", cmbType.Text) == false)
            {
                x1 = false;
                MessageBox.Show("您输入的属性值 " + cmbType.Text + " 不为空或不存在于系统中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (boperate.juageValueLimits("select Detail from tb_Detail", cmbDetail.Text) == false)
            {
                x1 = false;
                MessageBox.Show("您输入的属性值 " + cmbDetail.Text + " 不为空或不存在于系统中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (boperate.juageValueLimits("select Leather from tb_Leather", cmbLeather.Text) == false)
            {
                x1 = false;
                MessageBox.Show("您输入的属性值 " + cmbLeather.Text + " 不为空或不存在于系统中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (boperate.juageValueLimits("select Color from tb_Color", cmbColor.Text) == false)
            {
                x1 = false;
                MessageBox.Show("您输入的属性值 " + cmbColor.Text + " 不为空或不存在于系统中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (boperate.juageValueLimits("select StitchingC from tb_StitchingC", cmbStitchingC.Text) == false)
            {
                x1 = false;
                MessageBox.Show("您输入的属性值 " + cmbStitchingC.Text + " 不为空或不存在于系统中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (boperate.juageValueLimits("select Thickness from tb_Thickness", cmbThickness.Text) == false)
            {
                x1 = false;
                MessageBox.Show("您输入的属性值 " + cmbThickness.Text + "不为空或不存在于系统中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
       
            return x1;
        }
        #endregion

        #region at
        private void at(string x1, string x2)
        {

            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString();
             a11 = boperate.numN2(11, 4, "0001", x1, "wareid", x2);
                if (a11 == "Exceed Limited")
                {
                    MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    txtWareID.Text = a11;
                    boperate.getcom("insert into tb_WareInfo(WareID,WName,ExternalM,Type,Detail,Leather,"
                    + "Color,StitchingC,Thickness,ACTIVE,Maker,Date,Year,Month,Day) values('"
                    + txtWareID.Text + "','" + txtWName.Text.Trim() + "','" + cmbExternalM.Text + "','" + cmbType.Text + "','" + cmbDetail.Text +
                    "','" + cmbLeather.Text + "','" + cmbColor.Text + "','" + cmbStitchingC.Text + "','" + cmbThickness.Text +
                    "','Y','" + FrmLogin.M_str_name + "','" + varDate + "','" + year + "','" + month + "','" + day + "')");

                }

      
         
        }
        #endregion
        #region del

        #endregion
        #region look

        #endregion
        private bool juageWareidExists()
        {
            bool a = true;
            string v1 = txtWareID.Text;
            if (boperate.exists("SELECT * FROM TB_GODE WHERE WAREID='" + v1 + "'"))
            {

                a = false;
                MessageBox.Show("该品号在入库单中已经存在，不允许修改！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (boperate.exists("SELECT * FROM TB_ORDER WHERE WAREID='" + v1 + "'"))
            {

                a = false;
                MessageBox.Show("该品号在订单中已经存在，不允许修改！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            else if (boperate.exists("SELECT * FROM TB_outsourcingunitprice WHERE WAREID='" + v1 + "'"))
            {

                a = false;
                MessageBox.Show("该品号在委外加工单价信息中已经存在，不允许修改！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (boperate.exists("SELECT * FROM TB_workorder WHERE WAREID='" + v1 + "' OR DETWAREID='" + v1 + "'"))
            {

                a = false;
                MessageBox.Show("该品号在工单中已经存在，不允许修改！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            return a;



        }
        private void at()
        {
            Type = cmbType.Text;
            ExternalM = cmbExternalM.Text;
            Leather = cmbLeather.Text;
            PColor = cmbColor.Text;
            StitchingC = cmbStitchingC.Text;
            Detail = cmbDetail.Text;
            Thickness = cmbThickness.Text;


        }
        private bool ab1()
        {
            bool x1 = false;
            at();
            if (boperate.exists("SELECT * FROM TB_WAREINFO WHERE WNAME='"+txtWName .Text +"' AND ExternalM='" + cmbExternalM.Text +
               "' AND TYPE='" + cmbType.Text + "' AND  DETAIL='" + cmbDetail.Text + "' AND Leather='" + cmbLeather.Text +
               "' AND COLOR='" + cmbColor.Text + "' AND StitchingC='" + cmbStitchingC.Text + "' AND Thickness='" + cmbThickness.Text + "'"))
            {
                x1 = true;

            }
            return x1;


        }
  
        #region cellclick
        private void dgvWareInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //cmbMaterial.Enabled = false;
            txtWareID.Text = Convert.ToString(dgvWareInfo[0, dgvWareInfo.CurrentCell.RowIndex].Value).Trim();
       
            try
            {
                if (txtWareID.Text.Substring(0, 1) == "5")
                {

                    cmbMaterial.Text = "原材料";
                }
                else if (txtWareID.Text.Substring(0, 1) == "8")
                {

                    cmbMaterial.Text = "半成品";
                }
                else
                {

                    cmbMaterial.Text = "成品";
                }
                txtWName.Text = Convert.ToString(dgvWareInfo[1, dgvWareInfo.CurrentCell.RowIndex].Value).Trim();
                cmbExternalM.Text = Convert.ToString(dgvWareInfo[2, dgvWareInfo.CurrentCell.RowIndex].Value).Trim();
                cmbType.Text = Convert.ToString(dgvWareInfo[3, dgvWareInfo.CurrentCell.RowIndex].Value).Trim();
                cmbDetail.Text = Convert.ToString(dgvWareInfo[4, dgvWareInfo.CurrentCell.RowIndex].Value).Trim();
                cmbLeather.Text = Convert.ToString(dgvWareInfo[5, dgvWareInfo.CurrentCell.RowIndex].Value).Trim();
                cmbColor.Text = Convert.ToString(dgvWareInfo[6, dgvWareInfo.CurrentCell.RowIndex].Value).Trim();
                cmbStitchingC.Text = Convert.ToString(dgvWareInfo[7, dgvWareInfo.CurrentCell.RowIndex].Value).Trim();
                cmbThickness.Text = Convert.ToString(dgvWareInfo[8, dgvWareInfo.CurrentCell.RowIndex].Value).Trim();
                if (Convert.ToString(dgvWareInfo[9, dgvWareInfo.CurrentCell.RowIndex].Value).Trim() == "不可用")
                {
                    cmbACTIVE.Text = "N";
                }
                else
                {
                    cmbACTIVE.Text = "Y";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        #endregion
        private void cmbType_DropDown(object sender, EventArgs e)
        {
            cmbType.DataSource = dt1;
            cmbType.DisplayMember = "TYPE";
        }

        private void cmbExternalM_DropDown(object sender, EventArgs e)
        {
            cmbExternalM.DataSource = dt2;
            cmbExternalM.DisplayMember = "ExternalM";

        }

        private void cmbLeather_DropDown(object sender, EventArgs e)
        {
            cmbLeather.DataSource = dt3;
            cmbLeather.DisplayMember = "Leather";
        }

        private void cmbColor_DropDown(object sender, EventArgs e)
        {
            cmbColor.DataSource = dt4;
            cmbColor.DisplayMember = "Color";

        }

        private void cmbStitchingC_DropDown(object sender, EventArgs e)
        {
            cmbStitchingC.DataSource = dt5;
            cmbStitchingC.DisplayMember = "StitchingC";
        }

        private void cmbDetail_DropDown(object sender, EventArgs e)
        {
            cmbDetail.DataSource = dt6;
            cmbDetail.DisplayMember = "Detail";
        }

        private void cmbThickness_DropDown(object sender, EventArgs e)
        {
            cmbThickness.DataSource = dt7;
            cmbThickness.DisplayMember = "Thickness";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            at();
            MessageBox.Show(PColor);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            cmbMaterial.Enabled = true;
            cmbType.Enabled = true;
            cmbExternalM.Enabled = true;
            cmbLeather.Enabled = true;
            cmbStitchingC.Enabled = true;
            cmbDetail.Enabled = true;
            cmbThickness.Enabled = true;
            txtWName.Text = "";
            cmbACTIVE.Text = "";
            M_int_judge = 0;
            ClearText();
        }

 
        private void btnSave_Click(object sender, EventArgs e)
        {

          
            try
            {

                ac();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {

                string v1 = Convert.ToString(dgvWareInfo[0, dgvWareInfo.CurrentCell.RowIndex].Value).Trim();
                DataTable dty1 = boperate.getdt("select * from tb_GODE where WAREid='" + v1 + "'");
                DataTable dty2 = boperate.getdt("select * from tb_Workorder where WAREid='" + v1 + "'");
                DataTable dty3 = boperate.getdt("select * from tb_order where WAREid='" + v1 + "'");
                if (dty1.Rows.Count > 0)
                {
                    MessageBox.Show("该品号已经在入库单中使用了，不允许删除！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dty2.Rows.Count > 0)
                {
                    MessageBox.Show("该品号已经在工单中使用了，不允许删除！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dty3.Rows.Count > 0)
                {
                    MessageBox.Show("该品号已经在订单中使用了，不允许删除！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (MessageBox.Show("确定要删除该条品号信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        boperate.getcom("delete from tb_WareInfo where WareID='" + Convert.ToString(dgvWareInfo[0, dgvWareInfo.CurrentCell.RowIndex].Value).Trim() + "'");
                        frmWareInfo_Load(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           
            try
            {
                string sql = @" where WareID like '%" + txtWareID.Text + "%' AND WNAME LIKE '%" + txtWName.Text + "%' AND TYPE LIKE '%" + cmbType.Text +
             "%' AND EXTERNALM LIKE '%" + cmbExternalM.Text + "%' AND LEATHER LIKE '%" + cmbLeather.Text + "%' AND COLOR LIKE '%" + cmbColor.Text +
             "%' AND STITCHINGC LIKE '%" + cmbStitchingC.Text + "%' AND DETAIL LIKE '%" + cmbDetail.Text + "%' AND THICKNESS LIKE '%" + cmbThickness.Text +
             "%' AND ACTIVE LIKE '%" + cmbACTIVE.Text + "%' ";
              
                if (checkBox1.Checked)
                {
                    dt = bc.getdt(M_str_sql + sql);

                }
                else
                {
                    
                    dt = bc.getdt(M_str_sql + sql+" AND ACTIVE='Y'");

                }
                if (dt.Rows.Count > 0)
                {
                    dgvWareInfo.DataSource = dt;
                    dgvStateControl();
                }
                else
                {
                    MessageBox.Show("没有要查找的相关记录！");
                    dgvWareInfo.DataSource = null;
                }
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

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
