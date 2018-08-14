using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Craft_RecipeList : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            
           
        }
 
    }
    protected void btnCkAll_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox ck = (CheckBox)GridView1.Rows[i].FindControl("chk");
            ck.Checked = true;
        }
    }
    protected void btnDel_Click(object sender, EventArgs e)//删除还没有完善
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox ck = (CheckBox)GridView1.Rows[i].FindControl("chk");
            if (ck.Checked == true)
            {
                string recipeno = GridView1.Rows[i].Cells[4].Text;
                List<String> commandlist = new List<String>();
                switch (recipeno.Substring(0,5))
                {
                    case "70306":
                        commandlist.Add("update ht_qa_mater_formula set is_del = '1' where formula_code = '" + recipeno + "'");
                        commandlist.Add("delete from ht_pub_aprv_flowinfo where BUSIN_ID = '" + recipeno + "'");
                        opt.TransactionCommand(commandlist);
                        break;
                    case "70307":
                        commandlist.Add("update ht_qa_aux_formula set is_del = '1' where formula_code = '" + recipeno + "'");
                        commandlist.Add("delete from ht_pub_aprv_flowinfo where BUSIN_ID = '" + recipeno + "'");
                        opt.TransactionCommand(commandlist);
                        break;
                    default:
                        commandlist.Add("update ht_qa_coat_formula set is_del = '1' where formula_code = '" + recipeno + "'");
                        commandlist.Add("delete from ht_pub_aprv_flowinfo where BUSIN_ID = '" + recipeno + "'");
                        opt.TransactionCommand(commandlist);
                        break;
                }

            }
           
        }
        bindGrid();
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "updatetree", " window.parent.update();", true);
    }
    protected void bindGrid()
    {
        string query = "select r.formula_code as 配方编码，r.formula_name as 配方名称,r.b_date as 启用时间,r.CREATE_ID as 编辑人员,s.name as 审批状态  from ht_qa_mater_formula r left join ht_inner_aprv_status s on s.id = r.flow_status  where r.prod_code ='" + hdcode.Value + "' and r.is_del ='0' union select r.formula_code as 配方编码，r.formula_name as 配方名称,r.b_date as 启用时间,r.CREATE_ID as 编辑人员,s.name as 审批状态   from ht_qa_aux_formula r left join ht_inner_aprv_status s on s.id = r.flow_status  where r.prod_code = '" + hdcode.Value + "' and r.is_del ='0'  union select r.formula_code as 配方编码，r.formula_name as 配方名称,r.b_date as 启用时间,r.CREATE_ID as 编辑人员,s.name as 审批状态  from ht_qa_coat_formula r left join ht_inner_aprv_status s on s.id = r.flow_status   where r.prod_code = '" + hdcode.Value + "'  and r.is_del ='0'";
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        DataSet data =  opt.CreateDataSetOra(query);
        GridView1.DataSource =data;
        GridView1.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                DataRowView mydrv = data.Tables[0].DefaultView[i];

                ((Label)GridView1.Rows[i].FindControl("labAprv")).Text = mydrv["审批状态"].ToString();

                if (!(mydrv["审批状态"].ToString() == "未提交" || mydrv["审批状态"].ToString() == "未通过"))
                {
                    ((Button)GridView1.Rows[i].FindControl("btnSubmit")).Enabled = false;
                    ((Button)GridView1.Rows[i].FindControl("btnSubmit")).CssClass = "btngrey";

                }
                else
                {
                    ((Button)GridView1.Rows[i].FindControl("btnSubmit")).Enabled = true;
                    ((Button)GridView1.Rows[i].FindControl("btnSubmit")).CssClass = "btn1 auth";

                }
            }
        }
            
    }
    protected void btnGridDetail_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string formula_code = GridView1.DataKeys[rowIndex].Value.ToString();
        if (formula_code.Substring(0, 5) == "70306")
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "tab2Click("+ formula_code + ");", true);
        }
        else if (formula_code.Substring(0, 5) == "70307")
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "tab3Click(" + formula_code + ");", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "tab4Click(" + formula_code + ");", true);
        }

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        bindGrid();
    }

    //查看审批单
    protected void btnFLow_Click(object sender, EventArgs e)
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
            int index = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号                 
            string id = GridView1.DataKeys[index].Value.ToString();
            string mode = "03";
            if (id.Substring(0, 5) == "70306")
                mode = "03";
            else if (id.Substring(0, 5) == "70307")
                mode = "10";
            else
                mode = "11";
            /*启动审批TB_ZT标题,MODULENAME审批类型编码,BUSIN_ID业务数据id,URL 单独登录url*/
            //"TB_ZT", "MODULENAME", "BUSIN_ID",  "URL"
            string[] subvalue = { "配方:" + GridView1.Rows[index].Cells[4].Text, mode, id, Page.Request.UserHostName.ToString() };
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
           string log_message = MSYS.Common.AprvFlow.createApproval(subvalue) ? "提交审批成功," : "提交审批失败，";
           log_message += "业务数据ID：" + id;
           opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);

            bindGrid();

        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }


}