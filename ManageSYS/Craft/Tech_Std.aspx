<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tech_Std.aspx.cs" Inherits="Craft_Tech_Std" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server" >
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>技术标准管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/select.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
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
function treeClick(code) {
    $("#txtCode").val(code) ;
    $("#btnUpdate").click();
}
function tabClick(code) {
    $("#hideprc").attr('value', code);
    $("#UpdateGrid").click();
}

    </script>
</head>
<body>
    <form id="Form1" runat="server">
    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="#">工艺管理</a></li>
            <li><a href="#">技术标准管理</a></li>
        </ul>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="mainbox">
        <div class="mainleft">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>   
             <div class="leftinfo">
     <div class="listtitle">工艺标准</div>
    <% = tvHtml %>
    </div>
    </ContentTemplate>
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID = "btnModify" />
    </Triggers>
    </asp:UpdatePanel>
        </div>
        <!--mainleft end-->
        <div class="mainright">
        <table class = "dflist">
        <tr>
        <td colspan="2">
           <div class="listtitle">
                工艺标准信息<span style="position: relative; float: right" >                    
                   <asp:Button ID="btnModify" runat="server"  CssClass = "btnmodify" Text = "保存"
                    OnClick = "btnModify_Click"/>     
                    <asp:Button ID="btnUpdate" runat="server"  CssClass = "btnhide"
                    OnClick = "btnUpdate_Click"/>               
                </span>
            </div>
            </td>
            </tr>
            <tr>
             <td colspan="2">
             <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>  
            <table class="tablelist">
                <tbody>
                    <tr>
                        <td width="100">
                            工艺标准名
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" class="dfinput1"></asp:TextBox>
                        </td>
                        <td width="100">
                            标准编码
                        </td>
                        <td>
                            <asp:TextBox ID="txtCode" runat="server" class="dfinput1"></asp:TextBox>
                        </td>
                        <td width="100">
                            产品编码
                        </td>
                        <td>
                            <asp:TextBox ID="txtPro" runat="server" class="dfinput1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="100">
                            标准版本号
                        </td>
                        <td>
                            <asp:TextBox ID="txtVersion" runat="server" class="dfinput1"></asp:TextBox>
                        </td>
                        <td width="100">
                            执行日期
                        </td>
                        <td>
                            <asp:TextBox ID="txtExeDate" runat="server" class="dfinput1"></asp:TextBox>
                        </td>
                        <td width="100">
                            结束日期
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndDate" runat="server" class="dfinput1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="100">
                            受控状态
                        </td>
                        <td>
                            <asp:DropDownList ID="listStatus" runat="server"  CssClass = "drpdwnlist">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td width="100">
                            编制人
                        </td>
                        <td>
                            <asp:TextBox ID="txtCreator" runat="server" class="dfinput1"></asp:TextBox>
                        </td>
                        <td width="100">
                            编制日期
                        </td>
                        <td>
                            <asp:TextBox ID="txtCrtDate" runat="server" class="dfinput1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="100">
                            编制部门
                        </td>
                        <td>
                            <asp:DropDownList ID="listCrtApt" runat="server" CssClass = "drpdwnlist">
                            </asp:DropDownList>
                        </td>
                        <td width="100">
                            是否有效
                        </td>
                        <td width="100">
                            <asp:CheckBox ID="ckValid" runat="server" Text=" " />
                        </td>
                        <td width="100">
                            描述
                        </td>
                        <td>
                            <asp:TextBox ID="txtDscpt" runat="server" class="dfinput1"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
            </ContentTemplate>
            <Triggers>
            <asp:AsyncPostBackTrigger ControlID = "btnUpdate" />
            </Triggers>
            </asp:UpdatePanel>
        </td></tr>
           <tr>
        <td colspan="2">
           <div class="listtitle">
                从标准   
               <asp:DropDownList ID="listtech" runat="server" CssClass = "drpdwnlist">
               </asp:DropDownList> 
               复制为标准  
               <asp:DropDownList ID="listtechC" runat="server" CssClass = "drpdwnlist">
               </asp:DropDownList>              
                   <asp:Button ID="btnCopy" runat="server"  CssClass = "btnmodify" Text = "复制"
                    OnClick = "btnCopy_Click"/> 
            </div>
            </td>
            </tr>
        <tr>
        <td valign="top" width="200">
           <div style="margin-top: 10px; width: 200px; height: 400px;">                  
     <div class="listtitle">工序导航</div>
         <% = subtvHtml %></div>
        </td>
        <td valign="top" >        
          <table class = "dfsublist"> 
          <tr>
          <td>
           <div class="listtitle">
                工艺参数标准表<span style="position: relative; float: right">                
                        <asp:Button ID="btnCkAll" runat="server" CssClass="btnset" Text="全选" onclick="btnCkAll_Click" />
                         <asp:Button ID="btnDelSel" runat="server" CssClass="btndel" Text="删除" onclick="btnDelSel_Click" />
                    <asp:HiddenField ID="hideprc" runat="server" Value = "7020201" />
                    <asp:Button ID="UpdateGrid" runat="server" CssClass = "btnhide" OnClick = "UpdateGrid_Click" />
                    </span>
            </div>
            </td></tr>
            <tr><td>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>   
                <div   style="overflow: scroll; width: 650px;">
            <asp:GridView ID="GridView1" runat="server" class="grid" AllowPaging="True" 
                        AutoGenerateColumns="False" >
                <Columns>
                    <asp:TemplateField >
                        <ItemTemplate>
                            <asp:CheckBox ID="chk" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="参数编码" SortExpression="参数编码">
                        <ItemTemplate>
                            <asp:TextBox ID="txtCodeM" runat="server" DataValueField="参数编码" DataTextField="参数编码"
                              cssclass = "tbinput1" Enabled = "false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="参数名" SortExpression="参数名">
                        <ItemTemplate>
                            <asp:DropDownList ID="listParaName" runat="server"  
    CssClass= "drpdwnlist" onselectedindexchanged="listParaName_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="标准值" SortExpression="标准值">
                        <ItemTemplate>
                            <asp:TextBox ID="txtValueM" runat="server" DataValueField="标准值" DataTextField="标准值"
                              cssclass = "tbinput"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="上限" SortExpression="上限">
                        <ItemTemplate>
                            <asp:TextBox ID="txtUlimitM" runat="server" DataValueField="上限" DataTextField="上限"
                                cssclass = "tbinput"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="下限" SortExpression="下限">
                        <ItemTemplate>
                            <asp:TextBox ID="txtLlimitM" runat="server" DataValueField="下限" DataTextField="下限"
                                cssclass = "tbinput"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="允差" SortExpression="允差">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDevM" runat="server" DataValueField="允差" DataTextField="允差"
                                cssclass = "tbinput"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单位" SortExpression="单位">
                        <ItemTemplate>
                            <asp:TextBox ID="txtUnitM" runat="server" DataValueField="单位" DataTextField="单位"
                                cssclass = "tbinput"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                 
                     <asp:TemplateField >
                        <ItemTemplate>
                            <asp:Button ID="btnSave" runat="server" Text="保存" cssclass ="btn1" onclick="btnSave_Click" />
                           
                        </ItemTemplate>
                    </asp:TemplateField>    
                     <asp:TemplateField >
                        <ItemTemplate>
                            <asp:Button ID="btnDel" runat="server" Text="删除" cssclass ="btn1" onclick="btnDel_Click" />
                           
                        </ItemTemplate>
                    </asp:TemplateField>                
                </Columns>
                <HeaderStyle CssClass="gridheader" />
                <RowStyle CssClass="gridrow" />
            </asp:GridView> 
            </div> 
            </ContentTemplate>
                  <Triggers>
                  <asp:AsyncPostBackTrigger ControlID = "btnCkAll" />
                  <asp:AsyncPostBackTrigger ControlID = "btnUpdate" />
                  <asp:AsyncPostBackTrigger ControlID = "UpdateGrid" />
                  <asp:AsyncPostBackTrigger ControlID = "btnDelSel" />    
                  <asp:AsyncPostBackTrigger ControlID = "GridView1" />   
                  <asp:AsyncPostBackTrigger ControlID = "hideprc" />
                  <asp:AsyncPostBackTrigger ControlID = "btnCopy" />
                  </Triggers>
            </asp:UpdatePanel>  </td></tr></table>
        </td>
        </tr>
        </table>   
        </div>      
        </div>
        <!--mainright end-->
       
    
    <script type="text/javascript">
        $('.tablelist tbody tr:odd').addClass('odd');
	</script>
    </form>
</body>
</html>
