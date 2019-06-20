using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public partial class Quality_Inspect_Process : MSYS.Web.BasePage
{

    public class GenericItem : ITemplate
    {

        public string column;
        public string ID;

        public GenericItem(string column, string ID)
        {

            this.column = column;
            this.ID = ID;

        }

        public void InstantiateIn(Control container)
        {

            //TextBox txt = new TextBox();

            //txt.Width = Unit.Pixel(15);

            //txt.DataBinding += new EventHandler(this.BindData);
            //DropDownList ddl = new DropDownList();
            //ddl.DataBinding += new EventHandler(this.BindData);
            TextBox tb = new TextBox();
            tb.DataBinding += new EventHandler(this.BindData);
            tb.ID = this.ID;
            tb.CssClass = "tbinput1";
            //container.Controls.Add(ddl);
            container.Controls.Add(tb);
            HiddenField HF = new HiddenField();
            HF.Value = column;

            container.Controls.Add(HF);

        }

        public void BindData(object sender, EventArgs e)
        {

            //TextBox txt = (TextBox)sender;
            //txt.Text = "▲";            
            //DropDownList ddl = (DropDownList)sender;
            //ddl.Items.Add(new ListItem("○"));
            //ddl.Items.Add(new ListItem("▲"));
            //ddl.Items.Add(new ListItem("●"));
            TextBox tb = (TextBox)sender;
            //tb.Text = "zyd";
            tb.CssClass = "tbinput1";
            tb.ID = this.ID; 

        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            initView();
            //bindGridView2();
           //bindView2();
        }

    }
    protected void initView()
    {

       MSYS.Data.SysUser user =  (MSYS.Data.SysUser)Session["User"];
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(prod_code, "select prod_code,prod_name from ht_pub_prod_design where is_del= '0'", "prod_name", "prod_code");
        opt.bindDropDownList(DropDownList1, "select prod_code,prod_name from ht_pub_prod_design where is_del= '0'", "prod_name", "prod_code");
        opt.bindDropDownList(listProd, "select prod_code,prod_name from ht_pub_prod_design where is_del= '0'", "prod_name", "prod_code");
        opt.bindDropDownList(listTeam, "select team_code,team_name from ht_sys_team where is_del = '0' order by team_code", "team_name", "team_code");
        opt.bindDropDownList(listShift, "select shift_code,shift_name from ht_sys_shift where is_del = '0'", "shift_name", "shift_code");
        listTeam.SelectedValue = opt.GetSegValue(" select team_code from ht_prod_schedule where date_begin < '" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' and date_end > '" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "team_code");
        listShift.SelectedValue = opt.GetSegValue(" select Shift_code from ht_prod_schedule where date_begin < '" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' and date_end > '" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "Shift_code");
        listEditor.Items.Clear();
        ListItem item = new ListItem(user.text, user.id);
        listEditor.Items.Add(item);
        listEditor.SelectedValue = user.id;
        txtProdTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd");

        string query = "select r.inspect_code,s.name as insgroup,r.inspect_name,0 as value,t.lower_value,t.upper_value,r.unit,'' as status,t.minus_score from ht_qlt_inspect_proj r left join ht_inner_inspect_group s on s.id = r.inspect_group left join ht_qlt_inspect_stdd t on t.inspect_code = r.inspect_code and t.is_del = '0' where r.inspect_group in('1','2','3') and r.is_del = '0' order by r.inspect_group";
       
        DataSet data = opt.CreateDataSetOra(query);
        bindGrid1(data);
        
    }

   public string[] getSTDD() { 
            
           string sql = "select r.inspect_code,s.name as insgroup,r.inspect_name,0 as value,t.lower_value,t.upper_value,r.unit,'' as status,t.minus_score from ht_qlt_inspect_proj r left join ht_inner_inspect_group s on s.id = r.inspect_group left join ht_qlt_inspect_stdd t on t.inspect_code = r.inspect_code and t.is_del = '0' where r.inspect_group in('1','2','3') and r.is_del = '0' order by r.inspect_group";
           MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
           DataSet data = opt.CreateDataSetOra(sql);
           string[] title=new string[data.Tables[0].Rows.Count];
           for (int i = 0; i < data.Tables[0].Rows.Count; i++)
           {
               title[i] = data.Tables[0].Rows[i][2].ToString();
           }
           return title;
    }

    protected void bindGrid1(DataSet data)
    {
            GridView1.DataSource = data;
            GridView1.DataBind();

            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                {
                    DataRowView mydrv = data.Tables[0].DefaultView[i];

                    ((TextBox)GridView1.Rows[i].FindControl("txtPara")).Text = mydrv["value"].ToString();
                    ((TextBox)GridView1.Rows[i].FindControl("txtValue")).Text = mydrv["lower_value"].ToString() + "~" + mydrv["upper_value"].ToString();
                    ((Label)GridView1.Rows[i].FindControl("labStatus")).Text = mydrv["status"].ToString();
                    if (mydrv["status"].ToString() == "超限")
                    {
                        ((TextBox)GridView1.Rows[i].FindControl("txtScore")).Text = mydrv["minus_score"].ToString();
                        ((Label)GridView1.Rows[i].FindControl("labStatus")).CssClass = "labstatu";
                    }
                    else
                    {
                        ((TextBox)GridView1.Rows[i].FindControl("txtScore")).Text = "0";
                         ((Label)GridView1.Rows[i].FindControl("labStatus")).CssClass = "labstatuGreen";
                    }
                }
            }
   
    }
   
    protected void listProd_SelectedIndexChanged(object sender,EventArgs e)
    {
        string query = "select r.inspect_code, h.name as insgroup,r.inspect_name,nvl(t.inspect_value,0) as value,s.lower_value,s.upper_value,r.unit,case when t.inspect_value is null then '' when t.inspect_value > s.lower_value and t.inspect_value <s.upper_value then '合格' else '超限' end as status,s.minus_score  from ht_qlt_inspect_proj r left join ht_qlt_inspect_stdd s on s.inspect_code = r.inspect_code and s.is_del = '0' left join ht_qlt_inspect_record t on t.inspect_code = r.inspect_code left join ht_inner_inspect_group h on h.id = r.inspect_group where r.inspect_type = '1' and r.is_del = '0' and r.inspect_group in('1','2','3') and t.prod_code = '" + listProd.SelectedValue + "' and t.team_id = '" + listTeam.SelectedValue + "' and t.record_time = '" + txtProdTime.Text + "' union select r.inspect_code,s.name as insgroup,r.inspect_name,0 as value,t.lower_value,t.upper_value,r.unit,'' as status,t.minus_score from ht_qlt_inspect_proj r left join ht_inner_inspect_group s on s.id = r.inspect_group left join ht_qlt_inspect_stdd t on t.inspect_code = r.inspect_code and t.is_del = '0' where r.inspect_group in('1','2','3')  and r.is_del = '0' and r.inspect_code in (select inspect_code from ht_qlt_inspect_proj where inspect_group in('1','2','3') minus select inspect_code from ht_qlt_inspect_record where Prod_code = '" + listProd.SelectedValue + "' and team_id = '" + listTeam.SelectedValue + "' and record_time = '" + txtProdTime.Text + "')";
       MSYS.DAL.DbOperator opt =new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
            bindGrid1(data);
      
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {

            string[] seg = { "INSPECT_CODE", "PROD_CODE","TEAM_ID", "RECORD_TIME",  "SHIFT_ID", "INSPECT_VALUE", "CREAT_ID", "CREATE_TIME" };
            string[] value = {GridView1.DataKeys[row.RowIndex].Value.ToString(), listProd.SelectedValue,  listTeam.SelectedValue, txtProdTime.Text,listShift.SelectedValue, ((TextBox)row.FindControl("txtPara")).Text, listEditor.SelectedValue, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
           
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

            string log_message = opt.MergeInto(seg, value, 4, "HT_QLT_INSPECT_RECORD") == "Success" ? "保存理化检测结果成功" : "保存理化检测结果失败";
            log_message += "--详情:" + string.Join(",", value);
            InsertTlog(log_message);
        }

        //bindGridView2();
       
    }

    protected void createView() {
        StringBuilder query = new StringBuilder();
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        StringBuilder qu = new StringBuilder();
        qu.Append(createSql2(true, prod_code.Text, start_time.Text, end_time.Text, team_code.SelectedValue, schedule_time.SelectedValue));
        System.Diagnostics.Debug.WriteLine("sql语句" + qu.ToString());
        DataSet data = opt.CreateDataSetOra(qu.ToString());
        DataTable ta = new DataTable();
        /*ta.Columns.Add(new DataColumn("record_time_2"));
        ta.Columns.Add(new DataColumn("product_2"));
        ta.Columns.Add(new DataColumn("team_2"));
        ta.Columns.Add(new DataColumn("smell_2"));
        ta.Columns.Add(new DataColumn("adjoin_2"));
        ta.Columns.Add(new DataColumn("white_2"));
        ta.Columns.Add(new DataColumn("water_2"));
        ta.Columns.Add(new DataColumn("input_2"));
        ta.Columns.Add(new DataColumn("sugar_2"));
        ta.Columns.Add(new DataColumn("soda_2"));
        ta.Columns.Add(new DataColumn("ntri_2"));
        ta.Columns.Add(new DataColumn("score_2"));*/
        for (int i = 0; i < data.Tables[0].Rows.Count; i++)
        {
            DataRow mydrv = data.Tables[0].Rows[i];
            DataRow sourceRow = ta.NewRow();
            /*sourceRow["记录时间"] = mydrv[0].ToString();
            sourceRow["产品"] = mydrv[1].ToString();
            sourceRow["班组"] = mydrv["team_2"].ToString();
            sourceRow["班时"] = mydrv["班时"].ToString();
            sourceRow["adjoin_2"] = mydrv["连片粘连率"].ToString();
            sourceRow["white_2"] = mydrv["白片率"].ToString();
            sourceRow["water_2"] = mydrv["水分"].ToString();
            sourceRow["input_2"] = mydrv["填充值"].ToString();
            sourceRow["sugar_2"] = mydrv["水溶性糖"].ToString();
            sourceRow["soda_2"] = mydrv["总植物碱"].ToString();
            sourceRow["ntri_2"] = mydrv["总氮"].ToString();
            sourceRow["score_2"] = mydrv["得分"].ToString();*/
            ta.Rows.Add(sourceRow);
        }

        /*string[] title = this.getSTDD();
        for (int i = 0; i < title.Length; i++)
        {
            TemplateField templateField = new TemplateField();
            templateField.HeaderText = title[i];
            templateField.ItemTemplate = new GenericItem("", title[i]);
            ViewState[title[i]] = true;
            GridView2.Columns.Add(templateField);
        }*/
        GridView2.DataSource = ta;
        GridView2.DataBind();
    }

    protected void bindView2() { 
         MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();       
        StringBuilder qu = new StringBuilder();
         qu.Append(createSql2(true, prod_code.Text, start_time.Text, end_time.Text, team_code.SelectedValue, schedule_time.SelectedValue));
         DataSet data = opt.CreateDataSetOra(qu.ToString());
         string[] title = this.getSTDD();
         if (data != null && data.Tables[0].Rows.Count > 0)
         {
             for (int i = 0; i < data.Tables[0].Rows.Count; i++)
             {
                 //DataRowView mydrv = data.Tables[0].DefaultView[i];
                 DataRow mydrv = data.Tables[0].Rows[i];
                 //((TextBox)GridView2.Rows[i].FindControl("product_2")).Text = data.Tables[0].Rows[i][0].ToString();
                 ((TextBox)GridView2.Rows[i].FindControl("record_time_2")).Text = mydrv[0].ToString();
                 ((TextBox)GridView2.Rows[i].FindControl("product_2")).Text = mydrv[1].ToString();
                 ((TextBox)GridView2.Rows[i].FindControl("team_2")).Text = mydrv["team_2"].ToString();
                 ((TextBox)GridView2.Rows[i].FindControl("team_time_2")).Text = mydrv["班时"].ToString();
                 ((TextBox)GridView2.Rows[i].FindControl("smell_2")).Text = mydrv["霉变异味"].ToString();
                 ((TextBox)GridView2.Rows[i].FindControl("adjoin_2")).Text = mydrv["连片粘连率"].ToString();
                 ((TextBox)GridView2.Rows[i].FindControl("white_2")).Text = mydrv["白片率"].ToString();
                 ((TextBox)GridView2.Rows[i].FindControl("water_2")).Text = mydrv["水分"].ToString();
                 ((TextBox)GridView2.Rows[i].FindControl("input_2")).Text = mydrv["填充值"].ToString();
                 ((TextBox)GridView2.Rows[i].FindControl("sugar_2")).Text = mydrv["水溶性糖"].ToString();
                 ((TextBox)GridView2.Rows[i].FindControl("soda_2")).Text = mydrv["总植物碱"].ToString();
                 ((TextBox)GridView2.Rows[i].FindControl("ntri_2")).Text = mydrv["总氮"].ToString();
                 //((TextBox)GridView2.Rows[i].FindControl("score_2")).Text = mydrv["得分"].ToString();
                 /*for (int p = 0; p < title.Length; p++)
                 {
                     //System.Diagnostics.Debug.WriteLine(title[p]);
                     ((TextBox)GridView2.Rows[i].FindControl(title[p])).Text = mydrv[title[p]].ToString();
                 }*/
             }
         }

    }

    protected void bindGridView2() {
        StringBuilder query = new StringBuilder();
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        StringBuilder qu = new StringBuilder();
        qu.Append(createSql2(true, prod_code.Text, start_time.Text, end_time.Text, team_code.SelectedValue, schedule_time.SelectedValue));
        System.Diagnostics.Debug.WriteLine("sql语句" + qu.ToString());
        DataSet data = opt.CreateDataSetOra(qu.ToString());
        DataTable ta = new DataTable();
        /*ta.Columns.Add(new DataColumn("record_time_2"));
        ta.Columns.Add(new DataColumn("product_2"));
        ta.Columns.Add(new DataColumn("team_2"));
        ta.Columns.Add(new DataColumn("smell_2"));
        ta.Columns.Add(new DataColumn("adjoin_2"));
        ta.Columns.Add(new DataColumn("white_2"));
        ta.Columns.Add(new DataColumn("water_2"));
        ta.Columns.Add(new DataColumn("input_2"));
        ta.Columns.Add(new DataColumn("sugar_2"));
        ta.Columns.Add(new DataColumn("soda_2"));
        ta.Columns.Add(new DataColumn("ntri_2"));
        ta.Columns.Add(new DataColumn("score_2"));*/
        for (int i = 0; i < data.Tables[0].Rows.Count; i++) {
            DataRow mydrv = data.Tables[0].Rows[i];
            DataRow sourceRow = ta.NewRow();
            /*sourceRow["记录时间"] = mydrv[0].ToString();
            sourceRow["产品"] = mydrv[1].ToString();
            sourceRow["班组"] = mydrv["team_2"].ToString();
            sourceRow["班时"] = mydrv["班时"].ToString();
            sourceRow["adjoin_2"] = mydrv["连片粘连率"].ToString();
            sourceRow["white_2"] = mydrv["白片率"].ToString();
            sourceRow["water_2"] = mydrv["水分"].ToString();
            sourceRow["input_2"] = mydrv["填充值"].ToString();
            sourceRow["sugar_2"] = mydrv["水溶性糖"].ToString();
            sourceRow["soda_2"] = mydrv["总植物碱"].ToString();
            sourceRow["ntri_2"] = mydrv["总氮"].ToString();
            sourceRow["score_2"] = mydrv["得分"].ToString();*/
            ta.Rows.Add(sourceRow);
        }

        string[] title = this.getSTDD();
        /*for (int i = 0; i < title.Length; i++)
        {
            TemplateField templateField = new TemplateField();
            templateField.HeaderText = title[i];
            templateField.ItemTemplate = new GenericItem("", title[i]);
            ViewState[title[i]] = true;
            GridView2.Columns.Add(templateField);
        }*/
        /*TemplateField templateField2 = new TemplateField();
        templateField2.HeaderText = "标题人名";
        templateField2.ItemTemplate = new GenericItem("","zyd");
        
        GridView2.Columns.Add(templateField2);
        TemplateField templateField2 = new TemplateField();
        templateField2.HeaderText = "标题人名2";
        templateField2.ItemTemplate = new GenericItem("","fyr");
        GridView2.Columns.Add(templateField2);*/
            GridView2.DataSource = ta;
            GridView2.DataBind();
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                {
                    //DataRowView mydrv = data.Tables[0].DefaultView[i];
                    DataRow mydrv = data.Tables[0].Rows[i];
                    //((TextBox)GridView2.Rows[i].FindControl("product_2")).Text = data.Tables[0].Rows[i][0].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("record_time_2")).Text = mydrv[0].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("product_2")).Text = mydrv[1].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("team_2")).Text = mydrv["team_2"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("team_time_2")).Text = mydrv["班时"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("smell_2")).Text = mydrv["霉变异味"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("adjoin_2")).Text = mydrv["连片粘连率"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("white_2")).Text = mydrv["白片率"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("water_2")).Text = mydrv["水分"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("input_2")).Text = mydrv["填充值"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("sugar_2")).Text = mydrv["水溶性糖"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("soda_2")).Text = mydrv["总植物碱"].ToString();
                    ((TextBox)GridView2.Rows[i].FindControl("ntri_2")).Text = mydrv["总氮"].ToString();
                    //((TextBox)GridView2.Rows[i].FindControl("score_2")).Text = mydrv["得分"].ToString();
                    /*for (int p = 0; p < title.Length; p++)
                    {
                        //System.Diagnostics.Debug.WriteLine(title[p]);
                        ((TextBox)GridView2.Rows[i].FindControl(title[p])).Text = mydrv[title[p]].ToString();
                    }*/
                }

            }
            //System.Diagnostics.Debug.WriteLine(((TextBox)GridView2.Rows[0].FindControl("霉变异味")).Text);
            
   
    }

    protected void normalizedData()
    {
        string query = "";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(query);
        List<Dictionary<string, string>> nor_data = new List<Dictionary<string, string>>();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            Dictionary<string, string> temp = new Dictionary<string, string>();
            temp.Add("time", data.Tables[0].Rows[0][4].ToString());
            temp.Add("team", data.Tables[0].Rows[0][2].ToString());
            temp.Add(data.Tables[0].Rows[0][5].ToString(), data.Tables[0].Rows[0][6].ToString());
            for (int i = 1; i < data.Tables[0].Rows.Count; i++)
            {
                if (data.Tables[0].Rows[i][4].ToString() == temp["time"] && data.Tables[0].Rows[i][2].ToString() == temp["team"])
                {
                    temp.Add(data.Tables[0].Rows[i][5].ToString(), data.Tables[0].Rows[i][6].ToString());
                }
                else {
                    nor_data.Add(temp);
                    temp = new Dictionary<string, string>();
                    temp.Add("time", data.Tables[0].Rows[i][4].ToString());
                    temp.Add("team", data.Tables[0].Rows[i][2].ToString());
                    temp.Add(data.Tables[0].Rows[i][5].ToString(), data.Tables[0].Rows[i][6].ToString());
                }
            }
        }
    }

    protected void btn_Search(object sender, EventArgs e)
    {
        bindGridView2();
        //createView();
        //bindView2();
    }


    protected void btnRowSave_Click(object sender, EventArgs e) 
    {
        bindGridView2();
        //System.Diagnostics.Debug.WriteLine((GridView2.Rows[0].TemplateControl.FindControl("product_2") as TextBox).Text);
        System.Diagnostics.Debug.WriteLine(((TextBox)GridView2.Rows[0].FindControl("霉变异味")).Text);
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        Button btn = (Button)sender;

        //string[] text = { "smell_2", "adjoin_2", "white_2", "water_2", "input_2", "sugar_2", "soda_2", "ntri_2" };
        string[] text = this.getSTDD();
        int loop = text.Length;
        int Rowindex = ((GridViewRow)btn.NamingContainer).RowIndex;//获得行号
        string sql = "select prod_code from ht_pub_prod_design where is_del=0 and prod_name='"+((TextBox)GridView2.Rows[Rowindex].FindControl("product_2")).Text+"'";
        DataSet data =  opt.CreateDataSetOra(sql);
        string prod_code = data.Tables[0].Rows[0][0].ToString();
        string teamid = "01";
        string team=((TextBox)GridView2.Rows[Rowindex].FindControl("product_2")).Text;
        if (team == "乙班") {
            teamid = "02";
        }
        if (team == "丙班")
        {
            teamid = "03";
        }
        //((TextBox)GridView2.Rows[Rowindex].FindControl("txtNodeName")).Text
        //string[] seg = { "SECTION_CODE", "NODENAME", "ORDERS", "DESCRIPT", "CREATE_TIME", "TAG" };
        //string[] value = { listSection2.SelectedValue, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtNodeName")).Text, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtOrder")).Text, ((TextBox)GridView2.Rows[Rowindex].FindControl("txtDscrpt")).Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ((TextBox)GridView2.Rows[Rowindex].FindControl("txtTag")).Text };
        for (int i = 0; i < loop; i++) {
            string[] seg = { "INSPECT_VALUE" };
            string[] value = { ((TextBox)GridView2.Rows[Rowindex].FindControl(text[i])).Text };
            //string[] arg = {"703100001001","703100001002","703100001003","703100002001","703070305007","703100003001","703100003002","703100003003"};
            string[] arg = this.getSTDDCode();
            string log_message = opt.UpDateData(seg, value, "HT_QLT_INSPECT_RECORD", " where RECORD_TIME = '" + ((TextBox)GridView2.Rows[Rowindex].FindControl("record_time_2")).Text + "' and INSPECT_CODE='"+arg[i]+"' and prod_code = '"+prod_code+"' and is_del=0 and team_id='"+teamid+"'") == "Success" ? "更新路径节点成功" : "更新路径节点失败";
            log_message += "--详情:" + string.Join(",", value);
            InsertTlog(log_message);
        }
        
    }

    public string[] getSTDDCode() {
        string sql = "select r.inspect_code,s.name as insgroup,r.inspect_name,0 as value,t.lower_value,t.upper_value,r.unit,'' as status,t.minus_score from ht_qlt_inspect_proj r left join ht_inner_inspect_group s on s.id = r.inspect_group left join ht_qlt_inspect_stdd t on t.inspect_code = r.inspect_code and t.is_del = '0' where r.inspect_group in('1','2','3') and r.is_del = '0' order by r.inspect_group";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(sql);
        string[] title = new string[data.Tables[0].Rows.Count];
        for (int i = 0; i < data.Tables[0].Rows.Count; i++)
        {
            title[i] = data.Tables[0].Rows[i][0].ToString();
        }
        return title;
    }

    private string createSql2(bool Isteamgroup, string prodCode, string startTime, string endTime, string teamcode, string timecode)
    {
        StringBuilder sql = new StringBuilder();
        StringBuilder str = new StringBuilder();
        StringBuilder temp = new StringBuilder();
        //sql.Append("select g1.record_time as 记录时间,  p.prod_name as 产品 ");
        sql.Append("select g1.record_time as record_time_2, p.prod_name as product_2");

        //班组可选
        if (Isteamgroup)
            // sql.Append(",t.team_name as 班组, case when g1.shift_id='01' then '早班' when g1.shift_id='02' then '中班' when g1.shift_id='03' then '晚班' end as 班时 ");
            sql.Append(", t.team_name as team_2, case when g1.shift_id='01' then '早班' when g1.shift_id='02' then '中班' when g1.shift_id ='03' then '晚班' end as 班时");
        else
            sql.Append(",'' as 班组, case  when g1.shift_id='01' then '早班' when g1.shift_id='02' then '中班' when g1.shift_id='03' then '晚班' end as 班时 ");

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select inspect_code,inspect_name from ht_qlt_inspect_proj  where inspect_group in('1','2','3')  and is_del = '0' order by inspect_code";
        DataSet data = opt.CreateDataSetOra(query);

        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            int i = 1;
            temp.Append("100");

            foreach (DataRow row in data.Tables[0].Rows)
            {
                string name = row["inspect_name"].ToString();
                string code = row["inspect_code"].ToString();
                sql.Append(",round(avg(nvl(g");
                sql.Append(i.ToString());
                sql.Append(".");
                sql.Append(name);
                sql.Append(",0)),2) as ");
                sql.Append(name);


                temp.Append("-round(avg(nvl(g");
                temp.Append(i.ToString());
                temp.Append(".score,0)),2)");

                if (i > 1)
                    str.Append(" left join ");

                str.Append("(select   a.prod_code,a.team_id,a.shift_ID,a.record_time ,nvl(b.score,0) as score, nvl(a.inspect_value,0) as ");
                str.Append(name);
                str.Append("  from ht_qlt_inspect_record a left join ht_qlt_inspect_event b on b.record_id = a.id where a.inspect_code = '");
                str.Append(code);
                str.Append("' and substr(a.record_time,1,10) > '" + startTime + "' and substr(a.record_time,1,10)<'" + endTime + "'");// 添加参数
                if (i == 1)
                {
                    str.Append(" and a.prod_code='" + prodCode + "'");
                }
                if (teamcode != "00" || teamcode==null)
                {
                    str.Append(" and a.team_id='" + teamcode + "'");
                }
                if (timecode != "00" || timecode == null) {
                    str.Append(" and a.shift_id = '" + timecode + "'");                
                }
                str.Append(")g");


                str.Append(i.ToString());
                if (i > 1)
                {
                    str.Append(" on g1.prod_code = g");
                    str.Append(i.ToString());
                    str.Append(".prod_code  and g1.team_id = g");
                    str.Append(i.ToString());
                    str.Append(".team_id  and g1.shift_id = g");
                    str.Append(i.ToString());
                    str.Append(".shift_id  and g1.record_time = g");
                    str.Append(i.ToString());
                    str.Append(".record_time  ");
                }
                i++;
            }
            temp.Append(" as 得分");
            sql.Append(",");
            sql.Append(temp);
            sql.Append(" from ");
            sql.Append(str.ToString());
            sql.Append("left join ht_pub_prod_design p on p.prod_code = g1.prod_code left join ht_sys_team t on t.team_code = g1.team_id ");

            sql.Append(" group by p.prod_name");
            if (Isteamgroup)
                sql.Append(",t.team_name, g1.record_time, g1.shift_id");
            sql.Append(" order by g1.record_time, t.team_name");
            return sql.ToString();
        }
        else return null;

    }


    [WebMethod]
    public static string getTitle()
    {
        System.Diagnostics.Debug.WriteLine("getTitle 收到请求");
        var json = new JObject();
        var titles = new JArray();
        string sql = "select r.inspect_code,s.name as insgroup,r.inspect_name,0 as value,t.lower_value,t.upper_value,r.unit,'' as status,t.minus_score from ht_qlt_inspect_proj r left join ht_inner_inspect_group s on s.id = r.inspect_group left join ht_qlt_inspect_stdd t on t.inspect_code = r.inspect_code and t.is_del = '0' where r.inspect_group in('1','2','3') and r.is_del = '0' order by r.inspect_group";
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra(sql);
        //string[] title = new string[data.Tables[0].Rows.Count];
        for (int i = 0; i < data.Tables[0].Rows.Count; i++)
        {
           // title[i] = data.Tables[0].Rows[i][2].ToString();
            titles.Add(data.Tables[0].Rows[i][2].ToString());
        }
        json.Add("titles", titles);
        return json.ToString();
    }

    [WebMethod]
    public static string getRows(string start_time, string end_time, string prod_code, string team_code, string schedule_time)
    {
        System.Diagnostics.Debug.WriteLine("getRows is running");
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        var json = new JObject();
        var rows = new JArray();
        StringBuilder qu = new StringBuilder();
        qu.Append(createSql3(true, prod_code, start_time, end_time, team_code, schedule_time));
        DataSet data = opt.CreateDataSetOra(qu.ToString());
        int col = data.Tables[0].Columns.Count;
        for (int i = 0; i < data.Tables[0].Rows.Count; i++) {
            DataRow dr = data.Tables[0].Rows[i];
            var row = new JArray();
            for (int j = 0; j < col; j++)
            {
                row.Add(dr[j]);
            }
            rows.Add(row);
        }
        json.Add("rows", rows);
        System.Diagnostics.Debug.WriteLine(json.ToString());
        return json.ToString();
    }

    public static string createSql3(bool Isteamgroup, string prodCode, string startTime, string endTime, string teamcode, string timecode)
    {
        StringBuilder sql = new StringBuilder();
        StringBuilder str = new StringBuilder();
        StringBuilder temp = new StringBuilder();
        //sql.Append("select g1.record_time as 记录时间,  p.prod_name as 产品 ");
        sql.Append("select g1.record_time as record_time_2, p.prod_name as product_2");

        //班组可选
        if (Isteamgroup)
            // sql.Append(",t.team_name as 班组, case when g1.shift_id='01' then '早班' when g1.shift_id='02' then '中班' when g1.shift_id='03' then '晚班' end as 班时 ");
            sql.Append(", t.team_name as team_2, case when g1.shift_id='01' then '早班' when g1.shift_id='02' then '中班' when g1.shift_id ='03' then '晚班' end as 班时");
        else
            sql.Append(",'' as 班组, case  when g1.shift_id='01' then '早班' when g1.shift_id='02' then '中班' when g1.shift_id='03' then '晚班' end as 班时 ");

        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select inspect_code,inspect_name from ht_qlt_inspect_proj  where inspect_group in('1','2','3')  and is_del = '0' order by inspect_code";
        DataSet data = opt.CreateDataSetOra(query);

        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            int i = 1;
            temp.Append("100");

            foreach (DataRow row in data.Tables[0].Rows)
            {
                string name = row["inspect_name"].ToString();
                string code = row["inspect_code"].ToString();
                sql.Append(",round(avg(nvl(g");
                sql.Append(i.ToString());
                sql.Append(".");
                sql.Append(name);
                sql.Append(",0)),2) as ");
                sql.Append(name);


                temp.Append("-round(avg(nvl(g");
                temp.Append(i.ToString());
                temp.Append(".score,0)),2)");

                if (i > 1)
                    str.Append(" left join ");

                str.Append("(select   a.prod_code,a.team_id,a.shift_ID,a.record_time ,nvl(b.score,0) as score, nvl(a.inspect_value,0) as ");
                str.Append(name);
                str.Append("  from ht_qlt_inspect_record a left join ht_qlt_inspect_event b on b.record_id = a.id where a.inspect_code = '");
                str.Append(code);
                str.Append("' and substr(a.record_time,1,10) > '" + startTime + "' and substr(a.record_time,1,10)<'" + endTime + "'");// 添加参数
                if (i == 1)
                {
                    str.Append(" and a.prod_code='" + prodCode + "'");
                }
                if (teamcode != "00" || teamcode == null)
                {
                    str.Append(" and a.team_id='" + teamcode + "'");
                }
                if (timecode != "00" || timecode == null)
                {
                    str.Append(" and a.shift_id = '" + timecode + "'");
                }
                str.Append(")g");


                str.Append(i.ToString());
                if (i > 1)
                {
                    str.Append(" on g1.prod_code = g");
                    str.Append(i.ToString());
                    str.Append(".prod_code  and g1.team_id = g");
                    str.Append(i.ToString());
                    str.Append(".team_id  and g1.shift_id = g");
                    str.Append(i.ToString());
                    str.Append(".shift_id  and g1.record_time = g");
                    str.Append(i.ToString());
                    str.Append(".record_time  ");
                }
                i++;
            }
            temp.Append(" as 得分");
            sql.Append(",");
            sql.Append(temp);
            sql.Append(" from ");
            sql.Append(str.ToString());
            sql.Append("left join ht_pub_prod_design p on p.prod_code = g1.prod_code left join ht_sys_team t on t.team_code = g1.team_id ");

            sql.Append(" group by p.prod_name");
            if (Isteamgroup)
                sql.Append(",t.team_name, g1.record_time, g1.shift_id");
            sql.Append(" order by g1.record_time, t.team_name");
            return sql.ToString();
        }
        else return null;

    }

    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e) { 
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}