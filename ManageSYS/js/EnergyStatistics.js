/*==========================================*/
//饼图
function requestColumnChart() {
    var name, unit;

    $.ajax({
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataTyep: "json",
        url: "Statistics.aspx/getcConsumption",
        data: "",
        success: function (data) {

        },
        error: function () {
            alert("没有获取到数据");
        }
    });
}

function requestOracleData() { 
    $.ajax({
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "Statistics.aspx/getProcessConsumption",
        data: "{'name':'工厂', 'process':'1', 'unit':'1'}", 
        success: function (data) {
            //ora = data.d.toString();
            getOracleChart(data.d.toString().toLowerCase());

        },
        error: function () {
            alert("获取ORACLE信息失败");
        }

    });
}

function getOracleChart(ora) {
    chart3 = Highcharts.chart('container3', {
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false
        },
        title: {
            text: '表空间使用率'
        },
        tooltip: {
            headerFormat: '{series.name}<br>',
            pointFormat: '{point.name}: <b>{point.percentage:.1f}%</b>'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                    style: {
                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                    }
                },
                states: {
                    hover: {
                        enabled: false
                    }
                },
                slicedOffset: 20,         // 突出间距
                point: {                  // 每个扇区是数据点对象，所以事件应该写在 point 下面
                    events: {
                        // 鼠标滑过是，突出当前扇区
                        mouseOver: function () {
                            this.slice();
                        },
                        // 鼠标移出时，收回突出显示
                        mouseOut: function () {
                            this.slice();
                        },
                        // 默认是点击突出，这里屏蔽掉
                        click: function () {
                            return false;
                        }
                    }
                }
            }
        },
        series: [{
            type: 'pie',
            name: '表空间使用率',
            data: ora
        }]

    });
} 