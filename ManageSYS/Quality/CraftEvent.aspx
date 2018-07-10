<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CraftEvent.aspx.cs" Inherits="Quality_CraftEvent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>工艺事件确认</title>
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
                <li><a href="#">工艺事件</a></li>
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
                        <div class="listtitle">
                            <span style="position: relative; float: right">
                                <asp:Button ID="btnSelAll1" runat="server" Text="全选" CssClass="btnview auth" OnClick="btnSelAll_Click" />
                                <asp:Button ID="btnIgnore1" runat="server" Text="忽略" CssClass="btndel auth" OnClick="btnIgnore_Click" />
                                <asp:Button ID="btnConfirm1" runat="server" Text="确认" CssClass="btnpatch auth" OnClick="btnConfirm_Click" />
                            </span>
                        </div>


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
                                 <asp:BoundField DataField="minus_score" HeaderText="扣分" />
                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:Button ID="btngrid1Ignore" runat="server" Text="忽略" CssClass="btn1" OnClick="btngrid1Ignore_Click"/>
                                        <asp:Button ID="btngrid1Sure" runat="server" Text="确认" CssClass="btn1"  OnClick="btngrid1Sure_Click"/>
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                            <AlternatingRowStyle CssClass="gridalterrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnConfirm1" />
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        <asp:AsyncPostBackTrigger ControlID="btnIgnore1" />
                        <asp:AsyncPostBackTrigger ControlID="btnSelAll1" />
                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
            <div id="tab2" class="tabson" style="margin-top: 0px; padding-top: 0px;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="listtitle">
                            <span style="position: relative; float: right">
                                <asp:Button ID="btnSelAll" runat="server" Text="全选" CssClass="btnview auth" OnClick="btnSelAll_Click" />
                                <asp:Button ID="btnIgnore" runat="server" Text="忽略" CssClass="btndel auth" OnClick="btnIgnore_Click" />
                                <asp:Button ID="btnConfirm" runat="server" Text="确认" CssClass="btnpatch auth" OnClick="btnConfirm_Click" />
                            </span>
                        </div>


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
                                        <asp:Button ID="btngrid2Ignore" runat="server" Text="忽略" CssClass="btn1" OnClick="btngrid2Ignore_Click" />
                                        <asp:Button ID="btngrid2Sure" runat="server" Text="确认" CssClass="btn1" OnClick="btngrid2Sure_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                            <AlternatingRowStyle CssClass="gridalterrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnConfirm" />
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        <asp:AsyncPostBackTrigger ControlID="btnIgnore" />
                        <asp:AsyncPostBackTrigger ControlID="btnSelAll" />
                        <asp:AsyncPostBackTrigger ControlID="GridView2" />
                    </Triggers>
                </asp:UpdatePanel>

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
