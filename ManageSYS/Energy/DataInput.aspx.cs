using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class Energy_DataInput : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }

    }

    //初始化数据
    protected void BindData()
    {
        string query = "SELECT ID as 记录ID, NAME as 能耗点, PROCESS_CODE as 工序编码, B_TIME as 开始时间, E_TIME as 结束时间, AMOUNT as 能耗总量, UNIT as 单位，(case when IS_VALID = 1  then '是' when IS_VALID = 0 then '否'  end) as 是否有效   FROM HT_ENG_MANUAL_DATA WHERE IS_DEL = 0";
        DataBaseOperator opt = new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data == null)
        {
            System.Diagnostics.Debug.WriteLine("从数据库获取信息失败");
        }
        else {
            GridView1.DataSource = data;
            GridView1.DataBind();
        }

        return;
        
    }

    protected void btnEdit_Click(object sender, EventArgs e)//
    {
        System.Diagnostics.Debug.WriteLine("触发按钮");
        try
        {
            Button btn = (Button)sender;
            int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
            System.Diagnostics.Debug.WriteLine("index为："+rowIndex.ToString());
            System.Diagnostics.Debug.WriteLine(GridView1.Rows[rowIndex].Cells[1].Text);
            consumeID.Text = GridView1.Rows[rowIndex].Cells[1].Text;
            StartTime.Text = GridView1.Rows[rowIndex].Cells[4].Text;
            EndTime.Text = GridView1.Rows[rowIndex].Cells[5].Text;
            energyConsumption.Text = GridView1.Rows[rowIndex].Cells[6].Text;
            if (GridView1.Rows[rowIndex].Cells[8].Text == "是")
                rdValid.Checked = true;
            else
                rdValid.Checked = false;

            UpdatePanel1.Update();
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string[] seg = { "ID","NAME", "PROCESS_CODE", "B_TIME", "E_TIME", "AMOUNT", "UNIT", "IS_VALID" };
        string tradeTime = DateTime.Now.ToString("yyyyMMddHHmmss", DateTimeFormatInfo.InvariantInfo);
        System.Diagnostics.Debug.WriteLine(tradeTime);
        string[] value = {tradeTime, energyConsumptionPoint.Text, processName.Text, StartTime.Text, EndTime.Text, energyConsumption.Text, remark.Text, Convert.ToInt16(rdValid.Checked).ToString() };
        DataBaseOperator opt = new DataBaseOperator();
        System.Diagnostics.Debug.WriteLine(StartTime.Text);
        string flag = opt.InsertData(seg, value, "HT_ENG_MANUAL_DATA");
        System.Diagnostics.Debug.WriteLine(flag);
        if (flag!= "Success")
        {
            System.Diagnostics.Debug.WriteLine("插入数据失败");
            
        }
        BindData();
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        DataBaseOperator opt = new DataBaseOperator();
        string query = "update HT_ENG_MANUAL_DATA set IS_DEL=1 where ID='"+consumeID.Text+"'";
        opt.UpDateOra(query);
        BindData();
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        string[] seg = { "NAME", "PROCESS_CODE", "B_TIME", "E_TIME", "AMOUNT", "UNIT", "IS_VALID" };
        string[] value = { energyConsumptionPoint.Text, processName.Text, StartTime.Text, EndTime.Text, energyConsumption.Text, remark.Text, Convert.ToInt16(rdValid.Checked).ToString() };
        string condition = " where ID = '" + consumeID.Text + "'";
        DataBaseOperator opt = new DataBaseOperator();
        opt.UpDateData(seg, value, "HT_ENG_MANUAL_DATA", condition);
        BindData();
    }
}