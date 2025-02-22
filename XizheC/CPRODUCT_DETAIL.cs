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
    public class CPRODUCT_DETAIL
    {
        basec bc = new basec();
        private   string _WAREID;
        public  string WAREID
        {
            set { _WAREID =value ; }
            get { return _WAREID; }

        }

        private string _SELLUNITPRICE;
        public string SELLUNITPRICE
        {
            set { _SELLUNITPRICE = value; }
            get { return _SELLUNITPRICE; }

        }
        private string _STORAGE_MAX_COUNT;
        public string STORAGE_MAX_COUNT
        {
            set { _STORAGE_MAX_COUNT = value; }
            get { return _STORAGE_MAX_COUNT; }

        }
        private string _COLOR;
        public string COLOR
        {
            set { _COLOR = value; }
            get { return _COLOR; }

        }
        private string _SIZE;
        public string SIZE
        {
            set { _SIZE = value; }
            get { return _SIZE; }

        }
        DataTable dt = new DataTable();
        public CPRODUCT_DETAIL()
        {
           
        }
     
        public static DataTable SqlDTM(string TableName, string ColumnName)
        {

            return basec.getdts("SELECT " + ColumnName + " FROM " + TableName);
        }
        #region GET_SELLUNITPRICE_AND_MAX_STORAGECOUNT()
        public void  GET_SELLUNITPRICE_AND_MAX_STORAGECOUNT(string WAREID,string COID,string SIID)
        {

            DataView dv = new DataView(bc.GET_STORAGE_AND_SELLUNITPRICE(WAREID, COID));
            dv.RowFilter = "SIID='" + SIID  + "'";
            dt = dv.ToTable();

            if (dt.Rows.Count > 0)
            {
                STORAGE_MAX_COUNT = dt.Rows[0]["STORAGECOUNT"].ToString();
                SELLUNITPRICE = dt.Rows[0]["SELLUNITPRICE"].ToString();
                COLOR = dt.Rows[0]["COLOR"].ToString();
                SIZE = dt.Rows[0]["SIZE"].ToString();

            }
        }
        #endregion
     
 
  
    }
}
