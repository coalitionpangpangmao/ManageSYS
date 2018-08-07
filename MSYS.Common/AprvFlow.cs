using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Collections;
namespace MSYS.Common
{
    /// <summary>
    /// 将审批业务转化为事务操作
    /// </summary>
    public class AprvFlow
    {
        /// <summary>
        /// 传入一个需审批的业务，对业务安审批流程进行分解
        /// </summary>
        /// <param name="keys">
        /// keys[0]:TB_ZT标题
        /// keys[1]:MODULENAME审批类型编码
        /// keys[2]:BUSIN_ID业务数据id
        /// keys[3]:登录url
        /// 自动根据上下文状态确定 TBR_ID填报人id,TBR_NAME填报人name,TB_BM_ID填报部门id,TB_DATE申请时间创建日期,ID审批
        /// </param>
        /// <returns></returns>
        public static bool createApproval(string[] keys)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            MSYS.Data.SysUser user = (MSYS.Data.SysUser)HttpContext.Current.Session["User"];
            List<String> commandlist = new List<String>();

            //插入审批主业务
            string ID = opt.GetSegValue(" select zs18.aprvflow_id_seq.nextval from dual", "nextval");
            string[] seg = { "TB_ZT", "MODULENAME", "BUSIN_ID", "URL", "TBR_ID", "TBR_NAME", "TB_BM_ID", "TB_DATE", "ID" };
            string[] value = { keys[0], keys[1], keys[2], keys[3], user.id, user.text, user.OwningBusinessUnitId, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ID };
            commandlist.Add(opt.InsertDatastr(seg, value, "HT_PUB_APRV_FLOWINFO"));

            //插入审批流程
            //1、插入审批流程第一步提交流程 顺序号为0
            string[] subseg0 = { "GONGWEN_ID", "ROLENAME", "POS", "WORKITEMID", "ISENABLE", "USERID", "USERNAME", "OPINIONTIME", "COMMENTS", "STATUS" };
            string[] subvalue0 = { ID, user.UserRole, "0", "提交审批", "1", user.id, user.text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "提交", "2" };
            commandlist.Add(opt.InsertDatastr(subseg0, subvalue0, "HT_PUB_APRV_OPINION"));

            //2、根据审批模版中定义的顺序将审批业务分解为顺序流程
            string query = "select * from HT_PUB_APRV_MODEL where PZ_TYPE = '" + value[1] + "' order by INDEX_NO";
            DataSet data = opt.CreateDataSetOra(query);
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    string enable = "0";
                    if ("1" == row["INDEX_NO"].ToString())
                        enable = "1";
                    string[] subseg = { "GONGWEN_ID", "ROLENAME", "POS", "WORKITEMID", "ISENABLE" };
                    string[] subvalue = { ID, row["ROLE"].ToString(), row["INDEX_NO"].ToString(), row["FLOW_NAME"].ToString(), enable };
                    commandlist.Add(opt.InsertDatastr(subseg, subvalue, "HT_PUB_APRV_OPINION"));
                }
            }
            if ("Success" == opt.TransactionCommand(commandlist))
                return true;

            else
                return false;

        }

        /// <summary>
        /// 处理审批业务时进行的操作
        /// </summary>
        /// <param name="ID">审批流程ID号</param>
        /// <param name="keys">
        /// keys[0]:COMMENTS  意见内容
        /// keys[1]:STATUS  状态
        /// USERID  用户id,USERNAME  用户名 根据上下文自动填充
        /// </param>
        /// <returns></returns>
        public static bool authorize(string ID, string[] keys)
        {
            MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
            MSYS.Data.SysUser user = (MSYS.Data.SysUser)HttpContext.Current.Session["user"];
            List<String> commandlist = new List<String>();

            //1.改变当前审批业务状态
            string[] seg = { "USERID", "USERNAME", "OPINIONTIME", "COMMENTS", "STATUS" };
            string[] value = { user.id, user.text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), keys[0], keys[1] };
            commandlist.Add(opt.UpdateStr(seg, value, "HT_PUB_APRV_OPINION", " where id = '" + ID + "'"));

            //2.将当前审批业务的下一环节业务置为Enable，提交给相对应的角色
            string nextID = opt.GetSegValue("select id from ht_pub_aprv_opinion where (gongwen_id,pos) in ( select gongwen_id,to_char(to_number(pos)+1) from ht_pub_aprv_opinion where id= '" + ID + "')", "ID");
            if (nextID != "NoRecord")
                commandlist.Add("update HT_PUB_APRV_OPINION set ISENABLE = '1' where id = '" + nextID + "'");

            //3.从审批主表中匹配审批业务详情，将审批结果在被审批业务中进行反馈
            DataSet data = opt.CreateDataSetOra("select s.id,t.aprv_table,t.aprv_tabseg,t.BUZ_ID,s.BUSIN_ID from ht_pub_aprv_opinion r left join ht_pub_aprv_flowinfo  s on s.id = r.gongwen_id left join ht_pub_aprv_type t on t.pz_type = s.modulename  where r.id = '" + ID + "'");
            string flowid = data.Tables[0].Rows[0][0].ToString();
            string table = data.Tables[0].Rows[0][1].ToString();
            string tableseg = data.Tables[0].Rows[0][2].ToString();
            string busid = data.Tables[0].Rows[0][3].ToString();
            string busvalue = data.Tables[0].Rows[0][4].ToString();
            //如果明细审批单步被拒绝，则整个审批单状态被置为己驳回，被审批业务不通过
            if (keys[1] == "1")
            {
                commandlist.Add("Update HT_PUB_APRV_FLOWINFO set STATE = '1' where id = '" + flowid + "'");
                //将业务主表的审批字段置为己驳回
                commandlist.Add("update " + table + " set " + tableseg + " = '1' where " + busid + " = '" + busvalue + "'");
            }
            //如果明细审批单所有流程均通过，则整个审批单状态被置为己通过
            if ("Success" == opt.TransactionCommand(commandlist))
            {

                data = opt.CreateDataSetOra("select status,GONGWEN_ID from ht_pub_aprv_opinion where pos = (select Max(pos) from ht_pub_aprv_opinion where gongwen_ID = (select gongwen_ID from ht_pub_aprv_opinion where id = '" + ID + "')) and id = '" + ID + "'");
                if (data != null && data.Tables[0].Rows.Count > 0 && data.Tables[0].Rows[0][0].ToString() == "2")
                {
                    commandlist.Clear();
                    commandlist.Add("Update HT_PUB_APRV_FLOWINFO set STATE = '2' where id = '" + flowid + "'");
                    //将业务主表的审批字段置为己通过
                    commandlist.Add("update " + table + " set " + tableseg + " = '2' where " + busid + " = '" + busvalue + "'");
                    opt.TransactionCommand(commandlist);
                }
                return true;
            }
            else
                return false;

        }

        //public static bool authorize(string ID, string[] keys)//审批后更新表ID为流程号，value分别为USERID  用户id,USERNAME  用户名,COMMENTS  意见内容,OPINIONTIME  意见填写日期,STATUS  状态
        //{
        //    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        //    MSYS.Data.SysUser user = (MSYS.Data.SysUser)HttpContext.Current.Session["user"];
        //    string[] seg = { "USERID", "USERNAME", "OPINIONTIME", "COMMENTS", "STATUS" };
        //    string[] value = { user.Id, user.Name, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), keys[0], keys[1] };
        //    if (seg.Length == value.Length)
        //    {
        //        //string query = "select * from ht_pub_aprv_opinion where gongwen_ID = (select gongwen_ID from ht_pub_aprv_opinion where id = '" + ID + "') and pos < (select pos from ht_pub_aprv_opinion where id = '" + ID + "') and status <'2'";//找到当前明细审批对应的主审批ID，查看比之顺序号小的业务是不是有未办理或己经被驳回的业务，如果有则逻辑出错，返回；如果没有执行下一步操作          

        //        opt.UpDateData(seg, value, "HT_PUB_APRV_OPINION", " where id = '" + ID + "'");
        //        opt.UpDateOra("update HT_PUB_APRV_OPINION set ISENABLE = '1' where id = '" + (Convert.ToInt16(ID) + 1).ToString() + "'");
        //        DataSet data = opt.CreateDataSetOra("select s.id,t.aprv_table,t.aprv_tabseg,t.BUZ_ID,s.BUSIN_ID from ht_pub_aprv_opinion r left join ht_pub_aprv_flowinfo  s on s.id = r.gongwen_id left join ht_pub_aprv_type t on t.pz_type = s.modulename  where r.id = '" + ID + "'");
        //        string flowid = data.Tables[0].Rows[0][0].ToString();
        //        string table = data.Tables[0].Rows[0][1].ToString();
        //        string tableseg = data.Tables[0].Rows[0][2].ToString();
        //        string busid = data.Tables[0].Rows[0][3].ToString();
        //        string busvalue = data.Tables[0].Rows[0][4].ToString();
        //        if (keys[1] == "1")//如果明细审批单步被拒绝，则整个审批单状态被置为己驳回
        //        {
        //            opt.UpDateOra("Update HT_PUB_APRV_FLOWINFO set STATE = '1' where id = '" + flowid + "'");
        //            //将业务主表的审批字段置为己驳回
        //            opt.UpDateOra("update " + table + " set " + tableseg + " = '1' where " + busid + " = '" + busvalue + "'");
        //        }

        //        else//如果明细审批单所有流程均通过，则整个审批单状态被置为己通过
        //        {
        //            data = opt.CreateDataSetOra("select status,GONGWEN_ID from ht_pub_aprv_opinion where pos = (select Max(pos) from ht_pub_aprv_opinion where gongwen_ID = (select gongwen_ID from ht_pub_aprv_opinion where id = '" + ID + "')) and id = '" + ID + "'");
        //            if (data != null && data.Tables[0].Rows.Count > 0 && data.Tables[0].Rows[0][0].ToString() == "2")
        //            {
        //                List<String> commandlist = new List<String>();
        //                commandlist.Add("Update HT_PUB_APRV_FLOWINFO set STATE = '2' where id = '" + flowid + "'");
        //                commandlist.Add("update " + table + " set " + tableseg + " = '2' where " + busid + " = '" + busvalue + "'");
        //                opt.TransactionCommand(commandlist);
        //            }
        //            //将业务主表的审批字段置为己通过

        //        }
        //        return true;

        //    }
        //    return false;
        //}
        //public static bool createApproval(string[] keys)//启动审批/*TB_ZT标题,MODULENAME审批类型编码,BUSIN_ID业务数据id, 单独登录url,*/
        //{
        //    MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        //    MSYS.Data.SysUser user = (MSYS.Data.SysUser)HttpContext.Current.Session["User"];
        //    string[] seg = { "TB_ZT", "MODULENAME", "BUSIN_ID", "URL", "TBR_ID", "TBR_NAME", "TB_BM_ID", "TB_DATE" };//TBR_ID填报人id,TBR_NAME填报人name,TB_BM_ID填报部门id,TB_DATE申请时间创建日期
        //    string[] value = { keys[0], keys[1], keys[2], keys[3], user.Id, user.Name, user.OwningBusinessUnitId, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        //    if (seg.Length == value.Length)
        //    {
        //        if ("Success" == opt.InsertData(seg, value, "HT_PUB_APRV_FLOWINFO"))
        //        {
        //            string id = opt.GetSegValue("select Max(ID) as ID from HT_PUB_APRV_FLOWINFO", "ID");
        //            string query = "select * from HT_PUB_APRV_MODEL where PZ_TYPE = '" + value[1] + "' order by INDEX_NO";
        //            DataSet data = opt.CreateDataSetOra(query);
        //            if (data != null && data.Tables[0].Rows.Count > 0)
        //            {
        //                foreach (DataRow row in data.Tables[0].Rows)
        //                {

        //                    string enable = "0";
        //                    if ("1" == row["INDEX_NO"].ToString())
        //                    {
        //                        enable = "1";
        //                        string[] subseg = { "GONGWEN_ID", "ROLENAME", "POS", "WORKITEMID", "ISENABLE", "USERID", "USERNAME", "OPINIONTIME", "COMMENTS", "STATUS" };
        //                        string[] subvalue = { id, row["ROLE"].ToString(), row["INDEX_NO"].ToString(), row["FLOW_NAME"].ToString(), enable, user.Id, user.Name, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "提交", "2" };
        //                        opt.InsertData(subseg, subvalue, "HT_PUB_APRV_OPINION");
        //                    }
        //                    else
        //                    {
        //                        if ("2" == row["INDEX_NO"].ToString())
        //                            enable = "1";
        //                        string[] subseg = { "GONGWEN_ID", "ROLENAME", "POS", "WORKITEMID", "ISENABLE" };
        //                        string[] subvalue = { id, row["ROLE"].ToString(), row["INDEX_NO"].ToString(), row["FLOW_NAME"].ToString(), enable };
        //                        opt.InsertData(subseg, subvalue, "HT_PUB_APRV_OPINION");
        //                    }
        //                }
        //            }
        //        }
        //        return true;

        //    }
        //    else
        //        return false;

        //}
    }
}
