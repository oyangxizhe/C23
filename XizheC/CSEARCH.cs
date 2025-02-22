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
    public class CSEARCH
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
        private string _sqlse;
        public string sqlse
        {
            set { _sqlse = value; }
            get { return _sqlse; }

        }
        private string _sqlei;
        public string sqlei
        {
            set { _sqlei = value; }
            get { return _sqlei; }

        }
        private static bool _IFExecutionSUCCESS;
        public static bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }

        #endregion
        #region setsql
        string setsql = @"
SELECT 
DISTINCT(A.STID),
B.SHOES_TYPE 
FROM 
WAREINFO A 
LEFT JOIN SHOES_TYPE B ON A.STID=B.STID 
WHERE 
A.STID IS NOT NULL 
AND A.STID<>''

";
      
        string setsqlo = @"
SELECT 
DISTINCT(A.SYID),
D.STYLE  
FROM WAREINFO A 
LEFT JOIN STYLE D ON A.SYID=D.SYID 
WHERE 
A.SYID IS NOT NULL
AND A.SYID<>''
";
        string setsqlt = @"
SELECT
DISTINCT(A.TTID),
E.TOE_TYPE 
FROM WAREINFO A
LEFT JOIN TOE_TYPE E ON A.TTID=E.TTID 
WHERE A.TTID IS NOT NULL AND A.TTID<>''
";
        string setsqlth = @"
SELECT 
DISTINCT(A.HHID),
F.HEEL_HEIGHT 
FROM WAREINFO A 
LEFT JOIN HEEL_HEIGHT F ON A.HHID=F.HHID 
WHERE A.HHID IS NOT NULL AND A.HHID<>''
";
        string setsqlf = @"
SELECT 
DISTINCT(A.HTID),
G.HEEL_TYPE
FROM WAREINFO A  
LEFT JOIN HEEL_TYPE G ON A.HTID=G.HTID 
WHERE A.HTID IS NOT NULL AND A.HTID<>''
";
        string setsqlfi = @"
SELECT 
DISTINCT(A.PZID),
H.PRICE_ZONE  
FROM WAREINFO A
LEFT JOIN PRICE_ZONE H ON A.PZID=H.PZID 
WHERE 
A.PZID IS NOT NULL AND A.PZID<>''
";
        string setsqlsi= @"
SELECT
DISTINCT(A.COID),
B.COLOR,
B.QUERY_COLOR_IMG 
FROM
COLOR_MANAGE A LEFT JOIN COLOR B ON A.COID=B.COID
WHERE A.COID IS NOT NULL AND A.COID<>''
";
        string setsqlse= @"
SELECT 
DISTINCT(A.SIID),
B.SIZE 
FROM SIZE_MANAGE A 
LEFT JOIN SIZE B ON A.SIID=B.SIID 
WHERE A.SIID IS NOT NULL AND A.SIID<>''
";
        string setsqlei = @"
SELECT 
DISTINCT(A.BRID),
C.BRAND 
FROM WAREINFO A LEFT JOIN BRAND C ON A.BRID=C.BRID
WHERE A.BRID IS NOT NULL AND A.BRID<>''
";
 
        DataTable dt = new DataTable();
        #endregion
         public CSEARCH()
        {
           
            sql = setsql; 
            sqlo = setsqlo;
            sqlt = setsqlt; 
            sqlth = setsqlth;
            sqlf = setsqlf;
            sqlfi = setsqlfi;
            sqlsi = setsqlsi;
            sqlse = setsqlse;
            sqlei = setsqlei;

         
        }
         public CSEARCH(string WAREID,string COID)
         {

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
    
    }
}
