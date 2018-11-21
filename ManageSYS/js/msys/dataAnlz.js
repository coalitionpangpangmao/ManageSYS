/*
* 
*
* show highchart 
* 
*
* Copyright (c) 2018 Yang
*/

$(function () {
    // initialization chart and actions 
    showtempchart();
    $.ajax({
        type: "POST",
        url: "../Response/RealDataHandler.ashx",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(GetEquipJsonData()),
        dataType: "json",
        success: function (result) {
           for(var i = 0; i< result.length;i++) 
           {
                DrawPicture(result[i]);
                $('#statics').append(result[i]["statics"].toString());
            }
        },
        error: function (message) {
            $("#request-process-patent").html("从服务器获取数据失败！");
        }
    });
    $("#clear-button").click(function () { clearPlot(); });
    $("#refresh-button").click(function () { showPointPlot(); });

    function GetPointJsonData() {
        var json = {
            "type": 'Para',
            "point": $('#listpoint').val(),
            "startTime": $('#txtstartTime').val(),
            "stopTime": $('#txtendTime').val()
        }
        return json;
    }
    function GetEquipJsonData() {
        var json = {
            "type": 'Equip',
            "point": $('#hdEquip').val(),
            "startTime": $('#txtstartTime').val(),
            "stopTime": $('#txtendTime').val()
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
                categories: xAxis,
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
            series: [{ name: para_name, data: yAxis, tooltip: { value: ' '}}]
        });
    }

    function showtempchart() {
        debugger;
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
                crosshair: true
            },
            yAxis: {
                minorGridLineWidth: 0,
                gridLineWidth: 0,
                alternateGridColor: null,
                plotBands: [{ // Light air
                    from: Number.NEGATIVE_INFINITY,
                    to: 5,
                    color: 'rgba(255,0, 0, 0.4)',
                    label: {
                        text: 'Fault',
                        style: {
                            color: '#606060'
                        }
                    }
                }, { // Light breeze
                    from: 5,
                    to: 7,
                    color: 'rgba(255, 255, 0, 0.4)',
                    label: {
                        text: 'Warning',
                        style: {
                            color: '#606060'
                        }
                    }
                }, { // Gentle breeze
                    from: 7,
                    to: 13,
                    color: 'rgba(0,255, 0, 0.4)',
                    label: {
                        text: 'good',
                        style: {
                            color: '#606060'
                        }
                    }
                }, { // Moderate breeze
                    from: 13,
                    to: 15,
                    color: 'rgba(255, 255, 0, 0.4)',
                    label: {
                        text: 'Warning',
                        style: {
                            color: '#606060'
                        }
                    }
                }, { // Fresh breeze
                    from: 15,
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
            series: [{ name: "数据点趋势", data: [5,4.5,8,11,13,9.8,16,12,8.9,7,9], tooltip: { value: ' ' } }]
        });
    }
});
     