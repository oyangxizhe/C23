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
    public class CWARE_INFO
    {
        basec bc = new basec();
    
        #region nature
        public  string _GETID;
        public  string GETID
        {
            set { _GETID =value ; }
            get { return _GETID; }

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
        private static bool _IFExecutionSUCCESS;
        public static bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        private int _COLOR_MAX_COUNT;
        public int COLOR_MAX_COUNT
        {
            set { _COLOR_MAX_COUNT = value; }
            get { return _COLOR_MAX_COUNT; }

        }
        private decimal _MAX_SELLUNITPRICE;
        public decimal MAX_SELLUNITPRICE
        {
            set { _MAX_SELLUNITPRICE = value; }
            get { return _MAX_SELLUNITPRICE; }

        }
        private decimal _MIN_SELLUNITPRICE;
        public decimal MIN_SELLUNITPRICE
        {
            set { _MIN_SELLUNITPRICE = value; }
            get { return _MIN_SELLUNITPRICE; }

        }
        private string _SELLUNITPRICE_PERIOD;
        public string SELLUNITPRICE_PERIOD
        {
            set { _SELLUNITPRICE_PERIOD = value; }
            get { return _SELLUNITPRICE_PERIOD; }

        }
        #endregion
        #region setsql
        string setsql = @"

SELECT 
A.WAREID AS WAREID,
A.WNAME AS WNAME,
A.CO_WAREID AS CO_WAREID,
A.CWAREID AS CWAREID,
A.SPEC AS SPEC,
A.CUID AS CUID,
CASE WHEN B.CNAME IS NOT NULL THEN B.CNAME 
ELSE D.SNAME 
END
AS CNAME,
A.REMARK AS REMARK,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID) AS MAKER,
SUBSTRING(A.DATE,1,10) AS DATE,
CASE WHEN A.ACTIVE='Y' THEN '正常'
WHEN A.ACTIVE='HOLD' THEN 'HOLD'
ELSE '作废'
END  AS ACTIVE,
A.BOM_UNIT AS BOM_UNIT,
A.BRAND AS BRAND,
A.SKU AS SKU,
A.MPA_UNIT AS MPA_UNIT
FROM  WAREINFO A
LEFT JOIN CUSTOMERINFO_MST B ON A.CUID=B.CUID
LEFT JOIN SUPPLIERINFO_MST D ON A.CUID=D.SUID

";
      
        string setsqlo = @"
SELECT 
A.WAREID AS WAREID,
B.WNAME AS WNAME,
B.CO_WAREID AS CO_WAREID,
B.CWAREID AS CWAREID,
B.SPEC AS SPEC,
C.CUID AS CUID,
C.CNAME AS CNAME,
A.SELLUNITPRICE AS SELLUNITPRICE,
A.CURRENCY AS CURRENCY,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID) AS MAKER,
SUBSTRING(A.DATE,1,10) AS DATE,
B.REMARK AS REMARK,
CASE WHEN B.ACTIVE='Y' THEN '正常'
WHEN B.ACTIVE='HOLD' THEN 'HOLD'
ELSE '作废'
END  AS ACTIVE,
B.BOM_UNIT AS BOM_UNIT,
B.BRAND AS BRAND,
B.SKU AS SKU,
B.MPA_UNIT AS MPA_UNIT
FROM  SELLUNITPRICE A
LEFT JOIN WAREINFO B ON A.WareID =B.WareID 
LEFT JOIN CustomerInfo_MST C ON B.CUID=C.CUID 
";
        /*SNAME TO CNAME FOR SEARCH */string setsqlt = @"
SELECT 
A.WAREID AS WAREID,
B.WNAME AS WNAME,
B.CO_WAREID AS CO_WAREID,
B.CWAREID AS CWAREID,
B.SPEC AS SPEC,
C.SUID AS CUID,
C.SName AS CNAME,   
A.PurchaseUnitPrice AS PURCHASEUNITPRICE,
A.CURRENCY AS CURRENCY,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID) AS MAKER,
SUBSTRING(A.DATE,1,10) AS DATE,
B.REMARK AS REMARK,
CASE WHEN B.ACTIVE='Y' THEN '正常'
WHEN B.ACTIVE='HOLD' THEN 'HOLD'
ELSE '作废'
END  AS ACTIVE,
B.BOM_UNIT AS BOM_UNIT,
B.BRAND AS BRAND,
B.SKU AS SKU,
B.MPA_UNIT AS MPA_UNIT
FROM  PURCHASEUNITPRICE A
LEFT JOIN WAREINFO B ON A.WareID =B.WareID 
LEFT JOIN SUPPLIERInfo_MST C ON A.SUID=C.SUID
";
        string setsqlth = @"
SELECT
A.WAREID,
A.WNAME,
A.SPEC,
A.UNIT,
A.MARKET_PRICE,
A.FREIGHT,
A.IMG,
SUBSTRING(IMG,4,LEN(IMG)-3) AS IMG2,
B.SHOES_TYPE,
C.BRAND,
D.STYLE,
E.TOE_TYPE,
F.HEEL_HEIGHT,
G.HEEL_TYPE,
H.PRICE_ZONE,
A.COID,
I.COLOR ,
A.MAKERID,
A.DATE 
FROM 
WAREINFO A
LEFT JOIN SHOES_TYPE B ON A.STID=B.STID
LEFT JOIN BRAND C ON A.BRID=C.BRID
LEFT JOIN STYLE D ON A.SYID=D.SYID 
LEFT JOIN TOE_TYPE E ON A.TTID=E.TTID
LEFT JOIN HEEL_HEIGHT F ON A.HHID=F.HHID
LEFT JOIN HEEL_TYPE G ON A.HTID=G.HTID
LEFT JOIN PRICE_ZONE H ON A.PZID=H.PZID
LEFT JOIN COLOR I ON A.COID=I.COID
";
        string v1, v2, v3;
        DataTable dt = new DataTable();
        #endregion
         public CWARE_INFO()
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            //GETID =bc.numYM(10, 4, "0001", "SELECT * FROM WORKORDER_SCRAP_MST", "WSID", "WS");
            sql = setsql; /*WAREINFO*/
            sqlo = setsqlo; /*ORDER*/
            sqlt = setsqlt; /*PURCHASE*/
            sqlth = setsqlth;
            string a1 = bc.getOnlyString("SELECT COUNT(*) FROM COLOR");
            int color_max_count = 0;
            if (a1 != "")
            {
                color_max_count = Convert.ToInt32(a1);
            }
            COLOR_MAX_COUNT = color_max_count;
        }
         public CWARE_INFO(string WAREID,string COID)
         {
             decimal d1 = 0, d2 = 0;
            
             dt = bc.getdt(@"
SELECT 
A.SELLUNITPRICE AS SELLUNITPRICE
FROM 
SELL_UNIT_PRICE_SHOP A 
LEFT JOIN WAREINFO B ON A.WAREID=B.WAREID 
WHERE A.WAREID='" + WAREID + "' AND A.COID='" + COID + "'");
             DataView dv = new DataView(dt);
             dv.Sort = "SELLUNITPRICE ASC";
             DataTable dtx = dv.ToTable();
             if (dtx.Rows.Count > 0)
             {
                 v1 = dtx.Rows[0]["SELLUNITPRICE"].ToString();
             }
             dv.Sort = "SELLUNITPRICE DESC";
             dtx = dv.ToTable();
             if (dtx.Rows.Count > 0)
             {
                 v2 = dtx.Rows[0]["SELLUNITPRICE"].ToString();
             }
             if (!string.IsNullOrEmpty(v1))
             {
                 d1 = decimal.Parse(v1);
             }
             if (!string.IsNullOrEmpty(v2))
             {
                 d2 = decimal.Parse(v2);
             }
             if (d1 != d2)
             {
                v3 = d1.ToString("0.00") + " - " + d2.ToString("0.00");
             }
             else
             {
                v3= d1.ToString("0.00");

             }
             SELLUNITPRICE_PERIOD = v3;
             MIN_SELLUNITPRICE = d1;
             MAX_SELLUNITPRICE = d2;
         }
         #region EMPTY_DTT()
         public DataTable EMPTY_DT()
         {
             DataTable dtt = new DataTable();
             dtt.Columns.Add("COID", typeof(string));
             dtt.Columns.Add("COLOR", typeof(string));
             dtt.Columns.Add("SIZE", typeof(string));
             dtt.Columns.Add("COUNT", typeof(string));
             dtt.Columns.Add("IMAPATH", typeof(string));
             dtt.Columns.Add("SELLUNITPRICE", typeof(decimal));
             dtt.Columns.Add("QUERY_COLOR_IMG", typeof(string));
             return dtt;
         }
         #endregion
         #region GET_MIN_SELLUNITPRICE_WAREINFO()
         public DataTable GET_MIN_SELLUNITPRICE_WAREINFO(DataTable dt)
         {
      
             DataTable dtt = new DataTable();
             dtt.Columns.Add("WAREID", typeof(string));
             dtt.Columns.Add("WNAME", typeof(string));
             dtt.Columns.Add("COID", typeof(string));
             dtt.Columns.Add("IMG", typeof(string));
             dtt.Columns.Add("IMG2", typeof(string));
             dtt.Columns.Add("SELLUNITPRICE", typeof(decimal));
             dtt.Columns.Add("MARKET_PRICE", typeof(string));
           
             foreach (DataRow dr in dt.Rows)
             {
                   CWARE_INFO cware_info = new CWARE_INFO(dr["WAREID"].ToString(), dr["COID"].ToString());
                   DataRow dr1=dtt.NewRow ();
                   dr1["WAREID"]=dr["WAREID"].ToString ();
                   dr1["WNAME"]=dr["WNAME"].ToString ();
                   dr1["COID"]=dr["COID"].ToString ();
                   dr1["IMG"]=dr["IMG"].ToString ();
                   dr1["IMG2"] = dr["IMG2"].ToString();
                   dr1["SELLUNITPRICE"] = cware_info.MIN_SELLUNITPRICE;
                   dr1["MARKET_PRICE"] = dr["MARKET_PRICE"].ToString();
                   dtt.Rows .Add (dr1);
             }
             return dtt;
         }
         #endregion

     #region GET_MAX_STORAGECOUNT()
     public DataTable GET_MAX_STORAGECOUNT(string SOURCE)
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add("WAREID", typeof(string));
            dtt.Columns.Add("CO_WAREID", typeof(string));
            dtt.Columns.Add("WNAME", typeof(string));
            dtt.Columns.Add("CWAREID", typeof(string));
            dtt.Columns.Add("SPEC", typeof(string));
            dtt.Columns.Add("CUID", typeof(string));
            dtt.Columns.Add("CNAME", typeof(string));
            /*dtt.Columns.Add("SUID", typeof(string));
            dtt.Columns.Add("SNAME", typeof(string));*/
            dtt.Columns.Add("REMARK", typeof(string));
            dtt.Columns.Add("SELLUNITPRICE", typeof(string));
            dtt.Columns.Add("PURCHASEUNITPRICE", typeof(string));
            dtt.Columns.Add("CURRENCY", typeof(string));
            dtt.Columns.Add("DATE", typeof(string));
            dtt.Columns.Add("MAKER", typeof(string));
            dtt.Columns.Add("ACTIVE", typeof(string));
            dtt.Columns.Add("BRAND", typeof(string));
            dtt.Columns.Add("MPA_UNIT", typeof(string));
            dtt.Columns.Add("SKU", typeof(string));
            dtt.Columns.Add("BOM_UNIT", typeof(string));
            dtt.Columns.Add("STORAGENAME", typeof(string));
            dtt.Columns.Add("STORAGE_LOCATION", typeof(string));
            dtt.Columns.Add("BATCHID", typeof(string));
            dtt.Columns.Add("MAX_STORAGE_COUNT", typeof(string));
            DataTable dtx = new DataTable();
            if (SOURCE == "O")
            {
                dtx = bc.getdt(sqlo);
            }
            else if (SOURCE == "P")
            {
                dtx = bc.getdt(sqlt);
               
            }
            else
            {
                dtx = bc.getdt(sql);

            }

            if (dtx.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dtx.Rows)
                {

                    DataRow dr = dtt.NewRow();
                    dr["WAREID"] = dr1["WAREID"].ToString();
                    dr["CO_WAREID"] = dr1["CO_WAREID"].ToString();
                    dr["WNAME"] = dr1["WNAME"].ToString();
                    dr["CWAREID"] = dr1["CWAREID"].ToString();
                    dr["SPEC"] = dr1["SPEC"].ToString();

                    if (SOURCE == "O")
                    {
                        dr["SELLUNITPRICE"] = dr1["SELLUNITPRICE"].ToString();
                        dr["CURRENCY"] = dr1["CURRENCY"].ToString();
                    }
                    if (SOURCE == "P")
                    {
                        dr["PURCHASEUNITPRICE"] = dr1["PURCHASEUNITPRICE"].ToString();
                        dr["CURRENCY"] = dr1["CURRENCY"].ToString();
                       
                    }
                    dr["CUID"] = dr1["CUID"].ToString();
                    dr["CNAME"] = dr1["CNAME"].ToString();
                    dr["DATE"] = dr1["DATE"].ToString();
                    dr["MAKER"] = dr1["MAKER"].ToString();
                    dr["ACTIVE"] = dr1["ACTIVE"].ToString();
                    dr["BRAND"] = dr1["BRAND"].ToString();
                    dr["MPA_UNIT"] = dr1["MPA_UNIT"].ToString();
                    dr["SKU"] = dr1["SKU"].ToString();
                    dr["BOM_UNIT"] = dr1["BOM_UNIT"].ToString();

                    DataTable dtx1 = bc.getmaxstoragecount(dr1["WAREID"].ToString(), dr1["SKU"].ToString());
                    if (dtx1.Rows.Count > 0)
                    {
                        dr["STORAGENAME"] = dtx1.Rows[0]["仓库"].ToString();
                        dr["STORAGE_LOCATION"] = dtx1.Rows[0]["库位"].ToString();
                        dr["BATCHID"] = dtx1.Rows[0]["批号"].ToString();
                        dr["MAX_STORAGE_COUNT"] = dtx1.Rows[0]["库存数量"].ToString();
                    }
                    dtt.Rows.Add(dr);
                }
            }
           
            return dtt;
        }
        #endregion
    }
}
