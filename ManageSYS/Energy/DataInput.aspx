<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataInput.aspx.cs" Inherits="Energy_DataInput" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>能源数据手动录入</title>
<link href="../css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../js/jquery.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $(".click").click(function () {
            $(".tip").fadeIn(200);
        });

        $(".tiptop a").click(function () {
            $(".tip").fadeOut(200);
        });

        $(".sure").click(function () {
            $(".tip").fadeOut(100);
        });

        $(".cancel").click(function () {
            $(".tip").fadeOut(100);
        });

    });
</script>
</head>

<body>
<form id="Form1" runat = "server">
	<div class="place">
    <span>位置：</span>
    <ul class="placeul">
    <li><a href="#">能源管理</a></li>
    <li><a href="#">数据手动录入</a></li>
    </ul>
    </div>
    
    <div class="rightinfo">    
 <table class="tablelist">    	
        <tbody>
        <tr>
        <td width="100">数据类型</td>
        <td><asp:TextBox ID="txtID" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td  width="100">工艺段</td>
        <td><asp:TextBox ID="TextBox1" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td  width="100">参数值</td>
        <td><asp:TextBox ID="TextBox2" runat="server" class="dfinput1"    ></asp:TextBox></td>
      
        </tr> 
        <tr>
        <td width="100">父级标识</td>
        <td><asp:TextBox ID="TextBox3" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td  width="100">班组</td>
        <td><asp:TextBox ID="TextBox4" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td  width="100">班次</td>
        <td><asp:TextBox ID="TextBox5" runat="server" class="dfinput1"    ></asp:TextBox></td>
      
        </tr>
     
        </tbody>
        </table>
        <div class="tools">
    
    	<ul class="toolbar">
        <li class="click"><span><img src="../images/t01.png" /></span>添加</li>
        <li class="click"><span><img src="../images/t02.png" /></span>修改</li>
        <li><span><img src="../images/t03.png" /></span>删除</li>
        <li><span><img src="../images/t04.png" /></span>统计</li>
        </ul>
    
    </div>
         <table class="tablelist">    
                <thead>
    	<tr>
        <th colspan="6">
        查看详情
        </th>
        </tr>
        </thead>
        <tbody>
        <tr>
        <td colspan="6">
          <asp:GridView ID="GridView1" runat="server" Width="1200px" CssClass="datable" border="0"
            CellPadding="2" CellSpacing="1">
            <RowStyle CssClass="lupbai" />
           
            <HeaderStyle CssClass="lup" />
            <AlternatingRowStyle CssClass="trnei" />
        </asp:GridView>
        </td>
        </tr></tbody>
    </table>
    
   
    
    
    
    <div class="tip">
    	<div class="tiptop"><span>提示信息</span><a></a></div>
        
      <div class="tipinfo">
        <span><img src="../images/ticon.png" /></span>
        <div class="tipright">
        <p>是否确认对信息的修改 ？</p>
        <cite>如果是请点击确定按钮 ，否则请点取消。</cite>
        </div>
        </div>
        
        <div class="tipbtn">
        <input name="" type="button"  class="sure" value="确定" />&nbsp;
        <input name="" type="button"  class="cancel" value="取消" />
        </div>    
    </div>
    </div>   
        <script type="text/javascript">
            $('.tablelist tbody tr:odd').addClass('odd');
	</script>   
</form>
</body>
</html>
