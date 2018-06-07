<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EventDeal.aspx.cs" Inherits="Quality_EventDeal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>工艺事件处理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript">   
        function treeClick(code) {
            debugger;
            $("#hidecode").val(code);
            $("#btnView").click();
          
        }

        </script>
</head>
<body>
    <script type="text/javascript" src="../code/highcharts.js"></script>
    <script type="text/javascript" src="../code/modules/series-label.js"></script>
    <script type="text/javascript" src="../code/modules/exporting.js"></script>
    <form id="Form1" runat="server">
    <div class="place">
        <span>位置:</span>
        <ul class="placeul">
            <li><a href="#">质量分析与评估</a></li>
            <li><a href="#">工艺事件处理</a></li>
        </ul>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="mainbox">
        <table class = "tablelist">
        <tr>
        <td align="center" colspan="3">
        时间：<asp:TextBox ID="txtStarttime" runat="server" CssClass = "dfinput1"></asp:TextBox>
        至：<asp:TextBox ID="txtEndtime" runat="server"  CssClass = "dfinput1"></asp:TextBox>
        </td></tr>
        <tr>
        <td valign="top">
        <table class = "tablelist">
        <tr><th width="350px">
        待处理工艺事件列表
        </th></tr>
        <tr>
        <td width="350px">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
               <asp:GridView ID="GridView1" runat="server"  class="grid"  >
                                           <HeaderStyle CssClass="gridheader" />
                                            <RowStyle CssClass="gridrow" />
                                        </asp:GridView>
            </ContentTemplate>
            <Triggers>
            </Triggers>
            </asp:UpdatePanel>
        </td></tr>
        </table>
        </td>
        <td width="10px"></td>
        <td valign="top">
        <table class = "tablelist">
        <tr><td>工艺事件：<asp:TextBox ID="txtEvent" runat="server" CssClass = "dfinput1" 
                ></asp:TextBox><asp:DropDownList ID="listType" runat="server" CssClass = "drpdwnlist">
            </asp:DropDownList>
            &nbsp;
            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass = "btn1" />
            
        </td></tr>
        <tr><th>
        原因分析
        </th></tr>
        <tr>
        <td >
            <asp:TextBox ID="txtReason" runat="server" CssClass = "dfinput1" Height="100px" 
                Width="400px" TextMode="MultiLine"></asp:TextBox>
        </td></tr>
        <tr><th>
        处理措施
        </th></tr>
        <tr>
        <td >
            <asp:TextBox ID="txtDeal" runat="server" CssClass = "dfinput1" Height="100px" 
                Width="400px" TextMode="MultiLine"></asp:TextBox>
        </td></tr>
          <tr><th>
        补充说明
        </th></tr>
        <tr>
        <td >
            <asp:TextBox ID="txtPlus" runat="server" CssClass = "dfinput1" Height="100px" 
                Width="400px" TextMode="MultiLine"></asp:TextBox>
        </td></tr>
        </table>
        </td>
        </tr>
        </table>
        <!--mainleft end-->
        
    </div>
    
    </form>
</body>
</html>
