<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Evaluat_Sensor.aspx.cs" Inherits="Quality_Evaluat_Sensor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>感观评测报告</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">质量评估与分析</a></li>
                <li><a href="#">质量考核</a></li>
                <li><a href="#">感观评测报告</a></li>
            </ul>
        </div>
        <div class="formbody">
            <div id="usual1" class="usual">
                <div class="itab">
                    <ul>
                        <li><a href="#tab1" class="selected" id="tabtop1">感观评测报表列表</a></li>
                        <li><a href="#tab2" id="tabtop2">感观评测报表详情</a></li>
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

                                <td width="100">检测月份
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMonth" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM'})"></asp:TextBox>
                                </td>
                                <td width="100">产品
                                </td>
                                <td>
                                    <asp:DropDownList ID="listProd" runat="server" CssClass="drpdwnlist"></asp:DropDownList>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div class="listtitle" style="margin-top: 10px">
                        感观评测记录列表
                    </div>
                    <div style="overflow: auto">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="GridView1" runat="server" class="grid" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="15" HeaderStyle-Wrap="False" RowStyle-Wrap="False" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="序号">
                                            <ItemTemplate>
                                                <%#(Container.DataItemIndex+1).ToString()%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="sensor_month" HeaderText="评吸月份" />
                                        <asp:BoundField DataField="prod_name" HeaderText="产品名称" />
                                        <asp:BoundField DataField="editor" HeaderText="评吸人" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnGridEdit" runat="server" Text="查看详情" CssClass="btn1" Width="90px"
                                                    OnClick="btnGridEdit_Click" />
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
                </div>
            </div>
            <div id="tab2" class="tabson">
                <div class="framelist">
                    <div class="listtitle" style="margin-top: 10px">
                        感观评测报表<span style="position: relative; float: right"><asp:Button ID="btnExport" runat="server"
                            CssClass="btnset auth" Text="导出" OnClick="btnExport_Click" />
                        </span>
                    </div>
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div>
                            <asp:HiddenField ID="hideMonth" runat="server" /></div>
                                <asp:GridView ID="GridView2" runat="server" class="grid" AllowPaging="true" OnPageIndexChanging="GridView2_PageIndexChanging" PageSize="15" HeaderStyle-Wrap="False" RowStyle-Wrap="False"
                                    AutoGenerateColumns="true" RowStyle-BorderStyle="Solid" RowStyle-BorderWidth="1">
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
