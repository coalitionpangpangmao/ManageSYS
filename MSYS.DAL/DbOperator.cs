using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.Common;
using System.Data;
using System.Web.UI.WebControls;
using MSYS.DAL;

public class DbOperator
{
    private IDbOperator dboperator = null;

    /// <summary>
    /// 传入一个IDbOperator类型的数据库操作对象，后续操作就一样
    /// </summary>
    /// <param name="opt"></param>
    public DbOperator(IDbOperator opt)
    {        
        dboperator = opt;     
    }
    /// <summary>
    /// 如果不传参数，则默认是Oracle数据库  需要在配置文件中设置连接字符串
    /// </summary>
    public DbOperator()
    {  
        string sAppSettingText = ConfigurationManager.AppSettings["DbOperator"];
        //通过字符串获取需要的dll信息  
        string sDll = sAppSettingText.Split(',')[0];
        //获取类型信息  
        string sType = sAppSettingText.Split(',')[1];
        //创建对象  
        Object obj = Activator.CreateInstance(sDll, sType).Unwrap()  ;
        dboperator = (IDbOperator)obj;
    }
    public string UpDateOra(string query)
    {
        return dboperator.UpDate(query);
    }
    public DataSet CreateDataSetOra(string query)
    {
        return dboperator.CreateDataSet(query);
    }
    public string UpDateData(string[] seg, string[] value, string table, string condition)
    {
        if (seg.Length == value.Length)
        {
            string query = "update " + table + " set ";
            for (int i = 0; i < seg.Length; i++)
            {
                query += seg[i] + " = '" + value[i] + "' ";
                if (i < seg.Length - 1)
                    query += ",";
            }
            query += condition;
            return UpDateOra(query);
        }
        else
            return "Error!!";
    }
    public string InsertData(string[] seg, string[] value, string table)
    {
        if (seg.Length == value.Length)
        {
            string query = "insert into " + table + "(";
            for (int i = 0; i < seg.Length; i++)
            {
                query += seg[i];
                if (i < seg.Length - 1)
                    query += ",";
            }
            query += ")values(";
            for (int i = 0; i < value.Length; i++)
            {
                query += "'" + value[i] + "'";
                if (i < seg.Length - 1)
                    query += ",";
            }
            query += ")";
            return UpDateOra(query);
        }
        else
            return "Error!!";
    }
    //插入日志记录
    public string InsertTlog(string user, string cmt, string record)
    {
        string query = "insert into HT_SVR_LOGIN_RECORD(F_USER,F_COMPUTER,F_TIME,F_DESCRIPT)values('"
                + user + "','"
                + cmt + "','"
                + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','"
                + record + "')";
        return UpDateOra(query);
    }
    //绑定下拉表
    public void bindDropDownList(DropDownList list, string query, string textField, string valueField)
    {
        DataSet data = CreateDataSetOra(query);
        list.Items.Clear();
        list.DataSource = data;
        list.DataTextField = textField;
        list.DataValueField = valueField;
        list.DataBind();
        ListItem item = new ListItem("", "");
        list.Items.Insert(0, item);
    }
    //通过查询语句获取某一字段值
    public string GetSegValue(string query, string seg)
    {
        DataSet data = CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string temp = data.Tables[0].Rows[0][seg].ToString();
            return temp;
        }
        else return "NoRecord";
    }
    //查询语句后  行列倒置
    protected DataSet ShiftTable(string query)
    {
        DataSet DstTemp = CreateDataSetOra(query);
        DataTable TabTemp = new DataTable();
        DataRow MyRow;
        String[,] AryTemp = new String[DstTemp.Tables[0].Columns.Count, DstTemp.Tables[0].Rows.Count + 1];
        int i = 0;
        int ii;
        foreach (DataColumn col in DstTemp.Tables[0].Columns)
        {
            ii = 1;
            AryTemp[i, 0] = col.Caption;
            // ii = 0;
            foreach (DataRow row in DstTemp.Tables[0].Rows)
            {
                AryTemp[i, ii++] = row[i].ToString();
            }
            i++;
        }

        for (i = 0; i <= DstTemp.Tables[0].Rows.Count; i++)
            TabTemp.Columns.Add(new DataColumn(i.ToString()));

        for (i = 0; i < DstTemp.Tables[0].Columns.Count; i++)
        {
            MyRow = TabTemp.NewRow();
            for (ii = 0; ii <= DstTemp.Tables[0].Rows.Count; ii++)
            {
                MyRow[ii] = AryTemp[i, ii];
            }
            TabTemp.Rows.Add(MyRow);
        }

        DataSet set = new DataSet();
        set.Merge(TabTemp);
        return set;
    }

    public bool hasRecord(string query)
    {
        DataSet data = CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
            return true;
        else
            return false;
    }
    #region Approval
    //审批相关操作
    public bool createApproval(string[] keys)//启动审批/*TB_ZT标题,MODULENAME审批类型编码,BUSIN_ID业务数据id, 单独登录url,*/
    {
        string[] seg = { "TB_ZT", "MODULENAME", "BUSIN_ID", "URL", "TBR_ID", "TBR_NAME", "TB_BM_ID", "TB_BM_NAME", "TB_DATE" };//TBR_ID填报人id,TBR_NAME填报人name,TB_BM_ID填报部门id,TB_BM_NAME填报部门name,TB_DATE申请时间创建日期
        string[] value = { keys[0], keys[1], keys[2], keys[3], "cookieID", "cookieNAME", GetSegValue("select * from HT_SVR_USER where ID = '" + "cookieID" + "'", "ORG_ID"), GetSegValue("select * from HT_SVR_USER where ID = '" + "cookieID" + "'", "ORG_NAME"), System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        if (seg.Length == value.Length)
        {
            InsertData(seg, value, "HT_PUB_APRV_FLOWINFO");
            string id = GetSegValue("select Max(ID) as ID from HT_PUB_APRV_FLOWINFO", "ID");
            string query = "select * from HT_PUB_APRV_MODEL where PZ_TYPE = '" + value[1] + "' order by INDEX_NO";
            DataSet data = CreateDataSetOra(query);
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in data.Tables[0].Rows)
                {

                    string enable = "0";
                    if ("1" == row["INDEX_NO"].ToString())
                    {
                        enable = "1";
                        string[] subseg = { "GONGWEN_ID", "ROLENAME", "POS", "WORKITEMID", "ISENABLE", "USERID", "USERNAME", "OPINIONTIME", "COMMENTS", "STATUS" };
                        string[] subvalue = { id, row["ROLE"].ToString(), row["INDEX_NO"].ToString(), row["FLOW_NAME"].ToString(), enable, "cookieId", "cookieName", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "提交", "2" };
                        InsertData(subseg, subvalue, "HT_PUB_APRV_OPINION");
                    }
                    else
                    {
                        if ("2" == row["INDEX_NO"].ToString())
                            enable = "1";
                        string[] subseg = { "GONGWEN_ID", "ROLENAME", "POS", "WORKITEMID", "ISENABLE" };
                        string[] subvalue = { id, row["ROLE"].ToString(), row["INDEX_NO"].ToString(), row["FLOW_NAME"].ToString(), enable };
                        InsertData(subseg, subvalue, "HT_PUB_APRV_OPINION");
                    }
                }
            }
            return true;

        }
        else
            return false;

    }
    public bool authorize(string ID, string[] keys)//审批后更新表ID为流程号，value分别为USERID  用户id,USERNAME  用户名,COMMENTS  意见内容,OPINIONTIME  意见填写日期,STATUS  状态
    {

        string[] seg = { "USERID", "USERNAME", "OPINIONTIME", "COMMENTS", "STATUS" };
        string[] value = { "cookieID", "cookieNAME", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), keys[0], keys[1] };
        if (seg.Length == value.Length)
        {
            //string query = "select * from ht_pub_aprv_opinion where gongwen_ID = (select gongwen_ID from ht_pub_aprv_opinion where id = '" + ID + "') and pos < (select pos from ht_pub_aprv_opinion where id = '" + ID + "') and status <'2'";//找到当前明细审批对应的主审批ID，查看比之顺序号小的业务是不是有未办理或己经被驳回的业务，如果有则逻辑出错，返回；如果没有执行下一步操作          

            UpDateData(seg, value, "HT_PUB_APRV_OPINION", " where id = '" + ID + "'");
            UpDateOra("update HT_PUB_APRV_OPINION set ISENABLE = '1' where id = '" + (Convert.ToInt16(ID) + 1).ToString() + "'");
            DataSet data = CreateDataSetOra("select s.id,t.aprv_table,t.aprv_tabseg,t.BUZ_ID,s.BUSIN_ID from ht_pub_aprv_opinion r left join ht_pub_aprv_flowinfo  s on s.id = r.gongwen_id left join ht_pub_aprv_type t on t.pz_type = s.modulename  where r.id = '" + ID + "'");
            string flowid = data.Tables[0].Rows[0][0].ToString();
            string table = data.Tables[0].Rows[0][1].ToString();
            string tableseg = data.Tables[0].Rows[0][2].ToString();
            string busid = data.Tables[0].Rows[0][3].ToString();
            string busvalue = data.Tables[0].Rows[0][4].ToString();
            if (keys[1] == "1")//如果明细审批单步被拒绝，则整个审批单状态被置为己驳回
            {
                UpDateOra("Update HT_PUB_APRV_FLOWINFO set STATE = '1' where id = '" + flowid + "'");
                //将业务主表的审批字段置为己驳回
                UpDateOra("update " + table + " set " + tableseg + " = '1' where " + busid + " = '" + busvalue + "'");
            }

            else//如果明细审批单所有流程均通过，则整个审批单状态被置为己通过
            {
                data = CreateDataSetOra("select status,GONGWEN_ID from ht_pub_aprv_opinion where pos = (select Max(pos) from ht_pub_aprv_opinion where gongwen_ID = (select gongwen_ID from ht_pub_aprv_opinion where id = '" + ID + "')) and id = '" + ID + "'");
                if (data != null && data.Tables[0].Rows.Count > 0 && data.Tables[0].Rows[0][0].ToString() == "2")
                {
                    UpDateOra("Update HT_PUB_APRV_FLOWINFO set STATE = '2' where id = '" + flowid + "'");
                }
                //将业务主表的审批字段置为己通过
                UpDateOra("update " + table + " set " + tableseg + " = '2' where " + busid + " = '" + busvalue + "'");
            }
            return true;

        }
        return false;
    }

    #endregion


}
