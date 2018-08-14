/*
* 
*
* show highchart 
* 
*
* Copyright (c) 2018 Yang
*/
$(document).ready(function () {
    showPointPlot();
});

   
    function GetJsonData() {
        var json = {
            "prod": $('#listRecipe').val(),           
            "startTime": $('#txtBtime').val(),
            "stopTime": $('#txtEtime').val()
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
                $('#statics').append(result[0]["statics"].toString());
            },
            error: function (message) {
                $("#request-process-patent").html("从服务器获取数据失败！");
            }
        });
    };

    function DrawPicture(result) {

        var xAxis = result["xAxis"];
        var yAxis_set = result["yAxis"];
        var yAxis_real = result["yAxis"];
        var yAxis_rate = result["yAxis"];
        var para_name = result["para_name"];
        var chart = new Highcharts.Chart({
            chart: {
                renderTo: 'container',
                type: 'spline',          
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
            title: { text: '成品批间对比' },
            xAxis: [{                
                    type: 'datetime',
                    labels: {
                        overflow: 'justify'
                    },
                    categories: xAxis,
                    crosshair: true             
            }],
            yAxis: [{
                labels: {
                    format: '{value} ',
                    style: { color: Highcharts.getOptions().colors[1] }
                }, title: {
                    text: '设定/实际',
                    style: { color: Highcharts.getOptions().colors[1] }
                }
            }, { // Secondary yAxis
                title: {
                    text: '批间波动',
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
            series: [{ name: '设定', type: 'column', yAxis: 0, data: yAxis_set, tooltip: { valueSuffix: ' Kg' } },
           { name: '实际', type: 'column', yAxis: 0, data: yAxis_real, tooltip: { valueSuffix: ' Kg' } },
           { name: '波动', type: 'spline', yAxis: 1, data: yAxis_rate, tooltip: { valueSuffix: ' %' } }
            ]

        });

    }


     