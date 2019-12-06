using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
public partial class Quality_CraftEvent : MSYS.Web.BasePage
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
        opt.bindDropDownList(listStatus, "select ID,NAME from ht_inner_inspect_status  order by ID", "NAME", "ID");
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
        string query = "select distinct t.para_name,s.prod_name,a.name as typename,r.value,r.range,r.b_time,r.e_time,h.team_name,nvl(j.status,0) as status,nvl(r.minus_score,0) as minus_score,r.id,r.type,r.prod_code,r.para_code from hv_qlt_data_event r left join ht_pub_prod_design s on s.prod_code = r.prod_code left join ht_pub_tech_para t on t.para_code = r.para_code left join ht_sys_team h on h.team_code = r.team left join ht_qlt_auto_event j on j.record_id = r.id and j.sort = r.type left join ht_inner_qlt_type a on a.id = r.type where r.b_time>'" + txtBtime.Text + "' and r.e_time <'" + txtEtime.Text + "'";
        if (listStatus.SelectedValue != "")
            query += " and nvl(j.status,0) = '" + listStatus.SelectedValue + "'";
        query += "  order by r.b_time,r.id";
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
                switch (status)
                {
                    case "0"://未处理
                        ((Button)row.FindControl("btngrid1Ignore")).CssClass = "btn1";
                        ((Button)row.FindControl("btngrid1Sure")).CssClass = "btn1";
                        ((Button)row.FindControl("btngrid1fdback")).CssClass = "btnhide";
                        ((Button)row.FindControl("btngrid1done")).CssClass = "btnhide";
                        break;

                    case "3"://己处理
                        ((Button)row.FindControl("btngrid1Ignore")).CssClass = "btnhide";
                        ((Button)row.FindControl("btngrid1Sure")).CssClass = "btnhide";
                        ((Button)row.FindControl("btngrid1fdback")).CssClass = "btn1";
                        ((Button)row.FindControl("btngrid1done")).CssClass = "btnhide";
                        break;
                    case "5"://己反馈
                        ((Button)row.FindControl("btngrid1Ignore")).CssClass = "btnhide";
                        ((Button)row.FindControl("btngrid1Sure")).CssClass = "btnhide";
                        ((Button)row.FindControl("btngrid1fdback")).CssClass = "btnhide";
                        ((Button)row.FindControl("btngrid1done")).CssClass = "btn1";
                        break;
                    default://  case "1"://己忽略  case "2"://处理中 case "4"://跟踪中case 6 己完成
                        ((Button)row.FindControl("btngrid1Ignore")).CssClass = "btnhide";
                        ((Button)row.FindControl("btngrid1Sure")).CssClass = "btnhide";
                        ((Button)row.FindControl("btngrid1fdback")).CssClass = "btnhide";
                        ((Button)row.FindControl("btngrid1done")).CssClass = "btnhide";
                        break;
                }
            }
        }
    }

    protected void bindgrid2()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select distinct t.id,r.prod_name,s.team_name,t.inspect_type,t.inspect_code,t.insgroup,t.inspect_name,t.value,t.range,t.unit,t.status,t.minus_score,t.record_time from hv_craft_offline t left join ht_pub_prod_design r on r.prod_code = t.prod_code left join ht_sys_team s on s.team_code = t.team_id where t.RECORD_TIME between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' ";
        if (listStatus.SelectedValue != "")
            query += " and t.status = '" + listStatus.SelectedValue + "'";
        query += " order by t.record_time,t.id";
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
                switch (status)
                {
                    case "0"://未处理
                        ((Button)row.FindControl("btngrid2Ignore")).CssClass = "btn1";
                        ((Button)row.FindControl("btngrid2Sure")).CssClass = "btn1";
                        ((Button)row.FindControl("btngrid2fdback")).CssClass = "btnhide";
                        ((Button)row.FindControl("btngrid2done")).CssClass = "btnhide";
                        break;

                    case "3"://己处理
                        ((Button)row.FindControl("btngrid2Ignore")).CssClass = "btnhide";
                        ((Button)row.FindControl("btngrid2Sure")).CssClass = "btnhide";
                        ((Button)row.FindControl("btngrid2fdback")).CssClass = "btn1";
                        ((Button)row.FindControl("btngrid2done")).CssClass = "btnhide";
                        break;
                    case "5"://己反馈
                        ((Button)row.FindControl("btngrid2Ignore")).CssClass = "btnhide";
                        ((Button)row.FindControl("btngrid2Sure")).CssClass = "btnhide";
                        ((Button)row.FindControl("btngrid2fdback")).CssClass = "btnhide";
                        ((Button)row.FindControl("btngrid2done")).CssClass = "btn1";
                        break;
                    default://  case "1"://己忽略  case "2"://处理中 case "4"://跟踪中case 6 己完成
                        ((Button)row.FindControl("btngrid2Ignore")).CssClass = "btnhide";
                        ((Button)row.FindControl("btngrid2Sure")).CssClass = "btnhide";
                        ((Button)row.FindControl("btngrid2fdback")).CssClass = "btnhide";
                        ((Button)row.FindControl("btngrid2done")).CssClass = "btnhide";
                        break;
                }
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindgrid1();
        bindgrid2();
    }
    /// <summary>
    /// 在线工艺事件
    /// </summary>

    protected void btnSelAll1_Click(object sender, EventArgs e)
    {
        int ckno = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (((CheckBox)GridView1.Rows[i].FindControl("ck")).Checked)
                ckno++;
        }
        bool check = (ckno < GridView1.Rows.Count);
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            ((CheckBox)GridView1.Rows[i].FindControl("ck")).Checked = check;

        }
    }
    protected void btnIgnore1_Click(object sender, EventArgs e)
    {
        MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
        foreach (GridViewRow row in GridView1.Rows)
        {

            if (((CheckBox)row.FindControl("ck")).Checked && ((Label)row.FindControl("labStatus")).Text == "未处理")
            {
                int index = row.RowIndex;
                string[] seg = { "RECORD_ID", "SORT", "SCORE", "STATUS", "CREAT_ID", "CREATE_TIME" };
                string[] value = { GridView1.DataKeys[index].Values[0].ToString(), GridView1.DataKeys[index].Values[1].ToString(), row.Cells[10].Text, "1", user.id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
                MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

                string log_message = opt.MergeInto(seg, value, 2, "HT_QLT_AUTO_EVENT") == "Success" ? "保存工艺质量事件成功" : "保存工艺质量事件失败";
                log_message += "--详情:" + string.Join(",", value);
                InsertTlog(log_message);

            }
        }
        bindgrid1();
    }
    protected void btnConfirm1_Click(object sender, EventArgs e)
    {
        MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (((CheckBox)row.FindControl("ck")).Checked && ((Label)row.FindControl("labStatus")).Text == "未处理")
            {
                int index = row.RowIndex;
                string[] seg = { "RECORD_ID", "SORT", "SCORE", "STATUS", "CREAT_ID", "CREATE_TIME" };
                string[] value = { GridView1.DataKeys[index].Values[0].ToString(), GridView1.DataKeys[index].Values[1].ToString(), row.Cells[10].Text, "2", user.id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
                MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                string log_message = opt.MergeInto(seg, value, 2, "HT_QLT_AUTO_EVENT") == "Success" ? "保存工艺质量事件成功" : "保存工艺质量事件失败";
                log_message += "--详情:" + string.Join(",", value);
                InsertTlog(log_message);
            }
        }
        bindgrid1();
    }
    protected void btngrid1Ignore_Click(object sender, EventArgs e)
    {
        MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int index = row.RowIndex;
        string[] seg = { "RECORD_ID", "SORT", "SCORE", "STATUS", "CREAT_ID", "CREATE_TIME" };
        string[] value = { GridView1.DataKeys[index].Values[0].ToString(), GridView1.DataKeys[index].Values[1].ToString(), row.Cells[10].Text, "1", user.id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string log_message = opt.MergeInto(seg, value, 2, "HT_QLT_AUTO_EVENT") == "Success" ? "忽略工艺质量事件成功" : "忽略工艺质量事件失败";
        log_message += "--详情:" + string.Join(",", value);
        InsertTlog(log_message);
        bindgrid1();
    }
    protected void btngrid1Sure_Click(object sender, EventArgs e)
    {
        MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int index = row.RowIndex;
        string[] seg = { "RECORD_ID", "SORT", "SCORE", "STATUS", "CREAT_ID", "CREATE_TIME" };
        string[] value = { GridView1.DataKeys[index].Values[0].ToString(), GridView1.DataKeys[index].Values[1].ToString(), row.Cells[10].Text, "2", user.id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string log_message = opt.MergeInto(seg, value, 2, "HT_QLT_AUTO_EVENT") == "Success" ? "确认工艺质量事件成功" : "确认工艺质量事件失败";
        log_message += "--详情:" + string.Join(",", value);
        InsertTlog(log_message);
        bindgrid1();
    }

    protected void btngrid1fdback_Click(object sender, EventArgs e)
    {

        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int index = row.RowIndex;

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string log_message = opt.UpDateOra("update HT_QLT_AUTO_EVENT set status = '4' where RECORD_ID = '" + GridView1.DataKeys[index].Values[0].ToString() + "' and SORT = '" + GridView1.DataKeys[index].Values[1].ToString() + "' and status = '3'") == "Success" ? "反馈工艺质量事件成功" : "反馈工艺质量事件失败";
        log_message += "--详情:" + GridView1.DataKeys[index].Values[0].ToString();
        InsertTlog(log_message);
        bindgrid1();
    }

    protected void btngrid1done_Click(object sender, EventArgs e)
    {

        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int index = row.RowIndex;

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string log_message = opt.UpDateOra("update HT_QLT_AUTO_EVENT set status = '6' where RECORD_ID = '" + GridView1.DataKeys[index].Values[0].ToString() + "' and SORT = '" + GridView1.DataKeys[index].Values[1].ToString() + "' and status = '5'") == "Success" ? "反馈工艺质量事件成功" : "反馈工艺质量事件失败";
        log_message += "--详情:" + GridView1.DataKeys[index].Values[0].ToString();
        InsertTlog(log_message);
        bindgrid1();
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
        ScriptManager.RegisterStartupScript(UpdatePanel3, this.Page.GetType(), "", " $('.shade').fadeIn(200);", true);

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
            Rows = Rows.FindAll(s => s.status == "过程值"&& (s.value < lower || s.value > upper));
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


    protected void btnFeed1_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            int index = row.RowIndex;

            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string log_message = opt.UpDateOra("update HT_QLT_AUTO_EVENT set status = '4' where RECORD_ID = '" + GridView1.DataKeys[index].Values[0].ToString() + "' and SORT = '" + GridView1.DataKeys[index].Values[1].ToString() + "' and status = '3'") == "Success" ? "反馈工艺质量事件成功" : "反馈工艺质量事件失败";
            log_message += "--详情:" + GridView1.DataKeys[index].Values[0].ToString();
            InsertTlog(log_message);
            bindgrid1();
        }
    }

    protected void btnDone1_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            int index = row.RowIndex;

            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string log_message = opt.UpDateOra("update HT_QLT_AUTO_EVENT set status = '6' where RECORD_ID = '" + GridView1.DataKeys[index].Values[0].ToString() + "' and SORT = '" + GridView1.DataKeys[index].Values[1].ToString() + "' and status = '5'") == "Success" ? "反馈工艺质量事件成功" : "反馈工艺质量事件失败";
            log_message += "--详情:" + GridView1.DataKeys[index].Values[0].ToString();
            InsertTlog(log_message);
            bindgrid1();
        }
    }
    /// <summary>
    /// 离线工艺事件
    /// </summary>
    protected void btnSelAll_Click(object sender, EventArgs e)
    {
        int ckno = 0;
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            if (((CheckBox)GridView2.Rows[i].FindControl("ck")).Checked)
                ckno++;
        }
        bool check = (ckno < GridView2.Rows.Count);
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            ((CheckBox)GridView2.Rows[i].FindControl("ck")).Checked = check;

        }
    }
    protected void btnIgnore_Click(object sender, EventArgs e)
    {
        MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
        foreach (GridViewRow row in GridView2.Rows)
        {
            if (((CheckBox)row.FindControl("ck")).Checked && ((Label)row.FindControl("labStatus")).Text == "未处理")
            {
                int index = row.RowIndex;
                string[] seg = { "RECORD_ID", "INSPECT_CODE", "STATUS", "SCORE", "CREAT_ID", "CREATE_TIME" };
                string[] value = { GridView2.DataKeys[index].Values[0].ToString(), GridView2.DataKeys[index].Values[1].ToString(), "1", row.Cells[11].Text, user.id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
                MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();


                string log_message = opt.MergeInto(seg, value, 1, "HT_QLT_INSPECT_EVENT") == "Success" ? "保存工艺质量事件成功" : "保存工艺质量事件失败";
                log_message += "--详情:" + string.Join(",", value);
                InsertTlog(log_message);
            }
        }
        bindgrid2();
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
        foreach (GridViewRow row in GridView2.Rows)
        {
            if (((CheckBox)row.FindControl("ck")).Checked && ((Label)row.FindControl("labStatus")).Text == "未处理")
            {
                int index = row.RowIndex;
                string[] seg = { "RECORD_ID", "INSPECT_CODE", "STATUS", "SCORE", "CREAT_ID", "CREATE_TIME" };
                string[] value = { GridView2.DataKeys[index].Values[0].ToString(), GridView2.DataKeys[index].Values[1].ToString(), "2", row.Cells[11].Text, user.id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
                MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
                string log_message = opt.MergeInto(seg, value, 1, "HT_QLT_INSPECT_EVENT") == "Success" ? "确认工艺质量事件成功" : "确认工艺质量事件失败";
                log_message += "--详情:" + string.Join(",", value);
                InsertTlog(log_message);
            }
        }
        bindgrid2();
    }
    protected void btngrid2Ignore_Click(object sender, EventArgs e)
    {
        MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int index = row.RowIndex;
        string[] seg = { "RECORD_ID", "INSPECT_CODE", "STATUS", "SCORE", "CREAT_ID", "CREATE_TIME" };
        string[] value = { GridView2.DataKeys[index].Values[0].ToString(), GridView2.DataKeys[index].Values[1].ToString(), "1", row.Cells[11].Text, user.id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string log_message = opt.MergeInto(seg, value, 1, "HT_QLT_INSPECT_EVENT") == "Success" ? "忽略工艺质量事件成功" : "忽略工艺质量事件失败";
        log_message += "--详情:" + string.Join(",", value);
        InsertTlog(log_message);
        bindgrid2();
    }
    protected void btngrid2Sure_Click(object sender, EventArgs e)
    {
        MSYS.Data.SysUser user = (MSYS.Data.SysUser)Session["User"];
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int index = row.RowIndex;
        string[] seg = { "RECORD_ID", "INSPECT_CODE", "STATUS", "SCORE", "CREAT_ID", "CREATE_TIME" };
        string[] value = { GridView2.DataKeys[index].Values[0].ToString(), GridView2.DataKeys[index].Values[1].ToString(), "2", row.Cells[11].Text, user.id, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string log_message = opt.MergeInto(seg, value, 1, "HT_QLT_INSPECT_EVENT") == "Success" ? "保存工艺质量事件成功" : "保存工艺质量事件失败";
        log_message += "--详情:" + string.Join(",", value);
        InsertTlog(log_message);
        bindgrid2();
    }

    protected void btngrid2fdback_Click(object sender, EventArgs e)
    {

        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int index = row.RowIndex;

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        string log_message = opt.UpDateOra("update HT_QLT_INSPECT_EVENT set status = '4' where RECORD_ID = '" + GridView2.DataKeys[index].Values[0].ToString() + "' and INSPECT_CODE = '" + GridView2.DataKeys[index].Values[1].ToString() + "' and status = '3'") == "Success" ? "反馈工艺质量事件成功" : "反馈工艺质量事件失败";
        log_message += "--详情:" + GridView2.DataKeys[index].Values[0].ToString();
        InsertTlog(log_message);
        bindgrid2();
    }


    protected void btngrid2done_Click(object sender, EventArgs e)
    {

        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int index = row.RowIndex;

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string log_message = opt.UpDateOra("update HT_QLT_INSPECT_EVENT set status = '6' where RECORD_ID = '" + GridView2.DataKeys[index].Values[0].ToString() + "' and INSPECT_CODE = '" + GridView2.DataKeys[index].Values[1].ToString() + "' and status = '5'") == "Success" ? "完结工艺质量事件成功" : "完结工艺质量事件失败";
        log_message += "--详情:" + GridView2.DataKeys[index].Values[0].ToString();
        InsertTlog(log_message);
        bindgrid2();
    }
    protected void btnFeed_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView2.Rows)
        {
            int index = row.RowIndex;

            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string log_message = opt.UpDateOra("update HT_QLT_INSPECT_EVENT set status = '4' where RECORD_ID = '" + GridView2.DataKeys[index].Values[0].ToString() + "' and INSPECT_CODE = '" + GridView2.DataKeys[index].Values[1].ToString() + "' and status = '3'") == "Success" ? "反馈工艺质量事件成功" : "反馈工艺质量事件失败";
            log_message += "--详情:" + GridView2.DataKeys[index].Values[0].ToString();
            InsertTlog(log_message);
            bindgrid2();
        }
    }

    protected void btnDone_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView2.Rows)
        {
            int index = row.RowIndex;

            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            string log_message = opt.UpDateOra("update HT_QLT_INSPECT_EVENT set status = '6' where RECORD_ID = '" + GridView2.DataKeys[index].Values[0].ToString() + "' and INSPECT_CODE = '" + GridView2.DataKeys[index].Values[1].ToString() + "' and status = '5'") == "Success" ? "反馈工艺质量事件成功" : "反馈工艺质量事件失败";
            log_message += "--详情:" + GridView2.DataKeys[index].Values[0].ToString();
            InsertTlog(log_message);
            bindgrid2();
        }
    }
}