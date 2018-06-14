<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FRDB.aspx.cs" Inherits="Device_FRDB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>故障库维护</title>
<link href="../css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../js/jquery.js"></script>
<script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
     <script type="text/javascript">
         function treeClick() {           
             $("#tabtop2").click();
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
            <li><a href="#">设备管理</a></li>
            <li><a href="#">故障库维护</a></li>
        </ul>
    </div>
    <div class="formbody">
        <div id="usual1" class="usual">
            <div class="itab">
                <ul>
                    <li><a href="#tab1" class="selected" id = "tabtop1">故障库列表</a></li>
                    <li><a href="#tab2"  id = "tabtop2">故障项详情</a></li>
                </ul>
            </div>
            
        </div>
        <div id="tab1" class="tabson">
                <div class = "framelist">
                    <div class="listtitle">
                        查询条件<span style="position: relative; float: right">
                            <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" 
                            onclick="btnSearch_Click" />     <asp:HiddenField ID="hdcode" runat="server" /> </span>

                    </div>
                    <table class="tablelist">
                        <tbody>
                            <tr>
                                <td width="100">
                                    发生状态
                                </td>
                                <td>
                                    <asp:DropDownList ID="listType1" runat="server" CssClass = "drpdwnlist">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value = "1">渐发性故障</asp:ListItem>
                                    <asp:ListItem Value = "2">突发性故障</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td width="100">
                                    故障性质
                                </td>
                              <td>
                                    <asp:DropDownList ID="listType2" runat="server" CssClass = "drpdwnlist">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value = "1">间断性</asp:ListItem>
                                    <asp:ListItem Value = "2">永久性</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td width="100">
                                    影响程度
                                </td>
                                <td width="100">
                                    <asp:DropDownList ID="listType3" runat="server" CssClass = "drpdwnlist">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value = "1">完全性</asp:ListItem>
                                    <asp:ListItem Value = "2">局部性</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>                         
                             <tr>
                                <td width="100">
                                    危险性
                                </td>
                                <td>
                                    <asp:DropDownList ID="listType4" runat="server" CssClass = "drpdwnlist">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value = "1">危险性</asp:ListItem>
                                    <asp:ListItem Value = "2">安全性</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td width="100px">
                                    发生原因
                                </td>
                              <td>
                                    <asp:DropDownList ID="listType5" runat="server" CssClass = "drpdwnlist">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value = "1">磨损性</asp:ListItem>
                                    <asp:ListItem Value = "2">错用性</asp:ListItem>
                                    <asp:ListItem Value = "3">错用性</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td width="100">
                                    发展规律
                                </td>
                                <td >
                                    <asp:DropDownList ID="listType6" runat="server" CssClass = "drpdwnlist">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value = "1">随机故障</asp:ListItem>
                                    <asp:ListItem Value = "2">周期性故障</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>  
                        </tbody>
                    </table>
               
                <div class="listtitle" style="margin-top: 10px">
                    故障库列表<span style="position: relative; float: right"><asp:Button 
                    ID="btnAdd" runat="server"
                    CssClass="btnadd  auth" Text="新增" onclick="btnAdd_Click"  />                   
                        <asp:Button ID="btnCkAll" runat="server" CssClass="btnset" Text="全选" onclick="btnCkAll_Click" />
                         <asp:Button ID="btnDelSel" runat="server" CssClass="btndel auth" Text="删除" onclick="btnDelSel_Click" />
                    </span></div>
                    <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>                
                 <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="ID" AutoGenerateColumns="False"  >                   
                     <Columns>
                        <asp:TemplateField >
                        <ItemTemplate>
                            <asp:CheckBox ID="chk" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField  ControlStyle-Width = "110px">
                     <ItemTemplate>
                            <asp:Button ID="btnGridView" runat="server" Text="查看详情" cssclass ="btn1" onclick="btnGridView_Click"  Width = "90px"/>
                        </ItemTemplate>
                    </asp:TemplateField>   
                         <asp:BoundField DataField="故障名" HeaderText="故障名" />
                         <asp:BoundField DataField="故障位置" HeaderText="故障位置" />
                         <asp:BoundField DataField="编制人" HeaderText="编制人" />
                                
                     <asp:TemplateField >
                        <ItemTemplate>
                            <asp:Button ID="btnGridDel" runat="server" Text="删除" cssclass ="btn1 auth" onclick="btnGridDel_Click" />
                              </ItemTemplate>
                    </asp:TemplateField>           
                     </Columns>
                   
                     <HeaderStyle CssClass="gridheader" />
                <RowStyle CssClass="gridrow" />
            </asp:GridView>
                  </ContentTemplate>
                <Triggers>
                 <asp:AsyncPostBackTrigger ControlID = "btnSearch" />
                  <asp:AsyncPostBackTrigger ControlID = "btnDelSel" />
                  <asp:AsyncPostBackTrigger ControlID = "btnCkAll" />
                     <asp:AsyncPostBackTrigger ControlID = "GridView1" />
                       <asp:AsyncPostBackTrigger ControlID = "btnSave" />
                </Triggers>
            </asp:UpdatePanel>
            </div>
            </div>
             </div> 
        <div id="tab2" class="tabson">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                 <div class = "framelist">                   
                    <table class="tablelist">
                        <tbody>
                      <tr>
                                <td width="100">
                                    故障名
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" CssClass = "dfinput1"></asp:TextBox>
                                </td>
                                <td width="100">
                                    故障类型
                                </td>
                              <td>
                                  <asp:DropDownList ID="listEqType" runat="server" CssClass = "drpdwnlist">
                                    <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value = '0'>电气故障</asp:ListItem>
                                            <asp:ListItem Value = '1'>机械故障</asp:ListItem>
                                  </asp:DropDownList>
                                </td>
                                <td width="100">
                                    编制人
                                </td>
                                <td width="100">
                                    <asp:TextBox ID="txtEditor" runat="server" CssClass = "dfinput1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="100">
                                    具体位置
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLocation" runat="server" CssClass = "dfinput1"></asp:TextBox>
                                </td>
                                <td width="100">
                                    所属工段
                                </td>
                              <td>
                                  <asp:DropDownList ID="listSection" runat="server" CssClass = "drpdwnlist">
                                  </asp:DropDownList>
                                </td>
                               
                            </tr>
                            <tr>
                                <td width="100">
                                    发生状态
                                </td>
                                <td>
                                    <asp:DropDownList ID="listStyle1" runat="server" CssClass = "drpdwnlist">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value = "1">渐发性故障</asp:ListItem>
                                    <asp:ListItem Value = "2">突发性故障</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td width="100">
                                    故障性质
                                </td>
                              <td>
                                    <asp:DropDownList ID="listStyle2" runat="server" CssClass = "drpdwnlist">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value = "1">间断性</asp:ListItem>
                                    <asp:ListItem Value = "2">永久性</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td width="100">
                                    影响程度
                                </td>
                                <td width="100">
                                    <asp:DropDownList ID="listStyle3" runat="server" CssClass = "drpdwnlist">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value = "1">完全性</asp:ListItem>
                                    <asp:ListItem Value = "2">局部性</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>                         
                             <tr>
                                <td width="100">
                                    危险性
                                </td>
                                <td>
                                    <asp:DropDownList ID="listStyle4" runat="server" CssClass = "drpdwnlist">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value = "1">危险性</asp:ListItem>
                                    <asp:ListItem Value = "2">安全性</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td width="100px">
                                    发生原因
                                </td>
                              <td>
                                    <asp:DropDownList ID="listStyle5" runat="server" CssClass = "drpdwnlist">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value = "1">磨损性</asp:ListItem>
                                    <asp:ListItem Value = "2">错用性</asp:ListItem>
                                    <asp:ListItem Value = "3">固有薄弱性</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td width="100">
                                    发展规律
                                </td>
                                <td >
                                    <asp:DropDownList ID="listStyle6" runat="server" CssClass = "drpdwnlist">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value = "1">随机故障</asp:ListItem>
                                    <asp:ListItem Value = "2">周期性故障</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>  
                            <tr>
                               <td>故障现场</td>
                            <td colspan="5" height="65px">  <asp:TextBox ID="txtScean" runat="server" CssClass = "dfinput1" 
                                    TextMode="MultiLine" Height="60px" Width="500px"></asp:TextBox>
                            </td>
                            </tr>
                            <tr>
                            <td>故障描述</td>
                            <td colspan="5" height="65px">  <asp:TextBox ID="txtDescpt" runat="server" CssClass = "dfinput1"   TextMode="MultiLine" Height="60px" Width="500px"></asp:TextBox>
                            </td>
                            </tr>
                            <tr>
                               <td>故障原因</td>
                            <td colspan="5">  <asp:TextBox ID="txtReason" runat="server" CssClass = "dfinput1" 
                                    TextMode="MultiLine" Height="60px" Width="500px"></asp:TextBox>
                            </td>
                            </tr>
                            <tr>
                               <td>解决方案</td>
                            <td colspan="5" height="65px">  <asp:TextBox ID="txtSolution" runat="server" CssClass = "dfinput1" 
                                    TextMode="MultiLine" Height="60" Width="500px"></asp:TextBox>
                            </td>
                            </tr>
                            <tr>
                            <td colspan="6" align="center">
                                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass = "btnview auth" OnClick = "btnSave_Click" />
                            </td></tr>
                        </tbody>
                    </table>
                    </div>
                </ContentTemplate>
                  <Triggers>                 
                  <asp:AsyncPostBackTrigger ControlID = "btnAdd"/>                 
                  <asp:AsyncPostBackTrigger ControlID = "GridView1"/>
                  </Triggers>
            </asp:UpdatePanel> 
                  
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
