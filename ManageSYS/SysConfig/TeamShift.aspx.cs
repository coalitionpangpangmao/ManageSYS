using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SysConfig_TeamShift : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            bindGrid1();
            bindGrid2();   
        }
 
    }
     
   
    protected void bindGrid1()
    {
        string query = "select TEAM_CODE  as 班组编码,TEAM_NAME  as 班组名称,WORKSHOP_ID  as 车间ID,CREATE_TIME  as 创建时间,MODIFY_TIME  as 修改时间 from ht_sys_team where is_del = '0' and is_valid = '1' order by TEAM_CODE";
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra(query);  
            GridView1.DataSource = data;
            GridView1.DataBind();
    }
    protected void btnSaveT_Click(object sender, EventArgs e)
    {
       DataBaseOperator opt =new DataBaseOperator();
        string query = "select * from  from ht_sys_team where TEAM_CODE = '" + txtCodeT.Text + "'";
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string[] seg = { "TEAM_CODE", "TEAM_NAME", "WORKSHOP_ID", "MODIFY_TIME", "IS_DEL" };
            string[] value = { txtCodeT.Text, txtNameT.Text, listLineT.SelectedValue, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "0" };           
            string condition = " where TEAM_CODE = '" + txtCodeT.Text + "'";
            if (opt.UpDateData(seg, value, "ht_sys_team", condition) == "Success")
                opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "班组配置保存成功， 保存参数："+string.Join(" ", value));
            else
                opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "班组配置保存失败， 保存参数：" + string.Join(" ", value));
        
        }
        else
        {
            string[] seg = { "TEAM_CODE", "TEAM_NAME", "WORKSHOP_ID", "CREATE_TIME" };
            string[] value = { txtCodeT.Text, txtNameT.Text, listLineT.SelectedValue, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
            if(opt.InsertData(seg, value, "ht_sys_team")=="Success")
                opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "班组配置保存成功， 保存参数："+string.Join(" ", value));
            else
                opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "班组配置保存失败， 保存参数：" + string.Join(" ", value));
        
        }
        bindGrid1();
    }

    protected void bindGrid2()
    {
        string query = " select SHIFT_CODE  as 班时编码,SHIFT_NAME  as 班时名称,WORKSHOP_ID  as 所属车间ID,BEGIN_TIME  as 班时开始时间,END_TIME  as 班时结束时间,INTER_DAY as 是否跨天,CREATE_TIME  as 创建时间,MODIFY_TIME  as 修改时间 from ht_sys_shift where is_del = '0' and is_valid = '1' order by SHIFT_CODE";
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView2.DataSource = data;
        GridView2.DataBind();
    }

    protected void btnSaveS_Click(object sender, EventArgs e)
    {
       DataBaseOperator opt =new DataBaseOperator();
        string query = "select * from  from ht_sys_shift where SHIFT_CODE = '" + txtCodeS.Text + "'";
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string[] seg = { "SHIFT_CODE", "SHIFT_NAME", "WORKSHOP_ID", "BEGIN_TIME", "END_TIME", "MODIFY_TIME", "IS_DEL", "INTER_DAY" };
            string[] value = {txtCodeS.Text,txtNameS.Text,listLineS.SelectedValue,txtStarttime.Text,txtEndtime.Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "0",Convert.ToInt16(ckInter.Checked).ToString() };
            string condition = " where SHIFT_CODE = '" + txtCodeS.Text + "'";
            if (opt.UpDateData(seg, value, "ht_sys_shift", condition) == "Success")
                opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "班时配置保存成功， 保存参数："+ string.Join(" ", value));
            else
                opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "班时配置保存失败， 保存参数：" + string.Join(" ", value));
        
        }
        else
        {
            string[] seg = { "SHIFT_CODE", "SHIFT_NAME", "WORKSHOP_ID", "BEGIN_TIME", "END_TIME", "CREATE_TIME", "INTER_DAY" };
            string[] value = { txtCodeS.Text, txtNameS.Text, listLineS.SelectedValue, txtStarttime.Text, txtEndtime.Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Convert.ToInt16(ckInter.Checked).ToString() };
            if(opt.InsertData(seg, value, "ht_sys_shift")=="Success")
                opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "班时配置保存成功， 保存参数：" + string.Join(" ", value));
            else
                opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "班时配置保存失败， 保存参数：" + string.Join(" ", value));
        
        }
        bindGrid2();
    }

    protected void btnGrid1Del_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string query = "update ht_sys_team set IS_DEL = '1'  where TEAM_CODE = '" + GridView1.Rows[rowIndex].Cells[1].Text + "'";
       DataBaseOperator opt =new DataBaseOperator();
        if (opt.UpDateOra(query) == "Success")
            opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "班组配置删除成功, 班组编码：" + GridView1.Rows[rowIndex].Cells[1].Text);
        else
            opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "班组配置删除失败, 班组编码:" + GridView1.Rows[rowIndex].Cells[1].Text);
        bindGrid1();
    }

    protected void btnGrid2Del_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string query = "update ht_sys_shift set IS_DEL = '1'  where SHIFT_CODE = '" + GridView2.Rows[rowIndex].Cells[1].Text + "'";
       DataBaseOperator opt =new DataBaseOperator();
        if (opt.UpDateOra(query) == "Success")
            opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "班时配置删除成功， 班时编码：" + GridView2.Rows[rowIndex].Cells[1].Text);
        else
            opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "班时配置删除失败， 班时编码：" + GridView2.Rows[rowIndex].Cells[1].Text);
        bindGrid2();
    }
}