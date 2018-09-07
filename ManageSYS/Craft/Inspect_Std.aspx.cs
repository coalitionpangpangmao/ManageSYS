using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
public partial class Craft_InspectStd : MSYS.Web.BasePage
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

        bindGrid();
    }
    protected void bindGrid()
    {
        string query = "select r.inspect_type as 检查类型,r.inspect_group as 分组, r.inspect_code as 检查项目编码,j.upper_value as 上限 ,j.lower_value as 下限,j.minus_score as 单次扣分,j.REMARK as 备注 from ht_qlt_inspect_proj r left join ht_QLT_inspect_stdd j on j.inspect_code = r.inspect_code  where r.is_del = '0' and r.is_valid = '1' ";   
        if (listtype.SelectedValue != "")
            query += " and r.inspect_type = '" + listtype.SelectedValue + "'";
        
            if(listSection.SelectedValue != "")
                query += " and  r.INspect_Group = '" + listSection.SelectedValue + "'";
            query += " order by r.inspect_code";
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            for (int i = GridView1.PageSize * GridView1.PageIndex; i < GridView1.PageSize * (GridView1.PageIndex + 1) && i < data.Tables[0].Rows.Count; i++)
            {
                int j = i - GridView1.PageSize * GridView1.PageIndex;
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                GridViewRow row = GridView1.Rows[j];
                ((DropDownList)row.FindControl("listType")).SelectedValue = mydrv["检查类型"].ToString();
                DropDownList list = (DropDownList)row.FindControl("listGroup");
                if (mydrv["检查类型"].ToString() == "0")
                {
                    opt.bindDropDownList(list, "select Section_code,Section_name from ht_pub_tech_section where is_valid = '1' and is_del = '0' order by section_code", "Section_name", "Section_code");
                }
                else
                {
                    opt.bindDropDownList(list, "select ID,Name from ht_inner_inspect_group t", "Name", "ID");
                }
                list.SelectedValue = mydrv["分组"].ToString();
                DropDownList list2 = (DropDownList)row.FindControl("listInspect");
                opt.bindDropDownList(list2, "select inspect_code,inspect_name from ht_qlt_inspect_proj where  inspect_group = '"+ list.SelectedValue + "' and is_del = '0'", "inspect_name", "inspect_code");
                ((DropDownList)row.FindControl("listInspect")).SelectedValue = mydrv["检查项目编码"].ToString();
                ((TextBox)row.FindControl("txtUpper")).Text = mydrv["上限"].ToString();
                ((TextBox)row.FindControl("txtLower")).Text = mydrv["下限"].ToString();
                ((TextBox)row.FindControl("txtScore")).Text = mydrv["单次扣分"].ToString();
                ((TextBox)row.FindControl("txtRemark")).Text = mydrv["备注"].ToString();
            }
        }

    }

    protected DataSet bindInspect()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select ID,INSPECT_TYPE from HT_INNER_BOOL_DISPLAY");
    }
    protected void initView()
    {        
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
       opt.bindDropDownList(listtype, "select distinct ID,inspect_type from ht_inner_bool_display t", "inspect_type", "ID");
   
        
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid();
    }

    //protected void btnAdd_Click(object sender, EventArgs e)
    //{
    //    string query = "select r.inspect_type as 检查类型,r.inspect_group as 分组, r.inspect_code as 检查项目编码,j.upper_value as 上限 ,j.lower_value as 下限,j.minus_score as 单次扣分,j.REMARK as 备注  from ht_qlt_inspect_proj r left join ht_QLT_inspect_stdd j on j.inspect_code = r.inspect_code  where r.is_del = '0' and r.is_valid = '1' ";
    //    if (listtype.SelectedValue == "")
    //        query = " and r.inspect_type = '" + listtype.SelectedValue + "'";

    //    if (listSection.SelectedValue != "")
    //        query += " and  r.INspect_Group = '" + listSection.SelectedValue + "'";
    //    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
    //    DataSet set = opt.CreateDataSetOra(query);
    //    DataTable data = set.Tables[0];
    //    if (data == null)
    //    {
    //        data = new DataTable();
    //        data.Columns.Add("检查类型");
    //        data.Columns.Add("分组");
    //        data.Columns.Add("检查项目编码");
    //        data.Columns.Add("上限");
    //        data.Columns.Add("下限");
    //        data.Columns.Add("单次扣分");
    //        data.Columns.Add("备注");
    //    }
    //    object[] value = { "", "", "", 0, 0,0 ,""};
    //    data.Rows.Add(value);


    //    GridView1.DataSource = data;
    //    GridView1.DataBind();
    //    if (data != null && data.Rows.Count > 0)
    //    {
    //        for (int i = 0; i < data.Rows.Count; i++)
    //        {
    //            DataRowView mydrv = data.DefaultView[i];
    //            GridViewRow row = GridView1.Rows[i];
    //            ((DropDownList)row.FindControl("listType")).SelectedValue = mydrv["检查类型"].ToString();
    //            DropDownList list = (DropDownList)row.FindControl("listGroup");
    //            if (mydrv["检查类型"].ToString() == "0")
    //            {
    //                opt.bindDropDownList(list, "select Section_code,Section_name from ht_pub_tech_section where is_valid = '1' and is_del = '0' order by section_code", "Section_name", "Section_code");
    //            }
    //            else
    //            {
    //                opt.bindDropDownList(list, "select ID,Name from ht_inner_inspect_group t", "Name", "ID");
    //            }
    //            list.SelectedValue = mydrv["分组"].ToString();
    //            DropDownList list2 = (DropDownList)row.FindControl("listInspect");
    //            opt.bindDropDownList(list2, "select inspect_code,inspect_name from ht_qlt_inspect_proj where  inspect_group = '" + list.SelectedValue + "' and is_del = '0'", "inspect_name", "inspect_code");
    //            ((DropDownList)row.FindControl("listInspect")).SelectedValue = mydrv["检查项目编码"].ToString();
    //            ((TextBox)GridView1.Rows[i].FindControl("txtUpper")).Text = mydrv["上限"].ToString();
    //            ((TextBox)GridView1.Rows[i].FindControl("txtLower")).Text = mydrv["下限"].ToString();
    //            ((TextBox)GridView1.Rows[i].FindControl("txtScore")).Text = mydrv["单次扣分"].ToString();
    //            ((TextBox)GridView1.Rows[i].FindControl("txtRemark")).Text = mydrv["备注"].ToString();
    //        }
    //    }
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string code = GridView1.DataKeys[rowindex].Value.ToString();
      
       List<String> commandlist = new List<String>();
       commandlist.Add("delete from HT_QLT_INSPECT_STDD where INSPECT_CODE = '" + code + "'");
       string[] seg = { "INSPECT_CODE", "UPPER_VALUE", "LOWER_VALUE", "MINUS_SCORE", "REMARK", "CREATE_ID", "CREATE_TIME" };
       GridViewRow row = GridView1.Rows[rowindex];
       string[] value = { ((DropDownList)row.FindControl("listInspect")).SelectedValue, ((TextBox)row.FindControl("txtUpper")).Text, ((TextBox)row.FindControl("txtLower")).Text, ((TextBox)row.FindControl("txtScore")).Text, ((TextBox)row.FindControl("txtRemark")).Text, ((MSYS.Data.SysUser)Session["User"]).id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
       MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
       commandlist.Add(opt.InsertDatastr(seg, value, "HT_QLT_INSPECT_STDD"));
            opt.TransactionCommand(commandlist);
            bindGrid();
    }

    protected void btnGrid1CkAll_Click(object sender, EventArgs e)//全选
    {
        try
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                ((CheckBox)GridView1.Rows[i].FindControl("ck")).Checked = true;
            }
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void btnGrid1DelSel_Click(object sender, EventArgs e)//删除选中记录
    {
        try
        {
            //    createGridView();
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("ck")).Checked)
                {
                    string projcode = GridView1.DataKeys[i].Value.ToString();
                    string query = "delete from  HT_QLT_INSPECT_STDD   where INSPECT_CODE = '" + projcode + "'";
                   MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
                   string log_message = opt.UpDateOra(query) == "Success" ? "删除工艺检查标准成功" : "删除工艺检查标准失败";
                   log_message += "标识:" + projcode;
                   InsertTlog(log_message);
                }
            }           
            bindGrid();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

 

    protected void listtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        if (listtype.SelectedValue == "0")
        {
            opt.bindDropDownList(listSection, "select Section_code,Section_name from ht_pub_tech_section where is_valid = '1' and is_del = '0' order by section_code", "Section_name", "Section_code");
        }
        else
        {
            opt.bindDropDownList(listSection, "select ID,Name from ht_inner_inspect_group t", "Name", "ID");
        }

    }

}