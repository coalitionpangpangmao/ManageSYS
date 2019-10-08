using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Craft_Recipe : MSYS.Web.BasePage
{
    protected string tvHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            tvHtml = InitTree();
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listPro1, "select PROD_CODE,PROD_NAME from ht_pub_prod_design t where is_del = '0' ", "PROD_NAME", "PROD_CODE");
            opt.bindDropDownList(listStatus1, "select * from HT_INNER_BOOL_DISPLAY t", "CTRL_NAME", "ID");
            opt.bindDropDownList(listCrtApt1, "select F_CODE,F_NAME from ht_svr_org_group ", "F_NAME", "F_CODE");
            opt.bindDropDownList(listCreator1, "select s.name,s.id from ht_svr_user s   order by id desc", "Name", "ID");
            opt.bindDropDownList(listPro2, "select PROD_CODE,PROD_NAME from ht_pub_prod_design t where is_del = '0' ", "PROD_NAME", "PROD_CODE");
            opt.bindDropDownList(listStatus2, "select * from HT_INNER_BOOL_DISPLAY t", "CTRL_NAME", "ID");
            opt.bindDropDownList(listCrtApt2, "select F_CODE,F_NAME from ht_svr_org_group order by f_code  ", "F_NAME", "F_CODE");
            opt.bindDropDownList(listCreator2, "select s.name,s.id from ht_svr_user s   order by id desc", "Name", "ID");
            opt.bindDropDownList(listPro3, "select PROD_CODE,PROD_NAME from ht_pub_prod_design t where is_del = '0' ", "PROD_NAME", "PROD_CODE");
            opt.bindDropDownList(listStatus3, "select * from HT_INNER_BOOL_DISPLAY t", "CTRL_NAME", "ID");
            opt.bindDropDownList(listCrtApt3, "select F_CODE,F_NAME from ht_svr_org_group order by f_code  ", "F_NAME", "F_CODE");
            opt.bindDropDownList(listCreator3, "select s.name,s.id from ht_svr_user s   order by id desc", "Name", "ID");
        }


    }

    #region model
    public string InitTree()
    {

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select prod_code,prod_name from ht_pub_prod_design where is_del = '0' order by prod_code");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul id='browser' class='filetree treeview-famfamfam'>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                tvHtml += "<li ><span class='folder'  value = '" + row["prod_code"].ToString() + "'>" + row["prod_name"].ToString() + "</span>";

                tvHtml += InitTreeRecipe(row["prod_code"].ToString());
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }
    public string InitTreeRecipe(string prod_code)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        //DataSet data = opt.CreateDataSetOra("select formula_code as 配方编码，formula_name as 配方名称,b_date as 启用时间,CREATE_ID as 编辑人员,is_valid as 是否有效  from ht_qa_mater_formula where prod_code ='" + prod_code + "' and is_del ='0' union select formula_code as 配方编码，formula_name as 配方名称,b_date as 启用时间,CREATE_ID as 编辑人员,is_valid as 是否有效  from ht_qa_coat_formula where prod_code = '" + prod_code + "' and is_del ='0'  union select formula_code as 配方编码，formula_name as 配方名称,b_date as 启用时间,CREATE_ID as 编辑人员,is_valid as 是否有效  from ht_qa_FLA_formula  where prod_code = '" + prod_code + "'  and is_del ='0'");
        DataSet data = opt.CreateDataSetOra("select h.formula_code as 配方编码，h.formula_name as 配方名称,h.b_date as 启用时间,h.CREATE_ID as 编辑人员,h.is_valid as 是否有效  from ht_qa_mater_formula h left join ht_pub_prod_design s on h.formula_code = s.mater_formula_code where s.prod_code ='" + prod_code + "' and h.is_del ='0' union select formula_code as 配方编码，formula_name as 配方名称,b_date as 启用时间,CREATE_ID as 编辑人员,is_valid as 是否有效  from ht_qa_coat_formula where prod_code = '" + prod_code + "' and is_del ='0'  union select formula_code as 配方编码，formula_name as 配方名称,b_date as 启用时间,CREATE_ID as 编辑人员,is_valid as 是否有效  from ht_qa_FLA_formula  where prod_code = '" + prod_code + "'  and is_del ='0'");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                tvHtml += "<li ><span class='file'  value = '" + row["配方编码"].ToString() + "'>" + row["配方名称"].ToString() + "</span></li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        tvHtml = InitTree();

        ScriptManager.RegisterStartupScript(UpdatePanel5, this.Page.GetType(), "updatetree", " initTree();", true);
    }
    #endregion
    #region tab1
    protected void btnCkAll_Click(object sender, EventArgs e)
    {
        int ckno = 0;
        for (int i = 0; i < GridViewAll.Rows.Count; i++)
        {
            if (((CheckBox)GridViewAll.Rows[i].FindControl("chk")).Checked)
                ckno++;
        }
        bool check = (ckno < GridViewAll.Rows.Count);
        for (int i = 0; i < GridViewAll.Rows.Count; i++)
        {
            ((CheckBox)GridViewAll.Rows[i].FindControl("chk")).Checked = check;

        }
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        for (int i = 0; i < GridViewAll.Rows.Count; i++)
        {
            CheckBox ck = (CheckBox)GridViewAll.Rows[i].FindControl("chk");
            if (ck.Checked == true)
            {
                string recipeno = GridViewAll.Rows[i].Cells[4].Text;
                List<String> commandlist = new List<String>();
                switch (recipeno.Substring(0, 5))
                {
                    case "70306":
                        commandlist.Add("update ht_qa_mater_formula set is_del = '1' where formula_code = '" + recipeno + "'");
                        commandlist.Add("delete from ht_pub_aprv_flowinfo where BUSIN_ID = '" + recipeno + "'");
                        commandlist.Add("update ht_pub_Prod_design set MATER_FORMULA_CODE = '' where MATER_FORMULA_CODE = '" + recipeno + "'");

                        break;
                    case "70308":
                        commandlist.Add("update ht_qa_coat_formula set is_del = '1' where formula_code = '" + recipeno + "'");
                        commandlist.Add("delete from ht_pub_aprv_flowinfo where BUSIN_ID = '" + recipeno + "'");
                        commandlist.Add("update ht_pub_Prod_design set COAT_FORMULA_CODE = '' where COAT_FORMULA_CODE = '" + recipeno + "'");
                        break;
                    default:
                        commandlist.Add("update ht_qa_FLA_formula set is_del = '1' where formula_code = '" + recipeno + "'");
                        commandlist.Add("delete from ht_pub_aprv_flowinfo where BUSIN_ID = '" + recipeno + "'");
                        commandlist.Add("update ht_pub_Prod_design set FLA_FORMULA_CODE = '' where FLA_FORMULA_CODE = '" + recipeno + "'");
                        break;
                }
                string log_message = opt.TransactionCommand(commandlist) == "Success" ? "删除配方成功" : "删除配方失败";
                log_message += "--标识:" + recipeno;
                InsertTlog(log_message);
            }

        }
        bindGrid();
        ScriptManager.RegisterStartupScript(UpdatePanelall, this.Page.GetType(), "updatetree", "  $('#btnUpdate').click();", true);
    }
    protected void bindGrid()
    {
        string query = "select r.formula_code as 配方编码，r.formula_name as 配方名称,r.b_date as 启用时间,r.CREATE_ID as 编辑人员,s.name as 审批状态  from ht_qa_mater_formula r left join ht_inner_aprv_status s on s.id = r.flow_status  where r.prod_code ='" + hdcode.Value + "' and r.is_del ='0' union select r.formula_code as 配方编码，r.formula_name as 配方名称,r.b_date as 启用时间,r.CREATE_ID as 编辑人员,s.name as 审批状态   from ht_qa_FLA_formula r left join ht_inner_aprv_status s on s.id = r.flow_status  where r.prod_code = '" + hdcode.Value + "' and r.is_del ='0'  union select r.formula_code as 配方编码，r.formula_name as 配方名称,r.b_date as 启用时间,r.CREATE_ID as 编辑人员,s.name as 审批状态  from ht_qa_coat_formula r left join ht_inner_aprv_status s on s.id = r.flow_status   where r.prod_code = '" + hdcode.Value + "'  and r.is_del ='0'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridViewAll.DataSource = data;
        GridViewAll.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < GridViewAll.Rows.Count; i++)
            {
                DataRowView mydrv = data.Tables[0].DefaultView[i];

                ((Label)GridViewAll.Rows[i].FindControl("labAprv")).Text = mydrv["审批状态"].ToString();

                if (!(mydrv["审批状态"].ToString() == "未提交" || mydrv["审批状态"].ToString() == "未通过"))
                {
                    ((Button)GridViewAll.Rows[i].FindControl("btnSubmit")).Enabled = false;
                    ((Button)GridViewAll.Rows[i].FindControl("btnSubmit")).CssClass = "btngrey";

                }
                else
                {
                    ((Button)GridViewAll.Rows[i].FindControl("btnSubmit")).Enabled = true;
                    ((Button)GridViewAll.Rows[i].FindControl("btnSubmit")).CssClass = "btn1 auth";

                }
            }
        }

    }
    protected void btnGridDetail_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string formula_code = GridViewAll.DataKeys[rowIndex].Value.ToString();
        if (formula_code.Substring(0, 5) == "70306")
        {
            ScriptManager.RegisterStartupScript(btnUpdateList, this.Page.GetType(), "", " $('#tabtop2').click();$('#hdcode1').attr('value', '" + formula_code + "');$('#btnUpdate1').click();", true);
        }
        else if (formula_code.Substring(0, 5) == "70308")
        {
            ScriptManager.RegisterStartupScript(btnUpdateList, this.Page.GetType(), "", " $('#tabtop3').click();$('#hdcode2').attr('value', '" + formula_code + "');$('#btnUpdate2').click();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(btnUpdateList, this.Page.GetType(), "", " $('#tabtop4').click();$('#hdcode3').attr('value', '" + formula_code + "');$('#btnUpdate3').click();", true);
        }

    }
    protected void btnUpdateList_Click(object sender, EventArgs e)
    {
        bindGrid();
        bindData1();
        bindData2();
        bindData3();
    }

    //查看审批单
    protected void btnFLow_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string ID = GridViewAll.DataKeys[rowIndex].Value.ToString();
        string query = "select pos as 顺序号, workitemid as 审批环节,username as 负责人,comments as 意见,opiniontime 审批时间,(case status when '0' then '未审批'  when '1' then '未通过' else '己通过' end)  as 审批状态  from ht_pub_aprv_opinion r left join ht_pub_aprv_flowinfo s on r.gongwen_id = s.id where s.busin_id  = '" + ID + "' order by pos";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        GridView4.DataSource = opt.CreateDataSetOra(query);
        GridView4.DataBind();
        ScriptManager.RegisterStartupScript(UpdatePanelall, this.Page.GetType(), "", "$('#flowinfo').fadeIn(200);", true);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)//提交审批
    {
        try
        {
            Button btn = (Button)sender;
            int index = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号                 
            string id = GridViewAll.DataKeys[index].Value.ToString();
            string mode = "03";
            if (id.Substring(0, 5) == "70306")
                mode = "03";
            else if (id.Substring(0, 5) == "70308")
                mode = "11";
            else
                mode = "10";
            /*启动审批TB_ZT标题,MODULENAME审批类型编码,BUSIN_ID业务数据id,URL 单独登录url*/
            //"TB_ZT", "MODULENAME", "BUSIN_ID",  "URL"
            string[] subvalue = { "配方:" + GridViewAll.Rows[index].Cells[4].Text, mode, id, Page.Request.UserHostName.ToString() };
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
    #endregion

    #region tab2
    protected void bindData1()
    {
        string query;
        if (hdcode1.Value.Length == 8)
            query = "select FORMULA_CODE  as 配方编号,FORMULA_NAME  as 配方名称,PROD_CODE  as 产品编码,STANDARD_VOL  as 标准版本号,B_DATE  as 执行日期,E_DATE  as 结束日期,CONTROL_STATUS  as 受控状态,CREATE_ID  as 编制人,CREATE_DATE  as 编制日期,CREATE_DEPT_ID  as 编制部门,REMARK  as 备注,is_valid from ht_qa_mater_formula where is_del = '0' and FORMULA_CODE = '" + hdcode1.Value + "'";
        else
            query = "select FORMULA_CODE  as 配方编号,FORMULA_NAME  as 配方名称,PROD_CODE  as 产品编码,STANDARD_VOL  as 标准版本号,B_DATE  as 执行日期,E_DATE  as 结束日期,CONTROL_STATUS  as 受控状态,CREATE_ID  as 编制人,CREATE_DATE  as 编制日期,CREATE_DEPT_ID  as 编制部门,REMARK  as 备注,is_valid from ht_qa_mater_formula where is_del = '0' and PROD_CODE = '" + hdcode1.Value + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.UpDateOra("delete from ht_qa_mater_formula_detail where is_valid = '0'");
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            txtCode1.Text = data.Tables[0].Rows[0]["配方编号"].ToString();
            txtName1.Text = data.Tables[0].Rows[0]["配方名称"].ToString();
            listPro1.SelectedValue = data.Tables[0].Rows[0]["产品编码"].ToString();
            txtVersion1.Text = data.Tables[0].Rows[0]["标准版本号"].ToString();
            txtExeDate1.Text = data.Tables[0].Rows[0]["执行日期"].ToString();
            txtEndDate1.Text = data.Tables[0].Rows[0]["结束日期"].ToString();
            //  listStatus.SelectedValue = data.Tables[0].Rows[0]["受控状态"].ToString();
            listCreator1.SelectedValue = data.Tables[0].Rows[0]["编制人"].ToString();
            txtCrtDate1.Text = data.Tables[0].Rows[0]["编制日期"].ToString();
            listCrtApt1.SelectedValue = data.Tables[0].Rows[0]["编制部门"].ToString();
            txtDscpt1.Text = data.Tables[0].Rows[0]["备注"].ToString();
            ckValid1.Checked = ("1" == data.Tables[0].Rows[0]["is_valid"].ToString());

        }
        else
        {
            txtCode1.Text = "";
            txtName1.Text = "";
            listPro1.SelectedValue = "";
            txtVersion1.Text = "";
            txtExeDate1.Text = "";
            txtEndDate1.Text = "";
            //  listStatus.SelectedValue = data.Tables[0].Rows[0]["受控状态"].ToString();
            listCreator1.SelectedValue = "";
            txtCrtDate1.Text = "";
            listCrtApt1.SelectedValue = "";
            txtDscpt1.Text = "";
            ckValid1.Checked = false;
        }
        bindGrid1();
        bindGrid1_2();
    }

    protected void grid1Databind(DataTable data, GridView gv)
    {

        gv.DataSource = data;
        gv.DataBind();
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        if (data != null && data.Rows.Count > 0)
        {
            for (int i = gv.PageSize * gv.PageIndex; i < gv.PageSize * (gv.PageIndex + 1) && i < data.Rows.Count; i++)
            {
                int j = i - gv.PageSize * gv.PageIndex;
                DataRowView mydrv = data.DefaultView[i];
                GridViewRow row = gv.Rows[j];
               // DropDownList list = (DropDownList)row.FindControl("listGridType1");
                //list.SelectedValue = mydrv["物料分类"].ToString();
                //opt.bindDropDownList((DropDownList)row.FindControl("listGridName1"), "select material_code,material_name from ht_pub_materiel  where  is_del = '0' and mat_category = '原材料' and substr(TYPE_CODE,1,4) ='" + list.SelectedValue + "'", "material_name", "material_code");
                ((TextBox)row.FindControl("txtCodeM")).Text = mydrv["物料编码"].ToString();
                ((TextBox)row.FindControl("GridName1")).Text = mydrv["物料名称"].ToString();
                ((TextBox)row.FindControl("txtAmountM")).Text = mydrv["批投料量"].ToString();
                ((TextBox)row.FindControl("txtGroupM")).Text = mydrv["优先组"].ToString();
                ((TextBox)row.FindControl("GridType1")).Text = mydrv["物料分类"].ToString();
            }
        }

    }
    /*protected DataSet gridTypebind(string code)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select mattree_name,mattree_code from ht_pub_mattree t where length(mattree_code) = 4 and parent_code = '02' and mattree_code ='" + code + "' union select '' as mattree_name,'' as mattree_code from dual  order by mattree_code desc");
        //  return opt.CreateDataSetOra("select material_code,material_name from ht_pub_materiel  where  is_del = '0' and mat_category = '原材料'");      
    }*/
    protected DataSet gridTypebind(String code)
    {
        string s1_1 = code.Replace(",", "','"); //返回结果为：1','2','3','4','5','6
        code = string.Format("'{0}'", s1_1);
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        return opt.CreateDataSetOra("select mattree_name,mattree_code from ht_pub_mattree t where length(mattree_code) = 4 and parent_code = '02' and mattree_code in (" + code + ") union select '' as mattree_name,'' as mattree_code from dual  order by mattree_code desc");
    }
    protected void listGirdName1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList list = (DropDownList)sender;
        GridViewRow row = (GridViewRow)list.NamingContainer;

        ((TextBox)row.FindControl("txtCodeM")).Text = list.SelectedValue;
    }
    protected void listGridType1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList list = (DropDownList)sender;
        GridViewRow row = (GridViewRow)list.NamingContainer;
        if (list.SelectedValue != "")
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.bindDropDownList((DropDownList)row.FindControl("listGridName1"), "select material_code,material_name from ht_pub_materiel  where  is_del = '0' and mat_category = '原材料' and substr(TYPE_CODE,1,4) ='" + list.SelectedValue + "'", "material_name", "material_code");
        }
    }
    protected void btnUpdate1_Click(object sender, EventArgs e)
    {
        bindData1();
    }
    protected void btnModify1_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        hdcode1.Value = txtCode1.Text;


        string[] seg = { "FORMULA_CODE", "FORMULA_NAME", "PROD_CODE", "STANDARD_VOL", "B_DATE", "E_DATE", "CONTROL_STATUS", "CREATE_ID", "CREATE_DATE", "CREATE_DEPT_ID", "REMARK" };
        string[] value = { txtCode1.Text, txtName1.Text, listPro1.SelectedValue, txtVersion1.Text, txtExeDate1.Text, txtEndDate1.Text, listStatus1.SelectedValue, listCreator1.SelectedValue, txtCrtDate1.Text, listCrtApt1.SelectedValue, txtDscpt1.Text, };
        List<String> commandlist = new List<string>();
        commandlist.Add(opt.getMergeStr(seg, value, 1, "ht_qa_mater_formula"));
        commandlist.Add("update ht_pub_prod_design set mater_formula_code = '" + txtCode1.Text + "' where prod_code = '" + listPro1.SelectedValue + "'");
        string log_message = opt.TransactionCommand(commandlist) == "Success" ? "原料配方保存成功，" : "原料配方保存失败，";
        log_message += "--详情：" + string.Join(",", value);
        InsertTlog(log_message);

        bindGrid1();
        bindGrid1_2();
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "updatetree", " $('#btnUpdate').click();", true);
    }

    protected void bindGrid1()
    {
        string query;
        if (hdcode1.Value.Length == 8)
        {
            query = "select r.MATER_CODE   as 物料编码,s.material_name as 物料名称,r.BATCH_SIZE  as 批投料量,r.FRONT_GROUP   as 优先组,s.mat_type   as 物料分类,r.id from ht_qa_mater_formula_detail r left join ht_pub_materiel s on s.material_code = r.mater_code where r.is_del = '0' and  r.MATER_FLAG = 'YG' and FORMULA_CODE = '" + hdcode1.Value + "' order by r.id";
            //query = "select r.MATER_CODE   as 物料编码,s.material_name as 物料名称,r.BATCH_SIZE  as 批投料量,r.FRONT_GROUP   as 优先组,r.CLS_CODE   as 物料分类,r.id from ht_qa_mater_formula_detail r left join ht_pub_materiel s on s.material_code = r.mater_code where r.is_del = '0' and  FORMULA_CODE = '" + hdcode1.Value + "' order by r.id";
        }
        else
        {
            query = "select distinct r.MATER_CODE   as 物料编码,s.material_name as 物料名称,r.BATCH_SIZE  as 批投料量,r.FRONT_GROUP   as 优先组,s.mat_type   as 物料分类,r.id from ht_qa_mater_formula_detail r left join ht_pub_materiel s on s.material_code = r.mater_code left join ht_qa_mater_formula t on t.FORMULA_CODE = r.FORMULA_CODE where r.is_del = '0' and r.MATER_FLAG = 'YG' and t.is_del ='0'  and t.PROD_CODE = '" + hdcode1.Value + "'  order by r.id";
            //query = "select distinct r.MATER_CODE   as 物料编码,s.material_name as 物料名称,r.BATCH_SIZE  as 批投料量,r.FRONT_GROUP   as 优先组,r.CLS_CODE   as 物料分类,r.id from ht_qa_mater_formula_detail r left join ht_pub_materiel s on s.material_code = r.mater_code left join ht_qa_mater_formula t on t.FORMULA_CODE = r.FORMULA_CODE where r.is_del = '0' and t.is_del ='0'  and t.PROD_CODE = '" + hdcode1.Value + "'  order by r.id";
        }
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
    
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null)
            grid1Databind(data.Tables[0], GridView1);
    }
    protected void bindGrid1_2()
    {
        string query;
        if (hdcode1.Value.Length == 8)
            query = "select  r.MATER_CODE   as 物料编码,s.material_name as 物料名称,r.BATCH_SIZE  as 批投料量,r.FRONT_GROUP   as 优先组,s.mat_type   as 物料分类,r.id from ht_qa_mater_formula_detail r left join ht_pub_materiel s on s.material_code = r.mater_code where r.is_del = '0' and r.MATER_FLAG = 'SP' and FORMULA_CODE = '" + hdcode1.Value + "'  order by r.id";
        else
            query = "select distinct r.MATER_CODE   as 物料编码,s.material_name as 物料名称,r.BATCH_SIZE  as 批投料量,r.FRONT_GROUP   as 优先组,s.mat_type   as 物料分类,r.id from ht_qa_mater_formula_detail r left join ht_pub_materiel s on s.material_code = r.mater_code left join ht_qa_mater_formula t on t.FORMULA_CODE = r.FORMULA_CODE where r.is_del = '0' and t.is_del ='0' and r.MATER_FLAG = 'SP'  and t.PROD_CODE = '" + hdcode1.Value + "'  order by r.id";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
      
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null)
            grid1Databind(data.Tables[0], GridView1_2);
    }
    protected void btnAdd1_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string id = opt.GetSegValue(" select MFORMULA_DT_ID_SEQ.nextval as id from dual", "ID");
        string[] seg = { "id", "IS_VALID", "MATER_FLAG", "FORMULA_CODE" };
        string[] value = { id, "0", "YG", txtCode1.Text };
        opt.MergeInto(seg, value, 1, "ht_qa_mater_formula_detail");

        string query;

        if (hdcode1.Value.Length == 8)
            query = "select r.MATER_CODE   as 物料编码,s.material_name as 物料名称,r.BATCH_SIZE  as 批投料量,r.FRONT_GROUP   as 优先组,r.CLS_CODE   as 物料分类,r.id from ht_qa_mater_formula_detail r left join ht_pub_materiel s on s.material_code = r.mater_code where r.is_del = '0' and  r.MATER_FLAG = 'YG' and FORMULA_CODE = '" + hdcode1.Value + "'  order by r.id";
        else
            query = "select distinct r.MATER_CODE   as 物料编码,s.material_name as 物料名称,r.BATCH_SIZE  as 批投料量,r.FRONT_GROUP   as 优先组,r.CLS_CODE   as 物料分类,r.id from ht_qa_mater_formula_detail r left join ht_pub_materiel s on s.material_code = r.mater_code left join ht_qa_mater_formula t on t.FORMULA_CODE = r.FORMULA_CODE where r.is_del = '0' and r.MATER_FLAG = 'YG' and t.is_del ='0'  and t.PROD_CODE = '" + hdcode1.Value + "'  order by r.id";

        DataSet data = opt.CreateDataSetOra(query);
        if (data != null)
            grid1Databind(data.Tables[0], GridView1);
    }
    protected void btnAdd1_2_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string id = opt.GetSegValue(" select MFORMULA_DT_ID_SEQ.nextval as id from dual", "ID");
        string[] seg = { "id", "IS_VALID", "MATER_FLAG", "FORMULA_CODE" };
        string[] value = { id, "0", "SP", txtCode1.Text };
        opt.MergeInto(seg, value, 1, "ht_qa_mater_formula_detail");

        string query;
        if (hdcode1.Value.Length == 8)
            query = "select r.MATER_CODE   as 物料编码,s.material_name as 物料名称,r.BATCH_SIZE  as 批投料量,r.FRONT_GROUP   as 优先组,r.CLS_CODE   as 物料分类,r.id from ht_qa_mater_formula_detail r left join ht_pub_materiel s on s.material_code = r.mater_code where r.is_del = '0' and  r.MATER_FLAG = 'SP' and FORMULA_CODE = '" + hdcode1.Value + "'  order by r.id";
        else
            query = "select distinct r.MATER_CODE   as 物料编码,s.material_name as 物料名称,r.BATCH_SIZE  as 批投料量,r.FRONT_GROUP   as 优先组,r.CLS_CODE   as 物料分类,r.id from ht_qa_mater_formula_detail r left join ht_pub_materiel s on s.material_code = r.mater_code left join ht_qa_mater_formula t on t.FORMULA_CODE = r.FORMULA_CODE where r.is_del = '0' and r.MATER_FLAG = 'SP' and t.is_del ='0'  and t.PROD_CODE = '" + hdcode1.Value + "'  order by r.id";

        DataSet data = opt.CreateDataSetOra(query);
        if (data != null)
            grid1Databind(data.Tables[0], GridView1_2);
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

        bindGrid1();
    }
    protected void GridView1_2_PageIndexChanging(object sender, GridViewPageEventArgs e)
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

        bindGrid1_2();
    }



    protected void btnDel1_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号  
            string mtr_code = ((TextBox)GridView1.Rows[Rowindex].FindControl("txtCodeM")).Text;
            string query;
            if (mtr_code == "")
                query = "delete from HT_QA_MATER_FORMULA_DETAIL where id= '" + GridView1.DataKeys[Rowindex].Value.ToString() + "'";
            else
                query = "update HT_QA_MATER_FORMULA_DETAIL set IS_DEL = '1'  where FORMULA_CODE = '" + txtCode1.Text + "' and MATER_CODE = '" + mtr_code + "'";
          
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string log_message = opt.UpDateOra(query) == "Success" ? "删除配方详情成功，" : "删除配方详情失败,";
            log_message += ",物料编码：" + mtr_code;
            InsertTlog(log_message);
            bindGrid1();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnDel1_2_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号  
            string mtr_code = ((TextBox)GridView1_2.Rows[Rowindex].FindControl("txtCodeM")).Text;
            string query;
            if (mtr_code == "")
                query = "delete from HT_QA_MATER_FORMULA_DETAIL where id= '" + GridView1_2.DataKeys[Rowindex].Value.ToString() + "'";
            else
                query = "update HT_QA_MATER_FORMULA_DETAIL set IS_DEL = '1'  where FORMULA_CODE = '" + txtCode1.Text + "' and MATER_CODE = '" + mtr_code + "'";
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string log_message = opt.UpDateOra(query) == "Success" ? "删除配方详情成功，" : "删除配方详情失败,";
            log_message += ",物料编码：" + mtr_code;
            InsertTlog(log_message);
            bindGrid1_2();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnCkAll1_Click(object sender, EventArgs e)
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
    protected void btnCkAll1_2_Click(object sender, EventArgs e)
    {
        int ckno = 0;
        for (int i = 0; i < GridView1_2.Rows.Count; i++)
        {
            if (((CheckBox)GridView1_2.Rows[i].FindControl("chk")).Checked)
                ckno++;
        }
        bool check = (ckno < GridView1_2.Rows.Count);
        for (int i = 0; i < GridView1_2.Rows.Count; i++)
        {
            ((CheckBox)GridView1_2.Rows[i].FindControl("chk")).Checked = check;

        }
    }
    protected void btnDelSel1_Click(object sender, EventArgs e)
    {

        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            if (((CheckBox)GridView1.Rows[i].FindControl("chk")).Checked)
            {
                string mtr_code = ((TextBox)GridView1.Rows[i].FindControl("txtCodeM")).Text;
                string query;
                if (mtr_code == "")
                    query = "delete from HT_QA_MATER_FORMULA_DETAIL where id= '" + GridView1.DataKeys[i].Value.ToString() + "'";
                else
                    query = "update HT_QA_MATER_FORMULA_DETAIL set IS_DEL = '1'  where FORMULA_CODE = '" + txtCode1.Text + "' and MATER_CODE = '" + mtr_code + "'";
                MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                string log_message = opt.UpDateOra(query) == "Success" ? "删除配方详情成功" : "删除配方详情失败";
                log_message += ",物料编码：" + txtCode1.Text;
                InsertTlog(log_message);
            }
        }
        bindGrid1();

    }
    protected void btnDelSel1_2_Click(object sender, EventArgs e)
    {

        for (int i = 0; i <= GridView1_2.Rows.Count - 1; i++)
        {
            if (((CheckBox)GridView1_2.Rows[i].FindControl("chk")).Checked)
            {
                string mtr_code = ((TextBox)GridView1_2.Rows[i].FindControl("txtCodeM")).Text;
                string query;
                if (mtr_code == "")
                    query = "delete from HT_QA_MATER_FORMULA_DETAIL where id= '" + GridView1_2.DataKeys[i].Value.ToString() + "'";
                else
                    query = "update HT_QA_MATER_FORMULA_DETAIL set IS_DEL = '1'  where FORMULA_CODE = '" + txtCode1.Text + "' and MATER_CODE = '" + mtr_code + "'";
                MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                string log_message = opt.UpDateOra(query) == "Success" ? "删除配方详情成功" : "删除配方详情失败";
                log_message += ",物料编码：" + txtCode1.Text;
                InsertTlog(log_message);
            }
        }
        bindGrid1();

    }
    protected void btnGridSave1_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            // if (((CheckBox)row.FindControl("chk")).Checked)
            //  {
            string mtr_code = ((TextBox)row.FindControl("txtCodeM")).Text;
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string[] seg = { "ID", "FORMULA_CODE", "MATER_CODE", "BATCH_SIZE", "FRONT_GROUP", "CLS_CODE", "IS_DEL", "MATER_FLAG", "IS_VALID" };
            string[] value = { GridView1.DataKeys[row.RowIndex].Value.ToString(), txtCode1.Text, mtr_code, ((TextBox)row.FindControl("txtAmountM")).Text, ((TextBox)row.FindControl("txtGroupM")).Text, ((DropDownList)row.FindControl("listGridType1")).SelectedValue, "0", "YG", "1" };
            string log_message = opt.MergeInto(seg, value, 2, "ht_qa_mater_formula_detail") == "Success" ? "物料保存成功" : "物料保存失败";
            log_message += ",物料编码：" + txtCode1.Text;
            InsertTlog(log_message);
            //  }
        }
        bindGrid1();
    }
    protected void btnGridSave1_2_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1_2.Rows)
        {
            // if (((CheckBox)row.FindControl("chk")).Checked)
            //  {
            string mtr_code = ((TextBox)row.FindControl("txtCodeM")).Text;
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string[] seg = { "ID", "FORMULA_CODE", "MATER_CODE", "BATCH_SIZE", "FRONT_GROUP", "CLS_CODE", "IS_DEL", "MATER_FLAG", "IS_VALID" };
            string[] value = { GridView1_2.DataKeys[row.RowIndex].Value.ToString(), txtCode1.Text, mtr_code, ((TextBox)row.FindControl("txtAmountM")).Text, ((TextBox)row.FindControl("txtGroupM")).Text, ((DropDownList)row.FindControl("listGridType1")).SelectedValue, "0", "SP", "1" };
            string log_message = opt.MergeInto(seg, value, 2, "ht_qa_mater_formula_detail") == "Success" ? "物料保存成功" : "物料保存失败";
            log_message += ",物料编码：" + txtCode1.Text;
            InsertTlog(log_message);
            //  }
        }
        bindGrid1_2();
    }
    protected void btnSave1_Click(object sender, EventArgs e)
    {

        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        string mtr_code = ((TextBox)row.FindControl("txtCodeM")).Text;
        if (row.RowIndex >= 0)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

            string[] seg = { "ID", "FORMULA_CODE", "MATER_CODE", "BATCH_SIZE", "FRONT_GROUP", "CLS_CODE", "IS_DEL", "MATER_FLAG", "IS_VALID" };
            string[] value = { GridView1.DataKeys[row.RowIndex].Value.ToString(), txtCode1.Text, mtr_code, ((TextBox)row.FindControl("txtAmountM")).Text, ((TextBox)row.FindControl("txtGroupM")).Text, ((DropDownList)row.FindControl("listGridType1")).SelectedValue, "0", "YG", "1" };
            string log_message = opt.MergeInto(seg, value, 2, "ht_qa_mater_formula_detail") == "Success" ? "物料保存成功" : "物料保存失败";
            log_message += ",物料编码：" + txtCode1.Text;
            InsertTlog(log_message);

            bindGrid1();
        }
    }

    protected void btnSave1_2_Click(object sender, EventArgs e)
    {

        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        string mtr_code = ((TextBox)row.FindControl("txtCodeM")).Text;
        if (row.RowIndex >= 0)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

            string[] seg = { "ID", "FORMULA_CODE", "MATER_CODE", "BATCH_SIZE", "FRONT_GROUP", "CLS_CODE", "IS_DEL", "MATER_FLAG", "IS_VALID" };
            string[] value = { GridView1_2.DataKeys[row.RowIndex].Value.ToString(), txtCode1.Text, mtr_code, ((TextBox)row.FindControl("txtAmountM")).Text, ((TextBox)row.FindControl("txtGroupM")).Text, ((DropDownList)row.FindControl("listGridType1")).SelectedValue, "0", "SP", "1" };
            string log_message = opt.MergeInto(seg, value, 2, "ht_qa_mater_formula_detail") == "Success" ? "物料保存成功" : "物料保存失败";
            log_message += ",物料编码：" + txtCode1.Text;
            InsertTlog(log_message);

            bindGrid1_2();
        }
    }
    protected void btnAddR1_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string str = opt.GetSegValue("select Max(Formula_code) as code from Ht_Qa_Mater_Formula ", "CODE");
        if (str == "")
            str = "00000000";
        txtCode1.Text = "70306" + (Convert.ToInt16(str.Substring(5)) + 1).ToString().PadLeft(3, '0');
        MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
        listCreator1.SelectedValue = user.id;
        txtCrtDate1.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
        listCrtApt1.SelectedValue = user.OwningBusinessUnitId;

        txtName1.Text = "";
        txtVersion1.Text = "";
        txtExeDate1.Text = "";
        txtEndDate1.Text = "";
        listStatus1.SelectedValue = "";
        txtDscpt1.Text = "";
        ckValid1.Checked = false;
    }
    #endregion

    #region tab3
    protected void grid2Databind(DataTable data, GridView gv)
    {
        gv.DataSource = data;
        gv.DataBind();

        if (data != null && data.Rows.Count > 0)
        {
            for (int i = gv.PageSize * gv.PageIndex; i < gv.PageSize * (gv.PageIndex + 1) && i < data.Rows.Count; i++)
            {
                int j = i - gv.PageSize * gv.PageIndex;
                DataRowView mydrv = data.DefaultView[i];
                GridViewRow row = gv.Rows[j];
                ((DropDownList)row.FindControl("listGridName2")).SelectedValue = mydrv["香料种类"].ToString();
                ((TextBox)row.FindControl("txtCodeM")).Text = mydrv["香料种类"].ToString();
                ((TextBox)row.FindControl("txtScale")).Text = mydrv["比例"].ToString();
                ((TextBox)row.FindControl("txtPercent")).Text = mydrv["每罐调配所需"].ToString();
                ((TextBox)row.FindControl("txtBatchNum")).Text = mydrv["每批用量"].ToString();
                ((TextBox)row.FindControl("remark")).Text = mydrv["备注"].ToString();

            }
        }
    }
  
    protected DataSet gridHTYbind(String code)   
    {
        string s1_1 = code.Replace(",", "','"); //返回结果为：1','2','3','4','5','6
        code = string.Format("'{0}'", s1_1);
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select material_code,material_name from ht_pub_materiel where substr(TYPE_CODE,1,4) in (" + code + ") or substr(material_code,1,4) in (" + code + ") and is_del = '0' union select '' as material_code, '' as material_name from dual order by material_code desc");
    }
    protected void btnUpdate2_Click(object sender, EventArgs e)//没有实现
    {
        bindData2();
    }

    protected void btnModify2_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        hdcode2.Value = txtCode2.Text;
        {
            string[] seg = { "FORMULA_CODE", "FORMULA_NAME", "PROD_CODE", "STANDARD_VOL", "B_DATE", "E_DATE", "CONTROL_STATUS", "CREATE_ID", "CREATE_DATE", "CREATE_DEPT_ID", "REMARK" };
            string[] value = { txtCode2.Text, txtName2.Text, listPro2.SelectedValue, txtVersion2.Text, txtExeDate2.Text, txtEndDate2.Text, listStatus2.SelectedValue, listCreator2.SelectedValue, txtCrtDate2.Text, listCrtApt2.SelectedValue, txtDscpt2.Text, };
            List<String> commandlist = new List<string>();
            commandlist.Add(opt.getMergeStr(seg, value, 1, "ht_qa_coat_formula"));
            commandlist.Add("update ht_pub_prod_design set coat_formula_code = '" + txtCode2.Text + "' where prod_code = '" + listPro2.SelectedValue + "'");
            string log_message = opt.TransactionCommand(commandlist) == "Success" ? "回填夜配方保存成功" : "回填夜配方保存失败";
            log_message += ",保存参数：" + string.Join(",", value);
            InsertTlog(log_message);
        }
        bindGrid2();

        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "updatetree", " $('#btnUpdate').click();", true);
    }
    protected void bindData2()
    {
        string query;
        if (hdcode2.Value.Length == 8)
            query = "select FORMULA_CODE  as 配方编号,FORMULA_NAME  as 配方名称,PROD_CODE  as 产品编码,STANDARD_VOL  as 标准版本号,B_DATE  as 执行日期,E_DATE  as 结束日期,CONTROL_STATUS  as 受控状态,CREATE_ID  as 编制人,CREATE_DATE  as 编制日期,CREATE_DEPT_ID  as 编制部门,REMARK  as 备注,is_valid from ht_qa_coat_formula where is_del = '0' and FORMULA_CODE = '" + hdcode2.Value + "'";
        else
            query = "select FORMULA_CODE  as 配方编号,FORMULA_NAME  as 配方名称,PROD_CODE  as 产品编码,STANDARD_VOL  as 标准版本号,B_DATE  as 执行日期,E_DATE  as 结束日期,CONTROL_STATUS  as 受控状态,CREATE_ID  as 编制人,CREATE_DATE  as 编制日期,CREATE_DEPT_ID  as 编制部门,REMARK  as 备注,is_valid from ht_qa_coat_formula where is_del = '0' and PROD_CODE = '" + hdcode2.Value + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.UpDateOra("delete from ht_qa_coat_formula_detail where is_valid = '0'");
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            txtCode2.Text = data.Tables[0].Rows[0]["配方编号"].ToString();
            txtName2.Text = data.Tables[0].Rows[0]["配方名称"].ToString();
            listPro2.SelectedValue = data.Tables[0].Rows[0]["产品编码"].ToString();
            txtVersion2.Text = data.Tables[0].Rows[0]["标准版本号"].ToString();
            txtExeDate2.Text = data.Tables[0].Rows[0]["执行日期"].ToString();
            txtEndDate2.Text = data.Tables[0].Rows[0]["结束日期"].ToString();
            // listStatus.SelectedValue = data.Tables[0].Rows[0]["受控状态"].ToString();
            listCreator2.SelectedValue = data.Tables[0].Rows[0]["编制人"].ToString();
            txtCrtDate2.Text = data.Tables[0].Rows[0]["编制日期"].ToString();
            listCrtApt2.SelectedValue = data.Tables[0].Rows[0]["编制部门"].ToString();
            txtDscpt2.Text = data.Tables[0].Rows[0]["备注"].ToString();
            ckValid2.Checked = ("1" == data.Tables[0].Rows[0]["is_valid"].ToString());
        }
        else
        {
            txtCode2.Text = "";
            txtName2.Text = "";
            listPro2.SelectedValue = "";
            txtVersion2.Text = "";
            txtExeDate2.Text = "";
            txtEndDate2.Text = "";
            // listStatus.SelectedValue = data.Tables[0].Rows[0]["受控状态"].ToString();
            listCreator2.SelectedValue = "";
            txtCrtDate2.Text = "";
            listCrtApt2.SelectedValue = "";
            txtDscpt2.Text = "";
            ckValid2.Checked = false;
        }
        bindGrid2();
        bindGrid2_2();

    }
    protected void listGirdName2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList list = (DropDownList)sender;
        GridViewRow row = (GridViewRow)list.NamingContainer;
        ((TextBox)row.FindControl("txtCodeM")).Text = list.SelectedValue;
    }

    protected void bindGrid2()
    {
        string query;
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        if (hdcode2.Value.Length == 8)
        {
            query = "select r.MATER_CODE as 香料种类,r.coat_scale as 比例,r.need_size as 每罐调配所需,r.BATCH_NUM as 每批用量,r.id, r.remark as 备注 from ht_qa_coat_formula_detail r  left join ht_pub_materiel s on s.material_code = r.mater_code   where r.coat_flag = 'HT' and r.is_del = '0'  and r.formula_code  = '" + hdcode2.Value + "' order by r.id";
            string sql = "select remark from ht_qa_coat_formula where formula_code = " + hdcode2.Value;
            DataSet dt = opt.CreateDataSetOra(sql);
            formula.Text = dt.Tables[0].Rows[0][0].ToString();
            
        }
        else
            query = "select distinct r.MATER_CODE as 香料种类,r.coat_scale as 比例,r.need_size as 每罐调配所需,r.BATCH_NUM as 每批用量,r.id, r.remark as 备注 from ht_qa_coat_formula_detail r  left join ht_pub_materiel s on s.material_code = r.mater_code left join ht_qa_coat_formula t on t.formula_code = r.formula_code  where r.coat_flag = 'HT' and r.is_del = '0'and t.is_del = '0' and t.PROD_CODE  = '" + hdcode2.Value + "' order by r.id";
        string sql1 = "select remark from ht_qa_coat_formula where formula_code = " + hdcode2.Value;
        DataSet dt1 = opt.CreateDataSetOra(sql1);
        string test = dt1.Tables[0].Rows[0][0].ToString();
        System.Diagnostics.Debug.WriteLine("the hdcode2 is:"+ hdcode2.Value);
        System.Diagnostics.Debug.WriteLine("the test resultis :"+test);
        DataSet data = opt.CreateDataSetOra(query);
        //string sql = "select remark from ht_qa_coat_formula where formula_code = "+txtCode2.Text;

       // formula.Text = data.Tables[0].Rows[0][]
        if (data != null)
            grid2Databind(data.Tables[0], GridView2);

    }
    protected void bindGrid2_2()
    {
        string query;
        if (hdcode2.Value.Length == 8)
            query = "select r.MATER_CODE as 香料种类,r.coat_scale as 比例,r.need_size as 每罐调配所需,r.BATCH_NUM as 每批用量,r.id from ht_qa_coat_formula_detail r  left join ht_pub_materiel s on s.material_code = r.mater_code   where r.coat_flag = 'LY' and r.is_del = '0'   and r.formula_code  = '" + hdcode2.Value + "'  order by r.id";
        else
            query = "select distinct r.MATER_CODE as 香料种类,r.coat_scale as 比例,r.need_size as 每罐调配所需,r.BATCH_NUM as 每批用量,r.id  from ht_qa_coat_formula_detail r  left join ht_pub_materiel s on s.material_code = r.mater_code left join ht_qa_coat_formula t on t.formula_code = r.formula_code  where r.coat_flag = 'LY' and r.is_del = '0' and t.is_del = '0' and t.PROD_CODE  = '" + hdcode2.Value + "'  order by r.id";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
       
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null)
            grid2Databind(data.Tables[0], GridView2_2);
    }
    protected void btnAdd2_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string id = opt.GetSegValue(" select CFORMULA_DT_ID_SEQ.nextval as id from dual", "ID");
        string[] seg = { "id", "IS_VALID", "coat_flag", "FORMULA_CODE" };
        string[] value = { id, "0", "HT", txtCode2.Text };
        opt.MergeInto(seg, value, 1, "ht_qa_coat_formula_detail");

        string query;
        if (hdcode2.Value.Length == 8)
            query = "select r.MATER_CODE as 香料种类,r.coat_scale as 比例,r.need_size as 每罐调配所需,r.BATCH_NUM as 每批用量,r.id  from ht_qa_coat_formula_detail r  left join ht_pub_materiel s on s.material_code = r.mater_code   where r.coat_flag = 'HT' and r.is_del = '0'  and r.formula_code  = '" + hdcode2.Value + "'  order by r.id";
        else
            query = "select r.MATER_CODE as 香料种类,r.coat_scale as 比例,r.need_size as 每罐调配所需,r.BATCH_NUM as 每批用量,r.id  from ht_qa_coat_formula_detail r  left join ht_pub_materiel s on s.material_code = r.mater_code left join ht_qa_coat_formula t on t.formula_code = r.formula_code  where r.coat_flag = 'HT' and r.is_del = '0' and t.is_del = '0'  and t.PROD_CODE  = '" + hdcode2.Value + "'  order by r.id";

        DataSet data = opt.CreateDataSetOra(query);
        if (data != null)
        {
            grid2Databind(data.Tables[0], GridView2);
        }

    }
    protected void btnAdd2_2_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string id = opt.GetSegValue(" select CFORMULA_DT_ID_SEQ.nextval as id from dual", "ID");
        string[] seg = { "id", "IS_VALID", "coat_flag", "FORMULA_CODE" };
        string[] value = { id, "0", "LY", txtCode2.Text };
        opt.MergeInto(seg, value, 1, "ht_qa_coat_formula_detail");

        string query;
        if (hdcode2.Value.Length == 8)
            query = "select r.MATER_CODE as 香料种类,r.coat_scale as 比例,r.need_size as 每罐调配所需,r.BATCH_NUM as 每批用量,r.id  from ht_qa_coat_formula_detail r  left join ht_pub_materiel s on s.material_code = r.mater_code   where r.coat_flag = 'LY' and r.is_del = '0'  and r.formula_code  = '" + hdcode2.Value + "'  order by r.id";
        else
            query = "select r.MATER_CODE as 香料种类,r.coat_scale as 比例,r.need_size as 每罐调配所需,r.BATCH_NUM as 每批用量,r.id  from ht_qa_coat_formula_detail r  left join ht_pub_materiel s on s.material_code = r.mater_code left join ht_qa_coat_formula t on t.formula_code = r.formula_code  where r.coat_flag = 'LY' and r.is_del = '0' and t.is_del = '0'  and t.PROD_CODE  = '" + hdcode2.Value + "'  order by r.id";

        DataSet data = opt.CreateDataSetOra(query);
        if (data != null)
        {
            grid2Databind(data.Tables[0], GridView2_2);
        }

    }
    protected void btnDel2_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号  
            string mtr_code = ((TextBox)GridView2.Rows[Rowindex].FindControl("txtCodeM")).Text;           
            string query;
            if(mtr_code == "")
                query = "delete from ht_qa_coat_formula_detail where id= '" + GridView2.DataKeys[Rowindex].Value.ToString() + "'";
            else
                query ="update ht_qa_coat_formula_detail set IS_DEL = '1'  where FORMULA_CODE = '" + txtCode2.Text + "' and MATER_CODE = '" + mtr_code + "'";
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string log_message = opt.UpDateOra(query) == "Success" ? "物料删除成功" : "物料删除失败";
            log_message += ",物料编号：" + txtCode2.Text;
            InsertTlog(log_message);
            bindGrid2();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnDel2_2_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号  
            string mtr_code = ((TextBox)GridView2_2.Rows[Rowindex].FindControl("txtCodeM")).Text;
            string query;
            if (mtr_code == "")
                query = "delete from ht_qa_coat_formula_detail where id= '" + GridView2_2.DataKeys[Rowindex].Value.ToString() + "'";
            else
                query = "update ht_qa_coat_formula_detail set IS_DEL = '1'  where FORMULA_CODE = '" + txtCode2.Text + "' and MATER_CODE = '" + mtr_code + "'";
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string log_message = opt.UpDateOra(query) == "Success" ? "物料删除成功" : "物料删除失败";
            log_message += ",物料编号：" + txtCode2.Text;
            InsertTlog(log_message);
            bindGrid2_2();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnCkAll2_Click(object sender, EventArgs e)
    {
        int ckno = 0;
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            if (((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked)
                ckno++;
        }
        bool check = (ckno < GridView2.Rows.Count);
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            ((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked = check;

        }
    }
    protected void btnCkAll2_2_Click(object sender, EventArgs e)
    {
        int ckno = 0;
        for (int i = 0; i < GridView2_2.Rows.Count; i++)
        {
            if (((CheckBox)GridView2_2.Rows[i].FindControl("chk")).Checked)
                ckno++;
        }
        bool check = (ckno < GridView2_2.Rows.Count);
        for (int i = 0; i < GridView2_2.Rows.Count; i++)
        {
            ((CheckBox)GridView2_2.Rows[i].FindControl("chk")).Checked = check;

        }
    }
    protected void btnDelSel2_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked)
                {
                    string mtr_code = ((DropDownList)GridView2.Rows[i].FindControl("listGridName2")).SelectedValue;
                    string query;
                    if (mtr_code == "")
                        query = "delete from ht_qa_coat_formula_detail where id= '" + GridView2.DataKeys[i].Value.ToString() + "'";
                    else
                        query = "update ht_qa_coat_formula_detail set IS_DEL = '1'  where FORMULA_CODE = '" + txtCode2.Text + "' and MATER_CODE = '" + mtr_code + "'";
                    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                    string log_message = opt.UpDateOra(query) == "Success" ? "物料删除成功" : "物料删除失败";
                    log_message += "，物料编号：" + txtCode2.Text;
                    InsertTlog(log_message);
                }
            }
            bindGrid2();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnDelSel2_2_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i <= GridView2_2.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView2_2.Rows[i].FindControl("chk")).Checked)
                {
                    string mtr_code = ((DropDownList)GridView2_2.Rows[i].FindControl("listGridName2")).SelectedValue;
                    string query;
                    if (mtr_code == "")
                        query = "delete from ht_qa_coat_detail where id= '" + GridView2_2.DataKeys[i].Value.ToString() + "'";
                    else
                        query = "update ht_qa_coat_formula_detail set IS_DEL = '1'  where FORMULA_CODE = '" + txtCode2.Text + "' and MATER_CODE = '" + mtr_code + "'";
                    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                    string log_message = opt.UpDateOra(query) == "Success" ? "物料删除成功" : "物料删除失败";
                    log_message += "，物料编号：" + txtCode2.Text;
                    InsertTlog(log_message);
                }
            }
            bindGrid2_2();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnGridSave2_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GridView2.Rows)
            {
                string mtr_code = ((DropDownList)row.FindControl("listGridName2")).SelectedValue;
                MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                string[] seg = { "ID", "FORMULA_CODE", "MATER_CODE", "coat_scale", "need_size", "BATCH_NUM", "coat_flag", "IS_DEL", "IS_VALID" };
                string[] value = { GridView2.DataKeys[row.RowIndex].Value.ToString(), txtCode2.Text, mtr_code, ((TextBox)row.FindControl("txtScale")).Text, ((TextBox)row.FindControl("txtPercent")).Text, ((TextBox)row.FindControl("txtBatchNum")).Text, "HT", "0", "1" };
                string log_message = opt.MergeInto(seg, value, 2, "ht_qa_coat_formula_detail") == "Success" ? "物料保存成功" : "物料保存失败";
                log_message += "，物料编号:" + txtCode2.Text;
                InsertTlog(log_message);

            }
            bindGrid2();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }


    }

    protected void btnGridSave2_2_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GridView2_2.Rows)
            {
                string mtr_code = ((DropDownList)row.FindControl("listGridName2")).SelectedValue;
                MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                string[] seg = { "ID", "FORMULA_CODE", "MATER_CODE", "coat_scale", "need_size", "BATCH_NUM", "coat_flag", "IS_DEL", "IS_VALID" };
                string[] value = { GridView2_2.DataKeys[row.RowIndex].Value.ToString(), txtCode2.Text, mtr_code, ((TextBox)row.FindControl("txtScale")).Text, ((TextBox)row.FindControl("txtPercent")).Text, ((TextBox)row.FindControl("txtBatchNum")).Text, "LY", "0", "1" };
                string log_message = opt.MergeInto(seg, value, 2, "ht_qa_coat_formula_detail") == "Success" ? "物料保存成功" : "物料保存失败";
                log_message += "，物料编号:" + txtCode2.Text;
                InsertTlog(log_message);

            }
            bindGrid2_2();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }


    }
    protected void btnSave2_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            string mtr_code = ((DropDownList)row.FindControl("listGridName2")).SelectedValue;
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string[] seg = { "ID", "FORMULA_CODE", "MATER_CODE", "coat_scale", "need_size", "BATCH_NUM", "coat_flag", "IS_DEL", "IS_VALID" };
            string[] value = { GridView2.DataKeys[row.RowIndex].Value.ToString(), txtCode2.Text, mtr_code, ((TextBox)row.FindControl("txtScale")).Text, ((TextBox)row.FindControl("txtPercent")).Text, ((TextBox)row.FindControl("txtBatchNum")).Text, "HT", "0", "1" };
            string log_message = opt.MergeInto(seg, value, 2, "ht_qa_coat_formula_detail") == "Success" ? "物料保存成功" : "物料保存失败";
            log_message += "，物料编号:" + txtCode2.Text;
            InsertTlog(log_message);
            bindGrid2();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }


    }

    protected void btnFormulaSave_Click(object sender, EventArgs e)
    {
        try
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string[] seg = { "FORMULA_CODE", "PROD_CODE", "REMARK" };
            string[] value = { txtCode2.Text, listPro2.SelectedValue, formula.Text };
            string log_message = opt.MergeInto(seg, value, 1, "ht_qa_coat_formula") == "Success" ? "回填液配方公式保存成功" : "回填液配方公式保存失败";
            log_message += ",回填液配方编码：" + txtCode2.Text;
            //InsertTlog(log_message);
            bindGrid2();
        }
        catch (Exception ee) {
            Response.Write(ee.Message);
        }


    }

    protected void btnSave2_2_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            string mtr_code = ((DropDownList)row.FindControl("listGridName2")).SelectedValue;
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string[] seg = { "ID", "FORMULA_CODE", "MATER_CODE", "coat_scale", "need_size", "BATCH_NUM", "coat_flag", "IS_DEL", "IS_VALID" };
            string[] value = { GridView2_2.DataKeys[row.RowIndex].Value.ToString(), txtCode2.Text, mtr_code, ((TextBox)row.FindControl("txtScale")).Text, ((TextBox)row.FindControl("txtPercent")).Text, ((TextBox)row.FindControl("txtBatchNum")).Text, "LY", "0", "1" };
            string log_message = opt.MergeInto(seg, value, 2, "ht_qa_coat_formula_detail") == "Success" ? "物料保存成功" : "物料保存失败";
            log_message += "，物料编号:" + txtCode2.Text;
            InsertTlog(log_message);
            bindGrid2_2();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }


    }
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

    protected void GridView2_2_PageIndexChanging(object sender, GridViewPageEventArgs e)
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

        bindGrid2_2();
    }


    protected void btnAddR2_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string str = opt.GetSegValue("select Max(Formula_code) as code from ht_qa_coat_formula ", "CODE");
        if (str == "")
            str = "00000000";
        txtCode2.Text = "70308" + (Convert.ToInt16(str.Substring(5)) + 1).ToString().PadLeft(3, '0');
        MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
        listCreator2.SelectedValue = user.id;
        txtCrtDate2.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
        listCrtApt2.SelectedValue = user.OwningBusinessUnitId;


        txtName2.Text = "";

        txtVersion2.Text = "";
        txtExeDate2.Text = "";
        txtEndDate2.Text = "";
        listStatus2.SelectedValue = "";
        txtDscpt2.Text = "";
        ckValid2.Checked = false;

    }

    #endregion

    #region tab4
    protected void bindData3()
    {
        string query;
        if (hdcode3.Value.Length == 8)
            query = "select FORMULA_CODE  as 配方编号,FORMULA_NAME  as 配方名称,PROD_CODE  as 产品编码,STANDARD_VOL  as 标准版本号,B_DATE  as 执行日期,E_DATE  as 结束日期,CONTROL_STATUS  as 受控状态,CREATE_ID  as 编制人,CREATE_DATE  as 编制日期,CREATE_DEPT_ID  as 编制部门,REMARK  as 备注,is_valid from ht_qa_FLA_formula where is_del = '0' and FORMULA_CODE = '" + hdcode3.Value + "'";
        else
            query = "select FORMULA_CODE  as 配方编号,FORMULA_NAME  as 配方名称,PROD_CODE  as 产品编码,STANDARD_VOL  as 标准版本号,B_DATE  as 执行日期,E_DATE  as 结束日期,CONTROL_STATUS  as 受控状态,CREATE_ID  as 编制人,CREATE_DATE  as 编制日期,CREATE_DEPT_ID  as 编制部门,REMARK  as 备注,is_valid from ht_qa_FLA_formula where is_del = '0' and PROD_CODE = '" + hdcode3.Value + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.UpDateOra("delete from ht_qa_FLA_formula_detail where is_valid = '0'");
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            txtCode3.Text = data.Tables[0].Rows[0]["配方编号"].ToString();
            txtName3.Text = data.Tables[0].Rows[0]["配方名称"].ToString();
            listPro3.SelectedValue = data.Tables[0].Rows[0]["产品编码"].ToString();
            txtVersion3.Text = data.Tables[0].Rows[0]["标准版本号"].ToString();
            txtExeDate3.Text = data.Tables[0].Rows[0]["执行日期"].ToString();
            txtEndDate3.Text = data.Tables[0].Rows[0]["结束日期"].ToString();
            // listStatus.SelectedValue = data.Tables[0].Rows[0]["受控状态"].ToString();
            listCreator3.SelectedValue = data.Tables[0].Rows[0]["编制人"].ToString();
            txtCrtDate3.Text = data.Tables[0].Rows[0]["编制日期"].ToString();
            listCrtApt3.SelectedValue = data.Tables[0].Rows[0]["编制部门"].ToString();
            txtDscpt3.Text = data.Tables[0].Rows[0]["备注"].ToString();
            ckValid3.Checked = ("1" == data.Tables[0].Rows[0]["is_valid"].ToString());
        }
        else
        {
            txtCode3.Text = "";
            txtName3.Text = "";
            listPro3.SelectedValue = "";
            txtVersion3.Text = "";
            txtExeDate3.Text = "";
            txtEndDate3.Text = "";
            // listStatus.SelectedValue = data.Tables[0].Rows[0]["受控状态"].ToString();
            listCreator3.SelectedValue = "";
            txtCrtDate3.Text = "";
            listCrtApt3.SelectedValue = "";
            txtDscpt3.Text = "";
            ckValid3.Checked = false;
        }
        bindGrid3();

    }
    protected void bindGrid3()
    {
        string query;
        if (hdcode3.Value.Length == 8)
            query = "select r.MATER_CODE as 香料种类,r.coat_scale as 比例,r.need_size as 每罐调配所需,r.BATCH_NUM as 每批用量,r.id  from ht_qa_FLA_formula_detail r  left join ht_pub_materiel s on s.material_code = r.mater_code   where r.coat_flag = 'XJ' and r.is_del = '0'  and r.formula_code  = '" + hdcode3.Value + "'";
        else
            query = "select distinct r.MATER_CODE as 香料种类,r.coat_scale as 比例,r.need_size as 每罐调配所需,r.BATCH_NUM as 每批用量,r.id  from ht_qa_FLA_formula_detail r  left join ht_pub_materiel s on s.material_code = r.mater_code left join ht_qa_FLA_formula t on t.formula_code = r.formula_code  where r.coat_flag = 'XJ' and r.is_del = '0' and t.is_del = '0' and t.PROD_CODE  = '" + hdcode3.Value + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null)
            grid3Databind(data.Tables[0]);
    }
    protected void btnAdd3_Click(object sender, EventArgs e)  //没有实现
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string id = opt.GetSegValue(" select FFORMULA_DT_ID_SEQ.nextval as id from dual", "ID");
        string[] seg = { "id", "IS_VALID", "coat_flag", "FORMULA_CODE" };
        string[] value = { id, "0", "XJ", txtCode3.Text };
        opt.MergeInto(seg, value, 1, "ht_qa_FLA_formula_detail");
        string query;
        if (hdcode3.Value.Length == 8)
            query = "select r.MATER_CODE as 香料种类,r.coat_scale as 比例,r.need_size as 每罐调配所需,r.BATCH_NUM as 每批用量,r.id  from ht_qa_FLA_formula_detail r  left join ht_pub_materiel s on s.material_code = r.mater_code   where r.coat_flag = 'XJ' and r.is_del = '0'   and r.formula_code  = '" + hdcode3.Value + "'";
        else
            query = "select r.MATER_CODE as 香料种类,r.coat_scale as 比例,r.need_size as 每罐调配所需,r.BATCH_NUM as 每批用量,r.id  from ht_qa_FLA_formula_detail r  left join ht_pub_materiel s on s.material_code = r.mater_code left join ht_qa_FLA_formula t on t.formula_code = r.formula_code  where r.coat_flag = 'XJ' and r.is_del = '0'   and t.PROD_CODE  = '" + hdcode3.Value + "'";
        DataSet data = opt.CreateDataSetOra(query);       
        if (data != null)
        {
            grid3Databind(data.Tables[0]);
        }     
    }
    protected void grid3Databind(DataTable data)
    {
        GridView3.DataSource = data;
        GridView3.DataBind();

        if (data != null && data.Rows.Count > 0)
        {
            for (int i = GridView3.PageSize * GridView3.PageIndex; i < GridView3.PageSize * (GridView3.PageIndex + 1) && i < data.Rows.Count; i++)
            {
                int j = i - GridView3.PageSize * GridView3.PageIndex;
                DataRowView mydrv = data.DefaultView[i];
                GridViewRow row = GridView3.Rows[j];
                ((DropDownList)row.FindControl("listGridName3")).SelectedValue = mydrv["香料种类"].ToString();
                ((TextBox)row.FindControl("txtCodeM")).Text = mydrv["香料种类"].ToString();
                ((TextBox)row.FindControl("txtScale")).Text = mydrv["比例"].ToString();
                ((TextBox)row.FindControl("txtPercent")).Text = mydrv["每罐调配所需"].ToString();
                ((TextBox)row.FindControl("txtBatchNum")).Text = mydrv["每批用量"].ToString();
            }
        }
    }
    protected void btnUpdate3_Click(object sender, EventArgs e)//没有实现
    {
        bindData3();
    }

    protected void btnModify3_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        hdcode3.Value = txtCode3.Text;
        {
            string[] seg = { "FORMULA_CODE", "FORMULA_NAME", "PROD_CODE", "STANDARD_VOL", "B_DATE", "E_DATE", "CONTROL_STATUS", "CREATE_ID", "CREATE_DATE", "CREATE_DEPT_ID", "REMARK" };
            string[] value = { txtCode3.Text, txtName3.Text, listPro3.SelectedValue, txtVersion3.Text, txtExeDate3.Text, txtEndDate3.Text, listStatus3.SelectedValue, listCreator3.SelectedValue, txtCrtDate3.Text, listCrtApt3.SelectedValue, txtDscpt3.Text, };
            List<String> commandlist = new List<string>();
            commandlist.Add(opt.getMergeStr(seg, value, 1, "ht_qa_FLA_formula"));
            commandlist.Add("update ht_pub_prod_design set FLA_FORMULA_CODE = '" + txtCode3.Text + "' where prod_code = '" + listPro3.SelectedValue + "'");
            string log_message = opt.TransactionCommand(commandlist) == "Success" ? "回填夜配方保存成功" : "回填夜配方保存失败";
            log_message += ",保存参数：" + string.Join(",", value);
            InsertTlog(log_message);
        }
        bindGrid3();

        ScriptManager.RegisterStartupScript(UpdatePanel3, this.Page.GetType(), "updatetree", " $('#btnUpdate').click();", true);
    }

    protected void listGirdName3_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList list = (DropDownList)sender;
        int rowIndex = ((GridViewRow)list.NamingContainer).RowIndex;
        ((TextBox)GridView3.Rows[rowIndex].FindControl("txtCodeM")).Text = list.SelectedValue;
    }


    protected void btnDel3_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号  
            string mtr_code = ((TextBox)GridView3.Rows[Rowindex].FindControl("txtCodeM")).Text;
            string query;
            if (mtr_code == "")
                query = "delete from ht_qa_FLA_formula_detail where id = '" + GridView3.DataKeys[Rowindex].Value.ToString() + "'";
            else
                query = "update ht_qa_FLA_formula_detail set IS_DEL = '1'  where FORMULA_CODE = '" + txtCode3.Text + "' and MATER_CODE = '" + mtr_code + "'";
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string log_message = opt.UpDateOra(query) == "Success" ? "物料删除成功" : "物料删除失败";
            log_message += ",物料编号：" + txtCode3.Text;
            InsertTlog(log_message);
            bindGrid3();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnCkAll3_Click(object sender, EventArgs e)
    {
        int ckno = 0;
        for (int i = 0; i < GridView3.Rows.Count; i++)
        {
            if (((CheckBox)GridView3.Rows[i].FindControl("chk")).Checked)
                ckno++;
        }
        bool check = (ckno < GridView3.Rows.Count);
        for (int i = 0; i < GridView3.Rows.Count; i++)
        {
            ((CheckBox)GridView3.Rows[i].FindControl("chk")).Checked = check;

        }
    }
    protected void btnDelSel3_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i <= GridView3.Rows.Count - 1; i++)
            {
                if (((CheckBox)GridView3.Rows[i].FindControl("chk")).Checked)
                {
                    string mtr_code = ((DropDownList)GridView3.Rows[i].FindControl("listGridName3")).SelectedValue;
                    string query;
                    if(mtr_code == "")
                    query = "delete from ht_qa_FLA_formula_detail where id = '" + GridView3.DataKeys[i].Value.ToString() + "'";
                    else
                     query = "update ht_qa_FLA_formula_detail set IS_DEL = '1'  where FORMULA_CODE = '" + txtCode3.Text + "' and MATER_CODE = '" + mtr_code + "'";
                    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                    string log_message = opt.UpDateOra(query) == "Success" ? "物料删除成功" : "物料删除失败";
                    log_message += "，物料编号：" + txtCode3.Text;
                    InsertTlog(log_message);
                }
            }
            bindGrid3();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void btnGridSave3_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GridView3.Rows)
            {

                string mtr_code = ((DropDownList)row.FindControl("listGridName3")).SelectedValue;
                MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                string[] seg = { "ID", "FORMULA_CODE", "MATER_CODE", "coat_scale", "need_size", "BATCH_NUM", "coat_flag", "IS_DEL", "IS_VALID" };
                string[] value = {GridView3.DataKeys[row.RowIndex].Value.ToString(), txtCode3.Text, mtr_code, ((TextBox)row.FindControl("txtScale")).Text, ((TextBox)row.FindControl("txtPercent")).Text,((TextBox)row.FindControl("txtBatchNum")).Text, "XJ", "0" ,"1"};
                string log_message = opt.MergeInto(seg, value, 2, "ht_qa_FLA_formula_detail") == "Success" ? "物料保存成功" : "物料保存失败";
                log_message += "，物料编号:" + txtCode3.Text;
                InsertTlog(log_message);

            }
            bindGrid3();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }


    }
    protected void btnSave3_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int Rowindex = row.RowIndex;//获得行号  
            string mtr_code = ((DropDownList)GridView3.Rows[Rowindex].FindControl("listGridName3")).SelectedValue;
            if (Rowindex >= 0)
            {
                MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

                string[] seg = { "ID", "FORMULA_CODE", "MATER_CODE", "coat_scale", "need_size", "BATCH_NUM", "coat_flag", "IS_DEL", "IS_VALID" };
                string[] value = { GridView3.DataKeys[Rowindex].Value.ToString(), txtCode3.Text, mtr_code, ((TextBox)row.FindControl("txtScale")).Text, ((TextBox)row.FindControl("txtPercent")).Text, ((TextBox)row.FindControl("txtBatchNum")).Text, "XJ", "0", "1" };
                string log_message = opt.MergeInto(seg, value, 2, "ht_qa_FLA_formula_detail") == "Success" ? "物料保存成功" : "物料保存失败";
                log_message += "，物料编号:" + txtCode3.Text;
                InsertTlog(log_message);
                bindGrid3();
            }
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }


    }
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
    protected void btnAddR3_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string str = opt.GetSegValue("select Max(Formula_code) as code from ht_qa_FLA_formula ", "CODE");
        if (str == "")
            str = "00000000";
        txtCode3.Text = "70309" + (Convert.ToInt16(str.Substring(5)) + 1).ToString().PadLeft(3, '0');
        MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
        listCreator3.SelectedValue = user.id;
        txtCrtDate3.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
        listCrtApt3.SelectedValue = user.OwningBusinessUnitId;


        txtName3.Text = "";

        txtVersion3.Text = "";
        txtExeDate3.Text = "";
        txtEndDate3.Text = "";
        listStatus3.SelectedValue = "";
        txtDscpt3.Text = "";
        ckValid3.Checked = false;

    }
    #endregion
}