Highcharts.setOptions({
    global: {
        useUTC: false 
    }
});
window.onload = requestOracleData;
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
                }, 5000);
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


/*==========================================*/
//获取数据库数据
function requestOracleData() { 
    $.ajax({
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: "monitor.aspx/ReGetOracleInfo",
        success: function (data) {
            //ora = data.d.toString();
            getOracleChart(data.d.toString());

        },
        error: function () {
            alert("获取ORACLE信息失败");
        }

    });
}

function getOracleChart(ora){
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
										mouseOver: function() {
												this.slice();
										},
										// 鼠标移出时，收回突出显示
										mouseOut: function() {
												this.slice();
										},
										// 默认是点击突出，这里屏蔽掉
										click: function() {
												return false;
										}
								}
						}
				}
		},
		series: [{
				type: 'pie',
				name: '表空间使用率',
				data: [
						{
								name: '已使用表空间',
								y: parseFloat(ora),
								sliced: true, // 突出显示这个点（扇区），用于强调。
						},
						['剩余表空间',   100-parseFloat(ora) ],
				]
		}]
});
}
