<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Inspect_online.aspx.cs" Inherits="Quality_Inspect_online" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>在线数据统计</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
</head>
<body>
    <form id="Form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">质量评估</a></li>
                <li><a href="#">在线数据统计</a></li>
            </ul>
        </div>

        <div class="mainbox">

            <table class="tablelist" style="margin-bottom: 5px">
                <tbody>
                    <tr>
                        <td colspan="7" align="center">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    时间     
                    <asp:TextBox ID="txtBtime" runat="server" CssClass="dfinput1"
                        onclick="WdatePicker({dateFmt:'yyyy-MM'})" OnTextChanged="txtBtime_TextChanged" AutoPostBack="true"></asp:TextBox>

                                    产品：<asp:DropDownList ID="listProd" runat="server" CssClass="drpdwnlist"></asp:DropDownList>
                                    工艺段：<asp:DropDownList ID="listSection" runat="server" CssClass="drpdwnlist" OnSelectedIndexChanged="listSection_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    数据点：<asp:DropDownList ID="listPoint" runat="server" CssClass="drpdwnlist"></asp:DropDownList>
                                    &nbsp;                         
                            <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" OnClick="btnSearch_Click" />
                                </ContentTemplate>
                                <Triggers>

                                    <asp:AsyncPostBackTrigger ControlID="txtBtime" />
                                    <asp:AsyncPostBackTrigger ControlID="listSection" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" class="grid"  AllowPaging ="true"  OnPageIndexChanging ="GridView1_PageIndexChanging" PagerSettings-FirstPageText="第一页" PagerSettings-LastPageText="最后页" PagerSettings-Mode="NumericFirstLast" PageSize="12">

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





    </form>
</body>

</html>
