using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using MSYS.Web.StoreService;

public partial class Product_StorageOut : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
          

        }

    }
    protected void bindGrid1()
    {
        MSYS.Web.StorageOpt strg = new MSYS.Web.StorageOpt();
        DataTable data = strg.queryStorage(txtYear.Text, txtname.Text, txtcatgory.Text, txttype.Text, txtprovince.Text, txtwarehouse.Text);
        GridView1.DataSource = data;
        GridView1.DataBind();

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string year = txtYear.Text.ToString().Trim();
        string materialName = txtname.Text.ToString().Trim();
        string province = txtprovince.Text.ToString().Trim();
        string warehouse = txtwarehouse.SelectedValue.ToString().Trim();
        string type = txttype.SelectedValue.ToString().Trim();
        string category = txtcatgory.SelectedValue.ToString().Trim();
        MSYS.Web.StoreService.StoreServiceInterfaceService service = new MSYS.Web.StoreService.StoreServiceInterfaceService();
        tWaOnhand[] lists = service.getOnhandNoBjPageList(materialName.ToString(), type.ToString(), category.ToString(),year.ToString(), province.ToString(), warehouse.ToString(),"");
        System.Diagnostics.Debug.WriteLine(year);
        System.Diagnostics.Debug.WriteLine(materialName);
        System.Diagnostics.Debug.WriteLine(province);
        System.Diagnostics.Debug.WriteLine(warehouse);


        DataTable data = new DataTable();
        data.Columns.Add("ID");
        data.Columns.Add("物料编码");
        data.Columns.Add("物料名称");
        //data.Columns.Add("物料编码");
        data.Columns.Add("仓库");
        data.Columns.Add("货位号");
        data.Columns.Add("类别");
        data.Columns.Add("类型");
        data.Columns.Add("产地");
        data.Columns.Add("库存(kg)");
        data.Columns.Add("使用叶组");
        data.Columns.Add("件数");
        data.Columns.Add("单重(kg)");
        /*if (lists == null || lists.Length == 0)
        {
            System.Diagnostics.Debug.WriteLine("无数据");
            GridView1.DataSource = data;
            GridView1.DataBind();
            return;

        }*/
        if (lists ==  null)
        {

            GridView1.DataSource = null;
            GridView1.DataBind();
            return;
        }
        foreach (tWaOnhand mat in lists)
        {
            string[] paras = new string[] { mat.id.ToString(), mat.materCode, mat.mName,  mat.warehouseName, mat.locationName, mat.materCategory, mat.materType, mat.provinceCode, mat.sumonhand.ToString(), mat.formulaName, mat.matPack, mat.pack };
            data.Rows.Add(paras);
        }

        GridView1.DataSource = data;
        GridView1.DataBind();
    }

}
     
      