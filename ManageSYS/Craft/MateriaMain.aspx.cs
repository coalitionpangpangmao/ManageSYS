using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Craft_MateriaMain : MSYS.Web.BasePage
{
    protected string tvHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            opt.bindDropDownList(listPrt1, "select mattree_code，mattree_name,PARENT_CODE  from ht_pub_mattree where is_del = '0' order by PARENT_CODE", "mattree_name", "mattree_code");
            opt.bindDropDownList(listType2, "select  MATTREE_CODE ,  MATTREE_NAME ,  PARENT_CODE    from HT_PUB_MATTREE where  is_del = '0' order by PARENT_CODE", "MATTREE_NAME", "MATTREE_CODE");
            tvHtml = InitTree();
        }
    }

   
    public   string  InitTree()
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select mattree_code,mattree_name  from ht_pub_mattree where IS_DEL = '0'  and length(mattree_code) = 2  order by mattree_code ");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul id='browser' class='filetree treeview-famfamfam' >";
            DataRow[] rows = data.Tables[0].Select();
          foreach (DataRow row in rows)
            {  
                tvHtml += "<li ><span class='folder' value = '" + row["mattree_code"].ToString() + "'>" + row["mattree_name"].ToString() + "</span></a>";
                tvHtml += InitTreeM(row["mattree_code"].ToString());
                tvHtml += "</li>";
            }
          tvHtml += "</ul>";
          return tvHtml;     
       
        }
        else
            return "";
    }

    public  string InitTreeM(string mattree_code)
    {
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
       DataSet data = opt.CreateDataSetOra("select mattree_code,mattree_name  from ht_pub_mattree where IS_DEL = '0' and  PARENT_CODE = '" + mattree_code + "' order by mattree_code ");
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                tvHtml += "<li ><span class='folder' value = '" + row["mattree_code"].ToString() + "'>" + row["mattree_name"].ToString() + "</span></a>";
                tvHtml += InitTreeM(row["mattree_code"].ToString());
               tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("开始更新");
        ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "update", "alert('开始更新！！！')", true);
        MSYS.Web.UpdateMarclassInfo info = new MSYS.Web.UpdateMarclassInfo();
        MSYS.Web.UpdateMaterialInfo info2 = new MSYS.Web.UpdateMaterialInfo();
        //  string str = info.GetXmlStr();
        if ("Success" == info.InsertLocalFromMaster() && "Success" == info2.InsertLocalFromMaster())
        {           
            ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "update", "alert('更新完毕！！！')", true);
        }
        else
            ScriptManager.RegisterStartupScript(UpdatePanel2, this.Page.GetType(), "update", "alert('更新失败，请检查网络设置及数据运行情况！！！')", true);
        System.Diagnostics.Debug.WriteLine("更新结束");
    }
    #region tab1
    protected void bindData(string mtr_code)
    {
        string query = "select * from HT_PUB_MATTREE where  MATTREE_CODE = '" + mtr_code + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            txtCode1.Text = mtr_code;
            txtName1.Text = data.Tables[0].Rows[0]["MATTREE_NAME"].ToString();
            listPrt1.SelectedValue = data.Tables[0].Rows[0]["PARENT_CODE"].ToString();
            ckValid1.Checked = ("1"== data.Tables[0].Rows[0]["IS_VALID"].ToString());
        }
        bindGrid(mtr_code);


    }

    protected void btnAdd1_Click(object sender, EventArgs e)
    {
        string query = "select nvl(Max(mattree_code),0) as code  from ht_pub_mattree";
        if (listPrt1.SelectedValue == "")
            query += " where Parent_code is null";
        else
            query += " where Parent_code ='" + listPrt1.SelectedValue + "'";

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        int codelength = listPrt1.SelectedValue.Length;
        string code = opt.GetSegValue(query, "code");

        if (codelength == 6)
        {
            if (code == "0")
                code = listPrt1.SelectedValue + "000";
            codelength = 9;
        }
        else
        {
            if (code == "0")
                code = listPrt1.SelectedValue + "00";
            codelength += 2;
        }

        txtCode1.Text = (Convert.ToInt32(code) + 1).ToString().PadLeft(codelength, '0');

        txtName1.Text = "";

        bindGrid(txtCode1.Text);


    }
    protected void btnModify1_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        string[] seg = { "mattree_code", "MATTREE_NAME", "PARENT_CODE", "IS_VALID" };
        string[] value = { txtCode1.Text, txtName1.Text, listPrt1.SelectedValue, Convert.ToInt16(ckValid1.Checked).ToString() };

        List<String> commandlist = new List<String>();
        commandlist.Add("delete from HT_PUB_MATTREE where MATTREE_CODE = '" + txtCode1.Text + "'");
        commandlist.Add(opt.InsertDatastr(seg, value, "HT_PUB_MATTREE"));


        string log_message = opt.TransactionCommand(commandlist) == "Success" ? "分类修改成功" : "分类修改失败";
        log_message += ",分类信息：" + string.Join(",", value);
        InsertTlog(log_message);
        bindGrid(txtCode1.Text);
        tvHtml = InitTree();
        ScriptManager.RegisterStartupScript(UpdatePanel4, this.Page.GetType(), "init", " initTree();", true);

    }
    protected void btnDel1_Click(object sender, EventArgs e)
    {
        string query = "update HT_PUB_MATTREE set IS_DEL = '1'  where MATTREE_CODE = '" + txtCode1.Text + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string log_message = opt.UpDateOra(query) == "Success" ? "分类删除成功" : "分类删除失败";
        log_message += ", 分类编码：" + txtCode1.Text;
        InsertTlog(log_message);
        bindGrid(txtCode1.Text);
        tvHtml = InitTree();
        ScriptManager.RegisterStartupScript(UpdatePanel4, this.Page.GetType(), "init", " initTree();", true);
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

        bindGrid(txtCode1.Text);
    }
    protected void bindGrid(string mtr_code)
    {

        string query = "select MATERIAL_CODE as 物料编码,MATERIAL_NAME as 物料名称,UNIT_CODE as 单位编码,MAT_CATEGORY as 类别,MAT_TYPE as 类型,MAT_LEVEL as 等级,LAST_UPDATE_TIME as 更新时间 from HT_PUB_MATERIEL where is_del = '0'   ";
        if (mtr_code != "")
            query += " and ( TYPE_CODE like '" + mtr_code + "%' or material_code like '" + mtr_code + "%')";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        GridView1.DataSource = data;
        GridView1.DataBind();

    }



    protected void btnGridDel1_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;
        string code = GridView1.DataKeys[rowindex].Value.ToString();
        string delSQL = "delete from HT_PUB_MATERIEL where MATERIAL_CODE= '" + code + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string log_message = opt.UpDateOra(delSQL) == "Success" ? "删除物料信息成功" : "删除物料信息失败";
        log_message += "--标识:" + code;
        InsertTlog(log_message);
        bindGrid(txtCode1.Text);//重新绑定
    }

    protected void btnSearch1_Click(object sender, EventArgs e)
    {
        bindGrid(txtCode1.Text);
    }
    protected void btnUpdate1_Click(object sender, EventArgs e)
    {
        bindData(hdcode.Value);
    }

    protected void btnDetail1_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        string code = GridView1.DataKeys[row.RowIndex].Value.ToString();
        bindData2(code);
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "viewDetail", " $('.shade').fadeIn(100);", true);

    }
   
    #endregion


    #region tab2
    protected void bindData2(string mtr_code)
    {
        string query = "select * from HT_PUB_MATERIEL where  MATERIAL_CODE = '" + mtr_code + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            txtCode2.Text = mtr_code;
            txtName2.Text = data.Tables[0].Rows[0]["MATERIAL_NAME"].ToString();
            listType2.SelectedValue = data.Tables[0].Rows[0]["TYPE_CODE"].ToString();
            txtCtgr2.Text = data.Tables[0].Rows[0]["MAT_TYPE"].ToString();
            txtUint2.Text = data.Tables[0].Rows[0]["UNIT_CODE"].ToString();
            txtPkmtr2.Text = data.Tables[0].Rows[0]["PK_MATERIAL"].ToString();
            txtLevel2.Text = data.Tables[0].Rows[0]["MAT_LEVEL"].ToString();
            txtVrt2.Text = data.Tables[0].Rows[0]["MAT_VARIETY"].ToString();
            txtWeight2.Text = data.Tables[0].Rows[0]["PIECE_WEIGHT"].ToString();
        }
    }


    protected void btnAdd2_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        if (txtCode1.Text == ""||Convert.ToInt16( opt.GetSegValue("select count(*) as num from ht_pub_mattree where parent_code = '" + txtCode1.Text + "'","num")) > 0)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel3, this.Page.GetType(), "alert", "alert('请选择正确的物料分类！！！');", true);
            return;
        }
        string code = opt.GetSegValue("select nvl(max(substr(material_code,length(material_code)-1,2)),0) as code from ht_pub_materiel  where type_code = '" + txtCode1.Text + "'", "code");
        code = (Convert.ToInt16(code) + 1).ToString().PadLeft(2, '0');
        txtCode2.Text = txtCode1.Text + code;
;
        txtName2.Text = "";
        listType2.SelectedValue = txtCode1.Text;
        txtCtgr2.Text = "";
        txtUint2.Text = "";
        txtPkmtr2.Text = "";
        txtLevel2.Text = "";
        txtVrt2.Text = "";
        txtWeight2.Text = "";
        ScriptManager.RegisterStartupScript(UpdatePanel3, this.Page.GetType(), "viewDetail", " $('.shade').fadeIn(100);", true);
        

    }

    protected void btnModify2_Click(object sender, EventArgs e)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string catgory = opt.GetSegValue("select MATTREE_NAME  from ht_pub_mattree t where MATTREE_CODE = '" + listType2.SelectedValue.Substring(0, 2) + "'", "MATTREE_NAME");
        catgory = (catgory == "NoRecord") ? "" : catgory;
        string[] seg = { "MATERIAL_CODE", "MATERIAL_NAME", "MAT_TYPE", "Type_code", "UNIT_CODE", "PK_MATERIAL", " MAT_LEVEL", "MAT_VARIETY", "PIECE_WEIGHT", " LAST_UPDATE_TIME", "REMARK" ,"MAT_CATEGORY"};
        string[] value = { txtCode2.Text, txtName2.Text, txtCtgr2.Text, listType2.SelectedValue, txtUint2.Text, txtPkmtr2.Text, txtLevel2.Text, txtVrt2.Text, txtWeight2.Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), txtDscrp2.Text ,catgory};
        
        string log_message = opt.MergeInto(seg, value, 1, "HT_PUB_MATERIEL") == "Success" ? "物料添加成功" : "物料添加失败";
        log_message += ",物料信息:" + string.Join(",", value);
        InsertTlog(log_message);
        bindGrid(txtCode1.Text);    
        ScriptManager.RegisterStartupScript(UpdatePanel3, this.Page.GetType(), "viewDetail", " $('.shade').fadeOut(100);", true);
    }
    protected void btnDel2_Click(object sender, EventArgs e)
    {
        string query = "update  HT_PUB_MATERIEL set IS_DEL = '1' where  MATERIAL_CODE = '" + txtCode2.Text + "'";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string log_message = opt.UpDateOra(query) == "Success" ? "物料删除成功" : "物料删除失败";
        log_message += ",物料编码：" + txtCode2.Text;
        InsertTlog(log_message);


    }
  
    #endregion
}