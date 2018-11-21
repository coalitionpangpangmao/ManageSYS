<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuConfig.aspx.cs" Inherits="Authority_GroupConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>菜单项配置</title>
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
    <form id="form1" runat="server">
    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="#">权限管理</a></li>
            <li><a href="#">MAP配置</a></li>
        </ul>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="mainbox">
        <div class="configmainleft">
            <div class="listtitle">
                父级菜单配置
            </div>
            <div  >
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdScrollY" runat="server" />
                        <table>
                            <tr>
                                <td>
                                    <div style="border: thin solid #a7b5bc; height: 180px; overflow: scroll;" valign="top">
                                        <asp:TreeView ID="RightTree" runat="server" Width="150px" OnSelectedNodeChanged="RightTree_SelectedNodeChanged">
                                        </asp:TreeView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table class="tablelist">
                                        <tr>
                                            <td>
                                                菜单名
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMenu" runat="server" class="dfinput1"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                菜单级数
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="listLevel" runat="server" class="drpdwnlist" Height="25px">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Value='1'>一级</asp:ListItem>
                                                    <asp:ListItem Value='2'>二级</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                父菜单
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="listPrt" runat="server" class="drpdwnlist" Height="25px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" height="50px">
                                                <asp:Button ID="btnSave2" runat="server" Text="保存" class="btnview auth" OnClick="btnSave2_Click" />
                                                &nbsp;&nbsp
                                                <asp:Button ID="btnDel" runat="server" Text="删除" class="btndel  auth" OnClick="btnDel_Click"  OnClientClick="javascript:return confirm('确认删除？');"/>
                                                &nbsp;&nbsp
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave2" />
                        <asp:AsyncPostBackTrigger ControlID="btnDel" />
                        <asp:AsyncPostBackTrigger ControlID="RightTree" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="configmainright">
            <div class="listtitle">
                页面URL映射<span style="position: relative; float: right">
                    <asp:Button ID="btnAdds" runat="server" CssClass="btnadd  auth" Text="新增" OnClick="btnAdds_Click" />
                </span>
            </div>
            <div id="gridPanel" onscroll="saveScroll()" style="height: 450px; overflow: scroll">
                <asp:UpdatePanel ID="updtpanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" class="grid" AutoGenerateColumns="False"
                            DataKeyNames="MapID">
                            <Columns>
                                <asp:TemplateField      HeaderText="MapID">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtID" runat="server" DataValueField="MapID" DataTextField="MapID"
                                            Width="50px" CssClass="tbinput" Enabled="False"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField      HeaderText="父菜单">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="listPrt" runat="server" CssClass="drpdwnlist" Width="120px"
                                            DataSource="<%#bindprt() %>" DataTextField="NAME" DataValueField="ID">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField      HeaderText="菜单名称">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtMenu" runat="server" DataValueField="菜单名称" DataTextField="菜单名称"
                                            CssClass="tbinput1"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField      HeaderText="URL">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtURL" runat="server" DataValueField="URL" DataTextField="URL"
                                            Width="200px" CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField      HeaderText="描述">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDscrp" runat="server" DataValueField="描述" DataTextField="描述"
                                            Width="200px" CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField      HeaderText="操作" >
                                    <ItemTemplate>
                                        <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn1  auth" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnDelete" runat="server" Text="删除" CssClass="btn1  auth" OnClick="btnDelete_Click"  OnClientClick="javascript:return confirm('确认删除？');"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                             <AlternatingRowStyle CssClass="gridalterrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAdds" />
                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
