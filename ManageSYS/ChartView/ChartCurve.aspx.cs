using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Quality_ChartCurve : System.Web.UI.Page
{
    protected string JavaHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            initData();
        }    
    }
    protected void initData()
    {
        if (hdcode.Value.Length > 20)
        {
            int bindex = hdcode.Value.IndexOf("_");
            int eindex = 0;
            string paracode = hdcode.Value.Substring(0, bindex);
            eindex = hdcode.Value.IndexOf("_", bindex + 1);
            string btime = hdcode.Value.Substring(bindex + 1, eindex - bindex);
            bindex = eindex;
            eindex = hdcode.Value.IndexOf("_", bindex + 1);
            string etime = hdcode.Value.Substring(bindex + 1, eindex - bindex);
            bindex = eindex;
            eindex = hdcode.Value.IndexOf("_", bindex + 1);
            string planno = hdcode.Value.Substring(bindex + 1, eindex - bindex);
           DataBaseOperator opt =new DataBaseOperator();
            DataSet set = opt.CreateDataSetOra("select g1.para_code,g1.value,g1.upper_limit,g1.lower_limit ,g1.eer_dev , g3.para_name from ht_prod_month_plan_detail g  left join ht_pub_prod_design g2 on g2.prod_code = g.prod_code  left join ht_tech_stdd_code_detail g1 on g1.tech_code = g2.tech_stdd_code left join ht_pub_tech_para g3 on g1.para_code = g3.para_code where g1.para_code = '" + paracode + "' and g.plan_no = '" + planno + "'");
            if (set != null && set.Tables[0].Rows.Count > 0)
            {
                double upvalue = Convert.ToDouble(set.Tables[0].Rows[0]["upper_limit"].ToString());
                double lowvalue = Convert.ToDouble(set.Tables[0].Rows[0]["lower_limit"].ToString());
                double stdvalue = Convert.ToDouble(set.Tables[0].Rows[0]["value"].ToString());
                double eerdev = Convert.ToDouble(set.Tables[0].Rows[0]["eer_dev"].ToString());
                string paraname = set.Tables[0].Rows[0]["para_name"].ToString();
                IHDataOpt ihopt = new IHDataOpt();
                DataRowCollection rows = ihopt.GetData(btime, etime, paracode, planno);
                CreateChart(stdvalue, upvalue, lowvalue, eerdev, rows, paraname);
            }
        }
          
          
    }
    protected void CreateChart(double stdvalue,double upvalue,double lowvalue,double errdev,DataRowCollection rows,string paraname)
    {
       
        
        JavaHtml = "<script type='text/javascript'>";
        JavaHtml += " var data = [];";
        for(int i = 0;i<rows.Count; i++)
        {
            JavaHtml += " {data.push([" + rows[i][0].ToString() + "," + rows[i][1].ToString() + "]);}";
        }
        JavaHtml += " Highcharts.chart('container',{ chart: { zoomType: 'x' }, title: { text: '" + paraname + "'  }, tooltip: { valueDecimals: 2  },  yAxis: {  title: {   text: '值' }, minorGridLineWidth: 0, gridLineWidth: 0, alternateGridColor: null,plotBands: [{  from: " + (lowvalue - 3 * errdev).ToString() + ", to:" + (lowvalue - errdev).ToString() + ", color: 'rgba(255, 0, 0, 0.1)'}, {  from: " + (lowvalue - errdev).ToString() + ",to:" + lowvalue.ToString() + ", color: 'rgba(255, 255, 0, 0.1)'}, { from:" + lowvalue.ToString() + ", to:" + upvalue.ToString() + ",color: 'rgba(0, 0, 213, 0.1)' }, { from: " + upvalue.ToString() + ", to:" + (upvalue + errdev).ToString() + ", color: 'rgba(255, 255, 0, 0.1)'  }, {  from: " + (upvalue + errdev).ToString() + ", to:" + (upvalue + 3*errdev).ToString() + ", color: 'rgba(255, 0, 0, 0.1)'}] }, series: [{ data: data, lineWidth: 0.5, name: '" + paraname + "' }] });";
        JavaHtml += "</script>";
    }
 
  
}
