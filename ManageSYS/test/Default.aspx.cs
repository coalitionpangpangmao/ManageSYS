using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MSYS.Web;
using System.Text;
using System.IO;
using System.Web.Services;
public partial class test_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UpdateEquipInfo info = new UpdateEquipInfo();
        string str = info.GetXmlStr();

        UpdateMaterialInfo info2 = new UpdateMaterialInfo();
        str = info2.GetXmlStr();

        UpdateMarclassInfo info3 = new UpdateMarclassInfo();
        str = info2.GetXmlStr();    
    }
    protected void bind()
    {
       
    }
  
    [WebMethod]
    public static string treedata() 
    {
        StreamReader sr = new StreamReader(System.Web.HttpRuntime.AppDomainAppPath + "Device\\tree_data1.json", Encoding.Default);
        String line;
        string jsonobj = "";
        while ((line = sr.ReadLine()) != null)
        {
            jsonobj = jsonobj + line.ToString();
        }
        return jsonobj;



    }
    /*
    protected void test()
    {
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                DataRowView mydrv = data.Tables[0].DefaultView[i];

                ((Label)GridView1.Rows[i].FindControl("labAprv")).Text = mydrv["审批状态"].ToString();
                ((Label)GridView1.Rows[i].FindControl("labIssue")).Text = mydrv["下发状态"].ToString();
                if (!(mydrv["审批状态"].ToString() == "未提交" || mydrv["审批状态"].ToString() == "未通过"))
                {
                    ((Button)GridView1.Rows[i].FindControl("btnSubmit")).Enabled = false;
                    ((Button)GridView1.Rows[i].FindControl("btnSubmit")).CssClass = "btngrey";
                    ((Button)GridView1.Rows[i].FindControl("btnGridEdit")).Text = "查看计划";
                }
                else
                {
                    ((Button)GridView1.Rows[i].FindControl("btnSubmit")).Enabled = true;
                    ((Button)GridView1.Rows[i].FindControl("btnSubmit")).CssClass = "btn1 auth";
                    ((Button)GridView1.Rows[i].FindControl("btnGridEdit")).Text = "编制计划";
                }
                if (mydrv["下发状态"].ToString() != "未下发")
                {
                    ((Button)GridView1.Rows[i].FindControl("btnIssued")).Enabled = false;
                    ((Button)GridView1.Rows[i].FindControl("btnIssued")).CssClass = "btngrey";
                }
                else
                {
                    ((Button)GridView1.Rows[i].FindControl("btnIssued")).Enabled = true;
                    ((Button)GridView1.Rows[i].FindControl("btnIssued")).CssClass = "btn1 auth";
                }

            }
        }

        commandlist.Add("delete from ht_pub_aprv_flowinfo where BUSIN_ID = '" + id + "'");


        tvHtml = InitTree();

        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "updatetree", " $('#browser').treeview({ toggle: function () { console.log('%s was toggled.', $(this).find('>span').text());},  persist: 'cookie', collapsed: true });", true);
    }
     * */
}