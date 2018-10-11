<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LbrctFeedback.aspx.cs" Inherits="Device_LbrctFeedback" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>滑润反馈</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/select.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>

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
                <li><a href="#">润滑情况反馈</a></li>
            </ul>
        </div>
        <div class="formbody">
            <div id="usual1" class="usual">
                <div class="itab">
                    <ul>
                        <li><a href="#tab1" class="selected" id="tabtop1">润滑计划列表</a></li>
                        <li><a href="#tab2" id="tabtop2">润滑记录</a></li>
                    </ul>
                </div>
            </div>
            <div id="tab1" class="tabson">
                <div class="framelist">
                     <div class="listtitle" style="margin-top: 0px; padding-top: 0px;">
                        查询条件<span style="position: relative; float: right">
                            <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" OnClick="btnSearch_Click" />
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
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:CheckBox ID="ckDone" runat="server" Text="己反馈" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div class="listtitle" style="margin-top: 10px">
                        润滑计划列表                 
                    </div>
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="PZ_CODE" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="8">
                                    <Columns>
                                        <asp:BoundField DataField="润滑计划" HeaderText="润滑计划" />
                                        <asp:BoundField DataField="计划号" HeaderText="计划号" />
                                        <asp:BoundField DataField="过期时间" HeaderText="过期时间" />
                                        <asp:BoundField DataField="申请人" HeaderText="申请人" />
                                        <asp:BoundField DataField="备注" HeaderText="备注" />
                                        <asp:TemplateField ItemStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:Button ID="btnGridview" runat="server" Text="查看" CssClass="btn1 auth" Width="75" OnClick="btnGridview_Click" />
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
                                <asp:AsyncPostBackTrigger ControlID="btnModify" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div id="tab2" class="tabson">
                <div class="framelist">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <div class="listtitle" style="margin-top: 10px">
                                润滑计划明细   
                                <asp:HiddenField ID="txtCode" runat="server" />
                            </div>

                            <asp:GridView ID="GridView2" runat="server" class="grid" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="GridView2_PageIndexChanging" PageSize="10"
                                DataKeyNames="ID">
                                <Columns>
                                     <asp:BoundField DataField="工段" HeaderText="工段" />
                                        <asp:BoundField DataField="设备名称" HeaderText="设备名称" />
                                        <asp:BoundField DataField="润滑部位" HeaderText="润滑部位" />
                                       <asp:BoundField DataField="润滑点数" HeaderText="润滑点数" />
                                        <asp:BoundField DataField="润滑油脂" HeaderText="润滑油脂" />
                                        <asp:BoundField DataField="润滑周期" HeaderText="润滑周期" />  
                                 <asp:BoundField DataField="润滑方式" HeaderText="润滑方式" />
                                        <asp:BoundField DataField="润滑量" HeaderText="润滑量" />
                                        <asp:BoundField DataField="过期时间" HeaderText="过期时间" />  
                                           
                                    <asp:TemplateField HeaderText="状态">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="listGrid2Status" runat="server" CssClass="drpdwnlist" Width="70px" Enabled="False" DataSource="<%# statusbind() %>" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                
                                    <asp:TemplateField HeaderText="操作">
                                        <ItemTemplate>
                                            <asp:Button ID="btngrid2Deal" runat="server" Text="反馈情况" CssClass="btn1 auth" OnClick="btngrid2Deal_Click" Width="100px" />
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
                            <asp:AsyncPostBackTrigger ControlID="GridView2" />
                            <asp:AsyncPostBackTrigger ControlID="btnModify" />
                            <asp:AsyncPostBackTrigger ControlID="GridView1" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="shade">
                <div class="info">
                    <div class="tiphead">
                        <span>反馈情况</span><a onclick="$('.shade').fadeOut(100);"></a>
                    </div>
                    <div class="gridinfo">
                        <asp:UpdatePanel ID="updtpanel1" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdType" runat="server" />
                                <table class="tablelist">
                                    <tr>
                                        <td>校准计划凭证号： </td>
                                        <td>
                                            <asp:TextBox ID="txtCodeZ" runat="server" CssClass="dfinput1" Width="450px" Enabled="False"></asp:TextBox></td>
                                    </tr>
                                    <tr style="padding-top: 5px">
                                        <td>明细序号： </td>
                                        <td>
                                            <asp:TextBox ID="txtID" runat="server" CssClass="dfinput1" Width="450px" Enabled="False"></asp:TextBox></td>
                                        </td>
                                    </tr>
                                    <tr style="padding-top: 5px">
                                        <td>反馈情况
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtScean" runat="server" CssClass="dfinput1" Height="70px"
                                                Width="450px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="padding-top: 5px">
                                        <td>补充说明
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPlus" runat="server" CssClass="dfinput1" Height="70px"
                                                Width="450px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>

                                <div class="shadebtn" align="center">
                                    <asp:Button ID="btnModify" class="sure" runat="server" Text="保存" OnClick="btnModify_Click" />
                                    <input name="" type="button" class="cancel" value="关闭" onclick="$('.shade').fadeOut(100);" />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="GridView1" />
                                <asp:AsyncPostBackTrigger ControlID="GridView2" />
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
