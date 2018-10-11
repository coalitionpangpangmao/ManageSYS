using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
public partial class Approval_APRVMonthPlan : MSYS.Web.BasePage
{
    protected string AprvtableHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            txtStarttime.Text = System.DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            txtEndtime.Text = System.DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            bindGrid1();
          
        }
 
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView theGrid = sender as GridView;
        int newPageIndex = 0;
        if (e.NewPageIndex == -3)
        {
            //点击跳转按钮
            TextBox txtNewPageIndex = null;

            //GridView较DataGrid提供了更多的API，获取分页块可以使用BottomPagerRow 或者TopPagerRow，当然还增加了HeaderRow和FooterRow
            GridViewRow pagerRow = theGrid.BottomPagerRow;

            if (pagerRow != null)
            {
                //得到text控件
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;
            }
            if (txtNewPageIndex != null)
            {
                //得到索引
                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1;
            }
        }
        else
        {
            //点击了其他的按钮
            newPageIndex = e.NewPageIndex;
        }
        //防止新索引溢出
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;
        //得到新的值
        theGrid.PageIndex = newPageIndex;
        //重新绑定

        bindGrid1();
    }
   
    protected void bindGrid1()
    {
        MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["user"];
        //string query = "select g1.tb_zt as 业务名,g1.tbr_name as 申请人,g1.tb_bm_name as 申请部门 ,g1.state as 主业务审批状态,g2.STATUS as 当前流程状态,g2.gongwen_id,g2.id ,g1.BUSIN_ID from HT_PUB_APRV_FLOWINFO g1 left join ht_pub_aprv_opinion g2 on g1.id = g2.gongwen_id  and g2.rolename = '" + user.UserRole + "'  where g1.TB_DATE between '" + txtStarttime.Text + "' and '" + txtEndtime.Text + "' and ISENABLE = '1'";
        //调试期间用下面SQL，正式运行用上面
        string query = "select g1.tb_zt as 业务名,g1.tbr_name as 申请人,g1.tb_bm_name as 申请部门 ,g1.state as 主业务审批状态,g2.STATUS as 当前流程状态,g2.gongwen_id,g2.id ,g1.BUSIN_ID from HT_PUB_APRV_FLOWINFO g1 left join ht_pub_aprv_opinion g2 on g1.id = g2.gongwen_id    where g1.TB_DATE between '" + txtStarttime.Text + "' and '" + txtEndtime.Text + "' and ISENABLE = '1'";
        if (ckDone.Checked)
            query += " and g1.state = '0'";
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
       // query += " and  g2.rolename = '" + opt.GetSegValue("select * from HT_SVR_USER where LOGINNAME = '" + "cookieName" + "'", "ROLE") + "'";
        //该处思路是找到当前登陆用户的角色，在列表中显示当前角色应审批的流程部分，还有按流程顺序，前面未完的部分不应出现在列表中
        DataSet data = opt.CreateDataSetOra(query);  
            GridView1.DataSource = data;
            GridView1.DataBind();
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                for (int i = GridView1.PageSize * GridView1.PageIndex; i <GridView1.PageSize * (GridView1.PageIndex+1) && i  < data.Tables[0].Rows.Count; i++)
                {
                    DataRowView mydrv = data.Tables[0].DefaultView[i];
                    GridViewRow row = GridView1.Rows[i - GridView1.PageSize * GridView1.PageIndex];
                    Label labState1 = (Label)row.FindControl("labStatus1");

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
                    Label labState2 = (Label)row.FindControl("labStatus2");

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
        int ckno = 0;
        for (int i = 0; i < GridView1.Rows.Count ; i++)
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
    //批量审批
    protected void btnAprvAll_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
           MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("ckBox")).Checked)
                {                   
                    string[] value = {"","2"};
                    string log_message = MSYS.Common.AprvFlow.authorize(GridView1.DataKeys[i].Values[0].ToString(), value) ? "审批成功" : "审批失败";
                    log_message += GridView1.DataKeys[i].Values[0].ToString();
                    InsertTlog(log_message);                  
                }
            }
        }

    }
    //业务明细
    protected void btnDetail_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        if (((Label)row.FindControl("labStatus2")).Text != "未审批")
            btnSure.Visible = false;
        else
            btnSure.Visible = true;
        int rowIndex = row.RowIndex;
        string ID = GridView1.DataKeys[rowIndex].Values[2].ToString();//审批业务ID
        string gong_ID = GridView1.DataKeys[rowIndex].Values[1].ToString();//主审批流程ID
        hideAprvid.Value = GridView1.DataKeys[rowIndex].Values[0].ToString();//子审批流程ID        
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
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
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
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
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);

        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            StringBuilder str = new StringBuilder();
           
            str.Append("<table class = 'aprvtablelist'><tr><th colspan='4'>");
            str.Append(data.Tables[0].Rows[0][0].ToString());
            str.Append("</th></tr>");
            str.Append("<tbody><tr><td> 申请人</td><td>");
            str.Append(data.Tables[0].Rows[0][1].ToString());
            str.Append("</td><td>申请时间</td><td>");
            str.Append(data.Tables[0].Rows[0][2].ToString());
            str.Append("</td></tr><tr><td>申请部门</td><td colspan='3'>");
            str.Append(data.Tables[0].Rows[0][3].ToString());
            str.Append("</td></tr><tr><td>备注</td><td colspan='3'>");
            str.Append(data.Tables[0].Rows[0][4].ToString());
            str.Append("</td></tr>");
            foreach (DataRow row in data.Tables[0].Rows)
            {
                str.Append("<tr><td>");
                str.Append(row["workitemid"].ToString() + "意见");
                str.Append("</td><td colspan='3'>");
                str.Append(row["comments"].ToString() + "(" + row["username"].ToString() + row["opiniontime"].ToString() + ")");
                str.Append("</td></tr>");
            }
            str.Append("<tr><td>说明</td><td colspan='3'><asp:TextBox ID='AprvDscrpt' runat='server'  CssClass = 'aprvtxt' Height = '70px' ></asp:TextBox></td></tr></tbody></table>");
            return str.ToString();
        }
        else
        return "";
    }
   
   
     protected void btnSure_Click(object sender, EventArgs e)
    {    
        
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
         string status;
         if(rdAprv1.Checked)
             status = "2";
         else
             status = "1";
         string[] seg = {txtComments.Text,status};
         string log_message = MSYS.Common.AprvFlow.authorize(hideAprvid.Value, seg) ? "审批成功" : "审批失败";
         log_message += hideAprvid.Value + string.Join(",",seg);
         InsertTlog(log_message);  
         bindGrid1();
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