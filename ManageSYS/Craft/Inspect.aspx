<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Inspect.aspx.cs" Inherits="Craft_Inspect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>工艺检查项目</title>
<link href="../css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../js/jquery.js"></script>
<script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
   <script type="text/javascript">   
       function GridClick() {
           $('#tabtop2').click();             
       }
    </script>
</head>
<body>
    <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="#">工艺管理</a></li>
            <li><a href="#">工艺检查项目</a></li>
        </ul>
    </div>
    <div class="formbody">
        <div id="usual1" class="usual">
            <div class="itab">
                <ul>
                    <li><a href="#tab1" class="selected" id = "tabtop1">检查项目列表</a></li>
                    <li><a href="#tab2"  id = "tabtop2">信息编辑</a></li>
                </ul>
            </div>
            
        </div>
        <div id="tab1" class="tabson">
                <div class = "framelist">
                    <div class="listtitle">
                        查询条件<span style="position: relative; float: right">
                            <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" 
                            onclick="btnSearch_Click" />  </span>

                    </div>
                    <table class="tablelist">
                        <tbody>
                            <tr>
                                <td width="100">
                                    检查项目
                                </td>
                                <td>
                                    <asp:TextBox ID="txtProj" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                                <td width="100">
                                    工艺段
                                </td>
                                <td>
                                    <asp:DropDownList ID="listSection" runat="server" CssClass = "drpdwnlist">
                                    </asp:DropDownList>                                  
                                </td>
                                <td width="100">
                                    检查类型
                                </td>
                                <td width="100">
                                    <asp:DropDownList ID="listtype" runat="server" CssClass = "drpdwnlist">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="70301">工艺检查</asp:ListItem>
                                        <asp:ListItem Value="70302">保养检查</asp:ListItem>
                                        <asp:ListItem Value="70303">日常巡检</asp:ListItem>
                                    </asp:DropDownList>     
                                </td>
                            </tr>                         
                               
                        </tbody>
                    </table>
               
                <div class="listtitle" style="margin-top: 10px">
                    检查项目列表  <span style="position: relative; float: right" >                        
                        <asp:Button ID="btnGrid1CkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnGrid1CkAll_Click" />
                        <asp:Button ID="btnGrid1DelSel" runat="server" CssClass="btndel" Text="删除" OnClick="btnGrid1DelSel_Click" /></span></div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>                
                 <asp:GridView ID="GridView1" runat="server" class="grid" 
                        AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="检查项目编码"  >
                     <Columns>
                     <asp:TemplateField >
                        <ItemTemplate>                                                  
                            <asp:CheckBox ID="ck" runat="server" />            
                        </ItemTemplate>
                            
                        </asp:TemplateField>  
                         <asp:BoundField DataField="检查项目编码" HeaderText="检查项目编码" />
                          <asp:BoundField DataField="检查项目" HeaderText="检查项目" />
                           <asp:BoundField DataField="所属工段" HeaderText="所属工段" />
                            <asp:BoundField DataField="所属区域" HeaderText="所属区域" />
                             <asp:BoundField DataField="检查类型" HeaderText="检查类型" />
                              <asp:BoundField DataField="编制人" HeaderText="编制人" />
                               <asp:BoundField DataField="备注" HeaderText="备注" />
                         <asp:TemplateField  HeaderText = "操作">
                        <ItemTemplate>                                                  
                                
                            <asp:Button ID="btnEdit" runat="server" Text="编辑" CssClass = "btn1" OnClick = "btnEdit_Click"/>               
                        </ItemTemplate>
                            
                        </asp:TemplateField >
                  </Columns>
                     <HeaderStyle CssClass="gridheader" />
                <RowStyle CssClass="gridrow" />
            </asp:GridView>
                  </ContentTemplate>
                <Triggers>
                 <asp:AsyncPostBackTrigger ControlID = "btnSave" />
                  <asp:AsyncPostBackTrigger ControlID = "btnSearch" />
                  <asp:AsyncPostBackTrigger ControlID = "btnGrid1CkAll" />
                    <asp:AsyncPostBackTrigger ControlID = "btnGrid1DelSel" />
                </Triggers>
            </asp:UpdatePanel>
            </div>
             </div> 
        <div id="tab2" class="tabson">
            <div class = "framelist">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>       
                <div class="listtitle">
                        编辑<span style="position: relative; float: right">
                            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btnview" 
                            onclick="btnSave_Click" />   </span>
                    </div>                    
                    <table class="tablelist">
                        <tbody>
                            <tr>
                                <td width="100">
                                    检验项目名称:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                                <td width="100">
                                    检验项目编码:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCode" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="100">
                                    工艺段:
                                </td>
                                <td>
                                    <asp:DropDownList ID="listSection2" runat="server" CssClass = "drpdwnlist">
                                    </asp:DropDownList>
                                </td>                                
                                <td width="100">
                                  所属区域:
                                </td>
                                <td>
                                    <asp:DropDownList ID="listArea" runat="server" CssClass = "drpdwnlist">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>再造梗丝车间</asp:ListItem>
                                    </asp:DropDownList>
                                </td>                               
                            </tr>
                            <tr>
                                <td width="100">
                                    检查类型:
                                </td>
                                <td>
                                    <asp:DropDownList ID="listType2" runat="server"  CssClass = "drpdwnlist">  <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="70301">工艺检查</asp:ListItem>
                                        <asp:ListItem Value="70302">保养检查</asp:ListItem>
                                        <asp:ListItem Value="70303">日常巡检</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td width="100">
                                    编制人:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEditor" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                            </tr>
                          <tr>
                          <td>备注</td>
                         <td colspan="3" height="90px">
                             <asp:TextBox ID="txtRemark" runat="server" CssClass = "dfinput1" Height="80px" 
                                 TextMode="MultiLine" Width="500px"></asp:TextBox>
                         </td>
                          </tr>
                        </tbody>
                    </table>
                  
                </ContentTemplate>
                  <Triggers>                 
                  <asp:AsyncPostBackTrigger ControlID = "GridView1"/>
                  </Triggers>
            </asp:UpdatePanel>       
            </div>            
            </div>   
    </div>
    <script type="text/javascript">
        $("#usual1 ul").idTabs(); 
    </script>
    
    <script type="text/javascript">
        $('.tablelist tbody tr:odd').addClass('odd');
	</script>
    </form>
</body>
</html>
