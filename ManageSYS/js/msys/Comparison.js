
$(document).ready(function () {
    $("#browser").treeview({
        toggle: function () {
            console.log("%s was toggled.", $(this).find(">span").text());
        },
        persist: "cookie",
        collapsed: true
    });
    showtempchart();
    // activate the button
    $('#btnCompare').click(function () {
        showPointPlot();
    });
});

function showPointPlot() {   
    var chart = $('#container').highcharts();
    while (chart.series.length) {
        chart.series[0].remove(false);
    }
    $("#cklistPara tbody").find("tr").each(function () {
        debugger;
        var item = $(this).find('label').text();;
        var json = {
            "type": "Para",
            "point": $(this).find('input').val(),
            "startTime": item.substr(item.indexOf('_')+1,19),
            "stopTime": item.substr(item.indexOf('~') + 1)
        }     
        $.ajax({
            type: "POST",
            url: "../Response/RealDataHandler.ashx",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(json),
            dataType: "json",
            success: function (result) {
                debugger;
                var xAxis = result[0]["xAxis"];
                var yAxis = result[0]["yAxis"];
                var para_name = result[0]["para_name"];
                chart.addSeries({
                    name: para_name,                  
                    data: yAxis
                });
                if (result[0]["statics"])
                    $('#statics').append(result[0]["statics"].toString());
            },
            error: function (message) {
                $("#request-process-patent").html("从服务器获取数据失败！");
            }
        });
        
    });
      
   
};
function refreshChart() {
    var chart = $('#container').highcharts();
    while (chart.series.length) {
        chart.series[0].remove(false);
    }
    chart.addSeries({
        name: 'temprature1',
        marker: {
            symbol: 'square'
        },
        data: [7.0, 6.9, 9.5, 10, 18.2, 21.5, 25.2, {
            y: 26.5,
            marker: {
                symbol: 'url(https://www.highcharts.com/samples/graphics/sun.png)'
            }
        }, 23.3, 18.3, 13.9, 9.6]

    });
    chart.addSeries({
        name: 'temprature2',
        marker: {
            symbol: 'diamond'
        },
        data: [{
            y: 3.9,
            marker: {
                symbol: 'url(https://www.highcharts.com/samples/graphics/snow.png)'
            }
        }, 4.2, 5.7, 8.5, 11.9, 9, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8]
    });
}
function drawPicture() {
    // create the chart
    $('#container').highcharts({
        chart: {
            events: {
                addSeries: function () {
                    var label = this.renderer.label('A series was added, about to redraw chart', 100, 120)
                                .attr({
                                    fill: Highcharts.getOptions().colors[0],
                                    padding: 10,
                                    r: 5,
                                    zIndex: 8
                                })
                                .css({
                                    color: '#FFFFFF'
                                })
                                .add();
                    setTimeout(function () {
                        label.fadeOut();
                    }, 1000);
                }
            }
        },
        title: { text: '过程数据比对' },
        yAxis: { title: { text: '值' } },
        legend: { layout: 'vertical', align: 'right', verticalAlign: 'middle' },
        plotOptions: { series: { cursor: 'pointer', pointStart: 0 } },
        credits: {
            enabled: false
        },
        responsive: {
            rules: [{
                condition: { maxWidth: 500 },
                chartOptions: {
                    legend: { layout: 'horizontal', align: 'center', verticalAlign: 'bottom' }
                }
            }]
        }
    });
 
}

function treeClick(obj) {
    var code = obj.value;
    if (obj.checked) {
        $("#hidecode").val(code);
        $("#btnAdd").click();
    }
    else {
        $("#hidecode").val(code);
        $("#btnDel").click();
    }
}



function showtempchart() {
    drawPicture();
    var chart = $('#container').highcharts();
    
    chart.addSeries({
        name: 'temprature1',
        marker: {
            symbol: 'square'
        },
        data: [7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, {
            y: 26.5,
            marker: {
                symbol: 'url(https://www.highcharts.com/samples/graphics/sun.png)'
            }
        }, 23.3, 18.3, 13.9, 9.6]

    });
    chart.addSeries({
        name: 'temprature2',
        marker: {
            symbol: 'diamond'
        },
        data: [{
            y: 3.9,
            marker: {
                symbol: 'url(https://www.highcharts.com/samples/graphics/snow.png)'
            }
        }, 4.2, 5.7, 8.5, 11.9, 15.2, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8]
    });
}
