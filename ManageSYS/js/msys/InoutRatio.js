/*
* 
*
* show highchart 
* 
*
* Copyright (c) 2018 Yang
*/


$(document).ready(function () {
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
    var type = 1;
    if ($('#rdSort1').attr('checked'))
        type = 1;
    else if ($('#rdSort2').attr('checked'))
        type = 2;
    else if ($('#rdSort3').attr('checked'))
        type = 3;
    else if ($('#rdSort4').attr('checked'))
        type = 4;
    else
        type = 1;
    var json = {
        "type": type,
        "prod_code": $("#listProd").val(),
        "startTime": $('#txtBtime').val(),
        "stopTime": $('#txtEtime').val()
    }
    return json;
}

function showPointPlot() {
    $.ajax({
        type: "POST",
        url: "../Response/InoutRatioHandler.ashx",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(GetPointJsonData()),
        dataType: "json",
        success: function (result) {
            DrawPicture(result);
            if (result["statics"]) {
                $('#statics').empty();
                $('#statics').append(result["statics"].toString());

            }
        },
        error: function (message) {
            $("#request-process-patent").html("从服务器获取数据失败！");
        }
    });
};
function DrawPicture(result) {
    var xAxis = result["xAxis"];
    var inAxis = result["inAxis"];
    var outAxis = result["outAxis"];
    var ratioAxis = result["ratioAxis"];
    if (xAxis == null) {
        alert('没有从数据库获取数据，请查看所选条件及生产报表数据信息是否正常！！');
        return;
    }
    Highcharts.chart('container', {
        chart: {
            events: {
                addSeries: function () {
                    var label = this.renderer.label('A series was added, about to redraw chart', 100, 120).attr({
                        fill: Highcharts.getOptions().colors[0],
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
            categories: xAxis,
            crosshair: true
        }],
        yAxis: [{
            labels: {
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
        series: [{ name: '投入', type: 'column', yAxis: 0, data: inAxis, tooltip: { valueSuffix: ' Kg' } },
       { name: '产出', type: 'column', yAxis: 0, data: outAxis, tooltip: { valueSuffix: ' Kg' } },
       { name: '投入产出比', type: 'spline', yAxis: 1, data: ratioAxis, tooltip: { valueSuffix: ' %' } }
        ]

    });
}


function showtempchart() {
    Highcharts.chart('container', {
        chart: {
            events: {
                addSeries: function () {
                    var label = this.renderer.label('A series was added, about to redraw chart', 100, 120).attr({
                        fill: Highcharts.getOptions().colors[0],
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
            categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
    'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
            crosshair: true
        }],
        yAxis: [{
            labels: {
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
        series: [{ name: '投入', type: 'column', yAxis: 0, data: [216.4, 194.1, 95.6, 54.4, 29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5], tooltip: { valueSuffix: ' Kg' } },
       { name: '产出', type: 'column', yAxis: 0, data: [116.4, 294.1, 195.6, 154.4, 129.9, 91.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5], tooltip: { valueSuffix: ' Kg' } },
       { name: '投入产出比', type: 'spline', yAxis: 1, data: [99, 56, 95.6, 80, 29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 90], tooltip: { valueSuffix: ' %' } }
        ]

    });

}


