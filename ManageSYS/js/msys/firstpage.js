

$(document).ready(function () {
    $(window).resize(function () {
        var width_frm = $(document.body).width();
        var height_frm = $(document.body).height();
        var width_div = width_frm / 2;
        var height_div = height_frm / 2;

        $('#container1').css("height", height_div);
        $('#container1').css("width", width_div);

        $('#container2').css("height", height_div);
        $('#container2').css("width", width_div);
    });
    showPlot();
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



function showPlot() {
    var sendData = "date=" + new Date().Format("yyyy-MM-dd")+ "";   
   // var sendData = "date=2018-09-10";
    $.ajax({
        type: "POST",
        url: "Response/ProductinfoHandler.ashx",
        data: sendData,
        dataType: "json",
        success: function (result) {            
            Drawchart(result["info"][0], 'container1','生产计划完成情况');
            Drawchart(result["info"][1], 'container2','产品产出情况');
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
function Drawchart(result, id,nameinfo) {
  
    if (result == null) {
        alert('没有从数据库获取数据，请查看质量采集设置是否正确，或IH实时数据库是否正常！！');
        return;
    }
    Highcharts.chart(id, {
        chart: {
            type: 'pie'
        },
        title: {
            text: null
        },
        legend: {
            enabled: false
        },
        tooltip: {
            headerFormat: '<span style="font-size:8px">{series.name}</span><br>',
            pointFormat: '<span style="color:{point.color},font-size:4px">{point.name}</span>: <b>{point.y:.2f}%</b> of total<br/>'
        },
        series: [{
            name: nameinfo,
            colorByPoint: true,
            data:result["productinfo"]
            }],
        credits: {
            enabled: false
        },
        drilldown: {
            series: result["productseries"]
        }
    });
}