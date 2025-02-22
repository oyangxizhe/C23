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
    public class CWORKORDER_SCRAP
    {
        basec bc = new basec();
    
        #region nature
        public  string _GETID;
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

        private static bool _IFExecutionSUCCESS;
        public static bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        #endregion
        #region sql
        string sql = @"
SELECT 
A.WSID AS WSID,
A.SCRAP_DATE AS SCRAP_DATE,
A.WOID AS WOID,
D.WAREID AS ID,
D.CO_WAREID AS CO_WAREID,
E.CNAME  AS CNAME,
CASE WHEN F.WORKORDER_STATUS_MST='CLOSE' THEN '已结案'
WHEN F.WORKORDER_STATUS_MST='PROGRESS' THEN '部分入库'
WHEN F.WORKORDER_STATUS_MST='DELAY' THEN 'DELAY'
WHEN F.WORKORDER_STATUS_MST='CANCEL' THEN '已作废'
ELSE 'OPEN'
END  
AS WORKORDER_STATUS_MST,
E.CNAME AS CNAME,
D.WNAME AS WNAME,
D.CWAREID AS CWAREID,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.SCRAP_MAKERID )  AS GODER,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID ) AS MAKER,
A.DATE AS DATE
FROM WORKORDER_SCRAP_MST A
LEFT JOIN WORKORDER_MST F ON A.WOID =F.WOID 
LEFT JOIN WAREINFO D ON D.WAREID =F.WAREID 
LEFT JOIN CUSTOMERINFO_MST E ON D.CUID =E.CUID 

";

       
        string sqlo = @"
INSERT INTO 
WORKORDER_SCRAP_DET
(
WSKEY,
WSID,
WOID,
REMARK,
YEAR,
MONTH,
DAY
)
VALUES
(
@WSKEY,
@WSID,
@WOID,
@REMARK,
@YEAR,
@MONTH,
@DAY

)
";
        string sqlt = @"
INSERT INTO
WORKORDER_SCRAP_MST
(
WSID,
WOID,
SCRAP_DATE,
SCRAP_MAKERID,
DATE,
MAKERID,
YEAR,
MONTH,
DAY
)
VALUES
(
@WSID,
@WOID,
@SCRAP_DATE,
@SCRAP_MAKERID,
@DATE,
@MAKERID,
@YEAR,
@MONTH,
@DAY

)
";
        string sqlth = @"
UPDATE 
WORKORDER_SCRAP_MST 
SET 
WOID=@WOID,
SCRAP_DATE=@SCRAP_DATE,
SCRAP_MAKERID=@SCRAP_MAKERID,
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
A.WSKEY AS 索引,
A.WSID AS 报废单号,
A.WOID AS 工单号, 
C.WAREID AS ID,
D.CO_WAREID AS 厂内成品料号,
D.WNAME AS 品名,
D.SPEC AS 规格,
D.CWAREID AS 客户料号,
C.GECOUNT AS 报废数量,
D.SKU AS 库存单位,
F.SCRAP_DATE AS 报废日期,
F.SCRAP_MAKERID AS 报废员工号,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=F.SCRAP_MAKERID )  AS 报废员,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=F.MAKERID )  AS 制单人,
F.DATE AS 制单日期,
H.STORAGENAME AS 仓库,
I.STORAGE_LOCATION AS 库位,
C.BATCHID AS 批号,
A.REMARK AS 备注
FROM WORKORDER_SCRAP_DET A 
LEFT JOIN GODE C ON A.WSKEY=C.GEKEY
LEFT JOIN WAREINFO D ON C.WAREID=D.WAREID
LEFT JOIN CUSTOMERINFO_MST E ON D.CUID=E.CUID
LEFT JOIN WORKORDER_SCRAP_MST F ON A.WSID=F.WSID
LEFT JOIN WORKORDER_MST G ON A.WOID =G.WOID 
LEFT JOIN STORAGEINFO H ON H.STORAGEID=C.STORAGEID
LEFT JOIN STORAGE_LOCATION I ON I.SLID=C.SLID

";
        #endregion
         public CWORKORDER_SCRAP()
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            GETID =bc.numYM(10, 4, "0001", "SELECT * FROM WORKORDER_SCRAP_MST", "WSID", "WS");
     
            getsql = sql;
            getsqlo = sqlo;
            getsqlt = sqlt;
            getsqlth = sqlth;
            getsqlf = sqlf;
            getsqlfi = sqlfi;
        }
    }
}
