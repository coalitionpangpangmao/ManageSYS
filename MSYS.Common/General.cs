namespace MSYS.Common
{
    //    using AV56eTW6mMjw87o6bxU;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Configuration;
    using System.Data;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    public static class General
    {
        //        private static XmlDocument AA1YNisqZ2Po240AFws2Y4MH25SW = null;
        //        private static DateTime ABuGXxneo6DAkrWJV4 = DateTime.Now;
        //        [CompilerGenerated]
        //        private static MatchEvaluator ACAVEfDt_JCsoeqSyb;
        //        [CompilerGenerated]
        //        private static MatchEvaluator AD8wVnlvXOE2UKMYlE;
        //        [CompilerGenerated]
        //        private static MatchEvaluator AEIRlK3EAPtY8VyHZY;
        //        [CompilerGenerated]
        //        private static MatchEvaluator AF_r3_UDSgdvZBKer4;
        //        public const string InnerScript = "<script language=\"javascript\" type=\"text/javascript\" charset=\"gb2312\">{0}</script>";
        //        public const string SrcScript = "<script language=\"javascript\" type=\"text/javascript\" src=\"{0}\" charset=\"gb2312\"></script>";

        //        [CompilerGenerated]
        //        private static string AA1YNisqZ2Po240AFws2Y4MH25SW(Match match1)
        //        {
        //            switch (match1.ToString())
        //            {
        //                case ">":
        //                    return "&gt;";

        //                case "<":
        //                    return "&lt;";

        //                case """:
        //                    return "&quot;";

        //                case "&":
        //                    return "&amp;";

        //                case "
        //":
        //                    return "&#xD;";

        //                case "
        //":
        //                    return "&#xA;";

        //                case "	":
        //                    return "&#xA;";
        //            }
        //            return match1.ToString();
        //        }

        //        [CompilerGenerated]
        //        private static string ABuGXxneo6DAkrWJV4(Match match1)
        //        {
        //            Wima.Common.General.ABuGXxneo6DAkrWJV4 rwjv = new Wima.Common.General.ABuGXxneo6DAkrWJV4 {
        //                AA1YNisqZ2Po240AFws2Y4MH25SW = match1
        //            };
        //            string str = rwjv.AA1YNisqZ2Po240AFws2Y4MH25SW.Groups[1].Value;
        //            string input = rwjv.AA1YNisqZ2Po240AFws2Y4MH25SW.Groups[3].Value;
        //            string str3 = rwjv.AA1YNisqZ2Po240AFws2Y4MH25SW.Groups[5].Value;
        //            input = new Regex("[\\[\\]\\*%]").Replace(input, new MatchEvaluator(rwjv.AA1YNisqZ2Po240AFws2Y4MH25SW));
        //            return (str + input + str3);
        //        }

        //        [CompilerGenerated]
        //        private static string ACAVEfDt_JCsoeqSyb(Match match1)
        //        {
        //            string str2 = match1.ToString();
        //            if (str2 != null)
        //            {
        //                if (str2 == "&gt;")
        //                {
        //                    return ">";
        //                }
        //                if (str2 == "&lt;")
        //                {
        //                    return "<";
        //                }
        //                if (str2 == "&qout;")
        //                {
        //                    return """;
        //                }
        //                if (str2 == "&amp;")
        //                {
        //                    return "&";
        //                }
        //            }
        //            return match1.ToString();
        //        }

        //        [CompilerGenerated]
        //        private static string AD8wVnlvXOE2UKMYlE(Match match1)
        //        {
        //            string str2 = match1.ToString();
        //            if (str2 != null)
        //            {
        //                if (str2 == "")
        //                {
        //                    return "<br>";
        //                }
        //                if (str2 == "
        //")
        //                {
        //                    return "<br>";
        //                }
        //                if (str2 == "
        //")
        //                {
        //                    return "<br>";
        //                }
        //                if (str2 == """)
        //                {
        //                    return "\\"";
        //                }
        //            }
        //            return match1.ToString();
        //        }

        //        public static string AddURLPara(string url, string paraName, string paraValue)
        //        {
        //            string str = url;
        //            if (str == null)
        //            {
        //                str = "";
        //            }
        //            if (str.IndexOf("?") == -1)
        //            {
        //                str = str + "?";
        //            }
        //            else if (!str.EndsWith("&"))
        //            {
        //                str = str + "&";
        //            }
        //            return (str + paraName + "=" + paraValue);
        //        }

        //        public static string AdvXmlAttribute(string name, string value)
        //        {
        //            if (string.IsNullOrEmpty(value))
        //            {
        //                return string.Empty;
        //            }
        //            return XmlAttribute(name, value);
        //        }

        //        public static void AppendControl(Page thisPage, string Id, string Value)
        //        {
        //            TextBox child = new TextBox {
        //                TextMode = TextBoxMode.MultiLine,
        //                ID = Id
        //            };
        //            child.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
        //            child.Text = Value;
        //            thisPage.Page.Form.Controls.Add(child);
        //        }

        //        public static void AppendDBLog(string sql)
        //        {
        //            if (CommConfig.GetBool("OpenDBLog") && !(string.IsNullOrEmpty(sql) || (sql.IndexOf("FROM SysMessage") >= 0)))
        //            {
        //                AppendXMLLog("SQL语句：" + sql);
        //            }
        //        }

        //        public static void AppendDBLog(string sql, Exception e)
        //        {
        //            if (CommConfig.GetBool("OpenDBLog") && (!string.IsNullOrEmpty(sql) && (sql.IndexOf("FROM SysMessage") < 0)))
        //            {
        //                string msg = "SQL语句：" + sql;
        //                if (e != null)
        //                {
        //                    msg = msg + "错误信息：" + e.ToString();
        //                }
        //                AppendXMLLog(msg);
        //            }
        //        }

        //        public static void AppendDBLog(string sql, Exception e, DateTime startTime)
        //        {
        //            if (CommConfig.GetBool("OpenDBLog") && (!string.IsNullOrEmpty(sql) && (sql.IndexOf("FROM SysMessage") < 0)))
        //            {
        //                object obj2 = "SQL语句：" + sql;
        //                object[] objArray = new object[4];
        //                objArray[0] = obj2;
        //                objArray[1] = " 大概执行时间：";
        //                TimeSpan span = (TimeSpan) (DateTime.Now - startTime);
        //                objArray[2] = span.TotalMilliseconds;
        //                objArray[3] = " 毫秒";
        //                string msg = string.Concat(objArray);
        //                if (e != null)
        //                {
        //                    msg = msg + "错误信息：" + e.ToString();
        //                }
        //                else
        //                {
        //                    span = (TimeSpan) (DateTime.Now - startTime);
        //                    if (CommConfig.GetInt("OpenDBTimeOutLog") > span.TotalMilliseconds)
        //                    {
        //                        return;
        //                    }
        //                }
        //                AppendXMLLog(msg);
        //            }
        //        }

        //        public static T[] AppendItem<T>(T[] source, T item)
        //        {
        //            T[] array = new T[source.Length + 1];
        //            source.CopyTo(array, 0);
        //            array[source.Length] = item;
        //            return array;
        //        }

        //        public static T[] AppendItems<T>(T[] source, T[] items)
        //        {
        //            T[] array = new T[source.Length + items.Length];
        //            source.CopyTo(array, 0);
        //            Array.Copy(items, 0, array, source.Length, items.Length);
        //            return array;
        //        }

        //        public static void AppendLog(string txt)
        //        {
        //            if (HttpContext.Current.Application["Log"] == null)
        //            {
        //                HttpContext.Current.Application["Log"] = txt + "";
        //            }
        //            else
        //            {
        //                HttpContext.Current.Application["Log"] = HttpContext.Current.Application["Log"].ToString() + txt + "";
        //            }
        //        }

        //        public static void AppendXMLLog(string msg)
        //        {
        //            try
        //            {
        //                string path = HttpContext.Current.Server.MapPath("~") + "/Logs";
        //                if (!Directory.Exists(path))
        //                {
        //                    Directory.CreateDirectory(path);
        //                }
        //                string str2 = "log_" + DateTime.Now.ToString("yyyyMMdd") + ".xml";
        //                string str3 = HttpContext.Current.Server.MapPath("~") + "/Logs/" + str2;
        //                if (!File.Exists(str3))
        //                {
        //                    string s = "<?xml version="1.0" encoding="utf-8" ?><Logs></Logs>";
        //                    byte[] bytes = Encoding.UTF8.GetBytes(s);
        //                    FileStream stream = File.Create(str3);
        //                    stream.Write(bytes, 0, bytes.Length);
        //                    stream.Close();
        //                    stream.Dispose();
        //                }
        //                if (AA1YNisqZ2Po240AFws2Y4MH25SW == null)
        //                {
        //                    AA1YNisqZ2Po240AFws2Y4MH25SW = new XmlDataDocument();
        //                    AA1YNisqZ2Po240AFws2Y4MH25SW.Load(str3);
        //                }
        //                XmlNode node = AA1YNisqZ2Po240AFws2Y4MH25SW.SelectSingleNode("Logs");
        //                XmlElement newChild = AA1YNisqZ2Po240AFws2Y4MH25SW.CreateElement("Log");
        //                newChild.SetAttribute("LogDate", DateTime.Now.ToString());
        //                newChild.InnerXml = CDATA(msg);
        //                node.AppendChild(newChild);
        //                TimeSpan span = (TimeSpan) (DateTime.Now - ABuGXxneo6DAkrWJV4);
        //                if (span.Seconds > 5)
        //                {
        //                    ABuGXxneo6DAkrWJV4 = DateTime.Now;
        //                    AA1YNisqZ2Po240AFws2Y4MH25SW.Save(str3);
        //                }
        //            }
        //            catch
        //            {
        //            }
        //        }

        //        public static string CDATA(string txt) => 
        //            ("<![CDATA[" + txt + "]]>");

        //        public static void CheckAndCreateDir(string folder)
        //        {
        //            string path = HttpContext.Current.Server.MapPath(StartPath + folder);
        //            if (!Directory.Exists(path))
        //            {
        //                Directory.CreateDirectory(path);
        //            }
        //        }

        //        public static bool CheckAndCreateFile(string fileName)
        //        {
        //            string path = HttpContext.Current.Server.MapPath(StartPath + fileName);
        //            if (!File.Exists(path))
        //            {
        //                File.Create(path);
        //                return false;
        //            }
        //            return true;
        //        }

        //        public static DataTable ConvertDtRowToCell(DataTable source, string[] marginRowFields, string[] marginCellFields, params string[] valueFields) => 
        //            ConvertDtRowToCell(source, marginRowFields, marginCellFields, true, valueFields);

        //        public static DataTable ConvertDtRowToCell(DataTable source, string[] marginRowFields, string[] marginCellFields, bool isValueFieldNum, params string[] valueFields)
        //        {
        //            int num3;
        //            DataTable table = null;
        //            table = ConvertDtRowToCellModel(source, marginRowFields, marginCellFields, valueFields);
        //            int length = marginRowFields.Length;
        //            int num2 = marginCellFields.Length;
        //            string str = "";
        //            for (num3 = 0; num3 < length; num3++)
        //            {
        //                str = str + marginRowFields[num3];
        //                if (num3 < (length - 1))
        //                {
        //                    str = str + ",";
        //                }
        //            }
        //            DataView defaultView = source.DefaultView;
        //            defaultView.Sort = str;
        //            DataTable table2 = defaultView.ToTable(true, marginRowFields);
        //            int count = table2.Rows.Count;
        //            int num5 = table.Columns.Count;
        //            int num6 = valueFields.Length;
        //            string str2 = valueFields[0];
        //            for (num3 = 0; num3 < count; num3++)
        //            {
        //                string str4;
        //                object obj2;
        //                string columnName;
        //                DataRow row = table.NewRow();
        //                string filter = "";
        //                int index = 0;
        //                while (index < length)
        //                {
        //                    str4 = table2.Rows[num3][index].ToString();
        //                    row[index] = table2.Rows[num3][index];
        //                    string str8 = filter;
        //                    filter = str8 + marginRowFields[index] + "='" + str4 + "'";
        //                    if (index < (length - 1))
        //                    {
        //                        filter = filter + " and ";
        //                    }
        //                    index++;
        //                }
        //                if (num2 > 0)
        //                {
        //                    string caption;
        //                    if (num6 == 1)
        //                    {
        //                        index = length;
        //                        while (index < num5)
        //                        {
        //                            caption = table.Columns[index].Caption;
        //                            str4 = "";
        //                            if (filter != "")
        //                            {
        //                                caption = filter + " and " + caption;
        //                            }
        //                            if (isValueFieldNum)
        //                            {
        //                                obj2 = source.Compute("sum(" + str2 + ")", caption);
        //                                if (obj2 != null)
        //                                {
        //                                    str4 = obj2.ToString();
        //                                }
        //                                else
        //                                {
        //                                    str4 = "";
        //                                }
        //                            }
        //                            else
        //                            {
        //                                obj2 = source.Compute("max(" + str2 + ")", caption);
        //                                if (obj2 != null)
        //                                {
        //                                    str4 = obj2.ToString();
        //                                }
        //                                else
        //                                {
        //                                    str4 = "";
        //                                }
        //                            }
        //                            row[index] = str4;
        //                            index++;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        index = length;
        //                        while (index < num5)
        //                        {
        //                            columnName = table.Columns[index].ColumnName;
        //                            string str7 = columnName.Substring(columnName.LastIndexOf("__") + 2);
        //                            caption = table.Columns[index].Caption;
        //                            str4 = "";
        //                            if (filter != "")
        //                            {
        //                                caption = filter + " and " + caption;
        //                            }
        //                            if (isValueFieldNum)
        //                            {
        //                                obj2 = source.Compute("sum(" + str7 + ")", caption);
        //                                if (obj2 != null)
        //                                {
        //                                    str4 = obj2.ToString();
        //                                }
        //                                else
        //                                {
        //                                    str4 = "";
        //                                }
        //                            }
        //                            else
        //                            {
        //                                obj2 = source.Compute("max(" + str7 + ")", caption);
        //                                if (obj2 != null)
        //                                {
        //                                    str4 = obj2.ToString();
        //                                }
        //                                else
        //                                {
        //                                    str4 = "";
        //                                }
        //                            }
        //                            row[index] = str4;
        //                            index++;
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    for (index = length; index < num5; index++)
        //                    {
        //                        columnName = table.Columns[index].ColumnName;
        //                        str4 = "";
        //                        obj2 = source.Compute("sum(" + columnName + ")", filter);
        //                        if (obj2 != null)
        //                        {
        //                            str4 = obj2.ToString();
        //                        }
        //                        else
        //                        {
        //                            str4 = "";
        //                        }
        //                        row[index] = str4;
        //                    }
        //                }
        //                table.Rows.Add(row);
        //            }
        //            return table;
        //        }

        //        public static DataTable ConvertDtRowToCell(DataTable source, string valueField, string valueFieldType, string[] marginRowFields, string[] marginCellFields)
        //        {
        //            int num3;
        //            DataTable table = null;
        //            table = ConvertDtRowToCellModel(source, marginRowFields, marginCellFields, new string[0]);
        //            int length = marginRowFields.Length;
        //            int num2 = marginCellFields.Length;
        //            string str = "";
        //            for (num3 = 0; num3 < length; num3++)
        //            {
        //                str = str + marginRowFields[num3];
        //                if (num3 < (length - 1))
        //                {
        //                    str = str + ",";
        //                }
        //            }
        //            DataView defaultView = source.DefaultView;
        //            defaultView.Sort = str;
        //            DataTable table2 = defaultView.ToTable(true, marginRowFields);
        //            int count = table2.Rows.Count;
        //            int num5 = table.Columns.Count;
        //            if (valueFieldType == null)
        //            {
        //                valueFieldType = source.Columns[valueField].DataType.ToString();
        //            }
        //            bool flag = true;
        //            if (valueFieldType.ToLower().IndexOf("string") > -1)
        //            {
        //                flag = false;
        //            }
        //            for (num3 = 0; num3 < count; num3++)
        //            {
        //                string str3;
        //                DataRow row = table.NewRow();
        //                string str2 = "";
        //                int index = 0;
        //                while (index < length)
        //                {
        //                    string str5;
        //                    str3 = table2.Rows[num3][index].ToString();
        //                    row[index] = table2.Rows[num3][index];
        //                    marginRowFields[index] = str5 = "='" + str3 + "'";
        //                    str2 = str2 + str5;
        //                    if (index < (length - 1))
        //                    {
        //                        str2 = str2 + ",";
        //                    }
        //                    index++;
        //                }
        //                for (index = length; index < num5; index++)
        //                {
        //                    object obj2;
        //                    string caption = table.Columns[index].Caption;
        //                    str3 = "";
        //                    if (str2 != "")
        //                    {
        //                        caption = str2 + "," + caption;
        //                    }
        //                    if (flag)
        //                    {
        //                        obj2 = source.Compute("sum(" + valueField + ")", caption);
        //                        if (obj2 != null)
        //                        {
        //                            str3 = obj2.ToString();
        //                        }
        //                        else
        //                        {
        //                            str3 = "";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        obj2 = source.Compute("max(" + valueField + ")", caption);
        //                        if (obj2 != null)
        //                        {
        //                            str3 = obj2.ToString();
        //                        }
        //                        else
        //                        {
        //                            str3 = "";
        //                        }
        //                    }
        //                    row[index] = str3;
        //                }
        //                table.Rows.Add(row);
        //            }
        //            return table;
        //        }

        //        public static DataTable ConvertDtRowToCell(DataTable source, string[] marginRowFields, string[] marginCellFields, string valueField, string rowToColField, ref string[] valueRowToColValues) => 
        //            ConvertDtRowToCell(source, marginRowFields, marginCellFields, valueField, true, rowToColField, ref valueRowToColValues);

        //        public static DataTable ConvertDtRowToCell(DataTable source, string[] marginRowFields, string[] marginCellFields, string valueField, bool isValueFieldNum, string rowToColField, ref string[] valueRowToColValues)
        //        {
        //            int num3;
        //            DataTable table = null;
        //            table = ConvertDtRowToCellModel(source, marginRowFields, marginCellFields, rowToColField, ref valueRowToColValues);
        //            int length = marginRowFields.Length;
        //            int num2 = marginCellFields.Length;
        //            string str = "";
        //            for (num3 = 0; num3 < length; num3++)
        //            {
        //                str = str + marginRowFields[num3];
        //                if (num3 < (length - 1))
        //                {
        //                    str = str + ",";
        //                }
        //            }
        //            DataView defaultView = source.DefaultView;
        //            defaultView.Sort = str;
        //            DataTable table2 = defaultView.ToTable(true, new string[] { str });
        //            int count = table2.Rows.Count;
        //            int num5 = table.Columns.Count;
        //            int num6 = valueRowToColValues.Length;
        //            for (num3 = 0; num3 < count; num3++)
        //            {
        //                string str3;
        //                string caption;
        //                object obj2;
        //                string columnName;
        //                DataRow row = table.NewRow();
        //                string str2 = "";
        //                int index = 0;
        //                while (index < length)
        //                {
        //                    str3 = table2.Rows[num3][index].ToString();
        //                    row[index] = table2.Rows[num3][index];
        //                    string str7 = str2;
        //                    str2 = str7 + marginRowFields[index] + "='" + str3 + "'";
        //                    if (index < (length - 1))
        //                    {
        //                        str2 = str2 + " and ";
        //                    }
        //                    index++;
        //                }
        //                if (num2 > 0)
        //                {
        //                    if (num6 == 1)
        //                    {
        //                        index = length;
        //                        while (index < num5)
        //                        {
        //                            caption = table.Columns[index].Caption;
        //                            str3 = "";
        //                            if (str2 != "")
        //                            {
        //                                caption = str2 + " and " + caption;
        //                            }
        //                            if (isValueFieldNum)
        //                            {
        //                                obj2 = source.Compute("sum(" + valueField + ")", caption);
        //                                if (obj2 != null)
        //                                {
        //                                    str3 = obj2.ToString();
        //                                }
        //                                else
        //                                {
        //                                    str3 = "";
        //                                }
        //                            }
        //                            else
        //                            {
        //                                obj2 = source.Compute("max(" + valueField + ")", caption);
        //                                if (obj2 != null)
        //                                {
        //                                    str3 = obj2.ToString();
        //                                }
        //                                else
        //                                {
        //                                    str3 = "";
        //                                }
        //                            }
        //                            row[index] = str3;
        //                            index++;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        index = length;
        //                        while (index < num5)
        //                        {
        //                            columnName = table.Columns[index].ColumnName;
        //                            string str6 = columnName.Substring(columnName.LastIndexOf("__") + 2);
        //                            caption = table.Columns[index].Caption;
        //                            str3 = "";
        //                            if (str2 != "")
        //                            {
        //                                caption = str2 + " and " + caption;
        //                            }
        //                            if (isValueFieldNum)
        //                            {
        //                                obj2 = source.Compute("sum(" + valueField + ")", caption);
        //                                if (obj2 != null)
        //                                {
        //                                    str3 = obj2.ToString();
        //                                }
        //                                else
        //                                {
        //                                    str3 = "";
        //                                }
        //                            }
        //                            else
        //                            {
        //                                obj2 = source.Compute("max(" + valueField + ")", caption);
        //                                if (obj2 != null)
        //                                {
        //                                    str3 = obj2.ToString();
        //                                }
        //                                else
        //                                {
        //                                    str3 = "";
        //                                }
        //                            }
        //                            row[index] = str3;
        //                            index++;
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    for (index = length; index < num5; index++)
        //                    {
        //                        columnName = table.Columns[index].ColumnName;
        //                        str3 = "";
        //                        caption = table.Columns[index].Caption;
        //                        if ((caption != "") && (str2 != ""))
        //                        {
        //                            caption = str2 + " and " + caption;
        //                        }
        //                        obj2 = source.Compute("sum(" + columnName + ")", caption);
        //                        if (obj2 != null)
        //                        {
        //                            str3 = obj2.ToString();
        //                        }
        //                        else
        //                        {
        //                            str3 = "";
        //                        }
        //                        row[index] = str3;
        //                    }
        //                }
        //                table.Rows.Add(row);
        //            }
        //            return table;
        //        }

        //        public static DataTable ConvertDtRowToCellModel(DataTable source, string[] marginRowFields, string[] marginCellFields, params string[] valueFields)
        //        {
        //            int num2;
        //            DataColumn column;
        //            string str2;
        //            DataTable table = null;
        //            table = new DataTable();
        //            int length = marginRowFields.Length;
        //            for (num2 = 0; num2 < length; num2++)
        //            {
        //                column = new DataColumn(marginRowFields[num2]) {
        //                    Caption = ""                };
        //                table.Columns.Add(column);
        //            }
        //            string str = "";
        //            int num3 = marginCellFields.Length;
        //            for (num2 = 0; num2 < num3; num2++)
        //            {
        //                str = str + marginCellFields[num2];
        //                if (num2 < (num3 - 1))
        //                {
        //                    str = str + ",";
        //                }
        //            }
        //            if ((marginCellFields != null) && (marginCellFields.Length > 0))
        //            {
        //                DataView defaultView = source.DefaultView;
        //                defaultView.Sort = str;
        //                DataTable table2 = defaultView.ToTable(true, marginCellFields);
        //                int count = table2.Rows.Count;
        //                for (num2 = 0; num2 < count; num2++)
        //                {
        //                    str2 = "";
        //                    string str3 = "";
        //                    for (int i = 0; i < num3; i++)
        //                    {
        //                        string str4 = table2.Rows[num2][i].ToString();
        //                        str2 = str2 + str4;
        //                        string str6 = str3;
        //                        str3 = str6 + marginCellFields[i] + "='" + str4 + "'";
        //                        if (i < (num3 - 1))
        //                        {
        //                            str2 = str2 + "__";
        //                            str3 = str3 + ",";
        //                        }
        //                    }
        //                    if ((valueFields == null) || (valueFields.Length < 2))
        //                    {
        //                        column = new DataColumn(str2) {
        //                            Caption = str3
        //                        };
        //                        table.Columns.Add(column);
        //                    }
        //                    else
        //                    {
        //                        for (int j = 0; j < valueFields.Length; j++)
        //                        {
        //                            column = new DataColumn(str2 + "__" + valueFields[j]) {
        //                                Caption = str3
        //                            };
        //                            table.Columns.Add(column);
        //                        }
        //                    }
        //                }
        //                return table;
        //            }
        //            if (valueFields != null)
        //            {
        //                for (num2 = 0; num2 < valueFields.Length; num2++)
        //                {
        //                    str2 = valueFields[num2];
        //                    column = new DataColumn(str2) {
        //                        Caption = ""                    };
        //                    table.Columns.Add(column);
        //                }
        //            }
        //            return table;
        //        }

        //        public static DataTable ConvertDtRowToCellModel(DataTable source, string[] marginRowFields, string[] marginCellFields, string rowToColField, ref string[] valueRowToColValues)
        //        {
        //            DataView defaultView;
        //            DataTable table2;
        //            int count;
        //            int num2;
        //            DataColumn column;
        //            string str2;
        //            string str3;
        //            DataTable table = null;
        //            table = new DataTable();
        //            if ((valueRowToColValues == null) || (valueRowToColValues.Length == 0))
        //            {
        //                defaultView = source.DefaultView;
        //                defaultView.Sort = rowToColField;
        //                table2 = defaultView.ToTable(true, new string[] { rowToColField });
        //                count = table2.Rows.Count;
        //                valueRowToColValues = new string[count];
        //                for (num2 = 0; num2 < count; num2++)
        //                {
        //                    valueRowToColValues[num2] = table2.Rows[num2][0].ToString();
        //                }
        //            }
        //            int length = marginRowFields.Length;
        //            for (num2 = 0; num2 < length; num2++)
        //            {
        //                column = new DataColumn(marginRowFields[num2]) {
        //                    Caption = ""                };
        //                table.Columns.Add(column);
        //            }
        //            string str = "";
        //            int num4 = marginCellFields.Length;
        //            for (num2 = 0; num2 < num4; num2++)
        //            {
        //                str = str + marginCellFields[num2];
        //                if (num2 < (num4 - 1))
        //                {
        //                    str = str + ",";
        //                }
        //            }
        //            if ((marginCellFields != null) && (marginCellFields.Length > 0))
        //            {
        //                defaultView = source.DefaultView;
        //                defaultView.Sort = str;
        //                table2 = defaultView.ToTable(true, marginCellFields);
        //                count = table2.Rows.Count;
        //                for (num2 = 0; num2 < count; num2++)
        //                {
        //                    str2 = "";
        //                    str3 = "";
        //                    for (int i = 0; i < num4; i++)
        //                    {
        //                        string str4 = table2.Rows[num2][i].ToString();
        //                        str2 = str2 + str4;
        //                        string str7 = str3;
        //                        str3 = str7 + marginCellFields[i] + "='" + str4 + "'";
        //                        if (i < (num4 - 1))
        //                        {
        //                            str2 = str2 + "__";
        //                            str3 = str3 + " and ";
        //                        }
        //                    }
        //                    if ((valueRowToColValues == null) || (valueRowToColValues.Length < 2))
        //                    {
        //                        column = new DataColumn(str2) {
        //                            Caption = str3
        //                        };
        //                        table.Columns.Add(column);
        //                    }
        //                    else
        //                    {
        //                        for (int j = 0; j < valueRowToColValues.Length; j++)
        //                        {
        //                            column = new DataColumn(str2 + "__" + valueRowToColValues[j]);
        //                            string str6 = str3 + " and " + rowToColField + "='" + valueRowToColValues[j] + "'";
        //                            column.Caption = str6;
        //                            table.Columns.Add(column);
        //                        }
        //                    }
        //                }
        //                return table;
        //            }
        //            if (valueRowToColValues != null)
        //            {
        //                for (num2 = 0; num2 < valueRowToColValues.Length; num2++)
        //                {
        //                    str2 = valueRowToColValues[num2];
        //                    str3 = rowToColField + "='" + str2 + "'";
        //                    column = new DataColumn(str2) {
        //                        Caption = str3
        //                    };
        //                    table.Columns.Add(column);
        //                }
        //            }
        //            return table;
        //        }

        //        public static void CopyFile(string fromFile, string toFile, int lengthEachTime)
        //        {
        //            byte[] buffer;
        //            FileStream stream = new FileStream(fromFile, FileMode.Open, FileAccess.Read);
        //            FileStream stream2 = new FileStream(toFile, FileMode.Append, FileAccess.Write);
        //            if (lengthEachTime < stream.Length)
        //            {
        //                int num;
        //                buffer = new byte[lengthEachTime];
        //                int num2 = 0;
        //                while (num2 <= (((int) stream.Length) - lengthEachTime))
        //                {
        //                    num = stream.Read(buffer, 0, lengthEachTime);
        //                    stream.Flush();
        //                    stream2.Write(buffer, 0, lengthEachTime);
        //                    stream2.Flush();
        //                    stream2.Position = stream.Position;
        //                    num2 += num;
        //                }
        //                int count = ((int) stream.Length) - num2;
        //                num = stream.Read(buffer, 0, count);
        //                stream.Flush();
        //                stream2.Write(buffer, 0, count);
        //                stream2.Flush();
        //            }
        //            else
        //            {
        //                buffer = new byte[stream.Length];
        //                stream.Read(buffer, 0, (int) stream.Length);
        //                stream.Flush();
        //                stream2.Write(buffer, 0, (int) stream.Length);
        //                stream2.Flush();
        //            }
        //            stream.Close();
        //            stream2.Close();
        //        }

        //        public static void CreateFile(string fileName, string content)
        //        {
        //            string str = HttpContext.Current.Server.MapPath(StartPath + fileName);
        //            CheckAndCreateFile(str);
        //            StreamWriter writer = new StreamWriter(File.OpenRead(str), Encoding.Default);
        //            writer.WriteLine(content);
        //            writer.Close();
        //        }

        //        public static string DataTableToJSON(DataTable dt)
        //        {
        //            int num;
        //            string str = null;
        //            if ((dt == null) || (dt.Columns.Count <= 0))
        //            {
        //                return "json={info:"Error"}";
        //            }
        //            if (dt.Rows.Count <= 0)
        //            {
        //                str = "json={info:"Success",count:"0",data:[";
        //                num = 0;
        //                while (num < dt.Columns.Count)
        //                {
        //                    str = str + """ + dt.Columns[num].ColumnName + "",";
        //                    num++;
        //                }
        //                return (str.TrimEnd(new char[] { ',' }) + "]}");
        //            }
        //            str = "json={info:"Success", count:"" + dt.Rows.Count.ToString() + "",data:[";
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                str = str + "{";
        //                for (num = 0; num < dt.Columns.Count; num++)
        //                {
        //                    string str3 = str;
        //                    str = str3 + dt.Columns[num].ColumnName + ":"" + row[dt.Columns[num]].ToString().Replace("
        //", "\\n").Replace(""", "\\"").Replace("\\", "\\\\") + "",";
        //                }
        //                str = str.TrimEnd(new char[] { ',' }) + "},";
        //            }
        //            return (str.TrimEnd(new char[] { ',' }) + "]};");
        //        }

        //        public static string DataTableToXml(DataTable dt, string rootName, string extraNodeString)
        //        {
        //            DataTable table = XmlToDataSet("<?xml version="1.0" encoding="utf-8" ?><root><extra>" + extraNodeString + "</extra></root>").Tables[0].Copy();
        //            DataSet set = new DataSet("WMList");
        //            set.Tables.Add(table);
        //            set.Tables.Add(dt);
        //            if (dt != null)
        //            {
        //                MemoryStream w = null;
        //                XmlTextWriter writer = null;
        //                try
        //                {
        //                    w = new MemoryStream();
        //                    writer = new XmlTextWriter(w, Encoding.Unicode);
        //                    set.WriteXml(writer);
        //                    int length = (int) w.Length;
        //                    byte[] buffer = new byte[length];
        //                    w.Seek(0L, SeekOrigin.Begin);
        //                    w.Read(buffer, 0, length);
        //                    UnicodeEncoding encoding = new UnicodeEncoding();
        //                    string str = encoding.GetString(buffer).Trim();
        //                    return ("<?xml version="1.0" encoding="utf-8" ?>" + str);
        //                }
        //                catch (Exception exception)
        //                {
        //                    throw exception;
        //                }
        //                finally
        //                {
        //                    if (writer != null)
        //                    {
        //                        writer.Close();
        //                        w.Close();
        //                        w.Dispose();
        //                    }
        //                }
        //            }
        //            return "";
        //        }

        //        public static object Deserialize(string value, Type type)
        //        {
        //            XmlSerializer serializer = new XmlSerializer(type);
        //            StringReader textReader = new StringReader(value);
        //            return serializer.Deserialize(textReader);
        //        }

        //        public static object Deserialize(Type type, string path)
        //        {
        //            if (File.Exists(path))
        //            {
        //                FileStream stream = File.OpenRead(path);
        //                byte[] buffer = new byte[stream.Length];
        //                stream.Read(buffer, 0, buffer.Length);
        //                stream.Close();
        //                stream.Dispose();
        //                return Deserialize(Encoding.UTF8.GetString(buffer), type);
        //            }
        //            return null;
        //        }

        //        public static PropertyDescriptor GetChildPropertyByName(PropertyDescriptor parentPD, string[] path, int beginIndex)
        //        {
        //            PropertyDescriptorCollection childProperties = parentPD.GetChildProperties();
        //            for (int i = 0; i < childProperties.Count; i++)
        //            {
        //                if (childProperties[i].Name == path[beginIndex])
        //                {
        //                    if (beginIndex == (path.Length - 1))
        //                    {
        //                        return childProperties[i];
        //                    }
        //                    return GetChildPropertyByName(childProperties[i], path, beginIndex + 1);
        //                }
        //            }
        //            return null;
        //        }

        //        public static string GetDateTimeValueSpan(string value, string valueSpan, string dateFmt)
        //        {
        //            string s = "s";
        //            double result = 0.0;
        //            if (valueSpan.Length >= 2)
        //            {
        //                s = valueSpan.Substring(0, 1);
        //                if (double.TryParse(s, out result))
        //                {
        //                    s = "s";
        //                    result = ToDefine<double>(valueSpan, 0.0);
        //                }
        //                else
        //                {
        //                    result = ToDefine<double>(valueSpan.Substring(1, valueSpan.Length - 1), 0.0);
        //                }
        //            }
        //            dateFmt = string.IsNullOrEmpty(dateFmt) ? "yyyy-MM-dd" : dateFmt;
        //            switch (s)
        //            {
        //                case "y":
        //                case "Y":
        //                    value = ToDefine<DateTime>(value, DateTime.Now).AddYears((int) result).ToString(dateFmt);
        //                    return value;

        //                case "H":
        //                case "h":
        //                    value = ToDefine<DateTime>(value, DateTime.Now).AddHours(result).ToString(dateFmt);
        //                    return value;

        //                case "d":
        //                case "D":
        //                    value = ToDefine<DateTime>(value, DateTime.Now).AddDays(result).ToString(dateFmt);
        //                    return value;

        //                case "M":
        //                    value = ToDefine<DateTime>(value, DateTime.Now).AddMonths((int) result).ToString(dateFmt);
        //                    return value;

        //                case "m":
        //                    value = ToDefine<DateTime>(value, DateTime.Now).AddMinutes(result).ToString(dateFmt);
        //                    return value;
        //            }
        //            value = ToDefine<DateTime>(value, DateTime.Now).AddSeconds(result).ToString(dateFmt);
        //            return value;
        //        }

        //        public static XmlNode GetEntityOtherNode(string entity, string nodeName, string name)
        //        {
        //            XmlDocument entityXmlDoc = GetEntityXmlDoc(entity);
        //            if (entityXmlDoc == null)
        //            {
        //                return null;
        //            }
        //            XmlNode node = null;
        //            node = entityXmlDoc.SelectSingleNode("Entity/Other/" + nodeName + "List");
        //            XmlNode node2 = null;
        //            if (node != null)
        //            {
        //                node2 = node.SelectSingleNode(nodeName + "[@Name='" + name + "']");
        //            }
        //            return node2;
        //        }

        //        public static string GetEntityOtherSQLString(string entity, string name)
        //        {
        //            string xmlPath = "Entity/Other/SQLList/SQL[@Name='" + name + "']/DataSource[@Type='" + DBType + "']";
        //            return GetEntitySQLString(entity, xmlPath);
        //        }

        //        public static XmlNode GetEntityRootNode(string entity) => 
        //            GetEntityXmlDoc(entity)?.SelectSingleNode("Entity");

        //        public static string GetEntitySQLString(string entity, string xmlPath)
        //        {
        //            XmlDocument entityXmlDoc = GetEntityXmlDoc(entity);
        //            if (entityXmlDoc == null)
        //            {
        //                return null;
        //            }
        //            XmlNode node = entityXmlDoc.SelectSingleNode(xmlPath);
        //            if (node == null)
        //            {
        //                return "";
        //            }
        //            return SwitchKeyWord.Convert(node.InnerText);
        //        }

        //        public static string GetEntitySQLString(string entity, XmlType xmlType, string flag)
        //        {
        //            string xmlPath = "Entity/";
        //            string str2 = string.Empty;
        //            switch (xmlType)
        //            {
        //                case XmlType.Grid:
        //                    str2 = "Grid";
        //                    break;

        //                case XmlType.Form:
        //                    str2 = "Form";
        //                    break;

        //                case XmlType.Toolbar:
        //                    str2 = "ToolBar";
        //                    break;

        //                case XmlType.Query:
        //                    str2 = "Query";
        //                    break;

        //                case XmlType.Save:
        //                    str2 = "Save";
        //                    break;

        //                case XmlType.Lookup:
        //                    str2 = "Lookup";
        //                    break;
        //            }
        //            string str4 = xmlPath;
        //            xmlPath = str4 + str2 + "List/" + str2 + "[@Flag='" + flag + "']/DataSource[@Type='" + DBType + "']";
        //            return GetEntitySQLString(entity, xmlPath);
        //        }

        //        public static XmlDocument GetEntityXmlDoc(string entity)
        //        {
        //            XmlDocument document = new XmlDocument();
        //            string path = HttpContext.Current.Server.MapPath(EntityPath + "/" + entity + ".xml");
        //            if (!File.Exists(path))
        //            {
        //                return null;
        //            }
        //            document.Load(path);
        //            return document;
        //        }

        //        public static XmlNode GetEntityXmlNode(string entity, XmlType type) => 
        //            GetEntityXmlNode(entity, type, 0);

        //        public static XmlNode GetEntityXmlNode(string entity, XmlType type, int flag) => 
        //            GetEntityXmlNode(entity, type, flag.ToString());

        //        public static XmlNode GetEntityXmlNode(string entity, XmlType type, string flag)
        //        {
        //            XmlDocument entityXmlDoc = GetEntityXmlDoc(entity);
        //            if (entityXmlDoc == null)
        //            {
        //                return null;
        //            }
        //            XmlNode node = null;
        //            string str = string.Empty;
        //            switch (type)
        //            {
        //                case XmlType.Grid:
        //                    node = entityXmlDoc.SelectSingleNode("Entity/GridList");
        //                    str = "Grid";
        //                    break;

        //                case XmlType.Form:
        //                    node = entityXmlDoc.SelectSingleNode("Entity/FormList");
        //                    str = "Form";
        //                    break;

        //                case XmlType.Toolbar:
        //                    node = entityXmlDoc.SelectSingleNode("Entity/ToolBarList");
        //                    str = "ToolBar";
        //                    break;

        //                case XmlType.Query:
        //                    node = entityXmlDoc.SelectSingleNode("Entity/QueryList");
        //                    str = "Query";
        //                    break;

        //                case XmlType.Save:
        //                    node = entityXmlDoc.SelectSingleNode("Entity/SaveList");
        //                    str = "Save";
        //                    break;

        //                case XmlType.Lookup:
        //                    node = entityXmlDoc.SelectSingleNode("Entity/LookupList");
        //                    str = "Lookup";
        //                    break;
        //            }
        //            XmlNode node2 = null;
        //            if (node != null)
        //            {
        //                node2 = node.SelectSingleNode(str + "[@Flag='" + flag + "']");
        //            }
        //            return node2;
        //        }

        //        public static string GetErrorPage() => 
        //            HttpContext.Current.Application["ErrorPage"]?.ToString();

        //        public static string GetNodeAttributeValue(XmlNode item, string attributeName) => 
        //            GetNodeAttributeValue(item, attributeName, string.Empty);

        //        public static string GetNodeAttributeValue(XmlNode item, string attributeName, string defValue)
        //        {
        //            if (item == null)
        //            {
        //                return defValue;
        //            }
        //            if ((item.Attributes[attributeName] == null) || (item.Attributes[attributeName].Value == ""))
        //            {
        //                return defValue;
        //            }
        //            return item.Attributes[attributeName].Value;
        //        }

        //        public static DataTable GetPagedDataSource(DataTable source, int pageSize, int pageIndex)
        //        {
        //            DataTable table = source.Clone();
        //            int count = source.Rows.Count;
        //            int num2 = (pageIndex - 1) * pageSize;
        //            if (count < num2)
        //            {
        //                num2 = count;
        //            }
        //            if ((num2 + pageSize) > count)
        //            {
        //                pageSize = count - num2;
        //            }
        //            for (int i = 0; i < pageSize; i++)
        //            {
        //                DataRow row = source.Rows[i + num2];
        //                DataRow row2 = table.NewRow();
        //                foreach (DataColumn column in table.Columns)
        //                {
        //                    row2[column] = row[column.ToString()];
        //                }
        //                table.Rows.Add(row2);
        //            }
        //            return table;
        //        }

        //        public static PropertyDescriptor GetPropertyByName=(object p, string name) => 
        //            GetPropertyByName(p, new string[] { name });

        //        public static PropertyDescriptor GetPropertyByName(object p, string[] path)
        //        {
        //            int num;
        //            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(p);
        //            string str = "";
        //            for (num = 0; num < properties.Count; num++)
        //            {
        //                str = str + properties[num].Name + ";";
        //            }
        //            for (num = 0; num < properties.Count; num++)
        //            {
        //                if (properties[num].Name == path[0])
        //                {
        //                    if (path.Length == 1)
        //                    {
        //                        return properties[num];
        //                    }
        //                    return GetChildPropertyByName(properties[num], path, 1);
        //                }
        //            }
        //            return null;
        //        }

        //        public static XmlNode GetSecEntityOtherNode(string entity, string nodeName, string name)
        //        {
        //            XmlDocument document = new XmlDocument();
        //            string path = HttpContext.Current.Server.MapPath(SecEntityPath + "/" + entity + ".xml");
        //            if (!File.Exists(path))
        //            {
        //                return null;
        //            }
        //            document.Load(path);
        //            XmlNode node = null;
        //            node = document.SelectSingleNode("Entity/Other/" + nodeName + "List");
        //            XmlNode node2 = null;
        //            if (node != null)
        //            {
        //                node2 = node.SelectSingleNode(nodeName + "[@Name='" + name + "']");
        //            }
        //            return node2;
        //        }

        //        public static XmlNode GetSecEntityXmlNode(string entity, XmlType type, string flag)
        //        {
        //            XmlDocument document = new XmlDocument();
        //            string path = HttpContext.Current.Server.MapPath(SecEntityPath + "/" + entity + ".xml");
        //            if (!File.Exists(path))
        //            {
        //                return null;
        //            }
        //            document.Load(path);
        //            XmlNode node = null;
        //            string str2 = string.Empty;
        //            switch (type)
        //            {
        //                case XmlType.Grid:
        //                    node = document.SelectSingleNode("Entity/GridList");
        //                    str2 = "Grid";
        //                    break;

        //                case XmlType.Form:
        //                    node = document.SelectSingleNode("Entity/FormList");
        //                    str2 = "Form";
        //                    break;

        //                case XmlType.Toolbar:
        //                    node = document.SelectSingleNode("Entity/ToolBarList");
        //                    str2 = "ToolBar";
        //                    break;

        //                case XmlType.Query:
        //                    node = document.SelectSingleNode("Entity/QueryList");
        //                    str2 = "Query";
        //                    break;

        //                case XmlType.Save:
        //                    node = document.SelectSingleNode("Entity/SaveList");
        //                    str2 = "Save";
        //                    break;

        //                case XmlType.Lookup:
        //                    node = document.SelectSingleNode("Entity/LookupList");
        //                    str2 = "Lookup";
        //                    break;
        //            }
        //            XmlNode node2 = null;
        //            if (node != null)
        //            {
        //                node2 = node.SelectSingleNode(str2 + "[@Flag='" + flag + "']");
        //            }
        //            return node2;
        //        }

        //        public static int GetTextLength(string str)
        //        {
        //            int num = 0;
        //            char[] chArray = str.ToCharArray();
        //            for (int i = 0; i < chArray.Length; i++)
        //            {
        //                if (chArray[i] > '\x00ff')
        //                {
        //                    num += 2;
        //                }
        //                else
        //                {
        //                    num++;
        //                }
        //            }
        //            return num;
        //        }

        //        public static double GetTextWidth(string Text)
        //        {
        //            if (Text == null)
        //            {
        //                return 0.0;
        //            }
        //            double num = 0.0;
        //            string str = new Regex("[一-龥]+").Replace(Text, "");
        //            num = (Text.Length - str.Length) * 12;
        //            return (num + (str.Length * 6));
        //        }

        //        public static double GetTextWidth(string Text, FontInfo fontInfo)
        //        {
        //            if (Text == null)
        //            {
        //                return 0.0;
        //            }
        //            double num = 0.0;
        //            string str = new Regex("[一-龥]+").Replace(Text, "");
        //            double num2 = fontInfo.Size.Unit.Value;
        //            if (fontInfo.Size.IsEmpty)
        //            {
        //                num2 = 12.0;
        //            }
        //            num = (Text.Length - str.Length) * num2;
        //            return (num + ((str.Length * num2) / 2.0));
        //        }

        //        public static string GetURLPara(string url, string paraName)
        //        {
        //            string str = null;
        //            if (url == null)
        //            {
        //                url = "";
        //            }
        //            if ((url.IndexOf("?") > -1) && (url.IndexOf("?") < (url.Length - 1)))
        //            {
        //                string[] strArray = url.Substring(url.IndexOf("?") + 1).Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
        //                for (int i = 0; i < strArray.Length; i++)
        //                {
        //                    if ((strArray[i] != null) && (strArray[i] != ""))
        //                    {
        //                        string[] strArray2 = strArray[i].Split(new char[] { '=' });
        //                        if ((strArray2[0] != null) && (strArray2[0].ToLower() == paraName.ToLower()))
        //                        {
        //                            if (strArray2.Length > 1)
        //                            {
        //                                str = strArray2[1];
        //                            }
        //                            else
        //                            {
        //                                str = "";
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            return str;
        //        }

        //        public static string GetWeekName(DateTime date)
        //        {
        //            switch (date.DayOfWeek)
        //            {
        //                case DayOfWeek.Sunday:
        //                    return "星期日";

        //                case DayOfWeek.Monday:
        //                    return "星期一";

        //                case DayOfWeek.Tuesday:
        //                    return "星期二";

        //                case DayOfWeek.Wednesday:
        //                    return "星期三";

        //                case DayOfWeek.Thursday:
        //                    return "星期四";

        //                case DayOfWeek.Friday:
        //                    return "星期五";

        //                case DayOfWeek.Saturday:
        //                    return "星期六";
        //            }
        //            return "";
        //        }

        //        public static string GetWeekName(string date)
        //        {
        //            DateTime now = DateTime.Now;
        //            if (DateTime.TryParse(date, out now))
        //            {
        //                return GetWeekName(now);
        //            }
        //            return GetWeekName(DateTime.Now);
        //        }

        //        public static string HCDATA =(string txt) => 
        //            ("<!--[CDATA[" + txt + "]]-->");

        //        public static bool IsChar(Type dataType)
        //        {
        //            switch (Type.GetTypeCode(dataType))
        //            {
        //                case TypeCode.Int16:
        //                case TypeCode.UInt16:
        //                case TypeCode.Int32:
        //                case TypeCode.UInt32:
        //                case TypeCode.Int64:
        //                case TypeCode.UInt64:
        //                case TypeCode.Single:
        //                case TypeCode.Double:
        //                case TypeCode.Decimal:
        //                    return false;
        //            }
        //            return true;
        //        }

        //        public static string Random(string forward, int length)
        //        {
        //            string str = string.Empty;
        //            for (int i = length; i >= 0; i -= 0x20)
        //            {
        //                str = str + Guid.NewGuid().ToString().Replace("-", "");
        //            }
        //            return (forward + str.Substring(0, length));
        //        }

        //        public static DataTable RCTransform(DataTable source, string keyId, string[] freeze) => 
        //            RCTransform(source, keyId, freeze, false);

        //        public static DataTable RCTransform(DataTable source, string keyId, string[] freeze, bool distinct)
        //        {
        //            int num;
        //            DataView defaultView = source.DefaultView;
        //            DataTable table = defaultView.ToTable(distinct, new string[] { keyId });
        //            DataTable table2 = new DataTable();
        //            DataRow row = null;
        //            DataRow[] rowArray = null;
        //            string[] strArray = new string[table.Rows.Count];
        //            for (num = 0; num < freeze.Length; num++)
        //            {
        //                table2.Columns.Add(new DataColumn(freeze[num]));
        //            }
        //            for (num = 0; num < table.Rows.Count; num++)
        //            {
        //                strArray[num] = table.Rows[num][0].ToString();
        //                table2.Columns.Add(new DataColumn(strArray[num]));
        //            }
        //            DataTable table4 = defaultView.ToTable(true, freeze);
        //            for (num = 0; num < table4.Rows.Count; num++)
        //            {
        //                row = table2.NewRow();
        //                for (int i = 0; i < freeze.Length; i++)
        //                {
        //                    row[freeze[i]] = table4.Rows[num][freeze[i]];
        //                }
        //                table2.Rows.Add(row);
        //            }
        //            string filterExpression = string.Empty;
        //            for (num = 0; num < strArray.Length; num++)
        //            {
        //                if (IsChar(source.Columns[keyId].DataType))
        //                {
        //                    rowArray = source.Select(keyId + "='" + strArray[num] + "'");
        //                }
        //                else
        //                {
        //                    rowArray = source.Select(keyId + "=" + strArray[num]);
        //                }
        //                for (int j = 0; j < rowArray.Length; j++)
        //                {
        //                    row = null;
        //                    filterExpression = "";
        //                    for (int k = 0; k < freeze.Length; k++)
        //                    {
        //                        string str2;
        //                        if (IsChar(table2.Columns[freeze[k]].DataType))
        //                        {
        //                            str2 = filterExpression;
        //                            filterExpression = str2 + " " + freeze[k] + "='" + rowArray[j][freeze[k]].ToString() + "'";
        //                        }
        //                        else
        //                        {
        //                            str2 = filterExpression;
        //                            filterExpression = str2 + " " + freeze[k] + "=" + rowArray[j][freeze[k]].ToString();
        //                        }
        //                        if (freeze.Length != (k + 1))
        //                        {
        //                            filterExpression = filterExpression + " And ";
        //                        }
        //                    }
        //                    DataRow[] rowArray2 = table2.Select(filterExpression);
        //                    if (rowArray2.Length > 0)
        //                    {
        //                        row = rowArray2[0];
        //                        for (int m = 0; m < source.Columns.Count; m++)
        //                        {
        //                            if ((table2.Columns[source.Columns[m].ColumnName] == null) && (source.Columns[m].ColumnName.ToLower() != keyId.ToLower()))
        //                            {
        //                                row[strArray[num]] = rowArray[j][source.Columns[m].ColumnName];
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            row = null;
        //            table = null;
        //            defaultView = null;
        //            return table2;
        //        }

        //        public static string ReadFile(string fileName)
        //        {
        //            string path = HttpContext.Current.Server.MapPath(StartPath + fileName);
        //            string str2 = "";
        //            StreamReader reader = new StreamReader(path);
        //            str2 = str2 = reader.ReadToEnd();
        //            reader.Close();
        //            return str2;
        //        }

        //        public static string ReadFileByEncode(string fileName)
        //        {
        //            FileStream stream = new FileStream(HttpContext.Current.Server.MapPath(StartPath + fileName), FileMode.OpenOrCreate);
        //            byte[] buffer = new byte[stream.Length];
        //            stream.Read(buffer, 0, buffer.Length);
        //            stream.Close();
        //            stream.Dispose();
        //            string str2 = "";
        //            try
        //            {
        //                str2 = Encoding.Unicode.GetString(buffer);
        //            }
        //            catch
        //            {
        //            }
        //            return str2;
        //        }

        //        public static byte[] ReadFileBytes(string fileName)
        //        {
        //            FileStream stream = new FileStream(HttpContext.Current.Server.MapPath(StartPath + fileName), FileMode.OpenOrCreate);
        //            byte[] buffer = new byte[stream.Length];
        //            stream.Read(buffer, 0, buffer.Length);
        //            stream.Close();
        //            stream.Dispose();
        //            return buffer;
        //        }

        //        public static void RenderLiteralToControl(string source, Control control)
        //        {
        //            int num = 0;
        //            for (int i = 0; i < control.Controls.Count; i++)
        //            {
        //                try
        //                {
        //                    if ((control.Controls[i].GetType().Name == "Literal") && (((Literal) control.Controls[i]).Text.Trim() == source.Trim()))
        //                    {
        //                        num = 1;
        //                        break;
        //                    }
        //                }
        //                catch
        //                {
        //                }
        //            }
        //            if (num == 0)
        //            {
        //                Literal child = new Literal {
        //                    Text = source
        //                };
        //                control.Controls.Add(child);
        //            }
        //        }

        //        public static void RenderLiteralToHeader(string source, Page page)
        //        {
        //            int num = 0;
        //            for (int i = 0; i < page.Header.Controls.Count; i++)
        //            {
        //                try
        //                {
        //                    if ((page.Header.Controls[i].GetType().Name == "Literal") && (((Literal) page.Header.Controls[i]).Text.Trim() == source.Trim()))
        //                    {
        //                        num = 1;
        //                        break;
        //                    }
        //                }
        //                catch
        //                {
        //                }
        //            }
        //            if (num == 0)
        //            {
        //                Literal child = new Literal {
        //                    Text = source
        //                };
        //                page.Header.Controls.Add(child);
        //            }
        //        }

        //        public static void RenderLiteralToPage(string source, Page page)
        //        {
        //            int num = 0;
        //            for (int i = 0; i < page.Controls.Count; i++)
        //            {
        //                try
        //                {
        //                    if ((page.Controls[i].GetType().Name == "Literal") && (((Literal) page.Controls[i]).Text.Trim() == source.Trim()))
        //                    {
        //                        num = 1;
        //                        break;
        //                    }
        //                }
        //                catch
        //                {
        //                }
        //            }
        //            if (num == 0)
        //            {
        //                Literal child = new Literal {
        //                    Text = source
        //                };
        //                page.Controls.Add(child);
        //            }
        //        }

        //        public static string RenderXMLBlock(string id, string xml) => 
        //            ("<xml id="" + id + "">" + HCDATA(xml) + "</xml>");

        //        public static string ReplaceForDtSelect(string source)
        //        {
        //            if (string.IsNullOrEmpty(source))
        //            {
        //                return source;
        //            }
        //            Regex regex = new Regex("( ('%))((\\S|\\s)*?)(%'( |\\)|$){1})");
        //            if (AD8wVnlvXOE2UKMYlE == null)
        //            {
        //                AD8wVnlvXOE2UKMYlE = new MatchEvaluator(General.ABuGXxneo6DAkrWJV4);
        //            }
        //            return regex.Replace(source, AD8wVnlvXOE2UKMYlE);
        //        }

        //        public static string ReplaceForJSON(string source)
        //        {
        //            Regex regex = new Regex("[\\<\\>"&\\r\\n]");
        //            if (AF_r3_UDSgdvZBKer4 == null)
        //            {
        //                AF_r3_UDSgdvZBKer4 = new MatchEvaluator(General.AD8wVnlvXOE2UKMYlE);
        //            }
        //            source = regex.Replace(source, AF_r3_UDSgdvZBKer4);
        //            source = source.Replace("", "<br>");
        //            return source;
        //        }

        //        public static string ReplaceForKeys(string source, Hashtable Keys)
        //        {
        //            Wima.Common.General.ACAVEfDt_JCsoeqSyb syb = new Wima.Common.General.ACAVEfDt_JCsoeqSyb {
        //                AA1YNisqZ2Po240AFws2Y4MH25SW = Keys
        //            };
        //            if (string.IsNullOrEmpty(source))
        //            {
        //                return source;
        //            }
        //            Regex regex = new Regex("(\\{@Key\\[)(')*[\\S\\s]+?(')*(\\]\\})");
        //            return regex.Replace(source, new MatchEvaluator(syb.AA1YNisqZ2Po240AFws2Y4MH25SW));
        //        }

        //        public static string ReplaceForXML(string source)
        //        {
        //            if (string.IsNullOrEmpty(source))
        //            {
        //                return source;
        //            }
        //            Regex regex = new Regex("[\\<\\>"&]");
        //            if (ACAVEfDt_JCsoeqSyb == null)
        //            {
        //                ACAVEfDt_JCsoeqSyb = new MatchEvaluator(General.AA1YNisqZ2Po240AFws2Y4MH25SW);
        //            }
        //            return regex.Replace(source, ACAVEfDt_JCsoeqSyb);
        //        }

        //        public static string RequestGetName(string name, string baseUrl)
        //        {
        //            string str = "";
        //            if (string.IsNullOrEmpty(baseUrl))
        //            {
        //                baseUrl = HttpContext.Current.Request.Url.ToString();
        //            }
        //            if ((baseUrl.IndexOf("?") > -1) && (str == ""))
        //            {
        //                str = baseUrl.Substring(baseUrl.IndexOf("?") + 1);
        //            }
        //            if (str != "")
        //            {
        //                string[] strArray = str.Split(new string[] { "&" }, StringSplitOptions.None);
        //                for (int i = 0; i < strArray.Length; i++)
        //                {
        //                    if (strArray[i] != "")
        //                    {
        //                        string[] strArray2 = strArray[i].Split(new string[] { "=" }, StringSplitOptions.None);
        //                        if (strArray2.Length != 0)
        //                        {
        //                            string str3 = strArray2[0];
        //                            if ((str3 != "1") && (str3 != ""))
        //                            {
        //                                string s = "";
        //                                if (strArray2.Length > 1)
        //                                {
        //                                    s = strArray2[1];
        //                                }
        //                                if (str3.ToLower() == name.ToLower())
        //                                {
        //                                    return HttpContext.Current.Server.UrlDecode(s);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            return null;
        //        }

        //        public static string RequestSetName(string name, string value, string baseUrl) => 
        //            RequestSetName(name, value, baseUrl, false);

        //        public static string RequestSetName(string name, string value, string baseUrl, bool hasEncode)
        //        {
        //            string str7;
        //            if (hasEncode)
        //            {
        //                value = HttpContext.Current.Server.UrlEncode(HttpContext.Current.Server.UrlDecode(value));
        //            }
        //            string str = "";
        //            string str2 = "";
        //            if (string.IsNullOrEmpty(baseUrl))
        //            {
        //                baseUrl = HttpContext.Current.Request.Url.ToString();
        //            }
        //            if (baseUrl.IndexOf("?") == -1)
        //            {
        //                str2 = baseUrl;
        //            }
        //            else
        //            {
        //                str2 = baseUrl.Substring(0, baseUrl.IndexOf("?"));
        //                if (str == "")
        //                {
        //                    str = baseUrl.Substring(baseUrl.IndexOf("?") + 1);
        //                }
        //            }
        //            str2 = str2 + "?1=1";
        //            bool flag = false;
        //            if (str != "")
        //            {
        //                string[] strArray = str.Split(new string[] { "&" }, StringSplitOptions.None);
        //                for (int i = 0; i < strArray.Length; i++)
        //                {
        //                    if (strArray[i] != "")
        //                    {
        //                        string[] strArray2 = strArray[i].Split(new string[] { "=" }, StringSplitOptions.None);
        //                        if (strArray2.Length != 0)
        //                        {
        //                            string str3 = strArray2[0];
        //                            if ((str3 != "1") && (str3 != ""))
        //                            {
        //                                string str4 = "";
        //                                if (strArray2.Length > 1)
        //                                {
        //                                    str4 = strArray2[1];
        //                                }
        //                                if (str3.ToLower() == name.ToLower())
        //                                {
        //                                    if ((name != "1") && (name != ""))
        //                                    {
        //                                        str7 = str2;
        //                                        str2 = str7 + "&" + name + "=" + value;
        //                                    }
        //                                    flag = true;
        //                                }
        //                                else
        //                                {
        //                                    string s = str4;
        //                                    if (hasEncode)
        //                                    {
        //                                        s = HttpContext.Current.Server.UrlEncode(HttpContext.Current.Server.UrlDecode(s));
        //                                    }
        //                                    str7 = str2;
        //                                    str2 = str7 + "&" + str3 + "=" + s;
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            if (!flag)
        //            {
        //                str7 = str2;
        //                str2 = str7 + "&" + name + "=" + value;
        //            }
        //            return str2;
        //        }

        //        public static string Serialize(Type type, object value)
        //        {
        //            XmlSerializer serializer = new XmlSerializer(type);
        //            StringBuilder sb = new StringBuilder();
        //            StringWriter writer = new StringWriter(sb);
        //            serializer.Serialize((TextWriter) writer, value);
        //            writer.Close();
        //            writer.Dispose();
        //            return sb.ToString();
        //        }

        //        public static void Serialize(Type type, object value, string path)
        //        {
        //            if (File.Exists(path))
        //            {
        //                File.Delete(path);
        //            }
        //            FileStream stream = File.Create(path);
        //            string s = Serialize(type, value);
        //            byte[] bytes = Encoding.UTF8.GetBytes(s);
        //            stream.Write(bytes, 0, bytes.Length);
        //            stream.Close();
        //            stream.Dispose();
        //        }

        //        public static string SetURLPara(string url, string paraName, string paraValue)
        //        {
        //            string str4;
        //            string str = "";
        //            if (url == null)
        //            {
        //                url = "";
        //            }
        //            if (!string.IsNullOrEmpty(paraValue))
        //            {
        //                paraValue = HttpContext.Current.Server.UrlEncode(HttpContext.Current.Server.UrlDecode(paraValue));
        //            }
        //            if (url.IndexOf("?") == -1)
        //            {
        //                str4 = str;
        //                return (str4 + url + "?" + paraName + "=" + paraValue);
        //            }
        //            if (url.IndexOf("?") == (url.Length - 1))
        //            {
        //                str4 = str;
        //                return (str4 + url + paraName + "=" + paraValue);
        //            }
        //            str = url.Substring(0, url.IndexOf("?") + 1) + "1=1";
        //            string[] strArray = url.Substring(url.IndexOf("?") + 1).Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
        //            bool flag = false;
        //            for (int i = 0; i < strArray.Length; i++)
        //            {
        //                if (((strArray[i] != null) && (strArray[i] != "")) && (strArray[i] != "1=1"))
        //                {
        //                    string[] strArray2 = strArray[i].Split(new char[] { '=' });
        //                    if ((strArray2.Length != 0) && (strArray2[0] != "1"))
        //                    {
        //                        if ((strArray2[0] != null) && (strArray2[0].ToLower() == paraName.ToLower()))
        //                        {
        //                            if ((paraName != "1") && (paraName != ""))
        //                            {
        //                                str4 = str;
        //                                str = str4 + "&" + strArray2[0] + "=" + paraValue;
        //                            }
        //                            flag = true;
        //                        }
        //                        else
        //                        {
        //                            string s = "";
        //                            if (strArray[i].Length > 1)
        //                            {
        //                                s = strArray2[1];
        //                                s = HttpContext.Current.Server.UrlEncode(HttpContext.Current.Server.UrlDecode(s));
        //                            }
        //                            str4 = str;
        //                            str = str4 + "&" + strArray2[0] + "=" + s;
        //                        }
        //                    }
        //                }
        //            }
        //            if (!flag)
        //            {
        //                str4 = str;
        //                str = str4 + "&" + paraName + "=" + paraValue;
        //            }
        //            return str;
        //        }

        //        public static bool ToBool(object value, bool defValue) => 
        //            ToDefine<bool>(value, defValue);

        //        public static bool ToBool(string value, bool defValue)
        //        {
        //            if (value == null)
        //            {
        //                return defValue;
        //            }
        //            return ((value == "1") || (value.ToLower() == "true"));
        //        }

        //        public static T ToDefine<T>(object value, T defValue)
        //        {
        //            if (value == null)
        //            {
        //                return defValue;
        //            }
        //            if ((defValue != null) && ((defValue.GetType().Name == "Boolean") || (defValue.GetType().Name == "bool")))
        //            {
        //                if (value.ToString() == "")
        //                {
        //                    return defValue;
        //                }
        //                string str = value.ToString().ToLower();
        //                if ((str != null) && (((str == "1") || (str == "y")) || ((str == "yes") || (str == "true"))))
        //                {
        //                    value = "true";
        //                }
        //                else
        //                {
        //                    value = "false";
        //                }
        //            }
        //            T local = default(T);
        //            Type[] types = new Type[] { Type.GetType("System.String"), Type.GetType(typeof(T).FullName + "&") };
        //            object[] parameters = new object[] { value.ToString(), local };
        //            if (!((bool) typeof(T).GetMethod("TryParse", types).Invoke(null, parameters)))
        //            {
        //                local = defValue;
        //            }
        //            else
        //            {
        //                local = (T) parameters[1];
        //            }
        //            return local;
        //        }

        //        public static object ToEnum(object value, Type type)
        //        {
        //            if (value == null)
        //            {
        //                return null;
        //            }
        //            return Enum.Parse(type, value.ToString());
        //        }

        //        public static int ToInt32(object value, int defValue) => 
        //            ToDefine<int>(value, defValue);

        //        public static Unit ToUnit(object value, Unit defValue)
        //        {
        //            if (value == null)
        //            {
        //                return defValue;
        //            }
        //            try
        //            {
        //                return Unit.Parse(value.ToString());
        //            }
        //            catch
        //            {
        //                return Unit.Empty;
        //            }
        //        }

        //        public static string TypeTransform(string type)
        //        {
        //            string str2 = type.ToLower();
        //            if (str2 != null)
        //            {
        //                if (str2 == "varchar")
        //                {
        //                    return "ed";
        //                }
        //                if (str2 == "money")
        //                {
        //                    return "price";
        //                }
        //                if (str2 == "datetime")
        //                {
        //                    return "calendar";
        //                }
        //                if (str2 == "double")
        //                {
        //                    return "dyn";
        //                }
        //            }
        //            return "ro";
        //        }

        //        public static string VattingForXML(string source)
        //        {
        //            Regex regex = new Regex("(&gt;)|(&lt;)|(&qout;)|(&amp;)");
        //            if (AEIRlK3EAPtY8VyHZY == null)
        //            {
        //                AEIRlK3EAPtY8VyHZY = new MatchEvaluator(General.ACAVEfDt_JCsoeqSyb);
        //            }
        //            return regex.Replace(source, AEIRlK3EAPtY8VyHZY);
        //        }

        //        public static string XmlAttribute(string name, string value) => 
        //            (name + "="" + value + "" ");

        //        public static DataSet XmlToDataSet(string xmlStr)
        //        {
        //            if (!string.IsNullOrEmpty(xmlStr))
        //            {
        //                StringReader input = null;
        //                XmlTextReader reader = null;
        //                try
        //                {
        //                    DataSet set = new DataSet();
        //                    input = new StringReader(xmlStr);
        //                    reader = new XmlTextReader(input);
        //                    set.ReadXml(reader);
        //                    return set;
        //                }
        //                catch (Exception exception)
        //                {
        //                    throw exception;
        //                }
        //                finally
        //                {
        //                    if (reader != null)
        //                    {
        //                        reader.Close();
        //                        input.Close();
        //                        input.Dispose();
        //                    }
        //                }
        //            }
        //            return null;
        //        }

        //        public static string DBType =>
        //            ConfigurationManager.AppSettings["DBType"];

        //        public static string EntityPath
        //        {
        //            get
        //            {
        //                string str = string.Empty;
        //                if (ConfigurationManager.AppSettings["EntityPath"] == null)
        //                {
        //                    return string.Empty;
        //                }
        //                str = ConfigurationManager.AppSettings["EntityPath"].ToString();
        //                return (StartPath + "/" + str);
        //            }
        //        }

        //        public static string ImageFolder
        //        {
        //            get
        //            {
        //                string str = string.Empty;
        //                if (ConfigurationManager.AppSettings["ImagePath"] == null)
        //                {
        //                    return string.Empty;
        //                }
        //                str = ConfigurationManager.AppSettings["ImagePath"].ToString();
        //                return ("/" + str);
        //            }
        //        }

        //        public static string ImagePath
        //        {
        //            get
        //            {
        //                string str = string.Empty;
        //                if (ConfigurationManager.AppSettings["ImagePath"] == null)
        //                {
        //                    return string.Empty;
        //                }
        //                str = ConfigurationManager.AppSettings["ImagePath"].ToString();
        //                return (StartPath + "/" + str);
        //            }
        //        }

        //        public static bool IsPagePostBack
        //        {
        //            get
        //            {
        //                bool flag = false;
        //                if (HttpContext.Current.Request.Url.ToString().ToLower().IndexOf(".asmx") > -1)
        //                {
        //                    flag = true;
        //                }
        //                return flag;
        //            }
        //        }

        public static string ProjectName
        {
            get
            {
                return HttpContext.Current.Application["ProjectName"].ToString();
            }
        }

        //        public static string SecEntityPath
        //        {
        //            get
        //            {
        //                string str = string.Empty;
        //                str = "SecondDeploy/Entity";
        //                return (StartPath + "/" + str);
        //            }
        //        }

        //        public static string StartPath =>
        //            ProjectName;

        //        [CompilerGenerated]
        //        private sealed class ABuGXxneo6DAkrWJV4
        //        {
        //            public Match AA1YNisqZ2Po240AFws2Y4MH25SW;

        //            public string AA1YNisqZ2Po240AFws2Y4MH25SW(Match match1)
        //            {
        //                string str2 = match1.ToString();
        //                if (str2 != null)
        //                {
        //                    if (str2 == "[")
        //                    {
        //                        return "[[]";
        //                    }
        //                    if (str2 == "]")
        //                    {
        //                        return "[]]";
        //                    }
        //                    if (str2 == "%")
        //                    {
        //                        return "[%]";
        //                    }
        //                    if (str2 == "*")
        //                    {
        //                        return "[*]";
        //                    }
        //                }
        //                return this.AA1YNisqZ2Po240AFws2Y4MH25SW.ToString();
        //            }
        //        }

        //        [CompilerGenerated]
        //        private sealed class ACAVEfDt_JCsoeqSyb
        //        {
        //            public Hashtable AA1YNisqZ2Po240AFws2Y4MH25SW;

        //            public string AA1YNisqZ2Po240AFws2Y4MH25SW(Match match1)
        //            {
        //                string str = Regex.Replace(Regex.Match(match1.Value, "(\\[)(')*[\\S\\s]+?(')*(\\])").Value, "[\\['\\]]+", "", RegexOptions.IgnoreCase);
        //                if (this.AA1YNisqZ2Po240AFws2Y4MH25SW[str] != null)
        //                {
        //                    return this.AA1YNisqZ2Po240AFws2Y4MH25SW[str].ToString();
        //                }
        //                return "";
        //            }
        //        }
        //        public enum XmlType
        //        {
        //            Grid,
        //            Form,
        //            Toolbar,
        //            Query,
        //            Save,
        //            Lookup,
        //            Other
        //        }
        //    }
        //}.Replace(source, ACAVEfDt_JCsoeqSyb);
        //        }
        //        public static string RequestGetName(string name, string baseUrl)
        //        {
        //            string str = "";
        //            if (string.IsNullOrEmpty(baseUrl))
        //            {
        //                baseUrl = HttpContext.Current.Request.Url.ToString();
        //            }
        //            if ((baseUrl.IndexOf("?") > -1) && (str == ""))
        //            {
        //                str = baseUrl.Substring(baseUrl.IndexOf("?") + 1);
        //            }
        //            if (str != "")
        //            {
        //                string[] strArray = str.Split(new string[] { "&" }, StringSplitOptions.None);
        //                for (int i = 0; i < strArray.Length; i++)
        //                {
        //                    if (strArray[i] != "")
        //                    {
        //                        string[] strArray2 = strArray[i].Split(new string[] { "=" }, StringSplitOptions.None);
        //                        if (strArray2.Length != 0)
        //                        {
        //                            string str3 = strArray2[0];
        //                            if ((str3 != "1") && (str3 != ""))
        //                            {
        //                                string s = "";
        //                                if (strArray2.Length > 1)
        //                                {
        //                                    s = strArray2[1];
        //                                }
        //                                if (str3.ToLower() == name.ToLower())
        //                                {
        //                                    return HttpContext.Current.Server.UrlDecode(s);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            return null;
        //        }

        //        public static string RequestSetName(string name, string value, string baseUrl) => 
        //            RequestSetName(name, value, baseUrl, false);

        //        public static string RequestSetName(string name, string value, string baseUrl, bool hasEncode)
        //        {
        //            string str7;
        //            if (hasEncode)
        //            {
        //                value = HttpContext.Current.Server.UrlEncode(HttpContext.Current.Server.UrlDecode(value));
        //            }
        //            string str = "";
        //            string str2 = "";
        //            if (string.IsNullOrEmpty(baseUrl))
        //            {
        //                baseUrl = HttpContext.Current.Request.Url.ToString();
        //            }
        //            if (baseUrl.IndexOf("?") == -1)
        //            {
        //                str2 = baseUrl;
        //            }
        //            else
        //            {
        //                str2 = baseUrl.Substring(0, baseUrl.IndexOf("?"));
        //                if (str == "")
        //                {
        //                    str = baseUrl.Substring(baseUrl.IndexOf("?") + 1);
        //                }
        //            }
        //            str2 = str2 + "?1=1";
        //            bool flag = false;
        //            if (str != "")
        //            {
        //                string[] strArray = str.Split(new string[] { "&" }, StringSplitOptions.None);
        //                for (int i = 0; i < strArray.Length; i++)
        //                {
        //                    if (strArray[i] != "")
        //                    {
        //                        string[] strArray2 = strArray[i].Split(new string[] { "=" }, StringSplitOptions.None);
        //                        if (strArray2.Length != 0)
        //                        {
        //                            string str3 = strArray2[0];
        //                            if ((str3 != "1") && (str3 != ""))
        //                            {
        //                                string str4 = "";
        //                                if (strArray2.Length > 1)
        //                                {
        //                                    str4 = strArray2[1];
        //                                }
        //                                if (str3.ToLower() == name.ToLower())
        //                                {
        //                                    if ((name != "1") && (name != ""))
        //                                    {
        //                                        str7 = str2;
        //                                        str2 = str7 + "&" + name + "=" + value;
        //                                    }
        //                                    flag = true;
        //                                }
        //                                else
        //                                {
        //                                    string s = str4;
        //                                    if (hasEncode)
        //                                    {
        //                                        s = HttpContext.Current.Server.UrlEncode(HttpContext.Current.Server.UrlDecode(s));
        //                                    }
        //                                    str7 = str2;
        //                                    str2 = str7 + "&" + str3 + "=" + s;
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            if (!flag)
        //            {
        //                str7 = str2;
        //                str2 = str7 + "&" + name + "=" + value;
        //            }
        //            return str2;
        //        }

        //        public static string Serialize(Type type, object value)
        //        {
        //            XmlSerializer serializer = new XmlSerializer(type);
        //            StringBuilder sb = new StringBuilder();
        //            StringWriter writer = new StringWriter(sb);
        //            serializer.Serialize((TextWriter) writer, value);
        //            writer.Close();
        //            writer.Dispose();
        //            return sb.ToString();
        //        }

        //        public static void Serialize(Type type, object value, string path)
        //        {
        //            if (File.Exists(path))
        //            {
        //                File.Delete(path);
        //            }
        //            FileStream stream = File.Create(path);
        //            string s = Serialize(type, value);
        //            byte[] bytes = Encoding.UTF8.GetBytes(s);
        //            stream.Write(bytes, 0, bytes.Length);
        //            stream.Close();
        //            stream.Dispose();
        //        }

        //        public static string SetURLPara(string url, string paraName, string paraValue)
        //        {
        //            string str4;
        //            string str = "";
        //            if (url == null)
        //            {
        //                url = "";
        //            }
        //            if (!string.IsNullOrEmpty(paraValue))
        //            {
        //                paraValue = HttpContext.Current.Server.UrlEncode(HttpContext.Current.Server.UrlDecode(paraValue));
        //            }
        //            if (url.IndexOf("?") == -1)
        //            {
        //                str4 = str;
        //                return (str4 + url + "?" + paraName + "=" + paraValue);
        //            }
        //            if (url.IndexOf("?") == (url.Length - 1))
        //            {
        //                str4 = str;
        //                return (str4 + url + paraName + "=" + paraValue);
        //            }
        //            str = url.Substring(0, url.IndexOf("?") + 1) + "1=1";
        //            string[] strArray = url.Substring(url.IndexOf("?") + 1).Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
        //            bool flag = false;
        //            for (int i = 0; i < strArray.Length; i++)
        //            {
        //                if (((strArray[i] != null) && (strArray[i] != "")) && (strArray[i] != "1=1"))
        //                {
        //                    string[] strArray2 = strArray[i].Split(new char[] { '=' });
        //                    if ((strArray2.Length != 0) && (strArray2[0] != "1"))
        //                    {
        //                        if ((strArray2[0] != null) && (strArray2[0].ToLower() == paraName.ToLower()))
        //                        {
        //                            if ((paraName != "1") && (paraName != ""))
        //                            {
        //                                str4 = str;
        //                                str = str4 + "&" + strArray2[0] + "=" + paraValue;
        //                            }
        //                            flag = true;
        //                        }
        //                        else
        //                        {
        //                            string s = "";
        //                            if (strArray[i].Length > 1)
        //                            {
        //                                s = strArray2[1];
        //                                s = HttpContext.Current.Server.UrlEncode(HttpContext.Current.Server.UrlDecode(s));
        //                            }
        //                            str4 = str;
        //                            str = str4 + "&" + strArray2[0] + "=" + s;
        //                        }
        //                    }
        //                }
        //            }
        //            if (!flag)
        //            {
        //                str4 = str;
        //                str = str4 + "&" + paraName + "=" + paraValue;
        //            }
        //            return str;
        //        }

        //        public static bool ToBool(object value, bool defValue) => 
        //            ToDefine<bool>(value, defValue);

        //        public static bool ToBool(string value, bool defValue)
        //        {
        //            if (value == null)
        //            {
        //                return defValue;
        //            }
        //            return ((value == "1") || (value.ToLower() == "true"));
        //        }

        //        public static T ToDefine<T>(object value, T defValue)
        //        {
        //            if (value == null)
        //            {
        //                return defValue;
        //            }
        //            if ((defValue != null) && ((defValue.GetType().Name == "Boolean") || (defValue.GetType().Name == "bool")))
        //            {
        //                if (value.ToString() == "")
        //                {
        //                    return defValue;
        //                }
        //                string str = value.ToString().ToLower();
        //                if ((str != null) && (((str == "1") || (str == "y")) || ((str == "yes") || (str == "true"))))
        //                {
        //                    value = "true";
        //                }
        //                else
        //                {
        //                    value = "false";
        //                }
        //            }
        //            T local = default(T);
        //            Type[] types = new Type[] { Type.GetType("System.String"), Type.GetType(typeof(T).FullName + "&") };
        //            object[] parameters = new object[] { value.ToString(), local };
        //            if (!((bool) typeof(T).GetMethod("TryParse", types).Invoke(null, parameters)))
        //            {
        //                local = defValue;
        //            }
        //            else
        //            {
        //                local = (T) parameters[1];
        //            }
        //            return local;
        //        }

        //        public static object ToEnum(object value, Type type)
        //        {
        //            if (value == null)
        //            {
        //                return null;
        //            }
        //            return Enum.Parse(type, value.ToString());
        //        }

        //        public static int ToInt32(object value, int defValue) => 
        //            ToDefine<int>(value, defValue);

        //        public static Unit ToUnit(object value, Unit defValue)
        //        {
        //            if (value == null)
        //            {
        //                return defValue;
        //            }
        //            try
        //            {
        //                return Unit.Parse(value.ToString());
        //            }
        //            catch
        //            {
        //                return Unit.Empty;
        //            }
        //        }

        //        public static string TypeTransform(string type)
        //        {
        //            string str2 = type.ToLower();
        //            if (str2 != null)
        //            {
        //                if (str2 == "varchar")
        //                {
        //                    return "ed";
        //                }
        //                if (str2 == "money")
        //                {
        //                    return "price";
        //                }
        //                if (str2 == "datetime")
        //                {
        //                    return "calendar";
        //                }
        //                if (str2 == "double")
        //                {
        //                    return "dyn";
        //                }
        //            }
        //            return "ro";
        //        }

        //        public static string VattingForXML(string source)
        //        {
        //            Regex regex = new Regex("(&gt;)|(&lt;)|(&qout;)|(&amp;)");
        //            if (AEIRlK3EAPtY8VyHZY == null)
        //            {
        //                AEIRlK3EAPtY8VyHZY = new MatchEvaluator(General.ACAVEfDt_JCsoeqSyb);
        //            }
        //            return regex.Replace(source, AEIRlK3EAPtY8VyHZY);
        //        }

        //        public static string XmlAttribute(string name, string value) => 
        //            (name + "="" + value + "" ");

        //        public static DataSet XmlToDataSet(string xmlStr)
        //        {
        //            if (!string.IsNullOrEmpty(xmlStr))
        //            {
        //                StringReader input = null;
        //                XmlTextReader reader = null;
        //                try
        //                {
        //                    DataSet set = new DataSet();
        //                    input = new StringReader(xmlStr);
        //                    reader = new XmlTextReader(input);
        //                    set.ReadXml(reader);
        //                    return set;
        //                }
        //                catch (Exception exception)
        //                {
        //                    throw exception;
        //                }
        //                finally
        //                {
        //                    if (reader != null)
        //                    {
        //                        reader.Close();
        //                        input.Close();
        //                        input.Dispose();
        //                    }
        //                }
        //            }
        //            return null;
        //        }

        //        public static string DBType =>
        //            ConfigurationManager.AppSettings["DBType"];

        //        public static string EntityPath
        //        {
        //            get
        //            {
        //                string str = string.Empty;
        //                if (ConfigurationManager.AppSettings["EntityPath"] == null)
        //                {
        //                    return string.Empty;
        //                }
        //                str = ConfigurationManager.AppSettings["EntityPath"].ToString();
        //                return (StartPath + "/" + str);
        //            }
        //        }

        //        public static string ImageFolder
        //        {
        //            get
        //            {
        //                string str = string.Empty;
        //                if (ConfigurationManager.AppSettings["ImagePath"] == null)
        //                {
        //                    return string.Empty;
        //                }
        //                str = ConfigurationManager.AppSettings["ImagePath"].ToString();
        //                return ("/" + str);
        //            }
        //        }

        //        public static string ImagePath
        //        {
        //            get
        //            {
        //                string str = string.Empty;
        //                if (ConfigurationManager.AppSettings["ImagePath"] == null)
        //                {
        //                    return string.Empty;
        //                }
        //                str = ConfigurationManager.AppSettings["ImagePath"].ToString();
        //                return (StartPath + "/" + str);
        //            }
        //        }

        //        public static bool IsPagePostBack
        //        {
        //            get
        //            {
        //                bool flag = false;
        //                if (HttpContext.Current.Request.Url.ToString().ToLower().IndexOf(".asmx") > -1)
        //                {
        //                    flag = true;
        //                }
        //                return flag;
        //            }
        //        }

        //        public static string ProjectName =>
        //            HttpContext.Current.Application["ProjectName"]?.ToString();

        //        public static string SecEntityPath
        //        {
        //            get
        //            {
        //                string str = string.Empty;
        //                str = "SecondDeploy/Entity";
        //                return (StartPath + "/" + str);
        //            }
        //        }

        //        public static string StartPath =>
        //            ProjectName;

        //        [CompilerGenerated]
        //        private sealed class ABuGXxneo6DAkrWJV4
        //        {
        //            public Match AA1YNisqZ2Po240AFws2Y4MH25SW;

        //            public string AA1YNisqZ2Po240AFws2Y4MH25SW(Match match1)
        //            {
        //                string str2 = match1.ToString();
        //                if (str2 != null)
        //                {
        //                    if (str2 == "[")
        //                    {
        //                        return "[[]";
        //                    }
        //                    if (str2 == "]")
        //                    {
        //                        return "[]]";
        //                    }
        //                    if (str2 == "%")
        //                    {
        //                        return "[%]";
        //                    }
        //                    if (str2 == "*")
        //                    {
        //                        return "[*]";
        //                    }
        //                }
        //                return this.AA1YNisqZ2Po240AFws2Y4MH25SW.ToString();
        //            }
        //        }

        //        [CompilerGenerated]
        //        private sealed class ACAVEfDt_JCsoeqSyb
        //        {
        //            public Hashtable AA1YNisqZ2Po240AFws2Y4MH25SW;

        //            public string AA1YNisqZ2Po240AFws2Y4MH25SW(Match match1)
        //            {
        //                string str = Regex.Replace(Regex.Match(match1.Value, "(\\[)(')*[\\S\\s]+?(')*(\\])").Value, "[\\['\\]]+", "", RegexOptions.IgnoreCase);
        //                if (this.AA1YNisqZ2Po240AFws2Y4MH25SW[str] != null)
        //                {
        //                    return this.AA1YNisqZ2Po240AFws2Y4MH25SW[str].ToString();
        //                }
        //                return "";
        //            }
        //        }
        //        public enum XmlType
        //        {
        //            Grid,
        //            Form,
        //            Toolbar,
        //            Query,
        //            Save,
        //            Lookup,
        //            Other
        //        }
        //    }
        //}
        //public static string EntityPath
        //        {
        //            get
        //            {
        //                string str = string.Empty;
        //                if (ConfigurationManager.AppSettings["EntityPath"] == null)
        //                {
        //                    return string.Empty;
        //                }
        //                str = ConfigurationManager.AppSettings["EntityPath"].ToString();
        //                return (StartPath + "/" + str);
        //            }
        //        }

        //        public static string ImageFolder
        //        {
        //            get
        //            {
        //                string str = string.Empty;
        //                if (ConfigurationManager.AppSettings["ImagePath"] == null)
        //                {
        //                    return string.Empty;
        //                }
        //                str = ConfigurationManager.AppSettings["ImagePath"].ToString();
        //                return ("/" + str);
        //            }
        //        }

        //        public static string ImagePath
        //        {
        //            get
        //            {
        //                string str = string.Empty;
        //                if (ConfigurationManager.AppSettings["ImagePath"] == null)
        //                {
        //                    return string.Empty;
        //                }
        //                str = ConfigurationManager.AppSettings["ImagePath"].ToString();
        //                return (StartPath + "/" + str);
        //            }
        //        }

        //        public static bool IsPagePostBack
        //        {
        //            get
        //            {
        //                bool flag = false;
        //                if (HttpContext.Current.Request.Url.ToString().ToLower().IndexOf(".asmx") > -1)
        //                {
        //                    flag = true;
        //                }
        //                return flag;
        //            }
        //        }

        //        public static string ProjectName =>
        //            HttpContext.Current.Application["ProjectName"]?.ToString();

        //        public static string SecEntityPath
        //        {
        //            get
        //            {
        //                string str = string.Empty;
        //                str = "SecondDeploy/Entity";
        //                return (StartPath + "/" + str);
        //            }
        //        }

        //        public static string StartPath =>
        //            ProjectName;

        //        [CompilerGenerated]
        //        private sealed class ABuGXxneo6DAkrWJV4
        //        {
        //            public Match AA1YNisqZ2Po240AFws2Y4MH25SW;

        //            public string AA1YNisqZ2Po240AFws2Y4MH25SW(Match match1)
        //            {
        //                string str2 = match1.ToString();
        //                if (str2 != null)
        //                {
        //                    if (str2 == "[")
        //                    {
        //                        return "[[]";
        //                    }
        //                    if (str2 == "]")
        //                    {
        //                        return "[]]";
        //                    }
        //                    if (str2 == "%")
        //                    {
        //                        return "[%]";
        //                    }
        //                    if (str2 == "*")
        //                    {
        //                        return "[*]";
        //                    }
        //                }
        //                return this.AA1YNisqZ2Po240AFws2Y4MH25SW.ToString();
        //            }
        //        }

        //        [CompilerGenerated]
        //        private sealed class ACAVEfDt_JCsoeqSyb
        //        {
        //            public Hashtable AA1YNisqZ2Po240AFws2Y4MH25SW;

        //            public string AA1YNisqZ2Po240AFws2Y4MH25SW(Match match1)
        //            {
        //                string str = Regex.Replace(Regex.Match(match1.Value, "(\\[)(')*[\\S\\s]+?(')*(\\])").Value, "[\\['\\]]+", "", RegexOptions.IgnoreCase);
        //                if (this.AA1YNisqZ2Po240AFws2Y4MH25SW[str] != null)
        //                {
        //                    return this.AA1YNisqZ2Po240AFws2Y4MH25SW[str].ToString();
        //                }
        //                return "";
        //            }
        //        }

        //        public enum XmlType
        //        {
        //            Grid,
        //            Form,
        //            Toolbar,
        //            Query,
        //            Save,
        //            Lookup,
        //            Other
        //        }
        //    }
        //}
        //ws2Y4MH25SW.AGF3("JQA="))
        //                    {
        //                        return "[%]";
        //                    }
        //                    if (str2 == "*")
        //                    {
        //                        return "[*]";
        //                    }
        //                }
        //                return this.AA1YNisqZ2Po240AFws2Y4MH25SW.ToString();
        //            }
        //        }

        //        [CompilerGenerated]
        //        private sealed class ACAVEfDt_JCsoeqSyb
        //        {
        //            public Hashtable AA1YNisqZ2Po240AFws2Y4MH25SW;

        //            public string AA1YNisqZ2Po240AFws2Y4MH25SW(Match match1)
        //            {
        //                string str = Regex.Replace(Regex.Match(match1.Value, "(\\[)(')*[\\S\\s]+?(')*(\\])").Value, "[\\['\\]]+", "", RegexOptions.IgnoreCase);
        //                if (this.AA1YNisqZ2Po240AFws2Y4MH25SW[str] != null)
        //                {
        //                    return this.AA1YNisqZ2Po240AFws2Y4MH25SW[str].ToString();
        //                }
        //                return "";
        //            }
        //        }

        //        public enum XmlType
        //        {
        //            Grid,
        //            Form,
        //            Toolbar,
        //            Query,
        //            Save,
        //            Lookup,
        //            Other
        //        }
        //    }
        //}

        //     return this.AA1YNisqZ2Po240AFws2Y4MH25SW[str].ToString();
        //                }
        //                return "";
        //            }
        //        }

        //        public enum XmlType
        //        {
        //            Grid,
        //            Form,
        //            Toolbar,
        //            Query,
        //            Save,
        //            Lookup,
        //            Other
        //        }
    }
}

