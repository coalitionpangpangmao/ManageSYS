using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
public partial class Device_RepairRecord : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            txtStart.Text = System.DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd");
            txtStop.Text = System.DateTime.Now.AddDays(45).ToString("yyyy-MM-dd");
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listEq, "select IDKEY,EQ_NAME from ht_eq_eqp_tbl where is_del = '0' and is_valid = '1'", "EQ_NAME", "IDKEY");
            opt.bindDropDownList(listOptor, "select ID,name  from ht_svr_user t where is_del ='0' ", "name", "ID");
           
            opt.bindDropDownList(listArea, "select r.section_code,r.section_name from ht_pub_tech_section r  where r.is_del = '0' and r.is_valid = '1'  union select '' as section_code,'' as section_name from dual order by section_code", "section_name", "section_code");
            try
            {
                string equip = Request.QueryString["Equip"];
                if (equip != null)
                    hideequip.Value = equip;
                bindGrid();
            }
            catch
            {
            }

        }

    }
    protected void bindGrid()
    {

        string query = "select t2.mt_name as 维保计划名,t2.pz_code as 凭证号, t3.section_name as 区域,t1.eq_name as 设备名称,t.reason as 维修原因,t.content as 维修内容,t.exp_finish_time as 期望完成时间,t4.name as 状态,t.remark as 备注 ,t.ID  from HT_EQ_RP_PLAN_detail t left join Ht_Eq_Eqp_Tbl t1 on t1.idkey = t.equipment_id left join HT_EQ_RP_PLAN t2 on t.main_id = t2.pz_code  left join ht_pub_tech_section t3 on t3.section_code = t.mech_area left join ht_inner_eqexe_status t4 on t4.id = t.status  where  t.is_del = '0'   and t.exp_finish_time between '" + txtStart.Text + "' and '" + txtStop.Text + "'   and t2.FLOW_STATUS = '2'  and t.STATUS  = '5' and t2.is_del = '0' union select '应急维修' as 维保计划名,'' as 凭证号, t3.section_name as 区域,t1.eq_name as 设备名称,t.reason as 维修原因,t.content as 维修内容,t.exp_finish_time as 期望完成时间,t4.name as 状态,t.remark as 备注 ,t.ID  from HT_EQ_RP_PLAN_detail t left join Ht_Eq_Eqp_Tbl t1 on t1.idkey = t.equipment_id  left join ht_pub_tech_section t3 on t3.section_code = t.mech_area left join ht_inner_eqexe_status t4 on t4.id = t.status  where  t.is_del = '0'   and t.create_time between '" + txtStart.Text + "' and '" + txtStop.Text + "'    and t.STATUS  = '5' ";
        if (hideequip.Value != "")
            query += " and t.equipment_id = " + hideequip.Value;
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();     

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid();
    }


    protected void btnGrid1View_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow Row = (GridViewRow)btn.NamingContainer;
        int rowIndex = Row.RowIndex;
        string ID = GridView1.DataKeys[rowIndex].Value.ToString();
      
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select * from HT_EQ_RP_PLAN_DETAIL where id = '" + ID + "'");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            DataRow row = data.Tables[0].Rows[0];
            txtPlanname.Text = Row.Cells[1].Text;
            txtPlanno.Text = Row.Cells[2].Text;
            txtCode.Text = ID;
            listEq.SelectedValue = row["EQUIPMENT_ID"].ToString();
            txtOpttime.Text = row["EXE_TIME"].ToString();
            listOptor.SelectedValue = row["RESPONER"].ToString();
            listArea.SelectedValue = row["MECH_AREA"].ToString();
            txtRecord.Text = row["REASON"].ToString() + ";" + row["CONTENT"].ToString() + ";" + row["REMARK"].ToString();
            string ftid = row["FAULT_ID"].ToString();
            txtFalut.Text = opt.GetSegValue("select '故障名:'||t.error_name||';故障描述：'|| t.error_description||';故障场景：'||t.scean||';故障原因：'||t.failure_cause||'；解决方案'||t.solution as faultinfo from ht_eq_fault_db t  where t.ID = '" + ftid + "'", "faultinfo");
            txtFeedback.Text = row["FEEDBACK"].ToString() + ";" + row["REMARKPLUS"].ToString();        
           
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "$('.shade').fadeIn(200);", true);

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
        bindGrid();
    }

  
}