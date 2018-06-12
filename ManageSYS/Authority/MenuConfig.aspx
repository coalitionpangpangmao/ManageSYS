<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuConfig.aspx.cs" Inherits="Authority_GroupConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>菜单项配置</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript">
        function selChange() {
            debugger;
            var str = $("#listType").val()
            if ($("#listType").val() == 0)
                $("#prtMenu").show();
            else
                $("#prtMenu").hide();
            $("#hide").val($("#listType").val());
            str = $("#hide").val();
        }
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
            <li><a href="#">菜单配置</a></li>
        </ul>
    </div>
    <div class="formbody">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="usual1" class="usual">
            <div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdScrollY" runat="server" />
                        <table>
                            <tr>
                                <td  >
                                <div style="border: thin solid #a7b5bc; height: 180px; overflow: scroll;" valign="top">
                                    <asp:TreeView ID="RightTree" runat="server" Width="150px" OnSelectedNodeChanged="RightTree_SelectedNodeChanged">
                                    </asp:TreeView></div>
                                </td>
                                <td>
                                    <ul class="forminfo">
                                        <li>
                                            <label>
                                                菜单名<b>*</b></label><asp:TextBox ID="txtMenu" runat="server" class="dfinput" Style="width: 518px;"></asp:TextBox></li>
                                        <li>
                                            <label>
                                                菜单级数<b>*</b></label><asp:DropDownList ID="listLevel" runat="server" class="drpdwnlist"
                                                    Height="25px" Width="518px">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Value='1'>一级</asp:ListItem>
                                                    <asp:ListItem Value='2'>二级</asp:ListItem>
                                                </asp:DropDownList>
                                        </li>
                                        <li>
                                            <label>
                                                父菜单<b>*</b></label><asp:DropDownList ID="listPrt" runat="server" class="drpdwnlist"
                                                    Height="25px" Width="518px">
                                                </asp:DropDownList>
                                            <li>
                                                <label>
                                                    &nbsp;</label>&nbsp;&nbsp;
                                                <asp:Button ID="btnSave2" runat="server" Text="保存" class="btnview" OnClick="btnSave2_Click" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnDel" runat="server" Text="删除" class="btndel" OnClick="btnDel_Click" />
                                                &nbsp;&nbsp;&nbsp;&nbsp; </li>
                                    </ul>
                                </td>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave2" />
                        <asp:AsyncPostBackTrigger ControlID="btnDel" />
                        <asp:AsyncPostBackTrigger ControlID = "RightTree" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="listtitle">
                页面URL映射<span style="position: relative; float: right">
                    <asp:Button ID="btnAdds" runat="server" CssClass="btnadd" Text="新增" OnClick="btnAdds_Click" />
                </span>
            </div>
            <div id="gridPanel" onscroll="saveScroll()" style="height: 300px; overflow: scroll">
                <asp:UpdatePanel ID="updtpanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" class="grid" AutoGenerateColumns="False"
                            DataKeyNames="MapID">
                            <Columns>
                                <asp:TemplateField HeaderText="MapID">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtID" runat="server" DataValueField="MapID" DataTextField="MapID"
                                            Width="50px" CssClass="tbinput" Enabled="False"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="父菜单">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="listPrt" runat="server" CssClass="drpdwnlist" Width="80px"
                                            DataSource="<%#bindprt() %>" DataTextField="NAME" DataValueField="ID">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="菜单名称">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtMenu" runat="server" DataValueField="菜单名称" DataTextField="菜单名称"
                                            CssClass="tbinput1"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="URL">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtURL" runat="server" DataValueField="URL" DataTextField="URL"
                                            Width="250px" CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="描述">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDscrp" runat="server" DataValueField="描述" DataTextField="描述"
                                            Width="250px" CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="操作" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn1" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnDelete" runat="server" Text="删除" CssClass="btn1" OnClick="btnDelete_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAdds" />
                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
</body>
</html>
