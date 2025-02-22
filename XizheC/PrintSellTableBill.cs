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
    public class PrintSellTableBill
    {
        basec bc = new basec();
        CORDER corder = new CORDER();
        private string _ORID;
        public string ORID
        {
            set { _ORID = value; }
            get { return _ORID; }

        }
        private string _SN;
        public string SN
        {
            set { _SN = value; }
            get { return _SN; }

        }
        private string _ErrowInfo;
        public string ErrowInfo
        {

            set { _ErrowInfo = value; }
            get { return _ErrowInfo; }

        }
        string sqlo = @"
SELECT
F.CUKEY AS CUKEY,
A.SEID AS 销货单号,
A.ORID as 订单号, 
A.SN as 项次,
E.WareID as ID,
B.CO_WAREID AS 料号,
B.WNAME AS 品名,
B.CWAREID AS 客户料号,
B.SPEC as 规格,
C.OCOUNT AS 订单数量,
C.SELLUNITPRICE AS 销售单价,
C.TAXRATE AS 税率,
CAST(ROUND(E.MRCount,2) AS DECIMAL(18,2)) as 销货数量 ,
CAST(ROUND(E.FREECount,2) AS DECIMAL(18,2)) as FREE数量 ,
CAST(ROUND(E.MRCOUNT*C.SELLUNITPRICE,4) AS DECIMAL(18,2)) AS 未税金额,
CAST(ROUND(E.MRCOUNT*C.SELLUNITPRICE*C.TAXRATE/100,2) AS DECIMAL(18,2)) AS 税额,
CAST(ROUND(E.MRCOUNT*C.SELLUNITPRICE*(1+C.TAXRATE/100),2) AS DECIMAL(18,2)) AS 含税金额,C.CUID as 客户代码,
D.CName as 客户名称 ,G.CONTACT AS 联系人,G.PHONE AS 联系电话,G.ADDRESS AS 送货地址,F.SELLDATE AS 销货日期,
F.SELLERID AS 销货员工号,(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=F.SELLERID )  AS 销货员,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=E.MAKERID )  AS 制单人,H.CustomerORID AS 客户订单号,
E.DATE AS 制单日期,A.REMARK AS 备注,E.BatchID AS 批号
from SELLTABLE_DET A 
LEFT JOIN ORDER_DET C ON A.ORID=C.ORID AND A.SN=C.SN
LEFT JOIN CUSTOMERINFO_MST D ON C.CUID=D.CUID
LEFT JOIN MATERE E ON A.SEKEY=E.MRKEY
LEFT JOIN WAREINFO B ON E.WAREID=B.WAREID
LEFT JOIN SELLTABLE_MST F ON A.SEID=F.SEID
LEFT JOIN CUSTOMERINFO_DET G ON F.CUKEY=G.CUKEY
LEFT JOIN Order_MST H ON C.ORID =H.ORID 

";

        string sqlt = @"
SELECT A.SEID AS 销货单号, 
A.SN as 项次,
B.CO_WAREID AS 料号,
B.WNAME AS 品名,
B.CWAREID AS 客户料号,
CAST(ROUND(SUM(E.MRCount),2) AS DECIMAL(18,2)) as 销货数量 ,
CAST(ROUND(SUM(E.FREECount),2) AS DECIMAL(18,2)) as FREE数量 ,
D.CName as 客户名称 ,
G.CONTACT AS 联系人,
G.PHONE AS 联系电话,
G.ADDRESS AS 送货地址,
F.SELLDATE AS 销货日期,
H.CustomerORID AS 客户订单号,
A.REMARK AS 备注,
E.BatchID AS 批号
from SELLTABLE_DET A 
LEFT JOIN ORDER_DET C ON A.ORID=C.ORID AND A.SN=C.SN
LEFT JOIN CUSTOMERINFO_MST D ON C.CUID=D.CUID
LEFT JOIN MATERE E ON A.SEKEY=E.MRKEY
LEFT JOIN WAREINFO B ON E.WAREID=B.WAREID
LEFT JOIN SELLTABLE_MST F ON A.SEID=F.SEID
LEFT JOIN CUSTOMERINFO_DET G ON F.CUKEY=G.CUKEY
LEFT JOIN Order_MST H ON C.ORID =H.ORID 

";
        string sqlth = @"
 GROUP BY A.SEID,
 A.SN,
 B.CO_WAREID,
 B.WNAME,
 B.CWAREID,
D.CNAME,
G.CONTACT,
G.PHONE,
G.ADDRESS,
F.SELLDATE,
H.CustomerORID,
A.REMARK,
E.BATCHID 
ORDER BY A.SEID,A.SN ASC
";

        public PrintSellTableBill()
        {

        }
        #region ask
        public DataTable ask(string seid)
        {
            string sql1 = sqlo;
            DataTable dtt = bc.getdt(sql1 + " WHERE A.SEID='" + seid + "' ORDER BY A.SEKEY ASC");
            return dtt;
        }
        #endregion
        #region table  /*crystalprint 1/2*/
        public DataTable table()
        {
            DataTable dt4 = new DataTable();
            dt4.Columns.Add("客户名称", typeof(string));
            dt4.Columns.Add("销货单号", typeof(string));
            dt4.Columns.Add("送货地址", typeof(string));
            dt4.Columns.Add("联系人", typeof(string));
            dt4.Columns.Add("联系电话", typeof(string));
            dt4.Columns.Add("销货日期", typeof(string));
            dt4.Columns.Add("客户订单号", typeof(string));
            dt4.Columns.Add("料号", typeof(string));
            dt4.Columns.Add("客户料号", typeof(string));
            dt4.Columns.Add("品名", typeof(string));
            dt4.Columns.Add("项次", typeof(string));
            dt4.Columns.Add("销货数量", typeof(string));
            dt4.Columns.Add("FREE数量", typeof(string));
            dt4.Columns.Add("批号", typeof(string));
            dt4.Columns.Add("备注", typeof(string));
            dt4.Columns.Add("合计销货数量", typeof(string));
            dt4.Columns.Add("合计FREE数量", typeof(string));
            return dt4;
        }
        #endregion
        #region askTOTALt
        public DataTable askt(string seid)
        {
            string sql1 = sqlo;
            DataTable dtt = bc.getdt(sqlt + " WHERE A.SEID='" + seid + "' " + sqlth);
            DataRow dr2 = dtt.NewRow();
            dtt.Columns.Add("合计销货数量", typeof(decimal));
            dtt.Columns.Add("合计FREE数量", typeof(decimal));
            dr2["合计销货数量"] = dtt.Compute("SUM(销货数量)", "");
            dr2["合计FREE数量"] = dtt.Compute("SUM(FREE数量)", "");
            dtt.Rows.Add(dr2);
            return dtt;
        }
        #endregion

        #region ask   /*crystalprint 2/2*/
        public DataTable asko(string seid)
        {
            DataTable dtt = this.table();
            DataTable dt = bc.getdt(sqlt + " WHERE A.SEID='" + seid + "' " + sqlth);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow dr1 = dtt.NewRow();
                    dr1["客户名称"] = dr["客户名称"].ToString();
                    dr1["销货单号"] = dr["销货单号"].ToString();
                    dr1["送货地址"] = dr["送货地址"].ToString();
                    dr1["联系人"] = dr["联系人"].ToString();
                    dr1["联系电话"] = dr["联系电话"].ToString();
                    dr1["销货日期"] = dr["销货日期"].ToString();
                    dr1["客户订单号"] = dr["客户订单号"].ToString();
                    dr1["料号"] = dr["料号"].ToString();
                    dr1["客户料号"] = dr["客户料号"].ToString();
                    dr1["品名"] = dr["品名"].ToString();
                    dr1["项次"] = dr["项次"].ToString();
                    dr1["销货数量"] = dr["销货数量"].ToString();
                    dr1["FREE数量"] = dr["FREE数量"].ToString();
                    dr1["批号"] = dr["批号"].ToString();
                    dr1["备注"] = dr["备注"].ToString();
                    dr1["合计销货数量"] = dt.Compute("SUM(销货数量)", "").ToString();
                    dr1["合计FREE数量"] = dt.Compute("SUM(FREE数量)", "").ToString();
                    dtt.Rows.Add(dr1);
                }
            }
            return dtt;
        }
        #endregion
       
    }
}
