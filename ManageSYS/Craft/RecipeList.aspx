<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecipeList.aspx.cs" Inherits="Craft_RecipeList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>产品管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript">
        function tab2Click(code) {
            window.parent.tab2Click(code);
        }
        function tab3Click(code) {
            window.parent.tab3Click(code);
        }
        function tab4Click(code) {
            window.parent.tab4Click(code);
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
    <asp:HiddenField ID="hdcode" runat="server" />
    <asp:Button ID="btnUpdate" runat="server" Text="Button" CssClass="btnhide" OnClick="btnUpdate_Click" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="mainbox">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="framelist">
                    <tr>
                        <td>
                            <div class="listtitle">
                                配方详情<span style="position: relative; float: right"><span style="position: relative;
                                    float: right">
                                    <asp:Button ID="btnCkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnCkAll_Click" />
                                    <asp:Button ID="btnDel" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnDel_Click"  OnClientClick="javascript:return confirm('确认删除？');"/>
                                </span></span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" class="grid"  DataKeyNames="配方编码" AutoGenerateColumns ="false">
                                <Columns>
                                    <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="选择">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  >
                                        <ItemTemplate>
                                            <asp:Button ID="btnGridDetail" runat="server" Text="配方详情" CssClass="btn1" Width="100px"
                                                OnClick="btnGridDetail_Click" />
                                        </ItemTemplate>
                                        <ItemStyle Width="110px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  >
                                        <ItemTemplate>
                                            <asp:Button ID="btnSubmit" runat="server" Text="提交审批" CssClass="btn1  auth" Width="100px"
                                                OnClick="btnSubmit_Click" />
                                        </ItemTemplate>
                                          <ItemStyle Width="110px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  >
                                        <ItemTemplate>
                                            <asp:Button ID="btnFLow" runat="server" Text="审批进度" CssClass="btn1" Width="100px"
                                                OnClick="btnFLow_Click" />
                                        </ItemTemplate>
                                        <ItemStyle Width="110px" />
                                    </asp:TemplateField>
                                    <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="配方编码" HeaderText="配方编码" />
                                    <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="配方名称" HeaderText="配方名称" />
                                    <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="启用时间" HeaderText="启用时间" />
                                    <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="编辑人员" HeaderText="编辑人员" />
                                    <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="审批状态">
                                    <ItemTemplate>
                                        <asp:Label ID="labAprv" runat="server"  CssClass="labstatu"  Width="60px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridheader" />
                                <RowStyle CssClass="gridrow" />
                                 <AlternatingRowStyle CssClass="gridalterrow" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnCkAll" />
                <asp:AsyncPostBackTrigger ControlID="btnDel" />
                <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div class="aprvinfo" id="flowinfo">
        <div class="tiptop">
            <span>审批流程详情</span><a onclick="Aprvlisthide()"></a></div>
        <div class="flowinfo">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="GridView3" runat="server" class="grid">
                        <HeaderStyle CssClass="gridheader" />
                        <RowStyle CssClass="gridrow" />
                         <AlternatingRowStyle CssClass="gridalterrow" />
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridView1" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
