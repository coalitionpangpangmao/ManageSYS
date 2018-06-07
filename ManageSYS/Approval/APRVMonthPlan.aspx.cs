using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Approval_APRVMonthPlan : System.Web.UI.Page
{
    protected string AprvtableHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStarttime.Text = System.DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            txtEndtime.Text = System.DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            bindGrid1();
          
        }
 
    }
     
   
    protected void bindGrid1()
    {
        string query = "select g1.tb_zt as 业务名,g1.tbr_name as 申请人,g1.tb_bm_name as 申请部门 ,g1.state as 主业务审批状态,g2.STATUS as 当前流程状态,g2.gongwen_id,g2.id ,g1.BUSIN_ID from HT_PUB_APRV_FLOWINFO g1 left join ht_pub_aprv_opinion g2 on g1.id = g2.gongwen_id  where g1.TB_DATE between '" + txtStarttime.Text + "' and '" + txtEndtime.Text + "' and ISENABLE = '1'";
        if (ckDone.Checked)
            query += " and g1.state = '0'";
       DataBaseOperator opt =new DataBaseOperator();
       // query += " and  g2.rolename = '" + opt.GetSegValue("select * from HT_SVR_USER where LOGINNAME = '" + "cookieName" + "'", "ROLE") + "'";
        //该处思路是找到当前登陆用户的角色，在列表中显示当前角色应审批的流程部分，还有按流程顺序，前面未完的部分不应出现在列表中
        DataSet data = opt.CreateDataSetOra(query);  
            GridView1.DataSource = data;
            GridView1.DataBind();
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
                {
                    DataRowView mydrv = data.Tables[0].DefaultView[i];
                    Label labState1 = (Label)GridView1.Rows[i].FindControl("labStatus1");

                    switch (mydrv["主业务审批状态"].ToString())
                    {
                        case "0":
                            labState1.Text = "办理中";
                            break;
                        case "1":
                            labState1.Text = "被驳回";
                            break;
                        case "2":
                            labState1.Text = "己办结";
                            break;
                        default:
                            labState1.Text = "办理中";
                            break;
                    }
                    Label labState2 = (Label)GridView1.Rows[i].FindControl("labStatus2");

                    switch (mydrv["当前流程状态"].ToString())
                    {
                        case "0":
                            labState2.Text = "未审批";
                            break;
                        case "1":
                            labState2.Text = "未通过";
                            break;
                        case "2":
                            labState2.Text = "己通过";
                            break;
                        default:
                            labState2.Text = "未审批";
                            break;
                    }
                }

            }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid1();

    }
    //全选
    protected void btnCkAll_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                ((CheckBox)GridView1.Rows[i].FindControl("ckBox")).Checked = true;
               
            }
        }
    }
    //批量审批
    protected void btnAprvAll_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
           DataBaseOperator opt =new DataBaseOperator();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("ckBox")).Checked)
                {                   
                    string[] value = {"","2"};
                    if (opt.authorize(GridView1.DataKeys[i].Values[0].ToString(), value))
                        opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "审批成功, 参数：" + "2");
                    else
                        opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "审批失败， 参数：" + "2");
                }
            }
        }

    }
    //业务明细
    protected void btnDetail_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string ID = GridView1.DataKeys[rowIndex].Values[2].ToString();//审批业务ID
        string gong_ID = GridView1.DataKeys[rowIndex].Values[1].ToString();//主审批流程ID
        hideAprvid.Value = GridView1.DataKeys[rowIndex].Values[0].ToString();//子审批流程ID        
       DataBaseOperator opt =new DataBaseOperator();
        string strsql = opt.GetSegValue("select s.plsql from ht_pub_aprv_flowinfo r left join ht_pub_aprv_type s on s.pz_type = r.modulename where r.id = '" + gong_ID + "'", "PLSQL");
        strsql = strsql.Replace("@BUZ_ID", ID);
        GridView2.DataSource = opt.CreateDataSetOra(strsql);
        GridView2.DataBind();
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "DetailClick();", true);
  
    }
    //
    protected void btnFLow_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string ID = GridView1.DataKeys[rowIndex].Values[1].ToString();
        string query = "select pos as 顺序号, workitemid as 审批环节,username as 负责人,comments as 意见,opiniontime 审批时间,(case status when '0' then '未审批'  when '1' then '未通过' else '己通过' end)  as 审批状态  from ht_pub_aprv_opinion where gongwen_ID = '" + ID + "' order by pos";
       DataBaseOperator opt =new DataBaseOperator();
        GridView3.DataSource = opt.CreateDataSetOra(query);
        GridView3.DataBind();
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "Aprvlist();", true);
    }
    //查看审批单
    protected void btnTable_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
         string ID = GridView1.DataKeys[rowIndex].Values[1].ToString();
         AprvtableHtml = initAprvtalbe(ID);
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", "AprvTable();", true);
    }
    //
 
    protected string initAprvtalbe(string id)
    {
        string query = "select s.tb_zt, s.tbr_name,s.tb_date,s.tb_bm_name,s.remark,r.rolename, r.pos,r.username,r.comments,r.opiniontime, r.status,r.workitemid from ht_pub_aprv_opinion r left join ht_pub_aprv_flowinfo s on s.id = r.gongwen_id where r.gongwen_id = '" + id + "' order by pos";
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string aprvstr = "<table class = 'aprvtablelist'><tr><th colspan='4'>";
            aprvstr += data.Tables[0].Rows[0][0].ToString();
            aprvstr += "</th></tr>";
            aprvstr += "<tbody><tr><td> 申请人</td><td>";
            aprvstr += data.Tables[0].Rows[0][1].ToString();
            aprvstr += "</td><td>申请时间</td><td>";
            aprvstr += data.Tables[0].Rows[0][2].ToString();
            aprvstr += "</td></tr><tr><td>申请部门</td><td colspan='3'>";
            aprvstr += data.Tables[0].Rows[0][3].ToString();
            aprvstr += "</td></tr><tr><td>备注</td><td colspan='3'>";
            aprvstr += data.Tables[0].Rows[0][4].ToString();
            aprvstr += "</td></tr>";
            foreach (DataRow row in data.Tables[0].Rows)
            {
                aprvstr += "<tr><td>";
                aprvstr += row["rolename"].ToString() + "意见";
                aprvstr += "</td><td colspan='3'>";
                aprvstr += row["comments"].ToString() + "(" + row["username"].ToString() + row["opiniontime"].ToString() + ")";
                aprvstr += "</td></tr>";
            }
            aprvstr += "<tr><td>说明</td><td colspan='3'><asp:TextBox ID='AprvDscrpt' runat='server'  CssClass = 'aprvtxt' Height = '70px' ></asp:TextBox></td></tr></tbody></table>";
            return aprvstr;
        }
        else
        return "";
    }
   
   
     protected void btnSure_Click(object sender, EventArgs e)
    {    
        
       DataBaseOperator opt =new DataBaseOperator();
         string status;
         if(rdAprv1.Checked)
             status = "2";
         else
             status = "1";
         string[] seg = {txtComments.Text,status};
         if (opt.authorize(hideAprvid.Value, seg))
             opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "业务明细保存成功, 保存参数:" + string.Join(" ", seg));
         else
             opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), "业务明细保存失败, 保存参数:" + string.Join(" ", seg));

    }

     protected void rdAprv1_CheckedChanged(object sender, EventArgs e)
     {
         if (rdAprv1.Checked)
             txtComments.Text = "同意";
         else
             txtComments.Text = "不同意";
     }
     protected void rdAprv2_CheckedChanged(object sender, EventArgs e)
     {
         if (rdAprv2.Checked)
             txtComments.Text = "不同意";
         else
             txtComments.Text = "同意";
     }
}