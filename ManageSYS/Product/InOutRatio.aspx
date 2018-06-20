<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InOutRatio.aspx.cs" Inherits="Product_InOutRatio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>投入产出比</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            DrawPic();
        });

        function DrawPic() {
            if ($("#hdcode1").val() == "")
                $("#hdcode1").val([216.4, 194.1, 95.6, 54.4, 29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5]);
            if ($("#hdcode2").val() == "")
                $("#hdcode2").val([116.4, 294.1, 195.6, 154.4, 129.9, 91.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5]);
            if ($("#hdcode3").val() == "")
                $("#hdcode3").val([99, 56, 95.6, 80, 29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 90]);
            if ($("#hdXaxis").val() == "")
                $("#hdXaxis").val(['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
            'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']);
            Highcharts.chart('container', {
                chart: {
                    events: {
                        addSeries: function () {
                            var label = this.renderer.label('A series was added, about to redraw chart', 100, 120).attr({ fill: Highcharts.getOptions().colors[0],
                                padding: 10,
                                r: 5,
                                zIndex: 8
                            }).css({
                                color: '#FFFFFF'
                            }).add();
                            setTimeout(function () {
                                label.fadeOut();
                            }, 1000);
                        }
                    },
                    zoomType: 'xy'
                },
                title: { text: '投入产出比' },
                xAxis: [{
                    categories: $("#hdXaxis").val().split(','),
                    crosshair: true
                }],
                yAxis: [{ labels: {
                    format: '{value} Kg',
                    style: { color: Highcharts.getOptions().colors[1] }
                }, title: {
                    text: '投入/产量',
                    style: { color: Highcharts.getOptions().colors[1] }
                }
                }, { // Secondary yAxis
                    title: {
                        text: '投入产出比',
                        style: { color: Highcharts.getOptions().colors[0] }
                    },
                    labels: {
                        format: '{value} %',
                        style: { color: Highcharts.getOptions().colors[0] }
                    },
                    opposite: true
                }],


                tooltip: {
                    shared: true
                },
                legend: {
                    layout: 'vertical',
                    align: 'left',
                    x: 120,
                    verticalAlign: 'top',
                    y: 100,
                    floating: true,
                    backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'
                },
                credits: { enabled: false },
                series: [{ name: '投入', type: 'column', yAxis: 0, data:$("#hdcode1").val().split(',').map(function(data){  return +data;   }), tooltip: { valueSuffix: ' Kg'} },
               { name: '产出', type: 'column', yAxis: 0, data: $("#hdcode2").val().split(',').map(function (data) { return +data; }), tooltip: { valueSuffix: ' Kg'} },
               { name: '投入产出比', type: 'spline', yAxis: 1, data: $("#hdcode3").val().split(',').map(function (data) { return +data; }), tooltip: { valueSuffix: ' %'} }
               ]
               
            });

        }
		</script>	


</head>
<body>
    <script type="text/javascript" src="../code/highcharts.js"></script>
    <script type="text/javascript" src="../code/modules/series-label.js"></script>
    <script type="text/javascript" src="../code/modules/exporting.js"></script>
      <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <form id="Form1" runat="server">
    <div class="place">
        <span>位置:</span>
        <ul class="placeul">
            <li><a href="#">生产管理</a></li>
            <li><a href="#">投入产出比</a></li>
        </ul>
    </div>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="framelist">
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers = "true">
                <ContentTemplate>
                    <table class="tablelist">
                        <tr>
                            <th>
                                牌号：<asp:DropDownList ID="listRecipe" runat="server" CssClass="drpdwnlist" 
                                    onselectedindexchanged="listRecipe_SelectedIndexChanged">
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdSort1" runat="server" GroupName="Sort" Text="按月" 
                                    oncheckedchanged="rdSort1_CheckedChanged" />
                                <asp:RadioButton ID="rdSort2" runat="server" GroupName="Sort" Text="按季" 
                                    oncheckedchanged="rdSort2_CheckedChanged" />
                                <asp:RadioButton ID="rdSort3" runat="server" GroupName="Sort" Text="按年" 
                                    oncheckedchanged="rdSort3_CheckedChanged" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtBtime" runat="server" CssClass="dfinput1" 
                                    onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" 
                                    ontextchanged="txtBtime_TextChanged"></asp:TextBox>
                                至：<asp:TextBox ID="txtEtime" runat="server" CssClass="dfinput1" 
                                    onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" 
                                    ontextchanged="txtEtime_TextChanged"></asp:TextBox>

                                &nbsp;
                                
                                  <input id="btnCompare" type="button" value="查询" class="btnview" onclick = "DrawPic()"/>
                            </th>
                        </tr>
                      
                    </table>
              
        <div >
            <asp:HiddenField ID="hdcode1" runat="server" />
             <asp:HiddenField ID="hdcode2" runat="server" />
              <asp:HiddenField ID="hdcode3" runat="server" />
                <asp:HiddenField ID="hdXaxis" runat="server" />
        </div>
        <div id="container" style="height: 400px; max-width: 800px; margin: 0 auto">
        </div>
          </ContentTemplate>
                <Triggers>
                  
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
