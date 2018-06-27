<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QualitySet.aspx.cs" Inherits="Quality_QualitySet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>质量标准</title>
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
            <li><a href="#">质量评估与分析</a></li>
            <li><a href="#">质量标准维护</a></li>
        </ul>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="rightinfo">
        <table class="dflist">
            <tr>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="listtitle">
                                标准版本信息<asp:DropDownList ID="listVersion" runat="server" CssClass="drpdwnlist" AutoPostBack="True"
                                    OnSelectedIndexChanged="listVersion_SelectedIndexChanged">
                                </asp:DropDownList>
                                <span style="position: relative; float: right">
                                    <asp:Button ID="btnDel" runat="server" Text="删除" class="btndel auth" OnClick="btnDel_Click" />
                                    <asp:Button ID="btnModify" runat="server" Text="保存" class="btnmodify auth" OnClick="btnModify_Click" />
                                </span>
                            </div>
                            <table class="tablelist">
                                <tbody>
                                    <tr>
                                        <td width="100">
                                            标准名
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
                                            标准版本号
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtVersion" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                        <td width="100">
                                            是否有效
                                        </td>
                                        <td width="100">
                                            <asp:CheckBox ID="ckValid" runat="server" Text=" " Enabled="False" />
                                        </td>
                                    </tr>
                                    <tr>
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
                                            <asp:DropDownList ID="listCrtApt" runat="server" Width="150" Height="25">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="100">
                                            描述
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtDscpt" runat="server" class="dfinput1" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="listVersion" />
                            <asp:AsyncPostBackTrigger ControlID="btnModify" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="listtitle">
                        从标准
                        <asp:DropDownList ID="listtech" runat="server" CssClass="drpdwnlist">
                        </asp:DropDownList>
                        复制为标准
                        <asp:DropDownList ID="listtechC" runat="server" CssClass="drpdwnlist">
                        </asp:DropDownList>
                        <asp:Button ID="btnCopy" runat="server" CssClass="btnmodify auth" Text="复制" OnClick="btnCopy_Click" />
                    </div>
                </td>
            </tr>
            <tr>
                <td valign="top" width="200">
                    <div style="margin-top: 10px; width: 200px; height: 400px;">
                        <div class="listtitle">
                            工序导航</div>
                        <% = subtvHtml %></div>
                </td>
                <td valign="top">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div style="margin-top: 10px; margin-left: 10px; width: 900px;">
                                <div class="listtitle">
                                    质量标准参数表<span style="position: relative; float: right"><asp:Button ID="btnAdd" runat="server"
                                        CssClass="btnadd  auth" Text="新增" OnClick="btnAdd_Click" />
                                        <asp:Button ID="btnCkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnCkAll_Click" />
                                        <asp:Button ID="btnDelSel" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnDelSel_Click" />
                                        <asp:HiddenField ID="hideprc" runat="server" Value="70202" />
                                        <asp:Button ID="UpdateGrid" runat="server" CssClass="btnhide" OnClick="UpdateGrid_Click" />
                                    </span>
                                </div>
                                <div style="width: 900px; overflow: scroll">
                                    <asp:GridView ID="GridView1" runat="server" class="grid" AllowPaging="True" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="参数编码" SortExpression="参数编码">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCodeM" runat="server" DataValueField="参数编码" DataTextField="参数编码"
                                                        CssClass="tbinput1"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="参数名" SortExpression="参数名">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="listParaName" runat="server" CssClass="drpdwnlist" OnSelectedIndexChanged="listParaName_SelectedIndexChanged"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="标准值" SortExpression="标准值">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtValueM" runat="server" DataValueField="标准值" DataTextField="标准值"
                                                        CssClass="tbinput"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="判断条件" SortExpression="判断条件">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDevM" runat="server" DataValueField="判断条件" DataTextField="判断条件"
                                                        CssClass="tbinput"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="考核类型" SortExpression="考核类型">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtType" runat="server" DataValueField="考核类型" DataTextField="考核类型"
                                                        CssClass="tbinput"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="工艺段" SortExpression="工序段">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPrcM" runat="server" DataValueField="工序段" DataTextField="工序段"
                                                        CssClass="tbinput"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="备注" SortExpression="备注">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDscrptM" runat="server" DataValueField="备注" DataTextField="备注"
                                                        CssClass="tbinput1"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn1 auth" OnClick="btnSave_Click" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnGridDel" runat="server" Text="删除" CssClass="btn1 auth" OnClick="btnGridDel_Click" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridheader" />
                                        <RowStyle CssClass="gridrow" />
                                        <AlternatingRowStyle CssClass="gridalterrow" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                            <asp:AsyncPostBackTrigger ControlID="btnCkAll" />
                            <asp:AsyncPostBackTrigger ControlID="btnModify" />
                            <asp:AsyncPostBackTrigger ControlID="btnDelSel" />
                            <asp:AsyncPostBackTrigger ControlID="listVersion" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <!--mainright end-->
    <script type="text/javascript">
        $('.tablelist tbody tr:odd').addClass('odd');
    </script>
    </form>
</body>
</html>
