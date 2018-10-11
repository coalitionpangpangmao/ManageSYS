﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
public partial class Product_StorageAux : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            txtStart.Text = System.DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            txtStop.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listApt, "select F_CODE,F_NAME from ht_svr_org_group", "F_NAME", "F_CODE");
            opt.bindDropDownList(listPrdct, "select prod_code,prod_name from ht_pub_prod_design where is_valid = '1' and is_del = '0'", "PROD_NAME", "PROD_CODE");
            opt.bindDropDownList(listPrdctPlan, "select PLAN_NO from ht_prod_month_plan_detail where EXE_STATUS < '4' and is_DEL = '0'", "PLAN_NO", "PLAN_NO");
            
            bindGrid1();
           
        }
 
    }
    protected void bindGrid1()
    {
       
            string query = "select g.out_date as 领退日期,g.strg_type as 出入库类型，g.order_sn as 单据号 ,g.ware_house as 仓库,g.audit_mark as 状态,g.creator as 编制人,g.issuer as 收发人  from HT_STRG_AUX g   where g.out_date between '" + txtStart.Text + "' and '" + txtStop.Text + "'  and IS_DEL = '0'";
            if (rdOut1.Checked)
                query += " and g.strg_type = '0'";
            else
                query += " and g.strg_type = '1'";
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            GridView1.DataSource = opt.CreateDataSetOra(query); ;
            GridView1.DataBind();
       
    }//绑定gridview1数据源
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid1();
    }
    protected void bindGrid2()
    {

        string query = " select g.mater_code as   辅料编码,g.mater_name as  辅料名称,g.num as 件数,g1.SPEC_VAL as 规格,g.num* to_number( g1.spec_val) as   领料量,g.unit_code as  计量单位,g.ID  from HT_STRG_AUX_SUB g left join ht_pub_materiel g1 on g1.material_code = g.mater_code  where g.main_code = '" + txtCode.Text + "' and g.IS_DEL = '0'";
       
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView2.DataSource = data;
        GridView2.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {          
            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                ((TextBox)GridView2.Rows[i].FindControl("txtGridUnit")).Text = mydrv["计量单位"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtGridcode")).Text = mydrv["辅料编码"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtGridName")).Text = mydrv["辅料名称"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtGridAmount")).Text = mydrv["领料量"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtNum")).Text = mydrv["件数"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtAvgWeight")).Text = mydrv["规格"].ToString();
               
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
            string[] subvalue = { GridView1.Rows[index].Cells[3].Text, "09", id, Page.Request.UserHostName.ToString() };
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
           string log_message = MSYS.Common.AprvFlow.createApproval(subvalue) ? "提交审批成功," : "提交审批失败，";
           log_message += "业务数据ID：" + id;
           InsertTlog(log_message);

            
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
     protected void btnGridopt_Click(object sender, EventArgs e)//出库，调用接口，变更库存情况
    {
        try
        {
             Button btn = (Button)sender;
            int index =  ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号                 
            string id = GridView1.DataKeys[index].Value.ToString();
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
           opt.UpDateOra("update HT_STRG_AUX set ISSUE_STATUS = '1'  where ORDER_SN = '" + id + "'"); 
            bindGrid1();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
     protected void btnGridview_Click(object sender, EventArgs e)//查看领退明细
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        txtCode.Text = GridView1.DataKeys[rowIndex].Value.ToString();      
        
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
         DataSet data = opt.CreateDataSetOra("select * from HT_STRG_AUX  where ORDER_SN =  '" +  txtCode.Text + "'");
         if(data != null && data.Tables[0].Rows.Count > 0)
         {
             txtPrdctdate.Text = data.Tables[0].Rows[0]["OUT_DATE"].ToString(); 
             listStatus.SelectedValue = data.Tables[0].Rows[0]["AUDIT_MARK"].ToString();            
             listStorage.SelectedValue = data.Tables[0].Rows[0]["WARE_HOUSE"].ToString(); 
             listApt.SelectedValue = data.Tables[0].Rows[0]["DEPT_ID"].ToString();
             listPrdctPlan.SelectedValue = data.Tables[0].Rows[0]["MONTHPLANNO"].ToString();
             listPrdct.SelectedValue = opt.GetSegValue("select Prod_code from ht_prod_month_plan_detail where plan_no = '" + listPrdctPlan.SelectedValue + "'", "PROD_CODE");             
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
        try
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked)
                {
                    string order_sn = GridView1.DataKeys[i].Value.ToString();
                    string query = "update HT_STRG_AUX set IS_DEL = '1'  where ORDER_SN = '" + order_sn + "'";
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
     protected void btnGridNew_Click(object sender, EventArgs e)// 新建领退明细
    {

        setBlank();
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        txtCode.Text = "SA" + System.DateTime.Now.ToString("yyyyMMdd") + (Convert.ToInt16(opt.GetSegValue("select count(ORDER_SN) as ordernum from HT_STRG_AUX where substr(ORDER_SN,1,10) ='SM" + System.DateTime.Now.ToString("yyyyMMdd") + "'", "ordernum")) + 1).ToString().PadLeft(3, '0');       
       
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
         listStatus.SelectedValue = "0";
         listStorage.SelectedValue = "0";
         listApt.SelectedValue = "";
         listPrdctPlan.SelectedValue = "";        
     }

     protected void btnModify_Click(object sender, EventArgs e)
     {
        MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        List<string> commandlist = new List<string>();
        string log_message;
         if (rdOut.Checked)
         {
             //生成领用主表记录
             string[] seg = { "OUT_DATE", "ORDER_SN", "EXPIRED_DATE", "MODIFY_TIME", "AUDIT_MARK", "WARE_HOUSE", "WARE_HOUSE_ID", "DEPT_NAME", "DEPT_ID", "CREATOR", "CREATOR_ID", "MONTHPLANNO", "STRG_TYPE" };
             string[] value = { txtPrdctdate.Text, txtCode.Text, txtValiddate.Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), listStatus.SelectedValue, listStorage.SelectedItem.Text, listStorage.SelectedValue, listApt.SelectedItem.Text, listApt.SelectedValue, "cookieName", "cookieID", listPrdctPlan.SelectedValue, "0" };
             commandlist.Add(opt.InsertDatastr(seg, value, "HT_STRG_AUX"));
            
             //根据生产计划对应的配方明细生成原料领用明细
             string query = "insert into HT_STRG_AUX_SUB  select distinct '' as ID, g3.mater_code ,g4.material_name ,g4.data_origin_flag as STORAGE,'' as Remark,g4.unit_code , '0' as IS_DEL,'" + txtCode.Text + "' as MAIN_CODE, 0 as NUM from ht_prod_month_plan_detail g1 left join ht_pub_prod_design g2 on g1.prod_code = g2.prod_code left join ht_qa_Aux_formula_detail g3 on g3.formula_code = g2.mater_formula_code left join ht_pub_materiel g4 on g4.material_code = g3.mater_code where g1.plan_no = '''" + listPrdctPlan.SelectedValue + "' and g4.data_origin_flag = '" + listStorage.SelectedValue + "'";
             commandlist.Add(query);

             log_message = opt.TransactionCommand(commandlist) == "Success" ? "生成辅料领用计划成功" : "生成辅料领用计划失败";
             log_message += "--详情:" + string.Join(",", value);
             InsertTlog(log_message);
             
         }
         if (rdIn.Checked)
         {
             //生成入库主表记录
             string[] seg = { "OUT_DATE", "ORDER_SN", "EXPIRED_DATE", "MODIFY_TIME", "AUDIT_MARK", "WARE_HOUSE", "WARE_HOUSE_ID", "DEPT_NAME", "DEPT_ID", "CREATOR", "CREATOR_ID", "STRG_TYPE" };
             string[] value = { txtPrdctdate.Text, txtCode.Text, txtValiddate.Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), listStatus.SelectedValue, listStorage.SelectedItem.Text, listStorage.SelectedValue, listApt.SelectedItem.Text, listApt.SelectedValue, "cookieName", "cookieID", "1" };

             log_message = opt.InsertData(seg, value, "HT_STRG_AUX") == "Success" ? "生成入库主表记录成功" : "生成入库主表记录失败";
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
            string query = " select g.mater_code as   辅料编码,g.mater_name as  辅料名称,g.num as 件数,g1.SPEC_VAL as 规格,g.num* to_number( g1.spec_val) as   领料量,g.unit_code as  计量单位,g.ID  from HT_STRG_AUX_SUB g left join ht_pub_materiel g1 on g1.material_code = g.mater_code   where g.main_code = '" + txtCode.Text + "'";
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            DataSet set = opt.CreateDataSetOra(query);
            DataTable data = new DataTable();
            if (set != null && set.Tables[0].Rows.Count >0)
                data = set.Tables[0];
           
            else
            {
                data.Columns.Add("辅料编码");
                data.Columns.Add("辅料名称");
                data.Columns.Add("件数");
                data.Columns.Add("规格");
                data.Columns.Add("领料量");
                data.Columns.Add("计量单位");
                data.Columns.Add("ID");
            }
            object[] value = { "", "", 0,"",0, "",0 };
            data.Rows.Add(value);
            GridView2.DataSource = data;
            GridView2.DataBind();
            if (data != null && data.Rows.Count > 0)
            {
                 for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                DataRowView mydrv = data.DefaultView[i];
                
                ((TextBox)GridView2.Rows[i].FindControl("txtGridUnit")).Text = mydrv["计量单位"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtGridcode")).Text = mydrv["辅料编码"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtGridName")).Text = mydrv["辅料名称"].ToString();
                 ((TextBox)GridView2.Rows[i].FindControl("txtGridAmount")).Text = mydrv["领料量"].ToString();
                 ((TextBox)GridView2.Rows[i].FindControl("txtNum")).Text = mydrv["件数"].ToString();
                 ((TextBox)GridView2.Rows[i].FindControl("txtAvgWeight")).Text = mydrv["规格"].ToString();
               
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
                    string query = "update HT_STRG_AUX_SUB set IS_DEL = '1'  where ID = '" + ID + "'";
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
   
    protected void btnGrid2Save_Click(object sender, EventArgs e)
    {
        try
        {
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            if (txtCode.Text == "")
                txtCode.Text = "SA" + System.DateTime.Now.ToString("yyyyMMdd") + (Convert.ToInt16(opt.GetSegValue("select count(ORDER_SN) as ordernum from HT_STRG_AUX where substr(ORDER_SN,1,10) ='SM" + System.DateTime.Now.ToString("yyyyMMdd") + "'", "ordernum")) + 1).ToString().PadLeft(3, '0');
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号             
            string ID = GridView2.DataKeys[Rowindex].Value.ToString();
            if (ID == "0")
            {
                ID = opt.GetSegValue("select STRGAUX_ID_SEQ.nextval as id from dual", "ID");      
            }
            string[] seg = {"ID", "MATER_CODE", "MATER_NAME", "STORAGE", "UNIT_CODE", "NUM", "MAIN_CODE" };
            string[] value = {ID, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtGridcode")).Text, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtGridName")).Text, listStorage.SelectedValue, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtGridUnit")).Text, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtNum")).Text, txtCode.Text };
           
            string log_message = opt.MergeInto(seg, value, 1, "HT_STRG_AUX_SUB") == "Success" ? "保存辅料领退明细成功" : "保存辅料领退明细失败";
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