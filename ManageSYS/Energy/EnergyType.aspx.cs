using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using MSYS.Data;

public partial class EnergyType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            //MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            //System.Diagnostics.Debug.WriteLine("正在初始化下拉菜单");
            //opt.bindDropDownList(energyConsumptionPoint, "SELECT DISTINCT ENG_CODE, ENG_NAME FROM HT_ENG_CONSUMPTION_ITEM WHERE IS_DEL=0", "ENG_NAME", "ENG_CODE");
        }

    }

    //初始化数据
    protected void BindData()
    {
        string query = "SELECT ID as 记录ID, ENG_NAME as 能耗项目, ENG_CODE as 能耗项目编码, PROCESS_NAME as 能耗工艺段, PROCESS_CODE as 能耗工艺段编码 ,UNIT_NAME as 能耗类型,UNIT_CODE as 能耗类型编码  ,(case when IS_VALID = 1  then '是' when IS_VALID = 0 then '否'  end) as 是否有效   FROM HT_ENG_CONSUMPTION_ITEM WHERE IS_DEL != 1 ";
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

    protected void btnEdit_Click(object sender, EventArgs e)//
    {
        try
        {
            Button btn = (Button)sender;
            int rowIndex = ((GridViewRow)btn.NamingContainer).RowIndex;
            ENG_NAME.Text = GridView1.Rows[rowIndex].Cells[2].Text;
            ENG_CODE.Text = GridView1.Rows[rowIndex].Cells[3].Text;
            UNIT_NAME.Text = GridView1.Rows[rowIndex].Cells[4].Text;
            UNIT_CODE.Text = GridView1.Rows[rowIndex].Cells[5].Text;
            PROCESS_NAME.Text = GridView1.Rows[rowIndex].Cells[6].Text;
            PROCESS_CODE.Text = GridView1.Rows[rowIndex].Cells[7].Text;
            ENERGYID.Text = GridView1.Rows[rowIndex].Cells[1].Text;
            string query = "SELECT REMARK FROM HT_ENG_CONSUMPTION_ITEM WHERE ID ="+ENERGYID.Text;
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            DataSet data = opt.CreateDataSetOra(query);
            remark.Text = data.Tables[0].Rows[0][0].ToString();
            System.Diagnostics.Debug.WriteLine("remark is "+ remark.Text);
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

    protected void btnClear_Click(object sender, EventArgs e)//
    {
        try{
            Button btn = (Button)sender;
            ENG_NAME.Text ="";
            ENG_CODE.Text = "";
            UNIT_NAME.Text = "";
            UNIT_CODE.Text = "";
            PROCESS_NAME.Text = "";
            PROCESS_CODE.Text = "";
            remark.Text = "";
            ENERGYID.Text = "请勿输入";
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
        string[] seg = { "ID", "ENG_CODE", "ENG_NAME", "PROCESS_CODE", "PROCESS_NAME", "CREATE_TIME", "UNIT_CODE", "UNIT_NAME", "REMARK", "IS_VALID" ,"IS_DEL"};
        string tradeTime = DateTime.Now.ToString("yyyyMMddHHmmss", DateTimeFormatInfo.InvariantInfo);
        string createTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
        string[] value = { tradeTime, ENG_CODE.Text, ENG_NAME.Text, PROCESS_CODE.Text, PROCESS_NAME.Text, createTime, UNIT_CODE.Text, UNIT_NAME.Text, remark.Text, Convert.ToInt16(rdValid.Checked).ToString(), "0" };
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string flag = opt.InsertData(seg, value, "HT_ENG_CONSUMPTION_ITEM");
        if (flag != "Success")
        {
            Response.Write("<script>alert('添加失败，数据已存在')</script>");

        }
        BindData();
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "update HT_ENG_CONSUMPTION_ITEM set IS_DEL=1 where ID='" + ENERGYID.Text + "'";
        opt.UpDateOra(query);
        BindData();
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        string[] seg = { "ENG_CODE", "ENG_NAME", "PROCESS_CODE", "PROCESS_NAME", "UNIT_NAME", "UNIT_CODE", "REMARK", "IS_VALID" };
        string[] value = { ENG_CODE.Text, ENG_NAME.Text, PROCESS_CODE.Text, PROCESS_NAME.Text, UNIT_NAME.Text, UNIT_CODE.Text, remark.Text, Convert.ToInt16(rdValid.Checked).ToString() };
        //string[] value = { energyConsumptionPoint.SelectedValue.ToString(), energyConsumptionPoint.SelectedItem.Text, processName.SelectedValue.ToString(), processName.SelectedItem.Text, StartTime.Text, EndTime.Text, energyConsumption.Text, department.SelectedValue.ToString(), department.SelectedItem.Text, remark.Text, Convert.ToInt16(rdValid.Checked).ToString() };
        string condition = " where ID =" + ENERGYID.Text;
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string flag = opt.UpDateData(seg, value, "HT_ENG_CONSUMPTION_ITEM", condition);
        if (flag != "Success")
        {
            Response.Write("<script>alert('修改数据失败，已存在相同数据')</script>");
        }
        BindData();
    }
}