using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
public partial class Quality_MonthTeamEva : MSYS.Web.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.PageLoad(sender, e);
        if (!IsPostBack)
        {
            txtStartTime.Text = System.DateTime.Now.ToString("yyyy-MM");
           
        }
    }
   
    protected void bindGrid()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select  g4.team_name as 班组,g4.team_code,g1.班组得分,g2.产品,g3.output as 产量,g3.rate as 产量占比,g2.在线考核分,g2.理化检测得分,g2.感观评测得分,g2.过程检测得分,g2.产品得分  from  (select  r.team,sum(t.rate *r.产品得分) as 班组得分 from hv_qlt_month_prod_Trep r left join hv_prod_month_output t on t.PROD_CODE = r.prod_code and t.month = '" + txtStartTime.Text + "'  where r.month = '" + txtStartTime.Text + "' group by r.team) g1 left join hv_qlt_month_prod_Trep g2 on g2.team = g1.team and g2.month = '" + txtStartTime.Text + "' left join hv_prod_month_output g3 on g3.PROD_CODE = g2.prod_code and g3.month = '" + txtStartTime.Text + "' left join ht_sys_team g4 on g4.team_code = g1.team order by g4.team_code,g2.产品";
        DataSet data = opt.CreateDataSetOra(query);
        GridAll.DataSource = data;
        GridAll.DataBind();

        for (int i = 0; i < 2; i++)
        {
            TableCell oldtc = GridAll.Rows[0].Cells[i];

            oldtc.RowSpan = 1;
            for (int j = 1; j < GridAll.Rows.Count; j++)
            {
                TableCell newtc = GridAll.Rows[j].Cells[i];
                if (newtc.Text == oldtc.Text)
                {
                    newtc.Visible = false;
                    oldtc.RowSpan = oldtc.RowSpan + 1;
                    oldtc.VerticalAlign = VerticalAlign.Middle;
                }
                else
                {
                    oldtc = newtc;
                    oldtc.RowSpan = 1;
                }
            }
        }
    } 

  


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGrid();
    }
}