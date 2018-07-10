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
                        <asp:GridView ID="GridView1" runat="server" class="grid" AllowPaging="True" AutoGenerateColumns="False"
                            DataKeyNames="id,type">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ck" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="产品" DataField="prod_name" />
                                <asp:BoundField HeaderText="工艺点" DataField="para_name" />
                                <asp:BoundField HeaderText="类型" DataField="type" />
                                <asp:BoundField HeaderText="值" DataField="value" />
                                <asp:BoundField HeaderText="范围" DataField="range" />
                                <asp:BoundField HeaderText="开始时间" DataField="b_time" />
                                <asp:BoundField HeaderText="结束时间" DataField="e_time" />
                                <asp:BoundField HeaderText="班组" DataField="team_name" />
                                <asp:TemplateField HeaderText="状态">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="labStatus" CssClass="labstatuGreen" Width="70px"> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:Button ID="btngrid1Deal" runat="server" Text="处理" CssClass="btn1" OnClick="btngrid1Deal_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                            <AlternatingRowStyle CssClass="gridalterrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
                        <asp:AsyncPostBackTrigger ControlID ="btnModify" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
            <div id="tab2" class="tabson" style="margin-top: 0px; padding-top: 0px;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView2" runat="server" class="grid" AllowPaging="True" AutoGenerateColumns="False"
                            DataKeyNames="ID,inspect_code">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ck" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="检验类型" DataField="inspect_type" />
                                <asp:BoundField HeaderText="分组" DataField="insgroup" />
                                <asp:BoundField HeaderText="检验项目" DataField="inspect_name" />
                                <asp:BoundField HeaderText="检测值" DataField="value" />

                                <asp:BoundField HeaderText="标准范围" DataField="range" />

                                <asp:BoundField DataField="unit" HeaderText="单位" />
                                <asp:TemplateField HeaderText="状态">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="labStatus" CssClass="labstatuGreen" Width="70px"> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="minus_score" HeaderText="扣分" />
                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:Button ID="btngrid2Deal" runat="server" Text="处理" CssClass="btn1 auth"  OnClick="btngrid2Deal_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                            <AlternatingRowStyle CssClass="gridalterrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        <asp:AsyncPostBackTrigger ControlID="GridView2" />
                        <asp:AsyncPostBackTrigger ControlID ="btnModify" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>


            <div class="shade">
                <div class="info">
                    <div class="tiphead">
                        <span>员工信息</span><a onclick="$('.shade').fadeOut(100);"></a>
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
                                            <asp:TextBox ID="txtStyle" runat="server" CssClass="dfinput1" Width="250px" Enabled="False"></asp:TextBox></td>
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
