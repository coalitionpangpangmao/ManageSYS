using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.IO;
public partial class Quality_Inspect_online : MSYS.Web.BasePage
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
        txtEtime.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listProd, "select distinct t.prod_code,r.prod_name from ht_prod_report t left join ht_pub_prod_design r on r.prod_code = t.prod_code where r.is_valid = '1' and r.is_del = '0' and  substr(t.starttime,1,10) between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' or substr(t.endtime,1,7) between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' or (t.starttime > '" + txtBtime.Text + "' and t.endtime < '" + txtEtime.Text + "') and t.prod_code is not null  and r.prod_name is not null", "prod_name", "prod_code");
        opt.bindDropDownList(listSection, "select section_code,section_name from ht_pub_tech_section  where is_valid = '1' and is_del = '0' order by section_code", "section_name", "section_code");
        opt.bindDropDownList(listPoint, "select para_code,para_name from ht_pub_tech_para t where  para_type like '___1%' and is_del = '0'", "para_name", "para_code");
        opt.bindDropDownList(listBatch, "select distinct plan_id from ht_qlt_data_record t where b_time between '" + txtBtime.Text + "' and '" + txtEtime.Text + "'", "plan_id", "plan_id");
        opt.bindDropDownList(listTeam, "select t.team_code,t.team_name from ht_sys_team t where t.is_del = '0' order by t.team_code", "team_name", "team_code");
        bindgrid();
    }
    protected string getQuerystr()
    {
        string query;
        if (rdAll.Checked)
        {
            if (listTeam.SelectedValue == "")
                query = "select distinct g.prod_code,g.para_code, h.prod_name as 产品,j.para_name as 工艺点,k.team_name as 班组,g.b_time as 开始时间,g.e_time as 结束时间,round(g.avg,2) as 均值,g.count as 采样点数,round(g.min,2) as 最小值,round(g.max,2) as 最大值,round(g.quarate,2) as 合格率,round(g.uprate,2) as 超上限率,round(g.downrate,2) as 超下限率,round(g.stddev,2) as 标准差,round(g.absdev,2) as 绝对差,round(g.range,2) as 范围,round(g.cpk,2) as cpk  from (select t.prod_code,t.para_code,'' as team,round(sum(t.avg*t.count)/sum(t.count),2) as avg,sum(t.count) as count,min(t.min) as min,max(t.max) as max,round(sum(t.quanum)/sum(t.count),2) as quarate,round(sum(t.upcount)/sum(t.count),2) as uprate,round(sum(t.downcount)/sum(t.count),2) as downrate,round(sqrt(sum(t.stddev*t.stddev*(t.count-1))/(sum(t.count)-1)),2) as stddev,round(sum(t.absdev *t.count)/sum(t.count),2) as absdev,round((max(t.max) - min(t.min)),2) as range ,null as cpk,min(b_time) as b_time,max(e_time) as e_time  from ht_qlt_data_record t where substr(t.b_time,1,10) between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' ";
            else
                query = "select distinct g.prod_code,g.para_code, h.prod_name as 产品,j.para_name as 工艺点,k.team_name as 班组,g.b_time as 开始时间,g.e_time as 结束时间,round(g.avg,2) as 均值,g.count as 采样点数,round(g.min,2) as 最小值,round(g.max,2) as 最大值,round(g.quarate,2) as 合格率,round(g.uprate,2) as 超上限率,round(g.downrate,2) as 超下限率,round(g.stddev,2) as 标准差,round(g.absdev,2) as 绝对差,round(g.range,2) as 范围,round(g.cpk,2) as cpk  from (select t.prod_code,t.para_code,t.team,round(sum(t.avg*t.count)/sum(t.count),2) as avg,sum(t.count) as count,min(t.min) as min,max(t.max) as max,round(sum(t.quanum)/sum(t.count),2) as quarate,round(sum(t.upcount)/sum(t.count),2) as uprate,round(sum(t.downcount)/sum(t.count),2) as downrate,round(sqrt(sum(t.stddev*t.stddev*(t.count-1))/(sum(t.count)-1)),2) as stddev,round(sum(t.absdev *t.count)/sum(t.count),2) as absdev,round((max(t.max) - min(t.min)),2) as range ,null as cpk,min(b_time) as b_time,max(e_time) as e_time  from ht_qlt_data_record t where substr(t.b_time,1,10) between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' ";

        }
        else
            query = "select distinct g.prod_code,g.para_code, h.prod_name as 产品,j.para_name as 工艺点,k.team_name as 班组,g.b_time as 开始时间,g.e_time as 结束时间,round(g.avg,2) as 均值,g.count as 采样点数,round(g.min,2) as 最小值,round(g.max,2) as 最大值,round(g.quarate,2) as 合格率,round(g.uprate,2) as 超上限率,round(g.downrate,2) as 超下限率,round(g.stddev,2) as 标准差,round(g.absdev,2) as 绝对差,round(g.range,2) as 范围,round(g.cpk,2) as cpk  from (select distinct t.prod_code, t.para_code, t.team, t.avg, t.count, t.min, t.max, t.quarate, t.uprate, t.downrate, t.stddev, t.absdev, t.range, t.cpk, t.b_time, t.e_time from ht_qlt_data_record t where substr(t.b_time,1,10) between '" + txtBtime.Text + "' and '" + txtEtime.Text + "'";

        if (listTeam.SelectedValue != "")
        {
            query += "  and t.team = '" + listTeam.SelectedValue + "'";
        }

        if (listProd.SelectedValue != "")
        {
            query += " and t.prod_code = '" + listProd.SelectedValue + "'";
        }
        if (listPoint.SelectedValue != "")
        {
            query += " and t.para_code = '" + listPoint.SelectedValue + "'";
        }

        if (listBatch.SelectedValue != "")
        {
            query += " and t.plan_id = '" + listBatch.SelectedValue + "'";
        }

        if (listSection.SelectedValue != "")
        {
            query += " and substr(t.para_code,1,5) = '" + listSection.SelectedValue + "'";
        }

        if (rdAll.Checked == true)
        {
            if (listTeam.SelectedValue == "")
                query += "group by (t.plan_id,t.para_code,t.prod_code) ) g left join ht_pub_prod_design h on h.prod_code = g.prod_code left join ht_pub_tech_para j on j.para_code = g.para_code left join ht_sys_team k on k.team_code = g.team order by h.prod_name,j.para_name,g.e_time,k.team_name,g.count ";
            else
                query += "group by (t.plan_id,t.para_code,t.prod_code,t.team) ) g left join ht_pub_prod_design h on h.prod_code = g.prod_code left join ht_pub_tech_para j on j.para_code = g.para_code left join ht_sys_team k on k.team_code = g.team order by h.prod_name,j.para_name,g.e_time,k.team_name,g.count ";
        }
        else
            query += ") g left join ht_pub_prod_design h on h.prod_code = g.prod_code left join ht_pub_tech_para j on j.para_code = g.para_code left join ht_sys_team k on k.team_code = g.team order by h.prod_name,j.para_name,g.e_time,k.team_name,g.count";
        return query;
    }
    protected void bindgrid()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = getQuerystr();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();

        if (data == null || data.Tables[0].Rows.Count <= 1)
        {
            return;
        }

        //for (int j = GridView1.PageIndex * GridView1.PageCount; j < (GridView1.PageIndex + 1) * GridView1.PageCount && j < GridView1.Rows.Count; j++)
        //{
        //    DataRowView myv = (DataRowView)data.Tables[0].DefaultView[j];
        //    int i = j - GridView1.PageIndex * GridView1.PageCount;
        //    if (myv["cpk"].ToString() == "")
        //    {               
        //        GridView1.Rows[i].BackColor = Color.FromArgb(80, 0, 200, 180);
        //    }
        //}
      
        for (int i = 0; i < 2; i++)
        {
            TableCell oldtc = GridView1.Rows[0].Cells[i];

            oldtc.RowSpan = 1;
            
            for (int j = 1; j < GridView1.Rows.Count; j++)
            {
                TableCell newtc = GridView1.Rows[j].Cells[i];
                if (newtc.Text == oldtc.Text)
                {
                    newtc.Visible = false;
                    oldtc.RowSpan = oldtc.RowSpan + 1;
                    oldtc.VerticalAlign = VerticalAlign.Middle;
                    oldtc.BorderStyle = BorderStyle.Solid;
                    oldtc.BorderWidth = 1;
                }
                else
                {
                    oldtc.BorderStyle = BorderStyle.Solid;
                    oldtc.BorderWidth = 1;
                    oldtc = newtc;
                    oldtc.RowSpan = 1;
                }
            }
        }



        //TableCell oldtc1 = GridView1.Rows[0].Cells[0];
        //TableCell oldtc2 = GridView1.Rows[0].Cells[1];    
        //oldtc1.RowSpan = 1;
        //oldtc2.RowSpan = 2;
        //for (int j = 1; j < GridView1.Rows.Count; j++)
        //{
        //    TableCell newtc1 = GridView1.Rows[j].Cells[0];

        //    if (newtc1.Text == oldtc1.Text)
        //    {
        //        newtc1.Visible = false;
        //        oldtc1.RowSpan = oldtc1.RowSpan + 1;
        //        oldtc1.VerticalAlign = VerticalAlign.Middle;
        //    }
        //    else
        //    {
        //        for (int h = j+1 - oldtc1.RowSpan; h < oldtc1.RowSpan+1; h++)
        //        {
        //            TableCell newtc2 = GridView1.Rows[h].Cells[1];
        //            if (newtc2.Text == oldtc2.Text)
        //            {
        //                newtc2.Visible = false;
        //                oldtc2.RowSpan = oldtc2.RowSpan + 1;
        //                oldtc2.VerticalAlign = VerticalAlign.Middle;
        //            }
        //            else
        //            {
        //                oldtc2 = newtc2;
        //                oldtc2.RowSpan = 1;
        //            }
        //        }
        //        oldtc1 = newtc1;
        //        oldtc1.RowSpan = 1;
        //    }
        //}               


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

        bindgrid();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindgrid();
    }


    protected void txtBtime_TextChanged(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listProd, "select distinct t.prod_code,r.prod_name from ht_prod_report t left join ht_pub_prod_design r on r.prod_code = t.prod_code where r.is_valid = '1' and r.is_del = '0' and  substr(t.starttime,1,10) between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' or substr(t.endtime,1,7) between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' or (t.starttime > '" + txtBtime.Text + "' and t.endtime < '" + txtEtime.Text + "') and t.prod_code is not null and r.prod_name is not null", "prod_name", "prod_code");
        opt.bindDropDownList(listBatch, "select distinct plan_id from ht_qlt_data_record t where b_time between '" + txtBtime.Text + "' and '" + txtEtime.Text + "'", "plan_id", "plan_id");
    }

    protected void listSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listPoint, "select para_code,para_name from ht_pub_tech_para  where is_valid = '1' and is_del = '0' and substr(para_code,1,5) = '" + listSection.SelectedValue + "' and para_type like '___1%'", "para_name", "para_code");
    }
    protected void txtEtime_TextChanged(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listProd, "select distinct t.prod_code,r.prod_name from ht_prod_report t left join ht_pub_prod_design r on r.prod_code = t.prod_code where r.is_valid = '1' and r.is_del = '0' and  substr(t.starttime,1,10) between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' or substr(t.endtime,1,7) between '" + txtBtime.Text + "' and '" + txtEtime.Text + "' or (t.starttime > '" + txtBtime.Text + "' and t.endtime < '" + txtEtime.Text + "') and t.prod_code is not null  and r.prod_name is not null", "prod_name", "prod_code");
        opt.bindDropDownList(listBatch, "select distinct plan_id from ht_qlt_data_record t where b_time between '" + txtBtime.Text + "' and '" + txtEtime.Text + "'", "plan_id", "plan_id");
    }


    protected void btngrid1View_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int index = row.RowIndex;
       
        string prodcode = GridView1.DataKeys[index].Values[0].ToString();
        string para_code = GridView1.DataKeys[index].Values[1].ToString();
        string starttime = GridView1.DataKeys[index].Values[2].ToString();
        string stoptime = GridView1.DataKeys[index].Values[3].ToString();
        
        MSYS.IHAction ihopt = new MSYS.IHAction();
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        //   List<MSYS.IHAction.ParaInfo> paralist = ihopt.GetData(txtBtime.Text, txtEtime.Text, listpara.SelectedValue);
        DataTable dt = new DataTable();
        dt.Columns.Add("时间");
        dt.Columns.Add("值");
        dt.Columns.Add("状态");
        if(rdAll.Checked && listTeam.SelectedValue != "")
        {
            string query = "select distinct g.prod_code,g.para_code, h.prod_name as 产品,j.para_name as 工艺点,k.team_name as 班组,g.b_time as 开始时间,g.e_time as 结束时间,round(g.avg,2) as 均值,g.count as 采样点数,round(g.min,2) as 最小值,round(g.max,2) as 最大值,round(g.quarate,2) as 合格率,round(g.uprate,2) as 超上限率,round(g.downrate,2) as 超下限率,round(g.stddev,2) as 标准差,round(g.absdev,2) as 绝对差,round(g.range,2) as 范围,round(g.cpk,2) as cpk  from (select distinct t.prod_code, t.para_code, t.team, t.avg, t.count, t.min, t.max, t.quarate, t.uprate, t.downrate, t.stddev, t.absdev, t.range, t.cpk, t.b_time, t.e_time from ht_qlt_data_record t where t.b_time between '" + starttime + "' and '" + stoptime + "'  and t.team = '" + listTeam.SelectedValue + "'  and t.prod_code = '" + prodcode + "'  and t.para_code = '" + para_code + "') g left join ht_pub_prod_design h on h.prod_code = g.prod_code left join ht_pub_tech_para j on j.para_code = g.para_code left join ht_sys_team k on k.team_code = g.team order by h.prod_name,j.para_name,g.e_time,k.team_name,g.count";
            DataSet data = opt.CreateDataSetOra(query);
            if (data != null && data.Tables[0].Rows.Count > 1)
            {
                foreach (DataRow srow in data.Tables[0].Rows)
                {
                    starttime = srow["开始时间"].ToString();
                    stoptime = srow["结束时间"].ToString();
                    MSYS.IHAction.TimeSeg seg = ihopt.GetTimeSegP(starttime, stoptime, para_code, prodcode);
                    List<MSYS.IHAction.ParaRes> Rows = ihopt.GetIHRealDataSet(seg);

                    foreach (MSYS.IHAction.ParaRes info in Rows)
                    {
                        var values = new object[3];
                        values[0] = info.timestamp;
                        values[1] = info.value;
                        values[2] = info.status;
                        dt.Rows.Add(values);
                    }
                }
            }
        }
        else
        {
            MSYS.IHAction.TimeSeg seg = ihopt.GetTimeSegP(starttime, stoptime, para_code, prodcode);
            List<MSYS.IHAction.ParaRes> Rows = ihopt.GetIHRealDataSet(seg);
           
            foreach (MSYS.IHAction.ParaRes info in Rows)
            {
                var values = new object[3];
                values[0] = info.timestamp;
                values[1] = info.value;
                values[2] = info.status;
                dt.Rows.Add(values);
            }
        }
        MSYS.Common.ExcelExport openXMLExcel = null;

        

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
        string filename = "在线质量统计" + System.DateTime.Now.ToString("HHmmss") + ".xls";
        String sourcePath = basedir + @"templates\在线质量统计.xls";
        String filepath = basedir + @"TEMP\" + filename;

        try
        {
            //申明一个ExcelSaveAs对象，该对象完成将数据写入Excel的操作           
            openXMLExcel = new MSYS.Common.ExcelExport(sourcePath, false);
          
           
          
            openXMLExcel.SetCurrentSheet(0);

            openXMLExcel.WriteData(2, 2, row.Cells[1].Text);
            openXMLExcel.WriteData(2, 6, row.Cells[0].Text);
            openXMLExcel.WriteData(3, 2, row.Cells[3].Text);
            openXMLExcel.WriteData(3, 6, row.Cells[4].Text);
            openXMLExcel.WriteData(4, 2, row.Cells[2].Text);
            openXMLExcel.WriteData(4, 6, row.Cells[6].Text);
            openXMLExcel.WriteData(6, 1, row.Cells[5].Text);
            openXMLExcel.WriteData(6, 2, row.Cells[7].Text);
            openXMLExcel.WriteData(6, 3, row.Cells[8].Text);
            openXMLExcel.WriteData(6, 4, row.Cells[9].Text);
            openXMLExcel.WriteData(6, 5, row.Cells[10].Text);
            openXMLExcel.WriteData(6, 6, row.Cells[11].Text);
            openXMLExcel.WriteData(6, 7, row.Cells[12].Text);
            openXMLExcel.WriteData(6, 8, row.Cells[13].Text);
            openXMLExcel.WriteData(6, 9, row.Cells[14].Text);
            openXMLExcel.WriteData(6, 10, row.Cells[15].Text);
            openXMLExcel.WriteDataIntoWorksheet(9, 1, dt);


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
}