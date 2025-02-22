using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop;
using System.Security.Cryptography;

namespace XizheC
{
    public class CORDER
    {
        basec bc = new basec();
        #region nature
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
        private  string _IDO;
        public  string IDO
        {
            set { _IDO = value; }
            get { return _IDO; }

        }
        private string _CO_COUNT;
        public string CO_COUNT
        {
            set { _CO_COUNT = value; }
            get { return _CO_COUNT; }

        }
        private string _WO_COUNT;
        public string WO_COUNT
        {
            set { _WO_COUNT = value; }
            get { return _WO_COUNT; }

        }
        private string _STORAGE_COUNT;
        public string STORAGE_COUNT
        {
            set { _STORAGE_COUNT = value; }
            get { return _STORAGE_COUNT; }

        }
        private string _ErrowInfo;
        public string ErrowInfo
        {

            set { _ErrowInfo = value; }
            get { return _ErrowInfo; }

        }
        private string _GETID;
        public string GETID
        {
            set { _GETID = value; }
            get { return _GETID; }

        }
        private string _WAREID;
        public string WAREID
        {
            set { _WAREID = value; }
            get { return _WAREID; }

        }
        private string _COID;
        public string COID
        {
            set { _COID = value; }
            get { return _COID; }

        }
        private string _SIID;
        public string SIID
        {
            set { _SIID = value; }
            get { return _SIID; }

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
        private string _ORKEY;
        public string ORKEY
        {
            set { _ORKEY = value; }
            get { return _ORKEY; }

        }
        private string _MAKERID;
        public string MAKERID
        {
            set { _MAKERID = value; }
            get { return _MAKERID; }

        }
        private string _CUID;
        public string CUID
        {
            set { _CUID = value; }
            get { return _CUID; }

        }
        #endregion

        DataTable dtx2 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt = new DataTable();
        int i,j;
        string setsql = @"
INSERT INTO ORDER_DET
(
ORKEY,
ORID,
SN,
WAREID,
COID,
SIID,
OCOUNT,
SELLUNITPRICE,
CONTACT,
PHONE,
EMAIL,
TAXRATE,
REMARK,
CUID,
ORDERSTATUS_DET,
YEAR,
MONTH,
DAY
)
VALUES 
(
@ORKEY,
@ORID,
@SN,
@WAREID,
@COID,
@SIID,
@OCOUNT,
@SELLUNITPRICE,
@CONTACT,
@PHONE,
@EMAIL,
@TAXRATE,
@REMARK,
@CUID,
@ORDERSTATUS_DET,
@YEAR,
@MONTH,
@DAY

)

";
        string setsqlo = @"
INSERT INTO ORDER_MST
(
ORID,
CUID,
ORDERDATE,
ORDERSTATUS_MST,
DATE,
MAKERID,
YEAR,
MONTH,
DAY
)
VALUES 
(
@ORID,
@CUID,
@ORDERDATE,
@ORDERSTATUS_MST,
@DATE,
@MAKERID,
@YEAR,
@MONTH,
@DAY


)

";
        public CORDER()
        {

            sql = setsql;
            sqlo = setsqlo;
            string v1 = bc.numYM(10, 4, "0001", "SELECT * FROM ORDER_MST", "ORID", "OR");
            string v2 = bc.numYMD(20, 12, "000000000001", "select * from Order_DET", "ORKEY", "OR");
            IDO = v1;
            GETID = v1;
            ORKEY = v2;

        }
        public bool IFNOALLOW_DELETE_ORID(string ORID)
        {
            bool b = false;
            if (bc.exists("SELECT * FROM CO_ORDER WHERE ORID='" + ORID + "'"))
            {
                b = true;
                ErrowInfo = "该订单号已经存在厂内订单中，不允许修改与删除！";
            }
            return b;
        }
       
     

        #region GET_TOTAL_ORDER
        public  DataTable GET_TOTAL_ORDER()
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add("索引", typeof(string));
            dtt.Columns.Add("订单号", typeof(string));
            dtt.Columns.Add("项次", typeof(string));
            dtt.Columns.Add("ID", typeof(string));
            dtt.Columns.Add("料号", typeof(string));
            dtt.Columns.Add("品名", typeof(string));
            dtt.Columns.Add("规格", typeof(string));
            dtt.Columns.Add("客户料号", typeof(string));
            dtt.Columns.Add("订单数量", typeof(decimal));
            dtt.Columns.Add("累计销货数量", typeof(decimal));
            dtt.Columns.Add("累计销退数量", typeof(decimal));
            dtt.Columns.Add("订单未结数量", typeof(decimal), "订单数量-累计销货数量+累计销退数量");
            dtt.Columns.Add("状态", typeof(string));
            dtt.Columns.Add("交货日期", typeof(string));

            DataTable dtx1 = bc.getdt("SELECT * FROM ORDER_DET ");
            if (dtx1.Rows.Count > 0)
            {
                for (i = 0; i < dtx1.Rows.Count; i++)
                {
                    DataRow dr = dtt.NewRow();
                    dr["索引"] = dtx1.Rows[i]["ORKEY"].ToString();
                    dr["订单号"] = dtx1.Rows[i]["ORID"].ToString();
                    dr["项次"] = dtx1.Rows[i]["SN"].ToString();
                    dr["ID"] = dtx1.Rows[i]["WAREID"].ToString();
                    dtx2 = bc.getdt("select * from wareinfo where wareid='" + dtx1.Rows[i]["WAREID"].ToString() + "'");
                    dr["料号"] = dtx2.Rows[0]["CO_WAREID"].ToString();
                    dr["品名"] = dtx2.Rows[0]["WNAME"].ToString();
                    dr["规格"] = dtx2.Rows[0]["SPEC"].ToString();
                    dr["客户料号"] = dtx2.Rows[0]["CWAREID"].ToString();
                    dr["订单数量"] = dtx1.Rows[i]["OCOUNT"].ToString();
                    dr["累计销货数量"] = 0;
                    dr["累计销退数量"] = 0;
                    dr["交货日期"] = dtx1.Rows[i]["DELIVERYDATE"].ToString();
                    if (dtx1.Rows[i]["ORDERSTATUS_DET"].ToString() == "OPEN")
                    {
                        dr["状态"] = "OPEN";
                    }
                    else if (dtx1.Rows[i]["ORDERSTATUS_DET"].ToString() == "PROGRESS")
                    {
                        dr["状态"] = "部分出货";
                    }
                    else if (dtx1.Rows[i]["ORDERSTATUS_DET"].ToString() == "DELAY")
                    {
                        dr["状态"] = "DELAY";
                    }
                    else
                    {
                        dr["状态"] = "已出货";
                    }

                    dtt.Rows.Add(dr);
                }

            }

            DataTable dtx4 = bc.getdt(@"
SELECT
A.ORID AS ORID,
A.SN AS SN,
B.WAREID AS WAREID,
SUM(B.MRCOUNT) AS MRCOUNT 
FROM SELLTABLE_DET A 
LEFT JOIN MATERE B ON A.SEKEY=B.MRKEY 
GROUP BY A.ORID,A.SN,B.WAREID
");
            if (dtx4.Rows.Count > 0)
            {
                for (i = 0; i < dtx4.Rows.Count; i++)
                {
                    for (j = 0; j < dtt.Rows.Count; j++)
                    {
                        if (dtt.Rows[j]["订单号"].ToString() == dtx4.Rows[i]["ORID"].ToString() && dtt.Rows[j]["项次"].ToString() == dtx4.Rows[i]["SN"].ToString())
                        {
                            dtt.Rows[j]["累计销货数量"] = dtx4.Rows[i]["MRCOUNT"].ToString();
                            break;
                        }

                    }
                }

            }
            DataTable dtx6 = bc.getdt(@"
SELECT 
A.ORID AS ORID,
A.SN AS SN,
B.WAREID AS WAREID,
SUM(B.GECOUNT) AS GECOUNT
FROM SELLRETURN_DET A 
LEFT JOIN GODE B ON A.SRKEY=B.GEKEY  
GROUP BY 
A.ORID,
A.SN,
B.WAREID

");
            if (dtx6.Rows.Count > 0)
            {
                for (i = 0; i < dtx6.Rows.Count; i++)
                {
                    for (j = 0; j < dtt.Rows.Count; j++)
                    {
                        if (dtt.Rows[j]["订单号"].ToString() == dtx6.Rows[i]["ORID"].ToString() && dtt.Rows[j]["项次"].ToString() == dtx6.Rows[i]["SN"].ToString())
                        {
                            dtt.Rows[j]["累计销退数量"] = dtx6.Rows[i]["GECOUNT"].ToString();
                            break;
                        }

                    }
                }

            }

            return dtt;
        }
        #endregion
        #region GET_ORDER_PROGRESS_COUNT
        public string GET_ORDER_PROGRESS_COUNT(string WAREID,string ORKEY)
        {
            string v = "0";
            DataView dv = new DataView(GET_TOTAL_ORDER());
            dv.RowFilter = "状态 NOT IN ('已出货') AND ID='" + WAREID + "' AND 索引 NOT IN ('"+ORKEY +"')";
            DataTable dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {

                v = dt.Compute("SUM(订单未结数量)", "").ToString();

            }
            return v;
        }
        #endregion

        #region UPDATE_ORDER_STATUS
        public void UPDATE_ORDER_STATUS(string ORID)
        {
            DataView dv = new DataView(GET_TOTAL_ORDER());
            dv.RowFilter = "订单号='" + ORID + "'";
            DataTable dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    decimal d0 = decimal.Parse(dr["订单数量"].ToString());
                    decimal d1 = decimal.Parse(dr["累计销货数量"].ToString());
                    decimal d2 = decimal.Parse(dr["累计销退数量"].ToString());

                   if (decimal.Parse (dr["订单未结数量"].ToString()) ==0)
                    {
                        basec.getcoms("UPDATE ORDER_DET SET ORDERSTATUS_DET='CLOSE' WHERE ORID='" + ORID + "' AND SN='" +dr["项次"].ToString () + "'");
                    }
                    else if (bc.JuageCurrentDateIFAboveDeliveryDate(DateTime.Now.ToString(), dr["交货日期"].ToString()))
                    {
                        basec.getcoms("UPDATE ORDER_DET SET ORDERSTATUS_DET='DELAY' WHERE ORID='" + ORID + "' AND SN='" + dr["项次"].ToString() + "'");
                    }
                    else if (d1 - d2 > 0)
                    {
                        basec.getcoms("UPDATE ORDER_DET SET ORDERSTATUS_DET='PROGRESS' WHERE ORID='" + ORID + "' AND SN='" + dr["项次"].ToString() + "'");
                    }
                    else
                    {
                        basec.getcoms("UPDATE ORDER_DET SET ORDERSTATUS_DET='OPEN' WHERE ORID='" + ORID + "' AND SN='" + dr["项次"].ToString() + "'");
                    }
                }
                if (bc.JuageOrderOrPurchaseStatus(ORID, 0))
                {
                    basec.getcoms("UPDATE ORDER_MST SET ORDERSTATUS_MST='CLOSE' WHERE ORID='" + ORID + "'");

                }
                else if (bc.JuageCurrentDateIFAboveDeliveryDate(ORID, 0))
                {
                    basec.getcoms("UPDATE ORDER_MST SET ORDERSTATUS_MST='DELAY' WHERE ORID='" + ORID + "'");
                }
                else if (JUAGE_REALTY_IFHAVE_SELLCOUNT(ORID))
                {

                    basec.getcoms("UPDATE ORDER_MST SET ORDERSTATUS_MST='PROGRESS' WHERE ORID='" + ORID + "'");
                }
                else
                {
                    basec.getcoms("UPDATE ORDER_MST SET ORDERSTATUS_MST='OPEN' WHERE ORID='" + ORID + "'");

                }
            }
        }
        #endregion
        #region JUAGE_REALTY_IFHAVE_SELLCOUNT
        public bool  JUAGE_REALTY_IFHAVE_SELLCOUNT(string ORID)
        {
            bool b = false;
            DataView dv = new DataView(GET_TOTAL_ORDER());
            dv.RowFilter = "订单号='" + ORID + "'";
            DataTable dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    decimal d1 = decimal.Parse(dr["累计销货数量"].ToString());
                    decimal d2 = decimal.Parse(dr["累计销退数量"].ToString());
                    if (d1 - d2 > 0)
                    {
                        b = true;
                        break;
                    }

                }
            }
            return b;
        }
        #endregion
        #region JUAGE_ORDER_IF_HAVE_NO_AUDIT
        public bool JUAGE_ORDER_IF_HAVE_NO_AUDIT(string ORID)
        {
            bool b = false;
            string s2 = bc.getOnlyString("SELECT IF_AUDIT FROM ORDER_MST WHERE ORID='" +ORID  + "'");
            if (s2 != "Y")
            {
                b = true;
                ErrowInfo = "此订单未审核，不能进行相关操作！";
            }
            return b;
        }
        #endregion


        #region SQlcommandE ORDER_DET
        public  void SQlcommandE_ORDER_DET(string sql, string SN, decimal OCOUNT, decimal SELLUNITPRICE)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
          
           
            SqlConnection sqlcon = bc.getcon();
            SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
            sqlcom.Parameters.Add("@ORKEY", SqlDbType.VarChar, 20).Value = ORKEY;
            sqlcom.Parameters.Add("@ORID", SqlDbType.VarChar, 20).Value = GETID;
            sqlcom.Parameters.Add("@SN", SqlDbType.VarChar, 20).Value = SN;
            sqlcom.Parameters.Add("@WAREID", SqlDbType.VarChar, 20).Value = WAREID;
            sqlcom.Parameters.Add("@COID", SqlDbType.VarChar, 20).Value = COID;
            sqlcom.Parameters.Add("@SIID", SqlDbType.VarChar, 20).Value = SIID;
            sqlcom.Parameters.Add("@OCOUNT", SqlDbType.VarChar, 20).Value = OCOUNT;
            sqlcom.Parameters.Add("@SELLUNITPRICE", SqlDbType.VarChar, 20).Value = SELLUNITPRICE;

           
            sqlcom.Parameters.Add("@CONTACT", SqlDbType.VarChar, 20).Value = CONTACT;
            sqlcom.Parameters.Add("@PHONE", SqlDbType.VarChar, 20).Value = PHONE;
            sqlcom.Parameters.Add("@EMAIL", SqlDbType.VarChar, 20).Value = EMAIL;

            sqlcom.Parameters.Add("@TAXRATE", SqlDbType.VarChar, 20).Value = "1";
            sqlcom.Parameters.Add("@REMARK", SqlDbType.VarChar, 20).Value = "";
            sqlcom.Parameters.Add("@CUID", SqlDbType.VarChar, 20).Value = CUID;
            sqlcom.Parameters.Add("@ORDERSTATUS_DET", SqlDbType.VarChar, 20).Value = "OPEN";
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcon.Open();
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        }
        #endregion

        #region SQlcommandE ORDER_MST
        public  void SQlcommandE_ORDER_MST(string sql)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
         
           
            SqlConnection sqlcon = bc.getcon();
            SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
            sqlcom.Parameters.Add("@ORID", SqlDbType.VarChar, 20).Value = GETID;
            sqlcom.Parameters.Add("@CUID", SqlDbType.VarChar, 20).Value = CUID;
            sqlcom.Parameters.Add("@ORDERDATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@ORDERSTATUS_MST", SqlDbType.VarChar, 20).Value = "OPEN";
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = MAKERID;
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
