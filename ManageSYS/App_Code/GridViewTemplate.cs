using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
///GridViewTemplate 的摘要说明
/// </summary>
public class GridViewTemplate : ITemplate
{
    /// <summary>
    /// 模板类型：标题或内容；
    /// DataControlRowType.Header和DataControlRowType.DataRow
    /// </summary>
    private DataControlRowType P_TemplateType;
    /// <summary>
    /// 列的名称：列标题时，为列显示名称；列内容时，为列的字段名称
    /// </summary>
    private string P_ColumnName;
    /// <summary>
    /// 列的类型：TextBox、DropDownList等
    /// </summary>
    private string P_ColumnType;

    public GridViewTemplate()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 构造函数：动态添加模版列
    /// </summary>
    /// <param name="TemplateType">模板类型：标题或内容；DataControlRowType.Header和DataControlRowType.DataRow</param>
    /// <param name="ColumnName">列的名称：列标题时，为列显示名称；列内容时，为列的字段名称</param>
    /// <param name="ColumnType">列的类型：列标题时，可为空；列内容时，为模板列的控件类型</param>
    public GridViewTemplate(DataControlRowType TemplateType, string ColumnName, string ColumnType)
    {
        P_TemplateType = TemplateType;
        P_ColumnName = ColumnName;
        P_ColumnType = ColumnType;
    }
    public void InstantiateIn(System.Web.UI.Control container)
    {
        switch (P_TemplateType)
        {
            case DataControlRowType.Header://列标题
                Literal lc = new Literal();
                lc.Text = P_ColumnName;
                container.Controls.Add(lc);
                break;
            case DataControlRowType.DataRow://模版列内容

                if (P_ColumnType.ToUpper() == "TextBox".ToUpper())
                {
                    TextBox tb = new TextBox();
                    tb.ID = "txt_" + P_ColumnName;
                    tb.EnableViewState = true;
                   // tb.AutoPostBack = true;                   
                    //tb.Text = "";
                   // tb.DataBinding += new EventHandler(tb_DataBinding);
                    tb.CssClass = "tbinput";
                    tb.Width = Unit.Parse("200px");
                    container.Controls.Add(tb);
                }
                else if (P_ColumnType.ToUpper() == "Label".ToUpper())
                {
                    System.Web.UI.WebControls.Label lb = new Label();
                    lb.ID = "lbl_" + P_ColumnName;
                    lb.EnableViewState = true;
                    //tb.Text = "";
                   // lb.DataBinding += new EventHandler(lb_DataBinding);
                    container.Controls.Add(lb);
                }
                else if (P_ColumnType.ToUpper() == "CheckBox".ToUpper())
                {
                    System.Web.UI.WebControls.CheckBox ck = new CheckBox();
                    ck.ID = "ck_" + P_ColumnName;
                    ck.EnableViewState = true;
                    //tb.Text = "";
                   // ck.DataBinding += new EventHandler(ck_DataBinding);
                    container.Controls.Add(ck);
                }
                else if (P_ColumnType.ToUpper() == "Button".ToUpper())
                {
                    System.Web.UI.WebControls.Button btn = new Button();
                    btn.ID = "btn_" + P_ColumnName;
                    btn.EnableViewState = true;
                    btn.CssClass = "btn1  auth";
                    btn.Text = "保存";
                    btn.Click += new EventHandler(btn_Click);
                    //tb.Text = "";
                    //ck.DataBinding += new EventHandler(lb_DataBinding);
                    container.Controls.Add(btn);
                }
                else
                { //默认为TextBox
                    TextBox tb = new TextBox();
                    tb.ID = "txt_" + P_ColumnName;
                   // tb.AutoPostBack = true;
                    tb.EnableViewState = true;
                    tb.CssClass = "tbinput";
                    tb.Width = Unit.Parse("150px") ;
                   // tb.DataBinding += new EventHandler(tb_DataBinding);
                    container.Controls.Add(tb);
                }



                break;
            default:
                break;
        }
    }
    void btn_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;           
           
            GridView gv = (GridView)btn.NamingContainer.DataKeysContainer;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
             int index = gvr.RowIndex;
            string sectioncode = gv.DataKeys[index].Values[0].ToString();
            string opathcode = gv.DataKeys[index].Values[1].ToString();
            string[] seg = {"SECTION_CODE",  "PATHCODE",  "PATHNAME",  "CREATE_TIME"};
            string pathcode = "";
            for (int i = 2; i < gv.Columns.Count - 1; i++)
            {
                pathcode += Convert.ToInt16(((CheckBox)gvr.FindControl("ck_" + (i - 1).ToString())).Checked).ToString();
            }
            string[] value = {sectioncode,pathcode, ((TextBox)gvr.FindControl("txt_Pathname")).Text,System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")};
           DataBaseOperator opt =new DataBaseOperator();
            opt.UpDateOra("delete from HT_PUB_PATH_SECTION where SECTION_CODE = '" + sectioncode + "' and PATHCODE = '" + opathcode + "'"); 
            opt.InsertData(seg, value, "HT_PUB_PATH_SECTION");
            opt.InsertTlog("", "", "");
           
           
        }
        catch (Exception ee)
        {
        }
       
    }
   /*  void ck_DataBinding(object sender, EventArgs e)
    {
        CheckBox ck = (CheckBox)sender;
        GridViewRow container = (GridViewRow)ck.NamingContainer;
        object dataValue = DataBinder.Eval(container.DataItem, P_ColumnName);
        if (dataValue != DBNull.Value)
        {
           ck.Checked  = Convert.ToBoolean( Convert.ToInt16(dataValue.ToString()));
        }
    }
   void tb_DataBinding(object sender, EventArgs e)
    {
        TextBox txtdata = (TextBox)sender;
        GridViewRow container = (GridViewRow)txtdata.NamingContainer;
        object dataValue = DataBinder.Eval(container.DataItem, P_ColumnName);
        if (dataValue != DBNull.Value)
        {
            txtdata.Text = dataValue.ToString();
        }
    }
    void lb_DataBinding(object sender, EventArgs e)
    {
        Label lbldata = (Label)sender;
        GridViewRow container = (GridViewRow)lbldata.NamingContainer;
        object dataValue = DataBinder.Eval(container.DataItem, P_ColumnName);
        if (dataValue != DBNull.Value)
        {
            lbldata.Text = dataValue.ToString();
        }
    }*/
}