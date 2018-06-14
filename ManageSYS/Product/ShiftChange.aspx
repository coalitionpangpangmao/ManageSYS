<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShiftChange.aspx.cs" Inherits="Product_ShiftChange" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>班组交接班记录</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/select.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
    <script type="text/javascript">

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
            <li><a href="#">生产管理</a></li>
            <li><a href="#">班组交接班记录</a></li>
        </ul>
    </div>
    <div class="formbody">
        <div id="usual1" class="usual">
            <div class="itab">
                <ul>
                    <li><a href="#tab1" class="selected" id="tabtop1">班组交接班列表</a></li>
                    <li><a href="#tab2" id="tabtop2">交接班详情</a></li>
                </ul>
            </div>
        </div>
        <div id="tab1" class="tabson">
            <div class="framelist">
                <div class="listtitle">
                    查询条件<span style="position: relative; float: right">
                        <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" OnClick="btnSearch_Click" />
                    </span>
                </div>
                <table class="tablelist">
                    <tbody>
                        <tr>
                            <td width="100">
                                时间
                            </td>
                            <td>
                                <asp:TextBox ID="txtStartDate" runat="server" class="dfinput1"></asp:TextBox>至
                                <asp:TextBox ID="txtStopDate" runat="server" class="dfinput1"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="listtitle" style="margin-top: 10px">
                    交接班列表
                </div>
                <div style="overflow: scroll; width: 100%; height: 300px;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdID" runat="server" />
                            <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="ID" AllowPaging="False"
                                AutoGenerateColumns="False" PageSize="12">
                                <Columns>
                                    <asp:BoundField DataField="日期" HeaderText="日期" />
                                    <asp:BoundField DataField="班组" HeaderText="班组" />
                                    <asp:BoundField DataField="班时" HeaderText="班时" />
                                    <asp:BoundField DataField="开始时间" HeaderText="开始时间" />
                                    <asp:BoundField DataField="结束时间" HeaderText="结束时间" />
                                    <asp:TemplateField HeaderText="操作">
                                        <ItemTemplate>
                                            <asp:Button ID="btnGrid1Edit" runat="server" Text="填写" CssClass="btn1 auth" OnClick="btnGrid1Edit_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridheader" />
                                <RowStyle CssClass="gridrow" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div id="tab2" class="tabson">
            <div class="framelist">
                <div class="listtitle">
                    交接班详情<span style="position: relative; float: right">
                        <asp:Button ID="btnSave" runat="server" Text="保存" class="btnview auth" OnClick="btnSave_Click" />
                    </span>
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table class="tablelist">
                                <tbody>
                                    <tr>
                                        <td width="100">
                                            统计日期
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDate" runat="server" class="dfinput1" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td width="100">
                                            班时
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listShift" runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="100">
                                            班组
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listTeam" runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100">
                                            牌号
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listProd" runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="100">
                                            计划号
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPlanNo" runat="server" class="dfinput1" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            填写人
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEditor" runat="server" class="dfinput1" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100">
                                            当班产量(箱)
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOutput" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                        <td width="100">
                                            交班人
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOlder" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                        <td width="100">
                                            接班人
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNewer" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100" height="55">
                                            设备运行情况
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtDevice" runat="server" class="dfinput1" Height="50px" TextMode="MultiLine"
                                                Width="500px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100" height="55">
                                            工艺质量情况
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtQlt" runat="server" class="dfinput1" Height="50px" TextMode="MultiLine"
                                                Width="500px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100" height="55">
                                            现场情况
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtScean" runat="server" class="dfinput1" Height="50px" TextMode="MultiLine"
                                                Width="500px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100" height="55px">
                                            备注
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtRemark" runat="server" class="dfinput1" Height="50px" TextMode="MultiLine"
                                                Width="500px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th colspan="6" height="40px">
                                            班组交接明细列表<span style="position: relative; float: right">
                                                <asp:Button ID="btnAdd" runat="server" Text="增加" class="btn1 auth" OnClick="btnAdd_Click" />
                                                <asp:Button ID="btnckAll" runat="server" Text="全选" class="btn1 auth" OnClick="btnCkAll_Click" />
                                                <asp:Button ID="btnDelSel" runat="server" Text="删除" class="btn1 auth" OnClick="btnDelSel_Click" />
                                            </span>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:GridView ID="GridView2" runat="server" class="grid" AllowPaging="False" AutoGenerateColumns="False"
                                                PageSize="12">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="物料名称">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="listMater" runat="server" CssClass="drpdwnlist" Width="120px"
                                                                DataSource="<%#gridTypebind()%>" DataTextField="material_name" DataValueField="material_code">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="数量">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="tbinput1"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="单位">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtUnit" runat="server" CssClass="tbinput1"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="备注">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDescpt" runat="server" CssClass="tbinput1"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="操作">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnGrid2Save" runat="server" Text="保存" CssClass="btn1 auth" OnClick="btnGrid2Save_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="gridheader" />
                                                <RowStyle CssClass="gridrow" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            <asp:AsyncPostBackTrigger ControlID="GridView1" />
                            <asp:AsyncPostBackTrigger ControlID="Gridview2" />
                            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                            <asp:AsyncPostBackTrigger ControlID="btnckAll" />
                            <asp:AsyncPostBackTrigger ControlID="btnDelSel" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
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
