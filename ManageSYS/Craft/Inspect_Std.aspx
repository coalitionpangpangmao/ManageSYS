<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Inspect_Std.aspx.cs" Inherits="Craft_InspectStd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>工艺检查项目标准</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
     <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">质量分析与评估</a></li>
                <li><a href="#">工艺检查项目标准</a></li>
            </ul>
        </div>
        <div class="formbody">
            <div id="usual1" class="usual">
                <div class="itab">
                    <ul>
                        <li><a href="#tab1" id="tabtop1">理化检测</a></li>
                        <li><a href="#tab2" id="tabtop2">感观评测</a></li>
                        <li><a href="#tab3" id="tabtop3">过程检测</a></li>
                    </ul>
                </div>
            </div>
            <div id="tab1" class="tabson">
                <div class="listtitle" style="margin-top: 10px">
                    检查项目列表 <span style="position: relative; float: right">

                        <asp:Button ID="btnGrid1CkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnGrid1CkAll_Click" />
                        <asp:Button ID="btnGrid1Save" runat="server" Text="保存" CssClass="btnmodify" OnClick="btnGrid1Save_Click" />
                        <asp:Button ID="btnGrid1DelSel" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnGrid1DelSel_Click" OnClientClick="javascript:return confirm('确认删除？');" /></span>
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" class="grid" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="14"
                                DataKeyNames="检查项目编码">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ck" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="分组">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="listGroup" runat="server" CssClass='drpdwnlist' Enabled="false" Width="200px">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="检查项目">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="listInspect" runat="server" CssClass='drpdwnlist' Enabled="false">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="下限">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtLower" runat="server" onkeyup="value=value.replace(/[^\d\|\-\.]/g,'')" CssClass='tbinput'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="上限">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUpper" runat="server" onkeyup="value=value.replace(/[^\d\|\-\.]/g,'')" CssClass='tbinput'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="单次扣分">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtScore" runat="server" onkeyup="value=value.replace(/[^\d\|\-\.]/g,'')" CssClass='tbinput'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="备注">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass='tbinput1'></asp:TextBox>
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

                            <asp:AsyncPostBackTrigger ControlID="btnGrid1CkAll" />
                            <asp:AsyncPostBackTrigger ControlID="btnGrid1Save" />
                            <asp:AsyncPostBackTrigger ControlID="btnGrid1DelSel" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div id="tab2" class="tabson">
                <div class="listtitle" style="margin-top: 10px">
                    检查项目列表 <span style="position: relative; float: right">

                        <asp:Button ID="btnGrid2CkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnGrid2CkAll_Click" />
                        <asp:Button ID="btnGrid2Save" runat="server" Text="保存" CssClass="btnmodify" OnClick="btnGrid2Save_Click" />
                        <asp:Button ID="btnGrid2DelSel" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnGrid2DelSel_Click" OnClientClick="javascript:return confirm('确认删除？');" /></span>
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView2" runat="server" class="grid" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="GridView2_PageIndexChanging" PageSize="14"
                                DataKeyNames="检查项目编码">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ck" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="检查项目">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="listInspect" runat="server" CssClass='drpdwnlist' Enabled="false">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="项目占比分">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtScore" runat="server" onkeyup="value=value.replace(/[^\d\.]/g,'')" CssClass='tbinput'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="备注">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass='tbinput1'></asp:TextBox>
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

                            <asp:AsyncPostBackTrigger ControlID="btnGrid2CkAll" />
                            <asp:AsyncPostBackTrigger ControlID="btnGrid2Save" />
                            <asp:AsyncPostBackTrigger ControlID="btnGrid2DelSel" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div id="tab3" class="tabson">
                <div class="listtitle" style="margin-top: 10px">
                    检查项目列表 <span style="position: relative; float: right">

                        <asp:Button ID="btnGrid3CkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnGrid3CkAll_Click" />
                        <asp:Button ID="btnGrid3Save" runat="server" Text="保存" CssClass="btnmodify" OnClick="btnGrid3Save_Click" />
                        <asp:Button ID="btnGrid3DelSel" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnGrid3DelSel_Click" OnClientClick="javascript:return confirm('确认删除？');" /></span>
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView3" runat="server" class="grid" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="GridView3_PageIndexChanging" PageSize="14"
                                DataKeyNames="检查项目编码">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ck" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="分组">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="listGroup" runat="server" CssClass='drpdwnlist' Enabled="false" Width="200px">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="检查项目">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="listInspect" runat="server" CssClass='drpdwnlist' Enabled="false">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="下限">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtLower" runat="server" onkeyup="value=value.replace(/[^\d\|\-\.]/g,'')" CssClass='tbinput'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="上限">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUpper" runat="server" onkeyup="value=value.replace(/[^\d\|\-\.]/g,'')" CssClass='tbinput'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="单次扣分">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtScore" runat="server" onkeyup="value=value.replace(/[^\d\|\-\.]/g,'')" CssClass='tbinput'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="备注">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass='tbinput1'></asp:TextBox>
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

                            <asp:AsyncPostBackTrigger ControlID="btnGrid3CkAll" />
                            <asp:AsyncPostBackTrigger ControlID="btnGrid3Save" />
                            <asp:AsyncPostBackTrigger ControlID="btnGrid3DelSel" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <script type="text/javascript">
            $('.tablelist tbody tr:odd').addClass('odd');
            $("#usual1 ul").idTabs();
        </script>
    </form>
</body>
</html>
