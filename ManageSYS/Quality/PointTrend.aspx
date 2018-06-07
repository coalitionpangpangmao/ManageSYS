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
    <script type="text/javascript">
        $(document).ready(function () {
            $("#browser").treeview({
                toggle: function () {
                    console.log("%s was toggled.", $(this).find(">span").text());
                }
            });
        });
        function treeClick(code) {
            $("#hdPrcd").val(code);
            $("#btnPara").click();        
            //在charframe中添加控件，完成数据刷新
        }
     
        $(function() {  
            var chart;                
            // initialization chart and actions  
            $(document).ready(function () {  
                chart = new Highcharts.Chart({  
                    chart: {  
                        renderTo: 'container',
                        type: 'spline'  
                    },  
                    title: {  
                        text: '数据点趋势图'  
                    },
                    xAxis: {
                        type: 'datetime',
                        labels: {
                            overflow: 'justify'
                        }
                    },
                    yAxis: {
                        title: {
                            text: 'Wind speed (m/s)'
                        },
                        minorGridLineWidth: 0,
                        gridLineWidth: 0,
                        alternateGridColor: null,
                        plotBands: [{ // Light air
                            from: 0.3,
                            to: 1.5,
                            color: 'rgba(255,0, 0, 0.4)',
                            label: {
                                text: 'Fault',
                                style: {
                                    color: '#606060'
                                }
                            }
                        }, { // Light breeze
                            from: 1.5,
                            to: 3.3,
                            color: 'rgba(255, 255, 0, 0.4)',
                            label: {
                                text: 'Warning',
                                style: {
                                    color: '#606060'
                                }
                            }
                        }, { // Gentle breeze
                            from: 3.3,
                            to: 5.5,
                            color: 'rgba(0,255, 0, 0.4)',
                            label: {
                                text: 'good',
                                style: {
                                    color: '#606060'
                                }
                            }
                        }, { // Moderate breeze
                            from: 5.5,
                            to: 8,
                            color: 'rgba(255, 255, 0, 0.4)',
                            label: {
                                text: 'Warning',
                                style: {
                                    color: '#606060'
                                }
                            }
                        }, { // Fresh breeze
                            from: 8,
                            to: 11,
                            color: 'rgba(255,0, 0, 0.4)',
                            label: {
                                text: 'fault',
                                style: {
                                    color: '#606060'
                                }
                            }
                        }]
                    },
                    credits: {  
                        enabled: false // remove high chart logo hyper-link  
                    },
                    plotOptions: {
                        spline: {
                            lineWidth: 4,
                            states: {
                                hover: {
                                    lineWidth: 5
                                }
                            },
                            marker: {
                                enabled: false
                            },
                            pointInterval: 30000, // one hour
                            pointStart: Date.UTC(2018, 4, 31, 0, 0, 0)
                        }
                    },
                    series: [{  
                        name: 'Point1',  
                        data: [10, 3, 5,4,8,2,6]  
                    }, {  
                        name: 'Point2',  
                        data: [5, 7, 3.8,9,11,5,1]  
                    }]  
                });  
                  
                // JQuery, mouse click event bind with dom buttons  
                $('#clear-button').on('click', function (e) {  
                    clearPlot();  
                });  
                  
                $('#refresh-button').on('click', function (e) {  
                    refreshPlot();  
                });  
            });  
              
            // clear all series of the chart  
            function clearPlot() {  
                //console.log("clear plot data!!!");  
                var series=chart.series;                  
                while(series.length > 0) {  
                    series[0].remove(false);  
                }  
                chart.redraw();  
            };  
              
            function refreshPlot() {  
                //console.log("refresh plot data!!!");  
                chart.addSeries({                          
                    id:1,  
                    name: "gloomyfish",  
                    data: [1,2,3]  
                }, false);  
                chart.addSeries({                          
                    id:2,  
                    name: "wang-er-ma",  
                    data: [5,2,1]  
                }, false);  
                chart.addSeries({                          
                    id:3,  
                    name: "zhang-san",  
                    data: [4,8,6]  
                }, false);  
                  
                chart.redraw();  
            };  
              
            setTimeout(function(){  
                var series=chart.series;                  
                while(series.length > 0) {  
                    series[0].remove(false);  
                }  
                chart.redraw();  
            },2000);  
              
            // add new series for the chart  
            setTimeout(function(){  
                chart.addSeries({                          
                    id:1,  
                    name: "gloomyfish",  
                    data: [1,2,3]  
                }, false);  
                chart.addSeries({                          
                    id:2,  
                    name: "wang-er-ma",  
                    data: [5,2,1]  
                }, false);  
                chart.addSeries({                          
                    id:3,  
                    name: "zhang-san",  
                    data: [4,8,6]  
                }, false);  
                  
                chart.redraw();  
            },2000);  
        });  
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
                        <asp:Button ID="btnPara" runat="server"  CssClass = "btnhide" OnClick = "btnPara_Click" />
                    </span>
                    </div>
                <div>
                    <table class="tablelist">                      
                        <tr>
                            <td>
                                开始时间
                            </td>
                            <td>
                                <asp:TextBox ID="txtBtime" runat="server" CssClass="tbinput1" 
                                    ontextchanged="txtBtime_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                结束时间
                            </td>
                            <td>
                                <asp:TextBox ID="txtEtime" runat="server" CssClass="tbinput1" 
                                    ontextchanged="txtEtime_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                          <tr>
                            <td>
                                生产计划号
                            </td>
                            <td>
                                <asp:DropDownList ID="listPlanno" runat="server" CssClass = "drpdwnlist" 
                                    Width="100px" onselectedindexchanged="listPlanno_SelectedIndexChanged">
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
                        <li><a href="#tab2" id="tabtop2">数据分析</a></li>
                    </ul>
                </div>
            </div>
            <div id="tab1" class="tabson">
            <div id="container" style="min-width: 310px; height: 400px; margin: 0 auto"></div>              
            </div>
            <div id="tab2" class="tabson">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode = "Conditional">
             <ContentTemplate>
                <table class="framelist">
                    <tr>
                        <td width="200px" valign="top" style="border-style: double;">
                            <div class="listtitle">
                                原始数据
                            </div>
                            <div style="width: 200px; overflow: scroll">
                                <asp:GridView ID="GridView1" runat="server" Width="200px" CssClass="datable" border="0"
                                    CellPadding="2" CellSpacing="1">
                                    <RowStyle CssClass="lupbai" />
                                    <Columns>
                                        <asp:CommandField ShowDeleteButton="True" />
                                    </Columns>
                                    <HeaderStyle CssClass="lup" />
                                    <AlternatingRowStyle CssClass="trnei" />
                                </asp:GridView>
                            </div>
                        </td>
                        <td>
                            <table class="tablelist">
                                <tr>
                                    <th>
                                        工艺参数
                                    </th>
                                    <th>
                                        采集参数
                                    </th>
                                </tr>
                                <tbody>
                                    <tr>
                                    <td valign="top" style="border-style: double">
                                            <table class="tablelist">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            标准值:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtStandardValue" runat="server"  CssClass="tbinput1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            上限:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtUPValue" runat="server" CssClass="tbinput1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            下限:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtLowValue" runat="server" CssClass="tbinput1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Button ID="WeightSet" runat="server" Text="设置" CssClass="btn1" />
                                                            <asp:Button ID="WeightFresh" runat="server" Text="刷新" CssClass="btn1" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    <td valign="top" style="border-style: double;">
                                            <table class="tablelist">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 80px">
                                                            采集周期
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPeriodic" runat="server" CssClass="tbinput1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            标签
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTagname" runat="server" CssClass="tbinput1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td >
                                                            采集条件
                                                            </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTagRst" runat="server" CssClass="tbinput1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td >
                                                            料头延时 
                                                            </td>
                                                            <td>
                                                            <asp:TextBox ID="txtHeadDelay" runat="server" CssClass="tbinput1"></asp:TextBox></td>
                                                              </tr>
                                                       <tr>
                                                        <td >
                                                            料尾延时
                                                             </td>
                                                            <td>
                                                            <asp:TextBox ID="txtTailDelay" runat="server" CssClass="tbinput1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Button ID="SetRst" runat="server" Text="设置" CssClass="btn1" />
                                                            <asp:Button ID="Flesh" runat="server" Text="刷新" CssClass="btn1" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <th colspan="2">
                                            统计信息
                                        </th>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="border-style: double" colspan="2">
                                            <table>
                                                <tr>
                                                    <td>
                                                        平均值:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAverg" runat="server" Width="60px" CssClass="tbinput1"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        超上限数
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtUpcount" runat="server" Width="60px" CssClass="tbinput1"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        标准偏差
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtStdDev" runat="server" Width="60px" CssClass="tbinput1"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        Ca(k)
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCak" runat="server" Width="60px" CssClass="tbinput1"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        总点数
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTotalCount" runat="server" Width="60px" CssClass="tbinput1"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        超上限率:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtUprate" runat="server" Width="60px" CssClass="tbinput1"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        最小值:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMin" runat="server" Width="60px" CssClass="tbinput1"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        Cpk:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCpk" runat="server" Width="60px" CssClass="tbinput1"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        达标数:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtQuaNum" runat="server" Width="60px" CssClass="tbinput1"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        超下限数:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDownCount" runat="server" Width="60px" CssClass="tbinput1"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        绝对偏差:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAbsDev" runat="server" Width="60px" CssClass="tbinput1"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        Cp:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCp" runat="server" Width="60px" CssClass="tbinput1"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        达标率:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtQuaRate" runat="server" Width="60px" CssClass="tbinput1"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        超下限率:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDownRate" runat="server" Width="60px" CssClass="tbinput1"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        最大值:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMax" runat="server" Width="60px" CssClass="tbinput1"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        Range:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtR" runat="server" Width="60px" CssClass="tbinput1"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table> 
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID = "btnPara" />
                 <asp:AsyncPostBackTrigger ControlID = "txtBtime" />
                  <asp:AsyncPostBackTrigger ControlID = "txtEtime" />
                   <asp:AsyncPostBackTrigger ControlID = "listPlanno" />
                </Triggers>
                  </asp:UpdatePanel>
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
