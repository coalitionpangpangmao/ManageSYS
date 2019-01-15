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
            initView();
        }
    }
    protected void initView()
    {
        bindGrid1();
        bindGrid2();
        bindGrid3();

    }
    #region tab1
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

        bindGrid1();
    }
    protected void bindGrid1()
    {
        string query = "select r.inspect_type as 检查类型,r.inspect_group as 分组, r.inspect_code as 检查项目编码,j.upper_value as 上限 ,j.lower_value as 下限,j.minus_score as 单次扣分,j.REMARK as 备注 from ht_qlt_inspect_proj r left join ht_QLT_inspect_stdd j on j.inspect_code = r.inspect_code  where r.is_del = '0' and r.is_valid = '1' and r.inspect_type = '1' and  r.INspect_Group in ('1','2','3')  order by r.inspect_code";
       
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
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
               
                DropDownList list = (DropDownList)row.FindControl("listGroup");               
                opt.bindDropDownList(list, "select ID,Name from ht_inner_inspect_group t where t.id in ('1','2','3')", "Name", "ID");              
                list.SelectedValue = mydrv["分组"].ToString();
                DropDownList list2 = (DropDownList)row.FindControl("listInspect");
                opt.bindDropDownList(list2, "select inspect_code,inspect_name from ht_qlt_inspect_proj where  inspect_group = '" + list.SelectedValue + "' and is_del = '0'", "inspect_name", "inspect_code");
                ((DropDownList)row.FindControl("listInspect")).SelectedValue = mydrv["检查项目编码"].ToString();
                ((TextBox)row.FindControl("txtUpper")).Text = mydrv["上限"].ToString();
                ((TextBox)row.FindControl("txtLower")).Text = mydrv["下限"].ToString();
                ((TextBox)row.FindControl("txtScore")).Text = mydrv["单次扣分"].ToString();
                ((TextBox)row.FindControl("txtRemark")).Text = mydrv["备注"].ToString();
            }
        }

    }  
    protected void btnGrid1Save_Click(object sender, EventArgs e)
    {
        List<string> commandlist = new List<string>();
          MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
          foreach (GridViewRow row in GridView1.Rows)
          {
              string[] seg = { "INSPECT_CODE", "UPPER_VALUE", "LOWER_VALUE", "MINUS_SCORE", "REMARK", "CREATE_ID", "CREATE_TIME" };

              string[] value = { ((DropDownList)row.FindControl("listInspect")).SelectedValue, ((TextBox)row.FindControl("txtUpper")).Text, ((TextBox)row.FindControl("txtLower")).Text, ((TextBox)row.FindControl("txtScore")).Text, ((TextBox)row.FindControl("txtRemark")).Text, ((MSYS.Data.SysUser)Session["User"]).id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
              commandlist.Add(opt.getMergeStr(seg, value, 1, "HT_QLT_INSPECT_STDD"));
          }
          string log_message = opt.TransactionCommand(commandlist) == "Success" ? "保存理化检测标准成功" : "保存理化检测项目标准失败";
          if (log_message == "保存理化检测标准成功")
          {             
                  string[] procseg = { };
                  object[] procvalues = { };
                  opt.ExecProcedures("Create_phechem_Report", procseg, procvalues);             
          }
          InsertTlog(log_message);
          bindGrid1();
          ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "success", "alert('" + log_message + "')", true);
      
    }
    protected void btnGrid1CkAll_Click(object sender, EventArgs e)//全选
    {
        int ckno = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (((CheckBox)GridView1.Rows[i].FindControl("ck")).Checked)
                ckno++;
        }
        bool check = (ckno < GridView1.Rows.Count);
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            ((CheckBox)GridView1.Rows[i].FindControl("ck")).Checked = check;

        }
    }
    protected void btnGrid1DelSel_Click(object sender, EventArgs e)//删除选中记录
    {

        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            if (((CheckBox)GridView1.Rows[i].FindControl("ck")).Checked)
            {
                string projcode = GridView1.DataKeys[i].Value.ToString();
                string query = "delete from  HT_QLT_INSPECT_STDD   where INSPECT_CODE = '" + projcode + "'";
                MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                string log_message = opt.UpDateOra(query) == "Success" ? "删除工艺检查标准成功" : "删除工艺检查标准失败";
                if (log_message == "删除工艺检查标准成功")
                {
                    string[] procseg = { };
                    object[] procvalues = { };
                    opt.ExecProcedures("Create_phechem_Report", procseg, procvalues);
                }
                log_message += "--标识:" + projcode;
                InsertTlog(log_message);
            }
        }
        bindGrid1();

    }
    #endregion


    #region tab2
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
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

        bindGrid2();
    }
    protected void bindGrid2()
    {
        string query = "select r.inspect_type as 检查类型,r.inspect_group as 分组, r.inspect_code as 检查项目编码,j.upper_value as 上限 ,j.lower_value as 下限,j.minus_score as 单次扣分,j.REMARK as 备注 from ht_qlt_inspect_proj r left join ht_QLT_inspect_stdd j on j.inspect_code = r.inspect_code  where r.is_del = '0' and r.is_valid = '1' and r.inspect_type = '1' and  r.INspect_Group ='4'  order by r.inspect_code";

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView2.DataSource = data;
        GridView2.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            for (int i = GridView2.PageSize * GridView2.PageIndex; i < GridView2.PageSize * (GridView2.PageIndex + 1) && i < data.Tables[0].Rows.Count; i++)
            {
                int j = i - GridView2.PageSize * GridView2.PageIndex;
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                GridViewRow row = GridView2.Rows[j];
            
                DropDownList list2 = (DropDownList)row.FindControl("listInspect");
                opt.bindDropDownList(list2, "select inspect_code,inspect_name from ht_qlt_inspect_proj where  inspect_group = '4' and is_del = '0'", "inspect_name", "inspect_code");
                ((DropDownList)row.FindControl("listInspect")).SelectedValue = mydrv["检查项目编码"].ToString();                
                ((TextBox)row.FindControl("txtScore")).Text = mydrv["单次扣分"].ToString();
                ((TextBox)row.FindControl("txtRemark")).Text = mydrv["备注"].ToString();
            }
        }

    }
    protected void btnGrid2Save_Click(object sender, EventArgs e)
    {
        List<string> commandlist = new List<string>();
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        foreach (GridViewRow row in GridView2.Rows)
        {
            string[] seg = { "INSPECT_CODE",  "MINUS_SCORE", "REMARK", "CREATE_ID", "CREATE_TIME" };

            string[] value = { ((DropDownList)row.FindControl("listInspect")).SelectedValue, ((TextBox)row.FindControl("txtScore")).Text, ((TextBox)row.FindControl("txtRemark")).Text, ((MSYS.Data.SysUser)Session["User"]).id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
            commandlist.Add(opt.getMergeStr(seg, value, 1, "HT_QLT_INSPECT_STDD"));
        }
        string log_message = opt.TransactionCommand(commandlist) == "Success" ? "保存感观评测标准成功" : "保存感观评测标准失败";
        if (log_message == "保存感观评测标准成功")
        {          
                string[] procseg = { };
                object[] procvalues = { };
                opt.ExecProcedures("Create_Sensor_Report", procseg, procvalues);
            }
        InsertTlog(log_message);
        bindGrid2();
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "success", "alert('" + log_message + "')", true);
    }
    protected void btnGrid2CkAll_Click(object sender, EventArgs e)//全选
    {
        int ckno = 0;
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            if (((CheckBox)GridView2.Rows[i].FindControl("ck")).Checked)
                ckno++;
        }
        bool check = (ckno < GridView2.Rows.Count);
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            ((CheckBox)GridView2.Rows[i].FindControl("ck")).Checked = check;

        }
    }
    protected void btnGrid2DelSel_Click(object sender, EventArgs e)//删除选中记录
    {

        for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
        {
            if (((CheckBox)GridView2.Rows[i].FindControl("ck")).Checked)
            {
                string projcode = GridView2.DataKeys[i].Value.ToString();
                string query = "delete from  HT_QLT_INSPECT_STDD   where INSPECT_CODE = '" + projcode + "'";
                MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                string log_message = opt.UpDateOra(query) == "Success" ? "删除工艺检查标准成功" : "删除工艺检查标准失败";
                if (log_message == "删除工艺检查标准成功")
                {
                    string[] procseg = { };
                    object[] procvalues = { };
                    opt.ExecProcedures("Create_Sensor_Report", procseg, procvalues);
                }
                log_message += "--标识:" + projcode;
                InsertTlog(log_message);
            }
        }
        bindGrid2();

    }
    #endregion


    #region tab3
    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
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

        bindGrid3();
    }
    protected void bindGrid3()
    {
        string query = "select r.inspect_type as 检查类型,r.inspect_group as 分组, r.inspect_code as 检查项目编码,j.upper_value as 上限 ,j.lower_value as 下限,j.minus_score as 单次扣分,j.REMARK as 备注 from ht_qlt_inspect_proj r left join ht_QLT_inspect_stdd j on j.inspect_code = r.inspect_code  where r.is_del = '0' and r.is_valid = '1' and r.inspect_type = '0' order by r.inspect_code";

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView3.DataSource = data;
        GridView3.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            for (int i = GridView3.PageSize * GridView3.PageIndex; i < GridView3.PageSize * (GridView3.PageIndex + 1) && i < data.Tables[0].Rows.Count; i++)
            {
                int j = i - GridView3.PageSize * GridView3.PageIndex;
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                GridViewRow row = GridView3.Rows[j];

                DropDownList list = (DropDownList)row.FindControl("listGroup");
                opt.bindDropDownList(list, "select Section_code,Section_name from ht_pub_tech_section where is_valid = '1' and is_del = '0' order by section_code", "Section_name", "Section_code");
                list.SelectedValue = mydrv["分组"].ToString();
                DropDownList list2 = (DropDownList)row.FindControl("listInspect");
                opt.bindDropDownList(list2, "select inspect_code,inspect_name from ht_qlt_inspect_proj where  inspect_group = '" + list.SelectedValue + "' and is_del = '0'", "inspect_name", "inspect_code");
                ((DropDownList)row.FindControl("listInspect")).SelectedValue = mydrv["检查项目编码"].ToString();
                ((TextBox)row.FindControl("txtUpper")).Text = mydrv["上限"].ToString();
                ((TextBox)row.FindControl("txtLower")).Text = mydrv["下限"].ToString();
                ((TextBox)row.FindControl("txtScore")).Text = mydrv["单次扣分"].ToString();
                ((TextBox)row.FindControl("txtRemark")).Text = mydrv["备注"].ToString();
            }
        }

    }
    protected void btnGrid3Save_Click(object sender, EventArgs e)
    {
        List<string> commandlist = new List<string>();
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        foreach (GridViewRow row in GridView3.Rows)
        {
            string[] seg = { "INSPECT_CODE", "UPPER_VALUE", "LOWER_VALUE", "MINUS_SCORE", "REMARK", "CREATE_ID", "CREATE_TIME" };

            string[] value = { ((DropDownList)row.FindControl("listInspect")).SelectedValue, ((TextBox)row.FindControl("txtUpper")).Text, ((TextBox)row.FindControl("txtLower")).Text, ((TextBox)row.FindControl("txtScore")).Text, ((TextBox)row.FindControl("txtRemark")).Text, ((MSYS.Data.SysUser)Session["User"]).id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
            commandlist.Add(opt.getMergeStr(seg, value, 1, "HT_QLT_INSPECT_STDD"));
        }
        string log_message = opt.TransactionCommand(commandlist) == "Success" ? "保存过程检测标准成功" : "保存过程检测标准失败";
        if (log_message == "保存过程检测标准成功")
        {

            string[] procseg = { };
            object[] procvalues = { };
            opt.ExecProcedures("Create_process_Report", procseg, procvalues);
        }
        InsertTlog(log_message);
        bindGrid3();
        ScriptManager.RegisterStartupScript(UpdatePanel3, this.Page.GetType(), "success", "alert('" + log_message + "')", true);
    }
    protected void btnGrid3CkAll_Click(object sender, EventArgs e)//全选
    {
        int ckno = 0;
        for (int i = 0; i < GridView3.Rows.Count; i++)
        {
            if (((CheckBox)GridView3.Rows[i].FindControl("ck")).Checked)
                ckno++;
        }
        bool check = (ckno < GridView3.Rows.Count);
        for (int i = 0; i < GridView3.Rows.Count; i++)
        {
            ((CheckBox)GridView3.Rows[i].FindControl("ck")).Checked = check;

        }
    }
    protected void btnGrid3DelSel_Click(object sender, EventArgs e)//删除选中记录
    {

        for (int i = 0; i <= GridView3.Rows.Count - 1; i++)
        {
            if (((CheckBox)GridView3.Rows[i].FindControl("ck")).Checked)
            {
                string projcode = GridView3.DataKeys[i].Value.ToString();
                string query = "delete from  HT_QLT_INSPECT_STDD   where INSPECT_CODE = '" + projcode + "'";
                MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                string log_message = opt.UpDateOra(query) == "Success" ? "删除工艺检查标准成功" : "删除工艺检查标准失败";
                if (log_message == "删除工艺检查标准成功")
                {

                    string[] procseg = { };
                    object[] procvalues = { };
                    opt.ExecProcedures("Create_process_Report", procseg, procvalues);
                }
                log_message += "--标识:" + projcode;
                InsertTlog(log_message);
            }
        }
        bindGrid3();

    }
    #endregion

}