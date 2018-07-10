<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CollectSet.aspx.cs" Inherits="Quality_CollectSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>质量采集点设置</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <link rel="stylesheet" href="../js/jquery-treeview/jquery.treeview.css" />
    <link rel="stylesheet" href="../js/jquery-treeview/screen.css" />
    <script type="text/javascript" src="../js/jquery-treeview/jquery.cookie.js"></script>
    <script src="../js/jquery-treeview/jquery.treeview.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#browser").treeview({
                toggle: function () {
                    console.log("%s was toggled.", $(this).find(">span").text());
                }
            });
        });
        function treeClick(code) {
            debugger;
            $("#hidecode").val(code);
            $("#btnView").click();

            //在charframe中添加控件，完成数据刷新
        }

    </script>
</head>
<body>
    <script type="text/javascript" src="../js/code/highcharts.js"></script>
    <script type="text/javascript" src="../../modules/series-label.js"></script>
    <script type="text/javascript" src="../../modules/exporting.js"></script>
    <form id="Form1" runat="server">
    <div class="place">
        <span>位置:</span>
        <ul class="placeul">
            <li><a href="#">质量分析与评估</a></li>
            <li><a href="#">采集设置</a></li>
        </ul>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="mainbox">
        <div class="mainleft">
            <div class="leftinfo">
                <div class="listtitle">
                    工艺模型</div>
                <% = tvHtml %>
            </div>
            <!--leftinfo end-->
        </div>
        <!--mainleft end-->
        <div class="mainright">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="framelist">
                        <tr>
                            <td>
                                <table class="tablelist">
                                    <tr>
                                        <th colspan="4">
                                            数据点设定
                                            <asp:HiddenField ID="hidecode" runat="server" />
                                            <asp:Button ID="btnView" runat="server" Text="Button" OnClick="btnView_Click" CssClass="btnhide" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <td width="100px">
                                            父模型
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listSection" runat="server" CssClass="drpdwnlist" AutoPostBack="True"
                                                OnSelectedIndexChanged="listSection_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="100px">
                                            权重：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWeight" runat="server" CssClass="dfinput1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            数据点名称
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listPointname" runat="server" CssClass="drpdwnlist" AutoPostBack="True"
                                                OnSelectedIndexChanged="listPointname_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            数据点ID
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtID" runat="server" CssClass="dfinput1" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            数据点类型
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listStyle" runat="server" CssClass="drpdwnlist">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>水份</asp:ListItem>
                                                <asp:ListItem>温度</asp:ListItem>
                                                <asp:ListItem>流量</asp:ListItem>
                                                <asp:ListItem>其它</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td width="100px">
                                            采集周期
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPeriodic" runat="server" CssClass="dfinput1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            监控变量标签：
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtVarTag" runat="server" CssClass="dfinput"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            断流监控标签：
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtCutoffTag" runat="server" CssClass="dfinput"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            备注：
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtDescript" runat="server" CssClass="dfinput"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table class="tablelist">
                                    <tr>
                                        <th colspan="6">
                                            数采条件表
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            批头批尾:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="TailList" runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            比较符：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="TailSignList" runat="server" CssClass="drpdwnlist" Width="50px">
                                                <asp:ListItem>&lt;=</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            目标值：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTailValue" runat="server" CssClass="tbinput"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            料头延时:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtHeadDelay" runat="server" CssClass="tbinput"></asp:TextBox>
                                            秒
                                        </td>
                                        <td>
                                            料尾延时：
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtTailDelay" runat="server" CssClass="tbinput"></asp:TextBox>
                                            秒
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            断流:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="LogicList" runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            比较符：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="SignList" runat="server" CssClass="drpdwnlist" Width="50px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            目标值：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDesValue" runat="server" CssClass="tbinput"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            断流前偏移
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBatchHeadDelay" runat="server" CssClass="tbinput"></asp:TextBox>秒
                                        </td>
                                        <td>
                                            断流后偏移
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBatchTailDelay" runat="server" CssClass="tbinput"></asp:TextBox>秒
                                        </td>
                                        <td>
                                            判定时长
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTimeseg" runat="server" CssClass="tbinput"></asp:TextBox>秒
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="6">
                                            &nbsp;
                                            <asp:Button ID="Delete" runat="server" Text="删除" OnClick="Delete_Click" CssClass="btndel auth" />
                                            &nbsp;
                                            <asp:Button ID="Save" runat="server" Text="保存" OnClick="Save_Click" CssClass="btnview auth">
                                            </asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td width="95%">
                                <div style="overflow: auto; height: 100px">
                                    <asp:GridView ID="GridView1" runat="server" class="grid">
                                        <HeaderStyle CssClass="gridheader" />
                                         <RowStyle CssClass="gridrow" /> <AlternatingRowStyle CssClass="gridalterrow" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnView" />
                    <asp:AsyncPostBackTrigger ControlID="Delete" />
                    <asp:AsyncPostBackTrigger ControlID="Save" />
                    <asp:AsyncPostBackTrigger ControlID="listSection" />
                    <asp:AsyncPostBackTrigger ControlID="listPointname" />
                </Triggers>
            </asp:UpdatePanel>
            <!--mainright end-->
        </div>
    </div>
    </form>
</body>
</html>
