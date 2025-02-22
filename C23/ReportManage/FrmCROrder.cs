using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace C23.ReportManage
{
    public partial class FrmCROrder : Form
    {
        C23.BaseClass.BaseOperate boperate = new C23.BaseClass.BaseOperate();
        C23.BaseClass.OperateAndValidate opAndvalidate = new C23.BaseClass.OperateAndValidate();
        public static string[] Array = new string[] { "", "" };
        public FrmCROrder()
        {
            InitializeComponent();
        }
        public  void Bind()
        {

            SqlConnection sqlcon = boperate.getcon();
            sqlcon.Open();
            DataTable dt = boperate.PrintOrder(" WHERE ORID='" + Array[0] + "'");
            C23.ReportManage.CReportFile.CROrder oRpt = new C23.ReportManage.CReportFile.CROrder();
            string ul = "C:\\Program Files\\Xizhe\\进销存管理系统\\CROrder.rpt";
            oRpt.Load(ul);
            oRpt.SetDataSource(dt);
            crystalReportViewer1.ReportSource = oRpt;
      
        }
        private void FrmCRScanCode_Load(object sender, EventArgs e)
        {
            Bind();
            
        }
    }
}
