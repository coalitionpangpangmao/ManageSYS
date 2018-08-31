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
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);  
            GridView1.DataSource = data;
            GridView1.DataBind();
    }
    protected void btnSaveT_Click(object sender, EventArgs e)
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
      
        
            string[] seg = { "TEAM_CODE", "TEAM_NAME", "WORKSHOP_ID", "CREATE_TIME","IS_DEL" };
            string[] value = { txtCodeT.Text, txtNameT.Text, listLineT.SelectedValue, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") ,"0"};          

            string log_message = opt.MergeInto(seg, value, 1, "ht_sys_team") == "Success" ? "班组配置保存成功" : "班组配置保存失败";
            log_message += "详情:" + string.Join(",", value);
            InsertTlog(log_message);
       
        bindGrid1();
    }

    protected void bindGrid2()
    {
        string query = " select SHIFT_CODE  as 班时编码,SHIFT_NAME  as 班时名称,WORKSHOP_ID  as 所属车间ID,BEGIN_TIME  as 班时开始时间,END_TIME  as 班时结束时间,INTER_DAY as 是否跨天,CREATE_TIME  as 创建时间,MODIFY_TIME  as 修改时间 from ht_sys_shift where is_del = '0' and is_valid = '1' order by SHIFT_CODE";
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView2.DataSource = data;
        GridView2.DataBind();
    }

    protected void btnSaveS_Click(object sender, EventArgs e)
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
     
            string[] seg = { "SHIFT_CODE", "SHIFT_NAME", "WORKSHOP_ID", "BEGIN_TIME", "END_TIME", "CREATE_TIME", "INTER_DAY","IS_DEL" };
            string[] value = { txtCodeS.Text, txtNameS.Text, listLineS.SelectedValue, txtStarttime.Text, txtEndtime.Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Convert.ToInt16(ckInter.Checked).ToString() ,"0"};           

            string log_message = opt.MergeInto(seg, value, 1, "ht_sys_shift") == "Success" ? "班时配置保存成功" : "班时配置保存失败";
            log_message += "详情:" + string.Join(",", value);
            InsertTlog(log_message);
        
        bindGrid2();
    }

    protected void btnGrid1Del_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string query = "update ht_sys_team set IS_DEL = '1'  where TEAM_CODE = '" + GridView1.Rows[rowIndex].Cells[1].Text + "'";
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
       string log_message = opt.UpDateOra(query) == "Success" ? "删除班组成功" : "删除班组失败";
       log_message += "标识:" + GridView1.Rows[rowIndex].Cells[1].Text;
       InsertTlog(log_message);
        bindGrid1();
    }

    protected void btnGrid2Del_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string query = "update ht_sys_shift set IS_DEL = '1'  where SHIFT_CODE = '" + GridView2.Rows[rowIndex].Cells[1].Text + "'";
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
       string log_message = opt.UpDateOra(query) == "Success" ? "删除班时成功" : "删除班时失败";
       log_message += "标识:" + GridView2.Rows[rowIndex].Cells[1].Text;
       InsertTlog(log_message);
        bindGrid2();
    }
}