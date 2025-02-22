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
    public class CAUDIT_JURISDICTION
    {
        basec bc = new basec();
        #region nature
        private string _GETID;
        public string GETID
        {
            set { _GETID = value; }
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
        #endregion
        string setsql = @"
SELECT 
DISTINCT(A.USID) AS USID,
B.UNAME AS UNAME,
C.ENAME AS ENAME,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID) AS MAKER,
A.DATE AS DATE 
FROM   
AUDIT_JURISDICTION  A 
LEFT JOIN USERINFO B ON A.USID=B.USID
LEFT JOIN EMPLOYEEINFO C ON C.EMID=B.EMID
";
        string setsqlo = @"INSERT INTO AUDIT_JURISDICTION(
USID,
BILL_NAME,
MAKERID,
DATE
)
VALUES 
(
@USID,
@BILL_NAME,
@MAKERID,
@DATE
)

";
        string setsqlt = @"
UPDATE AUDIT_JURISDICTION SET 
USID=@USID,
BILL_NAME=@BILL_NAME,
MAKERID=@MAKERID,
DATE=@DATE
";
        string setsqlth = @"
SELECT 
A.USID AS USID,
A.BILL_NAME AS BILL_NAME,
B.UNAME AS UNAME,
C.ENAME AS ENAME,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID) AS MAKER,
A.DATE AS DATE 
FROM   
AUDIT_JURISDICTION  A 
LEFT JOIN USERINFO B ON A.USID=B.USID
LEFT JOIN EMPLOYEEINFO C ON C.EMID=B.EMID
";

        public CAUDIT_JURISDICTION()
        {
            sql = setsql;
            sqlo = setsqlo;
            sqlt = setsqlt;
            sqlth = setsqlth;
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            //GETID = bc.numYMD(11, 3, "001", "SELECT * FROM QUALITY_INFO", "QUID", "QU");
        }
        public static DataTable SqlDTM(string TableName, string ColumnName)
        {

            return basec.getdts("SELECT " + ColumnName + " FROM " + TableName);
        }
    }
}
