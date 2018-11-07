using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using MSYS.Data;

public partial class Energy_DataInput : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            System.Diagnostics.Debug.WriteLine("正在初始化下拉菜单");
            opt.bindDropDownList(energyConsumptionPoint,"SELECT DISTINCT ENG_CODE, ENG_NAME FROM HT_ENG_CONSUMPTION_ITEM WHERE IS_DEL!=1 AND IS_VALID =1", "ENG_NAME", "ENG_CODE");
        }

    }

    //初始化数据
    protected void BindData()
    {
        string query = "SELECT ID as 记录ID, ENG_NAME as 能耗点, PROCESS_NAME as 工序编码, TIME as 日期, AMOUNT as 能耗总量, UNIT_NAME as 单位，(case when IS_VALID = 1  then '是' when IS_VALID = 0 then '否'  end) as 是否有效   FROM HT_ENG_MANUAL_DATA WHERE IS_DEL != 1";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data == null)
        {
            System.Diagnostics.Debug.WriteLine("从数据库获取信息失败");
        }
        else
        {
            GridView1.DataSource = data;
            GridView1.DataBind();
        }

        return;

    }

    protected void bindProcessList(object sender, EventArgs e) //当能耗点变动时更新工艺段可选内容
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(processName,"SELECT DISTINCT PROCESS_CODE, PROCESS_NAME FROM HT_ENG_CONSUMPTION_ITEM WHERE IS_DEL=0 AND ENG_CODE="+energyConsumptionPoint.SelectedValue.ToString(),"PROCESS_NAME","PROCESS_CODE");
        
    }

    protected void bindUnitList(object senders, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(department, "SELECT DISTINCT UNIT_CODE ,UNIT_NAME FROM HT_ENG_CONSUMPTION_ITEM WHERE IS_DEL =0 AND ENG_CODE = "+energyConsumptionPoint.SelectedValue.ToString()+ " AND PROCESS_CODE="+processName.SelectedValue.ToString(), "UNIT_NAME", "UNIT_CODE");
    }

    protected void btnEdit_Click(object sender, EventArgs e)//
    {
        try
        {
            Button btn = (Button)sender;
            int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            energyConsumptionPoint.SelectedIndex = energyConsumptionPoint.Items.IndexOf(energyConsumptionPoint.Items.FindByText(GridView1.Rows[rowIndex].Cells[2].Text)); //GridView1.Rows[rowIndex].Cells[2].Text;
            opt.bindDropDownList(processName, "SELECT DISTINCT PROCESS_CODE, PROCESS_NAME FROM HT_ENG_CONSUMPTION_ITEM WHERE IS_DEL=0 AND ENG_CODE=" + energyConsumptionPoint.SelectedValue.ToString(), "PROCESS_NAME", "PROCESS_CODE");       
            //processName.SelectedItem.Text = GridView1.Rows[rowIndex].Cells[3].Text;
            processName.SelectedIndex = processName.Items.IndexOf(processName.Items.FindByText(GridView1.Rows[rowIndex].Cells[3].Text));
            opt.bindDropDownList(department, "SELECT DISTINCT UNIT_CODE ,UNIT_NAME FROM HT_ENG_CONSUMPTION_ITEM WHERE IS_DEL =0 AND ENG_CODE = "+energyConsumptionPoint.SelectedValue.ToString()+ " AND PROCESS_CODE="+processName.SelectedValue.ToString(), "UNIT_NAME", "UNIT_CODE");
            //department.SelectedItem.Text = GridView1.Rows[rowIndex].Cells[6].Text;
            department.SelectedIndex = department.Items.IndexOf(department.Items.FindByText(GridView1.Rows[rowIndex].Cells[6].Text));
            consumeID.Text = GridView1.Rows[rowIndex].Cells[1].Text;
            Time.Text = GridView1.Rows[rowIndex].Cells[4].Text;
            energyConsumption.Text = GridView1.Rows[rowIndex].Cells[5].Text;
            System.Diagnostics.Debug.WriteLine("edit"+energyConsumptionPoint.SelectedItem.Value);
            if (GridView1.Rows[rowIndex].Cells[7].Text == "是")
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

    protected void btnClear_Click(object sender, EventArgs e)//
    {
        try
        {
            Button btn = (Button)sender;
            energyConsumptionPoint.Text = null;
            energyConsumptionPoint.SelectedItem.Text = "";
            processName.SelectedItem.Text = "";
            department.SelectedItem.Text = "";
            consumeID.Text = "";
            Time.Text = "";
            energyConsumption.Text = "";
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
        string[] seg = { "ID", "ENG_CODE","ENG_NAME", "PROCESS_CODE","PROCESS_NAME", "TIME","AMOUNT", "UNIT_CODE","UNIT_NAME", "REMARK","IS_VALID"};
        string tradeTime = DateTime.Now.ToString("yyyyMMddHHmmss", DateTimeFormatInfo.InvariantInfo);
        System.Diagnostics.Debug.WriteLine("name"+processName.SelectedItem.Text);
        string[] value = { tradeTime, energyConsumptionPoint.SelectedValue.ToString(),energyConsumptionPoint.SelectedItem.Text,processName.SelectedValue.ToString(), processName.SelectedItem.Text,Time.Text, energyConsumption.Text, department.SelectedValue.ToString(),department.SelectedItem.Text, remark.Text, Convert.ToInt16(rdValid.Checked).ToString()};
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        System.Diagnostics.Debug.WriteLine(Time.Text);
        string flag = opt.InsertData(seg, value, "HT_ENG_MANUAL_DATA");
        System.Diagnostics.Debug.WriteLine(flag);
        if (flag != "Success")
        {
            Response.Write("<script>alert('添加失败，数据已存在')</script>");

        }
        BindData();
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        //DataBaseOperator opt = new DataBaseOperator();
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "update HT_ENG_MANUAL_DATA set IS_DEL=1 where ID='" + consumeID.Text + "'";
        opt.UpDateOra(query);
        BindData();
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        string[] seg = {"ENG_CODE", "ENG_NAME", "PROCESS_CODE", "PROCESS_NAME", "TIME","AMOUNT", "UNIT_CODE", "UNIT_NAME", "REMARK", "IS_VALID" };     
        string[] value = {energyConsumptionPoint.SelectedValue.ToString(), energyConsumptionPoint.SelectedItem.Text, processName.SelectedValue.ToString(), processName.SelectedItem.Text, Time.Text, energyConsumption.Text, department.SelectedValue.ToString(), department.SelectedItem.Text, remark.Text, Convert.ToInt16(rdValid.Checked).ToString() };
        System.Diagnostics.Debug.WriteLine(Time.Text);
        string condition = " where ID = '" + consumeID.Text + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string flag = opt.UpDateData(seg, value, "HT_ENG_MANUAL_DATA", condition);
        if (flag != "Success")
        {
            System.Diagnostics.Debug.WriteLine(flag);
            System.Diagnostics.Debug.WriteLine("eng"+energyConsumptionPoint.SelectedItem.Text);
            Response.Write("<script>alert('修改数据失败，已存在相同数据')</script>");
        }
        BindData();
    }
} 