/*
* 
*
* show highchart 
* 
*
* Copyright (c) 2018 Yang
*/
function treeClick(code) {
    $("#hdPrcd").val(code);
    showPointPlot();
    //在charframe中添加控件，完成数据刷新
}

$(document).ready(function () {
    $("#browser").treeview({
        toggle: function () {
            console.log("%s was toggled.", $(this).find(">span").text());
        },
        persist: "cookie",
        collapsed: true
    });
    $(".folder").click(function () {
        $('.folder').removeClass("selectedbold");
        $('.file').removeClass("selectedbold");
        $(this).addClass("selectedbold");
    });
    $(".file").click(function () {
        $('.folder').removeClass("selectedbold");
        $('.file').removeClass("selectedbold");
        $(this).addClass("selectedbold");
    });
    showtempchart();
});
Date.prototype.Format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份   
        "d+": this.getDate(), //日   
        "H+": this.getHours(), //小时   
        "m+": this.getMinutes(), //分   
        "s+": this.getSeconds(), //秒   
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度   
        "S": this.getMilliseconds() //毫秒   
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}
Date.prototype.AddHours = function (h) {
    this.setHours(this.getHours() + h);
    return this;
}

function GetPointJsonData() {
    var json = {
        "type": 'Para',
        "point": $("#hdPrcd").val(),
        "startTime": new Date().AddHours(-2).Format("yyyy-MM-dd HH:mm:ss"),
        "stopTime": new Date().Format("yyyy-MM-dd HH:mm:ss")
    }

    return json;
}

function showPointPlot() {
    $.ajax({
        type: "POST",
        url: "../Response/RealDataHandler.ashx",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(GetPointJsonData()),
        dataType: "json",
        success: function (result) {
            DrawPicture(result[0]);
            if (result[0]["statics"]) {
                $('#statics').empty();
                $('#statics').append(result[0]["statics"].toString());

            }
        },
        error: function (message) {
            $("#request-process-patent").html("从服务器获取数据失败！");
        }
    });
};
function DrawPicture(result) {
    
    var upper = result["upper"];
    var lower = result["lower"];
    var value = result["value"];
    var errdev = result["errdev"];
    var xAxis = result["xAxis"];
    var yAxis = result["yAxis"];
    var para_name = result["pointname"];
    if (xAxis == null) {
        alert('没有从数据库获取数据，请查看质量采集设置是否正确，或IH实时数据库是否正常！！');
        return;
    }
    var chart = new Highcharts.Chart({
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
            },
            categories: xAxis == null ? [1, 2, 3, 4, 5, 6] : xAxis,
            crosshair: true
        },
        yAxis: {
            minorGridLineWidth: 0,
            gridLineWidth: 0,
            alternateGridColor: null,
            plotBands: [{ // Light air
                from: Number.NEGATIVE_INFINITY,
                to: lower,
                color: 'rgba(255,0, 0, 0.4)',
                label: {
                    text: 'Fault',
                    style: {
                        color: '#606060'
                    }
                }
            }, { // Light breeze
                from: lower,
                to: lower + errdev / 100 * (value - lower),
                color: 'rgba(255, 255, 0, 0.4)',
                label: {
                    text: 'Warning',
                    style: {
                        color: '#606060'
                    }
                }
            }, { // Gentle breeze
                from: lower + errdev / 100 * (value - lower),
                to: upper - errdev / 100 * (upper - lower),
                color: 'rgba(0,255, 0, 0.4)',
                label: {
                    text: 'good',
                    style: {
                        color: '#606060'
                    }
                }
            }, { // Moderate breeze
                from: upper - errdev / 100 * (upper - lower),
                to: upper,
                color: 'rgba(255, 255, 0, 0.4)',
                label: {
                    text: 'Warning',
                    style: {
                        color: '#606060'
                    }
                }
            }, { // Fresh breeze
                from: upper,
                to: Number.POSITIVE_INFINITY,
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
                }
                //                                pointInterval: 30000, // one hour
                //                                pointStart: Date.UTC(2018, 4, 31, 0, 0, 0)
            }
        },
        series: [{ name: para_name, data: yAxis, tooltip: { value: ' ' } }]
    });
}


function showtempchart() {

    Highcharts.chart('container', {
        chart: {
            type: 'spline'
        },
        title: {
            text: 'Monthly Average Temperature'
        },

        xAxis: {
            categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
                'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
        },
        yAxis: {
            title: {
                text: 'Temperature'
            },
            labels: {
                formatter: function () {
                    return this.value + '°';
                }
            }
        },
        tooltip: {
            crosshairs: true,
            shared: true
        },
        plotOptions: {
            spline: {
                marker: {
                    radius: 4,
                    lineColor: '#666666',
                    lineWidth: 1
                }
            }
        },
        credits: {
            enabled: false // remove high chart logo hyper-link  
        },
        series: [{
            marker: {
                symbol: 'square'
            },
            data: [7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, {
                y: 26.5,
                marker: {
                    symbol: 'url(https://www.highcharts.com/samples/graphics/sun.png)'
                }
            }, 23.3, 18.3, 13.9, 9.6]

        }]
    });
}
