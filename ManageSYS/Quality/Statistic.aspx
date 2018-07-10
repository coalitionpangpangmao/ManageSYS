<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Statistic.aspx.cs" Inherits="Quality_Statistic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>质量统计数据</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/select.css" rel="stylesheet" type="text/css" />
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
            $("#hdPrcd").val(code);
            $("#btnPara").click();

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
            <li><a href="#">统计数据</a></li>
        </ul>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="mainbox">
        <div class="mainleft">
            <div class="leftinfo">
                <div class="listtitle">
                    查询条件
                    <asp:HiddenField ID="hdPrcd" runat="server" />
                    <asp:Button ID="btnPara" runat="server" CssClass="btnhide" OnClick="btnPara_Click" /></div>
                <div>
                    <table class="tablelist">
                        <tr>
                            <td>
                                开始时间
                            </td>
                            <td>
                                <asp:TextBox ID="txtBtime" runat="server" CssClass="tbinput1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                结束时间
                            </td>
                            <td>
                                <asp:TextBox ID="txtEtime" runat="server" CssClass="tbinput1"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="listtitle">
                    工艺模型</div>
                <% = tvHtml %>
            </div>
            <!--leftinfo end-->
        </div>
        <!--mainleft end-->
        <div class="mainright">
            <div class="framelist">
                <div class="listtitle">
                    统计数据</div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" class="grid" AllowPaging="True">
                                <HeaderStyle CssClass="gridheader" />
                                <RowStyle CssClass="gridrow" />
                                <AlternatingRowStyle CssClass="gridalterrow" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnPara" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <!--mainright end-->
        </div>
    </div>
    </form>
</body>
</html>
