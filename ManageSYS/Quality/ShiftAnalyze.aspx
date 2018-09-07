<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShiftAnalyze.aspx.cs" Inherits="Quality_ShiftAnalyze" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>班组评估</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
</head>
<body>
    <form id="Form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">质量评估</a></li>
                <li><a href="#">每日班组评估</a></li>
            </ul>
        </div>
        <div class="rightinfo">
            <table class="tablelist" style="margin-bottom: 8px">
                <tr>
                    <td align="center">时间
                    <asp:TextBox ID="txtStartTime" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                        CssClass="dfinput1"></asp:TextBox>
                        <asp:DropDownList ID="listTeam" runat="server" CssClass="drpdwnlist" Width="80px">
                        </asp:DropDownList>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn1 auth" Text="查找" OnClick="btnSearch_Click" />
                        &nbsp;&nbsp;
                    <asp:Button ID="btnExport" runat="server" Text="导出" CssClass="btn1 auth" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridAll" runat="server" class="grid" AutoGenerateColumns="True"
                                    ShowHeaderWhenEmpty="True">

                                    <HeaderStyle CssClass="gridheader" />
                                    <RowStyle CssClass="gridrow" />
                                    <AlternatingRowStyle CssClass="gridalterrow" />
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <div id="usual1" class="usual">
                <div class="itab">
                    <ul>
                        <li><a href="#tab1" class="selected" id="tabtop1">在线评测</a></li>
                        <li><a href="#tab2" id="tabtop2">过程检测</a></li>
                        <li><a href="#tab3" id="tabtop3">理化检测</a></li>
                        <li><a href="#tab4" id="tabtop4">感观评测</a></li>
                    </ul>
                </div>
            </div>
            <div id="tab1" class="tabson">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True"
                            CssClass="grid">

                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                            <AlternatingRowStyle CssClass="gridalterrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div id="tab2" class="tabson">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView2" runat="server" ShowHeaderWhenEmpty="True"
                            CssClass="grid">

                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                            <AlternatingRowStyle CssClass="gridalterrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div id="tab3" class="tabson">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView3" runat="server" ShowHeaderWhenEmpty="True"
                            CssClass="grid">

                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                            <AlternatingRowStyle CssClass="gridalterrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
            <div id="tab4" class="tabson">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView4" runat="server" ShowHeaderWhenEmpty="True"
                            CssClass="grid">

                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                            <AlternatingRowStyle CssClass="gridalterrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
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
