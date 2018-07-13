<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Comparison.aspx.cs" Inherits="Energy_Comparison" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>数据对比</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/select.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <link rel="stylesheet" href="../js/jquery-treeview/jquery.treeview.css" />
    <link rel="stylesheet" href="../js/jquery-treeview/screen.css" />
    <script type="text/javascript" src="../js/jquery-treeview/jquery.cookie.js"></script>
    <script src="../js/jquery-treeview/jquery.treeview.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#browser").treeview({
                toggle: function () {
                    console.log("%s was toggled.", $(this).find(">span").text());
                }
            });

        });
        $(function () {
            // create the chart
            $('#container').highcharts({
                chart: {
                    events: {
                        addSeries: function () {
                            var label = this.renderer.label('A series was added, about to redraw chart', 100, 120)
										.attr({
										    fill: Highcharts.getOptions().colors[0],
										    padding: 10,
										    r: 5,
										    zIndex: 8
										})
										.css({
										    color: '#FFFFFF'
										})
										.add();
                            setTimeout(function () {
                                label.fadeOut();
                            }, 1000);
                        }
                    }
                },
                title: { text: '过程数据比对' },
                yAxis: { title: { text: '值'} },
                legend: { layout: 'vertical', align: 'right', verticalAlign: 'middle' },
                plotOptions: { series: { cursor: 'pointer', pointStart: 0} },
                credits: {
                    enabled: false
                },
                responsive: {
                    rules: [{ condition: { maxWidth: 500 },
                        chartOptions: {
                            legend: { layout: 'horizontal', align: 'center', verticalAlign: 'bottom'}
                        }
                    }]
                }
            });
            // activate the button
            $('#btnCompare').click(function () {
                var chart = $('#container').highcharts();
                chart.addSeries({ name: 'test1', data: [216.4, 194.1, 95.6, 54.4, 29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5] });
                chart.addSeries({ name: 'test2', data: [216.4, 194.1, 95.6, 80, 29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 90] });
                        // chart.addSeries(<%=JavaHtml %>);
                $(this).attr('disabled', true);
            });
        });
        function treeClick(obj) {
            debugger;
            var code = obj.value;
            if (obj.checked) {
                $("#hidecode").val(code);
                $("#btnAdd").click();
            }
            else {
                $("#hidecode").val(code);
                $("#btnDel").click();
            }
        }
    
        //在charframe中添加控件，完成数据刷新
    </script>
</head>
<body>
    <script type="text/javascript" src="../js/code/highcharts.js"></script>
    <script type="text/javascript" src="../../modules/series-label.js"></script>
    <script type="text/javascript" src="../../modules/exporting.js"></script>
    <form id="Form1" runat="server">
    <div class="place">
        <span>位置:</span>
        <ul class="placeul">
            <li><a href="#">质量分析与评估</a></li>
            <li><a href="#">数据对比</a></li>
        </ul>
    </div>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="mainbox">
        <div class="mainleft">
            <div class="leftinfo">
        
    
                <div class="listtitle">
                    工艺模型<span style="position: relative; float: right">
                        <asp:HiddenField ID="hidecode" runat="server" />
                        <asp:Button ID="btnAdd" runat="server" Text="Button" CssClass="btnhide" OnClick="btnAdd_Click" />
                        <asp:Button ID="btnDel" runat="server" Text="Button" CssClass="btnhide" OnClick="btnDel_Click" /></span></div>
                <% = tvHtml %>
            </div>
            <!--leftinfo end-->
        </div>
        <!--mainleft end-->
        <div class="mainright">
            <div class="framelist">
            <div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>                        
                        <table class="tablelist"  >
                            <tr>
                                <th>
                                    数据点：<asp:DropDownList ID="listpara" runat="server" CssClass="drpdwnlist" >
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    时间：<asp:TextBox ID="txtBtime" runat="server" CssClass="dfinput1"></asp:TextBox>
                                    至：<asp:TextBox ID="txtEtime" runat="server" CssClass="dfinput1"></asp:TextBox>
                                    &nbsp;
                                    <asp:Button ID="btnAddtime" runat="server" Text="添加"  OnClick = "btnAddtime_Click" CssClass="btn1 auth" />
                                     <asp:Button ID="btnDeltime" runat="server" Text="删除"  OnClick = "btnDeltime_Click" CssClass="btn1 auth" />
                                </th>
                            </tr>
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:CheckBoxList ID="cklistPara" runat="server" Width="98%">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                        <asp:AsyncPostBackTrigger ControlID="btnDel" />
                        <asp:AsyncPostBackTrigger ControlID = "btnCompare" />
                          <asp:AsyncPostBackTrigger ControlID = "btnAddtime" />
                          <asp:AsyncPostBackTrigger ControlID = "btnDeltime" />
                    </Triggers>
                </asp:UpdatePanel>
                </div>
                        <div class="listtitle">
                            趋势图 <span style="position: relative; float: right">
                                <asp:Button ID="btnCompare" runat="server" Text="比对" class="btnview"  />
                            </span>
                        </div>
                        <div id="container" style="height: 400px; max-width: 800px; margin: 0 auto">
                              
                        </div>
        
            </div>
            <!--mainright end-->
        </div>
    </div>
    </form>
</body>
</html>
