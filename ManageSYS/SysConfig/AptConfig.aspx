<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AptConfig.aspx.cs" EnableEventValidation="false"
    Inherits="SysConfig_AptConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>组织机构管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript">
        $().ready(function () { $("#gridPanel").scrollTop = $("#hdScrollY").val(); });
        function saveScroll() {
            var y = $("#gridPanel").scrollTop();
            $("#hdScrollY").val(y);

        }
    </script>
</head>
<body>
    <form runat="server">
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">系统管理</a></li>
                <li><a href="#">组织机构配置</a></li>
            </ul>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="rightinfo">
            <div class="gridtools  auth">
                <ul class="toolbar">
                    <asp:Button ID="btnAdd" CssClass="btnadd  auth" runat="server" OnClick="btnAdd_Click"
                        Text="添加" />
                    &nbsp; &nbsp;
                <asp:Button ID="btnView" CssClass="btnmodify  auth" runat="server" OnClick="btnView_Click"
                    Text="查看" />
                    &nbsp; &nbsp;
                <asp:Button ID="btnDel" CssClass="btndel  auth" runat="server" Text="删除" OnClick="btnDel_Click"  OnClientClick="javascript:return confirm('确认删除？');"/>
                             &nbsp; &nbsp;
    <asp:Button ID ="btnUpdate" CssClass ="btnpatch auth" runat ="server" Text ="同步数据" OnClick  ="btnUpdate_Click"  Width ="100px"/>
                </ul>
            </div>
            <div id="gridPanel" onscroll="saveScroll()" style="height: 350px; overflow: scroll">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" CssClass="grid" DataKeyNames="组织机构代码">
                            <Columns>
                                <asp:TemplateField     >
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="headck" runat="server" OnCheckedChanged="ck_CheckedChanged" AutoPostBack="True" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ck" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                            <AlternatingRowStyle CssClass="gridalterrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                        <asp:AsyncPostBackTrigger ControlID="btnModify" />
                        <asp:AsyncPostBackTrigger ControlID="btnDel" />
                        <asp:AsyncPostBackTrigger ControlID="btnView" />
                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
                          <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="shade">
            <div class="info">
                <div class="tiphead">
                    <span>组织机构信息</span><a onclick="$('.shade').fadeOut(100);"></a>
                </div>
                <div class="gridinfo">
                    <asp:UpdatePanel ID="updtpanel1" runat="server">
                        <ContentTemplate>
                            <table class="tablelist">
                                <tbody>
                                    <tr>
                                        <td width="100">组织机构代码
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCode" runat="server" class="dfinput1" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td width="100">组织机构名称
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtName" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                        <td width="100">父级标识
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listParent" runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100">类型
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtType" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                        <td width="100">权重
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWeight" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                        <td width="100">主数据标识
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSapID" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="120">角色
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listRole" runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="shadebtn" align="center">
                                <asp:HiddenField ID="hdScrollY" runat="server" />
                                <asp:Button ID="btnModify" class="sure" runat="server" Text="保存" OnClick="btnModify_Click" />
                                <input name="" type="button" class="cancel" value="关闭" onclick="$('.shade').fadeOut(100);" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                            <asp:AsyncPostBackTrigger ControlID="btnView" />
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
