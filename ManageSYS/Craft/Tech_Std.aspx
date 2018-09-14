<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tech_Std.aspx.cs" Inherits="Craft_Tech_Std" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>技术标准管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/select.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <link rel="stylesheet" href="../js/jquery-treeview/jquery.treeview.css" />
    <link rel="stylesheet" href="../js/jquery-treeview/screen.css" />
    <script type="text/javascript" src="../js/jquery-treeview/jquery.cookie.js"></script>
    <script src="../js/jquery-treeview/jquery.treeview.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
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
                <li><a href="#">工艺管理</a></li>
                <li><a href="#">技术标准管理</a></li>
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
                                    工艺标准信息<asp:DropDownList ID="listVersion" runat="server" CssClass="drpdwnlist" AutoPostBack="True" OnSelectedIndexChanged="listVersion_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <span style="position: relative; float: right">审批状态：<asp:DropDownList ID="listAprv" runat="server" CssClass="drpdwnlist" Width="80px" Enabled="False"></asp:DropDownList>
                                        <asp:Button ID="btnSubmit" runat="server" Text="提交审批" CssClass="btn1  auth" Width="80px" OnClick="btnSubmit_Click" />
                                        &nbsp;   
                                       <asp:Button ID="btnFLow" runat="server" Text="审批进度" CssClass="btn1" Width="80px" OnClick="btnFLow_Click" />
                                        &nbsp;&nbsp;           
                                  <asp:Button ID="btnAdd" runat="server" CssClass="btnadd auth" Text="新增" OnClick="btnAdd_Click" />
                                        <asp:Button ID="btnModify" runat="server" CssClass="btnmodify auth" Text="保存" OnClick="btnModify_Click" />
                                        <asp:Button ID="btnDelete" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnDelete_Click"  OnClientClick="javascript:return confirm('确认删除？');"/>
                                        <asp:Button ID="btnUpdate" runat="server" CssClass="btnhide" OnClick="btnUpdate_Click" />
                                    </span>
                                </div>
                                <table class="tablelist">
                                    <tbody>
                                        <tr>
                                            <td width="100">工艺标准名
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtName" runat="server" class="dfinput1"></asp:TextBox>
                                            </td>
                                            <td width="100">标准编码
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCode" runat="server" class="dfinput1" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td width="100">标准版本号
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtVersion" runat="server" class="dfinput1"></asp:TextBox>
                                            </td>
                                            <td width="100">受控状态
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="listStatus" runat="server" CssClass="drpdwnlist">
                                                </asp:DropDownList>
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


                                            <td width="100">编制人
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="listCreator" runat="server" CssClass="drpdwnlist"
                                                    Enabled="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="100">编制日期
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCrtDate" runat="server" class="dfinput1" Enabled="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="100">编制部门
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="listCrtApt" runat="server" CssClass="drpdwnlist"
                                                    Enabled="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="100">是否有效
                                            </td>
                                            <td width="100">
                                                <asp:CheckBox ID="ckValid" runat="server" Text=" " />
                                            </td>
                                            <td width="100">描述
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDscpt" runat="server" class="dfinput1"></asp:TextBox>
                                            </td>
                                            <td></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                                <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                                <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
                                <asp:AsyncPostBackTrigger ControlID="btnDelete" />
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
                                工序导航
                            </div>
                            <% = subtvHtml %>
                        </div>
                    </td>
                    <td valign="top">
                        <div style="margin-top: 10px; margin-left: 10px; min-width: 900px;">
                            <div id="usual1" class="usual">
                                <div class="itab">
                                    <ul>
                                        <li><a href="#tab1" id="tabtop1">设定参数</a></li>
                                        <li><a href="#tab2" id="tabtop2">工艺标准</a></li>
                                    </ul>
                                </div>
                            </div>
                            <div id="tab1" class="tabson">
                                <div class="listtitle">
                                    <span style="position: relative; float: right">
                                        <asp:Button ID="btnCkAll2" runat="server" CssClass="btnset" Text="全选" OnClick="btnCkAll2_Click" />
                                        <asp:Button ID="btnSave2" runat="server" Text="保存" CssClass="btnview auth" OnClick="btnSave2_Click" />
                                        <asp:Button ID="btnDelSel2" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnDelSel2_Click"  OnClientClick="javascript:return confirm('确认删除？');"/>
                                    </span>
                                </div>
                                <div style="overflow: scroll; height: 300px">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:GridView ID="GridView2" runat="server" class="grid" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="参数编码" SortExpression="参数编码">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCodeM" runat="server" DataValueField="参数编码" DataTextField="参数编码"
                                                                CssClass="tbinput1" Enabled="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="参数名" SortExpression="参数名">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="listParaName" runat="server" CssClass="drpdwnlist" Width="200px">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="标准值" SortExpression="标准值">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtValueM" runat="server" DataValueField="标准值" DataTextField="标准值"
                                                                CssClass="tbinput"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="上限" SortExpression="上限">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtUlimitM" runat="server" DataValueField="上限" DataTextField="上限"
                                                                CssClass="tbinput"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="下限" SortExpression="下限">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtLlimitM" runat="server" DataValueField="下限" DataTextField="下限"
                                                                CssClass="tbinput"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="允差" SortExpression="允差">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDevM" runat="server" DataValueField="允差" DataTextField="允差" CssClass="tbinput"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="单位" SortExpression="单位">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtUnitM" runat="server" DataValueField="单位" DataTextField="单位"
                                                                CssClass="tbinput"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <HeaderStyle CssClass="gridheader" />
                                                <RowStyle CssClass="gridrow" />
                                                <AlternatingRowStyle CssClass="gridalterrow" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnCkAll2" />
                                            <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                                            <asp:AsyncPostBackTrigger ControlID="UpdateGrid" />
                                            <asp:AsyncPostBackTrigger ControlID="btnDelSel2" />
                                            <asp:AsyncPostBackTrigger ControlID="hideprc" />
                                            <asp:AsyncPostBackTrigger ControlID="btnCopy" />
                                            <asp:AsyncPostBackTrigger ControlID="btnSave2" />
                                             <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                            <asp:AsyncPostBackTrigger ControlID ="listVersion" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div id="tab2" class="tabson">
                                <div class="listtitle">
                                    <span style="position: relative; float: right">
                                        <asp:Button ID="btnCkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnCkAll_Click" />
                                        <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btnview auth" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnDelSel" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnDelSel_Click"  OnClientClick="javascript:return confirm('确认删除？');"/>
                                        <asp:HiddenField ID="hideprc" runat="server" Value="70301" />
                                        <asp:Button ID="UpdateGrid" runat="server" CssClass="btnhide" OnClick="UpdateGrid_Click" />
                                    </span>
                                </div>
                                <div style="overflow: scroll; height: 300px">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:GridView ID="GridView1" runat="server" class="grid" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="参数编码" SortExpression="参数编码">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCodeM" runat="server" DataValueField="参数编码" DataTextField="参数编码"
                                                                CssClass="tbinput1" Enabled="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="参数名" SortExpression="参数名">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="listParaName" runat="server" CssClass="drpdwnlist" Width="200px">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="标准值" SortExpression="标准值">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtValueM" runat="server" DataValueField="标准值" DataTextField="标准值"
                                                                CssClass="tbinput"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="上限" SortExpression="上限">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtUlimitM" runat="server" DataValueField="上限" DataTextField="上限"
                                                                CssClass="tbinput"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="下限" SortExpression="下限">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtLlimitM" runat="server" DataValueField="下限" DataTextField="下限"
                                                                CssClass="tbinput"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="允差" SortExpression="允差">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDevM" runat="server" DataValueField="允差" DataTextField="允差" CssClass="tbinput"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="单位" SortExpression="单位">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtUnitM" runat="server" DataValueField="单位" DataTextField="单位"
                                                                CssClass="tbinput"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <HeaderStyle CssClass="gridheader" />
                                                <RowStyle CssClass="gridrow" />
                                                <AlternatingRowStyle CssClass="gridalterrow" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnCkAll" />
                                            <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                                            <asp:AsyncPostBackTrigger ControlID="UpdateGrid" />
                                            <asp:AsyncPostBackTrigger ControlID="btnDelSel" />
                                            <asp:AsyncPostBackTrigger ControlID="hideprc" />
                                            <asp:AsyncPostBackTrigger ControlID="btnCopy" />
                                               <asp:AsyncPostBackTrigger ControlID="btnSave2" />
                                             <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                             <asp:AsyncPostBackTrigger ControlID ="listVersion" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>

                            </div>
                        </div>
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
            $("#usual1 ul").idTabs();
        </script>
    </form>
</body>
</html>
