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
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listFla, "select Formula_code, FORMULA_NAME from ht_qa_fla_formula where is_valid = '1' and is_del = '0'", "FORMULA_NAME", "Formula_code");
        opt.bindDropDownList(listcoat, "select  Formula_code, FORMULA_NAME  from HT_QA_coat_FORMULA where is_valid = '1' and is_del = '0'", "FORMULA_NAME", "Formula_code");
        opt.bindDropDownList(listMtrl, "select Formula_code, FORMULA_NAME  from ht_qa_mater_formula where is_valid = '1' and is_del = '0'", "FORMULA_NAME", "Formula_code");
        opt.bindDropDownList(listTechStd, "select distinct tech_code,tech_name from ht_tech_stdd_code where is_valid = '1' and is_del = '0'", "tech_name", "tech_code");
        opt.bindDropDownList(listqlt, "select QLT_CODE,QLT_NAME from ht_qlt_stdd_code where is_del = '0' and is_valid = '1'", "QLT_NAME", "QLT_CODE");
    }
    protected void bindGrid()
    {
        string query = "select PROD_CODE  as 产品编码,PROD_NAME  as 产品名称,PACK_NAME  as 包装规格,case HAND_MODE when '1' then '自主加工' when '2' then '来料加工' else hand_mode end  as 加工方式,case is_valid when '1' then '有效' else '无效' end as 是否有效,(case B_FLOW_STATUS when '-1' then '未提交' when '0' then '办理中' when '1' then '未通过' else '己通过' end) as 审批状态 from ht_pub_prod_design where is_del = '0'";
        if (txtCodeS.Text != "")
            query += " and prod_code = '" + txtCodeS.Text + "'";
        if (txtNameS.Text != "")
            query += " and proD_name = '" + txtNameS.Text + "'";
        if (rdValidS.Checked)
            query += " and is_valid = '1'";
        else
            query += " and is_valid = '0'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            for (int i = GridView1.PageSize * GridView1.PageIndex; i < GridView1.PageSize * (GridView1.PageIndex + 1) && i < GridView1.Rows.Count; i++)
            {
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                int j = i - GridView1.PageSize * GridView1.PageIndex;
                ((Label)GridView1.Rows[j].FindControl("labGrid1Status")).Text = mydrv["审批状态"].ToString();
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid();

    }



    protected void btnGrid1Del_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string prod_code = GridView1.DataKeys[rowIndex].Value.ToString();

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        List<String> commandlist = new List<string>();
        commandlist.Add("update ht_pub_prod_design set is_del = '1' where PROD_CODE = '" + prod_code + "'");
        commandlist.Add("delete from ht_pub_aprv_flowinfo where BUSIN_ID = '" + prod_code + "'");
        string log_message = opt.TransactionCommand(commandlist) == "Success" ? "删除产品成功" : "删除产品失败";
        log_message += "--标识:" + prod_code;
        InsertTlog(log_message);
        bindGrid();
    }
    protected void btnGridDetail_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string prod_code = GridView1.DataKeys[rowIndex].Value.ToString();
        string aprvstatus = ((Label)GridView1.Rows[rowIndex].FindControl("labGrid1Status")).Text;
      //  if (aprvstatus == "未提交")
     //       btnModify.Visible = true;
     //   else
     //       btnModify.Visible = false;
        string query = "select * from ht_pub_prod_design where PROD_CODE = '" + prod_code + "' and  is_del = '0'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            txtCode.Text = data.Tables[0].Rows[0]["PROD_CODE"].ToString();
            txtName.Text = data.Tables[0].Rows[0]["PROD_NAME"].ToString();
            txtPack.Text = data.Tables[0].Rows[0]["PACK_NAME"].ToString();
            listType.SelectedValue = data.Tables[0].Rows[0]["HAND_MODE"].ToString();
            listTechStd.SelectedValue = data.Tables[0].Rows[0]["TECH_STDD_CODE"].ToString();
            listMtrl.SelectedValue = data.Tables[0].Rows[0]["MATER_FORMULA_CODE"].ToString();
            listFla.SelectedValue = data.Tables[0].Rows[0]["FLA_FORMULA_CODE"].ToString();
            listcoat.SelectedValue = data.Tables[0].Rows[0]["Coat_FORMULA_CODE"].ToString();
            listqlt.SelectedValue = data.Tables[0].Rows[0]["QLT_CODE"].ToString();
            txtValue.Text = data.Tables[0].Rows[0]["STANDARD_VALUE"].ToString();
            txtDscpt.Text = data.Tables[0].Rows[0]["REMARK"].ToString();
            txtPathcode.Text = data.Tables[0].Rows[0]["PATH_CODE"].ToString();
            bindGrid4();
        }
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", " $('.shade').fadeIn(200);", true);
      

    }
    //查看审批单
    protected void btnFLow_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string ID = GridView1.DataKeys[rowIndex].Value.ToString();
        string query = "select pos as 顺序号, workitemid as 审批环节,username as 负责人,comments as 意见,opiniontime 审批时间,(case status when '0' then '未审批'  when '1' then '未通过' else '己通过' end)  as 审批状态  from ht_pub_aprv_opinion r left join ht_pub_aprv_flowinfo s on r.gongwen_id = s.id where s.busin_id  = '" + ID + "' order by pos";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        GridView3.DataSource = opt.CreateDataSetOra(query);
        GridView3.DataBind();
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "$('#flowinfo').fadeIn(200);", true);
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
            string[] subvalue = { "产品:" + GridView1.Rows[index].Cells[4].Text, "02", id, Page.Request.UserHostName.ToString() };
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string log_message = MSYS.AprvFlow.createApproval(subvalue) ? "提交审批成功," : "提交审批失败，";
            log_message += ",业务数据ID：" + id;
            InsertTlog(log_message);

            bindGrid();

        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        setBlank();
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string str = opt.GetSegValue("select Max(PROD_CODE) as code from ht_pub_prod_design where substr(Prod_code,0,4) = '703" + listType.SelectedValue + "'", "CODE");
        if (str == "")
            str = "0000000";
        txtCode.Text = "703" + listType.SelectedValue + (Convert.ToInt16(str.Substring(4)) + 1).ToString().PadLeft(3, '0');
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", " $('.shade').fadeIn(200);", true);

    }

    protected void setBlank()
    {
         txtCode.Text = ""; 
        txtName.Text = ""; 
        txtPack.Text = ""; 
        listType.SelectedValue = ""; 
        listTechStd.SelectedValue = ""; 
        listMtrl.SelectedValue = ""; 
        listFla.SelectedValue = ""; 
        listcoat.SelectedValue = ""; 
        listqlt.SelectedValue = ""; 
        txtValue.Text = "";
        txtDscpt.Text = ""; 
        
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        string[] seg = { "PROD_CODE", "PROD_NAME", "PACK_NAME", "HAND_MODE", "TECH_STDD_CODE", "MATER_FORMULA_CODE", "FLA_FORMULA_CODE", "COAT_FORMULA_CODE", "QLT_CODE", "STANDARD_VALUE", "REMARK", "CREATE_TIME", "PATH_CODE" };
        string[] value = { txtCode.Text, txtName.Text, txtPack.Text, listType.SelectedValue, listTechStd.SelectedValue, listMtrl.SelectedValue, listFla.SelectedValue, listcoat.SelectedValue, listqlt.SelectedValue, txtValue.Text, txtDscpt.Text ,System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),txtPathcode.Text};
        string log_message = opt.MergeInto(seg, value, 1, "HT_PUB_PROD_DESIGN") == "Success" ? "保存产品信息成功," : "保存产品信息失败,";
        log_message += ",产品信息：" + string.Join(",", value);
        InsertTlog(log_message);
        bindGrid();
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", " $('.shade').fadeOut(100);", true);

    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        List<string> commandlist = new List<string>();

        commandlist.Add("update HT_PUB_PROD_DESIGN set IS_DEL = '1'  where PROD_CODE = '" + txtCode.Text + "'");

        commandlist.Add("delete from ht_pub_aprv_flowinfo where BUSIN_ID = '" + txtCode.Text + "'");
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string log_message = opt.TransactionCommand(commandlist)== "Success" ? "删除产品信息成功" : "删除产品信息失败";
        log_message += ",产品编码:" + txtCode.Text;
        InsertTlog(log_message);       
        bindGrid();
    }


    protected void bindGrid4()
    {
        string query = "select g.section_name as 工艺段, '' as 路径选择, '' as 路径详情,g.section_code from ht_pub_tech_section g  where g.is_valid = '1' and g.is_del = '0' and g.IS_PATH_CONFIG = '1' order by g.section_code";
       
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView4.DataSource = data;
        GridView4.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {           
            string[] subpath = txtPathcode.Text.Split('-');
            for (int i = 0; i < GridView4.Rows.Count; i++)
            {
                
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                ((TextBox)GridView4.Rows[i].FindControl("txtSection")).Text = mydrv["工艺段"].ToString();
                DropDownList list = (DropDownList)GridView4.Rows[i].FindControl("listpath");
                opt.bindDropDownList(list, "select pathname,pathcode from ht_pub_path_section where section_code = '" + mydrv["section_code"].ToString() + "'", "pathname", "pathcode");
                list.SelectedValue = mydrv["路径详情"].ToString();
                if (subpath.Length == GridView4.Rows.Count)
                    list.SelectedValue = subpath[i];
                query = createQuery(mydrv["section_code"].ToString());
                if (query != "")
                {
                    query += " and pathcode = '" + list.SelectedValue + "'";

                    DataSet set = opt.CreateDataSetOra(query);
                    if (set != null && set.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 1; j < set.Tables[0].Columns.Count - 2; j++)
                        {
                            CheckBox ck = new CheckBox();
                            if (0 == set.Tables[0].Rows.Count)
                                ck.Checked = false;
                            else
                                ck.Checked = (set.Tables[0].Rows[0][j].ToString() == "1");

                            ck.Text = set.Tables[0].Columns[j].Caption;
                            GridView4.Rows[i].Cells[2].Controls.Add(ck);
                        }
                    }
                }
            }
        }
        ScriptManager.RegisterStartupScript(UpdatePanel2,this.Page.GetType(),"disable"," $(\"input[type$='checkbox']\").attr('disabled', 'disabled');",true);

    }//绑定GridView4数据源

    protected void listpath_SelectedIndexChanged(object sender, EventArgs e)
    {

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        for (int i = 0; i < GridView4.Rows.Count; i++)
        {
            DataSet set = opt.CreateDataSetOra("select * from HT_PUB_PATH_NODE where SECTION_CODE ='" + GridView4.DataKeys[i].Value.ToString() + "' and is_del = '0'");
            DropDownList list = (DropDownList)GridView4.Rows[i].FindControl("listpath");
            string pathcode = list.SelectedValue;
            if (set != null && set.Tables[0].Rows.Count > 0)
            {
                if (pathcode.Length < set.Tables[0].Rows.Count)
                    pathcode = pathcode.PadRight(set.Tables[0].Rows.Count, '0');
                for (int j = 0; j < set.Tables[0].Rows.Count; j++)
                {
                    CheckBox ck = new CheckBox();
                    // ck.Enabled = false;               
                    ck.Text = set.Tables[0].Rows[j]["NODENAME"].ToString();
                    GridView4.Rows[i].Cells[2].Controls.Add(ck);
                    if (pathcode.Length > 0)
                        ck.Checked = (pathcode.Substring(j, 1) == "1");
                    else
                        ck.Checked = false;
                }
            }
        }
        txtPathcode.Text = getPathCode();
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "disable", " $(\"input[type$='checkbox']\").attr('disabled', 'disabled');", true);
    }
 
    protected string getPathCode()
    {       
        string pathcode = "";
        for (int i = 0; i < GridView4.Rows.Count; i++)
        {
            if (i > 0)
                pathcode += "-";
            DropDownList list = (DropDownList)GridView4.Rows[i].FindControl("listpath");
            if (list.SelectedValue != "")
            {
                pathcode += list.SelectedValue;
            }
            else
            {
                return "";               
            }
        }
        return pathcode;  
    }
  
    protected string createQuery(string section)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select nodename from ht_pub_path_node where section_code = '" + section + "' and is_del = '0' order by orders");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string query = "select PATHNAME as 路径名称";
            int i = 1;
            foreach (DataRow row in data.Tables[0].Rows)
            {
                query += ",substr(pathcode," + i.ToString() + ",1) as " + row[0].ToString();
                i++;
            }
            query += ",SECTION_CODE,pathcode  from ht_pub_path_section where section_code = '" + section + "'";
            return query;
        }
        else
            return "";
    }
   

}