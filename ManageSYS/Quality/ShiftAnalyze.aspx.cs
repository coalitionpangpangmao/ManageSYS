using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
public partial class Quality_ShiftAnalyze : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            txtStartTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            initView();
        }
    }
    protected void initView()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        opt.bindDropDownList(listTeam, "select team_code,team_name from ht_sys_team where is_del = '0' order by team_code", "team_name", "team_code");
        string adjust = opt.GetSegValue("select count(*) as num from ht_qlt_weight t where is_adjust = '1'", "num");
        if (!(adjust == "0"))
        {
            createView();
            opt.UpDateOra("update ht_qlt_weight set is_adjust = '0'");
        }
        
        bindStatistic();

    }
    //protected void createView()
    //{
    //    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
    //    DataSet data = opt.CreateDataSetOra("select * from ht_qlt_weight t where is_del = '0' order by ID ");
    //    if (data != null && data.Tables[0].Rows.Count == 4)
    //    {
    //        DataRowCollection Rows = data.Tables[0].Rows;
    //        StringBuilder str = new StringBuilder();

    //        str.Append(" select nvl(r.产品,nvl(s.产品,nvl(t.产品,p.产品))) as 产品,nvl(r.班组,nvl(s.班组,nvl(t.班组,p.班组))) as 班组,nvl(r.时间,nvl(s.检测时间,nvl(t.检测时间,p.检测时间))) as 时间,r.总分 as 在线考核分,s.得分 as 过程检测得分,t.得分 as 理化检测得分,p.得分 as 感观评测得分,nvl(r.总分,100) *");
    //        str.Append(Rows[0]["Weight"].ToString());
    //        str.Append("+ nvl(s.得分,100) * ");
    //        str.Append(Rows[1]["Weight"].ToString());
    //        str.Append("+ nvl(t.得分,100) * ");
    //        str.Append(Rows[2]["Weight"].ToString());
    //        str.Append("+ nvl(p.得分,100) * ");
    //        str.Append(Rows[3]["Weight"].ToString());
    //        str.Append(" as 总得分 from hv_online_daily_report r");
    //        str.Append(" full join hv_process_daily_report s on s.产品 = r.产品 and s.班组 = r.班组 and s.检测时间 = r.时间");
    //        str.Append(" full join hv_phychem_daily_report t on (t.产品 = r.产品 and t.班组 = r.班组 and t.检测时间 = r.时间 ) or (t.产品 = s.产品 and t.班组 = s.班组 and t.检测时间 = s.检测时间 )");
    //        str.Append(" full join hv_sensor_daily_report p on (p.产品 = r.产品 and p.班组 = r.班组 and p.检测时间 = r.时间) or (p.产品 = s.产品 and p.班组 = s.班组 and p.检测时间 = s.检测时间 ) or (t.产品 = p.产品 and t.班组 = p.班组 and t.检测时间 = p.检测时间 ) ");
    //        opt.UpDateOra("create or replace view hv_QLT_daily_report as " + str.ToString());
    //    }
    //}

    protected void createView()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        DataSet data = opt.CreateDataSetOra("select * from ht_qlt_weight t where is_del = '0' order by ID ");
        if (data != null && data.Tables[0].Rows.Count == 4)
        {
            DataRowCollection Rows = data.Tables[0].Rows;
            StringBuilder str = new StringBuilder();

            str.Append(" select nvl(r.产品,nvl(s.产品,nvl(t.产品,p.产品))) as 产品,nvl(r.班组,nvl(s.班组,nvl(t.班组,p.班组))) as 班组,nvl(r.时间,nvl(s.检测时间,nvl(t.检测时间,p.检测时间))) as 时间,r.总分 as 在线考核分,s.得分 as 过程检测得分,t.得分 as 理化检测得分,p.得分 as 感观评测得分,nvl(r.总分,100) *");
            str.Append(Rows[0]["Weight"].ToString());
            str.Append("+ nvl(t.得分,100) * ");
            str.Append(Rows[1]["Weight"].ToString());
            str.Append("+ nvl(p.得分,100) * ");
            str.Append(Rows[2]["Weight"].ToString());
            str.Append("+ nvl(s.得分,100) * ");
            str.Append(Rows[3]["Weight"].ToString());
            str.Append(" as 总得分 from (");
            str.Append(getOnlineScore());
            str.Append(")r");
            str.Append(" full join (");
            str.Append(getProcessScore());
            str.Append(") s on s.产品 = r.产品 and s.班组 = r.班组 and s.检测时间 = r.时间");
            str.Append(" full join (");
            str.Append(getPhycheScore());
            str.Append(") t on (t.产品 = r.产品 and t.班组 = r.班组 and t.检测时间 = r.时间 ) or (t.产品 = s.产品 and t.班组 = s.班组 and t.检测时间 = s.检测时间 )");
            str.Append(" full join (");
            str.Append(getSensorScore());
            str.Append(") p on (p.产品 = r.产品 and p.班组 = r.班组 and p.检测时间 = r.时间) or (p.产品 = s.产品 and p.班组 = s.班组 and p.检测时间 = s.检测时间 ) or (t.产品 = p.产品 and t.班组 = p.班组 and t.检测时间 = p.检测时间 ) ");
            opt.UpDateOra("create or replace view hv_QLT_daily_report as " + str.ToString());
        }
    }
    protected string getOnlineScore()
    {
         
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select distinct r.section_code ,r.section_name ,r.remark,r.weight from ht_pub_tech_section r left join ht_pub_tech_para s on substr(s.para_code,1,5) = r.section_code and s.is_del = '0' and s.is_valid = '1' where r.is_del = '0' and r.is_valid = '1' and  s.para_type like '______1%'   order by r.section_code";
        DataSet data = opt.CreateDataSetOra(query);

        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            int i = 1;
            StringBuilder sql = new StringBuilder();
            StringBuilder str = new StringBuilder();
            StringBuilder temp = new StringBuilder();
            sql.Append("select g1.prod_code as 产品,g1.team as 班组,g1.prodday as 时间");
            temp.Append("round(exp(");
            foreach (DataRow row in data.Tables[0].Rows)
            {
                string name = row["remark"].ToString();
                string code = row["section_code"].ToString();
                string weight = row["WEIGHT"].ToString();
                if (i > 1)
                {
                    str.Append(" left join ");
                    temp.Append("+");
                }
                temp.Append("round(ln(power(nvl(g");
                temp.Append(i.ToString());
                temp.Append(".score,1),");
                temp.Append(weight);
                temp.Append(")),3)");
                str.Append("(select prod_code,team,section, prodday,round(exp(sum(ln(power(quarate,weight)))),4) as score from hv_qlt_online_score  where  section = '");
                str.Append(code);
                str.Append("'  group by prod_code,team,section,prodday) g");
                str.Append(i.ToString());
                if (i > 1)
                {
                    str.Append(" on g1.prod_code = g");
                    str.Append(i.ToString());
                    str.Append(".prod_code  and g1.team = g");
                    str.Append(i.ToString());
                    str.Append(".team and g1.prodday = g");
                    str.Append(i.ToString());
                    str.Append(".prodday ");
                }
                i++;
            }
            temp.Append(")*100,2) as 总分");
            sql.Append(",");
            sql.Append(temp);
            sql.Append(" from ");
            sql.Append(str.ToString());
            return sql.ToString();
        }
        else return null;
    }

    protected string getProcessScore()
    {
     
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        string query = "select inspect_code,inspect_name from ht_qlt_inspect_proj  where INSPECT_TYPE = '0' and is_del = '0' order by inspect_code";
        DataSet data = opt.CreateDataSetOra(query);
        StringBuilder sql = new StringBuilder();
        StringBuilder str = new StringBuilder();
        StringBuilder temp = new StringBuilder();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            int i = 1;
            temp.Append("100");
            sql.Append("select g1.prod_code as 产品,g1.team_id as 班组,g1.record_time as 检测时间");
            foreach (DataRow row in data.Tables[0].Rows)
            {
                string name = row["inspect_name"].ToString();
                string code = row["inspect_code"].ToString();

                temp.Append("-nvl(g");
                temp.Append(i.ToString());
                temp.Append(".score,0)");

                if (i > 1)
                    str.Append(" left join ");

                str.Append("(select   a.prod_code,a.team_id,a.shift_ID,a.record_time ,nvl(b.score,0) as score, nvl(a.inspect_value,0) as ");
                str.Append(name);
                str.Append("  from ht_qlt_inspect_record a left join ht_qlt_inspect_event b on b.record_id = a.id where a.inspect_code = '");
                str.Append(code);
                str.Append("')g");

                str.Append(i.ToString());
                if (i > 1)
                {
                    str.Append(" on g1.prod_code = g");
                    str.Append(i.ToString());
                    str.Append(".prod_code  and g1.team_id = g");
                    str.Append(i.ToString());
                    str.Append(".team_id   and g1.record_time = g");
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
            return sql.ToString();

        }
        else return null;
    
    }

    protected string getPhycheScore()
    {
         
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        string query = "select inspect_code,inspect_name from ht_qlt_inspect_proj  where inspect_group in('1','2','3') and is_del = '0' order by inspect_code";
        DataSet data = opt.CreateDataSetOra(query);
        StringBuilder sql = new StringBuilder();
        StringBuilder str = new StringBuilder();
        StringBuilder temp = new StringBuilder();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            int i = 1;
            temp.Append("100");

            sql.Append("select g1.prod_code as 产品,g1.team_id as 班组,g1.record_time as 检测时间");
            foreach (DataRow row in data.Tables[0].Rows)
            {
                string name = row["inspect_name"].ToString();
                string code = row["inspect_code"].ToString();


                temp.Append("-nvl(g");
                temp.Append(i.ToString());
                temp.Append(".score,0)");

                if (i > 1)
                    str.Append(" left join ");

                str.Append("(select   a.prod_code,a.team_id,a.shift_ID,a.record_time ,nvl(b.score,0) as score, nvl(a.inspect_value,0) as ");
                str.Append(name);
                str.Append("  from ht_qlt_inspect_record a left join ht_qlt_inspect_event b on b.record_id = a.id where a.inspect_code = '");
                str.Append(code);
                str.Append("')g");


                str.Append(i.ToString());
                if (i > 1)
                {
                    str.Append(" on g1.prod_code = g");
                    str.Append(i.ToString());
                    str.Append(".prod_code  and g1.team_id = g");
                    str.Append(i.ToString());
                    str.Append(".team_id    and g1.record_time = g");
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
            return sql.ToString();

        }
        else return null;

    }
    protected string getSensorScore()
    {
       
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();

        string query = "select inspect_code,inspect_name from ht_qlt_inspect_proj  where inspect_group  ='4' and is_del = '0' order by inspect_code";
        DataSet data = opt.CreateDataSetOra(query);
        StringBuilder sql = new StringBuilder();
        StringBuilder str = new StringBuilder();
        StringBuilder temp = new StringBuilder();
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            int i = 1;
            temp.Append("100");

            sql.Append("select g1.prod_code as 产品,g1.team_id as 班组,g1.record_time as 检测时间");
            foreach (DataRow row in data.Tables[0].Rows)
            {
                string name = row["inspect_name"].ToString();
                string code = row["inspect_code"].ToString();
               
                temp.Append("-nvl(g");
                temp.Append(i.ToString());
                temp.Append(".score,0)");

                if (i > 1)
                    str.Append(" left join ");

                str.Append("(select   a.prod_code,a.team_id,a.shift_ID,a.record_time ,nvl(b.score,0) as score, nvl(a.inspect_value,0) as ");
                str.Append(name);
                str.Append("  from ht_qlt_inspect_record a left join ht_qlt_inspect_event b on b.record_id = a.id where a.inspect_code = '");
                str.Append(code);
                str.Append("')g");


                str.Append(i.ToString());
                if (i > 1)
                {
                    str.Append(" on g1.prod_code = g");
                    str.Append(i.ToString());
                    str.Append(".prod_code  and g1.team_id = g");
                    str.Append(i.ToString());
                    str.Append(".team_id  and g1.record_time = g");
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
           return sql.ToString();
        }
        else return null;
    }
    
    protected void bindStatistic()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select s.prod_name as 产品,r.team_name as 班组,t.在线考核分,t.过程检测得分,t.理化检测得分,t.感观评测得分,t.总得分 from hv_QLT_daily_report t left join ht_sys_team r on r.team_code = t.班组 left join ht_pub_prod_design s on s.prod_code = t.产品 where  t.时间 = '" + txtStartTime.Text + "'";
        if (listTeam.SelectedValue != "")
            query += " and t.班组 = '" + listTeam.SelectedValue + "'";
        query += " order by s.prod_name,r.team_name";
        GridAll.DataSource = opt.CreateDataSetOra(query);
        GridAll.DataBind();

        query = "select * from hv_online_daily_report t where 时间 = '" + txtStartTime.Text + "'";
        if (listTeam.SelectedValue != "")
            query += " and 班组 = '" + listTeam.SelectedItem.Text + "'";
        query += " order by 产品,班组";
        GridView1.DataSource = opt.CreateDataSetOra(query);
        GridView1.DataBind();
        query = "select * from hv_process_daily_report t where 检测时间 = '" + txtStartTime.Text + "'";
        if (listTeam.SelectedValue != "")
            query += " and 班组 = '" + listTeam.SelectedItem.Text + "'";
        query += " order by 产品,班组";
        GridView2.DataSource = opt.CreateDataSetOra(query);
        GridView2.DataBind();
        query = "select * from hv_phychem_daily_report t where 检测时间 = '" + txtStartTime.Text + "'";
        if (listTeam.SelectedValue != "")
            query += " and 班组 = '" + listTeam.SelectedItem.Text + "'";
        query += " order by 产品,班组";
        GridView3.DataSource = opt.CreateDataSetOra(query);
        GridView3.DataBind();
        query = "select * from hv_sensor_daily_report t where 检测时间 = '" + txtStartTime.Text + "'";
        if (listTeam.SelectedValue != "")
            query += " and 班组 = '" + listTeam.SelectedItem.Text + "'";
        query += " order by 产品,班组";
        GridView4.DataSource = opt.CreateDataSetOra(query);
        GridView4.DataBind();

    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindStatistic();
    }
}