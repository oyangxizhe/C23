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
    public class CWORKORDER_REPICKING
    {
        basec bc = new basec();

        #region nature
        private string _ErrowInfo;
        public string ErrowInfo
        {

            set { _ErrowInfo = value; }
            get { return _ErrowInfo; }

        }
        public string _GETID;
        public string GETID
        {
            set { _GETID = value; }
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

        #endregion
        private static bool _IFExecutionSUCCESS;
        public static bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }

        #region sql
        string sql = @"
SELECT 
A.WRID AS WRID,
A.REPICKING_DATE AS REPICKING_DATE,
D.WOID AS WOID,
D.WAREID AS ID,
CASE WHEN D.WORKORDER_STATUS_MST='CLOSE' THEN '已结案'
WHEN D.WORKORDER_STATUS_MST='PROGRESS' THEN '部份入库'
WHEN D.WORKORDER_STATUS_MST='DELAY' THEN 'Delay'
WHEN D.WORKORDER_STATUS_MST='CANCEL' THEN '已作废'
ELSE 'Open'
END  AS WORKORDER_STATUS_MST,
F.CNAME AS CNAME,
E.WNAME AS WNAME,
E.CWAREID AS CWAREID,
E.CO_WAREID AS CO_WAREID,
A.REPICKING_MAKERID AS REPICKING_MAKERID,
A.WR_COUNT AS WR_COUNT,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.REPICKING_MAKERID )  AS REPICKING_MEMBER,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID ) AS MAKER,
A.DATE AS DATE
FROM WORKORDER_REPICKING_MST A
LEFT JOIN WORKORDER_MST D ON A.WOID =D.WOID 
LEFT JOIN WareInfo E ON E.WareID =D.WareID 
LEFT JOIN CustomerInfo_MST F ON E.CUID =F.CUID 


";
        string sqlo = @"
INSERT INTO 
WORKORDER_REPICKING_DET
(
WRKEY,
WRID,
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
@WRKEY,
@WRID,
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
INSERT INTO 
WORKORDER_REPICKING_MST
(
WRID,
WOID,
WR_COUNT,
REPICKING_DATE,
REPICKING_MAKERID,
DATE,
MAKERID,
YEAR,
MONTH,
DAY
)
VALUES
(
@WRID,
@WOID,
@WR_COUNT,
@REPICKING_DATE,
@REPICKING_MAKERID,
@DATE,
@MAKERID,
@YEAR,
@MONTH,
@DAY
)
";
        string sqlth = @"
UPDATE WORKORDER_REPICKING_MST SET 
WOID=@WOID,
WR_COUNT=@WR_COUNT,
REPICKING_DATE=@REPICKING_DATE,
REPICKING_MAKERID=@REPICKING_MAKERID,
DATE=@DATE,
MAKERID=@MAKERID,
YEAR=@YEAR,
MONTH=@MONTH,
DAY=@DAY

";
        string sqlf = @"
INSERT INTO GODE
(
GEKEY,
GODEID,
SN,
WAREID,
GECOUNT,
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
@GEKEY,
@GODEID,
@SN,
@WAREID,
@GECOUNT,
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
A.WRKEY AS 索引,
A.WRID AS 退料单号,
A.WOID AS 工单号, 
A.SN AS 项次,
C.WAREID AS ID,
D.CO_WAREID AS 原物料或半成品编码,
D.WNAME AS 原物料类别或半成品,
D.SPEC AS 规格,
D.BRAND AS 品牌,
D.CWAREID AS 客户料号或原厂料号,
C.GECOUNT AS 退料数量,
D.SKU AS 库存单位,
F.REPICKING_DATE AS 退料日期,
F.REPICKING_MAKERID AS 退料员工号,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=F.REPICKING_MAKERID )  AS 退料员,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=F.MAKERID )  AS 制单人,
F.DATE AS 制单日期,
H.STORAGENAME AS 仓库,
I.STORAGE_LOCATION AS 库位,
C.BATCHID AS 批号,
A.REMARK AS 备注
FROM WORKORDER_REPICKING_DET A 
LEFT JOIN WORKORDER_DET B ON A.WOID=B.WOID AND A.SN=B.SN
LEFT JOIN Gode  C ON A.WRKEY=C.GEKEY
LEFT JOIN WAREINFO D ON C.WAREID=D.WAREID
LEFT JOIN CUSTOMERINFO_MST E ON D.CUID=E.CUID
LEFT JOIN WORKORDER_REPICKING_MST F ON A.WRID=F.WRID
LEFT JOIN WORKORDER_MST G ON B.WOID =G.WOID 
LEFT JOIN STORAGEINFO H ON H.STORAGEID=C.STORAGEID
LEFT JOIN STORAGE_LOCATION I ON I.SLID=C.SLID

";
        #endregion
        public CWORKORDER_REPICKING()
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            GETID = bc.numYM(10, 4, "0001", "SELECT * FROM WORKORDER_REPICKING_MST", "WRID", "WR");

            getsql = sql;
            getsqlo = sqlo;
            getsqlt = sqlt;
            getsqlth = sqlth;
            getsqlf = sqlf;
            getsqlfi = sqlfi;
        }
        public void save(DataTable dt)
        {


        }
        #region ask
        public DataTable ask(string WRID)
        {
            string sql1 = sqlo;
            DataTable dtt = bc.getdt(sqlfi + " WHERE A.WRID='" + WRID + "' ORDER BY A.WRKEY ASC");
            return dtt;
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
            dtt.Columns.Add("工单包装领用量", typeof(string));
            dtt.Columns.Add("退料单包装退料量", typeof(decimal));
            dtt.Columns.Add("工单入库累计耗用量", typeof(decimal));
            dtt.Columns.Add("工单报废累计耗用量", typeof(decimal));
            dtt.Columns.Add("累计领料量", typeof(decimal));
            dtt.Columns.Add("累计退料量", typeof(decimal));
            dtt.Columns.Add("累计报废量", typeof(decimal));
            dtt.Columns.Add("可退料量", typeof(decimal));
            dtt.Columns.Add("退料单位", typeof(string));
            dtt.Columns.Add("仓库", typeof(string));
            dtt.Columns.Add("库位", typeof(string));
            dtt.Columns.Add("批号", typeof(string));
            dtt.Columns.Add("库存单位", typeof(string));
            dtt.Columns.Add("退料数量", typeof(decimal));
            dtt.Columns.Add("本退料单累计退料量", typeof(decimal));
            dtt.Columns.Add("工单占用量", typeof(string));
            dtt.Columns.Add("采购在途量", typeof(string));
            dtt.Columns.Add("采购量", typeof(string));
            dtt.Columns.Add("客供否", typeof(string));
            dtt.Columns.Add("发料阶段", typeof(string));
            dtt.Columns.Add("BOM版本", typeof(string));
            dtt.Columns.Add("厂内订单号", typeof(string));
            dtt.Columns.Add("备注", typeof(string));
            return dtt;
        }
        #endregion

    }
}
