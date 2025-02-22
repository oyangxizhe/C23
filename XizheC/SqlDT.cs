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
    public class SqlDT
    {
        basec bc = new basec();
        public  string _WAREID;
        public  string WAREID
        {
            set { _WAREID =value ; }
            get { return _WAREID; }

        }
        public  string _SEMI_FINISHED;
        public  string SEMI_FINISHED
        {
            set { _SEMI_FINISHED = value; }
            get { return _SEMI_FINISHED; }

        }
        public  string _MATERIALS;
        public  string MATERIALS
        {
            set { _MATERIALS = value; }
            get { return _MATERIALS; }

        }
        public SqlDT()
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");

            WAREID = bc.numYMFREE(9, 4, "0001", "SELECT * FROM WareINFO WHERE SUBSTRING(WAREID,1,1)=9 AND YEAR='" + year +
                "' AND MONTH='" + month + "'", "WAREID", "9");
            SEMI_FINISHED = bc.numYMFREE(9, 4, "0001", "SELECT * FROM WareINFO WHERE SUBSTRING(WAREID,1,1)=8 AND YEAR='" + year +
                "' AND MONTH='" + month + "'", "WAREID", "8");
            MATERIALS = bc.numYMFREE(9, 4, "0001", "SELECT * FROM WareINFO WHERE SUBSTRING(WAREID,1,1)=5 AND YEAR='" + year +
                "' AND MONTH='" + month + "'", "WAREID", "5");


        }
        public static DataTable SqlDTM(string TableName, string ColumnName)
        {

            return basec.getdts("SELECT " + ColumnName + " FROM " + TableName);
        }
    }
}
