<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Optimize.aspx.cs" Inherits="Quality_Optimize" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>加料优化</title>
       <script type="text/javascript" src="../js/jquery.js"></script>
       <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
        <script type="text/javascript" src="../js/code/highcharts.js"></script>
        <link href="../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <body>
    <script type="text/javascript" src="../js/code/highcharts.js"></script>
    <script type="text/javascript" src="../js/code/modules/series-label.js"></script>
    <script type="text/javascript" src="../js/code/modules/exporting.js"></script>
    <form id="Form1" runat="server">
        <div class="place">
            <span>位置:</span>
            <ul class="placeul">
                <li><a href="#">友情链接</a></li>
                <li><a href="#">加香加料比例优化</a></li>     
            </ul>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="mainbox">
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

      <script type="text/javascript" src="../js/msys/Optimize.js"></script>
</body>
</html>
