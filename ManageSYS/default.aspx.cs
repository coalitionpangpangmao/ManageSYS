using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using MSYS.Data;
public partial class _default : MSYS.Web.BasePage
{
    protected string tvhtml;
    protected string warnHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        tvhtml = initNav();
        warnHtml = WarnEvent();
    }
    protected string initNav()
    {       
        StringBuilder str = new StringBuilder();
        string[] urls = new string[] { "Approval/APRVMonthPlan.aspx", "Craft/Recipe.aspx", "Product/Plan.aspx", "Product/StorageMater.aspx", "Quality/CraftEvent.aspx", "Quality/EventDeal.aspx", "Device/MtncExe.aspx", "Device/RepairExe.aspx", "Device/LbrctExe.aspx" };
        string[] menus = new string[]{"业务审批","配方编辑","生产计划排产","出入库确认","工艺事件确认","工艺事件处理","设备维保","设备维修","设备润滑"};
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        str.Append("<ul class='newlist'>");
       for(int i = 0;i<urls.Length;i++)
        {
            string mappingId = opt.GetSegValue("select * from ht_inner_map t where t.url = '" + urls[i] + "'", "MAPID");
            SysRightCollection rightCol = ((SysUser)Session["User"]).UserRights;
            var query = from SysRight right in rightCol
                        where right.mapID == mappingId && right.eType == SysRight.RightType.Button
                        select right;
            foreach (SysRight s in query)
            {
               str.Append("<li><a href='");
                str.Append(urls[i]);
                str.Append("' target='rightFrame'>");
                str.Append(menus[i]);
                str.Append("</a></li>");
            }
        }
       str.Append("</ul>");
       return str.ToString();

    }

    protected string WarnEvent()
    {
        StringBuilder str = new StringBuilder();
        string[] urls = new string[] {  "Device/MtncExe.aspx", "Device/RepairExe.aspx", "Device/LbrctExe.aspx" };
        string[] menus = new string[] { "维保过期提醒", "维修过期提醒", "润滑过期提醒" };
        string[] sqls = new string[]{"select t.mt_name as 维保计划,t1.f_name as 部门, t3.name as 执行状态,t.expired_date as 过期时间,t.pz_code as 凭证号 from ht_eq_mt_plan t left join ht_svr_org_group t1 on t1.f_code = t.create_dept_id   left join ht_inner_eqexe_status t3 on t3.id = t.task_status  where t.expired_date between '" +  System.DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + "' and '" + System.DateTime.Now.AddDays(7).ToString("yyyy-MM-dd") + "'  and t.IS_DEL = '0' and t.is_model = '0' and t.task_status <>'5' order by t.expired_date",
            " select t.mt_name as 维修计划,t1.f_name as 部门,t3.name as 执行状态,t.expired_date as 过期时间,t.pz_code as 凭证号 from HT_EQ_RP_PLAN t left join ht_svr_org_group t1 on t1.f_code = t.create_dept_id   left join ht_inner_aprv_status t2 on t2.id = t.flow_status left join ht_inner_eqexe_status t3 on t3.id = t.task_status  where t.expired_date between '" + System.DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + "' and '" + System.DateTime.Now.AddDays(7).ToString("yyyy-MM-dd") + "'  and t.IS_DEL = '0' and t.task_status <> '5' order by t.expired_date",
            "select t.mt_name as 润滑计划,t1.f_name as 部门,t3.name as 执行状态,t.expired_date as 过期时间,t.pz_code as 凭证号  from ht_eq_lb_plan t left join ht_svr_org_group t1 on t1.f_code = t.create_dept_id   left join ht_inner_eqexe_status t3 on t3.id = t.task_status   where t.expired_date between '" + System.DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + "' and '" + System.DateTime.Now.AddDays(7).ToString("yyyy-MM-dd") + "'  and t.IS_DEL = '0' and t.FLOW_STATUS <> '5' order by t.expired_date"};

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();      
        for (int i = 0; i < urls.Length; i++)
        {
            string mappingId = opt.GetSegValue("select * from ht_inner_map t where t.url = '" + urls[i] + "'", "MAPID");
            SysRightCollection rightCol = ((SysUser)Session["User"]).UserRights;
            var query = from SysRight right in rightCol
                        where right.mapID == mappingId && right.eType == SysRight.RightType.Button
                        select right;
            foreach (SysRight s in query)
            {
                DataSet data = opt.CreateDataSetOra(sqls[i]);
                if (data != null && data.Tables[0].Rows.Count > 0)
                {
                    str.Append("<div class='listtitle'>");
                    str.Append(menus[i]);
                    str.Append("</div>");
                    str.Append("<table width='100%' border='1' cellpadding='0' cellspacing='1' bgcolor='#a8c7ce'>");

                    str.Append("<tr>");
                    for (int h = 0;h < data.Tables[0].Columns.Count; h++)
                    {
                        str.Append(" <td  height='25px' bgcolor='d3eaef' class='staticHead' width = '70px' border='1'><div align='center'><span class='staticHeadtext'>");                       
                            str.Append(data.Tables[0].Columns[h].Caption);                       
                        str.Append("</span></div></td>");
                    }
                    str.Append("</tr>");
                    for (int j = 0; j < data.Tables[0].Rows.Count; j++)
                    {
                        str.Append("<tr>");
                        for (int h = 0; h < data.Tables[0].Columns.Count; h++)
                        {
                            str.Append("  <td height='25px' bgcolor='#FFFFFF' class='staticHead' border='1'><div align='center'><span class='staticRow'>");
                            str.Append(data.Tables[0].Rows[j][h].ToString());
                            str.Append("</span></div></td>");
                        }
                        str.Append("</tr>");
                    }
                    str.Append("</table>");
                }
              
            }
        }
      
        return str.ToString();
    }
}