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
    public class CCUSTOMER_INFO
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
        private string _MAKERID;
        public string MAKERID
        {
            set { _MAKERID = value; }
            get { return _MAKERID; }

        }

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
        private string _CUID;
        public  string CUID
        {
            set { _CUID = value; }
            get { return _CUID; }

        }
        private  string _CNAME;
        public  string CNAME
        {
            set { _CNAME = value; }
            get { return _CNAME; }

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
        private string _CUKEY;
        public  string CUKEY
        {
            set { _CUKEY = value; }
            get { return _CUKEY; }

        }
        private string _CONTACT;
        public string CONTACT
        {
            set { _CONTACT = value; }
            get { return _CONTACT; }

        }
        private string _PHONE;
        public string PHONE
        {
            set { _PHONE = value; }
            get { return _PHONE; }

        }
        private string _EMAIL;
        public string EMAIL
        {
            set { _EMAIL = value; }
            get { return _EMAIL; }

        }
        #endregion
        DataTable dtx2 = new DataTable();
        DataTable dt = new DataTable();
        #region sql
        string setsql = @"

";

        string setsqlo = @"


";

        string setsqlt = @"

INSERT INTO CUSTOMERINFO_MST
(
CUID,
CNAME,
CUKEY,
DATE,
MAKERID,
YEAR,
MONTH,
DAY

)
VALUES
(
@CUID,
@CNAME,
@CUKEY,
@DATE,
@MAKERID,
@YEAR,
@MONTH,
@DAY
)
";
        string setsqlth = @"
UPDATE CUSTOMERINFO_MST SET 
CNAME=@CNAME,
CUKEY=@CUKEY,
DATE=@DATE,
MAKERID=@MAKERID,
YEAR=@YEAR,
MONTH=@MONTH,
DAY=@DAY
";

        string setsqlf = @"
INSERT INTO CUSTOMERINFO_DET
(
CUKEY,
CUID,
CONTACT,
PHONE,
FAX,
POSTCODE,
EMAIL,
ADDRESS,
DEPART,                 
MAKERID,
DATE,
YEAR,
MONTH,
DAY
) 
VALUES 
(
@CUKEY,
@CUID,
@CONTACT,
@PHONE,
@FAX,
@POSTCODE,
@EMAIL,
@ADDRESS,
@DEPART,                 
@MAKERID,
@DATE,
@YEAR,
@MONTH,
@DAY
)
";
        string setsqlfi = @"
UPDATE CUSTOMERINFO_DET SET
CUID=@CUID,
CONTACT=@CONTACT,
PHONE=@PHONE,
FAX=@FAX,
POSTCODE=@POSTCODE,
EMAIL=@EMAIL,
ADDRESS=@ADDRESS,
DEPART=@DEPART,                 
MAKERID=@MAKERID,
DATE=@DATE,
YEAR=@YEAR,
MONTH=@MONTH,
DAY=@DAY



";
        string setsqlsi = @"

)
";
        #endregion
        public CCUSTOMER_INFO()
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            string v1 = bc.numYMCU(7, 5, "00001", "SELECT * FROM CUSTOMERINFO_MST", "CUID", "CU");
            string v2 = bc.numYMD(20, 12, "000000000001", "SELECT * FROM CUSTOMERINFO_DET", "CUKEY", "CU");
            GETID = v1;
            CUKEY = v2;
            sql = setsql;
            sqlo = setsqlo;
            sqlt = setsqlt;
            sqlth = setsqlth;
            sqlf = setsqlf;
            sqlfi = setsqlfi;
            sqlsi = setsqlsi;

        }

         #region FROM_USID_GET_CUID
         public void FROM_USID_GET_CUID(string USID)
         {
             dt = bc.getdt("SELECT A.EMID,B.CUID,B.CNAME FROM USERINFO A LEFT JOIN CUSTOMERINFO_MST B ON A.UNAME=B.CNAME WHERE A.USID='" + USID + "'");
             if (dt.Rows.Count > 0)
             {
                 CUID = dt.Rows[0]["CUID"].ToString();
                 CNAME = dt.Rows[0]["CNAME"].ToString();
                 EMID = dt.Rows[0]["EMID"].ToString();
              
             }
         
         }
         #endregion
        #region SQlcommandE CUSTOMERINFO_DET
        public  void SQlcommandE_CUSTOMERINFO_DET(string sql,string CUID, string EMID)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");


            string varMakerID = EMID;
            SqlConnection sqlcon = bc.getcon();
            SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
            sqlcom.Parameters.Add("@CUKEY", SqlDbType.VarChar, 20).Value = CUKEY;
            sqlcom.Parameters.Add("@CUID", SqlDbType.VarChar, 20).Value = CUID;
            sqlcom.Parameters.Add("@CONTACT", SqlDbType.VarChar, 20).Value = CONTACT;
            sqlcom.Parameters.Add("@PHONE", SqlDbType.VarChar, 20).Value = PHONE;
            sqlcom.Parameters.Add("@FAX", SqlDbType.VarChar, 20).Value = "";
            sqlcom.Parameters.Add("@POSTCODE", SqlDbType.VarChar, 20).Value = "";
            sqlcom.Parameters.Add("@EMAIL", SqlDbType.VarChar, 20).Value = EMAIL;
            sqlcom.Parameters.Add("@ADDRESS", SqlDbType.VarChar, 20).Value = "";
            sqlcom.Parameters.Add("@DEPART", SqlDbType.VarChar, 20).Value = "";
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = varMakerID;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcon.Open();
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        }
        #endregion

        #region SQlcommandE CUSTOMERINFO_MST
        public  void SQlcommandE_CUSTOMERINFO_MST(string CNAME, string EMID)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");

            string varMakerID =EMID;
            SqlConnection sqlcon = bc.getcon();
            SqlCommand sqlcom = new SqlCommand(setsqlt , sqlcon);
            sqlcom.Parameters.Add("@CUID", SqlDbType.VarChar, 20).Value = GETID;
            sqlcom.Parameters.Add("@CNAME", SqlDbType.VarChar, 20).Value = CNAME;
            sqlcom.Parameters.Add("@CUKEY", SqlDbType.VarChar, 20).Value = CUKEY;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = varMakerID;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcon.Open();
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        }
        #endregion
    }
}
