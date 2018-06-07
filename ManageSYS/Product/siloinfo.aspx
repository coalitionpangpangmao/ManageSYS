<%@ Page Language="C#" AutoEventWireup="true" CodeFile="siloinfo.aspx.cs" Inherits="Product_siloinfo" %>

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
    <form id="Form1" runat="server">
    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="#">生产管理</a></li>
            <li><a href="#">贮柜信息</a></li>
        </ul>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
  
        <asp:UpdatePanel ID="updtpanel" runat="server">
            <ContentTemplate>
                 <div class="rightinfo">    
         <table class="tablelist">    
                <thead>
    	<tr>
        <th >
        贮柜信息详情
        </th>
        </tr>
        </thead>
        <tbody>
        <tr>
        <td >
          <asp:GridView ID="GridView1" runat="server" Width="1200px" CssClass="datable" border="0"
            CellPadding="2" CellSpacing="1">
            <RowStyle CssClass="lupbai" />
           
            <HeaderStyle CssClass="lup" />
            <AlternatingRowStyle CssClass="trnei" />
        </asp:GridView>
        </td>
        </tr></tbody>
    </table>
    </div>   
     
        
            </ContentTemplate>
           <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GridView1" />
            </Triggers>
        </asp:UpdatePanel>
        <!--mainright end-->

   
 
    </form>
</body>
<script type="text/javascript">
    setWidth();
    $(window).resize(function () {
        setWidth();
    });
    function setWidth() {
        var width = ($('.leftinfos').width() - 12) / 2;
        $('.infoleft,.inforight').width(width);
    }
</script>
</html>
