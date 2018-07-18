using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Craft_Materia : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            try
            {
               DataBaseOperator opt =new DataBaseOperator();
                opt.bindDropDownList(listPrt, "select  MATTREE_CODE ,  MATTREE_NAME ,  PARENT_ID    from HT_PUB_MATTREE where  is_del = '0'", "MATTREE_NAME", "MATTREE_CODE");
                string mtr_code = Request["mtr_code"].ToString();
                if (mtr_code != "")
                {
                    bindData(mtr_code);
                }
            }
            catch
            { }
        }

    }
    protected void bindData(string mtr_code)
    {        
        string query = "select * from HT_PUB_MATTREE where  MATTREE_CODE = '" + mtr_code + "'";
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            txtCode.Text = mtr_code;
            txtName.Text = data.Tables[0].Rows[0]["MATTREE_NAME"].ToString();
            listPrt.SelectedValue = data.Tables[0].Rows[0]["PK_PARENT_CLASS"].ToString();
            ckValid.Checked = Convert.ToBoolean(Convert.ToInt16( data.Tables[0].Rows[0]["IS_VALID"].ToString()));
        }
        bindGrid(mtr_code);

       
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
       DataBaseOperator opt =new DataBaseOperator();
        string[] seg = { "MATTREE_CODE", "MATTREE_NAME", "PK_PARENT_CLASS" };
        string[] value = { txtCode.Text, txtName.Text, listPrt.SelectedValue};
        string log_message = opt.InsertData(seg, value, "HT_PUB_MATTREE") == "Success" ? "添加分类成功":"添加分类失败";
        log_message += "，分类信息：" + string.Join(" ", value);
        opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
        bindGrid(txtCode.Text);
        

    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
       DataBaseOperator opt =new DataBaseOperator();
        string[] seg = { "MATTREE_NAME", "PK_PARENT_CLASS", "IS_VALID" };
        string[] value = {  txtName.Text, listPrt.SelectedValue, Convert.ToInt16(ckValid.Checked).ToString() };
        string condition = " where MATTREE_CODE = '" + txtCode.Text + "'";
        string log_message = opt.UpDateData(seg, value, "HT_PUB_MATTREE", condition) == "Success"? "分类修改成功":"分类修改失败";
        log_message += ",分类信息：" + string.Join(" ", value);
        opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
        bindGrid(txtCode.Text);

    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        string query = "update HT_PUB_MATTREE set IS_DEL = '1'  where MATTREE_CODE = '" + txtCode.Text + "'";
       DataBaseOperator opt =new DataBaseOperator();
        string log_message = opt.UpDateOra(query) =="Success" ? "分类删除成功":"分类删除失败";
        log_message += ", 分类编码：" + txtCode.Text;
        opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
        bindGrid(txtCode.Text);
    }
    protected void bindGrid(string mtr_code)
    {

        string query = "select MATERIAL_CODE as 物料编码,MATERIAL_NAME as 物料名称,UNIT_CODE as 单位编码,MAT_CATEGORY as 类别,MAT_TYPE as 类型,MAT_LEVEL as 等级,LAST_UPDATE_TIME as 更新时间 from HT_PUB_MATERIEL where is_del = '0'  ";
        if(mtr_code != "")
            query += " and  MAT_CATEGORY = '" + mtr_code + "'";
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = data;
            GridView1.DataBind();
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                //  var lab = GridView1.Rows[i].FindControl("gridcode");

                ((Label)GridView1.Rows[i].FindControl("gridcode")).Text = "物料详情" + mydrv["物料编码"].ToString();

            }

        }

    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)//该函数没有使用
    {
        try
        {
            string TID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            string delSQL = "delete from HT_PUB_MATERIEL where MATERIAL_CODE= '" + TID + "'";
           DataBaseOperator opt =new DataBaseOperator();
            //opt.UpDateOra(delSQL);
            string log_message =  opt.UpDateOra(delSQL) == "Success" ? "删除成功" : "删除失败";
            log_message += "，分类信息：" + string.Join(" ", value);
            opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
            bindGrid(txtCode.Text);//重新绑定
        }
        catch (Exception ex)
        {

        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid(txtCode.Text);
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        bindData(hdcode.Value);
    }

    protected void btnDetail_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号  
            string mtr_code = GridView1.Rows[Rowindex].Cells[1].ToString();
            if (Rowindex >= 0)
            {
               DataBaseOperator opt =new DataBaseOperator();
                string query = "select * from HT_QA_MATER_FORMULA_DETAIL where FORMULA_CODE = '" + txtCode.Text + "' and MATER_CODE = '" + mtr_code + "'";
                DataSet data = opt.CreateDataSetOra(query);
                if (data != null && data.Tables[0].Rows.Count > 0)
                {
                    string[] seg = { "MATER_NAME", "BATCH_SIZE", "FRONT_GROUP", "MATER_SORT" };
                    string[] value = { ((TextBox)GridView1.Rows[Rowindex].FindControl("txtNameM")).Text, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtAmountM")).Text, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtGroupM")).Text, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtSortM")).Text };
                    string condition = " where FORMULA_CODE = '" + txtCode.Text + "' and MATER_CODE = '" + mtr_code + "'";
                    //opt.UpDateData(seg, value, "ht_qa_mater_formula_detail", condition);
                    string log_message = opt.UpDateData(seg, value, "ht_qa_mater_formula_detail", condition) == "Success" ? "更新成功" : "更新失败";
                    log_message += ", 分类编码：" + txtCode.Text;
                    opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
                }
                else
                {
                    string[] seg = { "FORMULA_CODE", "MATER_CODE", "MATER_NAME", "BATCH_SIZE", "FRONT_GROUP", "MATER_SORT" };
                    string[] value = { txtCode.Text, mtr_code, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtNameM")).Text, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtAmountM")).Text, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtGroupM")).Text, ((TextBox)GridView1.Rows[Rowindex].FindControl("txtSortM")).Text };
                    //opt.InsertData(seg, value, "ht_qa_mater_formula_detail");
                    string log_message = opt.InsertData(seg, value, "ht_qa_mater_formula_detail") == "Success" ? "更新成功" : "更新失败";
                    log_message += ", 分类编码：" + txtCode.Text;
                    opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
                }
                bindGrid(txtCode.Text);
            }
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }


    }

}