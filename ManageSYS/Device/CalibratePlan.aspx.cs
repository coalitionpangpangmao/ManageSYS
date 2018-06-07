using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
public partial class Device_CalibratePlan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStart.Text = System.DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd");
            txtStop.Text = System.DateTime.Now.AddDays(15).ToString("yyyy-MM-dd");
           DataBaseOperator opt =new DataBaseOperator();
            opt.bindDropDownList(listEditor, "select ID,name  from ht_svr_user t where role = ''", "name", "ID");
            opt.bindDropDownList(listApt, "select f_code,f_name  from ht_svr_org_group where F_role = '' ", "f_name", "f_code");
            opt.bindDropDownList(listModel, "select pz_code,mt_name from HT_EQ_MCLBR_PLAN where is_model = '1' and is_del = '0'", "mt_name", "pz_code");
            opt.bindDropDownList(listdspcth, "select ID,name  from ht_svr_user ", "name", "ID");
            bindGrid1();
           
        }
 
    }
    protected void bindGrid1()
    {
        try
        {
            string query = "select t.mt_name as 校准计划,(case t.flow_status when '-1' then '未提交' when '0' then '办理中' when '1' then '未通过' else '己通过' end) as 审批状态,(case t.TASK_STATUS when '0' then '未执行' when '1' then '执行中' when '2' then '己完成' else '己过期' end) as 执行状态,t.remark as 备注,t.pz_code from HT_EQ_MCLBR_PLAN t left join ht_svr_org_group t1 on t1.f_code = t.create_dept_id   where t.expired_date between '" + txtStart.Text + "' and '" + txtStop.Text + "'  and t.IS_DEL = '0'";
          
           DataBaseOperator opt =new DataBaseOperator();
            GridView1.DataSource = opt.CreateDataSetOra(query); ;
            GridView1.DataBind();
        }
        catch (Exception ee)
        {

        }

    }//绑定gridview1数据源
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid1();
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
                    string query = "update HT_EQ_MCLBR_PLAN set IS_DEL = '1'  where PZ_CODE = '" + order_sn + "'";
                   DataBaseOperator opt =new DataBaseOperator();
                    opt.UpDateOra(query);
                }
            }
            bindGrid1();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnGridNew_Click(object sender, EventArgs e)
    {
         setBlank();
         DataBaseOperator opt =new DataBaseOperator();
          txtCode.Text = "CL" + System.DateTime.Now.ToString("yyyyMMdd") + (Convert.ToInt16(opt.GetSegValue("select nvl( max(substr(pz_code,11,3)),0) as ordernum from HT_EQ_MCLBR_PLAN where substr(pz_code,1,10) ='MT" + System.DateTime.Now.ToString("yyyyMMdd") + "'", "ordernum")) + 1).ToString().PadLeft(3, '0');       
       
          ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "GridClick();", true);
        // this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "", "<script>GridClick();</script>", true);
    }

    protected void btnGridIssue_Click(object sender, EventArgs e)//查看审批流程
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string ID = GridView1.DataKeys[rowIndex].Value.ToString();
        string query = "select pos as 顺序号, workitemid as 审批环节,username as 负责人,comments as 意见,opiniontime 审批时间,(case status when '0' then '未审批'  when '1' then '未通过' else '己通过' end)  as 审批状态  from ht_pub_aprv_opinion r left join ht_pub_aprv_flowinfo s on r.gongwen_id = s.id where s.busin_id  = '" + ID + "' order by pos";
       DataBaseOperator opt =new DataBaseOperator();
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
            string[] subvalue = { GridView1.Rows[index].Cells[1].Text, "14", id, Page.Request.UserHostName.ToString() };
           DataBaseOperator opt =new DataBaseOperator();
            if (opt.createApproval(subvalue))
            {
                opt.UpDateOra("update HT_EQ_MCLBR_PLAN set FLOW_STATUS = '0'  where PZ_CODE = '" + id + "'"); 
            }
            
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
  
     protected void btnGridview_Click(object sender, EventArgs e)//查看明细
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        txtCode.Text = GridView1.DataKeys[rowIndex].Value.ToString();      
        
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra("select * from HT_EQ_MCLBR_PLAN  where PZ_CODE =  '" + txtCode.Text + "'");
         if(data != null && data.Tables[0].Rows.Count > 0)
         {
             DataRow row = data.Tables[0].Rows[0];
             txtName.Text = row["MT_NAME"].ToString();
             listEditor.SelectedValue = row["CREATE_ID"].ToString();
             listApt.SelectedValue = row["CREATE_DEPT_ID"].ToString();
             txtExptime.Text = row["EXPIRED_DATE"].ToString();
             txtdscrpt.Text = row["REMARK"].ToString();
             ckModel.Checked = ("1" == row["IS_MODEL"].ToString());
              bindGrid2(txtCode.Text);
         }
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", "GridClick();", true);     
    }
  
/// <summary>
/// /tab2
/// </summary>
     protected void setBlank()
     {
         txtName.Text = "";
         txtCode.Text = "";
         listEditor.SelectedValue = "";
         listApt.SelectedValue = "";
         txtExptime.Text = "";
         txtdscrpt.Text = "";
         ckModel.Checked = false;
     }

     protected DataSet sectionbind()
     {
        DataBaseOperator opt =new DataBaseOperator();
         return opt.CreateDataSetOra("select section_code,section_name from ht_pub_tech_section where is_del = '0' and is_valid = '1'");
     }

     protected void listGridsct_SelectedIndexChanged(object sender, EventArgs e)
     {
         DropDownList list = (DropDownList)sender;
         GridViewRow row = (GridViewRow)list.NamingContainer;
         int rowindex = row.RowIndex;
         DropDownList list1 = (DropDownList)row.FindControl("listGridEq");
        DataBaseOperator opt =new DataBaseOperator();
         opt.bindDropDownList(list1, "select IDKEY,EQ_NAME  from ht_eq_eqp_tbl where section_code = '" + list.SelectedValue + "'", "EQ_NAME", "IDKEY");
     }
     protected void bindGrid2(string code )
     {

         string query = "select t.section as 工段,t.equipment_id as 设备名称,t.point as 数据点,t.exp_finish_time as 期望完成时间,t.STATUS as 状态,t.remark as 备注 ,t.ID  from HT_EQ_MCLBR_PLAN_detail  t where t.main_id = '" + code + "' and t.is_del = '0'";

        DataBaseOperator opt =new DataBaseOperator();
         DataSet data = opt.CreateDataSetOra(query);
         GridView2.DataSource = data;
         GridView2.DataBind();
         if (data != null && data.Tables[0].Rows.Count > 0)
         {
             for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
             {
                 DataRowView mydrv = data.Tables[0].DefaultView[i];
                 ((DropDownList)GridView2.Rows[i].FindControl("listGridsct")).SelectedValue = mydrv["工段"].ToString();
                 ((DropDownList)GridView2.Rows[i].FindControl("listGridEq")).SelectedValue = mydrv["设备名称"].ToString();
                 ((TextBox)GridView2.Rows[i].FindControl("listGridPoint")).Text = mydrv["数据点"].ToString();

                 ((TextBox)GridView2.Rows[i].FindControl("listGrid2Status")).Text = mydrv["期望完成时间"].ToString();
                 ((DropDownList)GridView2.Rows[i].FindControl("listGrid2Status")).SelectedValue = mydrv["状态"].ToString();
                 ((TextBox)GridView2.Rows[i].FindControl("txtGridremark")).Text = mydrv["备注"].ToString();

             }

         }

     }//绑定GridView2数据源
    protected DataSet eqbind()
    {
       DataBaseOperator opt =new DataBaseOperator();
        return opt.CreateDataSetOra("select IDKEY,EQ_NAME from ht_eq_eqp_tbl where is_del = '0' and is_valid = '1'");
    }
   
      protected void btnCkAll_Click(object sender, EventArgs e)//全选
    {
        try
        {
            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                ((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked = true;
            }
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
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
                    string query = "update HT_EQ_MCLBR_PLAN_detail set IS_DEL = '1'  where ID = '" + ID + "'";
                   DataBaseOperator opt =new DataBaseOperator();
                    opt.UpDateOra(query);
                }
            }
            bindGrid2(txtCode.Text);
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)//
    {
        try
        {
            string query = "select t.section as 工段,t.equipment_id as 设备名称,t.point as 数据点,t.exp_finish_time as 期望完成时间,t.STATUS as 状态,t.remark as 备注 ,t.ID  from HT_EQ_MCLBR_PLAN_detail t  where t.main_id = '" + txtCode.Text + "' and t.is_del = '0'";
           DataBaseOperator opt =new DataBaseOperator();
            DataSet set = opt.CreateDataSetOra(query);
            DataTable data = new DataTable();
            if (set == null)
            {
                data.Columns.Add("区域");
                data.Columns.Add("设备名称");
                data.Columns.Add("维保原因");
                data.Columns.Add("维保内容");
                data.Columns.Add("期望完成时间");
                data.Columns.Add("状态");
                data.Columns.Add("备注");
                data.Columns.Add("ID");
            }
            else
                data = set.Tables[0];
            object[] value = { "", "", "","","","", "",0 };
            data.Rows.Add(value);
            GridView2.DataSource = data;
            GridView2.DataBind();
            if (data != null && data.Rows.Count > 0)
            {
                for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
                {
                    DataRowView mydrv = data.DefaultView[i];
                    ((DropDownList)GridView2.Rows[i].FindControl("listGridsct")).SelectedValue = mydrv["工段"].ToString();
                    ((DropDownList)GridView2.Rows[i].FindControl("listGridEq")).SelectedValue = mydrv["设备名称"].ToString();
                    ((DropDownList)GridView2.Rows[i].FindControl("listGridPoint")).SelectedValue = mydrv["数据点"].ToString();

                    ((TextBox)GridView2.Rows[i].FindControl("listGrid2Status")).Text = mydrv["期望完成时间"].ToString();
                    ((DropDownList)GridView2.Rows[i].FindControl("listGrid2Status")).SelectedValue = mydrv["状态"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("txtGridremark")).Text = mydrv["备注"].ToString();
                 
                }
            }
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnDspcth_Click(object sender, EventArgs e)//
    {
        try
        {
            bool ck = false;
            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked)
                {
                    ck = true;
                    string ID = GridView2.DataKeys[i].Value.ToString();
                    string query = "update HT_EQ_MCLBR_PLAN_detail set STATUS = '1' ,RESPONER = '" + listdspcth.SelectedValue + "' where ID = '" + ID + "'";
                   DataBaseOperator opt =new DataBaseOperator();
                    opt.UpDateOra(query);                  
                }
            }
            ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", "$('#dspcthor').hide();", true);
            if(ck == true)
            bindGrid2(txtCode.Text);
            else
                ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", "alert('请选择派工任务');", true);
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)//
    {
        try
        {
           DataBaseOperator opt =new DataBaseOperator();
            opt.UpDateOra("delete from HT_EQ_MCLBR_PLAN where PZ_CODE = '" + txtCode.Text + "'");          
            string[] seg = { "MT_NAME","PZ_CODE","CREATE_ID","CREATE_DEPT_ID","EXPIRED_DATE","REMARK","IS_MODEL","CREATE_TIME"};
            string[] value = {  txtName.Text , txtCode.Text , listEditor.SelectedValue , listApt.SelectedValue ,txtExptime.Text , txtdscrpt.Text ,Convert.ToInt16(ckModel.Checked).ToString(),System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
            opt.InsertData(seg, value, "HT_EQ_MCLBR_PLAN");
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void btnGrid2Save_Click(object sender, EventArgs e)//
    {
        try
        {           
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int rowIndex = row.RowIndex;
            string id = GridView2.DataKeys[rowIndex].Value.ToString();
           DataBaseOperator opt =new DataBaseOperator();
            opt.UpDateOra("delete from HT_EQ_MCLBR_PLAN_detail where ID = '" + id + "'");
            string[] seg = { "section", "equipment_id", "point", "exp_finish_time", "remark", "CREATE_TIME", "MAIN_ID" };
         
            string[] value = { ((DropDownList)row.FindControl("listGridsct")).SelectedValue, ((DropDownList)row.FindControl("listGridEq")).SelectedValue, ((DropDownList)row.FindControl("listGridPoint")).SelectedValue,  ((TextBox)row.FindControl("txtGridExptime")).Text, ((TextBox)row.FindControl("txtGridremark")).Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), txtCode.Text };
            opt.InsertData(seg, value, "HT_EQ_MCLBR_PLAN_detail");   
             
          
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
       DataBaseOperator opt =new DataBaseOperator();
        string query = "select * from HT_EQ_MCLBR_PLAN_detail where MAIN_ID = '" + listModel.SelectedValue + "' and is_del = '0'";
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in data.Tables[0].Rows)
            {              
                string[] seg = { "section", "equipment_id", "point", "exp_finish_time", "remark", "CREATE_TIME", "MAIN_ID" };
                string[] value = { row["section"].ToString(), row["equipment_id"].ToString(), row["point"].ToString(),System.DateTime.Now.AddMonths(1).ToString("yyyy-MM") + "-01", row["remark"].ToString(), System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), txtCode.Text };
                opt.InsertData(seg, value, "HT_EQ_MCLBR_PLAN_detail");

            }

        }
        bindGrid2(txtCode.Text);
    }
    
}