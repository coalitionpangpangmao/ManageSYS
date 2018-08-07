using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
public partial class Craft_Materia : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listPrt, "select mattree_code，mattree_name,PARENT_CODE  from ht_pub_mattree where is_del = '0' order by PARENT_CODE", "mattree_name", "mattree_code"); 
        }

    }
    protected void bindData(string mtr_code)
    {
        string query = "select * from HT_PUB_MATTREE where  MATTREE_CODE = '" + mtr_code + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            txtCode.Text = mtr_code;
            txtName.Text = data.Tables[0].Rows[0]["MATTREE_NAME"].ToString();
            listPrt.SelectedValue = data.Tables[0].Rows[0]["PARENT_CODE"].ToString();
            ckValid.Checked = Convert.ToBoolean(Convert.ToInt16(data.Tables[0].Rows[0]["IS_VALID"].ToString()));
        }
        bindGrid(mtr_code);


    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string query = "select nvl(Max(mattree_code),0) as code  from ht_pub_mattree" ;
        if (listPrt.SelectedValue == "")
            query += " where Parent_code is null";
        else
            query += " where Parent_code ='" + listPrt.SelectedValue + "'";
        
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        int codelength = listPrt.SelectedValue.Length;
        string code = opt.GetSegValue(query, "code");
        
            if(codelength == 6)
            {
                if(code == "0")
                code = listPrt.SelectedValue + "000";
                codelength = 9;
            }
            else
            {
                if(code == "0")
                code = listPrt.SelectedValue + "00";
                codelength += 2;
            }
         
            txtCode.Text = (Convert.ToInt32(code) + 1).ToString().PadLeft(codelength, '0');

        txtName.Text = "";
        
        bindGrid(txtCode.Text);


    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        string[] seg = { "mattree_code", "MATTREE_NAME", "PARENT_CODE", "IS_VALID" };
        string[] value = {txtCode.Text, txtName.Text, listPrt.SelectedValue, Convert.ToInt16(ckValid.Checked).ToString() };

        List<String> commandlist = new List<String>();
        commandlist.Add("delete from HT_PUB_MATTREE where MATTREE_CODE = '" + txtCode.Text + "'");
        commandlist.Add(opt.InsertDatastr(seg,value,"HT_PUB_MATTREE"));
       
       
        string log_message =  opt.TransactionCommand(commandlist) == "Success" ? "分类修改成功" : "分类修改失败";
        log_message += ",分类信息：" + string.Join(" ", value);
        opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
        bindGrid(txtCode.Text);

    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        string query = "update HT_PUB_MATTREE set IS_DEL = '1'  where MATTREE_CODE = '" + txtCode.Text + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string log_message = opt.UpDateOra(query) == "Success" ? "分类删除成功" : "分类删除失败";
        log_message += ", 分类编码：" + txtCode.Text;
        opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
        bindGrid(txtCode.Text);
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView theGrid = sender as GridView;
        int newPageIndex = 0;
        if (e.NewPageIndex == -3)
        {
            //点击跳转按钮
            TextBox txtNewPageIndex = null;

            //GridView较DataGrid提供了更多的API，获取分页块可以使用BottomPagerRow 或者TopPagerRow，当然还增加了HeaderRow和FooterRow
            GridViewRow pagerRow = theGrid.BottomPagerRow;

            if (pagerRow != null)
            {
                //得到text控件
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;
            }
            if (txtNewPageIndex != null)
            {
                //得到索引
                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1;
            }
        }
        else
        {
            //点击了其他的按钮
            newPageIndex = e.NewPageIndex;
        }
        //防止新索引溢出
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;
        //得到新的值
        theGrid.PageIndex = newPageIndex;
        //重新绑定

        bindGrid(txtCode.Text);
    }
    protected void bindGrid(string mtr_code)
    {

        string query = "select MATERIAL_CODE as 物料编码,MATERIAL_NAME as 物料名称,UNIT_CODE as 单位编码,MAT_CATEGORY as 类别,MAT_TYPE as 类型,MAT_LEVEL as 等级,LAST_UPDATE_TIME as 更新时间 from HT_PUB_MATERIEL where is_del = '0'  ";
        if (mtr_code != "")
            query += " and  MAT_CATEGORY = '" + mtr_code + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();

    }

  

    protected void btnGridDel_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string code = GridView1.DataKeys[rowindex].Value.ToString();
        string delSQL = "delete from HT_PUB_MATERIEL where MATERIAL_CODE= '" + code + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.UpDateOra(delSQL);
        bindGrid(txtCode.Text);//重新绑定
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
        Button btn = (Button)sender;
        int rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string code = GridView1.DataKeys[rowindex].Value.ToString();
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "viewDetail", " GridClick('" + code + "');",true);

    }

}