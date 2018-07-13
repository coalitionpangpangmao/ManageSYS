<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="Product_Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server" >
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>生产报表</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
    <link rel="stylesheet" href="../js/jquery-treeview/jquery.treeview.css" />
	<link rel="stylesheet" href="../js/jquery-treeview/screen.css" />
	<script type="text/javascript" src="../js/jquery-treeview/jquery.cookie.js"></script>
	<script src="../js/jquery-treeview/jquery.treeview.js" type="text/javascript"></script>

	<script type="text/javascript">
	$(document).ready(function(){
		$("#browser").treeview({
			toggle: function() {
				console.log("%s was toggled.", $(this).find(">span").text());
			}
		});
});
	function hidetab(tab) {	   
	    $(tab).parent().hide();
	    $("#tabtop1").click();   
	}
	function addclick() {
	    // $("#hidecode").val($("#hidecode").val() + 1);
	    $("#tabtop2").parent().show();	  
	    $("#tabtop2").click();
	}
    </script>
</head>
<body>
    <form id="Form1" runat="server">
    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="#">生产管理</a></li>
            <li><a href="#">生产报表</a></li>
        </ul>
    </div>
    <asp:HiddenField ID="hidecode" runat="server" Value="1" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="mainbox">
        <div class="mainleft">
             <div class="leftinfo">
     <div class="listtitle">报表</div>
    <% = tvHtml %>
    </div>
        </div>
        <!--mainleft end-->
        <div class="mainright">        
            <div id="usual1" class="usual">
                <div class="itab">
                    <ul>  
                    <li ><a href="#tab1" id = "tabtop1" class="selected">当前生产详情</a></li>    
                        <li ><a href='#tab2' id = 'tabtop2'>报表2</a><span  onclick ="hidetab('#tabtop2');"></span></li> 
                    </ul>
                </div>
                </div>
                <div id="tab1" class="tabson">
                <iframe name= "Frame1" src="../Report/原料每日用量.htm" height="400" 
                        style="width: 100%; position: relative; ">
                    </iframe>
            </div>
            <div id='tab2' class='tabson'><iframe name= 'Frame2' src='../Report/产入产出表.htm' height='400'  style='width: 100%; position: relative; '></iframe></div>
     
            </div>        
        </div>
        <!--mainright end-->
        	<script type="text/javascript">
        	    $("#usual1 ul").idTabs(); 
    </script>

    </form>
</body>
</html>
