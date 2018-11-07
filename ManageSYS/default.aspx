<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" type="text/css" href="js/jquery-EasyUI/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="js/jquery-EasyUI/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="js/jquery-EasyUI/demo.css" />
    <script type="text/javascript" src="js/jquery-EasyUI/jquery.min.js"></script>
    <script type="text/javascript" src="js/jquery-EasyUI/jquery.easyui.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
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
                    <%=tvhtml %>
                </div>
                <!--leftinfo end-->
            </div>

            <div class="mainright">
                <iframe height="500px" width="100%" frameborder="0" src="start_drilldown.htm" scrolling="no"
                    name="chartframe" id="chartframe" title="chartframe"
                    style="position: relative"></iframe>
                <div class="dflist">
                    <div id="p" class="easyui-panel" title="逾期提期" data-options="iconCls:'icon-save',collapsible:true">
                        <%=warnHtml %>
                    </div>
                </div>
            </div>
        </div>
    
    </form>
</body>

</html>
