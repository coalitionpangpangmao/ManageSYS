using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
public partial class Quality_EventDeal : MSYS.Web.BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            initView();
        }
    }
    protected void initView()
    {
        txtBtime.Text = System.DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
        txtEtime.Text = System.DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listStyle, "select ID,Name from ht_inner_qlt_type union select inspect_code as ID,inspect_name as name from ht_qlt_inspect_proj where is_del = '0'", "Name", "ID");
        bindgrid1();
        bindgrid2();
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

        bindgrid1();
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

        bindgrid2();
    }
    protected void bindgrid1()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select distinct t.para_name,s.prod_name,k.name,r.value,r.range,r.b_time,r.e_time,h.team_name,nvl(j.status,0) as status,r.id ,r.type,r.prod_code,r.para_code from hv_qlt_data_event r left join ht_pub_prod_design s on s.prod_code = r.prod_code left join ht_pub_tech_para t on t.para_code = r.para_code left join ht_sys_team h on h.team_code = r.team left join ht_qlt_auto_event j on j.record_id = r.id and j.sort = r.type left join ht_inner_qlt_type k on k.id = r.type where r.b_time>'" + txtBtime.Text + "' and r.e_time <'" + txtEtime.Text + "' and j.status in ( '2','3')  order by r.b_time,r.id";

        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string status = "";
            for (int i = GridView1.PageSize * GridView1.PageIndex; i < GridView1.PageSize * (GridView1.PageIndex + 1) && i < data.Tables[0].Rows.Count; i++)
            {
                int j = i - GridView1.PageSize * GridView1.PageIndex;
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                GridViewRow row = GridView1.Rows[j];
                status = mydrv["status"].ToString();
                ((Label)row.FindControl("labStatus")).Text = opt.GetSegValue("select * from ht_inner_inspect_status where id = '" + status + "'", "NAME");
            }
        }

    }

    protected void bindgrid2()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select t.id,r.prod_name,s.team_name,t.inspect_type,t.inspect_code,t.insgroup,t.inspect_name,t.value,t.range,t.unit,t.status,t.minus_score,t.record_time from hv_craft_offline t left join ht_pub_prod_design r on r.prod_code = t.prod_code left join ht_sys_team s on s.team_code = t.team_id where t.RECORD_TIME between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' and t.status in ( '2','3')  order by  t.record_time ,t.id ";
        DataSet data = opt.CreateDataSetOra(query);
        GridView2.DataSource = data;
        GridView2.DataBind();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string status = "";
          
             for (int i = GridView2.PageSize * GridView2.PageIndex; i < GridView2.PageSize * (GridView2.PageIndex + 1) && i < data.Tables[0].Rows.Count; i++)
            {
                int j = i - GridView2.PageSize * GridView2.PageIndex;
                DataRowView mydrv = data.Tables[0].DefaultView[i];
                GridViewRow row = GridView2.Rows[j];
                status = mydrv["status"].ToString();
                ((Label)row.FindControl("labStatus")).Text = opt.GetSegValue("select * from ht_inner_inspect_status where id = '" + status + "'", "NAME");
            }
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindgrid1();
        bindgrid2();
    }

    protected void btngrid2Deal_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdType.Value = "2";
        txtEventID.Text = GridView2.DataKeys[row.RowIndex].Values[0].ToString();
        listStyle.SelectedValue = GridView2.DataKeys[row.RowIndex].Values[1].ToString();
        if (((Button)row.FindControl("btngrid2Deal")).Text == "查看")
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet data = opt.CreateDataSetOra("select * from HT_QLT_INSPECT_EVENT where RECORD_ID = '" + txtEventID.Text + "' and INSPECT_CODE = '" + listStyle.SelectedValue + "'");
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                DataRow drow = data.Tables[0].Rows[0];
                txtScean.Text = drow["SCENE"].ToString();
                txtReason.Text = drow["REASON"].ToString();
                txtDeal.Text = drow["DEAL"].ToString();
                txtPlus.Text = drow["REMARK"].ToString();
            }
        }
        else
        {
            txtScean.Text = "";
            txtReason.Text = "";
            txtDeal.Text = "";
            txtPlus.Text = "";
        }
       
        ScriptManager.RegisterStartupScript(updtpanel1, this.Page.GetType(), "", " $('.shade').fadeIn(200);", true);

    }

    protected void btngrid1Deal_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdType.Value = "1";
        txtEventID.Text = GridView1.DataKeys[row.RowIndex].Values[0].ToString();
        listStyle.SelectedValue = GridView1.DataKeys[row.RowIndex].Values[1].ToString();
        if (((Button)row.FindControl("btngrid1Deal")).Text == "查看")
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet data = opt.CreateDataSetOra("select * from HT_QLT_AUTO_EVENT where RECORD_ID = '" + txtEventID.Text + "' and SORT = '" + listStyle.SelectedValue + "'");
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                DataRow drow = data.Tables[0].Rows[0];
                txtScean.Text = drow["SCENE"].ToString();
                txtReason.Text = drow["REASON"].ToString();
                txtDeal.Text = drow["DEAL"].ToString();
                txtPlus.Text = drow["REMARK"].ToString();
            }
        }
        else
        {
            txtScean.Text = "";
            txtReason.Text = "";
            txtDeal.Text = "";
            txtPlus.Text = "";
        }

        ScriptManager.RegisterStartupScript(updtpanel1, this.Page.GetType(), "", " $('#craftdetail').fadeIn(200);", true);

    }
    protected void btngrid1View_Click(object sender, EventArgs e)
    {

        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int index = row.RowIndex;
        lbPoint.Text = row.Cells[2].Text;
        lbprod_code.Text = GridView1.DataKeys[index].Values[2].ToString();
        lbpara_code.Text = GridView1.DataKeys[index].Values[3].ToString();
        lbsTime.Text = row.Cells[6].Text;
        lbeTime.Text = row.Cells[7].Text;
        bindGrid3();
        ScriptManager.RegisterStartupScript(UpdatePanel3, this.Page.GetType(), "", " $('#pointdetail').fadeIn(200);", true);

    }


    protected void btnPointView_Click(object sender, EventArgs e)
    {
        bindGrid3();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        MSYS.IHAction ihopt = new MSYS.IHAction();
        MSYS.IHAction.TimeSeg seg = ihopt.GetTimeSegP(lbsTime.Text, lbeTime.Text, lbpara_code.Text, lbprod_code.Text);
        List<MSYS.IHAction.ParaRes> Rows = ihopt.GetIHRealDataSet(seg);
        if (Rows == null)
            return;
        DataTable dt = new DataTable();
        dt.Columns.Add("时间");
        dt.Columns.Add("值");
        dt.Columns.Add("状态");
        if (rdUnQualified.Checked == true)
        {
            string query = "select r.upper_limit,r.lower_limit from ht_tech_stdd_code_detail r left join ht_pub_prod_design t on t.tech_stdd_code = r.tech_code where r.para_code = '" + lbpara_code.Text + "' and t.prod_code = '" + lbprod_code.Text + "'";
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            double upper = Convert.ToDouble(opt.GetSegValue(query, "upper_limit"));
            double lower = Convert.ToDouble(opt.GetSegValue(query, "lower_limit"));
            Rows = Rows.FindAll(s => s.status == "过程值" && (s.value < lower || s.value > upper));
        }

        foreach (MSYS.IHAction.ParaRes info in Rows)
        {
            var values = new object[3];
            values[0] = info.timestamp;
            values[1] = info.value;
            values[2] = info.status;
            dt.Rows.Add(values);
        }
        MSYS.Common.ExcelExport openXMLExcel = null;
        try
        {
            string basedir = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
            string strFolderPath = basedir + @"\TEMP";
            if (!System.IO.Directory.Exists(strFolderPath))
            {
                // 目录不存在，建立目录 
                System.IO.Directory.CreateDirectory(strFolderPath);
            }

            DirectoryInfo dyInfo = new DirectoryInfo(strFolderPath);
            //获取文件夹下所有的文件
            foreach (FileInfo feInfo in dyInfo.GetFiles())
            {
                //判断文件日期是否小于今天，是则删除
                if (feInfo.CreationTime < DateTime.Now.AddMinutes(-2))
                    feInfo.Delete();
            }
            foreach (DirectoryInfo dir in dyInfo.GetDirectories())
            {
                if (dir.CreationTime < DateTime.Now.AddMinutes(-2))
                    dir.Delete(true);
            }
            //导出文件模板所在位置
            string filename = "原始数据" + System.DateTime.Now.ToString("HHmmss") + ".xls";
            String sourcePath = basedir + @"templates\原始数据.xls";
            String filepath = basedir + @"TEMP\" + filename;
            openXMLExcel = new MSYS.Common.ExcelExport(sourcePath, false);
            openXMLExcel.SetCurrentSheet(0);

            openXMLExcel.WriteData(2, 2, lbPoint.Text);
            openXMLExcel.WriteData(3, 2, lbsTime.Text + "~" + lbeTime.Text);
            openXMLExcel.WriteDataIntoWorksheet(5, 1, dt);


            ///客户端再下载该文件，在客户端进行浏览
            FileInfo fi = new FileInfo(filepath);
            if (fi.Exists)     //判断文件是否已经存在,如果存在就删除!
            {
                fi.Delete();
            }
            openXMLExcel.SaveAs(filepath);

            openXMLExcel.Dispose();
            openXMLExcel = null;
            KillProcess("EXCEL.EXE");

            Response.Clear();
            Response.Buffer = true;

            Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8));
            Response.ContentType = "application/vnd.ms-excel";
            Response.TransmitFile(filepath);

            //  Response.End();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ee)
        {
            if (openXMLExcel != null)
            {
                openXMLExcel.Dispose();
            }

        }
    }
    protected void bindGrid3()
    {
        MSYS.IHAction ihopt = new MSYS.IHAction();
        MSYS.IHAction.TimeSeg seg = ihopt.GetTimeSegP(lbsTime.Text, lbeTime.Text, lbpara_code.Text, lbprod_code.Text);
        List<MSYS.IHAction.ParaRes> Rows = ihopt.GetIHRealDataSet(seg);
        if (Rows == null)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "msg", "alert('无数据，请查看数据库状态是否正常！’)", true);
        }
        else
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("时间");
            dt.Columns.Add("值");
            dt.Columns.Add("状态");
            if (rdUnQualified.Checked == true)
            {
                string query = "select r.upper_limit,r.lower_limit from ht_tech_stdd_code_detail r left join ht_pub_prod_design t on t.tech_stdd_code = r.tech_code where r.para_code = '" + lbpara_code.Text + "' and t.prod_code = '" + lbprod_code.Text + "'";
                MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                double upper = Convert.ToDouble(opt.GetSegValue(query, "upper_limit"));
                double lower = Convert.ToDouble(opt.GetSegValue(query, "lower_limit"));
                Rows = Rows.FindAll(s => s.status == "过程值" && (s.value < lower || s.value > upper));
            }

            foreach (MSYS.IHAction.ParaRes info in Rows)
            {
                var values = new object[3];
                values[0] = info.timestamp;
                values[1] = info.value;
                values[2] = info.status;
                dt.Rows.Add(values);
            }
            GridView3.DataSource = dt;
            GridView3.DataBind();

        }
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        if (hdType.Value == "1")
        {
            string[] seg = { "RECORD_ID", "SORT", "STATUS", "REASON", "SCENE", "DEAL", "REMARK" };
            string[] value = { txtEventID.Text, listStyle.SelectedValue, "3", txtReason.Text, txtScean.Text, txtDeal.Text, txtPlus.Text };
            
            string log_message = opt.MergeInto(seg, value, 2, "HT_QLT_AUTO_EVENT") == "Success" ? "处理工艺质量事件成功" : "处理工艺质量事件失败";
            log_message += "--详情:" + string.Join(",", value);
            InsertTlog(log_message);
            bindgrid1();

        }
        else
        {
            string[] seg = { "RECORD_ID", "INSPECT_CODE", "STATUS", "REASON", "SCENE", "DEAL", "REMARK" };
            string[] value = { txtEventID.Text, listStyle.SelectedValue, "3", txtReason.Text, txtScean.Text, txtDeal.Text, txtPlus.Text };
            string log_message = opt.MergeInto(seg, value, 1, "HT_QLT_INSPECT_EVENT") == "Success" ? "处理工艺质量事件成功" : "处理工艺质量事件失败";
            log_message += "--详情:" + string.Join(",", value);
            InsertTlog(log_message);
            bindgrid2();
        }
        ScriptManager.RegisterStartupScript(updtpanel1, this.Page.GetType(), "", " $('.shade').fadeOut(200);", true);
    }


}