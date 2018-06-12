<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChgPsd.aspx.cs" Inherits="Authority_ChgPsd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="js/jquery.js"></script>

<script language="javascript" type = "text/javascript">
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
    
   <div class="formbody">
    
    <div class="formtitle"><span>基本信息</span></div>
    
    <ul class="forminfo">
     <li><label>旧密码</label><asp:TextBox ID="txtold" runat="server" CssClass = "dfinput" 
             TextMode="Password"></asp:TextBox> <i></i></li>
    <li><label>新密码</label><asp:TextBox ID="txtPwd1" runat="server" CssClass = "dfinput" 
            TextMode="Password"></asp:TextBox> <i>建议用8位以上字母数字结合的密码</i></li>
    <li><label>确认密码</label><asp:TextBox ID="txtpwd2" runat="server" 
            CssClass = "dfinput" TextMode="Password"></asp:TextBox><i>请再次输入密码</i></li>
   
    <li><label>&nbsp;</label><asp:Button ID="btnConfirm" runat="server" Text="确认修改"   
            CssClass = "btn" onclick="btnConfirm_Click"/></li>
    </ul>
    
    
    </div>
  
    </form>
</body>
</html>
