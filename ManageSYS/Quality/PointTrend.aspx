<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PointTrend.aspx.cs" Inherits="Quality_PointTrend" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>工艺点趋势图</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>  
    <link rel="stylesheet" href="../js/jquery-treeview/jquery.treeview.css" />
    <link rel="stylesheet" href="../js/jquery-treeview/screen.css" />
    <script type="text/javascript" src="../js/jquery-treeview/jquery.cookie.js"></script>
    <script src="../js/jquery-treeview/jquery.treeview.js" type="text/javascript"></script>
    <script src="../js/msys/pointTrend.js" type="text/javascript"></script>
</head>
<body>
    <script type="text/javascript" src="../js/code/highcharts.js"></script>
    <script type="text/javascript" src="../js/code/modules/series-label.js"></script>
    <script type="text/javascript" src="../js/code/modules/exporting.js"></script>
    <form id="Form1" runat="server">
        <div class="place">
            <span>位置:</span>
            <ul class="placeul">
                <li><a href="#">质量分析与评估</a></li>
                <li><a href="#">趋势图分析</a></li>
            </ul>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="mainbox">
            <div class="mainleft">
                <div class="leftinfo" style="overflow: scroll"> 
                    <asp:HiddenField ID ="hdPrcd"  runat ="server"/>
                     <div class="listtitle">
                        工艺模型
                    </div>
                    <% = tvHtml %>
                </div>
                <!--leftinfo end-->
            </div>
            <!--mainleft end-->
            <div class="mainright">  
                        <div id="container" style="min-width: 310px; height: 400px; margin: 0 auto">
                        </div>
                        <div id="statics" >
                        </div>   
                <!--mainright end-->
            </div>
        </div>
     
    </form>
</body>
</html>
