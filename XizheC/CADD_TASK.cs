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

namespace XizheC
{
    public class CADD_TASK
    {
        basec bc = new basec();
    
        #region nature
        public  string _GETID;
        public  string GETID
        {
            set { _GETID =value ; }
            get { return _GETID; }

        }
        private string _sql;
        public string sql
        {
            set { _sql = value; }
            get { return _sql; }

        }
        private string _sqlo;
        public string sqlo
        {
            set { _sqlo = value; }
            get { return _sqlo; }

        }
        private string _sqlt;
        public string sqlt
        {
            set { _sqlt = value; }
            get { return _sqlt; }

        }
        private string _sqlth;
        public string sqlth
        {
            set { _sqlth = value; }
            get { return _sqlth; }

        }
        private string _sqlf;
        public string sqlf
        {
            set { _sqlf = value; }
            get { return _sqlf; }

        }
        private string _sqlfi;
        public string sqlfi
        {
            set { _sqlfi = value; }
            get { return _sqlfi; }

        }
        private string _sqlsi;
        public string sqlsi
        {
            set { _sqlsi = value; }
            get { return _sqlsi; }

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
        private string _XID;
        public string XID
        {
            set { _XID = value; }
            get { return _XID; }

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
        private static bool _IFExecutionSUCCESS;
        public static bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        private string _ErrowInfo;
        public string ErrowInfo
        {

            set { _ErrowInfo = value; }
            get { return _ErrowInfo; }

        }
        private string _WP_COUNT;
        public string WP_COUNT
        {

            set { _WP_COUNT = value; }
            get { return _WP_COUNT; }

        }
        #endregion
 
        int i,j;
        DataTable dtx2 = new DataTable();
        CBOM cbom = new CBOM();
        #region sql
        string setsql = @"
SELECT  
A.ATID AS ATID ,
A.AT_NAME AS AT_NAME,
A.PARENT_NODEID AS PARENT_NODEID,
CASE WHEN PARENT_NODEID IS NOT NULL THEN (SELECT AT_NAME FROM ADD_TASK WHERE ATID=A.PARENT_NODEID )
ELSE NULL
END
AS PARENT_NAME,
A.URL AS URL,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID ) AS MAKER,
A.DATE AS DATE 
FROM ADD_TASK A
";
     
        string setsqlo = @"
)
";

        string setsqlt = @"
INSERT INTO ADD_TASK(
ATID,
AT_NAME,
PARENT_NODEID,
URL,
DATE,
MAKERID,
YEAR,
MONTH
)
VALUES
(
@ATID,
@AT_NAME,
@PARENT_NODEID,
@URL,
@DATE,
@MAKERID,
@YEAR,
@MONTH
)
";
        string setsqlth = @"
UPDATE ADD_TASK SET 
ATID=@ATID,
AT_NAME=@AT_NAME,
PARENT_NODEID=@PARENT_NODEID,
URL=@URL,
DATE=@DATE,
MAKERID=@MAKERID,
YEAR=@YEAR,
MONTH=@MONTH

";
           
        string setsqlf = @"

";
         string setsqlfi = @"

";
         string setsqlsi = @"

";
        #endregion
         public CADD_TASK()
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            GETID =bc.numYM(10, 4, "0001", "SELECT * FROM ADD_TASK", "ATID", "AT");
             sql= setsql;
            sqlo=setsqlo;
            sqlt=setsqlt;
            sqlth=setsqlth;
            sqlf= setsqlf;
            sqlfi=setsqlfi;
            sqlsi = setsqlsi;
        }
        #region ask
        public DataTable ask(string wpid)
        {
            string sql1 = sqlo;
            DataTable dtt = bc.getdt(sqlfi + " WHERE A.WPID='" + wpid  + "' ORDER BY A.WPKEY ASC");
            return dtt;
        }
        #endregion
        #region dt_empty
        public DataTable  dt_empty()
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
            dtt.Columns.Add("领料单包装领用量", typeof(decimal));
            dtt.Columns.Add("工单包装领用量", typeof(decimal));
            dtt.Columns.Add("累计领用量", typeof(decimal));
            dtt.Columns.Add("累计退料量", typeof(decimal));
            dtt.Columns.Add("未领用量", typeof(decimal),"工单包装领用量-累计领用量+累计退料量");
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
        #region WORKORDER_WITHOUT_PICKING_COUNT
        public  DataTable WORKORDER_WITHOUT_PICKING_COUNT()
        {
            DataTable dtt =this.dt_empty();
            DataTable dtx1 = bc.getdt(@"SELECT * FROM WORKORDER_DET");
            if (dtx1.Rows.Count > 0)
            {
                for (i = 0; i < dtx1.Rows.Count; i++)
                {
                    DataRow dr = dtt.NewRow();
                    dr["工单号"] = dtx1.Rows[i]["WOID"].ToString();
                    dr["项次"] = dtx1.Rows[i]["SN"].ToString();
                    dr["子ID"] = dtx1.Rows[i]["DET_WAREID"].ToString();
                    dtx2 = bc.getdt("select * from wareinfo where wareid='" + dtx1.Rows[i]["DET_WAREID"].ToString() + "'");
                    dr["子料号"] = dtx2.Rows[0]["CO_WAREID"].ToString();
                    dr["子品名"] = dtx2.Rows[0]["WNAME"].ToString();
                    dr["子客户料号"] = dtx2.Rows[0]["CWAREID"].ToString();
                    dr["子规格"] = dtx2.Rows[0]["SPEC"].ToString();
                    dr["品牌"] = dtx2.Rows[0]["BRAND"].ToString();
                    dr["BOM单位"] = dtx2.Rows[0]["BOM_UNIT"].ToString();
                    dr["工单包装领用量"] = Math.Ceiling(decimal.Parse(dtx1.Rows[i]["WO_DOSAGE"].ToString()) *
                        decimal.Parse(cbom.GETBOM_TO_STOCK(dtx1.Rows[i]["DET_WAREID"].ToString())));
                    dr["领用单位"] = dtx2.Rows[0]["SKU"].ToString();
                    dr["库存单位"] = dtx2.Rows[0]["SKU"].ToString();
                    dr["累计领用量"] = 0;
                    dr["累计退料量"] = 0;
                    dr["状态"] = bc.getOnlyString("SELECT WORKORDER_STATUS_MST FROM WORKORDER_MST WHERE WOID='"+dtx1.Rows [i]["WOID"].ToString ()+"'");
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
    }
}
