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
    public class CMISC_GODE
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
        private string _SKU;
        public string SKU
        {
            set { _SKU = value; }
            get { return _SKU; }

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

        #region sql
        string setsql = @"
SELECT 
A.MGID AS MGID,
A.GODE_DATE AS GODE_DATE,
A.GODE_MAKERID AS GODE_MAKERID,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.GODE_MAKERID )  AS GODE_MEMBER,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID ) AS MAKER,
A.DATE AS DATE
FROM MISC_GODE_MST A
";
        string setsqlo = @"
INSERT INTO 
MISC_GODE_DET
(
MGKEY,
MGID,
SN,
REMARK,
YEAR,
MONTH,
DAY
)
VALUES
(
@MGKEY,
@MGID,
@SN,
@REMARK,
@YEAR,
@MONTH,
@DAY

)
";
        string setsqlt = @"
INSERT INTO 
MISC_GODE_MST
(
MGID,
GODE_DATE,
GODE_MAKERID,
DATE,
MAKERID,
YEAR,
MONTH,
DAY
)
VALUES
(
@MGID,
@GODE_DATE,
@GODE_MAKERID,
@DATE,
@MAKERID,
@YEAR,
@MONTH,
@DAY
)
";
        string setsqlth = @"
UPDATE MISC_GODE_MST SET 
GODE_DATE=@GODE_DATE,
GODE_MAKERID=@GODE_MAKERID,
DATE=@DATE,
MAKERID=@MAKERID,
YEAR=@YEAR,
MONTH=@MONTH,
DAY=@DAY

";
        string setsqlf = @"
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
        string setsqlfi = @"
SELECT 
A.MGKEY AS 索引,
A.MGID AS 入库单号, 
A.SN AS 项次,
C.WAREID AS ID,
D.CO_WAREID AS 料号,
D.WNAME AS 品名,
D.SPEC AS 规格,
D.BRAND AS 品牌,
D.CWAREID AS 客户料号,
C.GECOUNT AS 入库数量,
D.SKU AS 库存单位,
CASE WHEN E.CNAME IS NOT NULL THEN E.CNAME 
ELSE J.SNAME 
END 
AS 客户或供应商,
F.GODE_DATE AS 入库日期,
F.GODE_MAKERID AS 入库员工号,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=F.GODE_MAKERID )  AS 入库员,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=F.MAKERID )  AS 制单人,
F.DATE AS 制单日期,
H.STORAGENAME AS 仓库,
I.STORAGE_LOCATION AS 库位,
C.BATCHID AS 批号,
A.REMARK AS 备注
FROM MISC_GODE_DET A 
LEFT JOIN Gode  C ON A.MGKEY=C.GEKEY
LEFT JOIN WAREINFO D ON C.WAREID=D.WAREID
LEFT JOIN CUSTOMERINFO_MST E ON D.CUID=E.CUID
LEFT JOIN MISC_GODE_MST F ON A.MGID=F.MGID
LEFT JOIN STORAGEINFO H ON H.STORAGEID=C.STORAGEID
LEFT JOIN STORAGE_LOCATION I ON I.SLID=C.SLID
LEFT JOIN SUPPLIERINFO_MST J ON D.CUID=J.SUID
";
        #endregion
        public CMISC_GODE()
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            GETID = bc.numYM(10, 4, "0001", "SELECT * FROM MISC_GODE_MST", "MGID", "MG");
            sql = setsql;
            sqlo = setsqlo;
            sqlt = setsqlt;
            sqlth = setsqlth;
            sqlf = setsqlf;
            sqlfi = setsqlfi;
        }
        #region ask
        public DataTable ask(string MGID)
        {
            string sql1 = sqlo;
            DataTable dtt = bc.getdt(sqlfi + " WHERE A.MGID='" + MGID + "' ORDER BY A.MGKEY ASC");
            return dtt;
        }
        #endregion
        #region  JUAGE_CURRENT_STORAGECOUNT_IF_LESSTHAN_DELETE_COUNT
        public bool JUAGE_CURRENT_STORAGECOUNT_IF_LESSTHAN_DELETE_COUNT(string MGID)
        {
            bool b = false;
            DataTable dt = bc.getdt(sqlfi  + " WHERE A.MGID='" + MGID + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    WAREID  = dr["ID"].ToString();
                    SKU = dr["库存单位"].ToString();
                    decimal d= decimal.Parse(dr["入库数量"].ToString());
                    decimal d1 = 0;
                    DataView dv = new DataView(bc.getstoragecount_MRP ());
                    dv.RowFilter = "品号='" + WAREID  + "' AND 库存单位='" + SKU + "'";
                    DataTable dtx = dv.ToTable();
                    if (dtx.Rows.Count > 0)
                    {
                        d1 = decimal.Parse(dtx.Rows[0]["库存数量"].ToString());
                        if (d1 < d)
                        {
                            b = true;
                            ErrowInfo = "品号ID："+WAREID +" 库存数量：" + d1.ToString("#0.00") 
                                +"小于该品号要删除的入库数量：" + d.ToString("0.00") + "，不允许编辑或删除该单据";
                            break;
                        }
                    }
                    else
                    {

                        b = true;
                        ErrowInfo = "品号："+WAREID +" 库存数量为0："+"不允许编辑或删除该单据";
                        break;
                    }
                }
            }
            return b;
        }
        #endregion

    }
}
