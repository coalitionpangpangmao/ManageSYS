using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.Common;
using System.Data;
using System.Web.UI.WebControls;
using MSYS.DAL;
using System.Collections;
using System.Text;
namespace MSYS.DAL
{
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
            Object obj = Activator.CreateInstance(sDll, sType).Unwrap();
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
        public string TransactionCommand(ArrayList commandStringList)
        {
            return dboperator.TransactionCommand(commandStringList);
        }
        public string UpDateData(string[] seg, string[] value, string table, string condition)
        {
            string query = UpdateStr(seg, value, table, condition);
            if (query != "")
            {
                return UpDateOra(query);
            }
            else
                return "Error!!";
        }
        public string UpdateStr(string[] seg, string[] value, string table, string condition)
        {
            if (seg.Length == value.Length)
            {
                StringBuilder query = new StringBuilder();
                query.Append("update ");
                query.Append(table);
                query.Append(" set ");
                for (int i = 0; i < seg.Length; i++)
                {
                    query.Append(seg[i]);
                    query.Append(" = '");
                    query.Append(value[i]);
                    query.Append("' ");
                    if (i < seg.Length - 1)
                        query.Append(",");
                }
                query.Append(condition);
                return query.ToString();
            }
            else
                return "";
        }
        public string InsertData(string[] seg, string[] value, string table)
        {
            string query = InsertDatastr(seg, value, table);
            if (query != "")
            {
                return UpDateOra(query);
            }
            else
                return "Error!!";
        }
        public string InsertDatastr(string[] seg, string[] value, string table)
        {
            if (seg.Length == value.Length)
            {
                StringBuilder query = new StringBuilder();
                query.Append("insert into ");
                query.Append(table);
                query.Append("(");
                for (int i = 0; i < seg.Length; i++)
                {
                    query.Append(seg[i]);
                    if (i < seg.Length - 1)
                        query.Append(",");
                }
                query.Append(")values(");
                for (int i = 0; i < value.Length; i++)
                {
                    query.Append("'");
                    query.Append(value[i]);
                    query.Append("'");
                    if (i < seg.Length - 1)
                        query.Append(",");
                }
                query.Append(")");
                return query.ToString();
            }
            else
                return "";
        }
        public string MergeInto(string[] seg, string[] value, int key, string table)
        {
            string query = getMergeStr(seg, value, key, table);
            if (query != "")
            {
                return UpDateOra(query);
            }
            else
                return "Error!!";
        }
        public string getMergeStr(string[] seg, string[] value, int key, string table)
        {
            if (seg.Length == value.Length)
            {
                StringBuilder str = new StringBuilder();
                StringBuilder updatestr = new StringBuilder();
                StringBuilder condition = new StringBuilder();
                StringBuilder insertstr = new StringBuilder();



                str.Append("MERGE INTO ");
                str.Append(table);
                str.Append(" T1 ");
                str.Append("USING(select ");
                updatestr.Append(" UPDATE SET ");
                insertstr.Append(" Insert (");
                for (int i = 0; i < seg.Length; i++)
                {
                    if (i != 0)
                    {
                        str.Append(",");
                        insertstr.Append(",");
                        condition.Append(",");
                    }
                    //////////////////////////////////////////////
                    str.Append("'");
                    str.Append(value[i]);
                    str.Append("' AS ");
                    str.Append(seg[i]);
                    /////////////////////////////////////////////////
                    insertstr.Append(seg[i]);
                    condition.Append("'");
                    condition.Append(value[i]);
                    condition.Append("'");
                    ////////////////////////////////////////////////
                    if (i >= key)
                    {
                        if (i > key)
                            updatestr.Append(",");
                        updatestr.Append("T1.");
                        updatestr.Append(seg[i]);
                        updatestr.Append("=");
                        updatestr.Append("T2.");
                        updatestr.Append(seg[i]);
                    }
                }
                str.Append(" from dual) T2 on(");
                insertstr.Append(")values(");
                insertstr.Append(condition.ToString());
                insertstr.Append(")");

                condition.Clear();
                for (int j = 0; j < key; j++)
                {
                    if (j != 0)
                        condition.Append(" and ");
                    condition.Append("T1.");
                    condition.Append(seg[j]);
                    condition.Append("=");
                    condition.Append("T2.");
                    condition.Append(seg[j]);
                }

                str.Append(condition.ToString());
                str.Append(")");
                str.Append(" WHEN MATCHED THEN ");
                str.Append(updatestr.ToString());
                str.Append(" WHEN NOT MATCHED THEN ");
                str.Append(insertstr);
                return str.ToString();
            }
            else return "";
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

        //查询是否有符合条件的记录
        public bool hasRecord(string query)
        {
            DataSet data = CreateDataSetOra(query);
            if (data != null && data.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }




    }
}
