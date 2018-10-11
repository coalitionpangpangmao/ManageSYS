/*
* 
*
* faultSearch
* 
*
* Copyright (c) 2018 Yang
*/
function keyhide() {    
    $('.search').mouseleave(function () {
        var event = window.event;
        var scrollX = document.documentElement.scrollLeft || document.body.scrollLeft;
        var scrollY = document.documentElement.scrollTop || document.body.scrollTop;
        var x = event.pageX || event.clientX + scrollX;
        // x = parseInt(x) - 200;
        var y = event.pageY || event.clientY + scrollY;
        var obj = $("#keytext");
        if (!
    (x > obj.offsetLeft && x < (obj.offsetLeft + obj.clientWidth) && y > obj.offsetTop && y < (obj.offsetTop + obj.clientHeight))) {
            $("#keytext").hide();
        }
    });

    }
//$(document).ready(function () {
//    $('.search').mouseleave(function () {
//                debugger;
//                var event = window.event;
//                var scrollX = document.documentElement.scrollLeft || document.body.scrollLeft;
//                var scrollY = document.documentElement.scrollTop || document.body.scrollTop;
//                var x = event.pageX || event.clientX + scrollX;
//                // x = parseInt(x) - 200;
//                var y = event.pageY || event.clientY + scrollY;
//                var obj = $("#keytext");
//                if (!
//            (x > obj.offsetLeft && x < (obj.offsetLeft + obj.clientWidth) && y > obj.offsetTop && y < (obj.offsetTop + obj.clientHeight))) {
//                    $("#keytext").hide();
//                }
//            });
//    });



function keySelectHistory(e) {
    var event = e || window.event;
    var scrollX = document.documentElement.scrollLeft || document.body.scrollLeft;
    var scrollY = document.documentElement.scrollTop || document.body.scrollTop;
    var x = event.pageX || event.clientX + scrollX;
    // x = parseInt(x) - 200;
    var y = event.pageY || event.clientY + scrollY;
    $("#keytext").css({ "position": "fix", "top": y + "px", "left": x + "px" });
    $('#keytext').show();
   
    postData();
}
function keySelectHistory2() {
    postData();
}
function postData() {
    $.ajax({
        type: "POST",
        url: "../Response/faultDbHandler.ashx",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(getSendData()),

        success: function (result) {
            $("#request-process-patent").html("从服务器获取数据成功！");
            $('#keytext').addClass("text");
            $('#keytext').html(result);
        },
        error: function (message) {
            $("#request-process-patent").html("从服务器获取数据失败！");
        }
    });
}
function getSendData() {
    var json = {
        "Equip": $('#listEq').val(),
        "key": $('#txtName').val()
    }
    return json;
}



function fillData(code) {
    $("#txtFtID").val(code);
    $("#keytext").hide();
    $("#btnShow").click();
}

