using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Craft_Prdct : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            bindGrid();
            initView();
        }
 
    }
    protected void initView()
    {        
       DataBaseOperator opt =new DataBaseOperator();
        opt.bindDropDownList(listAux, "select Formula_code, FORMULA_NAME from ht_qa_aux_formula where is_valid = '1' and is_del = '0'", "FORMULA_NAME", "Formula_code");
        opt.bindDropDownList(listcoat, "select  Formula_code, FORMULA_NAME  from HT_QA_coat_FORMULA where is_valid = '1' and is_del = '0'", "FORMULA_NAME", "Formula_code");       
        opt.bindDropDownList(listMtrl, "select Formula_code, FORMULA_NAME  from ht_qa_mater_formula where is_valid = '1' and is_del = '0'", "FORMULA_NAME", "Formula_code");
        opt.bindDropDownList(listTechStd, "select distinct tech_code,tech_name from ht_tech_stdd_code where is_valid = '1' and is_del = '0'", "tech_name", "tech_code");
        opt.bindDropDownList(listqlt, "select QLT_CODE,QLT_NAME from ht_qlt_stdd_code where is_del = '0' and is_valid = '1'", "QLT_NAME", "QLT_NAME");
    }
    protected void bindGrid()
    {
        string query = "select PROD_CODE  as 产品编码,PROD_NAME  as 产品名称,PACK_NAME  as 包装规格,HAND_MODE  as 加工方式,is_valid as 是否有效,(case B_FLOW_STATUS when '-1' then '未提交' when '0' then '办理中' when '1' then '未通过' else '己通过' end) as 审批状态 from ht_pub_prod_design where is_del = '0'";
        if (txtCodeS.Text != "")
            query += " and prod_code = '" + txtCodeS.Text + "'";
        if (txtNameS.Text != "")
            query += " and proD_name = '" + txtNameS.Text + "'";
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra(query);
            GridView1.DataSource = data;
            GridView1.DataBind();
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                {
                    DataRowView mydrv = data.Tables[0].DefaultView[i];
                    ((Label)GridView1.Rows[i].FindControl("labGrid1Status")).Text = mydrv["审批状态"].ToString();
                }
            }
   
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid();

    }



    protected void btnGrid1Del_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string prod_code = GridView1.DataKeys[rowIndex].Value.ToString();

        DataBaseOperator opt = new DataBaseOperator();
        opt.UpDateOra("update ht_pub_prod_design set is_del = '1' where PROD_CODE = '" + prod_code + "'");
        bindGrid();
    }
    protected void btnGridDetail_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string prod_code = GridView1.DataKeys[rowIndex].Value.ToString();
        string query = "select * from ht_pub_prod_design where PROD_CODE = '" + prod_code + "' and  is_del = '0'";
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            txtCode.Text = data.Tables[0].Rows[0]["PROD_CODE"].ToString();
            txtName.Text = data.Tables[0].Rows[0]["PROD_NAME"].ToString();
            txtPack.Text = data.Tables[0].Rows[0]["PACK_NAME"].ToString();
           listType.SelectedValue = data.Tables[0].Rows[0]["HAND_MODE"].ToString();
            listTechStd.SelectedValue = data.Tables[0].Rows[0]["TECH_STDD_CODE"].ToString();
            listMtrl.SelectedValue = data.Tables[0].Rows[0]["MATER_FORMULA_CODE"].ToString();
            listAux.SelectedValue = data.Tables[0].Rows[0]["AUX_FORMULA_CODE"].ToString();
            listcoat.SelectedValue = data.Tables[0].Rows[0]["Coat_FORMULA_CODE"].ToString();
            listqlt.SelectedValue = data.Tables[0].Rows[0]["QLT_CODE"].ToString();
            txtValue.Text = data.Tables[0].Rows[0]["STANDARD_VALUE"].ToString();
            txtDscpt.Text = data.Tables[0].Rows[0]["REMARK"].ToString();
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "GridClick();", true);
        UpdatePanel2.Update();

    }
    //查看审批单
    protected void btnFLow_Click(object sender, EventArgs e)
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
            int index = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号                 
            string id = GridView1.DataKeys[index].Value.ToString();
            /*启动审批TB_ZT标题,MODULENAME审批类型编码,BUSIN_ID业务数据id,URL 单独登录url*/
            //"TB_ZT", "MODULENAME", "BUSIN_ID",  "URL"
            string[] subvalue = {"产品:"+ GridView1.Rows[index].Cells[4].Text, "02", id, Page.Request.UserHostName.ToString() };
           DataBaseOperator opt =new DataBaseOperator();
            if (opt.createApproval(subvalue))
            {
                string log_message = opt.UpDateOra("update HT_PUB_PROD_DESIGN set B_FLOW_STATUS = '0'  where PROD_CODE = '" + id + "'") == "Success" ? "提交审批成功" : "提交审批失败";
                log_message += ", 业务数据ID：" + id;
                opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
            }
            bindGrid();

        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataBaseOperator opt = new DataBaseOperator();
        string str = opt.GetSegValue("select Max(PROD_CODE) as code from ht_pub_prod_design where substr(Prod_code,0,4) = '703" + listType.SelectedValue + "'", "CODE");
        if (str == "")
            str = "0000000";
        txtCode.Text = "703" + listType.SelectedValue + (Convert.ToInt16(str.Substring(4)) + 1).ToString().PadLeft(3, '0');
       
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {

        DataBaseOperator opt = new DataBaseOperator();
        opt.UpDateOra("delete from HT_PUB_PROD_DESIGN where PROD_CODE = '" + txtCode.Text + "'");
        string[] seg = { "PROD_CODE", "PROD_NAME", "PACK_NAME", "HAND_MODE", "TECH_STDD_CODE", "MATER_FORMULA_CODE", "AUX_FORMULA_CODE", "COAT_FORMULA_CODE", "QLT_CODE", "STANDARD_VALUE", "REMARK" };
        string[] value = { txtCode.Text, txtName.Text, txtPack.Text, listType.SelectedValue, listTechStd.SelectedValue, listMtrl.SelectedValue, listAux.SelectedValue, listcoat.SelectedValue, listqlt.SelectedValue, txtValue.Text, txtDscpt.Text };
        string log_message = opt.InsertData(seg, value, "HT_PUB_PROD_DESIGN") == "Success" ? "保存产品信息成功," : "保存产品信息失败,";
        log_message += "产品信息：" + string.Join(" ", value);
        opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
        bindGrid();     
       
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        string query = "update HT_PUB_PROD_DESIGN set IS_DEL = '1'  where PROD_CODE = '" + txtCode.Text + "'";
       DataBaseOperator opt =new DataBaseOperator();
        if (opt.UpDateOra(query) == "Success")
        {
            opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(),"删除产品信息成功，产品编码："+txtCode.Text);
        }
        else
            opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "删除产品信息失败，产品编码：" + txtCode.Text);
        bindGrid();
    }
  


 
   

}