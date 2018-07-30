using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
public partial class Device_AutoCalibrate : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            txtStart.Text = System.DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd");
            txtStop.Text = System.DateTime.Now.AddDays(45).ToString("yyyy-MM-dd");
            bindGrid1();

        }

    }
    protected void bindGrid1()
    {

        string query = "select t.mt_name as 校准计划,t.pz_code as 计划号,t.expired_date as 过期时间,t1.name as 申请人,t.remark as 备注,t.pz_code from HT_EQ_MCLBR_PLAN t left join ht_svr_user t1 on t1.id = t.create_id   where t.expired_date between '" + txtStart.Text + "' and '" + txtStop.Text + "'  and t.IS_DEL = '0'  and t.CLBRT_TYPE = '1'";

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        GridView1.DataSource = opt.CreateDataSetOra(query); ;
        GridView1.DataBind();


    }//绑定gridview1数据源
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid1();
    }



    protected void btnGridview_Click(object sender, EventArgs e)//查看明细
    {
        Button btn = (Button)sender;
        int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
        txtCode.Value = GridView1.DataKeys[rowIndex].Value.ToString();
        bindGrid2(txtCode.Value);
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "", "$('#tabtop2').click();", true);
    }

    /// <summary>
    /// /tab2
    /// </summary>
     protected DataSet statusbind()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select ID, Name from ht_inner_eqexe_status order by ID ");
    }
    protected DataSet sectionbind()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        return opt.CreateDataSetOra("select section_code,section_name from ht_pub_tech_section where is_del = '0' and is_valid = '1' union select '' as section_code,'' as section_name from dual order by section_code");
    }
    protected void bindGrid2(string code)
    {

        string query = "select t.section as 工段,t.equipment_id as 设备名称,t.point as 数据点,t.OLDVALUE as 原值,t.POINTVALUE as 校准值,t.SAMPLE_TIME as 校准时间,t.STATUS as 状态,t.remark as 备注 ,t.ID  from HT_EQ_MCLBR_PLAN_detail  t where t.main_id = '" + code + "' and t.is_del = '0'";

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView2.DataSource = data;
        GridView2.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {

            for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
            {
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                GridViewRow row = GridView2.Rows[i];
                DropDownList list1 = (DropDownList)row.FindControl("listGridsct");
                DropDownList list2 = (DropDownList)row.FindControl("listGridEq");
                DropDownList list3 = (DropDownList)row.FindControl("listGridPoint");
                list1.SelectedValue = mydrv["工段"].ToString();
                opt.bindDropDownList(list2, "select IDKEY,EQ_NAME  from ht_eq_eqp_tbl where section_code = '" + list1.SelectedValue + "'  order by idkey", "EQ_NAME", "IDKEY");
                list2.SelectedValue = mydrv["设备名称"].ToString();
                opt.bindDropDownList(list3, "select para_code,para_name from ht_pub_tech_para where equip_code = '" + list2.SelectedValue + "' order by para_code ", "para_name", "para_code");
                list3.SelectedValue = mydrv["数据点"].ToString();
                ((TextBox)row.FindControl("txtGridOldvalue")).Text = mydrv["原值"].ToString();
                ((TextBox)row.FindControl("txtGridNewvalue")).Text = mydrv["校准值"].ToString();
                ((TextBox)row.FindControl("txtGridClbrtime")).Text = mydrv["校准时间"].ToString();
                ((DropDownList)row.FindControl("listGrid2Status")).SelectedValue = mydrv["状态"].ToString();
                ((TextBox)row.FindControl("txtGridremark")).Text = mydrv["备注"].ToString();

            }
        }

    }//绑定GridView2数据源




    protected void btnGrid2Save_Click(object sender, EventArgs e)//
    {
        try
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int rowIndex = row.RowIndex;
            string id = GridView2.DataKeys[rowIndex].Value.ToString();
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

            string[] seg = { "ID", "OLDVALUE", "POINTVALUE", "SAMPLE_TIME", "remark", "STATUS" };

            string[] value = { id, ((TextBox)row.FindControl("txtGridOldvalue")).Text, ((TextBox)row.FindControl("txtGridNewvalue")).Text, ((TextBox)row.FindControl("txtGridClbrtime")).Text, ((TextBox)row.FindControl("txtGridremark")).Text, "2" };
            opt.MergeInto(seg, value, 1, "HT_EQ_MCLBR_PLAN_detail");

        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }


}