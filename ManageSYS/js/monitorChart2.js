Highcharts.setOptions({
    global: {
        useUTC: false 
    }
});
function activeLastPointToolip(chart2) {
    var points = chart2.series[0].points;
    chart2.tooltip.refresh(points[points.length - 1]);
}
function getCpuData() {
    var cpu_detail = "<%=GetCpuInfo() %>";
    var cpu_detail_list = cpu_detail.split("|");
    return parseInt(cpu_detail_list[0]);
}
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
            var y = data.d.toString().split("|");
            var x = new Date();
            //var x = y[1].toString();
            chart2.series[0].addPoint([x.getTime(), parseInt(y[0])], true, true);
            activeLastPointToolip(chart2);

        },
        error: function () {
            alert("發送失敗");
        }

    });
}
char2 =null;
chart2 = Highcharts.chart('container2', {
    chart: {
        type: 'spline',
        marginRight: 10,
        events: {
            load: function () {
                //var series = this.series[0],
                //chart = this;
                //activeLastPointToolip(chart);
                /*setInterval(function () {
                var x = (new Date()).getTime(), // 当前时间
                y = getCpuData();// parseInt(cpudatalist[0])          // 随机值
                series.addPoint([x, y], true, true);
                activeLastPointToolip(chart);
                }, 1000);*/
                setInterval(function () {
                    requestMemData();
                }, 2000);
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
                    y: parseInt("10.45")
                });
            }
            return data;
        } ())
    }]
});

