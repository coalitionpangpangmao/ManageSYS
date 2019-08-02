using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MSYS.Data;

public partial class Inspect_Std : MSYS.Web.BasePage
{    
    protected string subtvHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
           opt.bindDropDownList(listVersion, "select INSPECT_STDD_NAME,INSPECT_STDD_CODE from HT_QLT_INSPECT_STDD where is_valid = '1' and is_del= '0'", "INSPECT_STDD_NAME", "INSPECT_STDD_CODE");           
            opt.bindDropDownList(listtech, "select INSPECT_STDD_CODE,INSPECT_STDD_NAME from HT_QLT_INSPECT_STDD where is_valid = '1' and is_del = '0'", "INSPECT_STDD_NAME", "INSPECT_STDD_CODE");
            opt.bindDropDownList(listtechC, "select INSPECT_STDD_CODE,INSPECT_STDD_NAME  from HT_QLT_INSPECT_STDD where is_valid = '1' and is_del = '0'", "INSPECT_STDD_NAME", "INSPECT_STDD_CODE");
           
            opt.bindDropDownList(listCreator, "select id,name from  ht_svr_user    order by id ", "Name", "ID");

            opt.bindDropDownList(listAprv, "select * from ht_inner_aprv_status ", "NAME", "ID");
        
           
        }
    }

    protected void SetEnable(bool enable)
    {
     //   btnAdd.Visible = enable;
     //   btnModify.Visible = enable;
     ////   btnDelete.Visible = enable;
     //   btnSave2.Visible = enable;
     //   btnDelSel2.Visible = enable;
     //   btnSave.Visible = enable;
     //   btnDelSel.Visible = enable;
    }
    protected void initView()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listVersion, "select INSPECT_STDD_NAME,INSPECT_STDD_CODE from HT_QLT_INSPECT_STDD where is_valid = '1' and is_del= '0'", "INSPECT_STDD_NAME", "INSPECT_STDD_CODE");
        opt.bindDropDownList(listtech, "select INSPECT_STDD_NAME,INSPECT_STDD_CODE from HT_QLT_INSPECT_STDD where is_valid = '1' and is_del= '0'", "INSPECT_STDD_NAME", "INSPECT_STDD_CODE");
        opt.bindDropDownList(listtechC, "select INSPECT_STDD_NAME,INSPECT_STDD_CODE from HT_QLT_INSPECT_STDD where is_valid = '1' and is_del= '0'", "INSPECT_STDD_NAME", "INSPECT_STDD_CODE");
    }
  

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select * from HT_QLT_INSPECT_STDD_SUB where STDD_CODE = '" + listtech.SelectedValue + "' and is_del = '0'";
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            List<string> commandlist = new List<string>();
            foreach (DataRow row in data.Tables[0].Rows)
            {
                string[] seg = { "STDD_CODE" ,"INSPECT_CODE", "MINUS_SCORE", "UPPER_VALUE", "LOWER_VALUE", "REMARK", "CREATE_ID", "CREATE_TIME"};
                string[] value = {  listtechC.SelectedValue,row["INSPECT_CODE"].ToString(), row["MINUS_SCORE"].ToString(), row["UPPER_VALUE"].ToString(), row["LOWER_VALUE"].ToString(), row["REMARK"].ToString(), ((SysUser)Session["User"]).id,System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };

                commandlist.Add(opt.getMergeStr(seg, value, 2, "HT_QLT_INSPECT_STDD_SUB"));
            }
            string log_message = opt.TransactionCommand(commandlist) == "Success" ? "复制标准成功" : "复制标准失败";
            log_message += ",复制标准：" + listtechC.SelectedValue;
            InsertTlog(log_message);
        }

      
        initView();
        bindGrid1();
        bindGrid2();

    }
    //保存标准版本
    protected void btnModify_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        {

            string[] seg = { "INSPECT_STDD_CODE", "INSPECT_STDD_NAME", "STANDARD_VOL", "CREATE_ID", "CREATE_DATE", "REMARK" };
            string[] value = { txtCode.Text, txtName.Text, txtVersion.Text, listCreator.SelectedValue, txtCrtDate.Text,  txtDscpt.Text };
            string log_message = opt.MergeInto(seg, value, 1, "HT_QLT_INSPECT_STDD") == "Success" ? "保存标准成功" : "保存标准失败";
            log_message += ",保存数据：" + string.Join(",", value);
            InsertTlog(log_message);
            ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "alert", "alert('" + log_message + "')", true);
        }
       
       
        listAprv.SelectedValue = "-1";
        initView();
       


    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        List<String> commandlist = new List<string>();
        commandlist.Add("delete HT_QLT_INSPECT_STDD  where INSPECT_STDD_CODE = '" + txtCode.Text + "'");
        commandlist.Add("delete from ht_pub_aprv_flowinfo where BUSIN_ID = '" + txtCode.Text + "'");
        string log_message = opt.TransactionCommand(commandlist) == "Success" ? "删除工艺标准成功" : "删除工艺标准失败";
        log_message += "--标识:" + txtCode.Text;
        InsertTlog(log_message);
        initView();
    }
    protected void bindData(string rcpcode)
    {
        string query = "select INSPECT_STDD_CODE  as 标准编码,INSPECT_STDD_NAME  as 标准名称,STANDARD_VOL  as 标准版本号,CREATE_ID  as 编制人,CREATE_DATE  as 编制日期,REMARK  as 备注,is_valid ,FLOW_STATUS from HT_QLT_INSPECT_STDD where is_del = '0' and INSPECT_STDD_CODE  = '" + rcpcode + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            txtCode.Text = rcpcode;
            txtName.Text = data.Tables[0].Rows[0]["标准名称"].ToString();
            txtVersion.Text = data.Tables[0].Rows[0]["标准版本号"].ToString();

            listCreator.SelectedValue = data.Tables[0].Rows[0]["编制人"].ToString();
            txtCrtDate.Text = data.Tables[0].Rows[0]["编制日期"].ToString();

            txtDscpt.Text = data.Tables[0].Rows[0]["备注"].ToString();
            ckValid.Checked = ("1" == data.Tables[0].Rows[0]["is_valid"].ToString());
            listAprv.SelectedValue = data.Tables[0].Rows[0]["FLOW_STATUS"].ToString();
            if (!(listAprv.SelectedItem.Text == "未提交" || listAprv.SelectedItem.Text == "未通过"))
            {
                btnSubmit.Enabled = false;
                btnSubmit.CssClass = "btngrey";
                SetEnable(false);

            }
            else
            {
                btnSubmit.Enabled = true;
                btnSubmit.CssClass = "btn1 auth";
                SetEnable(true);
            }
            bindGrid1();
            bindGrid2();
        }
        else
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtVersion.Text = "";

            listCreator.SelectedValue = "";
            txtCrtDate.Text = "";

            txtDscpt.Text = "";
            ckValid.Checked = false;
            listAprv.SelectedValue = "";
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
        }
       
       
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        bindData(txtCode.Text);

    }
    protected void UpdateGrid_Click(object sender, EventArgs e)
    {
        bindGrid1();
        bindGrid2();
       
    }
   
  
 
    protected void btnCkAll_Click(object sender, EventArgs e)
    {
        int ckno = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked)
                ckno++;
        }
        bool check = (ckno < GridView1.Rows.Count);
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            ((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked = check;

        }
    }
   
   
  
   

   
  
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string str = opt.GetSegValue("select nvl(Max(substr(inspect_stdd_code,7,3)),0)+1 as code  from HT_QLT_INSPECT_STDD t where is_del='0'", "CODE");

        txtCode.Text = "ISP703" + str.PadLeft(3, '0');
        MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
        listCreator.SelectedValue = user.id;
        txtCrtDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");        
        btnSubmit.Enabled = true;
        btnSubmit.CssClass = "btn1 auth";
        txtName.Text = "";
        txtVersion.Text = "";
       
        txtDscpt.Text = "";
        ckValid.Checked = false;
        listAprv.SelectedValue = "";

    }

    protected void btnFLow_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        string ID = txtCode.Text;
        string query = "select pos as 顺序号, workitemid as 审批环节,username as 负责人,comments as 意见,opiniontime 审批时间,(case status when '0' then '未审批'  when '1' then '未通过' else '己通过' end)  as 审批状态  from ht_pub_aprv_opinion r left join ht_pub_aprv_flowinfo s on r.gongwen_id = s.id where s.busin_id  = '" + ID + "' order by pos";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        GridView3.DataSource = opt.CreateDataSetOra(query);
        GridView3.DataBind();
        ScriptManager.RegisterStartupScript(UpdatePanel4, this.Page.GetType(), "", "$('#flowinfo').fadeIn(200);", true);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)//提交审批
    {

        /*启动审批TB_ZT标题,MODULENAME审批类型编码,BUSIN_ID业务数据id,URL 单独登录url*/
        //"TB_ZT", "MODULENAME", "BUSIN_ID",  "URL"
        string[] subvalue = { "工艺标准:" + txtName.Text, "04", txtCode.Text, Page.Request.UserHostName.ToString() };
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        if (MSYS.AprvFlow.createApproval(subvalue))
        {
            string log_message = ",工艺标准:" + txtName.Text + "提交审批成功";
            listAprv.SelectedValue = "0";
            InsertTlog(log_message);
            btnSubmit.Enabled = false;
            btnSubmit.CssClass = "btngrey";

        }
    }
    protected void listVersion_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindData(listVersion.SelectedValue);
       
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
        //string query = "select r.inspect_type as 检查类型,r.inspect_group as 分组, r.inspect_code as 检查项目编码,j.upper_value as 上限 ,j.lower_value as 下限,j.minus_score as 单次扣分,j.REMARK as 备注 from ht_qlt_inspect_proj r left join HT_QLT_INSPECT_STDD_SUB j on j.inspect_code = r.inspect_code  where  j.stdd_code = '" + listVersion.SelectedValue + "' and  r.is_del = '0' and r.is_valid = '1' and r.inspect_type = '1' and  r.INspect_Group in ('1','2','3')  order by r.inspect_code";
        string query = "select r.inspect_type as 检查类型,r.inspect_group as 分组, r.inspect_code as 检查项目编码,j.upper_value as 上限 ,j.lower_value as 下限,j.minus_score as 单次扣分,j.REMARK as 备注 from ht_qlt_inspect_proj r left join HT_QLT_INSPECT_STDD_SUB j on j.inspect_code = r.inspect_code  where  j.stdd_code = '" + listVersion.SelectedValue + "' and r.is_del = '0' and r.is_valid = '1' and r.inspect_type = '1' and  r.INspect_Group in ('1','2','3') union select  r.inspect_type as 检查类型,r.inspect_group as 分组, r.inspect_code as 检查项目编码,0 as 上限 ,0 as 下限,0 as 单次扣分,'' as 备注    from ht_qlt_inspect_proj r where r.is_del = '0' and r.is_valid = '1' and r.inspect_type = '1' and  r.INspect_Group in ('1','2','3') and r.inspect_code in   (select inspect_code from ht_qlt_inspect_proj where  r.is_del = '0' and r.is_valid = '1' and r.inspect_type = '1' and  r.INspect_Group in ('1','2','3') minus select inspect_code from HT_QLT_INSPECT_STDD_SUB where IS_DEL = '0'  and stdd_code = '" + listVersion.SelectedValue + "') order by 检查项目编码";

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
            string[] seg = { "INSPECT_CODE","STDD_CODE", "UPPER_VALUE", "LOWER_VALUE", "MINUS_SCORE", "REMARK", "CREATE_ID", "CREATE_TIME" };

            string[] value = { ((DropDownList)row.FindControl("listInspect")).SelectedValue,listVersion.SelectedValue, ((TextBox)row.FindControl("txtUpper")).Text, ((TextBox)row.FindControl("txtLower")).Text, ((TextBox)row.FindControl("txtScore")).Text, ((TextBox)row.FindControl("txtRemark")).Text, ((MSYS.Data.SysUser)Session["User"]).id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
            commandlist.Add(opt.getMergeStr(seg, value,2, "HT_QLT_INSPECT_STDD_SUB"));
        }
        string log_message = opt.TransactionCommand(commandlist) == "Success" ? "保存理化检测标准成功" : "保存理化检测项目标准失败";
        //if (log_message == "保存理化检测标准成功")
        //{
        //    string[] procseg = { };
        //    object[] procvalues = { };
        //    opt.ExecProcedures("Create_phychem_Report", procseg, procvalues);
        //}
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
                string query = "delete from  HT_QLT_INSPECT_STDD_SUB   where INSPECT_CODE = '" + projcode + "' and STDD_CODE = '" + listVersion.SelectedValue + "'";
                MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                string log_message = opt.UpDateOra(query) == "Success" ? "删除工艺检查标准成功" : "删除工艺检查标准失败";
                //if (log_message == "删除工艺检查标准成功")
                //{
                //    string[] procseg = { };
                //    object[] procvalues = { };
                //    opt.ExecProcedures("Create_phychem_Report", procseg, procvalues);
                //}
                log_message += "--标识:" + projcode;
                InsertTlog(log_message);
            }
        }
        bindGrid1();

    }
    #endregion


    #region tab3
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
        //string query = "select r.inspect_type as 检查类型,r.inspect_group as 分组, r.inspect_code as 检查项目编码,j.upper_value as 上限 ,j.lower_value as 下限,j.minus_score as 单次扣分,j.REMARK as 备注 from ht_qlt_inspect_proj r left join HT_QLT_INSPECT_STDD_SUB j on j.inspect_code = r.inspect_code  where  j.stdd_code = '" + listVersion.SelectedValue + "' and r.is_del = '0' and r.is_valid = '1' and r.inspect_type = '0' order by r.inspect_code";
        string query = "select r.inspect_type as 检查类型,r.inspect_group as 分组, r.inspect_code as 检查项目编码,j.upper_value as 上限 ,j.lower_value as 下限,j.minus_score as 单次扣分,j.REMARK as 备注 from ht_qlt_inspect_proj r left join HT_QLT_INSPECT_STDD_SUB j on j.inspect_code = r.inspect_code  where  j.stdd_code = '" + listVersion.SelectedValue + "' and r.is_del = '0' and r.is_valid = '1' and r.inspect_type = '0' union select  r.inspect_type as 检查类型,r.inspect_group as 分组, r.inspect_code as 检查项目编码,0 as 上限 ,0 as 下限,0 as 单次扣分,'' as 备注    from ht_qlt_inspect_proj r where r.is_del = '0' and r.is_valid = '1' and r.inspect_type = '0' and r.inspect_code in   (select inspect_code from ht_qlt_inspect_proj where  r.is_del = '0' and r.is_valid = '1' and r.inspect_type = '0' minus select inspect_code from HT_QLT_INSPECT_STDD_SUB where IS_DEL = '0'  and stdd_code = '" + listVersion.SelectedValue + "') order by 检查项目编码";

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
    protected void btnGrid2Save_Click(object sender, EventArgs e)
    {
        List<string> commandlist = new List<string>();
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        foreach (GridViewRow row in GridView2.Rows)
        {
            string[] seg = { "INSPECT_CODE", "STDD_CODE", "UPPER_VALUE", "LOWER_VALUE", "MINUS_SCORE", "REMARK", "CREATE_ID", "CREATE_TIME" };

            string[] value = { ((DropDownList)row.FindControl("listInspect")).SelectedValue,listVersion.SelectedValue, ((TextBox)row.FindControl("txtUpper")).Text, ((TextBox)row.FindControl("txtLower")).Text, ((TextBox)row.FindControl("txtScore")).Text, ((TextBox)row.FindControl("txtRemark")).Text, ((MSYS.Data.SysUser)Session["User"]).id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
            commandlist.Add(opt.getMergeStr(seg, value, 1, "HT_QLT_INSPECT_STDD_SUB"));
        }
        string log_message = opt.TransactionCommand(commandlist) == "Success" ? "保存过程检测标准成功" : "保存过程检测标准失败";
        //if (log_message == "保存过程检测标准成功")
        //{

        //    string[] procseg = { };
        //    object[] procvalues = { };
        //    opt.ExecProcedures("Create_process_Report", procseg, procvalues);
        //}
        InsertTlog(log_message);
        bindGrid2();
        ScriptManager.RegisterStartupScript(UpdatePanel5, this.Page.GetType(), "success", "alert('" + log_message + "')", true);
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
                string query = "delete from  HT_QLT_INSPECT_STDD_SUB   where INSPECT_CODE = '" + projcode + "' and STDD_CODE = '" + listVersion.SelectedValue + "'";
                MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                string log_message = opt.UpDateOra(query) == "Success" ? "删除工艺检查标准成功" : "删除工艺检查标准失败";
                //if (log_message == "删除工艺检查标准成功")
                //{

                //    string[] procseg = { };
                //    object[] procvalues = { };
                //    opt.ExecProcedures("Create_process_Report", procseg, procvalues);
                //}
                log_message += "--标识:" + projcode;
                InsertTlog(log_message);
            }
        }
        bindGrid2();

    }
    #endregion

}