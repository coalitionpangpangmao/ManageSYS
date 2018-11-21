using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Quality_WeightSet : MSYS.Web.BasePage
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
        string query = "select distinct r.section_code ,r.Weight,r.remark   from ht_pub_tech_section r left join ht_pub_tech_para s on substr(s.para_code,1,5) = r.section_code and s.is_del = '0' and s.is_valid = '1' where r.is_del = '0' and r.is_valid = '1' and  s.para_type like '______1%'   order by r.section_code";
        
       
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
            GridView1.DataSource = data;
            GridView1.DataBind();
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    DataRowView mydrv = data.Tables[0].DefaultView[i];
                    ((DropDownList)GridView1.Rows[i].FindControl("listSection")).SelectedValue = mydrv["section_code"].ToString();
                    ((TextBox)GridView1.Rows[i].FindControl("txtWeight")).Text = mydrv["Weight"].ToString();
                    ((TextBox)GridView1.Rows[i].FindControl("txtRemark")).Text = mydrv["remark"].ToString();

                }
            }
   
    }
    protected void bindGrid2()
    {
        string query = "select ID,NAME,WEIGHT,REMARK from ht_qlt_weight where is_del = '0' and is_valid = '1' order by ID";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView2.DataSource = data;
        GridView2.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                DataRowView mydrv = data.Tables[0].DefaultView[i];              
                ((TextBox)GridView2.Rows[i].FindControl("txtWeight")).Text = mydrv["Weight"].ToString();
                ((TextBox)GridView2.Rows[i].FindControl("txtRemark")).Text = mydrv["remark"].ToString();

            }
        }
    }
    protected DataSet bindSection()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        return opt.CreateDataSetOra("select distinct r.section_code ,r.section_name   from ht_pub_tech_section r left join ht_pub_tech_para s on substr(s.para_code,1,5) = r.section_code and s.is_del = '0' and s.is_valid = '1' where r.is_del = '0' and r.is_valid = '1' and  s.para_type like '______1%'   order by r.section_code");
    }

    protected void btnGrid1Save_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {            
            string[] seg = { "Weight", "remark" };
            string[] value = { ((TextBox)row.FindControl("txtWeight")).Text, ((TextBox)row.FindControl("txtRemark")).Text };
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

            string log_message = opt.UpDateData(seg, value, "HT_PUB_TECH_SECTION", " where section_code = '" + ((DropDownList)row.FindControl("listSection")).SelectedValue + "'") == "Success" ? "修改工艺段质量评估权重成功" : "修改工艺段质量评估权重失败";
            log_message += ",工艺段:" + ((DropDownList)row.FindControl("listSection")).SelectedValue;
            InsertTlog(log_message);
        }
    }

    protected void btnGrid2Save_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView2.Rows)
        {          
            string[] seg = { "Weight", "remark", "CREATE_ID", "CREATE_DATE", "is_adjust" };
            string[] value = { ((TextBox)row.FindControl("txtWeight")).Text, ((TextBox)row.FindControl("txtRemark")).Text, ((MSYS.Data.SysUser)Session["User"]).id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "1" };
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

            string log_message = opt.UpDateData(seg, value, "HT_QLT_WEIGHT", " where ID = '" + GridView2.DataKeys[row.RowIndex].Value.ToString() + "'") == "Success" ? "更改质量评估权重成功" : "更改质量评估权重失败";
            log_message += "--详情:" + string.Join(",", value);
            InsertTlog(log_message);
        }
    }
  


 
   

}