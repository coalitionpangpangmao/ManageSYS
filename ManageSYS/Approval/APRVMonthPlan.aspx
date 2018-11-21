<%@ Page Language="C#" AutoEventWireup="true" CodeFile="APRVMonthPlan.aspx.cs" Inherits="Approval_APRVMonthPlan " %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>审批管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        function DetailClick() {
            $('#tabtop2').click();

        }
        function AprvTable() {
            $('#tabtop3').click();

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
            <li><a href="#">权限管理</a></li>
            <li><a href="#">审批管理</a></li>
        </ul>
    </div>
    <div class="formbody">
        <div id="usual1" class="usual">
            <div class="itab">
                <ul>
                    <li><a href="#tab1" class="selected" id="tabtop1">审批表</a></li>
                    <li><a href="#tab2" id="tabtop2">业务明细</a></li>
                    <li><a href="#tab3" id="tabtop3">审批单详情</a></li>
                </ul>
            </div>
        </div>
        <div id="tab1" class="tabson">
            <div class="framelist">
                <div class="listtitle">
                    查询<span style="position: relative; float: right">
                        <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" OnClick="btnSearch_Click" />
                    </span>
                </div>
                <table class="tablelist">
                    <tbody>
                        <tr>
                            <td width="100">
                                时间从
                            </td>
                            <td>
                                <asp:TextBox ID="txtStarttime" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                至
                                <asp:TextBox ID="txtEndtime" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp; 办理中
                                <asp:CheckBox ID="ckDone" runat="server" Text=" " Checked="true" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="listtitle">
                    列表 <span style="position: relative; float: right">
                        <asp:Button ID="btnCkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnCkAll_Click" />
                        <asp:Button ID="btnAprvAll" runat="server" Width="110px" CssClass="btnview  auth"
                            Text="批量审批" OnClick="btnAprvAll_Click" /></span>
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="ID,gongwen_ID,BUSIN_ID"  AllowPaging ="true"  OnPageIndexChanging="GridView1_PageIndexChanging"
                                AutoGenerateColumns="False" PageSize ="12">
                                <Columns>
                                    <asp:TemplateField       >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="业务名" HeaderText="业务名"  />
                                    <asp:BoundField DataField="申请人" HeaderText="申请人"  />
                                    <asp:BoundField DataField="申请部门" HeaderText="申请部门"  />
                                    <asp:TemplateField      HeaderText="主业务审批状态">
                                        <ItemTemplate>
                                            <asp:Label ID="labStatus1" runat="server" CssClass="labstatu" Text="" Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField      HeaderText="当前流程状态">
                                        <ItemTemplate>
                                            <asp:Label ID="labStatus2" runat="server" CssClass="labstatu" Text="" Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField      HeaderText="操作" >
                                        <ItemTemplate>
                                            <asp:Button ID="btnDetail" runat="server" Text="业务明细" CssClass="btn1 auth" Width="80px"
                                                OnClick="btnDetail_Click" />
                                            <asp:Button ID="btnFLow" runat="server" Text="审批流程" CssClass="aprvclickbtn" OnClick="btnFLow_Click" />
                                            <asp:Button ID="btnTable" runat="server" Text="查看审批单" CssClass="btn1 auth" Width="80px"
                                                OnClick="btnTable_Click" />
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
                            <asp:AsyncPostBackTrigger ControlID="btnCkAll" />
                            <asp:AsyncPostBackTrigger ControlID="btnAprvAll" />
                            <asp:AsyncPostBackTrigger ControlID="GridView1" />
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                            <asp:AsyncPostBackTrigger ControlID = "btnSure" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div id="tab2" class="tabson">
            <div class="framelist">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table class="tablelist" style="margin-top: 10px">
                            <tr>
                                <th>
                                    审批
                                </th>
                                <th>
                                    <asp:RadioButton ID="rdAprv1" runat="server" Text="同意" GroupName="Group1" OnCheckedChanged="rdAprv1_CheckedChanged"
                                        AutoPostBack="True" />
                                    <asp:RadioButton ID="rdAprv2" runat="server" Text="不同意" GroupName="Group1" OnCheckedChanged="rdAprv2_CheckedChanged"
                                        AutoPostBack="True" />
                                    <asp:HiddenField ID="hideAprvid" runat="server" />
                                </th>
                                <th>
                                    <asp:Button ID="btnSure" runat="server" Text="确定" CssClass="btnmodify  auth" OnClick="btnSure_Click" />
                                </th>
                            </tr>
                            <tbody>
                                <tr>
                                    <td width="100">
                                        审批意见：
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtComments" runat="server" class="aprvtxt" Height="70px" TextMode="MultiLine"></asp:TextBox>
                                </tr>
                            </tbody>
                        </table>
                        <div class="listtitle">
                            业务明细
                        </div>
                        <div style="height: 300px; overflow: scroll">
                            <asp:GridView ID="GridView2" runat="server" class="grid">
                                <HeaderStyle CssClass="gridheader" />
                                <RowStyle CssClass="gridrow" />
                                 <AlternatingRowStyle CssClass="gridalterrow" />
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
                        <asp:AsyncPostBackTrigger ControlID="btnSure" />
                        <asp:AsyncPostBackTrigger ControlID="rdAprv1" />
                        <asp:AsyncPostBackTrigger ControlID="rdAprv2" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="tab3" class="tabson">
            <div class="framelist">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <% = AprvtableHtml %>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="aprvinfo" id="flowinfo">
        <div class="tiptop">
            <span>审批流程详情</span><a onclick="Aprvlisthide()"></a></div>
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
                    <asp:AsyncPostBackTrigger ControlID="GridView1" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <script type="text/javascript">
        $("#usual1 ul").idTabs();  
    </script>
    </form>
</body>
</html>
