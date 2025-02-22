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
    public class CPURCHASE_GODE
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
        int i,j;
        DataTable dtx2 = new DataTable();
        CBOM cbom = new CBOM();
        #region sql
        string setsql = @"
SELECT
A.PGID AS 入库单号,
A.PUID AS 采购单号,
A.SN AS 项次,
E.WAREID AS ID,
B.WNAME AS 物料类别,
B.SPEC AS 规格,
C.PCOUNT AS 采购数量,
C.MPA_UNIT AS 采购单位,
E.P_GECOUNT AS 入库数量 ,
E.FREECOUNT AS FREE数量,
C.SKU AS 库存单位,
C.SUID AS 供应商代码,
D.SNAME AS 供应商名称 ,
G.STORAGENAME AS 仓库,
H.STORAGE_LOCATION AS 库位,
E.BATCHID AS 批号,
F.GODEDATE AS 入库日期,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=F.GODERID )  AS 入库员,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=E.MAKERID )  AS 制单人,
E.DATE AS 制单日期,
A.REMARK AS 备注
FROM PURCHASEGODE_DET A 
LEFT JOIN PURCHASE_DET C ON A.PUID=C.PUID AND A.SN=C.SN
LEFT JOIN SUPPLIERINFO_MST D ON C.SUID=D.SUID
LEFT JOIN GODE E ON A.PGKEY=E.GEKEY
LEFT JOIN PURCHASEGODE_MST F ON A.PGID=F.PGID
LEFT JOIN WAREINFO B ON E.WAREID=B.WAREID
LEFT JOIN STORAGEINFO G ON E.STORAGEID=G.STORAGEID
LEFT JOIN STORAGE_LOCATION H ON E.SLID=H.SLID


";
        
        string setsqlo = @"
INSERT INTO PURCHASEGODE_DET
(
PGKEY,
PGID,
PUKEY,
PUID,
SN,
REMARK,
YEAR,
MONTH,
DAY
)
VALUES
(
@PGKEY,
@PGID,
@PUKEY,
@PUID,
@SN,
@REMARK,
@YEAR,
@MONTH,
@DAY
)
";

        string setsqlt = @"
INSERT INTO PURCHASEGODE_MST
(
PGID,
GODEDATE,
GODERID,
DATE,
MAKERID,
YEAR,
MONTH,
DAY
)
VALUES
(
@PGID,
@GODEDATE,
@GODERID,
@DATE,
@MAKERID,
@YEAR,
@MONTH,
@DAY
)
";
        string setsqlth = @"
UPDATE PURCHASEGODE_MST SET 
PGID=@PGID,
GODEDATE=@GODEDATE,
GODERID=@GODERID,
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
P_GECOUNT,
MPA_UNIT,
GECOUNT,
SKU,
BOM_GECOUNT,
BOM_UNIT,
FREECOUNT,
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
@P_GECOUNT,
@MPA_UNIT,
@GECOUNT,
@SKU,
@BOM_GECOUNT,
@BOM_UNIT,
@FREECOUNT,
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
         public CPURCHASE_GODE()
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            //GETID =bc.numYM(10, 4, "0001", "SELECT * FROM WORKORDER_PICKING_MST", "WPID", "WP");
     
             sql= setsql;
            sqlo=setsqlo;
            sqlt=setsqlt;
            sqlth=setsqlth;
            sqlf= setsqlf;
            sqlfi=setsqlfi;
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
