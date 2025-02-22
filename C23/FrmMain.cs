using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using XizheC;

namespace C23
{
    public partial class FrmMain: Form
    {
         DataTable dt = new DataTable();
         DataTable dt2 = new DataTable();
         basec bc = new basec();
         CUSER cuser = new CUSER();
         CEMPLOYEE_INFO cemplyee_info = new CEMPLOYEE_INFO();
         Color c2 = System.Drawing.ColorTranslator.FromHtml("#4a7bb8");
         Color c3 = System.Drawing.ColorTranslator.FromHtml("#24ade5");
        public FrmMain()
        {
            InitializeComponent();
        }
        private void MAIN_Load(object sender, EventArgs e)
        {
    
            dt = bc.getdt("SELECT * from RightList where USID = '"+FrmLogin .USID  +"'");
            SHOW_TREEVIEW(dt);
            menuStrip1.Font = new Font("宋体", 9);
            this.MaximizedBounds = Screen.FromControl(this).WorkingArea;/*全屏时显示任务栏 1/3/*/
            this.WindowState = FormWindowState.Maximized;/*全屏时显示任务栏 2/3/*/
            this.FormBorderStyle = FormBorderStyle.None;/*全屏时显示任务栏 3/3*/


            tsslUser.Text = "||当前用户：" + FrmLogin .ENAME  ;
            tsslDepart.Text = "||所属部门：" + FrmLogin .M_str_Depart ;
            tsslTime.Text = "||登录时间：" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();

            //treeView1.ExpandAll();
            listView1.BackColor = c2;
            listBox3.BackColor = c2;
            listBox3.Height = 84;
            groupBox1.BackColor = c2;
            listView1.ForeColor = Color.White;
            listView1.Font = new Font("新宋体", 11);

            listView1.Location = new Point(1, 75);
            listView2.BorderStyle = BorderStyle.None;
            //listView1.BorderStyle = BorderStyle.None;
            listView2.Location = new Point(195, 75);
            listBox3.Location = new Point(1, -35);
            listView1.Height = 660;
            listView2.Height = 660;
            listView1.Width = 194;
             //listView1 .BackgroundImage  = Image.FromFile(System .IO.Path.GetFullPath("Image/1.png"));
            groupBox1.Height = 675;

            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/1.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/2.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/3.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/4.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/5.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/6.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/7.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/8.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/9.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/10.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/11.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/12.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/13.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/14.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/15.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/16.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/17.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/18.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/19.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/20.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/21.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/22.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/23.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/24.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/25.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/26.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/27.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/28.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/29.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/30.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/31.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/32.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/33.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/34.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/35.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/36.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/37.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/38.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/39.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/40.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/41.png")));
            imageList1.Images.Add(Image.FromFile(System.IO.Path.GetFullPath("Image/42.png")));

            imageList1.ColorDepth = ColorDepth.Depth32Bit;/*防止图片失真*/
            listView1.View = View.SmallIcon;
            listView2.View = View.LargeIcon;
            imageList1.ImageSize = new Size(48, 48);/*set imglist size*/
            listView1.SmallImageList = imageList1;
            listView2.LargeImageList = imageList1;

        }
        #region show_treeview
        private void SHOW_TREEVIEW(DataTable dt)
        {
           
            dt = bc.GET_DT_TO_DV_TO_DT(dt, "", "PARENT_NODEID=0");
            if (dt.Rows.Count > 0)
            {
                for(int i=0;i<dt.Rows.Count ;i++)
                {
                
                    ListViewItem lvi = listView1.Items.Add(dt.Rows[i]["NODE_NAME"].ToString());
                    lvi.ImageIndex = Convert.ToInt32(dt.Rows[i]["NODEID"].ToString()) - 1;/*NEED THIS SO CAN SHOW*/
                }
                click(dt.Rows [0]["NODE_NAME"].ToString ());
                listView1.Items[0].BackColor = c3;

            }
        }
        #endregion

        #region show_treeview_O
        private void SHOW_TREEVIEW_O(string NODEID)
        {

            dt2 = bc.getdt("SELECT * FROM RIGHTLIST WHERE PARENT_NODEID='" + NODEID  + "'AND  USID = '" +FrmLogin .USID + "'");
            if (dt2.Rows.Count > 0)
            {
                for(int i=0;i<dt2.Rows.Count ;i++)
                {
                    ListViewItem lvi = listView2.Items.Add(dt2.Rows [i]["NODE_NAME"].ToString());
                    lvi.ImageIndex = Convert.ToInt32(dt2.Rows[i]["NODEID"].ToString()) - 1;/*NEED THIS SO CAN SHOW*/
                    
                }
            }
        }
        #endregion
      
    
     
         private void 退出系统ToolStripMenuItem1_Click(object sender, EventArgs e)
         {
             if (MessageBox.Show("确定要退出本系统吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
             {
                 Application.Exit();
             }
             else
             {
                 FrmMain fmain = new FrmMain();
                 fmain.Show();
             }
         }

     

         private void groupBox1_Paint(object sender, PaintEventArgs e)
         {
             e.Graphics.Clear(this.BackColor);  
         }

         private void listView1_Click(object sender, EventArgs e)
         {
             
             string v1 = listView1.SelectedItems[0].SubItems[0].Text.ToString();/*get selectitem value*/
             click(v1);
             
         }

         private void click(string NODE_NAME)
         {
             listView2.Items.Clear();
             string id = bc.getOnlyString("SELECT NODEID FROM RIGHTLIST WHERE NODE_NAME='" +NODE_NAME  + "'");
             SHOW_TREEVIEW_O(id);

             foreach (ListViewItem lvi in listView1.Items)
             {
                 if (lvi.Selected)
                 {
                     lvi.BackColor = c3;
                     pictureBox1.Focus();/*SELECTED AFTER MOVE FOCUS*/
                 }
                 else
                 {
                  
                     lvi.BackColor = c2;
                 }

             }

         }

         private void listView2_Click(object sender, EventArgs e)
         {
         string v1 = listView2.SelectedItems[0].SubItems[0].Text.ToString();/*get selectitem value*/

         #region v1
         if (v1 == "品号信息维护")
        {

            C23.BomManage.frmWareInfo FRM = new C23.BomManage.frmWareInfo();
            FRM.ShowDialog();

        }
         else if (v1 == "品号维护管理用")
         {
             C23.BomManage.frmWareInfoP FRM = new C23.BomManage.frmWareInfoP();
             FRM.ShowDialog();

         }
        else if (v1 == "员工信息维护")
        {
            C23.EmployeeManage.FrmEmployeeInfo FRM = new C23.EmployeeManage.FrmEmployeeInfo();
            FRM.IDO = cemplyee_info.GETID();
            FRM.ShowDialog();

        }

        else if (v1 == "工艺信息维护")
        {
            C23.BomManage.FrmCraft FRM = new C23.BomManage.FrmCraft();
            FRM.ShowDialog();

        }
        else if (v1 == "工种信息维护")
        {
            C23.BomManage.FrmWorker FRM = new C23.BomManage.FrmWorker();
            FRM.ShowDialog();

        }
        else if (v1 == "工艺工种工价信息维护")
        {
            C23.BomManage.FrmCraftAndWorker FRM = new C23.BomManage.FrmCraftAndWorker();
            FRM.ShowDialog();

        }
        else if (v1 == "工作组信息维护")
        {
            C23.BomManage.FrmWorkGroup FRM = new C23.BomManage.FrmWorkGroup();
            FRM.ShowDialog();

        }
        else if (v1 == "工作组成员信息维护")
        {
            C23.BomManage.FrmWorkGroupM FRM = new C23.BomManage.FrmWorkGroupM();
            FRM.ShowDialog();

        }
        else if (v1 == "单据性质维护")
        {
            C23.WorkOrderManage.FrmVoucherProterties FRM = new C23.WorkOrderManage.FrmVoucherProterties();
            FRM.ShowDialog();

        }
        else if (v1 == "工单维护作业")
        {
            C23.WorkOrderManage.FrmWorkOrder FRM = new C23.WorkOrderManage.FrmWorkOrder();
            FRM.ShowDialog();

        }
        else if (v1 == "录入委外加工领料单")
        {
            C23.StorageManage.FrmOutSourcingMateRe FRM = new C23.StorageManage.FrmOutSourcingMateRe();
            FRM.ShowDialog();

        }
        else if (v1 == "录入委外加工入库单")
        {
            C23.StorageManage.FrmOutSourcingGodE FRM = new C23.StorageManage.FrmOutSourcingGodE();
            FRM.ShowDialog();

        }
        else if (v1 == "录入委外加工费单价")
        {
            C23.StorageManage.FrmOutSourcingUnitPrice FRM = new C23.StorageManage.FrmOutSourcingUnitPrice();
            FRM.ShowDialog();

        }
        else if (v1 == "委外加工费查询")
        {
            C23.StorageManage.FrmOSProcessCost FRM = new C23.StorageManage.FrmOSProcessCost();
            FRM.ShowDialog();

        }
        else if (v1 == "仓库信息维护")
        {
            C23.StorageManage.frmStorageInfo FRM = new C23.StorageManage.frmStorageInfo();
            FRM.ShowDialog();

        }
        else if (v1 == "杂项领料作业")
        {
            C23.StorageManage.FrmMiscMateRe FRM = new C23.StorageManage.FrmMiscMateRe();
            FRM.ShowDialog();

        }
        else if (v1 == "杂项入库作业")
        {
            C23.StorageManage.FrmMiscGodE FRM = new C23.StorageManage.FrmMiscGodE();
            FRM.ShowDialog();

        }
        else if (v1 == "库存状况查询")
        {
            C23.StorageManage.frmStorageCase FRM = new C23.StorageManage.frmStorageCase();
            FRM.ShowDialog();

        }
        else if (v1 == "录入计件工资入库单")
        {
            C23.StorageManage.FrmPWGodE FRM = new C23.StorageManage.FrmPWGodE();
            FRM.ShowDialog();

        }
        else if (v1 == "查询入库计件工资明细")
        {
            C23.StorageManage.FrmPieceWages FRM = new C23.StorageManage.FrmPieceWages();
            FRM.ShowDialog();

        }
        else if (v1 == "客户信息维护")
        {
            C23.SellManage.FrmCustomerInfo FRM = new C23.SellManage.FrmCustomerInfo();
            FRM.ShowDialog();

        }
        else if (v1 == "录入销售核价单")
        {
            C23.SellManage.FrmSellUnitPrice FRM = new C23.SellManage.FrmSellUnitPrice();
            FRM.ShowDialog();

        }
        else if (v1 == "录入客户订单")
        {
            C23.SellManage.FrmOrder FRM = new C23.SellManage.FrmOrder();
            FRM.ShowDialog();

        }

        else if (v1 == "录入销货单")
        {
            C23.SellManage.FrmSellTable FRM = new C23.SellManage.FrmSellTable();
            FRM.ShowDialog();

        }
        else if (v1 == "录入销退单")
        {

            //FRM.ShowDialog();

        }
         else if (v1 =="套件信息维护")
        {
           
            C23.BomManage.FrmExternalM frm = new C23.BomManage.FrmExternalM();
            frm.ShowDialog();

        }
         else if (v1 == "型号信息维护")
         {
             C23.BomManage.FrmType frm = new C23.BomManage.FrmType();

             frm.ShowDialog() ;

         }
        else if (v1 == "细节信息维护")
        {
            C23.BomManage.FrmDetail FRM = new C23.BomManage.FrmDetail();
            FRM.ShowDialog(); 

        }
         else if (v1 == "皮种信息维护")
         {
             C23.BomManage.FrmLeather frm = new C23.BomManage.FrmLeather();
             frm.Show();

         }
         else if (v1 == "颜色信息维护")
         {
             C23.BomManage.FrmColor frm = new C23.BomManage.FrmColor();
             
             frm.Show();

         }
         else if (v1 == "线色信息维护")
         {
             C23.BomManage.FrmStitchingC frm = new C23.BomManage.FrmStitchingC();
          
             frm.Show();

         }
         else if (v1 == "海棉厚度信息维护")
         {
             C23.BomManage.FrmThickness frm = new C23.BomManage.FrmThickness();
       
             frm.Show();

         }
             else if (v1 == "用户帐户")
              {
                  C23.UserManage.FrmUSER_INFO FRM = new C23.UserManage.FrmUSER_INFO();
                  FRM.IDO = cuser.GETID();
                  FRM.ADD_OR_UPDATE = "ADD";
                  FRM.ShowDialog();

              }
              else if (v1 == "更改密码")
              {
                  C23.UserManage.FrmEditPwd FRM = new C23.UserManage.FrmEditPwd();
                  FRM.ShowDialog();

              }
              else if (v1 == "权限管理")
              {
                  C23.UserManage.FrmEditRight FRM = new C23.UserManage.FrmEditRight();
                  FRM.ShowDialog();

              }
#endregion

         }
  
    }
}
