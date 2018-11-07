function comparisonByTime()
{
    var btime = document.getElementById("Btime");
    var etime = document.getElementById("Etime");
    $.ajax({
        url: "Comparison.aspx/GetComparisonByTimeData",
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "{'btime':'" + btime.value + "','etime':'" + etime.value + "'}",
        success: function (msg) {
            console.log("返回成功");
            var chartdata = JSON.parse(msg.d);
            console.log(chartdata.comparisonData);
            chartdata.comparisonData.forEach(function (value) {
                value.y = parseInt(value.y);
            });
            var chart = Highcharts.chart('container', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: '能耗比对'
                },
                xAxis: {
                    categories: chartdata.categories,
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: '消耗量'
                    }
                },

                series: [{data:chartdata.comparisonData}]
            });
        },
        error: function () {
            console.log("获取数据失败");
        }
    });
}





function comparisonByProcessName() {
    console.log("执行comparisonByProcessName");
    var btime = document.getElementById("Btime");
    var etime = document.getElementById("Etime");
    var consumptionPoint = document.getElementById("energyConsumptionPoint");
    var processName = document.getElementById("processName");
    var unitName = document.getElementById("department");
    $.ajax({
        url: "Comparison.aspx/GetComparisonByProcessName",
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data:"{'btime':'"+btime.value+"','etime':'"+etime.value+"', 'consumptionPoint':'"+consumptionPoint.value+"', 'processName':'"+processName.value+"', 'unitName':'"+unitName.value+"'}",
        //data:"{'btime':'2018-10-01', 'etime':'2018-10-23'}",
        success: function (msg) {
            console.log("返回成功");
            var chartdata = JSON.parse(msg.d);
            console.log(chartdata);
            console.log(typeof (chartdata.comparisonData));
            chartdata.comparisonData.forEach(function (value) {
                value.y = parseInt(value.y);
            });
            var chart = Highcharts.chart('container', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: '能耗比对'
                },
                xAxis: {
                    categories: chartdata.time,
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: '消耗量'
                    }
                },

                series: [{ data: chartdata.comparisonData }]
            });
        },
        error: function () {
            console.log("获取数据失败");
        }
    });
}

function getStatisticData() {

    var type = document.getElementById("statisticType");
    var time = document.getElementById("time");
    $.ajax({
        url: "Comparison.aspx/GetStatisticData",
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "{'type':'"+type.value+"', 'time':'"+time.value+"'}",
        success: function (data) {
            console.log("返回成功");
            var chartdata = JSON.parse(data.d);
            console.log(chartdata);
            chartdata.statisticData.forEach(function (value) {
                value.y = parseInt(value.y);
            });
            var chart = Highcharts.chart('container2', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: '能耗统计'
                },
                xAxis: {
                    categories: chartdata.units,
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: '消耗量'
                    }
                },

                series: [{ data: chartdata.statisticData }]
            });
        },
        error: function (err) {
            console.log("获取数据失败");
        }
    });
}