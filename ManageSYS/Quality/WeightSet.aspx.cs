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
            bindGrid();        
        }
 
    }
  
    protected void bindGrid()
    {
        string query = "select PROD_CODE  as 产品编码,PROD_NAME  as 产品名称,PACK_NAME  as 包装规格,HAND_MODE  as 加工方式,is_valid as 是否有效,(case B_FLOW_STATUS when '-1' then '未提交' when '0' then '办理中' when '1' then '未通过' else '己通过' end) as 审批状态 from ht_pub_prod_design where is_del = '0'";
       
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra(query);
            GridView1.DataSource = data;
            GridView1.DataBind();
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                {
                    DataRowView mydrv = data.Tables[0].DefaultView[i];
                    ((Label)GridView1.Rows[i].FindControl("labGrid1Status")).Text = mydrv["审批状态"].ToString();
                }
            }
   
    }
    protected DataSet bindInspect()
    {
        return null;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
  


 
   

}