<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PointTrend.aspx.cs" Inherits="Quality_PointTrend" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>工艺点趋势图</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
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
                <li><a href="#">趋势图</a></li>
            </ul>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="mainbox">
            <div class="mainleft">
                <div class="leftinfo">
                    <div class="listtitle">
                        查询条件
                    <span>
                        <asp:HiddenField ID="hdPrcd" runat="server" />
                     
                    </span>
                    </div>
                    <div>
                          <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
                        <table class="tablelist">
                            <tr>
                                <td>开始时间
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBtime" runat="server" CssClass="tbinput1"
                                        OnTextChanged="txtBtime_TextChanged" onclick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>结束时间
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEtime" runat="server" CssClass="tbinput1"
                                        OnTextChanged="txtEtime_TextChanged" onclick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>生产计划号
                                </td>
                                <td>
                                    <asp:DropDownList ID="listPlanno" runat="server" CssClass="drpdwnlist"
                                        Width="100px" OnSelectedIndexChanged="listPlanno_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="listtitle">
                        工艺模型
                    </div>
                    <% = tvHtml %>
                </div>
                <!--leftinfo end-->
            </div>
            <!--mainleft end-->
            <div class="mainright">
                <div id="usual1" class="usual">
                    <div class="itab">
                        <ul>
                            <li><a href="#tab1" id="tabtop1">趋势图</a></li>
                            <li><a href="#tab2" id="tabtop2">原始数据</a></li>
                        </ul>
                    </div>
                </div>
                <div id="tab1" class="tabson">                
                        <div id="container" style="min-width: 310px; height: 400px; margin: 0 auto">
                        </div>
                        <div id="statics">
                        </div>                 
                </div>
                <div id="tab2" class="tabson">
                    <div id="initdata">
                        </div>      
                </div>
                <!--mainright end-->
            </div>
        </div>
        <script type="text/javascript">
            $("#usual1 ul").idTabs();
        </script>
    </form>
</body>
</html>
