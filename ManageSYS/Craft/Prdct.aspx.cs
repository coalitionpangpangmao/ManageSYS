using System;
using System.Collections.Generic;
using System.Linq;
using MSYS.Web.MateriaService;
using MSYS.Web.EquipService;
using MSYS.Web.StoreService;
using MSYS.Web.PlanService;
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
        opt.bindDropDownList(listisp, "select inspect_stdd_code,inspect_stdd_name from ht_qlt_inspect_stdd t where t.is_del = '0' order by t.inspect_stdd_code", "inspect_stdd_name", "inspect_stdd_code");
        opt.bindDropDownList(listPathAll, "select distinct pathname,pathcode  from ht_pub_path_prod t where t.is_del = '0' order by pathname", "pathname", "pathcode");
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        InsertLocalFromMaster();
        bindGrid();
        initView();
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "alert('同步完成');", true);
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
        /*if (data != null && data.Tables[0].Rows.Count > 0)
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
            listisp.SelectedValue = data.Tables[0].Rows[0]["INSPECT_STDD"].ToString();
            txtValue.Text = data.Tables[0].Rows[0]["SENSOR_SCORE"].ToString();
            txtDscpt.Text = data.Tables[0].Rows[0]["REMARK"].ToString();
            if (listPathAll.Items.FindByValue(data.Tables[0].Rows[0]["PATH_CODE"].ToString()) != null)
                listPathAll.SelectedValue = data.Tables[0].Rows[0]["PATH_CODE"].ToString();
            else
                listPathAll.SelectedValue = "";
            bindGrid4();
        }*/
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            txtCode.Text = data.Tables[0].Rows[0]["PROD_CODE"].ToString();
            txtName.Text = data.Tables[0].Rows[0]["PROD_NAME"].ToString();
            txtPack.Text = data.Tables[0].Rows[0]["PACK_NAME"].ToString();
            listType.SelectedValue = data.Tables[0].Rows[0]["HAND_MODE"].ToString();
            if (data.Tables[0].Rows[0]["TECH_STDD_CODE"].ToString() != "" && data.Tables[0].Rows[0]["TECH_STDD_CODE"].ToString().Length>3)
               listTechStd.SelectedValue = data.Tables[0].Rows[0]["TECH_STDD_CODE"].ToString();
            if (data.Tables[0].Rows[0]["MATER_FORMULA_CODE"].ToString() != "")
                listMtrl.SelectedValue = data.Tables[0].Rows[0]["MATER_FORMULA_CODE"].ToString();
            if (data.Tables[0].Rows[0]["FLA_FORMULA_CODE"].ToString() != "")
                listFla.SelectedValue = data.Tables[0].Rows[0]["FLA_FORMULA_CODE"].ToString();
            if (data.Tables[0].Rows[0]["Coat_FORMULA_CODE"].ToString() != "")
                listcoat.SelectedValue = data.Tables[0].Rows[0]["Coat_FORMULA_CODE"].ToString();
            if (data.Tables[0].Rows[0]["QLT_CODE"].ToString() != "" && data.Tables[0].Rows[0]["QLT_CODE"].ToString().Length>3)
                listqlt.SelectedValue = data.Tables[0].Rows[0]["QLT_CODE"].ToString();
            if (data.Tables[0].Rows[0]["INSPECT_STDD"].ToString() != ""&& data.Tables[0].Rows[0]["INSPECT_STDD"].ToString().Length>3)
                listisp.SelectedValue = data.Tables[0].Rows[0]["INSPECT_STDD"].ToString();
            txtValue.Text = data.Tables[0].Rows[0]["SENSOR_SCORE"].ToString();
            txtDscpt.Text = data.Tables[0].Rows[0]["REMARK"].ToString();
            if(data.Tables[0].Rows[0]["PATH_CODE"].ToString() != "")
                listPathAll.SelectedValue = data.Tables[0].Rows[0]["PATH_CODE"].ToString();
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
        listisp.SelectedValue = "";
        txtValue.Text = "";
        txtDscpt.Text = "";

    }
    protected void btnModify_Click(object sender, EventArgs e)
    {

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        string[] seg = { "PROD_CODE", "PROD_NAME", "PACK_NAME", "HAND_MODE", "TECH_STDD_CODE", "MATER_FORMULA_CODE", "FLA_FORMULA_CODE", "COAT_FORMULA_CODE", "QLT_CODE", "INSPECT_STDD", "SENSOR_SCORE", "REMARK", "CREATE_TIME", "PATH_CODE" };
        string[] value = { txtCode.Text, txtName.Text, txtPack.Text, listType.SelectedValue, listTechStd.SelectedValue, listMtrl.SelectedValue, listFla.SelectedValue, listcoat.SelectedValue, listqlt.SelectedValue, listisp.SelectedValue, txtValue.Text, txtDscpt.Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), listPathAll.SelectedValue };
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
        string log_message = opt.TransactionCommand(commandlist) == "Success" ? "删除产品信息成功" : "删除产品信息失败";
        log_message += ",产品编码:" + txtCode.Text;
        InsertTlog(log_message);
        bindGrid();
    }


    protected void bindGrid4()
    {
        string query = "select distinct g.section_name as 工艺段, '' as 路径选择, '' as 路径详情,g.section_code from ht_pub_tech_section g left join ht_pub_path_section h on h.section_code = g.section_code where h.section_code is not null and  g.is_valid = '1' and g.is_del = '0' and g.IS_PATH_CONFIG = '1' order by g.section_code";

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView4.DataSource = data;
        GridView4.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string[] subpath = listPathAll.SelectedValue.Split('-');
            for (int i = 0; i < GridView4.Rows.Count; i++)
            {

                DataRowView mydrv = data.Tables[0].DefaultView[i];
                ((TextBox)GridView4.Rows[i].FindControl("txtSection")).Text = mydrv["工艺段"].ToString();
                DropDownList list = (DropDownList)GridView4.Rows[i].FindControl("listpath");
                opt.bindDropDownList(list, "select pathname,pathcode from ht_pub_path_section where section_code = '" + mydrv["section_code"].ToString() + "'", "pathname", "pathcode");
                list.SelectedValue = mydrv["路径详情"].ToString();
                if (subpath.Length == GridView4.Rows.Count)
                    list.SelectedValue = subpath[i].Substring(5);
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
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "disable", " $(\"input[type$='checkbox']\").attr('disabled', 'disabled');", true);

    }//绑定GridView4数据源
    protected void listPathAll_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGrid4();
    }
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
        listPathAll.SelectedValue = getPathCode();
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
        DataSet data = opt.CreateDataSetOra("select nodename,orders from ht_pub_path_node where section_code = '" + section + "' and is_del = '0' order by orders");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string query = "select PATHNAME as 路径名称";
            int i = 1;
            foreach (DataRow row in data.Tables[0].Rows)
            {
                query += ",substr(pathcode," + i.ToString() + ",1) as " + row[0].ToString();
                i++;
            }
            query += ",SECTION_CODE,pathcode  from ht_pub_path_section where section_code = '" + section + "' and is_del = '0'";
            return query;
        }
        else
            return "";
    }

    public string InsertLocalFromMaster()
    {

        MSYS.Web.MateriaService.WsBaseDataInterfaceService service = new MSYS.Web.MateriaService.WsBaseDataInterfaceService();
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        List<string> commandlist = new List<string>();
        productEntity[] prods = service.getAllProductList(new productEntity());

        string[] matseg = { "ID", "PROD_CODE", "PROD_NAME", "PACK_NAME", "HAND_MODE",  "MATER_FORMULA_CODE", "AUX_FORMULA_CODE", "COAT_FORMULA_CODE", "REMARK", "CREATEOR_ID", "CREATE_TIME", "MODIFY_ID", "MODIFY_TIME", "STANDARD_VALUE", "XY_PROD_CODE", "IS_VALID", "IS_DEL", "B_FLOW_STATUS" };
        int SucCount = 0;

        foreach (productEntity prod in prods)
        {
            // if (prod.prodCode.Substring(0, 3) == "703")
            // {
            commandlist.Clear();
            if (prod.xyProdCode.Substring(0, 3) != "703")
                continue;
            string mformcode = ((prod.materFormulaCode == null) || prod.materFormulaCode.Length <= 3) ? "" : "7030" + (Convert.ToInt32(prod.materFormulaCode)+20).ToString();
            string aformcode = ((prod.auxFormulaCode == null) || prod.auxFormulaCode.Length <= 3) ? "" : "703" + prod.auxFormulaCode;
            string cformcode = ((prod.coatFormulaCode == null) || prod.coatFormulaCode.Length <= 3) ? "" : "7030" + (Convert.ToInt32(prod.coatFormulaCode)+3).ToString();
            string[] value = { prod.id.ToString(),prod.xyProdCode, prod.prodName, prod.packName, Convert.ToInt32(prod.handMode).ToString()
, mformcode, aformcode, cformcode, prod.remark, prod.createorId, prod.createTime.ToString("yyyy-MM-dd HH:mm:ss"), prod.modifyId, prod.modifyTime.ToString("yyyy-MM-dd HH:mm:ss"), prod.standardValue,prod.prodCode, "1", prod.isDel ,"2"};

            string temp = opt.getMergeStr(matseg, value, 2, "HT_PUB_PROD_DESIGN");
            commandlist.Add(temp);
            if (opt.UpDateOra(temp) != "Success")
                System.Diagnostics.Debug.Write(temp);
            //getTechstdd_SQL(prod.techStddId, prod.xyProdCode); // java.math.BigInteger cannot be cast to java.lang.String
            getMaterFormalu_SQL(prod.materFormulaId, prod.xyProdCode);
            getAuxFormalu_SQL(prod.auxFormulaId, prod.xyProdCode); //java.math.BigDecimal cannot be cast to java.lang.Double
            getCoatFormalu_SQL(prod.coatFormulaId, prod.xyProdCode);
            if (opt.TransactionCommand(commandlist) == "Success")
            {
                commandlist.Clear();
                //commandlist.Add("update ht_pub_prod_design t set tech_stdd_code = (select r.tech_code from ht_tech_stdd_code r where substr(t.tech_stdd_code,0,3)<>'TCH' and  r.id = to_number( t.tech_stdd_code)),mater_formula_code = (select s.formula_code from ht_qa_mater_formula s where  t.mater_formula_code is not null and  s.id = to_number( t.mater_formula_code)),aux_formula_code = (select  q.formula_code from ht_qa_aux_formula q where  t.aux_formula_code is not null and  q.id = to_number( t.aux_formula_code)),coat_formula_code = (select p.formula_code from ht_qa_coat_formula p where  t.coat_formula_code is not null and  p.id = to_number( t.coat_formula_code)) where t.prod_code = '" + prod.prodCode + "'");
                commandlist.Add("update ht_qa_mater_formula_detail r set formula_code = (select t.formula_code from ht_qa_mater_formula t where t.id = r.formula_code) where r.formula_code = '" + prod.materFormulaId + "'");
                commandlist.Add("update ht_qa_aux_formula_detail r set formula_code = (select t.formula_code from ht_qa_aux_formula t where t.id = r.formula_code) where r.formula_code = '" + prod.auxFormulaId + "'");
                commandlist.Add("update ht_qa_coat_formula_detail r set formula_code = (select t.formula_code from ht_qa_coat_formula t where t.id = r.formula_code) where r.formula_code = '" + prod.coatFormulaId + "'");
                commandlist.Add("update ht_qa_Fla_formula_detail r set formula_code = (select t.formula_code from ht_qa_Fla_formula t where t.id = r.formula_code) where r.formula_code = '" + prod.coatFormulaId + "'");
                //commandlist.Add("update ht_tech_stdd_code_detail r set tech_code = (select t.tech_code from ht_tech_stdd_code t where t.id = r.tech_code) where r.tech_code = '" + prod.techStddId + "'");
                //commandlist.Add("update ht_qa_mater_formula_detail t set t.mater_flag = (select r.mat_type from ht_pub_materiel r  where r.material_code = t.mater_code)");
                opt.TransactionCommand(commandlist);
                System.Diagnostics.Debug.Write("产品更新成功" + prod.prodCode + prod.prodName);
                SucCount++;
            }
            //  }

        }
        return SucCount.ToString() + "项产品更新成功,总记录条数：" + prods.Length;

    }
    //根据产品ID获取工艺标准信息并更新。
    protected static void getTechstdd_SQL(string id, string prodcode)
    {
        MSYS.Web.MateriaService.WsBaseDataInterfaceService service = new MSYS.Web.MateriaService.WsBaseDataInterfaceService();
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        //   List<string> commandlist = new List<string>();
        try
        {
            techStddInfoVO tech = service.getTechStdById(id);

            if (tech != null)
            {
                tQaTechStdd info = tech.techStddInfo;
                string[] seg = { "ID", "TECH_CODE", "TECH_NAME", "STANDARD_VOL", "REMARK", "PROD_CODE", "MODIFY_ID", "MODIFY_TIME", "IS_VALID", "IS_DEL", "E_DATE", "CREATE_ID", "CREATE_DEPT_ID", "CREATE_DATE", "CONTROL_STATUS", "B_DATE" };
                string[] value = { id, info.standardCode, info.techStddName, info.standardVol, info.remark, prodcode, info.modifyId, info.modifyTime.ToString("yyyy-MM-dd HH:mm:ss"), info.isValid, info.isDel, info.EDate.ToString("yyyy-MM-dd HH:mm:ss"), info.createId, info.createDept, info.createDate.ToString("yyyy-MM-dd HH:mm:ss"), info.controlStatus, info.BDate.ToString("yyyy-MM-dd HH:mm:ss") };

                string temp = opt.getMergeStr(seg, value, 2, "HT_TECH_STDD_CODE");
                //    commandlist.Add(temp);
                if (opt.UpDateOra(temp) != "Success")
                    System.Diagnostics.Debug.Write(temp);
                if (tech.techStdDetails != null && tech.techStdDetails.Length > 0)
                {
                    string[] subseg = { "ID", "TECH_CODE", "PARA_CODE", "PARA_TYPE", "REMARK", "VALUE", "UPPER_LIMIT", "LOWER_LIMIT", "UNIT" };
                    foreach (techStddDetail detail in tech.techStdDetails)
                    {
                        if (detail.baseDown != "" && detail.baseUp != "")
                        {
                            string stdvalue = detail.centerVal == "" ? ((Convert.ToDouble(detail.baseDown) + Convert.ToDouble(detail.baseUp)) / 2).ToString() : detail.centerVal;
                            string[] subvalue = { detail.id, info.standardCode, detail.projCode, detail.techParmType, detail.remark, stdvalue, detail.baseUp, detail.baseDown, detail.bzUnit };
                            temp = opt.getMergeStr(subseg, subvalue, 1, "HT_TECH_STDD_CODE_DETAIL");
                            //        commandlist.Add(temp);
                            if (opt.UpDateOra(temp) != "Success")
                                System.Diagnostics.Debug.Write(temp);
                        }
                    }

                }
                /*            "TECH_STDD_ID" IS '工艺技术标准主表id';
            "TECH_PRAM_TYPE" IS '技术参数类型';
            "PROJ_CODE" IS '工序项目编码';
            "PARM_STDD" IS '工艺参数或质量标准';
            "CENTER_VAL" IS '中心值';
            "BIAS_UP" IS '上偏值';
            "BIAS_DOWN" IS '下偏值';
            "STDD_VALUE" IS '工艺标准值';
            "PASS_PERCENT" IS '合格率';
            "BZ_UNIT" IS '计量单位';
            "SORT" IS '排序';
            "REMARK" IS '备注';*/
            }

        }
        catch (Exception ee)
        {

        }


    }

    protected static void getMaterFormalu_SQL(string id, string prodCode)
    {
        MSYS.Web.MateriaService.WsBaseDataInterfaceService service = new MSYS.Web.MateriaService.WsBaseDataInterfaceService();
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        //   List<string> commandlist = new List<string>();
        materFormulaVO info = service.getMatFormulaById(id);
        string temp;
        if (info != null)
        {
            string[] seg = { "ID", "FORMULA_CODE", "FORMULA_NAME", "ADJUST", "B_DATE", "CABO_SUM", "CONTROL_STATUS", "CREATE_DATE", "CREATE_DEPT_ID", "CREATE_ID", "E_DATE", "EXECUTEBATCH", "FLOW_STATUS", "IS_DEL", "IS_VALID", "MODIFY_ID", "MODIFY_TIME", "PIECE_NUM", "PIECES_SUM", "PROD_CODE", "REMARK", "SMALLS_NUM", "STANDARD_VOL", "STEM_NUM", "STICKS_NUM" };
           // string formcode = info.formulaCode == "" ? "" : "703" + info.formulaCode; //"703" + (Convert.ToInt32(prod.materFormulaCode)+12).ToString()
            string formcode = info.formulaCode == "" ? "" : "7030" + (Convert.ToInt32(info.formulaCode) + 20).ToString();
            string[] value = { id,formcode,info.formulaName,info.adjust,info.BDate.ToString("yyyy-MM-dd HH:mm:ss"),info.caboSum.ToString(),info.controlStatus,info.createDate,info.createDept,info.createId,info.EDate.ToString("yyyy-MM-dd HH:mm:ss"),info.executeBatch.ToString(),info.flowStatus,info.isDel,"1",
                                     info.modifyId,info.modifyTime,info.pieceNum.ToString(),info.piecesSum.ToString(),prodCode,info.remark,info.smallsNum.ToString(),info.standardVol,info.stemNum.ToString(),info.sticksNum.ToString()};
            temp = opt.getMergeStr(seg, value, 2, "HT_QA_MATER_FORMULA");
            //     commandlist.Add(temp);
            if (opt.UpDateOra(temp) != "Success")
                System.Diagnostics.Debug.Write(temp);


            if (info.ygSubList != null && info.ygSubList.Length > 0)
            {
                string[] subseg = { "ID", "MATER_CODE", "BATCH_SIZE", "FRONT_GROUP", "IS_DEL", "MATER_FLAG", "FORMULA_CODE", "MATER_SORT", "REMARK" };
                //string[] materseg = { "MATERIAL_CODE", "MATERIAL_NAME" };
                foreach (tQaMaterFormulaDetail detail in info.ygSubList)
                {
                    //string[] subvalue = { detail.id.ToString(), detail.materCode, detail.batchSize.ToString(), detail.frontGroup, "0", detail.materFlag, "703" + info.formulaCode, detail.materSort.ToString(), detail.remark };
                    string[] subvalue = { detail.id.ToString(), detail.materCode, detail.batchSize.ToString(), detail.frontGroup, "0", detail.materFlag, formcode, detail.materSort.ToString(), detail.remark };
                    //string[] matervalue = { detail.materCode.ToString(), detail.materName.ToString() };
                    string matersql = "update HT_PUB_MATERIEL set material_name = '"+detail.materName+"' where material_code = '"+detail.materCode+"'";
                    opt.CreateDataSetOra(matersql);
                    //opt.getMergeStr(materseg, matervalue, 2, "HT_PUB_MATERIEL");
                    System.Diagnostics.Debug.WriteLine(detail.materFlag);
                    
                    temp = opt.getMergeStr(subseg, subvalue, 2, "HT_QA_MATER_FORMULA_DETAIL");
                    //       commandlist.Add(temp);
                    if (opt.UpDateOra(temp) != "Success")
                        System.Diagnostics.Debug.Write(temp);
                }
            }
            if (info.spSubList != null && info.spSubList.Length > 0)
            {
                //string[] subseg = { "ID", "MATER_CODE", "BATCH_SIZE", "FRONT_GROUP", "IS_DEL", "MATER_FLAG", "FORMULA_CODE", "MATER_SORT", "REMARK" };
                string[] subseg = { "ID", "FORMULA_CODE", "MATER_CODE", "BATCH_SIZE", "FRONT_GROUP", "IS_DEL", "MATER_FLAG", "MATER_SORT", "REMARK" };
                foreach (tQaMaterFormulaDetail detail in info.spSubList)
                {
                    //string[] subvalue = { detail.id.ToString(), detail.materCode, detail.batchSize.ToString(), detail.frontGroup, detail.isDel, detail.materFlag, id, detail.materSort.ToString(), detail.remark };
                   // string[] subvalue = { detail.id.ToString(), id, detail.materCode, detail.batchSize.ToString(), detail.frontGroup, "0", detail.materFlag, detail.materSort.ToString(), detail.remark };
                    string[] subvalue = { detail.id.ToString(), formcode, detail.materCode, detail.batchSize.ToString(), detail.frontGroup, "0", detail.materFlag, detail.materSort.ToString(), detail.remark };
                    string matersql = "update HT_PUB_MATERIEL set material_name = '" + detail.materName + "' where material_code = '" + detail.materCode + "'";
                    opt.CreateDataSetOra(matersql);
                    temp = opt.getMergeStr(subseg, subvalue, 2, "HT_QA_MATER_FORMULA_DETAIL");
                    //         commandlist.Add(temp);
                    if (opt.UpDateOra(temp) != "Success")
                        System.Diagnostics.Debug.Write(temp);
                }
            }
        }

    }


    protected static void getAuxFormalu_SQL(string id, string prodCode)
    {
        MSYS.Web.MateriaService.WsBaseDataInterfaceService service = new MSYS.Web.MateriaService.WsBaseDataInterfaceService();
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        //  List<string> commandlist = new List<string>();
        try
        {
            auxFormulaVO info = service.getAuxFormulaById(id);
            string temp;
            if (info != null)
            {
                string[] seg = { "ID", "FORMULA_CODE", "FORMULA_NAME", "B_DATE", "CONTROL_STATUS", "CREATE_DATE", "CREATE_DEPT_ID", "CREATE_ID", "E_DATE", "IS_DEL", "IS_VALID", "MODIFY_ID", "MODIFY_TIME", "PROD_CODE", "REMARK", "STANDARD_VOL" };
                //string formcode = info.formulaCode == "" ? "" : "703" + info.formulaCode;//"703" + (Convert.ToInt32(prod.coatFormulaCode)+3).ToString();
                string formcode = info.formulaCode == "" ? "" : "703" + (Convert.ToInt32(info.formulaCode) + 3).ToString();
                string[] value = { id, formcode, info.formulaName, info.BDate.ToString("yyyy-MM-dd HH:mm:ss"), info.controlStatus, info.createDate, info.createDept, info.createId, info.EDate.ToString("yyyy-MM-dd HH:mm:ss"), info.isDel, "1", info.modifyId, info.modifyTime, prodCode, info.remark, info.standardVol };
                temp = opt.getMergeStr(seg, value, 2, "HT_QA_AUX_FORMULA");
                //    commandlist.Add(temp);
                if (opt.UpDateOra(temp) != "Success")
                    System.Diagnostics.Debug.Write(temp);
                if (info.auxSubList != null && info.auxSubList.Length > 0)
                {
                    string[] subseg = { "ID", "MATER_CODE", "FORMULA_CODE", "AUX_PERCENT", "AUX_SCALE", "AUX_SORT", "IS_DEL", "MATER_TYPE", "mattreeName", "REMARK" };
                    foreach (tQaAuxFormulaDetail detail in info.auxSubList)
                    {
                        string[] subvalue = { detail.id.ToString(), detail.materCode, "703" + info.formulaCode, detail.auxPercent.ToString(), detail.auxScale.ToString(), detail.auxSort.ToString(), detail.isDel, detail.materType, detail.mattreeName, detail.remark };
                        temp = opt.getMergeStr(subseg, subvalue, 3, "HT_QA_AUX_FORMULA_DETAIL");
                        //         commandlist.Add(temp);
                        if (opt.UpDateOra(temp) != "Success")
                            System.Diagnostics.Debug.Write(temp);
                    }
                }
            }
            // return commandlist;
        }
        catch (Exception ee)
        {

        }

    }


    protected static void getCoatFormalu_SQL(string id, string prodCode)
    {
        MSYS.Web.MateriaService.WsBaseDataInterfaceService service = new MSYS.Web.MateriaService.WsBaseDataInterfaceService();
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        List<string> commandlist = new List<string>();
        try
        {
            coatFormulaVO info = service.getCoatFormulaById(id);
            string temp;
            if (info != null)
            {
                //插入回填液
                string[] seg = { "ID", "FORMULA_CODE", "FORMULA_NAME", "B_DATE", "CONTROL_STATUS", "CREATE_DATE", "CREATE_DEPT_ID", "CREATE_ID", "E_DATE", "IS_DEL", "IS_VALID", "MODIFY_ID", "MODIFY_TIME", "PROD_CODE", "REMARK", "STANDARD_VOL", "FORMULA_TPY", "FORMULA_XJ", "W_TOTAL" };
                //string formcode = info.formulaCode == "" ? "" : "703" + info.formulaCode;//"703" + (Convert.ToInt32(prod.coatFormulaCode)+3).ToString()
                string formcode = info.formulaCode == "" ? "" : "7030" + (Convert.ToInt32(info.formulaCode) + 3).ToString();
                string[] value = { id, formcode, info.formulaName, info.BDate.ToString("yyyy-MM-dd HH:mm:ss"), info.controlStatus, info.createDate, info.createDept, info.createId, info.EDate.ToString("yyyy-MM-dd HH:mm:ss"), info.isDel, "1", info.modifyId, info.modifyTime, prodCode, info.remark, info.standardVol, info.formulaTpy.ToString(), info.formulaXj.ToString(), info.WTotal.ToString() };
                temp = opt.getMergeStr(seg, value, 2, "HT_QA_COAT_FORMULA");
                commandlist.Add(temp);
                if (opt.UpDateOra(temp) != "Success")
                    System.Diagnostics.Debug.Write(temp);

                if (info.coatTBYSubList != null && info.coatTBYSubList.Length > 0)
                {
                    // string[] subseg = { "ID", "MATER_CODE", "CLASS_NAME", "COAT_FLAG", "FORMULA_CODE", "COAT_SCALE", "COAT_SORT", "IS_DEL", "IS_VALID", "NEED_SIZE", "REMARK" };
                    string[] subseg = { "ID", "FORMULA_CODE", "MATER_CODE", "CLASS_NAME", "COAT_FLAG", "COAT_SCALE", "COAT_SORT", "IS_DEL", "IS_VALID", "NEED_SIZE", "REMARK" };
                    foreach (tQaCoatFormulaDetail detail in info.coatTBYSubList)
                    {
                        // string[] subvalue = { detail.id.ToString(), detail.classCode, detail.className, detail.coatFlag, info.formulaCode, detail.coatScale, detail.coatSort.ToString(), detail.isDel, detail.isValid, detail.needSize.ToString(), detail.remark };
                        //string[] subvalue = { detail.id.ToString(), "703" + info.formulaCode, detail.classCode, detail.className, "HT", detail.coatScale, detail.coatSort.ToString(), "0", "1", detail.needSize.ToString(), detail.remark };
                        string[] subvalue = { detail.id.ToString(), formcode, detail.classCode, detail.className, "HT", detail.coatScale, detail.coatSort.ToString(), "0", "1", detail.needSize.ToString(), detail.remark };
                        temp = opt.getMergeStr(subseg, subvalue, 2, "HT_QA_COAT_FORMULA_DETAIL");
                        commandlist.Add(temp);
                        if (opt.UpDateOra(temp) != "Success")
                            System.Diagnostics.Debug.Write(temp);
                    }
                }
                if (info.coatXJSubList.Length <= 0) {
                    return;
                }
                //插入香精香料
                string[] segX = { "ID", "FORMULA_CODE", "FORMULA_NAME", "B_DATE", "CONTROL_STATUS", "CREATE_DATE", "CREATE_DEPT_ID", "CREATE_ID", "E_DATE", "IS_DEL", "IS_VALID", "MODIFY_ID", "MODIFY_TIME", "PROD_CODE", "REMARK", "STANDARD_VOL", "FORMULA_TPY", "FORMULA_XJ", "W_TOTAL" };
                string xformcode = info.formulaCode == "" ? "" : "70309" + info.formulaCode.Substring(2);
                string[] valueX = { id, xformcode, "香精香料（新）", info.BDate.ToString("yyyy-MM-dd HH:mm:ss"), info.controlStatus, info.createDate, info.createDept, info.createId, info.EDate.ToString("yyyy-MM-dd HH:mm:ss"), info.isDel, "1", info.modifyId, info.modifyTime, prodCode, info.remark, info.standardVol, info.formulaTpy.ToString(), info.formulaXj.ToString(), info.WTotal.ToString() };
                temp = opt.getMergeStr(segX, valueX, 2, "HT_QA_Fla_FORMULA");
                string upsql = "update ht_pub_prod_design set fla_formula_code = '" + xformcode + "' where prod_code = '" + prodCode + "'";
                opt.CreateDataSetOra(upsql);
                commandlist.Add(temp);
                if (opt.UpDateOra(temp) != "Success")
                  System.Diagnostics.Debug.Write(temp);
                if (info.coatXJSubList != null && info.coatXJSubList.Length > 0)
                {
                    // string[] subseg = { "ID", "MATER_CODE", "CLASS_NAME", "COAT_FLAG", "FORMULA_CODE", "COAT_SCALE", "COAT_SORT", "IS_DEL", "IS_VALID", "NEED_SIZE", "REMARK" };
                    string[] subseg = { "ID", "FORMULA_CODE", "MATER_CODE", "CLASS_NAME", "COAT_FLAG", "COAT_SCALE", "COAT_SORT", "IS_DEL", "IS_VALID", "NEED_SIZE", "REMARK" };
                    foreach (tQaCoatFormulaDetail detail in info.coatXJSubList)
                    {
                        //string[] subvalue = { detail.id.ToString(), detail.classCode, detail.className, detail.coatFlag, id, detail.coatScale, detail.coatSort.ToString(), detail.isDel, detail.isValid, detail.needSize.ToString(), detail.remark };
                        string[] subvalue = { detail.id.ToString(), xformcode, detail.classCode, detail.className, "XJ", detail.coatScale, detail.coatSort.ToString(), "0", "1", detail.needSize.ToString(), detail.remark };
                        temp = opt.getMergeStr(subseg, subvalue, 2, "HT_QA_Fla_FORMULA_DETAIL");
                        //  commandlist.Add(temp);
                        if (opt.UpDateOra(temp) != "Success")
                          System.Diagnostics.Debug.Write(temp);
                    }
                }
            }

        }
        catch (Exception ee)
        {

        }
    }



}