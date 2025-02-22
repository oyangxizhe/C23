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
    public class CWORKORDER_COMPLETION
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
        #endregion
        private static bool _IFExecutionSUCCESS;
        public static bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        string KEY;
        #region sql
        string sql = @"
SELECT 
A.WMID AS WMID,
A.COMPLETION_DATE AS COMPLETION_DATE,
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
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.COMPLETION_MAKERID )  AS GODER,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID ) AS MAKER,
A.DATE AS DATE
FROM WORKORDER_COMPLETION_MST A
LEFT JOIN WORKORDER_MST F ON A.WOID =F.WOID 
LEFT JOIN WAREINFO D ON D.WAREID =F.WAREID 
LEFT JOIN CUSTOMERINFO_MST E ON D.CUID =E.CUID 

";

       
        string sqlo = @"
INSERT INTO 
WORKORDER_COMPLETION_DET
(
WMKEY,
WMID,
WOID,
REMARK,
YEAR,
MONTH,
DAY
)
VALUES
(
@WMKEY,
@WMID,
@WOID,
@REMARK,
@YEAR,
@MONTH,
@DAY

)
";
        string sqlt = @"
INSERT INTO
WORKORDER_COMPLETION_MST
(
WMID,
WOID,
COMPLETION_DATE,
COMPLETION_MAKERID,
DATE,
MAKERID,
YEAR,
MONTH,
DAY
)
VALUES
(
@WMID,
@WOID,
@COMPLETION_DATE,
@COMPLETION_MAKERID,
@DATE,
@MAKERID,
@YEAR,
@MONTH,
@DAY

)
";
        string sqlth = @"
UPDATE 
WORKORDER_COMPLETION_MST 
SET 
WOID=@WOID,
COMPLETION_DATE=@COMPLETION_DATE,
COMPLETION_MAKERID=@COMPLETION_MAKERID,
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
A.WMKEY AS 索引,
A.WMID AS 入库单号,
A.WOID AS 工单号, 
C.WAREID AS ID,
D.CO_WAREID AS 厂内成品料号,
D.WNAME AS 品名,
D.SPEC AS 规格,
D.CWAREID AS 客户料号,
C.GECOUNT AS 入库数量,
D.SKU AS 库存单位,
F.COMPLETION_DATE AS 入库日期,
F.COMPLETION_MAKERID AS 入库员工号,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=F.COMPLETION_MAKERID )  AS 入库员,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=F.MAKERID )  AS 制单人,
F.DATE AS 制单日期,
H.STORAGENAME AS 仓库,
I.STORAGE_LOCATION AS 库位,
C.BATCHID AS 批号,
A.REMARK AS 备注
FROM WORKORDER_COMPLETION_DET A 
LEFT JOIN GODE C ON A.WMKEY=C.GEKEY
LEFT JOIN WAREINFO D ON C.WAREID=D.WAREID
LEFT JOIN CUSTOMERINFO_MST E ON D.CUID=E.CUID
LEFT JOIN WORKORDER_COMPLETION_MST F ON A.WMID=F.WMID
LEFT JOIN WORKORDER_MST G ON A.WOID =G.WOID 
LEFT JOIN STORAGEINFO H ON H.STORAGEID=C.STORAGEID
LEFT JOIN STORAGE_LOCATION I ON I.SLID=C.SLID

";
        #endregion
         public CWORKORDER_COMPLETION()
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            GETID =bc.numYM(10, 4, "0001", "SELECT * FROM WORKORDER_COMPLETION_MST", "WMID", "WM");
     
            getsql = sql;
            getsqlo = sqlo;
            getsqlt = sqlt;
            getsqlth = sqlth;
            getsqlf = sqlf;
            getsqlfi = sqlfi;
        }
        public void save(DataTable dt)
        {

            SQlcommandE(dt, sqlo);
            SQlcommandEo(dt,sqlt);
        }
        #region ask
        public DataTable ask(string wpid)
        {
            string sql1 = sqlo;
            DataTable dtt = bc.getdt(sqlfi + " WHERE A.WPID='" + wpid  + "' ORDER BY A.WPKEY ASC");
            return dtt;
        }
        #endregion
        #region SQlcommandE
        protected void SQlcommandE(DataTable dt,string sql)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
       
            foreach (DataRow dr in dt.Rows)
            {
                KEY = bc.numYMD(20, 12, "000000000001", "SELECT * FROM WORKORDER_DET", "WOKEY", "WO");
                SqlConnection sqlcon = bc.getcon();
                SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
                sqlcom.Parameters.Add("@WOKEY", SqlDbType.VarChar, 20).Value = KEY;
                sqlcom.Parameters.Add("@WOID", SqlDbType.VarChar, 20).Value = XID;
                sqlcom.Parameters.Add("@DET_WAREID", SqlDbType.VarChar, 20).Value =dr["子ID"].ToString ();
                sqlcom.Parameters.Add("@SN", SqlDbType.VarChar, 20).Value = dr["项次"].ToString();
                sqlcom.Parameters.Add("@UNIT_DOSAGE", SqlDbType.VarChar, 20).Value = dr["组成用量"].ToString();
                sqlcom.Parameters.Add("@ATTRITION_DOSAGE", SqlDbType.VarChar, 20).Value = dr["损耗量"].ToString();
                //sqlcom.Parameters.Add("@NEED_DOSAGE", SqlDbType.VarChar, 20).Value = dr["需求量"].ToString();
                sqlcom.Parameters.Add("@WO_DOSAGE", SqlDbType.VarChar, 20).Value = dr["生产用量"].ToString();
                //sqlcom.Parameters.Add("@REMARK", SqlDbType.VarChar, 20).Value = v6;
           
                    sqlcom.Parameters.Add("@IFC_SUPPLY", SqlDbType.VarChar, 20).Value = dr["客供否"].ToString();
                    sqlcom.Parameters.Add("@PICKING_STAGE", SqlDbType.VarChar, 20).Value = dr["发料阶段"].ToString();
           
                sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
                sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = MAKERID;
                sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
                sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
                sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
                sqlcon.Open();
                sqlcom.ExecuteNonQuery();
                sqlcon.Close();

            }
        }
        #endregion
        #region SQlcommandEo

        protected void SQlcommandEo(DataTable dt,string sql)
        {
            
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            SqlConnection sqlcon = bc.getcon();
            SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
            sqlcom.Parameters.Add("@WOID", SqlDbType.VarChar, 20).Value = XID;
            sqlcom.Parameters.Add("@WAREID", SqlDbType.VarChar, 20).Value = dt.Rows[0]["ID"].ToString();
            sqlcom.Parameters.Add("@BOID", SqlDbType.VarChar, 20).Value = BOID;
            sqlcom.Parameters.Add("@WO_COUNT", SqlDbType.VarChar, 20).Value = WO_COUNT;
            sqlcom.Parameters.Add("@CRID", SqlDbType.VarChar, 20).Value = CRID;
            sqlcom.Parameters.Add("@SOURCE_STATUS", SqlDbType.VarChar, 20).Value = SOURCE_STATUS;
            sqlcom.Parameters.Add("@DELIVERY_DATE", SqlDbType.VarChar, 20).Value = DELIVERY_DATE;
            sqlcom.Parameters.Add("@GODE_NEED_DATE", SqlDbType.VarChar, 20).Value = GODE_NEED_DATE;
            sqlcom.Parameters.Add("@LAST_PICKING_DATE", SqlDbType.VarChar, 20).Value = LAST_PICKING_DATE;
            sqlcom.Parameters.Add("@COMPLETE_DATE", SqlDbType.VarChar, 20).Value = COMPLETE_DATE;
            sqlcom.Parameters.Add("@ADVICE_DELIVERY_DATE", SqlDbType.VarChar, 20).Value = ADVICE_DELIVER_DATE;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = MAKERID;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcon.Open();
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
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
            dtt.Columns.Add("未领用量", typeof(decimal));
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
            return dtt;
        }
        #endregion
    }
}
