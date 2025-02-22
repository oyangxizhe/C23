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
    public class CCO_ORDER
    {
        basec bc = new basec();
        private string _getsql;
        public string getsql
        {
            set { _getsql = value; }
            get { return _getsql; }

        }
        private string _getsqlo;
        public string getsqlo
        {
            set { _getsqlo = value; }
            get { return _getsqlo; }

        }
        private string _ErrowInfo;
        public string ErrowInfo
        {

            set { _ErrowInfo = value; }
            get { return _ErrowInfo; }

        }
        #region sql
        string sql = @"
SELECT
A.ORKEY AS 索引,
A.ORID as 订单号,
F.CRID AS 厂内订单号,
F.SOURCE_STATUS AS 来源码,
F.CO_COUNT AS CO_COUNT,
CASE WHEN F.SOURCE_STATUS IS  NULL  THEN A.OCOUNT
ELSE  F.CO_COUNT 
END  
AS 订单数量,
A.OCOUNT AS OCOUNT,
D.CUSTOMERORID AS 客户订单号,
A.LEADDAYS AS 前置天数,
A.SN as 项次,
A.WareID as ID,
B.WNAME AS 品名,
B.SPEC as 规格,
B.CO_WAREID AS 料号,
B.CWAREID AS 客户料号,
CASE WHEN F.SOURCE_STATUS='Y' THEN A.OCOUNT
ELSE F.CO_COUNT 
END AS  订单数量,
A.SellUnitPrice as 销售单价 ,
A.TaxRate as 税率,
CASE WHEN F.SOURCE_STATUS='ORDER' THEN A.SELLUNITPRICE*A.OCOUNT
ELSE A.SELLUNITPRICE*F.CO_COUNT
END 
AS 未税金额,
CASE WHEN F.SOURCE_STATUS='ORDER' THEN A.TAXRATE/100*A.SELLUNITPRICE*OCOUNT
ELSE A.TAXRATE/100*A.SELLUNITPRICE*F.CO_COUNT
END 
AS 税额,
CASE WHEN F.SOURCE_STATUS='ORDER' THEN A.SELLUNITPRICE*(1+(A.TAXRATE)/100)*OCOUNT
ELSE A.SELLUNITPRICE*(1+(A.TAXRATE)/100)*F.CO_COUNT
END
AS 含税金额,
A.CuID as 客户ID,
C.CUSTOMER_ID AS 客户代码,
C.CName as 客户名称,
D.ORDERDATE AS 订货日期,
CASE WHEN F.DELIVERY_DATE IS NOT NULL THEN F.DELIVERY_DATE
ELSE A.DELIVERYDATE
END  AS  交货日期,
D.SALEID AS 业务员工号,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=D.SALEID ) AS 业务员,
D.ORDERSTATUS_MST AS 状态,
A.LEADDAYS AS 前置天数,
A.STOCK_PREPOSITION AS 备料前置,
A.REMARK AS 备注,
E.ADDRESS AS 送货地址,
E.CONTACT AS 联系人,
E.PHONE AS 联系电话 ,
F.GODE_NEED_DATE AS 入库需求日期,
F.LAST_PICKING_DATE AS 最晚下料日期,
F.LAST_COMPLETE_DATE AS 最晚齐套日期,
F.ADVICE_DELIVERY_DATE AS 建议客户交期,
CASE WHEN F.IF_GENERATE_MRP='Y' THEN '已产生'
WHEN F.IF_GENERATE_MRP='DO_WITHOUT' THEN '无需产生'
ELSE '未产生'
END 
AS 产生MRP否
from Order_DET A 
LEFT JOIN WAREINFO B ON A.WAREID=B.WAREID
LEFT JOIN CUSTOMERINFO_MST C ON A.CUID=C.CUID
LEFT JOIN ORDER_MST D ON A.ORID=D.ORID
LEFT JOIN CUSTOMERINFO_DET E ON C.CUKEY=E.CUKEY
LEFT JOIN CO_ORDER F ON A.ORKEY=F.ORKEY
LEFT JOIN MRP G ON F.CRID=G.CRID
";
        #endregion
        #region sqlo
        string sqlo = @"
UPDATE CO_ORDER SET 
CO_COUNT=@CO_COUNT,
SOURCE_STATUS=@SOURCE_STATUS,
DELIVERY_DATE=@DELIVERY_DATE,
GODE_NEED_DATE=@GODE_NEED_DATE,
LAST_PICKING_DATE=@LAST_PICKING_DATE,
LAST_COMPLETE_DATE=@LAST_COMPLETE_DATE,
ADVICE_DELIVERY_DATE=@ADVICE_DELIVERY_DATE,
REMARK=@REMARK,
DATE=@DATE,
MAKERID=@MAKERID,
YEAR=@YEAR,
MONTH=@MONTH
";
        #endregion
        public CCO_ORDER()
        {
            getsql = sql;
            getsqlo = sqlo;

        }
        public bool IFNOALLOW_DELETE_CRID(string CRID)
        {
            bool b = false;
            if (bc.exists("SELECT * FROM MRP WHERE CRID='" + CRID + "'"))
            {
                b = true;
                ErrowInfo = "该厂内单号已经存在MRP中，不允许修改与删除！";
            }
            else if (bc.exists("SELECT * FROM WORKORDER_MST WHERE CRID='" + CRID + "'"))
            {
                b = true;
                ErrowInfo = "该厂内单号已经存在工单中，不允许修改与删除！";
            }

            return b;
        }
        public string GET_ADVICE_DELIVERY_DATE(string WAREID)
        {
            int BOM_MAX_PURCHASE_PHASE = 0;
            string ADVICE_DELIVERY_DATE = "";
            DataTable dt1 = bc.getdt(@"
SELECT 
B.DET_WAREID, 
C.PURCHASE_PHASE
FROM BOM_MST A 
LEFT JOIN BOM_DET B ON A.BOID=B.BOID 
LEFT JOIN WareInfo C ON B.DET_WAREID =C.WareID  
WHERE A.WAREID='" + WAREID + "' AND A.ACTIVE='Y'");
            if (dt1.Rows.Count > 0)
            {
                DataView dv = new DataView(dt1);
                dv.Sort = "PURCHASE_PHASE DESC";
                DataTable dt = dv.ToTable();
                BOM_MAX_PURCHASE_PHASE = Convert.ToInt32(dt.Rows[0]["PURCHASE_PHASE"].ToString());
                string v20 = bc.getOnlyString("SELECT PRODUCTION_PHASE FROM WAREINFO WHERE WAREID='" + WAREID + "'");
                int PRODUCTION_PHASE = Convert.ToInt32(v20);
                ADVICE_DELIVERY_DATE = DateTime.Now.AddDays(+PRODUCTION_PHASE + BOM_MAX_PURCHASE_PHASE).ToString("yyyy-MM-dd");

            }
            return ADVICE_DELIVERY_DATE;
        }

    }
}
