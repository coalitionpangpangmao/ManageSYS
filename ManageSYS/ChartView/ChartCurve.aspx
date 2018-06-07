<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChartCurve.aspx.cs" Inherits="Quality_ChartCurve" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   		<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
		<meta name="viewport" content="width=device-width, initial-scale=1"/>
		<title>Highcharts Example</title>
</head>
<body>
<script  type ="text/javascript" src="../code/highcharts.js"></script>
<script  type ="text/javascript"  src="../code/modules/boost.js"></script>
<script  type ="text/javascript"  src="../code/modules/exporting.js"></script>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdcode" runat="server" />
<div id="container" style="height: 400px; max-width: 800px; margin: 0 auto"></div>
<%=JavaHtml%>
    </form>
</body>
</html>
