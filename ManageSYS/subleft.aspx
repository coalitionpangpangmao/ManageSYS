<%@ Page Language="C#" AutoEventWireup="true" CodeFile="subleft.aspx.cs" Inherits="subleft" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="css/style.css" rel="stylesheet" type="text/css" />
<script type ="text/javascript" language="JavaScript" src="js/jquery.js"></script>

<script type="text/javascript">
    function showNav(s) {
        for (var i = 0; i < s.length; i++) { var code = s.substr(i, 1); if ('1' == code) $('.menu' + i).show(); else $('.menu' + i).hide(); };
    }
    $(function () {
        //导航切换
        $(".menuson li").click(function () {
            $(".menuson li.active").removeClass("active")
            $(this).addClass("active");
        });

        $('.title').click(function () {
            var $ul = $(this).next('ul');
            $('dd').find('ul').slideUp();
            if ($ul.is(':visible')) {
                $(this).next('ul').slideUp();
            } else {
                $(this).next('ul').slideDown();
            }
        });
    })	
</script>


</head>

<body style="background:#f0f9fd;"  >
<form id = "form1" runat = "server" >

 <dl class="leftmenu" >  
     <%=SysHtml%>
        
      <!--      <dd> 
     <div  class="title">
    <a  href="../EnergeStatus/E_Steam.aspx" target="rightFrame">能源分类统计</a>
    </div>
    	<ul class="menuson">
        <li class = "menu0" ><cite></cite><a  href="../EnergeStatus/E_Steam.aspx" target="rightFrame">蒸汽统计</a><i></i></li>
         <li class = "menu0"><cite></cite><a  href="../UserSet/OrderStatus.aspx" target="rightFrame">汽油统计</a><i></i></li>
         <li class = "menu0"><cite></cite><a  href="../UserSet/ProductStatus.aspx" target="rightFrame">制冷量统计</a><i></i></li>
        
         <li class = "menu0" ><cite></cite><a  href="../UserSet/UserSet.aspx" target="rightFrame">天然气统计</a><i></i></li>
         <li class = "menu0"><cite></cite><a  href="../UserSet/OrderStatus.aspx" target="rightFrame">柴油统计</a><i></i></li>
         <li class = "menu0"><cite></cite><a  href="../UserSet/ProductStatus.aspx" target="rightFrame">压缩空气统计</a><i></i></li>
        </ul>    
    </dd>    
    -->
    </dl>
    </form>
</body>
</html>
