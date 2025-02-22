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
    public class CWORKORDER_PICKING
    {
        basec bc = new basec();
        #region nature
        private string _GETID;
        public  string GETID
        {
            set { _GETID =value ; }
            get { return _GETID; }

        }
        private string _WOID;
        public string WOID
        {
            set { _WOID = value; }
            get { return _WOID; }

        }
        private string _WAREID;
        public string WAREID
        {
            set { _WAREID = value; }
            get { return _WAREID; }

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
        private string _getsqlfi;
        public string getsqlfi
        {
            set { _getsqlfi = value; }
            get { return _getsqlfi; }

        }
        private string _MAKERID;
        public string MAKERID
        {
            set { _MAKERID = value; }
            get { return _MAKERID; }

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
        DataTable dt = new DataTable();
        CBOM cbom = new CBOM();
        #region sql
        string sql = @"
SELECT 
A.WPID AS WPID,
A.PICKING_DATE AS PICKING_DATE,
D.WOID AS WOID,
D.WAREID AS ID,
CASE WHEN D.WORKORDER_STATUS_MST='CLOSE' THEN '已结案'
WHEN D.WORKORDER_STATUS_MST='PROGRESS' THEN '部分入库'
WHEN D.WORKORDER_STATUS_MST='DELAY' THEN 'Delay'
WHEN D.WORKORDER_STATUS_MST='CANCEL' THEN '已作废'
ELSE 'Open'
END  AS WORKORDER_STATUS_MST,
F.CNAME AS CNAME,
E.WNAME AS WNAME,
E.CWAREID AS CWAREID,
E.CO_WAREID AS CO_WAREID,
A.PICKING_MAKERID AS PICKING_MAKERID,
A.WP_COUNT AS WP_COUNT,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.PICKING_MAKERID )  AS PICKING_MEMBER,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID ) AS MAKER,
A.DATE AS DATE
FROM WORKORDER_PICKING_MST A
LEFT JOIN WORKORDER_MST D ON A.WOID =D.WOID 
LEFT JOIN WareInfo E ON E.WareID =D.WareID 
LEFT JOIN CustomerInfo_MST F ON E.CUID =F.CUID



";
        string sqlo = @"
INSERT INTO WORKORDER_PICKING_DET
(
WPKEY,
WPID,
WOKEY,
WOID,
SN,
REMARK,
YEAR,
MONTH,
DAY
)
VALUES
(
@WPKEY,
@WPID,
@WOKEY,
@WOID,
@SN,
@REMARK,
@YEAR,
@MONTH,
@DAY

)
";
        string sqlt = @"
INSERT INTO WORKORDER_PICKING_MST
(
WPID,
WOID,
WP_COUNT,
PICKING_DATE,
PICKING_MAKERID,
DATE,
MAKERID,
YEAR,
MONTH,
DAY
)
VALUES
(
@WPID,
@WOID,
@WP_COUNT,
@PICKING_DATE,
@PICKING_MAKERID,
@DATE,
@MAKERID,
@YEAR,
@MONTH,
@DAY
)
";
        string sqlth = @"
UPDATE WORKORDER_PICKING_MST SET 
WOID=@WOID,
WP_COUNT=@WP_COUNT,
PICKING_DATE=@PICKING_DATE,
PICKING_MAKERID=@PICKING_MAKERID,
DATE=@DATE,
MAKERID=@MAKERID,
YEAR=@YEAR,
MONTH=@MONTH,
DAY=@DAY

";
        string sqlf = @"
INSERT INTO MATERE
(
MRKEY,
MATEREID,
SN,
WAREID,
MRCOUNT,
SKU,
STORAGEID,
SLID,
BATCHID,
DATE,
MAKERID,
YEAR,
MONTH,
DAY
)
VALUES
(
@MRKEY,
@MATEREID,
@SN,
@WAREID,
@MRCOUNT,
@SKU,
@STORAGEID,
@SLID,
@BATCHID,
@DATE,
@MAKERID,
@YEAR,
@MONTH,
@DAY
)
";
         string sqlfi = @"
SELECT 
A.WPKEY AS 索引,
A.WPID AS 领料单号,
A.WOID as 工单号, 
A.SN as 项次,
C.WareID as ID,
D.CO_WAREID AS 原物料或半成品编码,
D.WNAME AS 原物料类别或半成品,
D.SPEC as 规格,
D.BRAND AS 品牌,
D.CWAREID AS 客户料号或原厂料号,
C.MRCOUNT AS 领料数量,
D.SKU as 库存单位,
F.PICKING_DATE AS 领料日期,
F.PICKING_MAKERID AS 领料员工号,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=F.PICKING_MAKERID )  AS 领料员,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=F.MAKERID )  AS 制单人,
F.DATE AS 制单日期,
H.STORAGENAME AS 仓库,
I.STORAGE_LOCATION AS 库位,
C.BatchID AS 批号,
A.REMARK AS 备注
from WORKORDER_PICKING_DET A 
LEFT JOIN WORKORDER_DET B ON A.WOID=B.WOID AND A.SN=B.SN
LEFT JOIN MATERE C ON A.WPKEY=C.MRKEY
LEFT JOIN WAREINFO D ON C.WAREID=D.WAREID
LEFT JOIN CUSTOMERINFO_MST E ON D.CUID=E.CUID
LEFT JOIN WORKORDER_PICKING_MST F ON A.WPID=F.WPID
LEFT JOIN WORKORDER_MST G ON B.WOID =G.WOID 
LEFT JOIN STORAGEINFO H ON H.STORAGEID=C.STORAGEID
LEFT JOIN STORAGE_LOCATION I ON I.SLID=C.SLID
";
        #endregion
         public CWORKORDER_PICKING()
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            GETID =bc.numYM(10, 4, "0001", "SELECT * FROM WORKORDER_PICKING_MST", "WPID", "WP");
     
            getsql = sql;
            getsqlo = sqlo;
            getsqlt = sqlt;
            getsqlth = sqlth;
            getsqlf = sqlf;
            getsqlfi = sqlfi;
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
                    dr["BOM单位"] = dtx1.Rows[i]["BOM_UNIT"].ToString();
                    decimal d4 = decimal.Parse(dtx1.Rows[i]["MPA_TO_STOCK"].ToString());
                    decimal d5 = decimal.Parse(dtx1.Rows[i]["STOCK_TO_BOM"].ToString());
                    dr["工单包装领用量"] = Math.Ceiling(decimal.Parse(dtx1.Rows[i]["WO_DOSAGE"].ToString()) *
                        d4 / d5);
                    dr["领用单位"] = dtx1.Rows[i]["SKU"].ToString();
                    dr["库存单位"] = dtx1.Rows[i]["SKU"].ToString();
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

        #region  JUAGE_RESIDUE_PICKING_COUNT_IF_LESSTHAN_REPICING_COUNT
        public bool JUAGE_RESIDUE_PICKING_COUNT_IF_LESSTHAN_REPICING_COUNT(string WPID)
        {
            bool b = false;
            dt = bc.getdt(sqlfi+" WHERE A.WPID='"+WPID +"'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    WOID = dr["工单号"].ToString();
                    string SN = dr["项次"].ToString();
                    decimal d1 = decimal.Parse(dr["领料数量"].ToString());
                    decimal d = 0;
                    decimal d2 = 0;
                    DataView dv = new DataView(this.WORKORDER_WITHOUT_PICKING_COUNT());
                    dv.RowFilter = "工单号='" + WOID + "' AND 项次='" + SN + "'";
                    DataTable dtx = dv.ToTable();
                    if (dtx.Rows.Count > 0)
                    {

                        d = decimal.Parse(dtx.Rows[0]["累计领用量"].ToString());
                        d2 = decimal.Parse(dtx.Rows[0]["累计退料量"].ToString());
                        if (d - d1 < d2)
                        {
                            b = true;
                            ErrowInfo = "项次:" +SN+ " 累计领料量："+d.ToString ("#0.00")+
                                "与删除的领料数量："+d1.ToString ("#0.00")+"差值："+(d-d1).ToString ("#0.00")+
                                "小于该项次的累计退料量："+d2.ToString ("0.00")+"，不允许编辑或删除该单据";
                            break;
                        }
                    }
                }
            }
          return b;
        }
        #endregion
    }
}
