<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LbrctRecord.aspx.cs" Inherits="Device_LbrctRecord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>润滑记录查询</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#case").hide();
        });
        function GridClick() {
            $('#tabtop2').click();
            var ck = $("#ckFault").attr("checked");
            if (ck) {
                $("#case").show();
            }
            else {
                $("#case").hide();
            }
        }

        function ckFaultClick() {
            var ck = $("#ckFault").attr("checked");
            if (ck) {
                $("#case").show();
            }
            else {
                $("#case").hide();
            }
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
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">设备管理</a></li>
                <li><a href="#">设备润滑管理</a></li>
                <li><a href="#">润滑记录查询</a></li>
            </ul>
        </div>
        <div class="formbody">


            <div class="framelist">
                <div class="listtitle">
                    查询条件<span style="position: relative; float: right">
                        <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" OnClick="btnSearch_Click" />
                        <asp:HiddenField ID="txtCode" runat="server" />
                    </span>
                </div>
                <table class="tablelist">
                    <tbody>
                        <tr>
                            <td width="100">时间
                            </td>
                            <td>
                                <asp:TextBox ID="txtStart" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>至
                                <asp:TextBox ID="txtStop" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>

                            </td>
                        </tr>
                    </tbody>
                </table>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="listtitle" style="margin-top: 10px">
                                润滑计划列表<span>
                                    <asp:HiddenField ID="hdcode" runat="server" />
                                </span>
                            </div>

                            <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="PZ_CODE" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="12">
                                <Columns>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <%#(Container.DataItemIndex+1).ToString()%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="润滑计划" HeaderText="润滑计划" />
                                    <asp:BoundField DataField="部门" HeaderText="部门" />
                                    <asp:BoundField DataField="审批状态" HeaderText="审批状态" />
                                    <asp:BoundField DataField="执行状态" HeaderText="执行状态" />
                                    <asp:BoundField DataField="备注" HeaderText="备注" />
                                    <asp:TemplateField ItemStyle-Width="80">
                                        <ItemTemplate>
                                            <asp:Button ID="btnGridview" runat="server" Text="查看" CssClass="btnred" Width="75" OnClick="btnGridview_Click" />
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
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />

                        </Triggers>
                    </asp:UpdatePanel>
                </div>

                <div class="shade">
                    <div style="width: 1150px; height: 380px; position: absolute; top: 6%; left: 8%; background: #fcfdfd; box-shadow: 1px 8px 10px 1px #9b9b9b; border-radius: 1px; behavior: url(js/pie.htc);">
                        <div class="tiphead">
                            <span>润滑详情</span><a onclick="$('.shade').fadeOut(100);"></a>
                        </div>
                        <div class="gridinfo">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="GridView2" runat="server" class="grid" DataKeyNames="ID" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="GridView2_PageIndexChanging" PageSize="10">
                                        <Columns>
                                            <asp:TemplateField HeaderText="序号">
                                                <ItemTemplate>
                                                    <%#(Container.DataItemIndex+1).ToString()%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="工段">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="listGridsct" runat="server" CssClass="drpdwnlist" Width="180px" DataSource="<%# sectionbind() %>" DataTextField="Section_NAME" DataValueField="Section_CODE" Enabled="False">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="设备名称">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="listGridEq" runat="server" CssClass="drpdwnlist" DataSource="<%# eqbind() %>" DataTextField="EQ_NAME" DataValueField="IDKEY" Enabled="False" Width="180px">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="润滑部位">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtGridpos" runat="server" DataValueField="润滑部位" DataTextField="润滑部位"
                                                        CssClass="tbinput"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="润滑点数">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtGridnum" runat="server" DataValueField="润滑点数" DataTextField="润滑点数"
                                                        CssClass="tbinput"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="润滑油脂">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtGridoil" runat="server" DataValueField="润滑油脂" DataTextField="润滑油脂"
                                                        CssClass="tbinput"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="润滑周期">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtGriPric" runat="server" DataValueField="润滑周期" DataTextField="润滑周期"
                                                        CssClass="tbinput"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="润滑方式">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtGridStyle" runat="server" DataValueField="润滑方式" DataTextField="润滑方式"
                                                        CssClass="tbinput"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="润滑量">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtGridamount" runat="server" DataValueField="润滑量" DataTextField="润滑量"
                                                        CssClass="tbinput"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="过期时间">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtGridExptime" runat="server" DataValueField="过期时间" DataTextField="过期时间" Width="80px" CssClass="tbinput"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="状态">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="listGrid2Status" runat="server" CssClass="drpdwnlist" Width="70px" Enabled="False" DataSource="<%# statusbind() %>" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
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
                                    <asp:AsyncPostBackTrigger ControlID="GridView1" />

                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
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
