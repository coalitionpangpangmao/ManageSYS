<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChgPsd.aspx.cs" Inherits="Authority_ChgPsd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="../css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../js/jquery.js"></script>

<script language="javascript">
    $(function () {
        $('.error').css({ 'position': 'absolute', 'left': ($(window).width() - 490) / 2 });
        $(window).resize(function () {
            $('.error').css({ 'position': 'absolute', 'left': ($(window).width() - 490) / 2 });
        })
    });  
</script> 


</head>
<body>
    <form id="form1" runat="server">
	<div class="place">
    <span>位置：</span>
    <ul class="placeul">
    <li><a href="#">首页</a></li>
    <li><a href="#">更改密码</a></li>
    </ul>
    </div>
    
    <div class="error">
    
    <h2>非常遗憾，您访问的页面不存在！</h2>
  
    <div class="reindex"><a href="#" target="_parent">确认</a></div>
    
    </div>
    </form>
</body>
</html>
