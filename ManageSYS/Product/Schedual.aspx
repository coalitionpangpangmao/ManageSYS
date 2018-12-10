<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Schedual.aspx.cs" Inherits="Product_Schedual" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>生产排班</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".click1").click(function () {
                $("#addtip").fadeIn(200);
            });
            $(".tiptop a").click(function () {
                $(".tip").fadeOut(200);
            });

            $(".sure").click(function () {
                $(".tip").fadeOut(100);
            });

            $(".cancel").click(function () {
                $(".tip").fadeOut(100);
            });

        });
        function GridClick(code) {
            $('#tabtop2').click();

        }
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
                <li><a href="#">生产计划排班</a></li>
            </ul>
        </div>
        <div class="formbody">
            <div class="framelist">
                <div class="listtitle">
                    排班管理<span style="position: relative; float: right">
                        <asp:Button ID="btnAdd" runat="server" Text="自动排班" CssClass="btnmodify auth" OnClick="btnAdd_Click" Width="100" />
                        <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview auth" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="btndel auth" OnClick="btnDel_Click" />
                    </span>
                </div>
                <table class="tablelist">
                    <tbody>
                        <tr>
                            <td width="100">车间名称
                            </td>
                            <td>
                                <asp:DropDownList ID="listPrdline" runat="server"
                                    CssClass="drpdwnlist">
                                    <asp:ListItem Value="703">再造梗丝生产线</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td width="100">排班区间
                            </td>
                            <td>
                                <asp:TextBox ID="txtStartDate" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>至
                            <asp:TextBox ID="txtEndDate" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="border-style: solid; border-width: thin; margin-right: 8px">
                                <div class="listtitle">班时浏览  </div>
                                <asp:GridView ID="GridView1" runat="server" class="grid" AllowPaging="False" DataKeyNames="班时编码"
                                    AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="班时" HeaderStyle-Wrap="False" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtShift" runat="server" DataValueField="班时" DataTextField="班时" CssClass="tbinput" Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="开始时间" HeaderStyle-Wrap="False" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtStarttime" runat="server" DataValueField="开始时间" DataTextField="开始时间" CssClass="tbinput1" Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="结束时间" HeaderStyle-Wrap="False" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEndtime" runat="server" DataValueField="结束时间" DataTextField="结束时间" Enabled="false" CssClass="tbinput1"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="是否跨天" HeaderStyle-Wrap="False" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ckInter" runat="server" Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <HeaderStyle CssClass="gridheader" />
                                    <RowStyle CssClass="gridrow" />
                                    <AlternatingRowStyle CssClass="gridalterrow" />
                                </asp:GridView>
                            </td>
                            <td colspan="2" style="border-style: solid; border-width: thin; margin-left: 8px">
                                <div class="listtitle">
                                    班组轮换 <span style="position: relative; float: right">
                                        <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btnmodify auth" OnClick="btnSave_Click" Width="100" />
                                    </span>
                                </div>
                                <asp:GridView ID="GridView3" runat="server" class="grid" AllowPaging="False" DataKeyNames="ID"
                                    AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="顺序号" HeaderStyle-Wrap="False" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtOrder" runat="server" CssClass="tbinput" Enabled="false" Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="早班" HeaderStyle-Wrap="False" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                            <ItemTemplate>
                                                <asp:DropDownList ID="listTeam1" runat="server" CssClass="drpdwnlist" Width="70px" DataSource='<%# gridTeambind()%>' DataValueField="team_code" DataTextField="team_name">
                                                </asp:DropDownList>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="中班"  HeaderStyle-Wrap="False" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="listTeam2" runat="server" CssClass="drpdwnlist" Width="70px" DataSource='<%# gridTeambind()%>' DataValueField="team_code" DataTextField="team_name">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="晚班" HeaderStyle-Wrap="False" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="listTeam3" runat="server" CssClass="drpdwnlist" Width="70px" DataSource='<%# gridTeambind()%>' DataValueField="team_code" DataTextField="team_name">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <HeaderStyle CssClass="gridheader" />
                                    <RowStyle CssClass="gridrow" />
                                    <AlternatingRowStyle CssClass="gridalterrow" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="listtitle" style="margin-top: 10px">
                    生产日历<span style="position: relative; float: right">
                        <asp:Button ID="btnckAll" runat="server" Text="全选" class="btnset auth" OnClick="btnckAll_Click" />
                        <asp:Button ID="btnGridDel" runat="server" Text="删除" class="btndel" OnClick="btnGridDel_Click" OnClientClick="javascript:return confirm('确认删除？');" />
                        <asp:Button ID="btnGridEdit" runat="server" Text="保存" class="btnmodify" OnClick="btnGridEdit_Click" />
                    </span>
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView2" runat="server" class="grid" DataKeyNames="ID" AllowPaging="true" OnPageIndexChanging="GridView2_PageIndexChanging"
                                AutoGenerateColumns="False" PageSize="9">
                                <Columns>
                                    <asp:BoundField DataField="日期" HeaderText="日期" />
                                    <asp:BoundField DataField="班组" HeaderText="班组" />
                                    <asp:BoundField DataField="班时" HeaderText="班时" />
                                    <asp:BoundField DataField="开始时间" HeaderText="开始时间" />
                                    <asp:BoundField DataField="结束时间" HeaderText="结束时间" />
                                    <asp:TemplateField HeaderText="状态">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="listStatus2" runat="server" CssClass="drpdwnlist" Width="60px">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="1">工作</asp:ListItem>
                                                <asp:ListItem Value="0">休息</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridheader" />
                                <RowStyle CssClass="gridrow" />
                                <AlternatingRowStyle CssClass="gridalterrow" />
                                <PagerStyle CssClass="gridpager" />
                                <PagerTemplate>
                                    <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> ' Width="120px"></asp:Label>
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
                            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                            <asp:AsyncPostBackTrigger ControlID="btnckAll" />
                            <asp:AsyncPostBackTrigger ControlID="btnGridEdit" />
                            <asp:AsyncPostBackTrigger ControlID="btnGridDel" />
                            <asp:AsyncPostBackTrigger ControlID="GridView2" />
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                            <asp:AsyncPostBackTrigger ControlID="btnDel" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <script type="text/javascript">
                $('.tablelist tbody tr:odd').addClass('odd');
            </script>
        </div>
    </form>
</body>
</html>
