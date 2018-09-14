<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="test_Default" %>
<%@ Register src="MSYSGridView.ascx" tagname="GridView" tagprefix="MSYS" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Basic ComboTree - jQuery EasyUI Demo</title>
	<link rel="stylesheet" type="text/css" href="../js/jquery-EasyUI/themes/default/easyui.css"/>
	<link rel="stylesheet" type="text/css" href="../js/jquery-EasyUI/themes/icon.css"/>
	<link rel="stylesheet" type="text/css" href="../js/jquery-EasyUI/demo.css"/>
	<script type="text/javascript" src="../js/jquery-EasyUI/jquery.min.js"></script>
	<script type="text/javascript" src="../js/jquery-EasyUI/jquery.easyui.min.js"></script>
 <script type="text/javascript">
     $(function () {
         $("#cc").combotree({
             url: 'FlowChartHandler.ashx',//Action，获取树的信息  

         });
     });
 </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <input id="cc" class="easyui-combotree"  style="width:200px;"/>  
    </div>
        <MSYS:GridView ID ="gridview" runat ="server" />
    </form>

     <%-- <td colspan="8" align="center">
                                                    <asp:Button ID="btnAdd" runat="server"
                                    CssClass="btnadd  auth" Text="新增" OnClick="btnAdd_Click" />
                                                       <asp:Button ID="btnModify" runat="server" Text="保存" CssClass="btnview  auth" OnClick ="btnModify_Click" />
                                                    <asp:Button ID="btnReset" runat="server" Text="重置" CssClass="btnset"  OnClick ="btnReset_Click"/>
                                                 

                                                </td>--%>
  
</body>
</html>
