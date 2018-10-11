using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
public partial class Product_StorageMater : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            initView();
           
        }
 
    }
    protected void initView()
    {
        txtStart.Text = System.DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
        txtStop.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listApt, "select F_CODE,F_NAME from ht_svr_org_group", "F_NAME", "F_CODE");
        opt.bindDropDownList(listPrdct, "select prod_code,prod_name from ht_pub_prod_design where is_valid = '1' and is_del = '0'", "PROD_NAME", "PROD_CODE");
        opt.bindDropDownList(listPrdctPlan, "select PLAN_NO from ht_prod_month_plan_detail where EXE_STATUS < '4' and is_DEL = '0'", "PLAN_NO", "PLAN_NO");
        opt.bindDropDownList(listStorage, "select * from ht_inner_mat_depot", "NAME", "ID");
        // opt.bindDropDownList(listStatus, "select ID,ISSUE_NAME from HT_INNER_BOOL_DISPLAY ", "ISSUE_NAME", "ID");
        opt.bindDropDownList(listStatus, "select * from ht_inner_aprv_status ", "NAME", "ID");
        opt.bindDropDownList(listCreator, "select ID,Name from ht_svr_user where is_del = '0'", "Name", "ID");
        bindGrid1();
    }
    protected void bindGrid1()
    {
        string query = "select g.out_date as 领退日期,r.strg_name as 出入库类型，t.ISSUE_Name as 下发状态,g.order_sn as 单据号 ,g1.YG as 烟梗总量,g2.SP as 碎片总量,k.name as 仓库,s.name as 审批状态,h.name as 编制人,j.name as 收发人  from ht_strg_materia g left join (select main_code, sum(original_demand) as yg from ht_strg_mater_sub where main_code = '' and mater_flag = 'YG' group by main_code ) g1 on g1.main_code = g.order_sn left join (select main_code, sum(original_demand) as sp from ht_strg_mater_sub where main_code = '' and mater_flag = 'SP' group by main_code ) g2 on g2.main_code = g.order_sn left join HT_INNER_BOOL_DISPLAY r on r.id = g.strg_type left join ht_inner_Aprv_status s on s.id = g.audit_mark left join HT_INNER_BOOL_DISPLAY t on t.id = g.issue_status left join ht_svr_user h on h.id = g.creator_id left join ht_svr_user j on j.id = g.issuer_id left join ht_inner_mat_depot k on k.id = g.ware_house_id  where g.out_date between '" + txtStart.Text + "' and '" + txtStop.Text + "'  and g.IS_DEL = '0'";
            if (rdOut1.Checked)
                query += " and g.strg_type = '0'";
            else
                query += " and g.strg_type = '1'";
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
           DataSet data = opt.CreateDataSetOra(query);
           GridView1.DataSource = data;
            GridView1.DataBind();
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
                {
                    DataRowView mydrv = data.Tables[0].DefaultView[i];
                    ((Label)GridView1.Rows[i].FindControl("labStrg")).Text = mydrv["出入库类型"].ToString();
                    ((Label)GridView1.Rows[i].FindControl("labAudit")).Text = mydrv["审批状态"].ToString();
                    ((Label)GridView1.Rows[i].FindControl("labIssue")).Text = mydrv["下发状态"].ToString();
                    if (mydrv["审批状态"].ToString() != "未提交")
                        ((Button)GridView1.Rows[i].FindControl("btnSubmit")).Enabled = false;
                    if (mydrv["下发状态"].ToString() != "未下发")
                        ((Button)GridView1.Rows[i].FindControl("btnGridopt")).Enabled = false; 
                    
                   

                }

            }
      
    }//绑定gridview1数据源
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid1();
    }
    protected void bindGrid2()
    {

        string query = " select STORAGE as  仓库,mater_flag as   类型 ,unit_code as  计量单位,mater_code as   原料编码,mater_name as  原料名称,original_demand as   领料量,ID  from ht_strg_mater_sub where main_code = '" + txtCode.Text + "' and IS_DEL = '0'";
       
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView2.DataSource = data;
        GridView2.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {          
            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                DataRowView mydrv = data.Tables[0].DefaultView[i];                
                ((DropDownList)GridView2.Rows[i].FindControl("listGridstrg")).SelectedValue = mydrv["仓库"].ToString();
                 ((DropDownList)GridView2.Rows[i].FindControl("listGridtype")).SelectedValue = mydrv["类型"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtGridUnit")).Text = mydrv["计量单位"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtGridcode")).Text = mydrv["原料编码"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtGridName")).Text = mydrv["原料名称"].ToString();
                 ((TextBox)GridView2.Rows[i].FindControl("txtGridAmount")).Text = mydrv["领料量"].ToString();
               
            }

        }

    }//绑定GridView2数据源

    protected void btnGridIssue_Click(object sender, EventArgs e)//查看审批流程
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string ID = GridView1.DataKeys[rowIndex].Value.ToString();
        string query = "select pos as 顺序号, workitemid as 审批环节,username as 负责人,comments as 意见,opiniontime 审批时间,(case status when '0' then '未审批'  when '1' then '未通过' else '己通过' end)  as 审批状态  from ht_pub_aprv_opinion r left join ht_pub_aprv_flowinfo s on r.gongwen_id = s.id where s.busin_id  = '" + ID + "' order by pos";
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        GridView3.DataSource = opt.CreateDataSetOra(query);
        GridView3.DataBind();
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "Aprvlist();", true);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)//提交审批
    {
        try
        {
            Button btn = (Button)sender;
            int index =  ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号                 
            string id = GridView1.DataKeys[index].Value.ToString();
                    /*启动审批TB_ZT标题,TBR_ID填报人id,TBR_NAME填报人name,TB_BM_ID填报部门id,TB_BM_NAME填报部门name,TB_DATE申请时间创建日期,MODULENAME审批类型编码,URL 单独登录url,BUSIN_ID业务数据id*/
            string[] subvalue = { "仓储" + ((Label)GridView1.Rows[index].FindControl("labStrg")).Text + id , "08", id, Page.Request.UserHostName.ToString() };
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            if (MSYS.Common.AprvFlow.createApproval(subvalue))
            {
                opt.UpDateOra("update HT_STRG_MATERIA set AUDIT_MARK = '0'  where ORDER_SN = '" + id + "'"); 
            }
            bindGrid1();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
     protected void btnGridopt_Click(object sender, EventArgs e)//出库，调用接口，变更库存情况
    {
      
             Button btn = (Button)sender;
            int index =  ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号                 
            string id = GridView1.DataKeys[index].Value.ToString();
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
           opt.UpDateOra("update HT_STRG_MATERIA set ISSUE_STATUS = '1'  where ORDER_SN = '" + id + "'"); 
            bindGrid1();
      
    }
     protected void btnGridview_Click(object sender, EventArgs e)//查看领退明细
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        txtCode.Text = GridView1.DataKeys[rowIndex].Value.ToString();      
        
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
         DataSet data = opt.CreateDataSetOra("select * from HT_STRG_MATERIA  where ORDER_SN =  '" +  txtCode.Text + "'");
         if(data != null && data.Tables[0].Rows.Count > 0)
         {
             txtPrdctdate.Text = data.Tables[0].Rows[0]["OUT_DATE"].ToString(); 
             listStatus.SelectedValue = data.Tables[0].Rows[0]["AUDIT_MARK"].ToString();            
             listStorage.SelectedValue = data.Tables[0].Rows[0]["WARE_HOUSE_ID"].ToString(); 
             listApt.SelectedValue = data.Tables[0].Rows[0]["DEPT_ID"].ToString();
             listPrdctPlan.SelectedValue = data.Tables[0].Rows[0]["MONTHPLANNO"].ToString();
             string temp = opt.GetSegValue("select Prod_code from ht_prod_month_plan_detail where plan_no = '" + listPrdctPlan.SelectedValue + "'", "PROD_CODE");
             listPrdct.SelectedValue = (temp== "NoRecord"? "": temp);
             txtBatchNum.Text = data.Tables[0].Rows[0]["BATCHNUM"].ToString(); 
             if(data.Tables[0].Rows[0]["STRG_TYPE"].ToString() == "0")
                 rdOut.Checked = true;
             else
                 rdIn.Checked = true;
        bindGrid2();
         }
        ScriptManager.RegisterStartupScript(UpdatePanel3, this.Page.GetType(), "", "GridClick();", true);
       // this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "", "<script>GridClick();</script>", true);
    }
     protected void btnGridDel_Click(object sender, EventArgs e)//删除选中记录
    {
      
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked)
                {
                    string order_sn = GridView1.DataKeys[i].Value.ToString();
                    string query = "update HT_STRG_MATERIA set IS_DEL = '1'  where ORDER_SN = '" + order_sn + "'";
                   MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
                    opt.UpDateOra(query);
                }
            }
            bindGrid2();
      
    }
     protected void btnGridNew_Click(object sender, EventArgs e)// 新建领退明细
    {

        setBlank();
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        txtCode.Text = "SM" + System.DateTime.Now.ToString("yyyyMMdd") + (Convert.ToInt16(opt.GetSegValue("select count(ORDER_SN) as ordernum from HT_STRG_MATERIA where substr(ORDER_SN,1,10) ='SM" + System.DateTime.Now.ToString("yyyyMMdd") + "'", "ordernum")) + 1).ToString().PadLeft(3, '0');
         MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
         listCreator.SelectedValue = user.id;
        listApt.SelectedValue = user.OwningBusinessUnitId;
        ScriptManager.RegisterStartupScript(UpdatePanel3, this.Page.GetType(), "", "GridClick();", true);
       // this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "", "<script>GridClick();</script>", true);
    }

     protected void btnReset_Click(object sender, EventArgs e)
     {
         setBlank();
     }
     protected void setBlank()
     {
         txtPrdctdate.Text = "";
         listStatus.SelectedValue = "";
         listStorage.SelectedValue = "";
         listApt.SelectedValue = "";
         listPrdctPlan.SelectedValue = "";
         txtBatchNum.Text = "";
     }

     protected void btnModify_Click(object sender, EventArgs e)
     {
        MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        List<string> commandlist = new List<string>();
        string log_message;
         if (rdOut.Checked)
         {
             //生成领用主表记录
             string[] seg = { "OUT_DATE", "ORDER_SN", "EXPIRED_DATE", "MODIFY_TIME",   "WARE_HOUSE_ID",  "DEPT_ID",  "CREATOR_ID", "MONTHPLANNO", "BATCHNUM", "STRG_TYPE" };
             string[] value = { txtPrdctdate.Text, txtCode.Text, txtValiddate.Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),   listStorage.SelectedValue,  listApt.SelectedValue, listCreator.SelectedValue,  listPrdctPlan.SelectedValue, txtBatchNum.Text, "0" };
           
             commandlist.Add(opt.InsertDatastr(seg, value, "HT_STRG_MATERIA"));
             //根据生产计划对应的配方明细生成原料领用明细
             string query = "insert into HT_STRG_MATER_SUB  select '' as ID, g3.mater_code ,g3.mater_name ,g4.data_origin_flag as STORAGE,g3.batch_size*" + txtBatchNum.Text + " as ORIGINAL_DEMAND, '' as REAL_DEMAND,'' as Remark,g4.unit_code , '0' as IS_DEL,g3.MATER_FLAG,'" + txtCode.Text + "' as MAIN_CODE  from ht_prod_month_plan_detail g1 left join ht_pub_prod_design g2 on g1.prod_code = g2.prod_code left join ht_qa_mater_formula_detail g3 on g3.formula_code = g2.mater_formula_code left join ht_pub_materiel g4 on g4.material_code = g3.mater_code where g1.plan_no = '''" + listPrdctPlan.SelectedValue + "' and g4.data_origin_flag = '" + listStorage.SelectedValue + "'";
             commandlist.Add(query);
             log_message = opt.TransactionCommand(commandlist) == "Success" ? "生成原料领用主表记录成功" : "生成原料领用主表记录失败";
             log_message += "--详情:" + string.Join(",", value);
             InsertTlog(log_message);
         }
         if (rdIn.Checked)
         {
             //生成入库主表记录
             string[] seg = { "OUT_DATE", "ORDER_SN", "EXPIRED_DATE", "MODIFY_TIME", "AUDIT_MARK", "WARE_HOUSE", "WARE_HOUSE_ID", "DEPT_NAME", "DEPT_ID", "CREATOR", "CREATOR_ID", "STRG_TYPE" };
             string[] value = { txtPrdctdate.Text, txtCode.Text, txtValiddate.Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), listStatus.SelectedValue, listStorage.SelectedItem.Text, listStorage.SelectedValue, listApt.SelectedItem.Text, listApt.SelectedValue, "cookieName", "cookieID", "1" };
             
             log_message = opt.InsertData(seg, value, "HT_STRG_MATERIA") == "Success" ? "生成入库主表记录成功" : "生成入库主表记录失败";
             log_message += "--详情:" + string.Join(",", value);
             InsertTlog(log_message);
         }
         bindGrid1();
         bindGrid2();

     }
      protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string query = " select STORAGE as  仓库,mater_flag as   类型 ,unit_code as  计量单位,mater_code as   原料编码,mater_name as  原料名称,original_demand as   领料量,ID  from ht_strg_mater_sub where main_code = '" + txtCode.Text + "'";
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            DataSet set = opt.CreateDataSetOra(query);
            DataTable data = new DataTable();
            if (set != null && set.Tables[0].Rows.Count >0)
                data = set.Tables[0];
           
            else
            {
                data.Columns.Add("仓库");
                data.Columns.Add("类型");
                data.Columns.Add("计量单位");
                data.Columns.Add("原料编码");
                data.Columns.Add("原料名称");
                data.Columns.Add("领料量");
                data.Columns.Add("ID");
            }
            object[] value = { "", "", "","","", 0,0 };
            data.Rows.Add(value);
            GridView2.DataSource = data;
            GridView2.DataBind();
            if (data != null && data.Rows.Count > 0)
            {
                 for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                DataRowView mydrv = data.DefaultView[i];                
                ((DropDownList)GridView2.Rows[i].FindControl("listGridstrg")).SelectedValue = mydrv["仓库"].ToString();
                 ((DropDownList)GridView2.Rows[i].FindControl("listGridtype")).SelectedValue = mydrv["类型"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtGridUnit")).Text = mydrv["计量单位"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtGridcode")).Text = mydrv["原料编码"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtGridName")).Text = mydrv["原料名称"].ToString();
                 ((TextBox)GridView2.Rows[i].FindControl("txtGridAmount")).Text = mydrv["领料量"].ToString();
               
            }

            }
        }
        catch (Exception ee)
        {
            string str = ee.Message;
        }
    }
      protected void btnCkAll_Click(object sender, EventArgs e)//全选
    {
        int ckno = 0;
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            if (((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked)
                ckno++;
        }
        bool check = (ckno < GridView2.Rows.Count);
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            ((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked = check;

        }
    }
    protected void btnDelSel_Click(object sender, EventArgs e)//删除选中记录
    {
        try
        {
            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked)
                {
                    string ID = GridView2.DataKeys[i].Value.ToString();
                    string query = "update HT_STRG_MATER_SUB set IS_DEL = '1'  where ID = '" + ID + "'";
                   MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
                    opt.UpDateOra(query);
                }
            }
            bindGrid2();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnCreate_Click(object sender, EventArgs e)//按明细生成领退单
    {
        try
        {
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            if (rdOut.Checked)
            {
                //生成领用主表记录
                string[] seg = { "OUT_DATE", "ORDER_SN", "EXPIRED_DATE", "MODIFY_TIME","DEPT_NAME", "DEPT_ID", "CREATOR", "CREATOR_ID",  "STRG_TYPE" };
                string[] value = { txtPrdctdate.Text, txtCode.Text, txtValiddate.Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),  listApt.SelectedItem.Text, listApt.SelectedValue, "cookieName", "cookieID", "0" };
                opt.InsertData(seg, value, "HT_STRG_MATERIA");                
               
            }
            if (rdIn.Checked)
            {
                //生成入库主表记录
                string[] seg = { "OUT_DATE", "ORDER_SN", "EXPIRED_DATE", "MODIFY_TIME", "DEPT_NAME", "DEPT_ID", "CREATOR", "CREATOR_ID", "STRG_TYPE" };
                string[] value = { txtPrdctdate.Text, txtCode.Text, txtValiddate.Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), listApt.SelectedItem.Text, listApt.SelectedValue, "cookieName", "cookieID", "1" };
                opt.InsertData(seg, value, "HT_STRG_MATERIA");             
            }
           
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }  
    protected void btnGrid2Save_Click(object sender, EventArgs e)
    {
        try
        {
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            if (txtCode.Text == "")
                txtCode.Text = "SM" + System.DateTime.Now.ToString("yyyyMMdd") + (Convert.ToInt16(opt.GetSegValue("select count(ORDER_SN) as ordernum from HT_STRG_MATERIA where substr(ORDER_SN,1,10) ='SM" + System.DateTime.Now.ToString("yyyyMMdd") + "'", "ordernum")) + 1).ToString().PadLeft(3, '0');
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号             
            string ID = GridView2.DataKeys[Rowindex].Value.ToString();
             if (ID == "0")
            {
                ID = opt.GetSegValue("select STRGMATER_ID_SEQ.nextval as id from dual", "ID"); 
            }
            string[] seg = {"ID", "STORAGE", "MATER_FLAG", "unit_code", "mater_code", "mater_name", "ORIGINAL_DEMAND", "MAIN_CODE" };
            string[] value = {ID, ((DropDownList)GridView2.Rows[Rowindex].FindControl("listGridstrg")).SelectedValue, ((DropDownList)GridView2.Rows[Rowindex].FindControl("listGridtype")).SelectedValue, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtGridUnit")).Text, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtGridcode")).Text, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtGridName")).Text, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtGridAmount")).Text,txtCode.Text };
           
            string log_message = opt.MergeInto(seg, value, 1, "HT_STRG_MATER_SUB") == "Success" ? "保存原料领退明细成功" : "保存原料领退明细失败";
            log_message += "--详情:" + string.Join(",", value);
            InsertTlog(log_message); 
            bindGrid2();            
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void listPrdctPlan_SelectedIndexChanged(object sender, EventArgs e)
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        listPrdct.SelectedValue = opt.GetSegValue("select Prod_code from ht_prod_month_plan_detail where plan_no = '" + listPrdctPlan.SelectedValue + "'", "PROD_CODE");
    }
}