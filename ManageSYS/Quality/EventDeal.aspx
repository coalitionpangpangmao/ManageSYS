<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EventDeal.aspx.cs" Inherits="Quality_EventDeal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>工艺事件处理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
</head>
<body>
    <form id="Form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">质量评估</a></li>
                <li><a href="#">工艺事件处理</a></li>
            </ul>
        </div>

        <div class="mainbox">
            <table class="tablelist" style="margin-bottom: 5px">
                <tbody>
                    <tr>
                        <td colspan="7" align="center">时间     
                    <asp:TextBox ID="txtBtime" runat="server" CssClass="dfinput1"
                        onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                            至
            <asp:TextBox ID="txtEtime" runat="server" CssClass="dfinput1"
                onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>

                            &nbsp;&nbsp;&nbsp;
                        
                            <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" OnClick="btnSearch_Click" />
                        </td>

                    </tr>
                </tbody>
            </table>
            <div id="usual1" class="usual">
                <div class="itab">
                    <ul>
                        <li><a href="#tab1" class="selected" id="tabtop1">在线工艺事件</a></li>
                        <li><a href="#tab2" id="tabtop2">离线工艺事件</a></li>
                    </ul>
                </div>
            </div>
            <div id="tab1" class="tabson" style="margin-top: 0px; padding-top: 0px;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" class="grid"  AllowPaging ="true"  OnPageIndexChanging ="GridView1_PageIndexChanging"  PageSize="14"  AutoGenerateColumns="False"
                            DataKeyNames="id,type">
                            <Columns>
                                <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ck" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="产品" DataField="prod_name" />
                                 <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="工艺点" DataField="para_name" />
                                 <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="类型" DataField="name" />
                                 <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="值" DataField="value" />
                                 <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="范围" DataField="range" />
                                 <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="开始时间" DataField="b_time" />
                                 <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="结束时间" DataField="e_time" />
                                 <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="班组" DataField="team_name" />
                                <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="状态">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="labStatus" CssClass="labstatuGreen" Width="70px" Text="处理中"> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:Button ID="btngrid1Deal" runat="server" Text="处理" CssClass="btn1" OnClick="btngrid1Deal_Click" />
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
                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
                        <asp:AsyncPostBackTrigger ControlID="btnModify" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
            <div id="tab2" class="tabson" style="margin-top: 0px; padding-top: 0px;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView2" runat="server" class="grid" AllowPaging ="true"  OnPageIndexChanging ="GridView2_PageIndexChanging"  PageSize="14"  AutoGenerateColumns="False"
                            DataKeyNames="ID,inspect_code">
                            <Columns>
                                <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ck" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="RECORD_TIME" HeaderText="检测时间" />
                                   <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="产品" DataField="prod_name" />
                                   <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="班组" DataField="team_name" />
                                 <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="检验类型" DataField="inspect_type" />
                                 <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="分组" DataField="insgroup" />
                                 <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="检验项目" DataField="inspect_name" />
                                 <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="检测值" DataField="value" />

                                 <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="标准范围" DataField="range" />

                                 <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="unit" HeaderText="单位" />
                                <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="状态">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="labStatus" CssClass="labstatuGreen" Width="70px" Text="处理中"> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="minus_score" HeaderText="扣分" />
                                <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:Button ID="btngrid2Deal" runat="server" Text="处理" CssClass="btn1 auth" OnClick="btngrid2Deal_Click" />
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
                    </Triggers>
                </asp:UpdatePanel>

            </div>


            <div class="shade">
                <div class="info">
                    <div class="tiphead">
                        <span>工艺事件处理情况</span><a onclick="$('.shade').fadeOut(100);"></a>
                    </div>
                    <div class="gridinfo">
                        <asp:UpdatePanel ID="updtpanel1" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdType" runat="server" />
                                <table class="tablelist">
                                    <tr>
                                        <td>事件记录ID： </td>
                                        <td>
                                            <asp:TextBox ID="txtEventID" runat="server" CssClass="dfinput1" Width="250px" Enabled="False"></asp:TextBox></td>
                                        <td>类型： </td>
                                        <td>
                                            <asp:DropDownList ID="listStyle" runat="server" CssClass="drpdwnlist" Width="250px" Enabled="false"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="padding-top: 5px">
                                        <td>现场记录
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtScean" runat="server" CssClass="dfinput1" Height="100px"
                                                Width="250px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                        <td>原因分析
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtReason" runat="server" CssClass="dfinput1" Height="100px"
                                                Width="250px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="padding-top: 5px">
                                        <td>处理措施
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDeal" runat="server" CssClass="dfinput1" Height="100px"
                                                Width="250px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                        <td>补充说明
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPlus" runat="server" CssClass="dfinput1" Height="100px"
                                                Width="250px" TextMode="MultiLine"></asp:TextBox>
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
        </div>

        <script type="text/javascript">
            $("#usual1 ul").idTabs();
        </script>
        <script type="text/javascript">
            $('.tablelist tbody tr:odd').addClass('odd');
        </script>
    </form>
</body>

</html>
