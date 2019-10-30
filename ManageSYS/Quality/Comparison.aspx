<%@ page language="C#" autoeventwireup="true" inherits="Quality_Comparison"  CodeFile="~/Quality/Comparison.aspx.cs" enableviewstatemac="false" enableEventValidation="false" viewStateEncryptionMode="Never" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>数据对比</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <link rel="stylesheet" href="../js/jquery-treeview/jquery.treeview.css" />
    <link rel="stylesheet" href="../js/jquery-treeview/screen.css" />
    <script type="text/javascript" src="../js/jquery-treeview/jquery.cookie.js"></script>
    <script src="../js/jquery-treeview/jquery.treeview.js" type="text/javascript"></script>
     <script src="../js/msys/Comparison.js" type="text/javascript"></script>
  
        
    
    
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
                <li><a href="#">在线数据评估</a></li>     
                <li><a href="#">批间对比</a></li>
            </ul>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="mainbox">
            <div class="mainleft">
                <div class="leftinfo" >
                    <div class="listtitle">
                        工艺模型<span style="position: relative; float: right">
                            <asp:HiddenField ID="hidecode" runat="server" />
                            <asp:Button ID="btnAdd" runat="server" Text="Button" CssClass="btnhide" OnClick="btnAdd_Click" />
                            <asp:Button ID="btnDel" runat="server" Text="Button" CssClass="btnhide" OnClick="btnDel_Click" /></span>
                    </div>

                    <% = tvHtml %>
                </div>
                <!--leftinfo end-->
            </div>
            <!--mainleft end-->
            <div class="mainright">
                 <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table class="tablelist">
                                <tr>
                                    <th>数据点：<asp:DropDownList ID="listpara" runat="server" CssClass="drpdwnlist">
                                    </asp:DropDownList>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    时间：<asp:TextBox ID="txtBtime" runat="server" CssClass="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                                        至：<asp:TextBox ID="txtEtime" runat="server" CssClass="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                                        &nbsp;
                                    <asp:Button ID="btnAddtime" runat="server" Text="添加" OnClick="btnAddtime_Click" CssClass="btnadd auth" />
                                        <asp:Button ID="btnDeltime" runat="server" Text="删除" OnClick="btnDeltime_Click" CssClass="btndel auth"  />
                                        限定值：<asp:TextBox ID="txtValue" runat="server" CssClass="dfinput1"></asp:TextBox>
                                        <asp:Button ID="btnExport" runat="server" Text="导出" class="btnset" OnClick="btnExport_Click" />
                                    </th>
                                </tr>
                                <tbody>
                                    <tr>
                                        <td >
                                            <div style="width: 100%; height: 70px; overflow: scroll">
                                            <asp:CheckBoxList ID="cklistPara" runat="server" Width="98%">
                                            </asp:CheckBoxList>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                            <asp:AsyncPostBackTrigger ControlID="btnDel" />                          
                            <asp:AsyncPostBackTrigger ControlID="btnAddtime" />
                            <asp:AsyncPostBackTrigger ControlID="btnDeltime" />
                         <asp:PostBackTrigger ControlID ="btnExport" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="listtitle">
                    趋势图 <span style="position: relative; float: right">
                        <input id="btnCompare" type="button" value="比对"  class ="btnview" />
                     
                    </span>
                </div>
                <div id="container" style="height: 420px; min-width: 800px; margin: 0 auto">
                </div>
                 <div id="statics" >
                        </div>   
                <!--mainright end-->
            </div>
        </div>
    </form>
</body>
</html>
