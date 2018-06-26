<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MateriaMain.aspx.cs" Inherits="Craft_MateriaMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server" >
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>物料管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/select.css" rel="stylesheet" type="text/css" />
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
function tab1Click(code) {
    $('#tabtop1').click();
    debugger;
    $("#Frame1").contents().find("'*[id$=hdcode]'").attr('value', code);
    $("#Frame1").contents().find("'*[id$=btnUpdate]'").click();
}
function tab2Click(code) {
    debugger;
    $('#tabtop2').click();
    $("#Frame2").contents().find("'*[id$=hdcode]'").attr('value', code);

    $("#Frame2").contents().find("'*[id$=btnUpdate]'").click();
}
    </script>
</head>
<body>
    <form id="Form1" runat="server">
    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="#">工艺管理</a></li>
            <li><a href="#">物料管理</a></li>
        </ul>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="mainbox">
        <div class="mainleft">
             <div class="leftinfo">
     <div class="listtitle">物料分类</div>
    <% = tvHtml %>
    </div>
        </div>
        <!--mainleft end-->
        <div class="mainright">        
            <div id="usual1" class="usual">
                <div class="itab">
                    <ul>
                        <li ><a href="#tab1" class="selected" id = "tabtop1">物料分类</a></li>
                        <li ><a href="#tab2" id = "tabtop2">物料信息维护</a></li>                      
                    </ul>
                </div>
                <div id="tab1" class="tabson">
                    <iframe id = "Frame1" name="Frame1" src="Materia.aspx" height="400" scrolling="no" style="width: 80%; position: absolute">
                    </iframe>
                </div>
                <div id="tab2" class="tabson">
                    <iframe id = "Frame2" name= "Frame2" src="MateriaDetail.aspx" height="400" scrolling="no" style="width: 80%; position: absolute">
                    </iframe>
                </div>
            </div>        
        </div>
        <!--mainright end-->
        	<script type="text/javascript">
        	    $("#usual1 ul").idTabs(); 
    </script>
   
    </div>
    </form>
</body>
</html>
