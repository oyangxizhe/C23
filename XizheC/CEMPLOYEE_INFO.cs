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
    public class CEMPLOYEE_INFO
    {
        basec bc = new basec();
        private static string _EMID;
        public static string EMID
        {
            set { _EMID = value; }
            get { return _EMID; }

        }
        private static string _ENAME;
        public static string ENAME
        {
            set { _ENAME = value; }
            get { return _ENAME; }

        }
        DataTable dt = new DataTable();
        public CEMPLOYEE_INFO()
        {
           
        }

        public string  GETID()
        {
            string v1 = bc.numYM(7, 3, "001", "SELECT * FROM EMPLOYEEINFO", "EMID", "");
            string GETID = "";
            if (v1 != "Exceed Limited")
            {
                GETID = v1;
            }
            return GETID;
        }
        
      
  
    }
}
