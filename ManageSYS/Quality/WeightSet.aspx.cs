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
        string query = "select section_code ,Weight,remark from ht_pub_tech_section  where is_del = '0' and is_valid = '1' order by section_code";
       
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
            GridView1.DataSource = data;
            GridView1.DataBind();
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < data.Tables[0].Rows.Count; i++)
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

        return opt.CreateDataSetOra("select section_name, section_code  from ht_pub_tech_section  where is_del = '0' and is_valid = '1' order by section_code");
    }

    protected void btnGrid1Save_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string[] seg = {  "Weight", "remark" };
        string[] value = { ((TextBox)GridView1.Rows[rowindex].FindControl("txtWeight")).Text, ((TextBox)GridView1.Rows[rowindex].FindControl("txtRemark")).Text };
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.UpDateData(seg, value, "HT_PUB_TECH_SECTION", " where section_code = '" + ((DropDownList)GridView1.Rows[rowindex].FindControl("listSection")).SelectedValue + "'");
    }

    protected void btnGrid2Save_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string[] seg = { "Weight", "remark" ,"CREATE_ID","CREATE_DATE"};
        string[] value = { ((TextBox)GridView2.Rows[rowindex].FindControl("txtWeight")).Text, ((TextBox)GridView2.Rows[rowindex].FindControl("txtRemark")).Text, ((MSYS.Data.SysUser)Session["User"]).id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.UpDateData(seg, value, "HT_QLT_WEIGHT", " where ID = '" + GridView2.DataKeys[rowindex].Value.ToString() + "'");
    }
  


 
   

}