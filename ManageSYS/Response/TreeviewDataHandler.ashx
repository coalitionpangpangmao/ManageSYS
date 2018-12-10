<%@ WebHandler Language="C#" Class="TreeviewDataHandler" %>

using System;
using System.Web;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
[Serializable]
public struct RequestTreeData
{
    public string table;//数据表
    public string idSeg;//字段
    public string textSeg;//文本
    public string rootSeg;//父节点对应字段
    public string roottext;//父节点对应文本 即点击树时的节点
    public string childtable;//加载节点所拥有的孩子对应的数据表
    public string childrootseg;//孩子节点父节点字段,文本就对应textSeg
    public string childidseg;
    public string childtextseg;
    public string type { get; set; }// String id = request.getParameter("root"); //treeview自动提交root参数为当前节点的id，如果是根节点，则为source  
}
[Serializable]
public struct TreeData
{
    public string id;
    public string text;
    public string cssClass;
    public bool expanded;
    public bool hasChild;
}

public class TreeviewDataHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        var data = context.Request;
        var sr = new StreamReader(data.InputStream);
        var stream = sr.ReadToEnd();
        var javaScriptSerializer = new JavaScriptSerializer();
        try
        {
            if (stream == "")
                return;
            else
            {
                var requestData = javaScriptSerializer.Deserialize<RequestTreeData>(stream);
                // datainfo = getTreeData(requestData);
                var responseData = InitTree(requestData);
                context.Response.ContentType = "text/plain";
                context.Response.Write(responseData);
            }


        }
        catch (Exception ee)
        {
            var e = ee.Message;
        }
    }

    private List<TreeData> getTreeData()
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select mattree_code,mattree_name  from ht_pub_mattree where IS_DEL = '0'  and length(mattree_code) = 2  order by mattree_code";
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            List<TreeData> tree = new List<TreeData>();
            foreach (DataRow row in data.Tables[0].Rows)
            {
                TreeData leaf = new TreeData();
                leaf.id = row["mattree_code"].ToString();
                leaf.text = row["mattree_name"].ToString();
                string childcount = opt.GetSegValue("select count(*) as num from ht_pub_mattree where PARENT_CODE = '" + leaf.id + "'", "num");
                if (childcount == "NoRecord" || childcount == "0")
                {
                    leaf.hasChild = false;
                    leaf.cssClass = "file";
                    leaf.expanded = false;
                }
                else
                {
                    leaf.hasChild = true;
                    leaf.cssClass = "folder";
                    leaf.expanded = false;
                }
                tree.Add(leaf);
            }
            return tree;
        }
        else
            return null;
    }

    //生成json格式数据  
    private List<TreeData> getTreeData(RequestTreeData category)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select " + category.idSeg + "," + category.textSeg + " from " + category.table + " where " + category.rootSeg + " = '" + category.roottext + "'";
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            List<TreeData> tree = new List<TreeData>();
            foreach (DataRow row in data.Tables[0].Rows)
            {
                TreeData leaf = new TreeData();
                leaf.id = row[category.idSeg].ToString();
                leaf.text = row[category.textSeg].ToString();
                string childcount = opt.GetSegValue("select count(*) as num from " + category.childtable + " where " + category.childrootseg + " = '" + leaf.id + "'", "num");
                if (childcount == "NoRecord" || childcount == "0")
                {
                    leaf.hasChild = false;
                    leaf.cssClass = "folder";
                    leaf.expanded = false;
                }
                else
                {
                    leaf.hasChild = true;
                    leaf.cssClass = "folder";
                    leaf.expanded = false;
                }
                tree.Add(leaf);
            }
            return tree;
        }
        else
            return null;
    }
    public string InitTree(RequestTreeData category)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select " + category.idSeg + "," + category.textSeg + " from " + category.table + " where " + category.rootSeg ;
        if (category.roottext != "")
            query += " = '" + category.roottext + "'";
        else
            query += " is null";
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml;
            if (category.type == "source")
                tvHtml = "<ul id='browser' class='filetree treeview-famfamfam' >";
            else
                tvHtml = "<ul style='display: block;'>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                TreeData leaf = new TreeData();
                leaf.id = row[category.idSeg].ToString();
                leaf.text = row[category.textSeg].ToString();
                string childcount = opt.GetSegValue("select count(*) as num from " + category.childtable + " where " + category.childrootseg + " = '" + leaf.id + "'", "num");
                if (childcount == "NoRecord" || childcount == "0")
                {
                    leaf.hasChild = false;
                    leaf.cssClass = "folder";
                    leaf.expanded = false;
                }
                else
                {
                    leaf.hasChild = true;
                    leaf.cssClass = "folder";
                    leaf.expanded = false;
                }
                if (leaf.hasChild)
                {
                    tvHtml += "<li class='expandable'>";
                    tvHtml += "<div class='hitarea expandable-hitarea'></div>";
                }
                else
                    tvHtml += "<li>";
                tvHtml += " <span class='" + leaf.cssClass + "' value='" + leaf.id + "' hasChild = '" + leaf.hasChild + "' hasDone = 'True'>" + leaf.text + "</span>";               
                if (leaf.hasChild)
                {
                    tvHtml += InitTreeChild(category, leaf.id);
                }
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";


    }

    public string InitTreeChild(RequestTreeData category, string code)
    {
        MSYS.DAL.DbOperator opt = new MSYS.DAL.DbOperator();
        string query = "select " + category.childidseg + "," + category.childtextseg + " from " + category.childtable + " where " + category.childrootseg + " = '" + code + "'";
        DataSet data = opt.CreateDataSetOra(query);
        if (data != null && data.Tables[0].Rows.Count > 0)
        {
            string tvHtml = "<ul style='display: none;'>";
            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                tvHtml += "<li ><span class='folder' value = '" + row[category.childidseg].ToString() + "'>" + row[category.childtextseg].ToString() + "</span></a>";
                tvHtml += "</li>";
            }
            tvHtml += "</ul>";
            return tvHtml;
        }
        else
            return "";
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}