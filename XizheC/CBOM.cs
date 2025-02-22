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
    public class CBOM
    {
        basec bc = new basec();
        #region natrue
        private string _getsql;
        public string getsql
        {
            set { _getsql = value; }
            get { return _getsql ; }

        }
        private string _getsqlo;
        public string getsqlo
        {
            set { _getsqlo = value; }
            get { return _getsqlo; }

        }
        private string _getsqlt;
        public string getsqlt
        {
            set { _getsqlt = value; }
            get { return _getsqlt; }

        }
        private string _getsqlth;
        public string getsqlth
        {
            set { _getsqlth = value; }
            get { return _getsqlth; }

        }
        private string _getsqlf;
        public string getsqlf
        {
            set { _getsqlf = value; }
            get { return _getsqlf; }

        }
        private string _ErrowInfo;
        public string ErrowInfo
        {

            set { _ErrowInfo = value; }
            get { return _ErrowInfo; }

        }
        #endregion
        #region sql
        string sql = @"
SELECT
A.BOKEY AS 索引,
A.BOID AS BOM编号,
B.WareID AS ID,
C.WNAME AS 品名,
C.CO_WAREID AS 料号,
C.CWAREID AS 客户料号,
C.SPEC AS 规格,
B.BOM_EDITION AS BOM版本,
B.BOM_DATE AS BOM日期,
A.DET_WAREID AS 子ID,
A.SN AS 项次,
D.WNAME AS 子品名,
D.CO_WAREID AS 子料号,
D.CWAREID AS 子客户料号,
D.SPEC AS 子规格,
A.REMARK AS 备注,
A.UNIT_DOSAGE AS 组成用量,
D.BOM_UNIT AS BOM单位,
A.ATTRITION_RATE AS 损耗率,
A.IFC_SUPPLY AS 客供否,
A.PICKING_STAGE AS 发料阶段,
B.ACTIVE AS 生效否,
D.BRAND AS 品牌,
A.BIT_NUMBER AS 位号,
E.CNAME AS 客户名称,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=B.MAKERID ) AS 制单人
FROM BOM_DET A 
LEFT JOIN BOM_MST B  ON A.BOID=B.BOID
LEFT JOIN WAREINFO C ON B.WAREID=C.WAREID
LEFT JOIN WAREINFO D ON D.WAREID=A.DET_WAREID
LEFT JOIN CUSTOMERINFO_MST E ON C.CUID=E.CUID
";
        #endregion
        #region sqlo
        string sqlo = @"
INSERT INTO 
BOM_DET
(
BOKEY,
BOID,
SN,
DET_WAREID,
UNIT_DOSAGE,
ATTRITION_RATE,
IFC_SUPPLY,
PICKING_STAGE,
REMARK,
YEAR,
MONTH,
DAY,
BIT_NUMBER
)  
VALUES 
(
@BOKEY,
@BOID,
@SN,
@DET_WAREID,
@UNIT_DOSAGE,
@ATTRITION_RATE,
@IFC_SUPPLY,
@PICKING_STAGE,
@REMARK,
@YEAR,
@MONTH,
@DAY,
@BIT_NUMBER
)";
        #endregion
        #region sqlt
        string sqlt = @"
INSERT INTO 
BOM_MST
(
BOID,
WAREID,
BOM_EDITION,
BOM_DATE,
ACTIVE,
DATE,
MAKERID,
YEAR,
MONTH
)  
VALUES 
(
@BOID,
@WAREID,
@BOM_EDITION,
@BOM_DATE,
@ACTIVE,
@DATE,
@MAKERID,
@YEAR,
@MONTH
)";
        #endregion
        #region sqlth
        string sqlth = @"
UPDATE BOM_MST SET 
WAREID=@WAREID,
BOM_EDITION=@BOM_EDITION,
BOM_DATE=@BOM_DATE,
ACTIVE=@ACTIVE,
DATE=@DATE,
MAKERID=@MAKERID,
YEAR=@YEAR,
MONTH=@MONTH
";
        #endregion
        #region sqlf
        string sqlf = @"
SELECT
B.BOID AS BOM编号,
B.WareID AS ID,
C.WNAME AS 品名,
C.CO_WAREID AS 料号,
C.CWAREID AS 客户料号,
B.BOM_EDITION AS BOM版本,
B.BOM_DATE AS BOM日期,
C.BOM_UNIT AS BOM单位,
C.SPEC AS 规格,
B.ACTIVE AS 生效否,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=B.MAKERID ) AS 制单人,
B.Date AS 制单日期
FROM BOM_MST B
LEFT JOIN WAREINFO C ON B.WAREID=C.WAREID
LEFT JOIN CUSTOMERINFO_MST E ON C.CUID=E.CUID
";
        #endregion


        public CBOM()
        {

            getsql = sql;
            getsqlo = sqlo;
            getsqlt = sqlt;
            getsqlth = sqlth;
            getsqlf = sqlf;

        }
        public bool IFNOEXISTS_BOM(string WAREID)
        {
            bool b = false;
            if (!bc.exists("SELECT * FROM BOM_MST WHERE WAREID='" + WAREID + "'"))
            {
                b = true;
                ErrowInfo = "此料号不存在BOM表！";

            }
            else if(!bc.exists("SELECT * FROM BOM_MST WHERE WAREID='" + WAREID + "' AND ACTIVE='Y'"))
            {
                b = true;
                ErrowInfo = "此料号BOM表未生效！";

            }
            
            return b;
        }
     public bool  IFNOALLOW_DELETE_BOM(string BOID)
    {
        bool b = false;
        if (bc.exists("SELECT * FROM MRP WHERE BOID='" + BOID  + "'"))
        {
            b = true;
            ErrowInfo = "该BOM已经存在MRP中，不允许修改与删除！";
        }
        else if (bc.exists("SELECT * FROM WORKORDER_MST WHERE BOID='" + BOID + "'"))
        {
            b = true;
            ErrowInfo = "该BOM已经存在工单中，不允许修改与删除！";
        }
            
        return b;
    }
     public string  GETBOM_TO_STOCK(string WAREID)
     {
         string b = bc.getOnlyString("SELECT MPA_TO_STOCK/STOCK_TO_BOM FROM WAREINFO WHERE WAREID='"+WAREID +"' AND ACTIVE='Y'");
         return b;
     }
  
    }
}
