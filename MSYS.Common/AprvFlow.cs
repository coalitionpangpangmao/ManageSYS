using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Collections;
namespace MSYS.Common
{
    public class AprvFlow
    {      
        //审批相关操作
        public static bool createApproval(string[] keys)//启动审批/*TB_ZT标题,MODULENAME审批类型编码,BUSIN_ID业务数据id, 单独登录url,*/
        {
            DbOperator opt = new DbOperator();
            MSYS.Data.SysUser user = (MSYS.Data.SysUser)HttpContext.Current.Session["User"];
            string[] seg = { "TB_ZT", "MODULENAME", "BUSIN_ID", "URL", "TBR_ID", "TBR_NAME", "TB_BM_ID", "TB_DATE" };//TBR_ID填报人id,TBR_NAME填报人name,TB_BM_ID填报部门id,TB_DATE申请时间创建日期
            string[] value = { keys[0], keys[1], keys[2], keys[3], user.Id, user.Name, user.OwningBusinessUnitId, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
            if (seg.Length == value.Length)
            {
                if ("Success" == opt.InsertData(seg, value, "HT_PUB_APRV_FLOWINFO"))
                {
                    string id = opt.GetSegValue("select Max(ID) as ID from HT_PUB_APRV_FLOWINFO", "ID");
                    string query = "select * from HT_PUB_APRV_MODEL where PZ_TYPE = '" + value[1] + "' order by INDEX_NO";
                    DataSet data = opt.CreateDataSetOra(query);
                    if (data != null && data.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in data.Tables[0].Rows)
                        {

                            string enable = "0";
                            if ("1" == row["INDEX_NO"].ToString())
                            {
                                enable = "1";
                                string[] subseg = { "GONGWEN_ID", "ROLENAME", "POS", "WORKITEMID", "ISENABLE", "USERID", "USERNAME", "OPINIONTIME", "COMMENTS", "STATUS" };
                                string[] subvalue = { id, row["ROLE"].ToString(), row["INDEX_NO"].ToString(), row["FLOW_NAME"].ToString(), enable, user.Id, user.Name, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "提交", "2" };
                                opt.InsertData(subseg, subvalue, "HT_PUB_APRV_OPINION");
                            }
                            else
                            {
                                if ("2" == row["INDEX_NO"].ToString())
                                    enable = "1";
                                string[] subseg = { "GONGWEN_ID", "ROLENAME", "POS", "WORKITEMID", "ISENABLE" };
                                string[] subvalue = { id, row["ROLE"].ToString(), row["INDEX_NO"].ToString(), row["FLOW_NAME"].ToString(), enable };
                                opt.InsertData(subseg, subvalue, "HT_PUB_APRV_OPINION");
                            }
                        }
                    }
                }
                return true;

            }
            else
                return false;

        }
        public static bool authorize(string ID, string[] keys)//审批后更新表ID为流程号，value分别为USERID  用户id,USERNAME  用户名,COMMENTS  意见内容,OPINIONTIME  意见填写日期,STATUS  状态
        {
            DbOperator opt = new DbOperator();
            MSYS.Data.SysUser user = (MSYS.Data.SysUser)HttpContext.Current.Session["user"];
            string[] seg = { "USERID", "USERNAME", "OPINIONTIME", "COMMENTS", "STATUS" };
            string[] value = { user.Id, user.Name, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), keys[0], keys[1] };
            if (seg.Length == value.Length)
            {
                //string query = "select * from ht_pub_aprv_opinion where gongwen_ID = (select gongwen_ID from ht_pub_aprv_opinion where id = '" + ID + "') and pos < (select pos from ht_pub_aprv_opinion where id = '" + ID + "') and status <'2'";//找到当前明细审批对应的主审批ID，查看比之顺序号小的业务是不是有未办理或己经被驳回的业务，如果有则逻辑出错，返回；如果没有执行下一步操作          

                opt.UpDateData(seg, value, "HT_PUB_APRV_OPINION", " where id = '" + ID + "'");
                opt.UpDateOra("update HT_PUB_APRV_OPINION set ISENABLE = '1' where id = '" + (Convert.ToInt16(ID) + 1).ToString() + "'");
                DataSet data = opt.CreateDataSetOra("select s.id,t.aprv_table,t.aprv_tabseg,t.BUZ_ID,s.BUSIN_ID from ht_pub_aprv_opinion r left join ht_pub_aprv_flowinfo  s on s.id = r.gongwen_id left join ht_pub_aprv_type t on t.pz_type = s.modulename  where r.id = '" + ID + "'");
                string flowid = data.Tables[0].Rows[0][0].ToString();
                string table = data.Tables[0].Rows[0][1].ToString();
                string tableseg = data.Tables[0].Rows[0][2].ToString();
                string busid = data.Tables[0].Rows[0][3].ToString();
                string busvalue = data.Tables[0].Rows[0][4].ToString();
                if (keys[1] == "1")//如果明细审批单步被拒绝，则整个审批单状态被置为己驳回
                {
                    opt.UpDateOra("Update HT_PUB_APRV_FLOWINFO set STATE = '1' where id = '" + flowid + "'");
                    //将业务主表的审批字段置为己驳回
                    opt.UpDateOra("update " + table + " set " + tableseg + " = '1' where " + busid + " = '" + busvalue + "'");
                }

                else//如果明细审批单所有流程均通过，则整个审批单状态被置为己通过
                {
                    data = opt.CreateDataSetOra("select status,GONGWEN_ID from ht_pub_aprv_opinion where pos = (select Max(pos) from ht_pub_aprv_opinion where gongwen_ID = (select gongwen_ID from ht_pub_aprv_opinion where id = '" + ID + "')) and id = '" + ID + "'");
                    if (data != null && data.Tables[0].Rows.Count > 0 && data.Tables[0].Rows[0][0].ToString() == "2")
                    {
                        ArrayList commandlist = new ArrayList();
                        commandlist.Add("Update HT_PUB_APRV_FLOWINFO set STATE = '2' where id = '" + flowid + "'");
                        commandlist.Add("update " + table + " set " + tableseg + " = '2' where " + busid + " = '" + busvalue + "'");
                        opt.TransactionCommand(commandlist);
                    }
                    //将业务主表的审批字段置为己通过

                }
                return true;

            }
            return false;
        }

  
    }
}
