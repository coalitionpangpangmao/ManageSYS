<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QualitySet.aspx.cs" Inherits="Quality_QualitySet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>质量标准</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../js/jquery.js"></script>
    <link rel="stylesheet" href="../js/jquery-treeview/jquery.treeview.css" />
    <link rel="stylesheet" href="../js/jquery-treeview/screen.css" />
    <script type="text/javascript" src="../js/jquery-treeview/jquery.cookie.js"></script>
    <script src="../js/jquery-treeview/jquery.treeview.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#browser").treeview({
                toggle: function () {
                    console.log("%s was toggled.", $(this).find(">span").text());
                }
            });
            $(".folder").click(function () {
                $('.folder').removeClass("selectedbold");
                $('.file').removeClass("selectedbold");
                $(this).addClass("selectedbold");
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
                 <li><a href="#">管理与配置</a></li>
                <li><a href="#">质量考核标准</a></li>
            </ul>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="mainbox">
            <table class="dflist">
                <tr>
                    <td colspan="2">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="listtitle">
                                    标准版本信息<asp:DropDownList ID="listVersion" runat="server" CssClass="drpdwnlist" AutoPostBack="True"
                                        OnSelectedIndexChanged="listVersion_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <span style="position: relative; float: right">审批状态：<asp:DropDownList ID="listAprv" runat="server" CssClass="drpdwnlist" Width="80px" Enabled="False"></asp:DropDownList>
                                        <asp:Button ID="btnSubmit" runat="server" Text="提交审批" CssClass="btn1  auth" Width="80px" OnClick="btnSubmit_Click" />                                        
                                       <asp:Button ID="btnFLow" runat="server" Text="审批进度" CssClass="btn1" Width="80px" OnClick="btnFLow_Click" />
                                        <asp:Button ID="btnNew" runat="server" Text="新增" class="btnadd auth" OnClick="btnNew_Click" />
                                        <asp:Button ID="btnDel" runat="server" Text="删除" class="btndel auth" OnClick="btnDel_Click" OnClientClick="javascript:return confirm('确认删除？');" />
                                        <asp:Button ID="btnModify" runat="server" Text="保存" class="btnmodify auth" OnClick="btnModify_Click" />
                                    </span>
                                </div>
                                <table class="tablelist">
                                    <tbody>
                                        <tr>
                                            <td width="100">标准编码
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCode" runat="server" class="dfinput1" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td width="100">标准名
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtName" runat="server" class="dfinput1"></asp:TextBox>
                                            </td>

                                            <td width="100">标准版本号
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtVersion" runat="server" class="dfinput1"></asp:TextBox>
                                            </td>
                                            <td width="100">编制人
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCreator" runat="server" class="dfinput1" Enabled="False"></asp:TextBox>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td width="100">执行日期
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtExeDate" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                            </td>
                                            <td width="100">结束日期
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEndDate" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                            </td>

                                            <td width="100">编制日期
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCrtDate" runat="server" class="dfinput1" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td width="100">描述
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDscpt" runat="server" class="dfinput1"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <div class="listtitle">
                                    从标准
                        <asp:DropDownList ID="listtech" runat="server" CssClass="drpdwnlist">
                        </asp:DropDownList>
                                    复制为标准
                        <asp:DropDownList ID="listtechC" runat="server" CssClass="drpdwnlist">
                        </asp:DropDownList>
                                    <asp:Button ID="btnCopy" runat="server" CssClass="btnmodify auth" Text="复制" OnClick="btnCopy_Click" />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="listVersion" />
                                <asp:AsyncPostBackTrigger ControlID="btnModify" />
                                <asp:AsyncPostBackTrigger ControlID="btnNew" />
                                <asp:AsyncPostBackTrigger ControlID="btnCopy" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>

                <tr>
                    <td valign="top" width="200">
                        <div style="margin-top: 10px; width: 200px; height: 400px;">
                            <div class="listtitle">
                                工序导航
                            </div>
                            <% = subtvHtml %>
                        </div>
                    </td>
                    <td valign="top" style="position: relative; right: 10px; left: 0px; height: 300px">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div style="position: relative; margin-top: 10px; margin-left: 10px; width: 98%;">
                                    <div class="listtitle">
                                        质量标准参数表<span style="position: relative; float: right"> <asp:Button ID="btnCkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnCkAll_Click" />
                                            <asp:Button ID="btnAdd" runat="server"
                                            CssClass="btnadd  auth" Text="新增" OnClick="btnAdd_Click" />
                                            <asp:Button ID="btnDelSel" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnDelSel_Click" OnClientClick="javascript:return confirm('确认删除？');" />
                                            <asp:HiddenField ID="hideprc" runat="server" Value="" />
                                            <asp:Button ID="UpdateGrid" runat="server" CssClass="btnhide" OnClick="UpdateGrid_Click" />
                                        </span>
                                    </div>
                                    <div style="position: relative; width: 100%; height: 300px; overflow: scroll">
                                        <asp:GridView ID="GridView1" runat="server" class="grid" AllowPaging="True" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="参数名" SortExpression="参数名">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="listParaName" runat="server" CssClass="drpdwnlist" Width="250px">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="考核类型" SortExpression="考核类型">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="listtype" runat="server" DataSource="<%#typebind() %>" DataValueField="ID" DataTextField="NAME" CssClass="drpdwnlist" Width="80px"></asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="下限">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtLower" runat="server" DataValueField="下限" DataTextField="下限"
                                                            CssClass="tbinput"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="上限">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtUpper" runat="server" DataValueField="上限" DataTextField="上限"
                                                            CssClass="tbinput"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              
                                                <asp:TemplateField HeaderText="超限扣分">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtScore" runat="server" DataValueField="超限扣分" DataTextField="超限扣分"
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
                                                        <asp:Button ID="btnGridDel" runat="server" Text="删除" CssClass="btn1 auth" OnClick="btnGridDel_Click" OnClientClick="javascript:return confirm('确认删除？');" />
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
                                <asp:AsyncPostBackTrigger ControlID="UpdateGrid" />
                                <asp:AsyncPostBackTrigger ControlID="GridView1" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <!--mainright end-->
        <div class="aprvinfo" id="flowinfo">
            <div class="tiptop">
                <span>审批流程详情</span><a onclick="$('#flowinfo').fadeOut(100);"></a>
            </div>
            <div class="flowinfo">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView3" runat="server" class="grid">
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                            <AlternatingRowStyle CssClass="gridalterrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnFlow" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <script type="text/javascript">
            $('.tablelist tbody tr:odd').addClass('odd');
        </script>
    </form>
</body>
</html>
