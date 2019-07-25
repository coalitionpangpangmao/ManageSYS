Highcharts.setOptions({
    global: {
        useUTC: false 
    }
});

//刷新新增的数据
function activeLastPointToolip(chart) {
    var points = chart.series[0].points;
    chart.tooltip.refresh(points[points.length - 1]);
}

//获取cpu数据
function requestData() {
    $.ajax({
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "monitor.aspx/ReGetCpuInfo",
        success: function (data) {
            activeLastPointToolip(chart);
            var y = data.d.toString();
            var x = new Date();
            chart.series[0].addPoint([x.getTime(), parseInt(y)], true, true);
            activeLastPointToolip(chart);

        },
        error: function () {
            alert("获取CPU信息失败");
        }

    });
}
char =null;
chart = Highcharts.chart('container', {
    chart: {
        type: 'spline',
        marginRight: 10,
        events: {
            load: function () {
                setInterval(function () {
                    requestData();
                }, 2000);
            }
        }
    },
    title: {
        text: 'CPU 使用率'
    },
    xAxis: {
        type: 'datetime',
        tickPixelInterval: 150,
    },
    yAxis: {
        max: 100,
        min: 0,
        title: {
            text: null
        }
    },
    tooltip: {
        formatter: function () {
            return '<b>' + this.series.name + '</b><br/>' +
						Highcharts.dateFormat('%Y-%m-%d %H:%M:%S', this.x) + '<br/>' +
						Highcharts.numberFormat(this.y, 2);
        }
    },
    legend: {
        enabled: false
    },
    series: [{
        name: 'CPU使用率',
        data: (function () {
            // 生成随机值
            var data = [],
						time = (new Date()).getTime(),
						i;
            for (i = -10; i <= 0; i += 1) {
                data.push({

                    x: time + i * 1000,
                    y: 0
                });
            }
            return data;
        } ())
    }]
});

/*==========================================*/

//获取内存数据
function requestMemData() { 
    $.ajax({
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "monitor.aspx/ReGetMemInfo",
        success: function (data) {
            activeLastPointToolip(chart2);
            //alert(data.d.toString());

            //var x = (new Date()).getTime();
            var y = data.d.toString();
            var x = new Date();
            chart2.series[0].addPoint([x.getTime(), parseInt(y)], true, true);
            activeLastPointToolip(chart2);

        },
        error: function () {
            alert("获取内存信息失败");
        }

    });
}

//定义内存图表
char2 =null;
chart2 = Highcharts.chart('container2', {
    chart: {
        type: 'spline',
        marginRight: 10,
        events: {
            load: function () {
                setInterval(function () {
                    requestMemData();
                }, 5000);
            }
        }
    },
    title: {
        text: '内存 使用率'
    },
    xAxis: {
        type: 'datetime',
        tickPixelInterval: 150,
    },
    yAxis: {
        max: 100,
        min: 0,
        title: {
            text: null
        }
    },
    tooltip: {
        formatter: function () {
            return '<b>' + this.series.name + '</b><br/>' +
						Highcharts.dateFormat('%Y-%m-%d %H:%M:%S', this.x) + '<br/>' +
						Highcharts.numberFormat(this.y, 2);
        }
    },
    legend: {
        enabled: false
    },
    series: [{
        name: '内存使用率',
        data: (function () {
            // 生成随机值
            var data = [],
						time = (new Date()).getTime(),
						i;
            for (i = -10; i <= 0; i += 1) {
                data.push({

                    x: time + i * 1000,
                    y: 0
                });
            }
            return data;
        } ())
    }]
});
