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

namespace XizheC
{
    public class ReturnSql
    {
        
        private string _TableName;
        public string TableName
        {
            set { _TableName  = value; }
            get { return _TableName ; }

        }
        private string _ColumnName;
        public string ColumnName
        {
            set { _ColumnName = value; }
            get { return _ColumnName; }

        }
        public  ReturnSql()
        {
          
           
        }
        public static DataTable ReturnSqlM(string TableName, string ColumnName)
        {
          
            return  basec .getdts("SELECT "+ColumnName +" FROM "+TableName) ;
        }
    }
}
