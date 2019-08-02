using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
public partial class Craft_SensorStdd : MSYS.Web.BasePage
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
       
        bindGrid2();
       

    }
 


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
        string query = "select r.inspect_type as 检查类型,r.inspect_group as 分组, r.inspect_code as 检查项目编码,j.minus_score as 单次扣分,j.REMARK as 备注 from ht_qlt_inspect_proj r left join HT_QLT_SENSOR_STDD j on j.inspect_code = r.inspect_code  where r.is_del = '0' and r.is_valid = '1' and r.inspect_type = '1' and  r.INspect_Group ='4'  order by r.inspect_code";

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
            commandlist.Add(opt.getMergeStr(seg, value, 1, "HT_QLT_SENSOR_STDD"));
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
                string query = "delete from  HT_QLT_SENSOR_STDD   where INSPECT_CODE = '" + projcode + "'";
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




}