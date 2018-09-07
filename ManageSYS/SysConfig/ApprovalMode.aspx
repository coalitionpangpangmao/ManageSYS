<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApprovalMode.aspx.cs" Inherits="SysConfig_ApprovalMode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>审批模版管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/select.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <link rel="stylesheet" href="../js/jquery-treeview/jquery.treeview.css" />
    <link rel="stylesheet" href="../js/jquery-treeview/screen.css" />
    <script type="text/javascript" src="../js/jquery-treeview/jquery.cookie.js"></script>
    <script src="../js/jquery-treeview/jquery.treeview.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#browser").treeview({
                toggle: function () {
                    console.log("%s was toggled.", $(this).find(">span").text());
                }
            });
        });
        function treeClick(code) {
            $("#txtCode").val(code);
            debugger;
            $("#btnUpdate").click();
        }
    </script>
</head>
<body>
    <form id="Form1" runat="server">
    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="#">系统配置</a></li>
            <li><a href="#">审批模版配置</a></li>
        </ul>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="mainbox">
        <div class="mainleft">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="leftinfo">
                        <div class="listtitle">
                            审批模版</div>
                        <% = tvHtml %>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnModify" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <!--mainleft end-->
        <div class="mainright">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="listtitle">
                        审批模版配置<span style="position: relative; float: right">
                          
                            <asp:Button ID="btnModify" runat="server" CssClass="btnhide" Text="保存" OnClick="btnModify_Click" />
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btnhide" OnClick="btnUpdate_Click" />
                        </span>
                    </div>
                    <table class="tablelist">
                        <tbody>
                            <tr>
                                <td width="100">
                                    审批模版名
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" class="dfinput1" Enabled="False"></asp:TextBox>
                                </td>
                                <td width="100">
                                    模版编号
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCode" runat="server" class="dfinput1" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div class="listtitle">
                        模版流程表<span style="position: relative; float: right">
                               <asp:Button ID="btnAdd" runat="server" CssClass="btnadd" Text="新建" OnClick="btnAdd_Click" />
                            <asp:Button ID="btnCkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnCkAll_Click" />
                            <asp:Button ID="btnDelSel" runat="server" CssClass="btndel  auth" Text="删除" OnClick="btnDelSel_Click" />
                        </span>
                    </div>
                    <div>
                        <asp:GridView ID="GridView1" runat="server" class="grid" AllowPaging="True" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="审批类型">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="listType" runat="server" CssClass="drpdwnlist">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="顺序号">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOrder" runat="server" DataValueField="顺序号" DataTextField="顺序号"
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="审批角色">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="listRole" runat="server" CssClass="drpdwnlist">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="发送环节名">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFlowname" runat="server" DataValueField="发送环节名" DataTextField="发送环节名"
                                            Width="150px" CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn1  auth" OnClick="btnSave_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="btn1  auth" OnClick="btnDel_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                            <AlternatingRowStyle CssClass="gridalterrow" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnCkAll" />
                    <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                    <asp:AsyncPostBackTrigger ControlID="btnDelSel" />
                    <asp:AsyncPostBackTrigger ControlID="GridView1" />
                    <asp:AsyncPostBackTrigger ControlID ="btnAdd" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <!--mainright end-->
    <script type="text/javascript">
        $('.tablelist tbody tr:odd').addClass('odd');
    </script>
    </form>
</body>
</html>
