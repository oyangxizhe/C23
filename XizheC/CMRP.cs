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
using System.Collections.Generic;

namespace XizheC
{
    public class CMRP
    {
        basec bc = new basec();
        DataTable dt = new DataTable();
        CWORKORDER_PICKING cworkorder_picking = new CWORKORDER_PICKING();
        CPURCHASE cpurchase = new CPURCHASE();
        #region nature
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
        private string _IDO;
        public string IDO
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
        private string _WAREID;
        public string WAREID
        {
            set { _WAREID = value; }
            get { return _WAREID; }

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
        private decimal  _MATERIEL_STORAGE_COUNT;
        public decimal  MATERIEL_STORAGE_COUNT
        {
            set { _MATERIEL_STORAGE_COUNT = value; }
            get { return _MATERIEL_STORAGE_COUNT; }

        }
        private decimal _P_COUNT;
        public  decimal  P_COUNT
        {
            set { _P_COUNT = value; }
            get { return _P_COUNT; }

        }
        private decimal _P_UNIT_COUNT;
        public  decimal  P_UNIT_COUNT
        {
            set { _P_UNIT_COUNT = value; }
            get { return _P_UNIT_COUNT; }

        }
        private string _ErrowInfo;
        public string ErrowInfo
        {

            set { _ErrowInfo = value; }
            get { return _ErrowInfo; }

        }
        private string _WORKORDER_PRECESS_COUNT;
        public string WORKORDER_PRECESS_COUNT
        {
            set { _WORKORDER_PRECESS_COUNT = value; }
            get { return _WORKORDER_PRECESS_COUNT; }

        }
        private string _ORDER_PRECESS_COUNT;
        public string ORDER_PRECESS_COUNT
        {
            set { _ORDER_PRECESS_COUNT = value; }
            get { return _ORDER_PRECESS_COUNT; }

        }
        private string _IFC_SUPPLY;
        public string IFC_SUPPLY
        {
            set { _IFC_SUPPLY = value; }
            get { return _IFC_SUPPLY; }

        }
        #endregion
        string sqlo = @"
UPDATE CO_ORDER SET 
CO_COUNT=@CO_COUNT,
SOURCE_STATUS=@SOURCE_STATUS,
DELIVERY_DATE=@DELIVERY_DATE,
GODE_NEEDDATE=@GODE_NEEDDATE,
LAST_PICKING_DATE=@LAST_PICKING_DATE,
COMPLETE_DATE=@COMPLETE_DATE,
ADVICE_DELIVERY_DATE=@ADVICE_DELIVERY_DATE,
REMARK=@REMARK,
DATE=@DATE,
MAKERID=@MAKERID,
YEAR=@YEAR,
MONTH=@MONTH
";
        int i = 0;
        DataTable dtt = new DataTable();
        CORDER corder = new CORDER();
        List<CMRP> list1 = new List<CMRP>();
        CWORKORDER cworkorder = new CWORKORDER();
        public CMRP()
        {
            getsqlo = sqlo;
        }
        #region getdigui
        public DataTable getdigui(string WAREID, decimal lasttime_unit_dosage, decimal lasttime_attrition_dosage)
        {

            dtcolumns();
            DataView DV = new DataView(bc.getstoragecount_MRP());
            DV.RowFilter = "品号='" + IDO + "'";
            DataTable dtx1 = DV.ToTable();
          
            WORKORDER_PRECESS_COUNT = cworkorder.GET_WORKORDER_PROGRESS_COUNT(WAREID);
            if (dtx1.Rows.Count > 0)
            {
                STORAGE_COUNT = dtx1.Rows[0]["库存数量"].ToString();
            }
            else
            {
                STORAGE_COUNT = "0";
            }
            if (!string.IsNullOrEmpty(CO_COUNT))
            {
                decimal d1 = decimal.Parse(STORAGE_COUNT) - decimal.Parse(ORDER_PRECESS_COUNT) + decimal.Parse(WORKORDER_PRECESS_COUNT);
                if (d1 >= decimal.Parse(CO_COUNT))
                {
                   
                    WO_COUNT = "0";
                    ErrowInfo = "生产数量为0，无需产生MRP！" + "(生产数量=(库存数量：" + STORAGE_COUNT + "-订单占用数量：" + ORDER_PRECESS_COUNT + "+工单在制数量：" + WORKORDER_PRECESS_COUNT +
                        ")="+d1.ToString ("#0.00")+">=厂内订单数量："+CO_COUNT+")" ;
                }
                else
                {
                    decimal d2 = decimal.Parse(CO_COUNT) -d1;
                    WO_COUNT = d2.ToString();

                }
            }
            DataTable dt5 = digui(WAREID, lasttime_unit_dosage, lasttime_attrition_dosage);
            return dt5;
        }
        #endregion
        #region digui
        public DataTable digui(string WAREID, decimal lasttime_unit_dosage, decimal lasttime_attrition_dosage)
        {
            string s1 = @"
SELECT 
A.WAREID AS ID,
D.WNAME AS 品名,
D.CO_WAREID AS 料号,
D.CWAREID AS 客户料号,
B.DET_WAREID AS 子ID,
C.WNAME AS 子品名,
C.CO_WAREID AS 子料号,
C.CWAREID AS 子客户料号,
C.SPEC AS 子规格,
C.PURCHASE_PHASE AS 采购周期,
A.BOID AS BOM编号,
A.ACTIVE AS 生效否,
B.UNIT_DOSAGE AS 组成用量,
B.ATTRITION_RATE AS 损耗率,
B.IFC_SUPPLY AS 客供否,
B.PICKING_STAGE AS 发料阶段,
C.BOM_UNIT AS BOM单位,
A.BOM_EDITION AS BOM版本,
C.MPA_TO_STOCK/C.STOCK_TO_BOM AS 库存折合BOM,
C.MPA_TO_STOCK AS 采购折合库存换算,
C.SKU AS 库存单位,
C.MPA_UNIT AS 采购单位
FROM BOM_MST A
LEFT JOIN BOM_DET B ON A.BOID=B.BOID
LEFT JOIN WAREINFO C ON B.DET_WAREID=C.WAREID
LEFT JOIN WAREINFO D ON A.WAREID=D.WAREID
";

            DataTable dtx = bc.getdt(s1 + " WHERE A.WAREID='" + WAREID + "' AND A.ACTIVE='Y'");
            if (dtx.Rows.Count > 0)
            {
                foreach (DataRow dr in dtx.Rows)
                {

                    if (bc.exists(s1 + " WHERE A.WAREID='" + dr["子ID"].ToString() + "' AND A.ACTIVE='Y'"))
                    {

                        digui(dr["子ID"].ToString(), decimal.Parse(dr["组成用量"].ToString()) * lasttime_unit_dosage,
                            decimal.Parse(dr["组成用量"].ToString()) * lasttime_attrition_dosage * decimal.Parse(dr["损耗率"].ToString()) / 100);
                    }
                    else
                    {
                        decimal d1, d2, d3, d4, d5;
                        d1 = decimal.Parse(dr["组成用量"].ToString()) * lasttime_unit_dosage;
                        d2 = decimal.Parse(dr["组成用量"].ToString()) *
                             decimal.Parse(dr["损耗率"].ToString()) / 100 * lasttime_attrition_dosage;
                        d3 = d1 + d2;
                        if (!string.IsNullOrEmpty(WO_COUNT))
                        {
                            d4 = d3 * decimal.Parse(WO_COUNT);/*use order generate advice_delivery_date,not need wo_count*/
                        }
                        else
                        {
                            d4 = 0;
                        }
                        d5 = Math.Ceiling(d4 * decimal.Parse(dr["库存折合BOM"].ToString()));
                        string v1, v2, v3, v4, v5;
                        v1 = d1.ToString("#0.00");
                        v2 = d2.ToString("#0.00");
                        v3 = d3.ToString("#0.00");
                        v4 = d4.ToString("#0.00");
                        v5 = d5.ToString("#0");
                        DataRow dr1 = dtt.NewRow();
                        dr1["ID"] = IDO;
                        dr1["项次"] = Convert.ToInt32(i + 1);
                        DataTable dtx1 = new DataTable();
                        dtx1 = bc.getdt("SELECT * FROM WAREINFO WHERE WAREID='" + WAREID + "'");
                        if (dtx1.Rows.Count > 0)
                        {
                            dr1["品名"] = dtx1.Rows[0]["WNAME"].ToString();
                            dr1["料号"] = dtx1.Rows[0]["CO_WAREID"].ToString();
                            dr1["客户料号"] = dtx1.Rows[0]["CWAREID"].ToString();
                           
                        }
                        dr1["BOM编号"] = dr["BOM编号"].ToString();
                        dr1["子ID"] = dr["子ID"].ToString();
                        DataTable dtx3 = new DataTable();
                        dtx3 = bc.getdt("SELECT * FROM WAREINFO WHERE WAREID='" + dr["子ID"].ToString() + "'");
                        if (dtx3.Rows.Count > 0)
                        {
                            dr1["MPA_TO_STOCK"] = dtx3.Rows[0]["MPA_TO_STOCK"].ToString();
                            dr1["MPA_TO_BOM"] = dtx3.Rows[0]["STOCK_TO_BOM"].ToString();

                        }
                        dr1["BOM_ID"] = dr["子ID"].ToString();
                        dr1["子品名"] = dr["子品名"].ToString();
                        dr1["子料号"] = dr["子料号"].ToString();
                        dr1["子客户料号"] = dr["子客户料号"].ToString();
                        dr1["子规格"] = dr["子规格"].ToString();
                        dr1["生效否"] = dr["生效否"].ToString();
                        dr1["组成用量"] = v1;
                        dr1["BOM单位"] = dr["BOM单位"].ToString();
                        dr1["损耗量"] = v2;
                        dr1["需求量"] = v3;
                        dr1["生产用量"] = v4;
                        dr1["工单包装领用量"] = v5;
                        dr1["累计领用量"] = 0;  /*use open workorder*/
                        dr1["累计退料量"] = 0;  /*use open workorder*/
                        dr1["领用单位"] = dr["库存单位"].ToString();
                        dr1["库存单位"] = dr["库存单位"].ToString();
                        dr1["采购单位"] = dr["采购单位"].ToString();
                        dr1["采购折合库存换算"] = dr["采购折合库存换算"].ToString();
                        DataView DV = new DataView(bc.getstoragecount_MRP());
                        DV.RowFilter = "品号='" + dr["子ID"].ToString() + "'";
                        DataTable dtx2 = DV.ToTable();
                        if (dtx2.Rows.Count > 0)
                        {
                            MATERIEL_STORAGE_COUNT= decimal.Parse (dtx2.Rows[0]["库存数量"].ToString());
                        }
                        else
                        {
                            MATERIEL_STORAGE_COUNT = 0;
                        }
                        dr1["库存数量"] = MATERIEL_STORAGE_COUNT;
                        dr1["工单占用库存"] = GET_WORKORDER_WITHOUT_PICKING_COUNT(dr["子ID"].ToString());
                        dr1["客供否"] = dr["客供否"].ToString();

                        dr1["发料阶段"] = dr["发料阶段"].ToString();
                        dr1["BOM版本"] = dr["BOM版本"].ToString();
                        dr1["采购周期"] = dr["采购周期"].ToString();
                        dr1["采购在途"] = cpurchase.GET_PURCHASE_ON_THE_WAY_COUNT(dr["子ID"].ToString());
                        decimal d6, d7;

                        d6 =decimal.Parse(GET_WORKORDER_WITHOUT_PICKING_COUNT(dr["子ID"].ToString()));
                        d7 = decimal.Parse(cpurchase.GET_PURCHASE_ON_THE_WAY_COUNT(dr["子ID"].ToString()));
                        decimal d9 = MATERIEL_STORAGE_COUNT - d6 + d7;
                        if (d9 >= d5)
                        {
                            P_COUNT = 0;
                            P_UNIT_COUNT =0;
                        }
                        else
                        {
                            P_COUNT = d5 - MATERIEL_STORAGE_COUNT + d6 - d7;
                            P_UNIT_COUNT = Math.Ceiling((d5 - MATERIEL_STORAGE_COUNT + d6 - d7) / decimal.Parse(dr["采购折合库存换算"].ToString()));
                        }
                        dr1["采购量"] = P_COUNT;
                        dr1["采购单位量"] = P_UNIT_COUNT;

                        CMRP crmp = new CMRP();
                        crmp.WAREID = dr["子ID"].ToString();
                        crmp.P_UNIT_COUNT = P_UNIT_COUNT;
                        crmp.IFC_SUPPLY = dr["客供否"].ToString();
                        list1.Add(crmp);

                        dtt.Rows.Add(dr1);
                        i = i + 1;
                    }
                }
            }

            return dtt;
        }
        #endregion
        #region dtcolumns
        public void dtcolumns()
        {

            dtt.Columns.Add("ID", typeof(string));
            dtt.Columns.Add("项次", typeof(string));
            dtt.Columns.Add("品名", typeof(string));
            dtt.Columns.Add("料号", typeof(string));
            dtt.Columns.Add("客户料号", typeof(string));
            dtt.Columns.Add("BOM编号", typeof(string));
            dtt.Columns.Add("子ID", typeof(string));
            dtt.Columns.Add("BOM_ID", typeof(string));
            dtt.Columns.Add("子料号", typeof(string));
            dtt.Columns.Add("子品名", typeof(string));
            dtt.Columns.Add("子客户料号", typeof(string));
            dtt.Columns.Add("子规格", typeof(string));
            dtt.Columns.Add("生效否", typeof(string));
            dtt.Columns.Add("组成用量", typeof(string));
            dtt.Columns.Add("BOM单位", typeof(string));
            dtt.Columns.Add("损耗量", typeof(string));
            dtt.Columns.Add("需求量", typeof(string));
            dtt.Columns.Add("生产用量", typeof(decimal));
            dtt.Columns.Add("MPA_TO_STOCK", typeof(decimal));
            dtt.Columns.Add("MPA_TO_BOM", typeof(decimal));
            dtt.Columns.Add("工单包装领用量", typeof(decimal));
            dtt.Columns.Add("领用单位", typeof(string));
            dtt.Columns.Add("库存数量", typeof(decimal));
            dtt.Columns.Add("工单占用库存", typeof(decimal));
            dtt.Columns.Add("累计领用量", typeof(decimal));
            dtt.Columns.Add("累计退料量", typeof(decimal));
            dtt.Columns.Add("未领用量", typeof(decimal),"工单包装领用量-累计领用量+累计退料量"); /*use open workorder*/
            dtt.Columns.Add("采购在途", typeof(decimal));
            dtt.Columns.Add("采购折合库存换算", typeof(decimal));
            dtt.Columns.Add("采购量", typeof(decimal));
            dtt.Columns.Add("采购单位量", typeof(string));
            dtt.Columns.Add("采购单位", typeof(string));
            dtt.Columns.Add("客供否", typeof(string));
            dtt.Columns.Add("发料阶段", typeof(string));
            dtt.Columns.Add("BOM版本", typeof(string));
            dtt.Columns.Add("厂内订单号", typeof(string));
            dtt.Columns.Add("采购周期", typeof(string));
            dtt.Columns.Add("库存单位", typeof(string));

            /*dtt.Columns.Add("工单号", typeof(string));//before workorder_table
            dtt.Columns.Add("规格", typeof(string));
            dtt.Columns.Add("品牌", typeof(string));
            dtt.Columns.Add("领料单包装领用量", typeof(decimal));
            dtt.Columns.Add("仓库", typeof(string));
            dtt.Columns.Add("库位", typeof(string));
            dtt.Columns.Add("批号", typeof(string));
            dtt.Columns.Add("领用量", typeof(decimal));
            dtt.Columns.Add("本领料单累计领用量", typeof(decimal));
            dtt.Columns.Add("工单占用量", typeof(string));
            dtt.Columns.Add("采购在途量", typeof(string));
            dtt.Columns.Add("备注", typeof(string));
            dtt.Columns.Add("状态", typeof(string));//before workorder_table*/
        }
        #endregion
        public bool IFNOALLOW_DELETE_MRP(string CRID)
        {
            bool b = false;
            if (bc.exists("SELECT * FROM WORKORDER_MST WHERE CRID='" + CRID + "'"))
            {
                b = true;
                ErrowInfo = "该厂内单号已经存在工单中，不允许修改与删除！";
            }

            return b;
        }
        #region GET_WORKORDER_WITHOUT_PICKING_COUNT
        public string GET_WORKORDER_WITHOUT_PICKING_COUNT(string WAREID)
        {
            string v = "0";
            DataView dv = new DataView(cworkorder_picking .WORKORDER_WITHOUT_PICKING_COUNT());
            dv.RowFilter = "状态 NOT IN ('CLOSE','CANCEL') AND 子ID='" + WAREID + "'";
            DataTable dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {

                v = dt.Compute("SUM(未领用量)", "").ToString();

            }
            return v;
        }
        #endregion
        #region GET_MRP_WO_COUNT
        public bool  GET_MRP_WO_COUNT(string SET_CO_COUNT,string CRID)
        {
            bool b = false;
            string SET_WAREID = bc.getOnlyString(@"
            SELECT 
B.WAREID
FROM CO_ORDER A LEFT JOIN ORDER_DET B ON A.ORKEY=B.ORKEY 
WHERE A.CRID='" + CRID + "'");
            IDO = SET_WAREID;
            CO_COUNT = SET_CO_COUNT;
            string ORKEY = bc.getOnlyString("SELECT ORKEY FROM CO_ORDER WHERE CRID='"+CRID+"'");
            ORDER_PRECESS_COUNT = corder.GET_ORDER_PROGRESS_COUNT(SET_WAREID, ORKEY);
            WORKORDER_PRECESS_COUNT = cworkorder.GET_WORKORDER_PROGRESS_COUNT(SET_WAREID);
            dt = this.getdigui(SET_WAREID , 1, 1);
            if (decimal.Parse(WO_COUNT) == 0)
            {
                b = true;
            }
            return b;
        }
        #endregion

        #region JUAGE_MRPIFNOT_NEED_GENERATE_PURCHASE
        public bool JUAGE_MRPIFNOT_NEED_GENERATE_PURCHASE()
        {
            bool b = true;
            for (i = 0; i < list1.Count; i++)
            {
                if (list1[i]._P_UNIT_COUNT !=0 && list1[i].IFC_SUPPLY!= "Y")
                {
                    b = false;
                    break;
                }
            }
            return b;
        }
        #endregion
    }
}
