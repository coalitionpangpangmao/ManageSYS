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
        bindStatistic();

    }
    

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
        string query;
        if (listTeam.SelectedValue == "")
            query = "select a5.prod_name as 产品, a1.score as 在线考核分,a2.score as 理化检测得分,a3.score as 感观评测得分,a4.score as 过程检测得分,  (select weight from ht_qlt_weight where id='1')*nvl(a1.score,100) +  (select weight from ht_qlt_weight where id='2')*nvl(a2.score,100)+ (select weight from ht_qlt_weight where id='3')*nvl(a3.score,100) + (select weight from ht_qlt_weight where id='4')*nvl(a4.score,100) as 总得分 from (select distinct prod_code from ht_qlt_inspect_record r where substr(r.record_time,1,10) = '" + txtStartTime.Text + "' union select distinct prod_code from ht_qlt_data_record where substr(b_time,1,10) = '" + txtStartTime.Text + "') a left join ht_pub_prod_design a5 on  a5.prod_code = a.prod_code left join (select sum(r.score * s.weight) as score,r.prod_code  from  (select a.section,sum(a.quarate * c.weight*100) as score,a.prod_code from (select r.prod_code, substr(r.para_code,1,5) as section,r.para_code,sum(r.quarate*r.count)/sum(r.count) as quarate from ht_qlt_data_record r where substr(r.b_time,1,10) = '" + txtStartTime.Text + "' group by r.para_code,r.prod_code) a   left join ht_pub_tech_para b on b.para_code  = a.para_code and b.para_type  like '______1%'  left join ht_qlt_weight c on c.id = a.para_code  group by a.section,a.prod_code )r  left join ht_pub_tech_section  s on s.section_code = r.section group by r.prod_code )a1 on a5.prod_name = a.prod_code left join (select round(avg(得分),2) as score,产品 from hv_phychem_daily_report  r left join ht_pub_prod_design t on r.产品 = t.prod_name  where substr(r.检测时间,1,10) = '" + txtStartTime.Text + "' group by 产品)a2 on a5.prod_name = a2.产品 left join (select round(avg(得分),2) as score,产品 from hv_sensor_daily_report  r left join ht_pub_prod_design t on r.产品 = t.prod_name  where substr(r.检测时间,1,10) = '" + txtStartTime.Text + "' group by 产品)a3 on a5.prod_name = a3.产品 left join (select round(avg(得分),2) as score,产品 from hv_process_daily_report  r left join ht_pub_prod_design t on r.产品 = t.prod_name  where substr(r.检测时间,1,10) = '" + txtStartTime.Text + "' group by 产品)a4 on a5.prod_name = a4.产品 order by a5.prod_name";
        else
            query = " select a5.prod_name as 产品,a6.team_name as 班组, a1.score as 在线考核分,a2.score as 理化检测得分,a3.score as 感观评测得分,a4.score as 过程检测得分,  (select weight from ht_qlt_weight where id='1')*nvl(a1.score,100) +  (select weight from ht_qlt_weight where id='2')*nvl(a2.score,100)+ (select weight from ht_qlt_weight where id='3')*nvl(a3.score,100) + (select weight from ht_qlt_weight where id='4')*nvl(a4.score,100) as 总得分   from  (select distinct team_id as team, prod_code from ht_qlt_inspect_record  where substr(record_time,1,10) =  '" + txtStartTime.Text + "' union select distinct team, prod_code from ht_qlt_data_record  where substr(b_time,1,10) =  '" + txtStartTime.Text + "') a  left join ht_pub_prod_design a5 on  a5.prod_code = a.prod_code  left join ht_sys_team a6 on a6.team_code = a.team  left join (select sum(r.score * s.weight) as score,r.prod_code,r.team  from  (select a.section,sum(a.quarate * c.weight*100) as score,a.prod_code,a.team from (select r.prod_code,r.team, substr(r.para_code,1,5) as section,r.para_code,sum(r.quarate*r.count)/sum(r.count) as quarate from ht_qlt_data_record r where substr(r.b_time,1,10) =  '" + txtStartTime.Text + "' group by r.para_code,r.prod_code,r.team) a   left join ht_pub_tech_para b on b.para_code  = a.para_code and b.para_type  like '______1%'  left join ht_qlt_weight c on c.id = a.para_code  group by a.section,a.prod_code ,a.team)r  left join ht_pub_tech_section  s on s.section_code = r.section group by r.prod_code,r.team  )a1 on a.prod_code = a1.prod_code and a6.team_code = a1.team  left join (select round(avg(得分),2) as score,产品,班组 from hv_phychem_daily_report  r left join ht_pub_prod_design t on r.产品 = t.prod_name  where substr(r.检测时间,1,10) =  '" + txtStartTime.Text + "' group by 产品,班组)a2 on a5.prod_name = a2.产品 and a6.team_name = a2.班组  left join (select round(avg(得分),2) as score,产品,班组 from hv_sensor_daily_report  r left join ht_pub_prod_design t on r.产品 = t.prod_name  where substr(r.检测时间,1,10) =  '" + txtStartTime.Text + "' group by 产品,班组)a3 on a5.prod_name = a3.产品 and a6.team_name = a3.班组  left join (select round(avg(得分),2) as score,产品,班组 from hv_process_daily_report  r left join ht_pub_prod_design t on r.产品 = t.prod_name  where substr(r.检测时间,1,10) =  '" + txtStartTime.Text + "' group by 产品,班组)a4 on a5.prod_name = a4.产品 and a6.team_name = a4.班组 where a6.team_code = '" + listTeam.SelectedValue + "'";

        GridAll.DataSource = opt.CreateDataSetOra(query);
        GridAll.DataBind();
        query = "select * from hv_online_daily_report t where substr(时间,1,10) = '" + txtStartTime.Text + "'";
        if (listTeam.SelectedValue != "")
            query += " and t.班组 = '" + listTeam.SelectedItem.Text + "'";
        query += " order by t.产品,t.班组";
        GridView1.DataSource = opt.CreateDataSetOra(query);
        GridView1.DataBind();
        query = "select * from hv_process_daily_report t where substr(检测时间,1,10) = '" + txtStartTime.Text + "'";
        if (listTeam.SelectedValue != "")
            query += " and t.班组 = '" + listTeam.SelectedItem.Text + "'";
        query += " order by t.产品,t.班组";
        GridView2.DataSource = opt.CreateDataSetOra(query);
        GridView2.DataBind();
        query = "select * from hv_phychem_daily_report t where substr(检测时间,1,10) = '" + txtStartTime.Text + "'";
        if (listTeam.SelectedValue != "")
            query += " and t.班组 = '" + listTeam.SelectedItem.Text + "'";
        query += " order by t.产品,t.班组";
        GridView3.DataSource = opt.CreateDataSetOra(query);
        GridView3.DataBind();
        query = "select * from hv_sensor_daily_report t where substr(检测时间,1,10) = '" + txtStartTime.Text + "'";
        if (listTeam.SelectedValue != "")
            query += " and t.班组 = '" + listTeam.SelectedItem.Text + "'";
        query += " order by t.产品,t.班组";
        GridView4.DataSource = opt.CreateDataSetOra(query);
        GridView4.DataBind();

    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindStatistic();
    }
}