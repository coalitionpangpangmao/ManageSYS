<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Inspect.aspx.cs" Inherits="Craft_Inspect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>工艺检查项目</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script> 
  
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
            <div class="framelist">
                <div class="listtitle">
                    查询条件<span style="position: relative; float: right">
                        <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnAdd" runat="server" CssClass="btnadd auth" Text="新增" OnClick="btnAdd_Click" />
                    </span>
                </div>
                <table class="tablelist">
                    <tbody>
                        <tr>
                            <td width="100">检查类型
                            </td>
                            <td width="100">
                                <asp:DropDownList ID="listtype" runat="server" CssClass="drpdwnlist" AutoPostBack="True"
                                    OnSelectedIndexChanged="listtype_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td width="100">分组
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="listSection" runat="server" CssClass="drpdwnlist">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="listtype" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="listtitle" style="margin-top: 10px">
                    检查项目列表 <span style="position: relative; float: right">
                        
                        <asp:Button ID="btnGrid1CkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnGrid1CkAll_Click" />
                        <asp:Button ID="btnGrid1DelSel" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnGrid1DelSel_Click" /></span>
                </div>
                <div style="height: 275px; overflow: auto">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" class="grid" AutoGenerateColumns="False"
                                DataKeyNames="检查项目编码" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="8">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ck" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="检查类型" HeaderText="检查类型" />
                                    <asp:BoundField DataField="分组" HeaderText="分组" />
                                    <asp:BoundField DataField="检查项目编码" HeaderText="检查项目编码" />
                                    <asp:BoundField DataField="检查项目" HeaderText="检查项目" />
                                    <asp:BoundField DataField="备注" HeaderText="备注" />
                                    <asp:TemplateField HeaderText="操作">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEdit" runat="server" Text="编辑" CssClass="btn1" OnClick="btnEdit_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridheader" />
                                <RowStyle CssClass="gridrow" />
                                <AlternatingRowStyle CssClass="gridalterrow" />
                                <PagerStyle CssClass="gridpager" />
                                <PagerTemplate>
                                    <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> ' Width="100px"></asp:Label>
                                    <asp:LinkButton ID="lbnFirst" runat="Server" Text="首页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="First"></asp:LinkButton>
                                    <asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="Prev"></asp:LinkButton>
                                    <asp:LinkButton ID="lbnNext" runat="Server" Text="下一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Next"></asp:LinkButton>
                                    <asp:LinkButton ID="lbnLast" runat="Server" Text="尾页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Last"></asp:LinkButton>
                                    到第
                                <asp:TextBox ID="txtNewPageIndex" runat="server" Width="20px" Text='<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>' />
                                    页  
             <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-2"
                 CommandName="Page" Text="跳转" />
                                </PagerTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                            <asp:AsyncPostBackTrigger ControlID="btnGrid1CkAll" />
                            <asp:AsyncPostBackTrigger ControlID="btnGrid1DelSel" />

                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="shade">
            <div class="info">
                <div class="tiphead">
                    <span>产品信息</span><a onclick="$('.shade').fadeOut(100);"></a>
                </div>
                <div class="gridinfo">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table class="tablelist">
                                <tbody>
                                    <tr>
                                        <td width="100">检查类型:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listType2" runat="server" CssClass="drpdwnlist"
                                                AutoPostBack="True" OnSelectedIndexChanged="listType2_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="100">检查分组:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listSection2" runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td width="100">检验项目编码:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCode" runat="server" class="dfinput1" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td width="100">检验项目名称:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtName" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td width="100">单位:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUnit" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                        <td width="100">编制人:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listCreator" runat="server"
                                                CssClass="drpdwnlist" Enabled="False">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>备注
                                        </td>
                                        <td colspan="3" height="90px">
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="dfinput1" Height="80px" TextMode="MultiLine"
                                                Width="500px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="shadebtn" align="center">
                                <asp:HiddenField ID="hdScrollY" runat="server" />
                                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="sure auth" OnClick="btnSave_Click" />
                                <input name="" type="button" class="cancel" value="关闭" onclick="$('.shade').fadeOut(100);" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GridView1" />
                            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>

        <script type="text/javascript">
            $('.tablelist tbody tr:odd').addClass('odd');
        </script>
    </form>
</body>
</html>
