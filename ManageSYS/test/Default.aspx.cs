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
      //  UpdateUserInfo info = new UpdateUserInfo();
      ////  string str = info.GetXmlStr();
      //  info.InsertLocalFromMaster();
        //UpdateMaterialInfo info2 = new UpdateMaterialInfo();
        //str = info2.GetXmlStr();

        //UpdateMarclassInfo info3 = new UpdateMarclassInfo();
        //str = info2.GetXmlStr();    
    }
    protected void bind()
    {
        /*
        string log_message = opt.InsertData(seg, value, "HT_PROD_SEASON_PLAN") == "Success" ? "新建季度生产计划成功" : "新建季度生产计划失败";
        log_message += "--详情:" + string.Join(",", value);
        InsertTlog(log_message);
         * 
         * 
           string log_message = opt.UpDateOra(query) == "Success" ? "删除权限成功" : "删除权限失败";
       log_message += "--标识:" + id;
        InsertTlog(log_message);
         
         string log_message;
            if (opt.MergeInto(seg, value, 1, "HT_PUB_TECH_SECTION") == "Success")
            {
                log_message = "保存工艺段成功";
                tvHtml = InitTree();
                opt.bindDropDownList(listSection_2, "select section_code,section_name from ht_pub_tech_section where is_del = '0' and is_valid = '1' order by section_code", "section_name", "section_code");
                opt.bindDropDownList(listSection, "select section_code,section_name from ht_pub_tech_section where is_valid = '1' and is_del = '0' order by section_code", "section_name", "section_code");
                ScriptManager.RegisterStartupScript(UpdatePanel4, this.Page.GetType(), "sucess", "initTree();alert('保存成功');", true);
            }
            else
                log_message = "保存工艺段失败";
            log_message += "--数据详情:" + string.Join("-",value);
            InsertTlog(log_message);
         * */


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