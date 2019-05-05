Date.prototype.format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份
        "d+": this.getDate(), //日
        "h+": this.getHours(), //小时
        "m+": this.getMinutes(), //分
        "s+": this.getSeconds(), //秒
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度
        "S": this.getMilliseconds() //毫秒
    };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ?
              (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
}

function getOptimizeData(list) {
    var date = new Date();
    $.ajax({
        type: "POST",
        url: "../Response/OptimizeHandler.ashx",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            'time':date.format("yyyy-MM-dd hh:mm:ss")
        }),
        dataType: "json",
        success: function (result) {
            var x = date.getTime();
            var y = result.data;
            list.addPoint([x, y], true, true);
        },
        error: function (message) {
            $("#request-process-patent").html("从服务器获取数据失败！");
        }
    });
};

Highcharts.setOptions({
    global: {
        useUTC: false
    }
});
function activeLastPointToolip(chart) {
    var points = chart.series[0].points;
    chart.tooltip.refresh(points[points.length - 1]);
}
var chart = Highcharts.chart('container', {
    chart: {
        type: 'spline',
        marginRight: 10,
        events: {
            load: function () {
                var series = this.series[0],
					chart = this;
                activeLastPointToolip(chart);
                setInterval(function () {
                    var x = (new Date()).getTime(), // 当前时间
						y = Math.random();          // 随机值
                    //series.addPoint([x, y], true, true);
                    getOptimizeData(series);
                    activeLastPointToolip(chart);
                }, 30000);
            }
        }
    },
    title: {
        text: '加香加料比例优化'
    },
    xAxis: {
        type: 'datetime',
        tickPixelInterval: 150
    },
    yAxis: {
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
        name: '加料比例',
        data: (function () {
            // 生成随机值
            var data = [],
				time = (new Date()).getTime(),
				i;
            for (i = -19; i <= 0; i += 1) {
                data.push({
                    x: time + i * 1000,
                    y: Math.random()
                });
            }
            return data;
        }())
    }]
});