<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageParts.aspx.cs" Inherits="Device_StorageParts" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>备件管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/select.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $(".click2").click(function () {
                $("#mdftip").fadeIn(200);
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
        function GridClick() {
            $('#tabtop2').click();

        }
        function Aprvlist() {
            $("#flowinfo").fadeIn(200);
        };

        function Aprvlisthide() {
            $("#flowinfo").fadeOut(100);
        };
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
            <li><a href="#">备件管理</a></li>
        </ul>
    </div>
    <div class="formbody">
        <div id="usual1" class="usual">
            <div class="itab">
                <ul>
                    <li><a href="#tab1" class="selected" id="tabtop1">备件领退</a></li>
                    <li><a href="#tab2" id="tabtop2">领退明细</a></li>
                     <li><a href="#tab3" id="tabtop3">备件库存查询</a></li>
                </ul>
            </div>
        </div>
        <div id="tab1" class="tabson">
            <div class="framelist">
                <div class="listtitle">
                    查询条件<span style="position: relative; float: right">
                        <asp:Button ID="btnSearch1" runat="server" Text="查询" CssClass="btnview" OnClick="btnSearch1_Click" />
                    </span>
                </div>
                <table class="tablelist">
                    <tbody>
                        <tr>
                            <td width="100">
                                时间
                            </td>
                            <td>
                                <asp:TextBox ID="txtStart" runat="server" class="dfinput1"></asp:TextBox>至
                                <asp:TextBox ID="txtStop" runat="server" class="dfinput1"></asp:TextBox>
                            </td>
                            <td>
                                领用部门
                            </td>
                            <td>
                                <asp:DropDownList ID="listApts" runat="server" CssClass = "drpdwnlist">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="listtitle" style="margin-top: 10px">
                    备件领退表<span style="position: relative; float: right">
                        <asp:Button ID="btnGridDel" runat="server" Text="删除" class="btndel" OnClick="btnGridDel_Click" />                       <asp:Button ID="btnGridNew" runat="server" Text="新建" class="btnadd" OnClick="btnGridNew_Click" />
                    </span>
                </div>
                <div>                						
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="PZ_CODE"
                             AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="申请部门" HeaderText="申请部门" />
                                <asp:BoundField DataField="申请人" HeaderText="申请人" />
                              
                                <asp:BoundField DataField="领用状态" HeaderText="领用状态" />
                                <asp:BoundField DataField="领用时间" HeaderText="领用时间" />
                                <asp:BoundField DataField="备注" HeaderText="备注" />
                                <asp:BoundField DataField="流程状态" HeaderText="流程状态" />
                                <asp:TemplateField ItemStyle-Width="80">
                                    <ItemTemplate>
                                        <asp:Button ID="btnSubmit" runat="server" Text="提交审批" CssClass="btn1" Width="75"
                                            OnClick="btnSubmit_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="80">
                                    <ItemTemplate>
                                        <asp:Button ID="btnGridIssue" runat="server" Text="审批进度" CssClass="btn1" Width="75"
                                            OnClick="btnGridIssue_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="80">
                                    <ItemTemplate>
                                        <asp:Button ID="btnGridopt" runat="server" Text="领用" CssClass="btn1" Width="75"
                                            OnClick="btnGridopt_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="80">
                                    <ItemTemplate>
                                        <asp:Button ID="btnGridview" runat="server" Text="查看" CssClass="btn1" Width="75"
                                            OnClick="btnGridview_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch1" />
                        <asp:AsyncPostBackTrigger ControlID="btnModify" />
                        <asp:AsyncPostBackTrigger ControlID = "btnGridNew" />
                        <asp:AsyncPostBackTrigger ControlID = "btnGridDel" />
                    </Triggers>
                </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div id="tab2" class="tabson">
            <div class="framelist">
                <div class="listtitle">
                    备件领退管理<span style="position: relative; float: right" class="click2">                       
                           <asp:Button ID="btnReset" runat="server" Text="重置" CssClass= "btnset"
                                            OnClick="btnReset_Click" />
                        <input id="Button2" type="button" value="保存" class="btnmodify" />
                    </span>
                </div>
                <div>
                  <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                <table class="tablelist">
                    <tbody>
                    <tr>
                    <td >单据号
                    </td>
                    <td >
                    <asp:TextBox ID="txtPzcode" runat="server" class="dfinput1"></asp:TextBox>
                    </td>
                      <td width="100">
                                领用时间
                            </td>
                            <td>
                                <asp:TextBox ID="txtTime" runat="server" class="dfinput1"></asp:TextBox>
                            </td>
                    </tr>
                        <tr>
                            <td width="100">
                                申请部门
                            </td>
                            <td>
                                <asp:DropDownList ID="listApt" runat="server" CssClass="drpdwnlist">
                                </asp:DropDownList>
                            </td>
                            <td width="100">
                                申请人
                            </td>
                            <td>
                                <asp:DropDownList ID="listEditor" runat="server" CssClass="drpdwnlist">
                                </asp:DropDownList>
                            </td>
                            </tr>
                            <tr>
                          
                            <td width="100">
                                备注
                            </td>
                            <td colspan="3"  >
                                <asp:TextBox ID="txtRemark" runat="server" class="dfinput1" Width = "450px"></asp:TextBox>
                            </td>
                        </tr>
                    
                    </tbody>
                </table>               
              
                <div class="listtitle" style="margin-top: 10px">
                    领退明细<span style="position: relative; float: right">                   
                    <asp:Button ID="btnAdd" runat="server"  CssClass="btnadd" Text="新增" OnClick="btnAdd_Click" />
                        <asp:Button ID="btnCkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnCkAll_Click" />
                        <asp:Button ID="btnDelSel" runat="server" CssClass="btndel" Text="删除" OnClick="btnDelSel_Click" />
                       
                    </span>
                </div>
                 <asp:GridView ID="GridView2" runat="server" class="grid" AllowPaging="True" AutoGenerateColumns="False"
                            DataKeyNames="ID">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="仓库" SortExpression="仓库">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="listGridstrg" runat="server" CssClass="drpdwnlist" Width = "100px">
                                            <asp:ListItem> </asp:ListItem>
                                            <asp:ListItem Value="1">库存商品库</asp:ListItem>
                                            <asp:ListItem Value="2">烟厂原料库</asp:ListItem>
                                            <asp:ListItem Value="3">鑫源原料库</asp:ListItem>
                                            <asp:ListItem Value="4">烟厂免费原料库</asp:ListItem>
                                            <asp:ListItem Value="5">鑫源免费原料库</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="名称" >
                                    <ItemTemplate>
                                        <asp:DropDownList ID="listGridName" runat="server" CssClass="drpdwnlist" Width="100" DataSource = '<%# gridTypebind() %>'  DataTextField = "material_name" DataValueField = "material_code">
                                            
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="编码" SortExpression="编码">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGridCode" runat="server" DataValueField="编码" DataTextField="编码"
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="规格" SortExpression="规格">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGridSpec" runat="server" DataValueField="规格" DataTextField="规格"
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="型号" SortExpression="型号">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGridModel" runat="server" DataValueField="型号" DataTextField="型号"
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="单位" SortExpression="单位">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGridUnit" runat="server" DataValueField="单位" DataTextField="单位"
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="数量" SortExpression="数量">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGridAmount" runat="server" DataValueField="数量" DataTextField="数量"
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="工艺段" >
                                    <ItemTemplate>
                                        <asp:DropDownList ID="listGridSection" runat="server" CssClass="drpdwnlist" Width="100"  DataSource = '<%#sectionbind()%>'  DataValueField="section_code"  DataTextField ="section_name">
                                            
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="备注" >
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGridDscprt" runat="server" DataValueField="备注" DataTextField="备注"
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="操作" ItemStyle-Width="80">
                                    <ItemTemplate>
                                        <asp:Button ID="btnGrid2Save" runat="server" Text="保存" CssClass="btn1" Width="75"
                                            OnClick="btnGrid2Save_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                        </asp:GridView>                    
                
                  </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnGridNew" />
                        <asp:AsyncPostBackTrigger ControlID="GridView1" />                       
                        <asp:AsyncPostBackTrigger ControlID = "btnReset" />                    
                        <asp:AsyncPostBackTrigger ControlID="btnCkAll" />
                        <asp:AsyncPostBackTrigger ControlID="btnDelSel" />
                        <asp:AsyncPostBackTrigger ControlID="GridView2" />
                        <asp:AsyncPostBackTrigger ControlID="btnModify" />
                        <asp:AsyncPostBackTrigger ControlID = "btnAdd" />
                    </Triggers>
                </asp:UpdatePanel>                 
            </div>            
            </div>
        </div>
        <div id="tab3" class="tabson">
            <div class="framelist">
                <div class="listtitle">
                    查询条件<span style="position: relative; float: right">
                        <asp:Button ID="btnSeach" runat="server" Text="查询" CssClass="btnview" OnClick="btnSearch_Click" />
                    </span>
                </div>
                <table class="tablelist">
                    <tbody>
                        <tr>
                            <td >
                                备件名称
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server" class="dfinput1"></asp:TextBox>
                                
                            </td>
                            <td>
                            备件编码
                            </td>
                            <td>
                               <asp:TextBox ID="txtCode" runat="server" class="dfinput1"></asp:TextBox>
                            </td>
                              <td  width="40px">
                                规格
                            </td>
                            <td>
                                <asp:TextBox ID="txtSpec" runat="server" class="dfinput1"></asp:TextBox>
                                
                            </td>
                            <td width="40px">
                            型号
                            </td>
                            <td>
                               <asp:TextBox ID="txtModel" runat="server" class="dfinput1"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>                       
                </table>
                <div class="listtitle" style="margin-top: 10px">
                    备件库存表
                </div>
                <div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>									
                        <asp:GridView ID="GridView4" runat="server" class="grid" 
                             AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="序号" HeaderText="序号" />
                                <asp:BoundField DataField="物料编码" HeaderText="物料编码" />
                              
                                <asp:BoundField DataField="物料名称" HeaderText="物料名称" />
                                <asp:BoundField DataField="规格" HeaderText="规格" />
                                <asp:BoundField DataField="型号" HeaderText="型号" />
                                <asp:BoundField DataField="货位" HeaderText="货位" />
                                 <asp:BoundField DataField="库存数量" HeaderText="库存数量" />
                                <asp:BoundField DataField="单位" HeaderText="单位" />
                                <asp:BoundField DataField="含税单价" HeaderText="含税单价" />
                                <asp:BoundField DataField="备注" HeaderText="备注" />                              
                            </Columns>
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                    
                    </Triggers>
                </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <div class="tip" id="mdftip">
                <div class="tiptop">
                    <span>提示信息</span><a></a></div>
                <div class="tipinfo">
                    <span>
                        <img src="../images/ticon.png" /></span>
                    <div class="tipright">
                        <p>
                            确认保存此条记录 ？</p>
                        <cite>如果是请点击确定按钮 ，否则请点取消。</cite>
                    </div>
                </div>
                <div class="tipbtn">
                    <asp:Button ID="btnModify" class="sure" runat="server" Text="确定" OnClick="btnModify_Click" />&nbsp;
                    <input name="" type="button" class="cancel" value="取消" />
                </div>
            </div>
        <div class="aprvinfo" id="flowinfo">
            <div class="tiptop">
                <span>审批流程详情</span><a onclick="Aprvlisthide()"></a></div>
            <div class="flowinfo">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView3" runat="server" class="grid">
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
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
    </div>
    </form>
</body>
</html>
