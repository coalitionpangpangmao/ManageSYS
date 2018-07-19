<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery.js"></script>
</head>
<body>
    <form id="form2" runat="server">
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">首页</a></li>
                <li><a href="#"></a></li>
            </ul>
        </div>
        <div class="mainbox">
            <div class="mainleft">

                <div class="leftinfo">
                    <div class="listtitle">待办事项</div>
                    <ul class="newlist">
                        <li><a href="Approval/APRVMonthPlan.aspx" target="rightFrame">业务审批</></li>
                        <li><a href="Craft/Recipe.aspx" target="rightFrame">配方编辑</a></li>
                        <li><a href="Product/Plan.aspx" target="rightFrame">生产计划排产</a></li>
                        <li><a href="Product/StorageMater.aspx" target="rightFrame">出入库确认</a></li>
                        <li><a href="Quality/CraftEvent.aspx"  target="rightFrame">工艺事件确认</a></li>
                        <li><a href="Quality/EventDeal.aspx"  target="rightFrame">工艺事件处理</a></li>
                        <li><a href="Device/MtncExe.aspx"  target="rightFrame">设备维保</a></li>
                         <li><a href="Device/RepairExe.aspx"  target="rightFrame">设备维修</a></li>
                        <li><a href="Device/LbrctExe.aspx"  target="rightFrame">设备润滑</a></li>
                    </ul>
                </div>
                <!--leftinfo end-->
            </div>
            <!--mainleft end-->


            <div class="mainright">
                <div class="dflist">
                    <div class="listtitle">月度完成情况</div>
                    <iframe name="chartframe" src="ChartView/start_drilldown.htm" height="280" scrolling="no" width="800"></iframe>
                </div>


                <div class="dflist">
                    <div class="listtitle">信息统计</div>
                    <ul class="newlist">
                        <li><i>月产量：</i>2535462</li>
                        <li><i>库存：</i>5546</li>
                        <li><i>计划产量：</i>2315</li>
                        <li><i>未完成：</i>1585</li>
                        <li><i>异常：</i>5342</li>
                    </ul>
                </div>
            </div>
            <!--mainright end-->
        </div>
    </form>
</body>
<script type="text/javascript">
    setWidth();
    $(window).resize(function () {
        setWidth();
    });
    function setWidth() {
        var width = ($('.leftinfos').width() - 12) / 2;
        $('.infoleft,.inforight').width(width);
    }
</script>
</html>
