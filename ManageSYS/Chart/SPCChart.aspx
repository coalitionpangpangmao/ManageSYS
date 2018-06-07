<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SPCChart.aspx.cs" Inherits="Quality_SPCChart" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="../css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../js/jquery.js"></script>
<script type="text/javascript" src="../js/jsapi.js"></script>
<script type="text/javascript" src="../js/format+zh_CN,default,corechart.I.js"></script>	
<script type="text/javascript" src="../js/jquery.ba-resize.min.js"></script>


</head>


<body>
<form id="Form1" runat ="server">
	<div class="place">
    <span>位置：</span>
    <ul class="placeul">
    <li><a href="#">质量分析</a></li>
    <li><a href="#">SPC</a></li>
    </ul>
    </div>
     <asp:ScriptManager ID="ScriptManager1"     runat="server">    
              </asp:ScriptManager>
    
    <div class="mainbox">
    
    <div class="mainleft">
    
    
    <div class="leftinfo">
    <div class="listtitle">配方</div>
        <asp:TreeView ID="trvModel" runat="server" ShowLines="True" 
            ExpandDepth="0" NodeIndent="0"  Width="198px" >             
                <SelectedNodeStyle BackColor="Transparent" CssClass="SelectedNodeStyle" Width="100%" />
                <RootNodeStyle Font-Bold="True" Font-Size="Small" HorizontalPadding="5px" 
                    Width="100%" />
                <ParentNodeStyle  Width="100%" />
                <LeafNodeStyle  Width="100%" />
                <NodeStyle Width="100%" />
            </asp:TreeView>     
    
    </div>
    <!--leftinfo end-->
    </div>
    <!--mainleft end-->
    
      <asp:UpdatePanel ID="updtpanel"  runat="server">
            <ContentTemplate>
    <div class="mainright">   
    
    <div  id = "div1" runat = "server" class="dflist">
    <div class="listtitle">实时曲线</div>  
     <chart:WebChartViewer id="WebChartViewer1" runat="server" />
        
    </div>
    </div>
          </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger    ControlID="trvModel" />
            </Triggers>
        </asp:UpdatePanel>
    <!--mainright end-->
    </div>
    
    </form>
</body>

</html>
