using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Craft_MateriaDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           DataBaseOperator opt =new DataBaseOperator();
            opt.bindDropDownList(listType, "select  MATTREE_CODE ,  MATTREE_NAME ,  PARENT_ID    from HT_PUB_MATTREE where  is_del = '0'", "MATTREE_NAME", "MATTREE_CODE");
            try
            {
              
                string mtr_code = Request["mtr_code"].ToString();
                if (mtr_code != "")
                {
                    bindData(mtr_code);
                }
            }
            catch
            { }
        }
    }
    protected void bindData(string mtr_code)
    {
        string query = "select * from HT_PUB_MATERIEL where  MATERIAL_CODE = '" + mtr_code + "'";
       DataBaseOperator opt =new DataBaseOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            txtCode.Text = mtr_code;
            txtName.Text = data.Tables[0].Rows[0]["MATERIAL_NAME"].ToString();
            listType.SelectedValue = data.Tables[0].Rows[0]["MAT_CATEGORY"].ToString();
            txtCtgr.Text = data.Tables[0].Rows[0]["MAT_TYPE"].ToString();
            txtUint.Text = data.Tables[0].Rows[0]["UNIT_CODE"].ToString();
            txtPkmtr.Text = data.Tables[0].Rows[0]["PK_MATERIAL"].ToString();
            txtLevel.Text = data.Tables[0].Rows[0]["MAT_LEVEL"].ToString();
            txtVrt.Text = data.Tables[0].Rows[0]["MAT_VARIETY"].ToString();
            txtWeight.Text = data.Tables[0].Rows[0]["PIECE_WEIGHT"].ToString();
        }
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
       string[] seg = {"MATERIAL_CODE","MATERIAL_NAME","MAT_TYPE","MAT_CATEGORY","UNIT_CODE","PK_MATERIAL"," MAT_LEVEL"," MAT_VARIETY","PIECE_WEIGHT"," LAST_UPDATE_TIME","REMARK"};
       string[] value = { txtCode.Text, txtName.Text, txtCtgr.Text, listType.SelectedValue, txtUint.Text, txtPkmtr.Text, txtLevel.Text, txtVrt.Text, txtWeight.Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), txtDscrp.Text };
       DataBaseOperator opt =new DataBaseOperator();
        string log_message = opt.InsertData(seg, value, "HT_PUB_MATERIEL")=="Success" ? "物料添加成功":"物料添加失败";
        log_message += ",物料信息:" + string.Join(" ", value);
        opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);
        
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        string[] seg = {  "MATERIAL_NAME", "MAT_TYPE", "MAT_CATEGORY", "UNIT_CODE", "PK_MATERIAL", " MAT_LEVEL", " MAT_VARIETY", "PIECE_WEIGHT", " LAST_UPDATE_TIME", "REMARK" };
        string[] value = { txtName.Text, txtCtgr.Text, listType.SelectedValue, txtUint.Text, txtPkmtr.Text, txtLevel.Text, txtVrt.Text, txtWeight.Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), txtDscrp.Text };
        string condition = " where MATERIAL_CODE = '" + txtCode.Text + "'";
       DataBaseOperator opt =new DataBaseOperator();
        string log_message = opt.UpDateData(seg, value, "HT_PUB_MATERIEL", condition) =="Success" ? "物料修改成功":"物料修改失败";
        log_message += ", 物料信息:" + string.Join(" ", value);
        opt.InsertTlog(Session["UserNmae"].ToString(), Page.Request.UserHostName.ToString(), log_message);
        
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        string query = "update  HT_PUB_MATERIEL set IS_DEL = '1' where  MATERIAL_CODE = '" + txtCode.Text + "'";
       DataBaseOperator opt =new DataBaseOperator();
        string log_message = opt.UpDateOra(query)== "Success" ? "物料删除成功":"物料删除失败";
        log_message += ",物料编码：" + txtCode.Text;
        opt.InsertTlog(Session["UserName"].ToString(), Page.Request.UserHostName.ToString(), log_message);

        
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        bindData(hdcode.Value);

    }
}