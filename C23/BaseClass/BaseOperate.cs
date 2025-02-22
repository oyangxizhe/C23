using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace C23.BaseClass
{

    class BaseOperate
    {

        int i;
        #region  建立数据库连接
        /// <summary>
        /// 建立数据库连接.
        /// </summary>
        /// <returns>返回SqlConnection对象</returns>
        public SqlConnection getcon()
        {
          
            string M_str_sqlcon = ConfigurationManager.AppSettings["ConnectionDB"].ToString();
            SqlConnection myCon = new SqlConnection(M_str_sqlcon);
            return myCon;
        }
        #endregion

        #region  执行SqlCommand命令
        /// <summary>
        /// 执行SqlCommand
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        public void getcom(string M_str_sqlstr)
        {
            SqlConnection sqlcon = this.getcon();
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand(M_str_sqlstr, sqlcon);
            sqlcom.ExecuteNonQuery();
            sqlcom.Dispose();
            sqlcon.Close();
            sqlcon.Dispose();
        }
        #endregion

        #region  创建DataSet对象
        /// <summary>
        /// 创建一个DataSet对象
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        /// <param name="M_str_table">表名</param>
        /// <returns>返回DataSet对象</returns>
        public DataSet getds(string M_str_sqlstr, string M_str_table)
        {
            SqlConnection sqlcon = this.getcon();
            SqlDataAdapter sqlda = new SqlDataAdapter(M_str_sqlstr, sqlcon);
            DataSet myds = new DataSet();
            sqlda.Fill(myds, M_str_table);
            return myds;
        }
        #endregion

        #region  创建SqlDataReader对象
        /// <summary>
        /// 创建一个SqlDataReader对象
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        /// <returns>返回SqlDataReader对象</returns>
        public SqlDataReader getread(string M_str_sqlstr)
        {
            SqlConnection sqlcon = this.getcon();
            SqlCommand sqlcom = new SqlCommand(M_str_sqlstr, sqlcon);
            sqlcon.Open();
            SqlDataReader sqlread = sqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            return sqlread;
        }
        #endregion

        public DataTable table(string M_str_sql)
        {
            SqlConnection sqlcon = this.getcon();
            SqlCommand sqlcmd = new SqlCommand(M_str_sql, sqlcon);
            sqlcon.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            sqlcon.Close();
            GC.Collect();
            return dt;
        }
        public DataTable getdt(string M_str_sql)
        {
            SqlConnection sqlcon = this.getcon();
            SqlCommand sqlcom = new SqlCommand(M_str_sql, sqlcon);
            SqlDataAdapter da = new SqlDataAdapter(sqlcom);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }
        public SqlDataAdapter getda(string M_str_sql)
        {
            SqlConnection sqlcon = this.getcon();
            SqlCommand sqlcom = new SqlCommand(M_str_sql, sqlcon);
            SqlDataAdapter da = new SqlDataAdapter(sqlcom);
            return da;


        }
        #region 编号
        public string numN(int digit, int wcodedigit, string wcode, string sql, string tbColumns, string prifix)
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            string P_str_Code, t, r, sql1, q = "";
            int P_int_Code, w, w1;

            sql1 = sql + " WHERE YEAR='" + year + "' AND  MONTH='" + month + "' AND DAY='" + day + "'";
            SqlDataReader sqlread = this.getread(sql1);
            DataTable dt = this.getdt(sql1);
            sqlread.Read();
            if (sqlread.HasRows)
            {
                P_str_Code = Convert.ToString(dt.Rows[(dt.Rows.Count - 1)][tbColumns]);
                w1 = digit - wcodedigit;
                P_int_Code = Convert.ToInt32(P_str_Code.Substring(w1, wcodedigit)) + 1;
                t = Convert.ToString(P_int_Code);
                w = wcodedigit - t.Length;
                if (w >= 0)
                {
                    while (w >= 1)
                    {
                        q = q + "0";
                        w = w - 1;

                    }
                    r = prifix + year + month + day + q + P_int_Code;
                }
                else
                {
                    r = "Exceed Limited";

                }

            }
            else
            {
                r = prifix + year + month + day + wcode;
            }
            sqlread.Close();
            return r;
        }
        #endregion
        #region 编号1
        public string numN1(int digit, int wcodedigit, string wcode, string sql, string tbColumns, string prifix)
        {

            string P_str_Code, t, r, sql1, q = "";
            int P_int_Code, w, w1;

            sql1 = sql;
            SqlDataReader sqlread = this.getread(sql1);
            DataTable dt = this.getdt(sql1);
            sqlread.Read();
            if (sqlread.HasRows)
            {
                P_str_Code = Convert.ToString(dt.Rows[(dt.Rows.Count - 1)][tbColumns]);
                w1 = digit - wcodedigit;
                P_int_Code = Convert.ToInt32(P_str_Code.Substring(w1, wcodedigit)) + 1;
                t = Convert.ToString(P_int_Code);
                w = wcodedigit - t.Length;
                if (w >= 0)
                {
                    while (w >= 1)
                    {
                        q = q + "0";
                        w = w - 1;

                    }
                    r = prifix + q + P_int_Code;
                }
                else
                {
                    r = "Exceed Limited";

                }


            }
            else
            {
                r = prifix + wcode;
            }
            sqlread.Close();
            return r;
        }
        #endregion
        #region 编号
        public string numN2(int digit, int wcodedigit, string wcode, string sql, string tbColumns, string prifix)
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            string P_str_Code, t, r, sql1, q = "";
            int P_int_Code, w, w1;

            sql1 = sql;
            SqlDataReader sqlread = this.getread(sql1);
            DataTable dt = this.getdt(sql1);
            sqlread.Read();
            if (sqlread.HasRows)
            {
                P_str_Code = Convert.ToString(dt.Rows[(dt.Rows.Count - 1)][tbColumns]);
                w1 = digit - wcodedigit;
                P_int_Code = Convert.ToInt32(P_str_Code.Substring(w1, wcodedigit)) + 1;
                t = Convert.ToString(P_int_Code);
                w = wcodedigit - t.Length;
                if (w >= 0)
                {
                    while (w >= 1)
                    {
                        q = q + "0";
                        w = w - 1;

                    }
                    r = prifix + year + month + day + q + P_int_Code;
                }
                else
                {
                    r = "Exceed Limited";

                }

            }
            else
            {
                r = prifix + year + month + day + wcode;
            }
            sqlread.Close();
            return r;
        }
        #endregion
        #region yesno
        public int yesno(string vars)
        {
            int k = 1;
            int i;
            for (i = 0; i < vars.Length; i++)
            {
                int p = Convert.ToInt32(vars[i]);
                if (p >= 48 && p <= 57 || p == 46)
                {
                    k = 1;
                }
                else
                {
                    k = 0; break;
                }

            }

            return k;

        }
        #endregion
        #region checkphone
        public bool  checkphone(string vars)
        {
            bool  k = true;
            int i;
            for (i = 0; i < vars.Length; i++)
            {
                int p = Convert.ToInt32(vars[i]);
                if (p >= 48 && p <= 57 || p == 46 || p==45)
                {
                   
                }
                else
                {
                    k = false ;
                    break;
                }

            }

            return k;

        }
        #endregion
        #region checkEMAIL
        public bool checkEmail(string vars)
        {
            bool k = true;
            int i;
            for (i = 0; i < vars.Length; i++)
            {
                int p = Convert.ToInt32(vars[i]);
                if (p >= 48 && p <= 57 || p == 46 || p>=64 && p<=90 || p>=97 && p<=122 )
                {

                }
                else
                {
                    k = false;
                    break;
                }

            }

            return k;

        }
        #endregion
        #region getstoragetable
        public DataTable getstoragetable()
        {
            DataTable dtk = new DataTable();
            dtk.Columns.Add("品号", typeof(string));
            dtk.Columns.Add("品名", typeof(string));
            dtk.Columns.Add("套件", typeof(string));
            dtk.Columns.Add("型号", typeof(string));
            dtk.Columns.Add("细节", typeof(string));
            dtk.Columns.Add("皮种", typeof(string));
            dtk.Columns.Add("颜色", typeof(string));
            dtk.Columns.Add("线色", typeof(string));
            dtk.Columns.Add("海棉厚度", typeof(string));
            dtk.Columns.Add("仓库", typeof(string));
            dtk.Columns.Add("库存数量", typeof(decimal));
            dtk.Columns.Add("可用否", typeof(string));
            return dtk;
        }
        #endregion

        #region getstoragecount
        public DataTable getstoragecount()
        {
            int s1, s2;
            DataTable dtk = this.getstoragetable();
            DataTable dtk1 = new DataTable();
            DataTable dtk2 = new DataTable();
            string sqlk1 = @"
select a.wareid,b.wname,a.storageID,c.storagetype,sum(a.gecount) from tb_gode a
left join tb_wareinfo b on a.wareid=b.wareid left join tb_storageinfo c
 on c.storageid=a.storageid group 
 by a.wareid,b.wname,A.STORAGEID,C.STORAGETYPE order by a.wareid,A.storageid";
            string sqlk2 = "select WAREID,STORAGEID,sum(MRcount) from TB_MATERE GROUP BY WAREID,STORAGEID order by wareid,storageid ";

            dtk1 = this.getdt(sqlk1);
            dtk2 = this.getdt(sqlk2);
              
            for (s1 = 0; s1 < dtk1.Rows.Count; s1++)
            {
                decimal d1 = 0;
                string z = "";
                decimal dec1 = 0;
                for (s2 = 0; s2 < dtk2.Rows.Count; s2++)
                {

                    if (dtk1.Rows[s1][0].ToString() == dtk2.Rows[s2][0].ToString() && dtk1.Rows[s1][2].ToString() == dtk2.Rows[s2][1].ToString())
                    {

                        dec1 = (decimal.Parse(dtk1.Rows[s1][4].ToString())) - (decimal.Parse(dtk2.Rows[s2][2].ToString()));
                        z = Convert.ToString(dec1);
                        //MessageBox.Show(dtk1.Rows[s1][0].ToString()+","+dtk1.Rows[s1][3].ToString()+",dec1="+dec1+",z="+z+","+"i0"+",d1="+d1);
                        break;
                    }

                }
            
                if (z != "")
                {
                  
                    d1 = decimal.Parse(z);
                    //MessageBox.Show(dtk1.Rows[s1][0].ToString() + "," + dtk1.Rows[s1][3].ToString() + ",dec1=" + dec1 + ",z=" + z + "," + "i1" + ",d1=" + d1);
                }
                else
                {
                   
                    d1=decimal.Parse(dtk1.Rows[s1][4].ToString());
                    //MessageBox.Show(dtk1.Rows[s1][0].ToString() + "," + dtk1.Rows[s1][3].ToString() + ",dec1=" + dec1 + ",z=" + z + "," + "i2" + ",d1=" + d1);
                }
                if (d1 != 0)
                {
                    DataRow dr = dtk.NewRow();
                    dr["品号"] = dtk1.Rows[s1][0].ToString();
                    dr["品名"] = dtk1.Rows[s1][1].ToString();
                    DataTable dtx2 = this.getdt("select * from tb_wareinfo where wareid='" + dtk1.Rows[s1][0].ToString() + "'");
                    dr["品名"] = dtx2.Rows[0]["WNAME"].ToString();
                    dr["套件"] = dtx2.Rows[0]["ExternalM"].ToString();
                    dr["型号"] = dtx2.Rows[0]["TYPE"].ToString();
                    dr["细节"] = dtx2.Rows[0]["DETAIL"].ToString();
                    dr["皮种"] = dtx2.Rows[0]["Leather"].ToString();
                    dr["颜色"] = dtx2.Rows[0]["COLOR"].ToString();
                    dr["线色"] = dtx2.Rows[0]["StitchingC"].ToString();
                    dr["海棉厚度"] = dtx2.Rows[0]["Thickness"].ToString();
                    dr["仓库"] = dtk1.Rows[s1][3].ToString();
                    dr["库存数量"] = d1;
                    dr["可用否"] = dtx2.Rows[0]["ACTIVE"].ToString();
                    dtk.Rows.Add(dr);
                
                }
           
             
            }

            return dtk;

        }
        #endregion
 

        #region juagestoragecount
        public bool juagestoragecount(string id)
        {
            int i;
            bool z = true;
            DataTable dt6 = this.getdt(@"
select A.WAREID,A.STORAGEID,B.STORAGETYPE,SUM(A.GECOUNT) FROM TB_GODE A LEFT JOIN TB_STORAGEINFO B ON 
A.STORAGEID=B.STORAGEID  WHERE A.GODEID='" + id + "' GROUP BY A.WAREID,A.STORAGEID,B.STORAGETYPE ORDER BY A.WAREID,A.STORAGEID ASC");
            if (dt6.Rows.Count > 0)
            {
                for (i = 0; i < dt6.Rows.Count; i++)
                {
                    string c1, c2;
                    c1 = dt6.Rows[i][0].ToString();
                    c2 = dt6.Rows[i][2].ToString();
                    DataRow[] dr = this.getstoragecount().Select("品号='" + c1 + "' and 仓库='" + c2 + "'");
                    if (dr.Length > 0)
                    {
                        if (decimal.Parse(dr[0]["库存数量"].ToString()) < decimal.Parse(dt6.Rows[i][3].ToString()))
                        {
                            MessageBox.Show("品号:" + dt6.Rows[i][0].ToString() + " 库存不足，不允许编辑或删除该单据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            z = false;
                            break;
                        }
                    }

                }
            }
            return z;
        }
        #endregion

        #region juagedate
        public int juagedate(DateTime dtp1, DateTime dtp2)
        {
            int y1, y2, m1, m2, d1, d2;
            y1 = Convert.ToInt32(dtp1.ToString("yyyy"));
            y2 = Convert.ToInt32(dtp2.ToString("yyyy"));
            m1 = Convert.ToInt32(dtp1.ToString("MM"));
            m2 = Convert.ToInt32(dtp2.ToString("MM"));
            d1 = Convert.ToInt32(dtp1.ToString("dd"));
            d2 = Convert.ToInt32(dtp2.ToString("dd"));
            int z1 = 1;
            if (y1 > y2)
            {
                z1 = 0;
                MessageBox.Show("起始年不能大于截止年！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (y1==y2 && m1 > m2)
            {
                z1 = 0;
                MessageBox.Show("起始月不能大于截止月！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else if (y1==y2 && m1==m2 && d1 > d2)
            {

                z1 = 0;
                MessageBox.Show("起始日不能大于截止日！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            return z1;
        }
        #endregion

        #region toexcel
        public void dgvtoExcel(DataGridView dataGridView1, string str1)
        {

            SaveFileDialog sfdg = new SaveFileDialog();
            sfdg.DefaultExt = "xls";
            sfdg.Filter = "Excel(*.xls)|*.xls";
            //sfdg.RestoreDirectory = true;
            sfdg.FileName = str1;
            //sfdg.CreatePrompt = true;
            sfdg.Title = "導出到EXCEL";
            int n, w;
            n = dataGridView1.RowCount;
            w = dataGridView1.ColumnCount;


            if (sfdg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Excel.ApplicationClass excel = new Excel.ApplicationClass();
                    excel.Application.Workbooks.Add(true);

                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {

                        excel.Cells[1, j + 1] = dataGridView1.Columns[j].HeaderText;
                    }
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        for (int x = 0; x < dataGridView1.ColumnCount; x++)
                        {
                            if (dataGridView1[x, i].Value != null)
                            {
                                if (dataGridView1[x, i].ValueType == typeof(string))
                                {
                                    excel.Cells[i + 2, x + 1] = "'" + dataGridView1[x, i].Value.ToString();
                                }
                                else
                                {
                                    excel.Cells[i + 2, x + 1] = dataGridView1[x, i].Value.ToString();
                                }
                            }
                        }
                    }
                    excel.get_Range(excel.Cells[1, 1], excel.Cells[1, w]).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;

                    //excel.get_Range(excel.Cells[2, 3], excel.Cells[n, 3]).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlRight;
                    excel.get_Range(excel.Cells[1, 1], excel.Cells[n + 1, w]).Borders.LineStyle = 1;
                    //excel.get_Range(excel.Cells[1, 1], excel.Cells[n, w]).Select();
                    excel.get_Range(excel.Cells[1, 1], excel.Cells[n+1, w]).Columns.AutoFit();
                    excel.Visible = false;
                    excel.ExtendList = false;
                    excel.DisplayAlerts = false;
                    excel.AlertBeforeOverwriting = false;
                    excel.ActiveWorkbook.SaveAs(sfdg.FileName, Excel.XlFileFormat.xlExcel7, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    excel.Quit();
                    MessageBox.Show("成功导出！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    excel = null;
                    GC.Collect();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    GC.Collect();
                }

            }
        }

        #endregion

        #region exists
        public bool exists(string sql)
        {
            DataTable dtx1 = this.getdt(sql);
            if (dtx1.Rows.Count > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region maxstoragecount
        public DataTable getmaxstoragecount(string wareid)
        {
            DataTable dt = this.getstoragecount();
            DataTable dtu1=new DataTable ();
            DataRow[] dr = dt.Select("品号='"+ wareid +"'");
            if (dr.Length > 0)
            {
                DataTable dtu = this.getstoragetable();

                for (i = 0; i < dr.Length; i++)
                {
                    DataRow dr1 = dtu.NewRow();
                    dr1["品号"] = dr[i]["品号"].ToString();
                    dr1["品名"] = dr[i]["品名"].ToString();
                    dr1["仓库"] = dr[i]["仓库"].ToString();
                    dr1["库存数量"] = dr[i]["库存数量"].ToString();
                    dtu.Rows.Add(dr1);

                }
                string s1 = "";
                decimal c1 = 0;
                decimal n = 0;
                string n1 = "";
                if (dtu.Rows.Count == 1)
                {

                    s1 = dtu.Rows[0]["仓库"].ToString();
                    c1 = decimal.Parse(dtu.Rows[0]["库存数量"].ToString());
                }
                else
                {
                    for (int j = 0; j < dtu.Rows.Count; j++)
                    {
                     
                        decimal c2 = decimal.Parse(dtu.Rows[j]["库存数量"].ToString());
                     
                        if (n > c2)
                        {
                           
                        }
                        else if (n==c2)
                        {
                           

                        }
                        else
                        {
                            n = c2;
                            n1 = dtu.Rows[j]["仓库"].ToString();
                          
                        }
                    }
                    s1 = n1;
                    c1 = n;
                }
                dtu1 = this.getstoragetable();
                DataRow dr2 = dtu1.NewRow();
                dr2["品号"] = dtu.Rows[0]["品号"].ToString();
                dr2["品名"] = dtu.Rows[0]["品名"].ToString();
                dr2["仓库"] = s1;
                dr2["库存数量"] = c1;
                dtu1.Rows.Add(dr2);
            }
            return dtu1;
        }
        #endregion
        #region getstorageid
        public string getstorageid(string storagetype)
        {
            string storageid = "";
            DataTable dtx3 = this.getdt("select * from tb_storageinfo where storagetype='" + storagetype  + "'");
            if (dtx3.Rows.Count > 0)
            {
                storageid = dtx3.Rows[0][0].ToString();
            }

            return storageid;
        }
        #endregion
        #region checkingWareidAndstorage
        public string  CheckingWareidAndStorage(string wareid,string storageType)
        {
            string storagecount = "A";
            DataTable dt = this.getstoragecount();
            DataTable dtu1 = new DataTable();
            DataRow[] dr = dt.Select("品号= '" + wareid + "' and 仓库='"+storageType +"'");
            if (dr.Length > 0)
            {
                storagecount = dr[0]["库存数量"].ToString();
            }
            return storagecount;
        }
        #endregion
        #region getOnlyString
        public string getOnlyString(string sql)
        {
            string s2="";
            DataTable dtu2 = this.getdt(sql);
            if (dtu2.Rows.Count > 0)
            {
                s2=dtu2.Rows[0][0].ToString();
               
            }

            return s2;
        }
        #endregion
        #region getprintinfo
        public DataTable getPrintInfo()
        {

            DataTable dtt = new DataTable();
            dtt.Columns.Add("销货单号", typeof(string));
            dtt.Columns.Add("订单号", typeof(string));
            dtt.Columns.Add("项次", typeof(string));
            dtt.Columns.Add("品号", typeof(string));
            dtt.Columns.Add("品名", typeof(string));
            dtt.Columns.Add("套件", typeof(string));
            dtt.Columns.Add("型号", typeof(string));
            dtt.Columns.Add("细节", typeof(string));
            dtt.Columns.Add("皮种", typeof(string));
            dtt.Columns.Add("颜色", typeof(string));
            dtt.Columns.Add("线色", typeof(string));
            dtt.Columns.Add("海棉厚度", typeof(string));
            dtt.Columns.Add("销售单价", typeof(decimal));
            dtt.Columns.Add("折扣率", typeof(decimal));
            dtt.Columns.Add("税率", typeof(decimal));
            dtt.Columns.Add("订单数量", typeof(decimal));
            dtt.Columns.Add("销货数量", typeof(decimal));
            dtt.Columns.Add("未税金额", typeof(decimal), "销售单价*折扣率*销货数量");
            dtt.Columns.Add("税额", typeof(decimal), "销售单价*折扣率*销货数量*税率/100");
            dtt.Columns.Add("含税金额", typeof(decimal), "销售单价*折扣率*销货数量*(1+税率/100)");
            dtt.Columns.Add("客户代码", typeof(string));
            dtt.Columns.Add("客户", typeof(string));
            dtt.Columns.Add("电话", typeof(string));
            dtt.Columns.Add("地址", typeof(string));
            dtt.Columns.Add("订货日期", typeof(string));
            dtt.Columns.Add("交货日期", typeof(string));
            dtt.Columns.Add("加急否", typeof(string));
            dtt.Columns.Add("制单人", typeof(string));
            dtt.Columns.Add("制单日期", typeof(string));
            return dtt;


        }
        #endregion
        #region getprintinfo1
        public DataTable getPrintInfo1()
        {

            DataTable dtt = new DataTable();
            dtt.Columns.Add("销货单号", typeof(string));
            dtt.Columns.Add("项次", typeof(string));
            dtt.Columns.Add("品号", typeof(string));
            dtt.Columns.Add("品名", typeof(string));
            dtt.Columns.Add("套件", typeof(string));
            dtt.Columns.Add("型号", typeof(string));
            dtt.Columns.Add("细节", typeof(string));
            dtt.Columns.Add("皮种", typeof(string));
            dtt.Columns.Add("颜色", typeof(string));
            dtt.Columns.Add("线色", typeof(string));
            dtt.Columns.Add("海棉厚度", typeof(string));
            dtt.Columns.Add("销售单价", typeof(decimal));
            dtt.Columns.Add("销货数量", typeof(decimal));
            dtt.Columns.Add("金额", typeof(decimal));
            dtt.Columns.Add("客户代码", typeof(string));
            dtt.Columns.Add("客户", typeof(string));
            dtt.Columns.Add("电话", typeof(string));
            dtt.Columns.Add("地址", typeof(string));
            dtt.Columns.Add("订货日期", typeof(string));
            dtt.Columns.Add("交货日期", typeof(string));
            dtt.Columns.Add("加急否", typeof(string));
            dtt.Columns.Add("制单人", typeof(string));
            dtt.Columns.Add("制单日期", typeof(string));
            return dtt;
        }
        #endregion
        #region ask
        public  DataTable ask(string sqlcondition,int GROUP,int printselltable)
        {
            DataTable dtt = this.getPrintInfo1();
            DataTable dtx6 = this.getdt(sqlcondition );
            if (dtx6.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtx6.Rows.Count; i1++)
                {
                    DataRow dr = dtt.NewRow();
                    dr["销货单号"] = dtx6.Rows[i1]["SEID"].ToString();

                    if (printselltable == 1)
                    {

                    }
                    else
                    {
                        dr["项次"] = dtx6.Rows[i1]["SN"].ToString();
                    }
                    dr["品号"] = dtx6.Rows[i1]["WAREID"].ToString();
                    DataTable dtx2 = this.getdt("select * from tb_wareinfo where wareid='" + dtx6.Rows[i1]["WAREID"].ToString() + "'");
                    dr["品名"] = dtx2.Rows[0]["WNAME"].ToString();
                    dr["套件"] = dtx2.Rows[0]["ExternalM"].ToString();
                    dr["型号"] = dtx2.Rows[0]["TYPE"].ToString();
                    dr["细节"] = dtx2.Rows[0]["DETAIL"].ToString();
                    dr["皮种"] = dtx2.Rows[0]["Leather"].ToString();
                    dr["颜色"] = dtx2.Rows[0]["COLOR"].ToString();
                    dr["线色"] = dtx2.Rows[0]["StitchingC"].ToString();
                    dr["海棉厚度"] = dtx2.Rows[0]["Thickness"].ToString();
                    if (GROUP == 0)
                    {
                        dr["销货数量"] = dtx6.Rows[i1]["SECOUNT"].ToString();
                        dr["制单人"] = dtx6.Rows[i1]["MAKER"].ToString();
                        dr["制单日期"] = dtx6.Rows[i1]["DATE"].ToString();
                        dr["销售单价"] = dtx6.Rows[i1]["SELLUNITPRICE"].ToString();
                        dr["金额"] = decimal.Parse(dtx6.Rows[i1]["SELLUNITPRICE"].ToString()) * decimal.Parse(dtx6.Rows[i1]["SECOUNT"].ToString());
                        dr["加急否"] = dtx6.Rows[i1]["URGENT"].ToString();
                    }
                    else
                    {
                        dr["销货数量"] = dtx6.Rows[i1][2].ToString();
                        bool n = juageurgent(dtx6.Rows[i1]["SEID"].ToString(), dtx6.Rows[i1]["WAREID"].ToString());
                        if (n == true)
                        {
                            dr["加急否"] = "加急";
                        }
                        else
                        {
                            dr["加急否"] = "";
                        }
                       
                    }
                    DataTable dtx8 = this.getdt("SELECT * FROM TB_SELLTABLE WHERE SEID='"+dtx6.Rows [i1]["SEID"].ToString()+"'");/*测试遗漏131111将dtx6.rows[0]["seid"].tostring()改为dtx6.rows[i1]["seid"].tostring()*/
                    {
                        dr["订货日期"] = dtx8.Rows[0]["ORDERDATE"].ToString();
                        dr["交货日期"] = dtx8.Rows[0]["DELIVERYDATE"].ToString();
                        dr["客户代码"] = dtx8.Rows[0]["CUID"].ToString();

                    }
                    DataTable dtx7 = this.getdt("select * from tb_customerinfo where cuid='" + dtx8.Rows[0]["CUID"].ToString() + "'");
                    dr["客户"] = dtx7.Rows[0]["CNAME"].ToString();
                    dr["电话"] = dtx7.Rows[0]["PHONE"].ToString();
                    dr["地址"] = dtx7.Rows[0]["ADDRESS"].ToString();
                    dtt.Rows.Add(dr);
                }

          
                DataRow dr1 = dtt.NewRow();
                dr1["销货数量"]=dtt.Compute ("SUM(销货数量)","").ToString();
                dr1[13] = dtt.Compute("SUM(金额)", "").ToString();
                dtt.Rows.Add(dr1);
                
            }
            return dtt;
        }
        #endregion
        private bool juageurgent(string v1,string v2)
        {
            bool s = false;
            string sql = @"select * from tb_selltable  WHERE seid='"+v1+"' and wareid='"+v2+"'";
            DataTable dtx = this.getdt(sql);
            for (i = 0; i < dtx.Rows.Count; i++)
            {
                if (dtx.Rows[i]["URGENT"].ToString() == "加急")
                {
                  
                    s = true;
                    break;
                }
             
            }
            return s;
        }
        #region PrintOrder
        public DataTable PrintOrder(string sqlcondition)
        {

            string M_str_sql1 = @"select * FROM TB_ORDER";
            DataTable dtt = this.getPrintInfo();
            DataTable dtx6 = this.getdt(M_str_sql1 + sqlcondition);
            if (dtx6.Rows.Count > 0)
            {
                for (i = 0; i < dtx6.Rows.Count; i++)
                {
                    DataRow dr = dtt.NewRow();
                   
                    dr["订单号"] = dtx6.Rows[i]["ORID"].ToString();
                    dr["项次"] = dtx6.Rows[i]["SN"].ToString();
                    dr["品号"] = dtx6.Rows[i]["WAREID"].ToString();
                    DataTable dtx2 = this.getdt("select * from tb_wareinfo where wareid='" + dtx6.Rows[i]["WAREID"].ToString() + "'");
                    dr["品名"] = dtx2.Rows[0]["WNAME"].ToString();
                    dr["套件"] = dtx2.Rows[0]["ExternalM"].ToString();
                    dr["型号"] = dtx2.Rows[0]["TYPE"].ToString();
                    dr["细节"] = dtx2.Rows[0]["DETAIL"].ToString();
                    dr["皮种"] = dtx2.Rows[0]["Leather"].ToString();
                    dr["颜色"] = dtx2.Rows[0]["COLOR"].ToString();
                    dr["线色"] = dtx2.Rows[0]["StitchingC"].ToString();
                    dr["海棉厚度"] = dtx2.Rows[0]["Thickness"].ToString();
                    dr["订单数量"]=dtx6.Rows [i]["OCOUNT"].ToString ();
                    dr["订货日期"]=dtx6.Rows [i]["ORDERDATE"].ToString ();
                    dr["交货日期"]=dtx6.Rows [i]["DELIVERYDATE"].ToString ();
                    dr["加急否"] = dtx6.Rows[i]["URGENT"].ToString();
                    dr["客户代码"] = dtx6.Rows[i]["CUID"].ToString();
                    DataTable dtx7 = this.getdt("select * from tb_customerinfo where cuid='" + dtx6.Rows[i]["CUID"].ToString() + "'");
                    dr["客户"] = dtx7.Rows[0]["CNAME"].ToString();
                    dr["电话"] = dtx7.Rows[0]["PHONE"].ToString();
                    dr["地址"] = dtx7.Rows[0]["ADDRESS"].ToString();
                    dtt.Rows.Add(dr);

                }
            }
            return dtt;
        }
        #endregion

        #region juageValueLimites arr,b
        public  bool juageValueLimits(string[] arr,string b)
        {
            DataTable dtzz = new DataTable();
            dtzz.Columns.Add("X", typeof(string));
         
            for (i = 0; i < arr.Length; i++)
            {
                DataRow dr = dtzz.NewRow();
                dr["X"] = arr[i];
                dtzz.Rows.Add(dr);
            }
            DataRow[] dr1 = dtzz.Select("X='" + b + "'");
            bool b1 = true;
            if (dr1.Length > 0)
            {

            }
            else
            {
                b1 = false;


            }
            return b1;
        }
        #endregion

        #region juageValueLimites sql,b
        public bool juageValueLimits(string sql, string b)
        {
            DataTable dt = this.getdt(sql);
            DataTable dtzz = new DataTable();
            dtzz.Columns.Add("X", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dtzz.NewRow();
                    dr["X"] = dt.Rows[i][0].ToString();
                    dtzz.Rows.Add(dr);
                }
            }
            DataRow dr1 = dtzz.NewRow();
            dr1["X"] = "";
            dtzz.Rows.Add(dr1);
            DataRow[] dr2 = dtzz.Select("X='" + b + "'");
            bool b1 = true;
            if (dr2.Length > 0)
            {

            }
            else
            {
                b1 = false;


            }
            return b1;
        }
        #endregion

        #region ExcelPrint
        public  void ExcelPrint(DataTable dt2, string BillName, string Printpath)
        {
            int j = 0;
            SaveFileDialog sfdg = new SaveFileDialog();
            //sfdg.DefaultExt = @"D:\xls";
            sfdg.Filter = "Excel(*.xls)|*.xls";
            sfdg.RestoreDirectory = true;
            sfdg.FileName = Printpath;
            sfdg.CreatePrompt = true;
            Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbook workbook;
            Excel.Worksheet worksheet;

            DateTime date1 = Convert.ToDateTime(dt2.Rows[0]["订货日期"].ToString());
            string d1 = date1.ToString("yyyy-MM-dd");
            DateTime date2 = Convert.ToDateTime(dt2.Rows[0]["交货日期"].ToString());
            string d2 = date2.ToString("yyyy-MM-dd");
            for (i = 0; i < dt2.Rows.Count; i++)
            {
                workbook = application.Workbooks._Open(sfdg.FileName, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing);
                worksheet = (Excel.Worksheet)workbook.Worksheets[1];

                application.Visible = false;
                application.ExtendList = false;
                application.DisplayAlerts = false;
                application.AlertBeforeOverwriting = false;
                if (BillName == "订单")
                {
                    worksheet.Cells[3, 2] = "";
                    worksheet.Cells[3, 5] = "";
                    worksheet.Cells[3, 9] = "";
                    worksheet.Cells[4, 2] = "";
                    worksheet.Cells[6, 1] = "";
                    worksheet.Cells[6, 2] = "";
                    worksheet.Cells[6, 3] = "";
                    worksheet.Cells[6, 4] = "";
                    worksheet.Cells[6, 5] = "";
                    worksheet.Cells[6, 6] = "";
                    worksheet.Cells[6, 7] = "";
                    worksheet.Cells[6, 6] = "";
                    worksheet.Cells[6, 9] = "";
                    worksheet.Cells[6, 10] = "";

                    worksheet.Cells[3, 2] = dt2.Rows[i]["订单号"].ToString();
                    worksheet.Cells[3, 5] = d1;
                    worksheet.Cells[3, 9] = d2;
                    worksheet.Cells[4, 2] = dt2.Rows[i]["客户"].ToString();
                    worksheet.Cells[6, 1] = dt2.Rows[i]["品号"].ToString();
                    worksheet.Cells[6, 2] = dt2.Rows[i]["品名"].ToString();
                    worksheet.Cells[6, 3] = dt2.Rows[i]["套件"].ToString();
                    worksheet.Cells[6, 4] = dt2.Rows[i]["订单数量"].ToString();
                    worksheet.Cells[6, 5] = dt2.Rows[i]["型号"].ToString();
                    worksheet.Cells[6, 6] = dt2.Rows[i]["细节"].ToString();
                    worksheet.Cells[6, 7] = dt2.Rows[i]["皮种"].ToString();
                    worksheet.Cells[6, 8] = dt2.Rows[i]["颜色"].ToString();
                    worksheet.Cells[6, 9] = dt2.Rows[i]["线色"].ToString();
                    worksheet.Cells[6, 10] = dt2.Rows[i]["加急否"].ToString();

                    workbook.Save();
                    csharpExcelPrint(sfdg.FileName);
                }
                else
                {
                    if (j == 0)
                    {
                        worksheet.Cells[2, 2] = "";
                        worksheet.Cells[2, 5] = "";
                        worksheet.Cells[2, 9] = "";
                        worksheet.Cells[3, 2] = "";
                        worksheet.Cells[3, 9] = "";
                        worksheet.Cells[4, 2] = "";
                        for (int s1 = 6; s1 <= 10; s1++)
                        {

                            worksheet.Cells[s1, 1] = "";
                            worksheet.Cells[s1, 2] = "";
                            worksheet.Cells[s1, 3] = "";
                            worksheet.Cells[s1, 4] = "";
                            worksheet.Cells[s1, 5] = "";
                            worksheet.Cells[s1, 6] = "";
                            worksheet.Cells[s1, 7] = "";
                            worksheet.Cells[s1, 8] = "";
                            worksheet.Cells[s1, 9] = "";
                            worksheet.Cells[s1, 10] = "";

                        }

                    }
                    worksheet.Cells[2, 2] = dt2.Rows[i]["销货单号"].ToString();
                    worksheet.Cells[2, 5] = d1;
                    worksheet.Cells[2, 9] = d2;
                    worksheet.Cells[3, 2] = dt2.Rows[i]["客户"].ToString();
                    worksheet.Cells[3, 9] = dt2.Rows[i]["电话"].ToString();
                    worksheet.Cells[4, 2] = dt2.Rows[i]["地址"].ToString();
                    worksheet.Cells[6, 1] = dt2.Rows[i]["品号"].ToString();
                    worksheet.Cells[6, 2] = dt2.Rows[i]["品名"].ToString();
                    worksheet.Cells[6, 3] = dt2.Rows[i]["套件"].ToString();
                    worksheet.Cells[6, 4] = dt2.Rows[i]["销货数量"].ToString();
                    worksheet.Cells[6, 5] = dt2.Rows[i]["型号"].ToString();
                    worksheet.Cells[6, 6] = dt2.Rows[i]["细节"].ToString();
                    worksheet.Cells[6, 7] = dt2.Rows[i]["皮种"].ToString();
                    worksheet.Cells[6, 8] = dt2.Rows[i]["颜色"].ToString();
                    worksheet.Cells[6, 9] = dt2.Rows[i]["线色"].ToString();
                    worksheet.Cells[6, 10] = dt2.Rows[i]["加急否"].ToString();
                    if (i+1 < dt2.Rows.Count)
                    {
                        worksheet.Cells[7, 1] = dt2.Rows[i + 1]["品号"].ToString();
                        worksheet.Cells[7, 2] = dt2.Rows[i + 1]["品名"].ToString();
                        worksheet.Cells[7, 3] = dt2.Rows[i + 1]["套件"].ToString();
                        worksheet.Cells[7, 4] = dt2.Rows[i + 1]["销货数量"].ToString();
                        worksheet.Cells[7, 5] = dt2.Rows[i + 1]["型号"].ToString();
                        worksheet.Cells[7, 6] = dt2.Rows[i + 1]["细节"].ToString();
                        worksheet.Cells[7, 7] = dt2.Rows[i + 1]["皮种"].ToString();
                        worksheet.Cells[7, 8] = dt2.Rows[i + 1]["颜色"].ToString();
                        worksheet.Cells[7, 9] = dt2.Rows[i + 1]["线色"].ToString();
                        worksheet.Cells[7, 10] = dt2.Rows[i + 1]["加急否"].ToString();
                    }
                    if (i + 2 < dt2.Rows.Count)
                    {
                        worksheet.Cells[8, 1] = dt2.Rows[i + 2]["品号"].ToString();
                        worksheet.Cells[8, 2] = dt2.Rows[i + 2]["品名"].ToString();
                        worksheet.Cells[8, 3] = dt2.Rows[i + 2]["套件"].ToString();
                        worksheet.Cells[8, 4] = dt2.Rows[i + 2]["销货数量"].ToString();
                        worksheet.Cells[8, 5] = dt2.Rows[i + 2]["型号"].ToString();
                        worksheet.Cells[8, 6] = dt2.Rows[i + 2]["细节"].ToString();
                        worksheet.Cells[8, 7] = dt2.Rows[i + 2]["皮种"].ToString();
                        worksheet.Cells[8, 8] = dt2.Rows[i + 2]["颜色"].ToString();
                        worksheet.Cells[8, 9] = dt2.Rows[i + 2]["线色"].ToString();
                        worksheet.Cells[8, 10] = dt2.Rows[i + 2]["加急否"].ToString();
                    }
           
                    if (i + 3 < dt2.Rows.Count)
                    {
                        worksheet.Cells[9, 1] = dt2.Rows[i + 3]["品号"].ToString();
                        worksheet.Cells[9, 2] = dt2.Rows[i + 3]["品名"].ToString();
                        worksheet.Cells[9, 3] = dt2.Rows[i + 3]["套件"].ToString();
                        worksheet.Cells[9, 4] = dt2.Rows[i + 3]["销货数量"].ToString();
                        worksheet.Cells[9, 5] = dt2.Rows[i + 3]["型号"].ToString();
                        worksheet.Cells[9, 6] = dt2.Rows[i + 3]["细节"].ToString();
                        worksheet.Cells[9, 7] = dt2.Rows[i + 3]["皮种"].ToString();
                        worksheet.Cells[9, 8] = dt2.Rows[i + 3]["颜色"].ToString();
                        worksheet.Cells[9, 9] = dt2.Rows[i + 3]["线色"].ToString();
                        worksheet.Cells[9, 10] = dt2.Rows[i + 3]["加急否"].ToString();
                    }
                    if (i + 4 < dt2.Rows.Count)
                    {
                        worksheet.Cells[10, 1] = dt2.Rows[i + 4]["品号"].ToString();
                        worksheet.Cells[10, 2] = dt2.Rows[i + 4]["品名"].ToString();
                        worksheet.Cells[10, 3] = dt2.Rows[i + 4]["套件"].ToString();
                        worksheet.Cells[10, 4] = dt2.Rows[i + 4]["销货数量"].ToString();
                        worksheet.Cells[10, 5] = dt2.Rows[i + 4]["型号"].ToString();
                        worksheet.Cells[10, 6] = dt2.Rows[i + 4]["细节"].ToString();
                        worksheet.Cells[10, 7] = dt2.Rows[i + 4]["皮种"].ToString();
                        worksheet.Cells[10, 8] = dt2.Rows[i + 4]["颜色"].ToString();
                        worksheet.Cells[10, 9] = dt2.Rows[i + 4]["线色"].ToString();
                        worksheet.Cells[10, 10] = dt2.Rows[i +4]["加急否"].ToString();
                    }
                    workbook.Save();
                    csharpExcelPrint(sfdg.FileName);
                    i = i + 4;

                }
            }
            application.Quit();
            worksheet = null;
            workbook = null;
            application = null;
            GC.Collect();

        }
        #endregion

        #region csharpExcelPrint
        private void csharpExcelPrint(string path)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = path;
            p.StartInfo.Verb = "print";
            p.Start();
        }
        #endregion
    }

}
