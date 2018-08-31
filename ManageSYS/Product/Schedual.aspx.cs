using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
public partial class Product_Schedual : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            txtStartDate.Text = System.DateTime.Now.ToString("yyyy-MM") + "-01";
            txtEndDate.Text = Convert.ToDateTime(txtStartDate.Text).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            bindGrid1();
            bindGrid2();
        }

    }
    protected void bindGrid1()
    {
        string query = "select SHIFT_CODE  as 班时编码,SHIFT_NAME  as 班时,BEGIN_TIME  as 开始时间,END_TIME  as 结束时间,INTER_DAY as 是否跨天 from ht_sys_shift where is_del = '0' and is_valid = '1' order by SHIFT_CODE";
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                DropDownList list = (DropDownList)GridView1.Rows[i].FindControl("listTeam");
                opt.bindDropDownList(list, "select * from ht_sys_team where is_del = '0' and is_valid = '1' order by TEAM_CODE", "TEAM_NAME", "TEAM_CODE");
                ((TextBox)GridView1.Rows[i].FindControl("txtShift")).Text = mydrv["班时"].ToString();
                ((TextBox)GridView1.Rows[i].FindControl("txtStarttime")).Text = mydrv["开始时间"].ToString();
                ((TextBox)GridView1.Rows[i].FindControl("txtEndtime")).Text = mydrv["结束时间"].ToString();
                ((CheckBox)GridView1.Rows[i].FindControl("ckInter")).Checked = Convert.ToBoolean(Convert.ToInt16(mydrv["是否跨天"].ToString()));

            }
        }

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DateTime startdate = Convert.ToDateTime(txtStartDate.Text);
        DateTime enddate = Convert.ToDateTime(txtEndDate.Text);
        if (GridView1.Rows.Count > 0)
        {
            List<string> commandlist = new List<string>();
            MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
            while (startdate < enddate)
            {
               
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    string endtime = startdate.ToString("yyyy-MM-dd") + " " + ((TextBox)GridView1.Rows[i].FindControl("txtEndtime")).Text;
                    if(((CheckBox)GridView1.Rows[i].FindControl("ckInter")).Checked)
                        endtime = startdate.AddDays(1).ToString("yyyy-MM-dd") + " " + ((TextBox)GridView1.Rows[i].FindControl("txtEndtime")).Text;
                    string[] seg = { "WORK_DATE", "WORK_SHOP_CODE", "SHIFT_CODE", "TEAM_CODE", "WORK_STAUS", "CREATE_TIME", "MODIFY_TIME", "DATE_BEGIN", "DATE_END" };
                    string[] value = { startdate.ToString("yyyy-MM-dd"), listPrdline.SelectedValue, GridView1.DataKeys[i].Value.ToString(), ((DropDownList)GridView1.Rows[i].FindControl("listTeam")).SelectedValue, ((DropDownList)GridView1.Rows[i].FindControl("listStatus")).SelectedValue, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), startdate.ToString("yyyy-MM-dd") + " " + ((TextBox)GridView1.Rows[i].FindControl("txtStarttime")).Text, endtime };
                   
                    commandlist.Add( opt.InsertDatastr(seg, value, "HT_PROD_SCHEDULE"));
                }
                startdate = startdate.AddDays(1);
            }
            string log_message =  opt.TransactionCommand(commandlist) == "Success" ? "排班成功" : "排班失败";
            log_message += "排班时间：" + txtStartDate.Text + "~" + txtEndDate.Text;
            InsertTlog(log_message);       
           
        }
        bindGrid2();
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
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

        bindGrid2();
    }
        protected void bindGrid2()
      {
          string query = "select g1.work_date as 日期,g2.team_name as 班组,g3.shift_name as 班时,g1.date_begin as 开始时间,g1.date_end as 结束时间,g1.work_staus as 状态,g1.Id from ht_prod_schedule g1 left join Ht_Sys_Team g2 on g2.team_code = g1.team_code left join ht_sys_shift g3 on g3.shift_code = g1.shift_code where g1.work_date between '" + txtStartDate.Text + "' and '" + txtEndDate.Text + "' and g1.is_del = '0' and g1.is_valid = '1' order by g1.work_date,g1.date_begin";       
         MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
          DataSet data = opt.CreateDataSetOra(query);
          GridView2.DataSource = data;
          GridView2.DataBind();
          if (data != null && data.Tables[0].Rows.Count > 0)
          {
              TableCell oldtc = GridView2.Rows[0].Cells[0];
              oldtc.RowSpan = 1;
              for (int i = GridView2.PageSize * GridView2.PageIndex; i < GridView2.PageSize * (GridView2.PageIndex + 1) && i < data.Tables[0].Rows.Count; i++)
              {
                  int j = i - GridView2.PageSize * GridView2.PageIndex;
                  DataRowView mydrv = data.Tables[0].DefaultView[i];
                  GridViewRow row = GridView2.Rows[j];
                  ((DropDownList)row.FindControl("listStatus2")).SelectedValue = mydrv["状态"].ToString();
                  if (j > 0)
                  {
                      TableCell newtc = GridView2.Rows[j].Cells[0];
                      TableCell restc = GridView2.Rows[j].Cells[0];
                      if (newtc.Text == oldtc.Text)
                      {
                          newtc.Visible = false;
                          oldtc.RowSpan = oldtc.RowSpan + 1;
                          oldtc.VerticalAlign = VerticalAlign.Middle;                       
                      }
                      else
                      {
                          oldtc = newtc;
                          oldtc.RowSpan = 1;                        
                      }
                  }
              }
          }
      }
    
        protected void btnckAll_Click(object sender, EventArgs e)//下发计划
        {
            try
            {
                for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
                {
                    if (((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked)
                        ((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked = false;
                    else
                        ((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked = true;
                }               
            }
            catch (Exception ee)
            {
                Response.Write(ee.Message);
            }
        }
        protected void btnGridEdit_Click(object sender, EventArgs e)//计划编辑
       {
           try
           {
               for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
               {
                   if (((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked)
                   {
                       string id = GridView2.DataKeys[i].Value.ToString();
                       string query = "update ht_prod_schedule set work_staus = '" + ((DropDownList)GridView2.Rows[i].FindControl("listStatus2")).SelectedValue + "'  where ID = '" + id + "'";
                      MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
                      string log_message = opt.UpDateOra(query) == "Success" ? "更新排班状态成功" : "更新排班状态失败";
                      log_message += "标识:" + id;
                      InsertTlog(log_message);
                   }
               }              
           }
           catch (Exception ee)
           {
               Response.Write(ee.Message);
           }
       }
        protected void btnGridDel_Click(object sender, EventArgs e)//删除计划
          {
              try
              {
                  for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
                  {
                      if (((CheckBox)GridView2.Rows[i].FindControl("chk")).Checked)
                      {
                          string id = GridView2.DataKeys[i].Value.ToString();
                          string query = "update ht_prod_schedule set IS_DEL = '1'  where ID = '" + id + "'";
                         MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
                         string log_message = opt.UpDateOra(query) == "Success" ? "删除排班成功" : "删除排班失败";
                         log_message += "标识:" + id;
                         InsertTlog(log_message);
                      }
                  }
                  bindGrid2();
              }
              catch (Exception ee)
              {
                  Response.Write(ee.Message);
              }
          }    

}