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
            initView();
            bindGrid();
        }

    }
    protected void initView()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listPathAll, "select distinct pathname,pathcode  from ht_pub_path_prod t where t.is_del = '0'", "pathname", "pathcode");
        opt.bindDropDownList(listAprv, "select * from ht_inner_aprv_status ", "NAME", "ID");
        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtCodeS.Text.Split('-').Length >= 5)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            List<String> commandlist = new List<String>();
            string[] seg = { "PATHNAME", "SECTION_CODE", "PATHCODE", "SECTION_PATH" };
           
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {               
                DropDownList list = (DropDownList)GridView1.Rows[i].FindControl("listpath");
                string section = GridView1.DataKeys[i].Value.ToString();
                string[] value = { txtNameS.Text, section, txtCodeS.Text, list.SelectedValue };
                commandlist.Add(opt.getMergeStr(seg, value, 2, "HT_PUB_PATH_PROD"));                
            }
            string log_message = opt.TransactionCommand(commandlist) == "Success" ? "保存全线路径成功" : "保存全线路径失败";
            log_message += "--标识:" + listPathAll.SelectedValue;
            InsertTlog(log_message);
            commandlist.Clear();
            commandlist.Add("delete HT_PUB_PARA_WEIGHT where PATH_NAME = '" + txtNameS.Text + "'");
           
            commandlist.Add("insert into ht_pub_para_weight ( select  r.para_code,t.pathcode,'0',0.2,'test1' from ht_pub_path_prod t  left join ht_pub_path_node s on s.is_del = '0' and  s.section_code = t.section_code and substr(t.section_path,s.orders,1) = '1' left join ht_pub_tech_para r on r.path_node = s.id and r.is_del = '0' and r.para_type like '______1%' where t.pathcode = '" + txtCodeS.Text + "' and t.is_del = '0' and r.para_code is not null union select r.para_code,'" + txtCodeS.Text + "','0',0.2,'test1' from ht_pub_tech_para r where r.para_type like '______1%' and r.path_node is null and r.is_del = '0')");
            opt.TransactionCommand(commandlist);
            opt.bindDropDownList(listPathAll, "select distinct pathname,pathcode  from ht_pub_path_prod t where t.is_del = '0'", "pathname", "pathcode");
            listPathAll.SelectedValue = txtCodeS.Text;
            bindGrid();
        }

    }



    protected void btnDel_Click(object sender, EventArgs e)
    {      

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        List<String> commandlist = new List<string>();
        commandlist.Add("delete from  ht_pub_path_prod  where pathcode = '" + listPathAll.SelectedValue + "'");
        commandlist.Add("delete from ht_pub_aprv_flowinfo where BUSIN_ID = '" + listPathAll.SelectedValue + "'");
        string log_message = opt.TransactionCommand(commandlist) == "Success" ? "删除全线路径成功" : "删除全线路径失败";
        log_message += "--标识:" + listPathAll.SelectedValue;
        InsertTlog(log_message);
        
    }
  
    protected void btnFLow_Click(object sender, EventArgs e)
    {

        string query = "select pos as 顺序号, workitemid as 审批环节,username as 负责人,comments as 意见,opiniontime 审批时间,(case status when '0' then '未审批'  when '1' then '未通过' else '己通过' end)  as 审批状态  from ht_pub_aprv_opinion r left join ht_pub_aprv_flowinfo s on r.gongwen_id = s.id where s.busin_id  = '" + listPathAll.SelectedValue + "' order by pos";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        GridView3.DataSource = opt.CreateDataSetOra(query);
        GridView3.DataBind();
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "$('#flowinfo').fadeIn(200);", true);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)//提交审批
    {
        try
        {
            
            /*启动审批TB_ZT标题,MODULENAME审批类型编码,BUSIN_ID业务数据id,URL 单独登录url*/
            //"TB_ZT", "MODULENAME", "BUSIN_ID",  "URL"
            string[] subvalue = { "全线路径:" + listPathAll.SelectedItem.Text, "05", listPathAll.SelectedValue, Page.Request.UserHostName.ToString() };
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string log_message = MSYS.AprvFlow.createApproval(subvalue) ? "提交审批成功," : "提交审批失败，";
            log_message += ",业务数据ID：" + listPathAll.SelectedValue;
            InsertTlog(log_message);
            
            //

        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }  
  
  


    protected void bindGrid()
    {
        string query = "select g.section_name as 工艺段, '' as 路径选择, '' as 路径详情,g.section_code from ht_pub_tech_section g  where g.is_valid = '1' and g.is_del = '0' and g.IS_PATH_CONFIG = '1' order by g.section_code";
       
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string[] subpath = listPathAll.SelectedValue.Split('-');
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                ((TextBox)GridView1.Rows[i].FindControl("txtSection")).Text = mydrv["工艺段"].ToString();
                DropDownList list = (DropDownList)GridView1.Rows[i].FindControl("listpath");
                opt.bindDropDownList(list, "select pathname,pathcode from ht_pub_path_section where section_code = '" + mydrv["section_code"].ToString() + "'", "pathname", "pathcode");
                list.SelectedValue = mydrv["路径详情"].ToString();
                if (subpath.Length == GridView1.Rows.Count)
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
                            GridView1.Rows[i].Cells[2].Controls.Add(ck);
                        }
                    }
                }
            }
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1,this.Page.GetType(),"disable"," $(\"input[type$='checkbox']\").attr('disabled', 'disabled');",true);

    }//绑定GridView4数据源

    protected void listpath_SelectedIndexChanged(object sender, EventArgs e)
    {

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            DataSet set = opt.CreateDataSetOra("select * from HT_PUB_PATH_NODE where SECTION_CODE ='" + GridView1.DataKeys[i].Value.ToString() + "' and is_del = '0'");
            DropDownList list = (DropDownList)GridView1.Rows[i].FindControl("listpath");
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
                    GridView1.Rows[i].Cells[2].Controls.Add(ck);
                    if (pathcode.Length > 0)
                        ck.Checked = (pathcode.Substring(j, 1) == "1");
                    else
                        ck.Checked = false;
                }
            }
        }
        txtCodeS.Text = getPathCode();
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "disable", " $(\"input[type$='checkbox']\").attr('disabled', 'disabled');", true);
    }

    protected void listPathAll_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGrid();
        txtNameS.Text = listPathAll.SelectedItem.Text;
        txtCodeS.Text = listPathAll.SelectedValue;
    }
 
    protected string getPathCode()
    {       
        string pathcode = "";
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (i > 0)
                pathcode += "-";
            DropDownList list = (DropDownList)GridView1.Rows[i].FindControl("listpath");
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
   

}