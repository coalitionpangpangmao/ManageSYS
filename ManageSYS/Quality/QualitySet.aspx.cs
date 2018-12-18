using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Quality_QualitySet : MSYS.Web.BasePage
{    
    protected string subtvHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listVersion, "select QLT_CODE,QLT_NAME from ht_qlt_STDD_CODE where is_valid = '1' and is_del= '0'", "QLT_NAME", "QLT_CODE");        
         
            opt.bindDropDownList(listtech, "select QLT_CODE,QLT_NAME from ht_qlt_STDD_CODE where is_valid = '1' and is_del= '0'", "QLT_NAME", "QLT_CODE");
            opt.bindDropDownList(listtechC, "select QLT_CODE,QLT_NAME from ht_qlt_STDD_CODE where is_valid = '1' and is_del= '0'", "QLT_NAME", "QLT_CODE");
            opt.bindDropDownList(listAprv, "select * from ht_inner_aprv_status ", "NAME", "ID");
            subtvHtml = InitTreePrcss();
        }
    }

    public string InitTreePrcss()
    {

       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
       DataSet data = opt.CreateDataSetOra("select distinct section_code,section_name  from ht_pub_tech_section r left join ht_pub_tech_para s on substr(s.para_code,1,5) = r.section_code and s.is_del = '0' and s.is_valid = '1' where r.is_del = '0' and r.is_valid = '1' and  s.para_type like '______1%' order by r.section_code ");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul id='browser' class='filetree treeview-famfamfam'>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {               
                tvHtml += "<li ><span class='folder'  onclick = \"tabClick(" + row["section_code"].ToString() + ")\">" + row["section_name"].ToString() + "</span></a>";
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }
 
    protected void bindData(string qltcode)
    {
        string query = "select * from ht_qlt_STDD_CODE where is_del = '0' and is_valid = '1' and QLT_CODE  = '" + qltcode + "'";
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            txtCode.Text = qltcode;
            txtName.Text = data.Tables[0].Rows[0]["QLT_NAME"].ToString();           
            txtVersion.Text = data.Tables[0].Rows[0]["STANDARD_VOL"].ToString();
            txtExeDate.Text = data.Tables[0].Rows[0]["B_DATE"].ToString();
            txtEndDate.Text = data.Tables[0].Rows[0]["E_DATE"].ToString();
            txtCreator.Text = data.Tables[0].Rows[0]["CREATE_ID"].ToString();
            txtCrtDate.Text = data.Tables[0].Rows[0]["CREATE_DATE"].ToString();
            txtDscpt.Text = data.Tables[0].Rows[0]["REMARK"].ToString();
              listAprv.SelectedValue = data.Tables[0].Rows[0]["FLOW_STATUS"].ToString();
            if (!(listAprv.SelectedItem.Text == "未提交" || listAprv.SelectedItem.Text == "未通过"))
            {
                btnSubmit.Enabled = false;
                btnSubmit.CssClass = "btngrey";

            }
            else
            {
                btnSubmit.Enabled = true;
                btnSubmit.CssClass = "btn1 auth";
            }
        }
        
    }
    protected void bindGrid(string qlt_code, string section)
    {

        string query = "select g1.PARA_CODE as 参数编码,g1.lower as 下限,g1.upper as 上限,g1.QLT_TYPE as 考核类型,g1.MINUS_SCORE as 超限扣分,g1.REMARK as 备注 from ht_QLT_stdd_code_detail g1 left join ht_pub_tech_para g2 on g2.para_code = g1.para_code where g1.is_del = '0' and  g2.para_type like '______1%'  and g1.qlt_code = '" + qlt_code + "' and substr(g1.para_code,1,5) = '" + section + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet set = opt.CreateDataSetOra(query);
        DataTable data = set.Tables[0];
        bindgrid(data, hideprc.Value);
    }
    protected void bindgrid(DataTable data,string section)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        GridView1.DataSource = data;
        GridView1.DataBind();
        if (data != null && data.Rows.Count > 0)
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                DataRowView mydrv = data.DefaultView[i];
                DropDownList list = (DropDownList)GridView1.Rows[i].FindControl("listParaName");
                opt.bindDropDownList(list, "select case when length(r.para_name) <8 then s.eq_name||r.para_name else r.para_name end as para_name,r.para_code  from HT_PUB_TECH_PARA r left join ht_eq_eqp_tbl s on s.idkey = r.equip_code  where  r.para_type like '______1%' and r.is_del = '0' and substr(r.PARA_CODE,1,5) = '" + section + "'", "PARA_NAME", "PARA_CODE");
                list.SelectedValue = mydrv["参数编码"].ToString();
                if (list.SelectedValue == "")
                    list.Enabled = true;
                else
                    list.Enabled = false;
                ((TextBox)GridView1.Rows[i].FindControl("txtLower")).Text = mydrv["下限"].ToString();
                ((TextBox)GridView1.Rows[i].FindControl("txtUpper")).Text = mydrv["上限"].ToString();
                ((DropDownList)GridView1.Rows[i].FindControl("listtype")).SelectedValue = mydrv["考核类型"].ToString();
                if (mydrv["考核类型"].ToString() == "")
                    ((DropDownList)GridView1.Rows[i].FindControl("listtype")).Enabled = true;
                else
                    ((DropDownList)GridView1.Rows[i].FindControl("listtype")).Enabled = false;
                ((TextBox)GridView1.Rows[i].FindControl("txtScore")).Text = mydrv["超限扣分"].ToString();
                ((TextBox)GridView1.Rows[i].FindControl("txtDscrptM")).Text = mydrv["备注"].ToString();


            }

        }
    }
    protected DataSet typebind()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select ID,NAME from ht_inner_qlt_type union select '','' from dual order by ID ");
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (listVersion.SelectedValue == "")
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "alert", "alert('请选择质量考核标准后，再增加明细！！')", true);
            return;
        }
        if (hideprc.Value == "")
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "alert", "alert('请所属工艺段后，再增加明细！！')", true);
            return;
        }
        string query = "select g1.PARA_CODE as 参数编码,g1.lower as 下限,g1.upper as 上限,g1.QLT_TYPE as 考核类型,g1.MINUS_SCORE as 超限扣分,g1.REMARK as 备注 from ht_QLT_stdd_code_detail g1 left join ht_pub_tech_para g3 on g3.para_code = g1.para_code  left join ht_pub_tech_section g2 on substr(g1.para_code ,1,5) = g2.section_code where g1.is_del = '0'  and g1.qlt_code =  '" + txtCode.Text + "' and g2.section_code = '" + hideprc.Value + "' and g3.para_type like '______1%' and g3.is_del = '0' and g1.is_del = '0'";
       
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet set = opt.CreateDataSetOra(query);
        DataTable data = set.Tables[0];
        if (data == null)
        {
            data = new DataTable();
            data.Columns.Add("参数编码");
            data.Columns.Add("下限");
            data.Columns.Add("上限");
            data.Columns.Add("考核类型");
            data.Columns.Add("超限扣分");
            data.Columns.Add("备注");
        }
        object[] value = { "", 0, 0, "", 0, "" };
        data.Rows.Add(value);

        bindgrid(data, hideprc.Value);
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string code = opt.GetSegValue("select nvl(max(substr(QLT_CODE,4,3)),0)+1 as code from ht_qlt_stdd_code t", "code");
        txtCode.Text = "QLT" + code.PadLeft(3, '0');
        MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
        txtCreator.Text = user.text;
        txtCrtDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        txtDscpt.Text = "";
        txtEndDate.Text = "";
        txtExeDate.Text = "";
        txtName.Text = "";
        txtVersion.Text = "";
        listAprv.SelectedValue = "";
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        

         string[] seg = { "QLT_CODE", "QLT_NAME", "STANDARD_VOL", "B_DATE", "E_DATE", "CREATE_ID", "CREATE_DATE", "REMARK" };
         string[] value = { txtCode.Text, txtName.Text, txtVersion.Text, txtExeDate.Text, txtEndDate.Text, ((MSYS.Data.SysUser)Session["User"]).id, txtCrtDate.Text, txtDscpt.Text, };
           
            string log_message;
            if ( opt.MergeInto(seg, value, 1, "HT_QLT_STDD_CODE") == "Success")
            {
                log_message = "保存质量考核标准成功";
                opt.bindDropDownList(listVersion, "select QLT_CODE,QLT_NAME from ht_qlt_STDD_CODE where is_valid = '1' and is_del= '0'", "QLT_NAME", "QLT_CODE");
                opt.bindDropDownList(listtech, "select QLT_CODE,QLT_NAME from ht_qlt_STDD_CODE where is_valid = '1' and is_del= '0'", "QLT_NAME", "QLT_CODE");
                opt.bindDropDownList(listtechC, "select QLT_CODE,QLT_NAME from ht_qlt_STDD_CODE where is_valid = '1' and is_del= '0'", "QLT_NAME", "QLT_CODE");
                listAprv.SelectedValue = "-1";
            }
            else
                log_message = "保存质量考核标准失败";
            log_message += "--数据详情:" + string.Join(",", value);
            InsertTlog(log_message);
      
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        try
        {
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();         
              List<String> commandlist = new List<string>();
        commandlist.Add("delete from HT_QLT_STDD_CODE where QLT_CODE = '" + txtCode.Text + "'");
        commandlist.Add("delete from ht_pub_aprv_flowinfo where BUSIN_ID = '" + txtCode.Text + "'");
        string log_message = opt.TransactionCommand(commandlist) == "Success" ? "删除质量考核标准成功" : "删除质量考核标准失败";
        log_message += "--标识:" + txtCode.Text;
        InsertTlog(log_message);
             opt.bindDropDownList(listVersion, "select QLT_CODE,QLT_NAME from ht_qlt_STDD_CODE where is_valid = '1' and is_del= '0'", "QLT_NAME", "QLT_CODE");
        opt.bindDropDownList(listtech, "select QLT_CODE,QLT_NAME from ht_qlt_STDD_CODE where is_valid = '1' and is_del= '0'", "QLT_NAME", "QLT_CODE");
        opt.bindDropDownList(listtechC, "select QLT_CODE,QLT_NAME from ht_qlt_STDD_CODE where is_valid = '1' and is_del= '0'", "QLT_NAME", "QLT_CODE");   
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnGridDel_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int Rowindex = row.RowIndex;//获得行号  
            string mtr_code = ((DropDownList)row.FindControl("listParaName")).SelectedValue;
            string type = ((DropDownList)row.FindControl("listtype")).SelectedValue;

            string query = "update HT_QLT_STDD_CODE_DETAIL set IS_DEL = '1'  where QLT_CODE = '" + txtCode.Text + "' and PARA_CODE = '" + mtr_code + "' and QLT_TYPE = '" + type + "'" ;
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
           string log_message = opt.UpDateOra(query) == "Success" ? "删除质量标准明细成功" : "删除质量标准明细失败";
           log_message += "--标识:" + txtCode.Text + ":" + mtr_code;
           InsertTlog(log_message);
            bindGrid(txtCode.Text, hideprc.Value);
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
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
    protected void btnDelSel_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                GridViewRow row = GridView1.Rows[i];
                if (((CheckBox)row.FindControl("chk")).Checked)
                {                
                  
                    string mtr_code = ((DropDownList)row.FindControl("listParaName")).SelectedValue;
                    string type = ((DropDownList)row.FindControl("listtype")).SelectedValue;

                    string query = "update HT_QLT_STDD_CODE_DETAIL set IS_DEL = '1'  where QLT_CODE = '" + txtCode.Text + "' and PARA_CODE = '" + mtr_code + "' and QLT_TYPE = '" + type + "'";
                   MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
                   string log_message = opt.UpDateOra(query) == "Success" ? "删除质量标准明细成功" : "删除质量标准明细失败";
                   log_message += "--标识:" + txtCode.Text + ":" + mtr_code;
                   InsertTlog(log_message);
                }
            }
            bindGrid(txtCode.Text, hideprc.Value);
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            
            if (row.RowIndex >= 0)
            {
                MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

                string[] seg = { "QLT_CODE", "PARA_CODE", "QLT_TYPE", "LOWER", "UPPER", "REMARK", "MINUS_SCORE","IS_DEL" };
                string[] value = { txtCode.Text, ((DropDownList)row.FindControl("listParaName")).SelectedValue, ((DropDownList)row.FindControl("listtype")).SelectedValue, ((TextBox)row.FindControl("txtLower")).Text, ((TextBox)row.FindControl("txtUpper")).Text, ((TextBox)row.FindControl("txtDscrptM")).Text, ((TextBox)row.FindControl("txtScore")).Text,"0" };
                string log_message = opt.MergeInto(seg, value, 3, "HT_QLT_STDD_CODE_DETAIL") == "Success" ? "保存质量考核详情成功" : "保存质量考核详情失败";
                log_message += "--详情:" + string.Join(",", value);
                InsertTlog(log_message);
                    bindGrid(txtCode.Text, hideprc.Value);
            }
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }


    }



    protected void btnCopy_Click(object sender, EventArgs e)
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        string query = "select * from HT_QLT_STDD_CODE_DETAIL where QLT_CODE = '" + listtech.SelectedValue + "' and is_del = '0'";
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in data.Tables[0].Rows)
            {
                string[] seg = { "QLT_CODE", "PARA_CODE", "QLT_TYPE", "LOWER", "UPPER", "REMARK", "MINUS_SCORE" };
                string[] value = { listtechC.SelectedValue, row["PARA_CODE"].ToString(), row["QLT_TYPE"].ToString(), row["LOWER"].ToString(), row["UPPER"].ToString(), row["REMARK"].ToString(), row["MINUS_SCORE"].ToString() };
             
                string log_message = opt.MergeInto(seg, value, 3, "HT_QLT_STDD_CODE_DETAIL") == "Success" ? "保存质量考核详情成功" : "保存质量考核详情失败";
                log_message += "--详情:" + string.Join(",", value);
                InsertTlog(log_message);
            }

        }
        bindGrid(listtechC.SelectedValue, hideprc.Value);
     
    }
    protected void UpdateGrid_Click(object sender, EventArgs e)
    {
        string query = "select g1.PARA_CODE as 参数编码,g1.lower as 下限,g1.upper as 上限,g1.QLT_TYPE as 考核类型,g1.MINUS_SCORE as 超限扣分,g1.REMARK as 备注 from ht_QLT_stdd_code_detail g1 left join ht_pub_tech_para g3 on g3.para_code = g1.para_code  left join ht_pub_tech_section g2 on substr(g1.para_code ,1,5) = g2.section_code where g1.is_del = '0'  and g1.qlt_code = '" + txtCode.Text + "' and g2.section_code = '" + hideprc.Value + "' and g3.para_type like '______1%' and g3.is_del = '0'";
       
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet set = opt.CreateDataSetOra(query);
        DataTable data = set.Tables[0];
        bindgrid(data, hideprc.Value);
    }
    protected void listVersion_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindData(listVersion.SelectedValue);
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
            string[] subvalue = {  txtName.Text, "07", txtCode.Text, Page.Request.UserHostName.ToString() };
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            if (MSYS.AprvFlow.createApproval(subvalue))
            {
                string log_message = "质量考核标准提交审批成功,业务数据ID：" + txtCode.Text;
                InsertTlog(log_message);
                listAprv.SelectedValue = "0";
                btnSubmit.Enabled = false;
                btnSubmit.CssClass = "btngrey";
            }
            else
            {

            }
          
    }
}