using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using XizheC;
using System.Collections.Generic;

namespace XizheC
{
    public class CWORKORDER
    {
        basec bc = new basec();
        CBOM cbom = new CBOM();
      
        #region nature
        private string _GETID;
        public  string GETID
        {
            set { _GETID =value ; }
            get { return _GETID; }

        }
        private string _getsql;
        public string getsql
        {
            set { _getsql = value; }
            get { return _getsql; }

        }
        private string _getsqlo;
        public string getsqlo
        {
            set { _getsqlo = value; }
            get { return _getsqlo; }

        }
        private string _getsqlt;
        public string getsqlt
        {
            set { _getsqlt = value; }
            get { return _getsqlt; }

        }
        private string _getsqlth;
        public string getsqlth
        {
            set { _getsqlth = value; }
            get { return _getsqlth; }

        }
        private string _getsqlf;
        public string getsqlf
        {
            set { _getsqlf = value; }
            get { return _getsqlf; }

        }
        private string _MAKERID;
        public string MAKERID
        {
            set { _MAKERID = value; }
            get { return _MAKERID; }

        }
        private string _CRID;
        public string CRID
        {
            set { _CRID = value; }
            get { return _CRID; }

        }
        private string _WO_COUNT;
        public string WO_COUNT
        {
            set { _WO_COUNT = value; }
            get { return _WO_COUNT; }

        }
        private string _TOTAL_WP_COUNT;
        public string TOTAL_WP_COUNT
        {
            set { _TOTAL_WP_COUNT = value; }
            get { return _TOTAL_WP_COUNT; }

        }
        private string _TOTAL_WR_COUNT;
        public string TOTAL_WR_COUNT
        {
            set { _TOTAL_WR_COUNT = value; }
            get { return _TOTAL_WR_COUNT; }

        }
        private string _TOTAL_WS_COUNT;
        public string TOTAL_WS_COUNT
        {
            set { _TOTAL_WS_COUNT = value; }
            get { return _TOTAL_WS_COUNT; }

        }
        private string _TOTAL_WM_COUNT;
        public string TOTAL_WM_COUNT
        {
            set { _TOTAL_WM_COUNT = value; }
            get { return _TOTAL_WM_COUNT; }

        }
        private string _TOTAL_MS_COUNT;
        public string TOTAL_MS_COUNT
        {
            set { _TOTAL_MS_COUNT = value; }
            get { return _TOTAL_MS_COUNT; }

        }
        private string _XID;
        public string XID
        {
            set { _XID = value; }
            get { return _XID; }

        }
        private string _SOURCE_STATUS;
        public string SOURCE_STATUS
        {
            set { _SOURCE_STATUS = value; }
            get { return _SOURCE_STATUS; }

        }
        private string _DELIVERY_DATE;
        public string DELIVERY_DATE
        {
            set { _DELIVERY_DATE = value; }
            get { return _DELIVERY_DATE; }

        }
        private string _GODE_NEED_DATE;
        public string GODE_NEED_DATE
        {
            set { _GODE_NEED_DATE = value; }
            get { return _GODE_NEED_DATE; }

        }
        private string _LAST_PICKING_DATE;
        public string LAST_PICKING_DATE
        {
            set { _LAST_PICKING_DATE = value; }
            get { return _LAST_PICKING_DATE; }

        }
        private string _COMPLETE_DATE;
        public string COMPLETE_DATE
        {
            set { _COMPLETE_DATE = value; }
            get { return _COMPLETE_DATE; }

        }
        private string _ADVICE_DELIVER_DATE;
        public string ADVICE_DELIVER_DATE
        {
            set { _ADVICE_DELIVER_DATE = value; }
            get { return _ADVICE_DELIVER_DATE; }

        }
        private string _BOID;
        public string BOID
        {
            set { _BOID = value; }
            get { return _BOID; }

        }
        private string _IFC_SUPPLY;
        public string IFC_SUPPLY
        {
            set { _IFC_SUPPLY = value; }
            get { return _IFC_SUPPLY; }

        }
        private string _PICKING_STAGE;
        public string PICKING_STAGE
        {
            set { _PICKING_STAGE = value; }
            get { return _PICKING_STAGE; }

        }
        private string _HAVE_NO_GODE_COUNT;
        public string HAVE_NO_GODE_COUNT
        {
            set { _HAVE_NO_GODE_COUNT = value; }
            get { return _HAVE_NO_GODE_COUNT; }

        }
        private string _ALLOW_GODE_COUNT;
        public string ALLOW_GODE_COUNT
        {
            set { _ALLOW_GODE_COUNT = value; }
            get { return _ALLOW_GODE_COUNT; }

        }
        private string _ErrowInfo;
        public string ErrowInfo
        {

            set { _ErrowInfo = value; }
            get { return _ErrowInfo; }

        }
        private string _WAREID;
        public string WAREID
        {

            set { _WAREID = value; }
            get { return _WAREID; }

        }
        private string _DET_WAREID;
        public string DET_WAREID
        {

            set { _DET_WAREID = value; }
            get { return _DET_WAREID; }

        }
        private string _BOM_WAREID;
        public string BOM_WAREID
        {

            set { _BOM_WAREID = value; }
            get { return _BOM_WAREID; }

        }
             
        private string _SN;
        public string SN
        {

            set { _SN = value; }
            get { return _SN; }

        }
        private string _UNIT_DOSAGE;
        public string UNIT_DOSAGE
        {

            set { _UNIT_DOSAGE = value; }
            get { return _UNIT_DOSAGE; }

        }
        private string _ATTRITION_DOSAGE;
        public string ATTRITION_DOSAGE
        {

            set { _ATTRITION_DOSAGE = value; }
            get { return _ATTRITION_DOSAGE; }

        }
        private string _WOID;
        public string WOID
        {

            set { _WOID = value; }
            get { return _WOID; }

        }
        private string _WO_DOSAGE;
        public string WO_DOSAGE
        {
            set { _WO_DOSAGE = value; }
            get { return _WO_DOSAGE; }

        }
        #endregion
        string KEY;
        int i,j;
        DataTable dtx2 = new DataTable();
        DataTable dt = new DataTable();
        #region sql
        string sql = @"
SELECT
A.WOID AS 工单号,
A.WAREID AS ID,
B.CO_WAREID AS 料号,
A.BOID AS BOM编号,
D.BOM_EDITION AS BOM版本,
B.CWAREID AS 客户料号,
B.WNAME AS 品名,
A.DATE AS 制单日期,
A.WO_COUNT AS 工单数量,
A.CRID AS 厂内订单号,
A.DELIVERY_DATE AS 交货日期,
A.GODE_NEED_DATE AS 入库需求日期,
A.LAST_PICKING_DATE AS 最晚下料日期,
A.COMPLETE_DATE AS 齐套日期,
A.ADVICE_DELIVERY_DATE AS 建议客户交期,
C.CUID AS 客户ID,
C.CUSTOMER_ID AS 客户代码,
C.CNAME AS 客户名称,
A.DATE AS 制单日期
FROM WORKORDER_MST A
LEFT JOIN WAREINFO B ON A.WAREID=B.WAREID
LEFT JOIN CUSTOMERINFO_MST C ON B.CUID=C.CUID
LEFT JOIN BOM_MST D ON A.BOID=D.BOID
";
         string sqlo = @"
INSERT INTO 
WORKORDER_DET
(
WOKEY,
WOID,
SN,
DET_WAREID,
BOM_WAREID,
UNIT_DOSAGE,
ATTRITION_DOSAGE,
WO_DOSAGE,
MPA_TO_STOCK,
SKU,
STOCK_TO_BOM,
BOM_UNIT,
IFC_SUPPLY,
PICKING_STAGE,
YEAR,
MONTH,
DAY
)  
VALUES 
(
@WOKEY,
@WOID,
@SN,
@DET_WAREID,
@BOM_WAREID,
@UNIT_DOSAGE,
@ATTRITION_DOSAGE,
@WO_DOSAGE,
@MPA_TO_STOCK,
@SKU,
@STOCK_TO_BOM,
@BOM_UNIT,
@IFC_SUPPLY,
@PICKING_STAGE,
@YEAR,
@MONTH,
@DAY

)";
         string sqlt = @"
INSERT INTO 
WORKORDER_MST
(
WOID,
CRID,
WAREID,
BOID,
WO_COUNT,
SOURCE_STATUS,
WORKORDER_STATUS_MST,
DELIVERY_DATE,
GODE_NEED_DATE,
LAST_PICKING_DATE,
COMPLETE_DATE,
ADVICE_DELIVERY_DATE,
SKU,
DATE,
MAKERID,
YEAR,
MONTH
)  
VALUES 
(
@WOID,
@CRID,
@WAREID,
@BOID,
@WO_COUNT,
@SOURCE_STATUS,
@WORKORDER_STATUS_MST,
@DELIVERY_DATE,
@GODE_NEED_DATE,
@LAST_PICKING_DATE,
@COMPLETE_DATE,
@ADVICE_DELIVERY_DATE,
@SKU,
@DATE,
@MAKERID,
@YEAR,
@MONTH

)";
         string sqlth = @"
UPDATE WORKORDER_MST SET
CRID=@CRID,
WAREID=@WAREID,
BOID=@BOID,
WO_COUNT=@WO_COUNT,
SOURCE_STATUS=@SOURCE_STATUS,
WORKORDER_STATUS_MST=@WORKORDER_STATUS_MST,
DELIVERY_DATE=@DELIVERY_DATE,
GODE_NEED_DATE=@GODE_NEED_DATE,
LAST_PICKING_DATE=@LAST_PICKING_DATE,
COMPLETE_DATE=@COMPLETE_DATE,
ADVICE_DELIVERY_DATE=@ADVICE_DELIVERY_DATE,
DATE=@DATE,
MAKERID=@MAKERID,
YEAR=@YEAR,
MONTH=@MONTH
";
         string sqlf = @"

";
        #endregion
         public CWORKORDER()
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            GETID =bc.numYM(10, 4, "0001", "SELECT * FROM WORKORDER_MST", "WOID", "WO");
     
            getsql = sql;
            getsqlo = sqlo;
            getsqlt = sqlt;
            getsqlth = sqlth;
            getsqlf = sqlf;
        }
        public void save()
        {

            SQlcommandE();
            SQlcommandEo(sql);
        }
        #region SQlcommandE
        public void  SQlcommandE()
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
      
          
                KEY = bc.numYMD(20, 12, "000000000001", "SELECT * FROM WORKORDER_DET", "WOKEY", "WO");
                SqlConnection sqlcon = bc.getcon();
                SqlCommand sqlcom = new SqlCommand(sqlo, sqlcon);
                sqlcom.Parameters.Add("@WOKEY", SqlDbType.VarChar, 20).Value = KEY;
                sqlcom.Parameters.Add("@WOID", SqlDbType.VarChar, 20).Value = WOID;
                sqlcom.Parameters.Add("@DET_WAREID", SqlDbType.VarChar, 20).Value =DET_WAREID ;
                sqlcom.Parameters.Add("@BOM_WAREID", SqlDbType.VarChar, 20).Value = BOM_WAREID;
                sqlcom.Parameters.Add("@SN", SqlDbType.VarChar, 20).Value = SN;
                sqlcom.Parameters.Add("@UNIT_DOSAGE", SqlDbType.VarChar, 20).Value = UNIT_DOSAGE;
                sqlcom.Parameters.Add("@ATTRITION_DOSAGE", SqlDbType.VarChar, 20).Value = ATTRITION_DOSAGE;
                sqlcom.Parameters.Add("@WO_DOSAGE", SqlDbType.VarChar, 20).Value = WO_DOSAGE;
                dt = bc.getdt("SELECT * FROM WAREINFO WHERE WAREID='" + DET_WAREID + "'");
                if (dt.Rows.Count > 0)
                {
                    sqlcom.Parameters.Add("@MPA_TO_STOCK", SqlDbType.VarChar, 20).Value = dt.Rows[0]["MPA_TO_STOCK"].ToString();
                    sqlcom.Parameters.Add("@SKU", SqlDbType.VarChar, 20).Value = dt.Rows[0]["SKU"].ToString();
                    sqlcom.Parameters.Add("@STOCK_TO_BOM", SqlDbType.VarChar, 20).Value = dt.Rows[0]["STOCK_TO_BOM"].ToString();
                    sqlcom.Parameters.Add("@BOM_UNIT", SqlDbType.VarChar, 20).Value = dt.Rows[0]["BOM_UNIT"].ToString();
                }
                sqlcom.Parameters.Add("@IFC_SUPPLY", SqlDbType.VarChar, 20).Value = IFC_SUPPLY;
                sqlcom.Parameters.Add("@PICKING_STAGE", SqlDbType.VarChar, 20).Value = PICKING_STAGE;

                sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
                sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = MAKERID;
                sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
                sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
                sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
                sqlcon.Open();
                sqlcom.ExecuteNonQuery();
                sqlcon.Close();

            
        }
        #endregion
        #region SQlcommandEo
        public  void SQlcommandEo(string sql)
        {
            
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            SqlConnection sqlcon = bc.getcon();
            SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
            string SKU= bc.getOnlyString("SELECT SKU FROM WAREINFO WHERE WAREID='"+WAREID +"'");
            sqlcom.Parameters.Add("@WOID", SqlDbType.VarChar, 20).Value = WOID;
            sqlcom.Parameters.Add("@WAREID", SqlDbType.VarChar, 20).Value = WAREID;
            sqlcom.Parameters.Add("@BOID", SqlDbType.VarChar, 20).Value = BOID;
            sqlcom.Parameters.Add("@WO_COUNT", SqlDbType.VarChar, 20).Value = WO_COUNT;
            sqlcom.Parameters.Add("@CRID", SqlDbType.VarChar, 20).Value = CRID;
            sqlcom.Parameters.Add("@SOURCE_STATUS", SqlDbType.VarChar, 20).Value = SOURCE_STATUS;
            sqlcom.Parameters.Add("@WORKORDER_STATUS_MST", SqlDbType.VarChar, 20).Value ="OPEN";
            sqlcom.Parameters.Add("@DELIVERY_DATE", SqlDbType.VarChar, 20).Value = DELIVERY_DATE;
            sqlcom.Parameters.Add("@GODE_NEED_DATE", SqlDbType.VarChar, 20).Value = GODE_NEED_DATE;
            sqlcom.Parameters.Add("@LAST_PICKING_DATE", SqlDbType.VarChar, 20).Value = LAST_PICKING_DATE;
            sqlcom.Parameters.Add("@COMPLETE_DATE", SqlDbType.VarChar, 20).Value = COMPLETE_DATE;
            sqlcom.Parameters.Add("@ADVICE_DELIVERY_DATE", SqlDbType.VarChar, 20).Value = ADVICE_DELIVER_DATE;
            sqlcom.Parameters.Add("@SKU", SqlDbType.VarChar, 20).Value = SKU;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = MAKERID;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcon.Open();
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        }
        #endregion
        #region JUAGE_ALLOW_PICKING_COUNT
        public bool JUAGE_ALLOW_PICKING_COUNT(string WOID, string WP_COUNT)
        {
            bool b = false;
            DataView dv = new DataView(ask());
            dv.RowFilter = "工单号='"+WOID +"'";
            DataTable dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {

                if (decimal.Parse(WP_COUNT) > decimal.Parse(dt.Rows[0]["可领料套数"].ToString()))
                {
                    b = true;
                    ErrowInfo = "领料套数不能大于可领料套数！(可领料套数=" + "工单数量：" + dt.Rows[0]["工单数量"].ToString() + 
                        " - 累计领料套数：" + dt.Rows[0]["累计领料套数"].ToString() + 
                        " + 累计退料套数：" + dt.Rows[0]["累计退料套数"].ToString() + ")";
                }
            }
            return b;
        }
        #endregion
        #region JUAGE_ALLOW_REPICKING_COUNT
        public bool JUAGE_ALLOW_REPICKING_COUNT(string WOID, string WR_COUNT)
        {
            bool b = false;
            DataView dv = new DataView(ask());
            dv.RowFilter = "工单号='" + WOID + "'";
            DataTable dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {

                if (decimal.Parse(WR_COUNT) > decimal.Parse(dt.Rows[0]["可退料套数"].ToString()))
                {
                    b = true;
                    ErrowInfo = "退料套数不能大于可退料套数！(可退料套数=" + 
                        "累计领料套数：" + dt.Rows[0]["累计领料套数"].ToString() + 
                        " - 累计退料套数：" + dt.Rows[0]["累计退料套数"].ToString() +
                         " - 累计报废套数：" + dt.Rows[0]["累计报废套数"].ToString() +
                        " - 累计工单入库数量：" + dt.Rows[0]["累计工单入库数量"].ToString() + ")";
                }
            }
            return b;
        }
        #endregion
        #region JUAGE_ALLOW_MATERIEL_SCRAP_COUNT
        public bool JUAGE_ALLOW_MATERIEL_SCRAP_COUNT(string WOID, string MS_COUNT)
        {
            bool b = false;
            DataView dv = new DataView(ask());
            dv.RowFilter = "工单号='" + WOID + "'";
            DataTable dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {

                if (decimal.Parse(MS_COUNT) > decimal.Parse(dt.Rows[0]["可报废套数"].ToString()))
                {
                    b = true;
                    ErrowInfo = "报废套数不能大于可报废套数！(可报废套数=" +
                          "累计领料套数：" + dt.Rows[0]["累计领料套数"].ToString() +
                        " - 累计退料套数：" + dt.Rows[0]["累计退料套数"].ToString() +
                         " - 累计报废套数：" + dt.Rows[0]["累计报废套数"].ToString() +
                        " - 累计工单入库数量：" + dt.Rows[0]["累计工单入库数量"].ToString() + ")";
                }
            }
            return b;
        }
        #endregion
        #region ask
        public  DataTable ask()
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add("工单号", typeof(string));
            dtt.Columns.Add("ID", typeof(string));
            dtt.Columns.Add("品名", typeof(string));
            dtt.Columns.Add("规格", typeof(string));
            dtt.Columns.Add("料号", typeof(string));
            dtt.Columns.Add("客户料号", typeof(string));
            dtt.Columns.Add("客户ID", typeof(string));
            dtt.Columns.Add("客户名称", typeof(string));
            dtt.Columns.Add("BOM编号", typeof(string));
            dtt.Columns.Add("BOM单位", typeof(string));
            dtt.Columns.Add("工单数量", typeof(decimal));
            dtt.Columns.Add("累计领料套数", typeof(string));
            dtt.Columns.Add("可领料套数", typeof(string));
            dtt.Columns.Add("累计退料套数", typeof(string));
            dtt.Columns.Add("可退料套数", typeof(string));
            dtt.Columns.Add("累计报废套数", typeof(string));
            dtt.Columns.Add("可报废套数", typeof(string));/*materiel*/
            dtt.Columns.Add("累计工单入库数量", typeof(decimal));
            dtt.Columns.Add("可入库数量", typeof(decimal));
            dtt.Columns.Add("累计工单报废数量", typeof(decimal));
            dtt.Columns.Add("可报废数量", typeof(decimal));/*product*/
            dtt.Columns.Add("未入库数量", typeof(decimal), "工单数量-累计工单入库数量-累计工单报废数量");
            //dtt.Columns.Add("TOTAL未入库数量", typeof(decimal), "SUM(未入库数量)");
            dtt.Columns.Add("领用单位", typeof(string));
            dtt.Columns.Add("库存单位", typeof(string));
            dtt.Columns.Add("BOM版本", typeof(string));
            dtt.Columns.Add("入库需求日期", typeof(string));
            dtt.Columns.Add("制单日期", typeof(string));
            dtt.Columns.Add("状态", typeof(string));
           
            DataTable dtx1 = bc.getdt("SELECT * FROM WORKORDER_MST");
            if (dtx1.Rows.Count > 0)
            {
                for (i = 0; i < dtx1.Rows.Count; i++)
                {
                    DataRow dr = dtt.NewRow();
                    dr["工单号"] = dtx1.Rows[i]["WOID"].ToString();
                    dr["ID"] = dtx1.Rows[i]["WAREID"].ToString();
                    dtx2 = bc.getdt("select * from wareinfo where wareid='" + dtx1.Rows[i]["WAREID"].ToString() + "'");
                    dr["料号"] = dtx2.Rows[0]["CO_WAREID"].ToString();
                    dr["客户料号"] = dtx2.Rows[0]["CWAREID"].ToString();
                    dr["品名"] = dtx2.Rows[0]["WNAME"].ToString();
                    dr["客户ID"] = dtx2.Rows[0]["CUID"].ToString();
                    dr["客户名称"] = bc.getOnlyString("SELECT CNAME FROM CUSTOMERINFO_MST WHERE CUID='"+
                        dtx2.Rows[0]["CUID"].ToString()+"'");
                    dr["规格"] = dtx2.Rows[0]["SPEC"].ToString();
                    dr["客户料号"] = dtx2.Rows[0]["CWAREID"].ToString();
                    dr["领用单位"] = dtx2.Rows[0]["SKU"].ToString();
                    dr["库存单位"] = dtx2.Rows[0]["SKU"].ToString();
                    dr["工单数量"] = dtx1.Rows[i]["WO_COUNT"].ToString();
                    dr["入库需求日期"] = dtx1.Rows[i]["GODE_NEED_DATE"].ToString();
                    if (dtx1.Rows[i]["WORKORDER_STATUS_MST"].ToString() == "OPEN")
                    {
                        dr["状态"] = "OPEN";
                    }
                    else if (dtx1.Rows[i]["WORKORDER_STATUS_MST"].ToString() == "PROGRESS")
                    {
                        dr["状态"] = "部分入库";
                    }
                    else if (dtx1.Rows[i]["WORKORDER_STATUS_MST"].ToString() == "DELAY")
                    {
                        dr["状态"] ="DELAY";
                    }
                    else if (dtx1.Rows[i]["WORKORDER_STATUS_MST"].ToString() == "CANCEL")
                    {
                        dr["状态"] = "已作废";
                    }
                    else
                    {
                        dr["状态"] = "已结案";
                    }
                    
                    dr["制单日期"] = dtx1.Rows[i]["DATE"].ToString();
                    dr["累计领料套数"] = 0;
                    dr["累计退料套数"] = 0;
                    dr["累计报废套数"] = 0;
                    dr["累计工单入库数量"] = 0;
                    dr["累计工单报废数量"] = 0;
                    dtt.Rows.Add(dr);
                }

            }

            DataTable dtx4 = bc.getdt(@"
SELECT
A.WOID AS WOID,
SUM(A.WP_COUNT) AS SUM_WP_COUNT
FROM WORKORDER_PICKING_MST A
GROUP BY A.WOID");
            if (dtx4.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dtx4.Rows)
                {
                    foreach (DataRow dr in dtt.Rows)
                    {
                        if (dr1["WOID"].ToString() == dr["工单号"].ToString())
                        {
                            dr["累计领料套数"] =dr1["SUM_WP_COUNT"].ToString();
                            break;
                        }
                    }
                }
            }
            DataTable dtx6 = bc.getdt(@"
SELECT
A.WOID AS WOID,
SUM(A.WR_COUNT) AS SUM_WR_COUNT
FROM WORKORDER_REPICKING_MST A
GROUP BY A.WOID");
            if (dtx6.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dtx6.Rows)
                {
                    foreach (DataRow dr in dtt.Rows)
                    {
                        if (dr1["WOID"].ToString() == dr["工单号"].ToString())
                        {
                            dr["累计退料套数"] = dr1["SUM_WR_COUNT"].ToString();
                            break;
                        }
                    }
                }
            }
            DataTable dtx8 = bc.getdt(@"
SELECT
A.WOID AS WOID,
SUM(A.MS_COUNT) AS SUM_MS_COUNT
FROM WORKORDER_MATERIEL_SCRAP_MST A
GROUP BY A.WOID");
            if (dtx8.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dtx8.Rows)
                {
                    foreach (DataRow dr in dtt.Rows)
                    {
                        if (dr1["WOID"].ToString() == dr["工单号"].ToString())
                        {
                            dr["累计报废套数"] = dr1["SUM_MS_COUNT"].ToString();
                            break;
                        }
                    }
                }
            }
            DataTable dtx5 = bc.getdt(@"
SELECT
A.WOID AS WOID,
SUM(B.GECOUNT) AS SUM_WM_COUNT
FROM WORKORDER_COMPLETION_DET A
LEFT JOIN GODE B ON A.WMKEY=B.GEKEY
GROUP BY A.WOID");
            if (dtx5.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dtx5.Rows)
                {
                    foreach (DataRow dr in dtt.Rows)
                    {
                        if (dr1["WOID"].ToString() == dr["工单号"].ToString())
                        {
                            dr["累计工单入库数量"] = dr1["SUM_WM_COUNT"].ToString();
                            break;
                        }
                    }
                }
               

            }
            DataTable dtx7 = bc.getdt(@"
SELECT
A.WOID AS WOID,
SUM(B.GECOUNT) AS SUM_WS_COUNT
FROM WORKORDER_SCRAP_DET A
LEFT JOIN GODE B ON A.WSKEY=B.GEKEY
GROUP BY A.WOID");
            if (dtx7.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dtx7.Rows)
                {
                    foreach (DataRow dr in dtt.Rows)
                    {
                        if (dr1["WOID"].ToString() == dr["工单号"].ToString())
                        {
                            dr["累计工单报废数量"] = dr1["SUM_WS_COUNT"].ToString();
                            break;
                        }
                    }
                }
            }
            foreach (DataRow dr in dtt.Rows)
            {
                decimal d0=0,d1=0,d2=0,d3=0,d4=0,d5=0;
                d0 = decimal.Parse(dr["工单数量"].ToString());
                d1 = decimal.Parse(dr["累计领料套数"].ToString());
                d2 = decimal.Parse(dr["累计退料套数"].ToString());
                d3 = decimal.Parse(dr["累计报废套数"].ToString());
                d4 = decimal.Parse(dr["累计工单入库数量"].ToString());
                d5 = decimal.Parse(dr["累计工单报废数量"].ToString());
                dr["可领料套数"] = d0 - d1 + d2;
                dr["可退料套数"] = d1 - d2 - d3 - d4 - d5;
                dr["可报废套数"] = d1 - d2 - d3 - d4 - d5;
                dr["可入库数量"] = d1 - d2 - d3 - d4 - d5;
                dr["可报废数量"] = d1 - d2 - d3 - d4 - d5;
            }
            return dtt;
        }
        #endregion
        #region GET_WORKORDER_PROGRESS_COUNT
        public string   GET_WORKORDER_PROGRESS_COUNT(string WAREID)
        {
            string v = "0";
            DataView dv = new DataView(ask());
            dv.RowFilter = "状态 NOT IN ('已结案','已作废') AND ID='"+WAREID +"'";
            DataTable dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {

               v = dt.Compute("SUM(未入库数量)", "").ToString();
                
            }
            return v;
        }
        #endregion
        #region GET_PROGRESS_COUNT_ONLY_ONE_WOID
        public string  GET_PROGRESS_COUNT_ONLY_ONE_WOID(string WOID)
        {
            string v = "0";
            DataView dv = new DataView(ask());
            dv.RowFilter = "工单号='" + WOID  + "'";
            DataTable dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {
                v = dt.Rows[0]["未入库数量"].ToString();
            }
            return v;
        }
        #endregion
        #region GET_PROGRESS_COUNT_WOID_LIST
        public List<CWORKORDER > GET_PROGRESS_COUNT_WOID_LIST(string WOID)
        {
            List<CWORKORDER> list = new List<CWORKORDER>();
            DataView dv = new DataView(ask());
            dv.RowFilter = "工单号='" + WOID + "'";
            DataTable dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {
                TOTAL_WP_COUNT = dt.Rows[0]["累计领料套数"].ToString();
                TOTAL_WR_COUNT = dt.Rows[0]["累计退料套数"].ToString();
                TOTAL_WS_COUNT = dt.Rows[0]["累计报废套数"].ToString();
                TOTAL_WM_COUNT = dt.Rows[0]["累计工单入库数量"].ToString();
                TOTAL_MS_COUNT = dt.Rows[0]["累计工单报废数量"].ToString();
                HAVE_NO_GODE_COUNT = dt.Rows[0]["未入库数量"].ToString();
                ALLOW_GODE_COUNT = dt.Rows[0]["可入库数量"].ToString();

            }
            return list;
        }
        #endregion

        #region GET_TOTAL_WORKORDER_GODE_COUNT
        public string GET_TOTAL_WORKORDER_GODE_COUNT(string WOID)
        {
            string v = "0";
            DataView dv = new DataView(ask());
            dv.RowFilter = "工单号='"+WOID +"'";
            DataTable dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {

                v = dt.Rows[0]["累计工单入库数量"].ToString();

            }
            return v;
        }
        #endregion

        #region dt_empty
        public DataTable dt_empty()
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add("工单号", typeof(string));
            dtt.Columns.Add("ID", typeof(string));
            dtt.Columns.Add("项次", typeof(string));
            dtt.Columns.Add("品名", typeof(string));
            dtt.Columns.Add("规格", typeof(string));
            dtt.Columns.Add("料号", typeof(string));
            dtt.Columns.Add("客户料号", typeof(string));
            dtt.Columns.Add("BOM编号", typeof(string));
            dtt.Columns.Add("子ID", typeof(string));
            dtt.Columns.Add("BOM_ID", typeof(string));
            dtt.Columns.Add("子料号", typeof(string));
            dtt.Columns.Add("子品名", typeof(string));
            dtt.Columns.Add("子客户料号", typeof(string));
            dtt.Columns.Add("子规格", typeof(string));
            dtt.Columns.Add("品牌", typeof(string));
            dtt.Columns.Add("生效否", typeof(string));
            dtt.Columns.Add("组成用量", typeof(string));
            dtt.Columns.Add("BOM单位", typeof(string));
            dtt.Columns.Add("损耗量", typeof(string));
            dtt.Columns.Add("需求量", typeof(string));
            dtt.Columns.Add("生产用量", typeof(string));
            dtt.Columns.Add("MPA_TO_STOCK", typeof(decimal));
            dtt.Columns.Add("MPA_TO_BOM", typeof(decimal));
            dtt.Columns.Add("领料单包装领用量", typeof(decimal));
            dtt.Columns.Add("工单包装领用量", typeof(decimal));
            dtt.Columns.Add("累计领用量", typeof(decimal));
            dtt.Columns.Add("累计退料量", typeof(decimal));
            dtt.Columns.Add("未领用量", typeof(decimal), "工单包装领用量-累计领用量+累计退料量");
            dtt.Columns.Add("领用单位", typeof(string));
            dtt.Columns.Add("库存数量", typeof(string));
            dtt.Columns.Add("仓库", typeof(string));
            dtt.Columns.Add("库位", typeof(string));
            dtt.Columns.Add("批号", typeof(string));
            dtt.Columns.Add("库存单位", typeof(string));
            dtt.Columns.Add("领用量", typeof(decimal));
            dtt.Columns.Add("本领料单累计领用量", typeof(decimal));
            dtt.Columns.Add("工单占用量", typeof(string));
            dtt.Columns.Add("采购在途量", typeof(string));
            dtt.Columns.Add("采购量", typeof(string));
            dtt.Columns.Add("客供否", typeof(string));
            dtt.Columns.Add("发料阶段", typeof(string));
            dtt.Columns.Add("BOM版本", typeof(string));
            dtt.Columns.Add("厂内订单号", typeof(string));
            dtt.Columns.Add("备注", typeof(string));
            dtt.Columns.Add("状态", typeof(string));
            return dtt;
        }
        #endregion

        #region WORKORDER_DET_TABLE
        public DataTable WORKORDER_DET_TABLE()
        {
            DataTable dtt = this.dt_empty();
            DataTable dtx1 = bc.getdt(@"SELECT * FROM WORKORDER_DET");
            if (dtx1.Rows.Count > 0)
            {
                for (i = 0; i < dtx1.Rows.Count; i++)
                {
                    DataRow dr = dtt.NewRow();
                    dr["工单号"] = dtx1.Rows[i]["WOID"].ToString();
                    dr["项次"] = dtx1.Rows[i]["SN"].ToString();
                    dr["ID"] = bc.getOnlyString("SELECT WAREID FROM WORKORDER_MST WHERE WOID='" + dtx1.Rows[i]["WOID"].ToString() + "'");
                    dr["子ID"] = dtx1.Rows[i]["DET_WAREID"].ToString();
                    dr["BOM_ID"] = dtx1.Rows[i]["BOM_WAREID"].ToString();
                    dtx2 = bc.getdt("select * from wareinfo where wareid='" + dtx1.Rows[i]["DET_WAREID"].ToString() + "'");
                    dr["子料号"] = dtx2.Rows[0]["CO_WAREID"].ToString();
                    dr["子品名"] = dtx2.Rows[0]["WNAME"].ToString();
                    dr["子客户料号"] = dtx2.Rows[0]["CWAREID"].ToString();
                    dr["子规格"] = dtx2.Rows[0]["SPEC"].ToString();
                    dr["品牌"] = dtx2.Rows[0]["BRAND"].ToString();
                    dr["BOM单位"] = dtx1.Rows[i]["BOM_UNIT"].ToString();
                    dr["组成用量"] = dtx1.Rows[i]["UNIT_DOSAGE"].ToString();
                    dr["损耗量"] = dtx1.Rows[i]["ATTRITION_DOSAGE"].ToString();
                    dr["需求量"] = decimal.Parse(dtx1.Rows[i]["UNIT_DOSAGE"].ToString()) + decimal.Parse(dtx1.Rows[i]["ATTRITION_DOSAGE"].ToString());
                    dr["生产用量"] = dtx1.Rows[i]["WO_DOSAGE"].ToString();
                    decimal d1 = decimal.Parse(dtx1.Rows[i]["MPA_TO_STOCK"].ToString());
                    decimal d2 = decimal.Parse(dtx1.Rows[i]["STOCK_TO_BOM"].ToString());
                    dr["MPA_TO_STOCK"] = dtx1.Rows[i]["MPA_TO_STOCK"].ToString();
                    dr["MPA_TO_BOM"] = dtx1.Rows[i]["STOCK_TO_BOM"].ToString();
                    dr["工单包装领用量"] = Math.Ceiling(decimal.Parse(dtx1.Rows[i]["WO_DOSAGE"].ToString()) *
                        d1/d2);
                    dr["领用单位"] = dtx1.Rows[i]["SKU"].ToString();
                    dr["库存单位"] = dtx1.Rows[i]["SKU"].ToString();
                    dr["累计领用量"] = 0;
                    dr["累计退料量"] = 0;
                    dr["状态"] = bc.getOnlyString("SELECT WORKORDER_STATUS_MST FROM WORKORDER_MST WHERE WOID='" + dtx1.Rows[i]["WOID"].ToString() + "'");
                    dr["客供否"] = dtx1.Rows[i]["IFC_SUPPLY"].ToString();
                    dr["发料阶段"] = dtx1.Rows[i]["PICKING_STAGE"].ToString();
                    dtt.Rows.Add(dr);
                }

            }

            DataTable dtx4 = bc.getdt(@"
SELECT
A.WOID AS WOID,
A.SN AS SN,
B.WAREID AS WAREID,
CAST(ROUND(SUM(B.MRCOUNT),2) AS DECIMAL(18,2)) AS MRCOUNT 
FROM WORKORDER_PICKING_DET A 
LEFT JOIN MATERE B ON A.WPKEY=B.MRKEY  
GROUP BY A.WOID,A.SN,B.WAREID");
            if (dtx4.Rows.Count > 0)
            {
                for (i = 0; i < dtx4.Rows.Count; i++)
                {
                    for (j = 0; j < dtt.Rows.Count; j++)
                    {
                        if (dtt.Rows[j]["工单号"].ToString() == dtx4.Rows[i]["WOID"].ToString() &&
                            dtt.Rows[j]["项次"].ToString() == dtx4.Rows[i]["SN"].ToString())
                        {
                            dtt.Rows[j]["累计领用量"] = dtx4.Rows[i]["MRCOUNT"].ToString();

                            break;
                        }

                    }
                }

            }

            DataTable dtx7 = bc.getdt(@"
SELECT
A.WOID AS WOID,
A.SN AS SN,
B.WAREID AS WAREID,
CAST(ROUND(SUM(B.GECOUNT),2) AS DECIMAL(18,2)) AS GECOUNT 
FROM WORKORDER_REPICKING_DET A 
LEFT JOIN GODE B ON A.WRKEY=B.GEKEY  
GROUP BY A.WOID,A.SN,B.WAREID");
            if (dtx7.Rows.Count > 0)
            {
                for (i = 0; i < dtx7.Rows.Count; i++)
                {
                    for (j = 0; j < dtt.Rows.Count; j++)
                    {
                        if (dtt.Rows[j]["工单号"].ToString() == dtx7.Rows[i]["WOID"].ToString() &&
                            dtt.Rows[j]["项次"].ToString() == dtx7.Rows[i]["SN"].ToString())
                        {
                            dtt.Rows[j]["累计退料量"] = dtx7.Rows[i]["GECOUNT"].ToString();

                            break;
                        }

                    }
                }

            }

            return dtt;
        }
        #endregion

        #region GET_WORKORDER_DET_DATA
        public DataTable  GET_WORKORDER_DET_DATA(string WOID)
        {
            
            DataView dv = new DataView(this.WORKORDER_DET_TABLE());
            dv.RowFilter = "工单号='" + WOID  + "'";
            DataTable dt = dv.ToTable();
            return dt;
        }
        #endregion

        #region GET_REALITY_PICKING_COUNT
        public decimal   GET_REALITY_PICKING_COUNT(string WOID)
        {
            decimal  d = 0;
            DataView dv = new DataView(this.ask ());
            dv.RowFilter = "工单号='" + WOID + "'";
            DataTable dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {
               d = decimal.Parse(dt.Rows[0]["累计领料套数"].ToString()) - decimal.Parse(dt.Rows[0]["累计退料套数"].ToString());
               
            }
            return d;
        }
        #endregion
        #region JUAGE_REALITY_PICKING_COUNT
        public bool  JUAGE_REALITY_PICKING_COUNT(string WOID)
        {
            bool b = false;
            decimal d = 0;
            DataView dv = new DataView(this.ask());
            dv.RowFilter = "工单号='" + WOID + "'";
            DataTable dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {
                d = decimal.Parse(dt.Rows[0]["累计领料套数"].ToString()) - decimal.Parse(dt.Rows[0]["累计退料套数"].ToString());
                if (d > 0)
                {
                    b = true;
                }
            }
            return b;
        }
        #endregion
        #region GET_REALITY_PICKING_DOSAGE
        public decimal  GET_REALITY_PICKING_DOSAGE(string WOID,string SN)
        {
            decimal d = 0;
            DataView dv = new DataView(this.WORKORDER_DET_TABLE());
            dv.RowFilter = "工单号='" + WOID + "' AND 项次='"+SN+"'";
            DataTable dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {
                d = decimal.Parse(dt.Rows[0]["累计领用量"].ToString()) - decimal.Parse(dt.Rows[0]["累计退料量"].ToString());
            }
            return d;
        }
        #endregion
        #region JUAGE_REALTY_IFHAVE_WORKORDER_GODE_COUNT
        public bool  JUAGE_REALTY_IFHAVE_WORKORDER_GODE_COUNT(string WOID)
        {
            bool b = false;
            DataView dv = new DataView(this.ask());
            dv.RowFilter = "工单号='" + WOID + "'";
            DataTable dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {
                decimal d = decimal.Parse(dt.Rows[0]["累计入库数量"].ToString());
                if (d > 0)
                {
                    b = true;
                }
            }
            return b;
        }
        #endregion

        #region UPDATE_WORKORDER_STATUS
        public void UPDATE_WORKORDER_STATUS(string WOID)
        {
            DataView dv = new DataView(ask());
            dv.RowFilter = "工单号='" + WOID + "'";
            DataTable dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {
                decimal d0 = decimal.Parse(dt.Rows[0]["工单数量"].ToString());
                decimal d1 = decimal.Parse(dt.Rows[0]["累计工单入库数量"].ToString());
                decimal d2 = decimal.Parse(dt.Rows[0]["累计工单报废数量"].ToString());

                if (decimal.Parse(dt.Rows[0]["未入库数量"].ToString()) == 0)
                {
                    basec.getcoms("UPDATE WORKORDER_MST SET WORKORDER_STATUS_MST='CLOSE' WHERE WOID='" + WOID + "' ");
                }
                else if (bc.JuageCurrentDateIFAboveDeliveryDate(DateTime.Now.ToString(), dt.Rows[0]["入库需求日期"].ToString()))
                {
                    basec.getcoms("UPDATE WORKORDER_MST SET WORKORDER_STATUS_MST='DELAY' WHERE WOID='" + WOID + "' ");
                }
                else if (d1 > 0)
                {
                    basec.getcoms("UPDATE WORKORDER_MST SET WORKORDER_STATUS_MST='PROGRESS' WHERE WOID='" + WOID + "'");
                }
                else
                {
                    basec.getcoms("UPDATE WORKORDER_MST SET WORKORDER_STATUS_MST='OPEN' WHERE WOID='" + WOID + "'");
                }
            }
        }
        #endregion
    }
}
