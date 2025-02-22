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
    public class CUSER
    {
        basec bc = new basec();
        private  string _USID;
        public  string USID
        {
            set { _USID = value; }
            get { return _USID; }

        }
        private string _UNAME;
        public string UNAME
        {
            set { _UNAME = value; }
            get { return _UNAME; }

        }
        private string _EMID;
        public string EMID
        {
            set { _EMID = value; }
            get { return _EMID; }

        }
        private string _ENAME;
        public string ENAME
        {
            set { _ENAME = value; }
            get { return _ENAME; }

        }
        private string _DEPART;
        public string DEPART
        {
            set { _DEPART = value; }
            get { return _DEPART; }

        }
        DataTable dt = new DataTable();
        public CUSER()
        {
           
        }
           public CUSER(string USID)
        {
            UNAME = bc.getOnlyString("SELECT UNAME FROM USERINFO WHERE USID='"+USID +"'");
        }
        public static DataTable SqlDTM(string TableName, string ColumnName)
        {

            return basec.getdts("SELECT " + ColumnName + " FROM " + TableName);
        }
        #region EMPTY_DT()
        public DataTable EMPTY_DT()
        {

            DataTable dtt = new DataTable();
            dtt.Columns.Add("USID", typeof(string));
            dtt.Columns.Add("UNAME", typeof(string));
            dtt.Columns.Add("FREE_REGISTRATION", typeof(string));
            dtt.Columns.Add("MY_ORDER", typeof(string));
            dtt.Columns.Add("CONTACT_CUSTOMER_SERVICE", typeof(string));
            return dtt;
        }
        #endregion
        #region GET_LOGIN_INFO()
        public DataTable GET_LOGIN_INFO(string USID)
        {
            DataTable dtt = this.EMPTY_DT();
            dt = bc.getdt("SELECT * FROM USERINFO WHERE USID='"+USID +"'");
            DataRow dr1 = dtt.NewRow();
            dr1["USID"] = dt.Rows [0]["USID"].ToString();
            dr1["UNAME"] = dt.Rows[0]["UNAME"].ToString();
            dr1["FREE_REGISTRATION"] = "退出";
            dr1["MY_ORDER"] = "我的订单";
            dr1["CONTACT_CUSTOMER_SERVICE"] = "联系客服";
            dtt.Rows.Add(dr1);
            return dtt;
        }
        #endregion
        #region PLEASE_LOGIN()
        public DataTable PLEASE_LOGIN()
        {
            DataTable dtt = this.EMPTY_DT();
            DataRow dr1 = dtt.NewRow();
            dr1["UNAME"] = "请登录";
            dr1["FREE_REGISTRATION"] = "免费注册";
            dr1["MY_ORDER"] = "我的订单";
            dr1["CONTACT_CUSTOMER_SERVICE"] = "联系客服";
            dtt.Rows.Add(dr1);
            return dtt;
        }
        #endregion

        #region GET_NODEID
        public int GET_NODEID(string NODE_NAME)
        {
            string v1 = bc.getOnlyString("SELECT NODEID FROM RIGHTNAME WHERE NODE_NAME='" + NODE_NAME + "'");
            int NODE_ID = Convert.ToInt32(bc.getOnlyString("SELECT NODEID FROM RIGHTNAME WHERE NODE_NAME='" + NODE_NAME + "'"));
            return NODE_ID;
        }
        #endregion
        public string GETID()
        {
            string v1 = bc.numYM(10, 4, "0001", "SELECT * FROM USERINFO", "USID", "US");
            string GETID = "";
            if (v1 != "Exceed Limited")
            {
                GETID = v1;
            }
            return GETID;
        }
        #region JUAGE_LOGIN_IF_SUCCESS
        public bool JUAGE_LOGIN_IF_SUCCESS(string UNAME, string PWD)
        {
            bool b = false;
            try
            {
                byte[] B = bc.GetMD5(PWD);
                SqlConnection sqlcon = bc.getcon();
                string sql1 = "SELECT * FROM USERINFO WHERE PWD=@PWD and UNAME=@UNAME";
                SqlCommand sqlcom = new SqlCommand(sql1, sqlcon);
                sqlcom.Parameters.Add("@PWD", SqlDbType.Binary, 50).Value = B;
                sqlcom.Parameters.Add("@UNAME", SqlDbType.VarChar, 50).Value = UNAME;
                sqlcon.Open();
                sqlcom.ExecuteNonQuery();
                if (sqlcom.ExecuteScalar().ToString() != "")
                {
                    string sql = @"SELECT B.DEPART,B.EMID,B.ENAME,A.USID AS USID,A.UNAME FROM USERINFO A 
LEFT JOIN EMPLOYEEINFO B ON A.EMID =B.EMID WHERE A.UNAME='" +UNAME  + "'";
                    DataTable dt = basec.getdts(sql);
                    if (dt.Rows.Count > 0)
                    {
                        DEPART = dt.Rows[0]["DEPART"].ToString();
                        ENAME = dt.Rows[0]["ENAME"].ToString();
                        EMID = dt.Rows[0]["EMID"].ToString();
                        USID = dt.Rows[0]["USID"].ToString();
                    }
                    b = true;
                }
                sqlcon.Close();
            }
            catch (Exception)
            {

            }
            return b;
        }
        #endregion
    }
}
