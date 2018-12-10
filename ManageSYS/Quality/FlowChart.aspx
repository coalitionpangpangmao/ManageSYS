<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlowChart.aspx.cs" Inherits="Quality_FlowChart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>流程图展示</title>
    <link href="../css/flowchart.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
</head>
<body style="margin: 0px; padding: 0px;">
    <div class="mainbox">
        <div class="place">
            <span>位置:</span>
            <ul class="placeul">
                <li><a href="#">质量分析与评估</a></li>
                <li><a href="#">在线数据评估</a></li>     
                <li><a href="#">流程图概览</a></li>
            </ul>
        </div>
        <div id="flowdiv">
            <iframe id="FlowFrame" name="FlowFrame" src="FLow/MainFlow.htm"></iframe>
        </div>
    </div>
</body>
</html>
