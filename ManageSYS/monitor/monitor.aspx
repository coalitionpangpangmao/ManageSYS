<%@ Page Language="C#" AutoEventWireup="true" CodeFile="monitor.aspx.cs" Inherits="monitor_monitor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>服务器运行状态</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
</head>
<body>
<form id="Form1" runat="server">
    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="#">服务器运行状态</a></li>
        </ul>
    </div>
    <div class="rightinfo">

        <div id="container" style="margin-left:10%; width:400px;height:400px; display:inline-block"></div>
        <div id ="container2" style=" margin-right:10%; margin-left:10%;width:400px; height:400px; display:inline-block"></div>
        <div id ="container3" style=" margin-right:10%; margin-left:10%;width:400px; height:400px; display:inline-block"></div>
    </div>
    </form>
 <script type="text/javascript" src="../js/highcharts.js"></script>
  <script type="text/javascript" src="../js/exporting.js"></script>
 <script type="text/javascript" src="../js/highcharts-zh_CN.js"></script>
  <script type="text/javascript" src="../js/monitorChart.js"></script>
</body>
