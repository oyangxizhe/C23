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
    public class CMISC_PICKING
    {
        basec bc = new basec();
        #region nature
        private string _GETID;
        public string GETID
        {
            set { _GETID = value; }
            get { return _GETID; }

        }
        private string _WAREID;
        public string WAREID
        {
            set { _WAREID = value; }
            get { return _WAREID; }

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
        DataTable dtx2 = new DataTable();
        DataTable dt = new DataTable();
        #region sql
        string setsql = @"
SELECT 
A.MPID AS MPID,
A.PICKING_DATE AS PICKING_DATE,
A.PICKING_MAKERID AS PICKING_MAKERID,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.PICKING_MAKERID )  AS PICKING_MEMBER,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID ) AS MAKER,
A.DATE AS DATE
FROM MISC_PICKING_MST A
";
        string setsqlo = @"
INSERT INTO MISC_PICKING_DET
(
MPKEY,
MPID,
SN,
REMARK,
YEAR,
MONTH,
DAY
)
VALUES
(
@MPKEY,
@MPID,
@SN,
@REMARK,
@YEAR,
@MONTH,
@DAY

)
";
        string setsqlt = @"
INSERT INTO MISC_PICKING_MST
(
MPID,
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
@MPID,
@PICKING_DATE,
@PICKING_MAKERID,
@DATE,
@MAKERID,
@YEAR,
@MONTH,
@DAY
)
";
        string setsqlth = @"
UPDATE MISC_PICKING_MST SET 
PICKING_DATE=@PICKING_DATE,
PICKING_MAKERID=@PICKING_MAKERID,
DATE=@DATE,
MAKERID=@MAKERID,
YEAR=@YEAR,
MONTH=@MONTH,
DAY=@DAY

";
        string setsqlf = @"
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
        string setsqlfi = @"
SELECT 
A.MPKEY AS 索引,
A.MPID AS 领料单号, 
A.SN AS 项次,
C.WAREID AS ID,
D.CO_WAREID AS 料号,
D.WNAME AS 品名,
D.SPEC AS 规格,
D.BRAND AS 品牌,
D.CWAREID AS 客户料号,
C.MRCOUNT AS 领料数量,
D.SKU AS 库存单位,
CASE WHEN E.CNAME IS NOT NULL THEN E.CNAME 
ELSE J.SNAME 
END 
AS 客户或供应商,
F.PICKING_DATE AS 领料日期,
F.PICKING_MAKERID AS 领料员工号,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=F.PICKING_MAKERID )  AS 领料员,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=F.MAKERID )  AS 制单人,
F.DATE AS 制单日期,
H.STORAGENAME AS 仓库,
I.STORAGE_LOCATION AS 库位,
C.BATCHID AS 批号,
A.REMARK AS 备注
FROM MISC_PICKING_DET A 
LEFT JOIN MATERE C ON A.MPKEY=C.MRKEY
LEFT JOIN WAREINFO D ON C.WAREID=D.WAREID
LEFT JOIN CUSTOMERINFO_MST E ON D.CUID=E.CUID
LEFT JOIN MISC_PICKING_MST F ON A.MPID=F.MPID
LEFT JOIN STORAGEINFO H ON H.STORAGEID=C.STORAGEID
LEFT JOIN STORAGE_LOCATION I ON I.SLID=C.SLID
LEFT JOIN SUPPLIERINFO_MST J ON D.CUID=J.SUID


";
        #endregion
        public CMISC_PICKING()
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            GETID = bc.numYM(10, 4, "0001", "SELECT * FROM MISC_PICKING_MST", "MPID", "MP");

            sql = setsql;
            sqlo = setsqlo;
            sqlt = setsqlt;
            sqlth = setsqlth;
            sqlf = setsqlf;
            sqlfi = setsqlfi;
        }
        #region ask
        public DataTable ask(string MPID)
        {
            string sql1 = sqlo;
            DataTable dtt = bc.getdt(sqlfi + " WHERE A.MPID='" + MPID + "' ORDER BY A.MPKEY ASC");
            return dtt;
        }
        #endregion
    }
}
