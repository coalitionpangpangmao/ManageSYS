<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExcelModel.aspx.cs" Inherits="SysConfig_ExcelModel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>报表模版管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>  
    <link rel="stylesheet" href="../js/jquery-treeview/jquery.treeview.css" />
    <link rel="stylesheet" href="../js/jquery-treeview/screen.css" />
    <script type="text/javascript" src="../js/jquery-treeview/jquery.cookie.js"></script>
    <script src="../js/jquery-treeview/jquery.treeview.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            initTreetoggle();
        });
        function initTreetoggle() {
            $("#browser").treeview({
                toggle: function () {
                    console.log("%s was toggled.", $(this).find(">span").text());
                },
                persist: "cookie",
                collapsed: true
            });
            $(".folder").click(function () {
                $('.folder').removeClass("selectedbold");
                $('.file').removeClass("selectedbold");
                $(this).addClass("selectedbold");
            });
            $(".file").click(function () {
                $('.folder').removeClass("selectedbold");
                $('.file').removeClass("selectedbold");
                $(this).addClass("selectedbold");
            });
        }
        function tab1Click(code) {
            $('#tabtop1').click();
            $("#hideTreeSel").attr('value', code);
            $("#btnUpdTab1").click()
        }
        function tab2Click(code) {
            $('#tabtop2').click();
            $("#hideTreeSel2").attr('value', code);
            $("#btnUpdTab2").click()
        }
    </script>
</head>
<body>
    <form id="Form1" runat="server">
    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="#">系统配置</a></li>
            <li><a href="#">报表模版管理</a></li>
        </ul>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="mainbox">
        <div class="mainleft">
            <div class="leftinfo" >
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="listtitle">
                            报表</div>
                        <% = tvHtml %>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="SaveBook" />
                        <asp:AsyncPostBackTrigger ControlID="Delete" />
                        <asp:AsyncPostBackTrigger ControlID="Save" />
                        <asp:AsyncPostBackTrigger ControlID="btnSegDel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <!--mainleft end-->
        <div class="mainright">
            <div id="usual1" class="usual">
                <div class="itab">
                    <ul>
                        <li><a href="#tab1" class="selected" id="tabtop1">报表模版</a></li>
                        <li><a href="#tab2" id="tabtop2">数据填充</a></li>
                    </ul>
                </div>
            </div>
            <div id="tab1" class="tabson">
                <div class="framelist">
                    <asp:HiddenField ID="parasel" runat="server" />
                    <asp:HiddenField ID="tempsel" runat="server" />
                    <asp:HiddenField ID="hideTreeSel" runat="server" />
                    <asp:Button ID="btnUpdTab1" runat="server" CssClass="btnhide" OnClick="btnUpdTab1_Click" />
                    <div class="tabson">
                        <iframe id="subFrame" name="subFrame" src="ImportExcel.aspx" height="35" scrolling="no"
                            width="600"></iframe>
                    </div>
                    <div class="tools">
                        <asp:Button ID="SaveBook" runat="server" Text="保存" OnClick="SaveBook_Click" CssClass="btnmodify auth" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Delete" runat="server" Text="删除" OnClick="Delete_Click" CssClass="btndel  auth"  OnClientClick="javascript:return confirm('确认删除？');"/>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table class="tablelist">
                                <tbody>
                                    <tr>
                                        <td height="30" width="100">
                                            报表名称
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="ReportName" runat="server" Width="400" CssClass="dfinput"></asp:TextBox>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td height="30" width="100">
                                            报表类型
                                        </td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="listType" runat="server" CssClass="drpdwnlist" Width="400px" >
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>系统</asp:ListItem>
                                                <asp:ListItem>质量</asp:ListItem>
                                                <asp:ListItem>生产</asp:ListItem>
                                                <asp:ListItem>库存</asp:ListItem>
                                                <asp:ListItem>工艺</asp:ListItem>
                                            </asp:DropDownList> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            参数集
                                        </td>
                                        <td>
                                            <asp:ListBox ID="Paralist" runat="server" Height="100px" Width="160px" OnSelectedIndexChanged="Paralist_SelectedIndexChanged"
                                                CssClass="dfinput" SelectionMode="Multiple"></asp:ListBox>
                                        </td>
                                        <td width="150">
                                            <asp:Button ID="Add" runat="server" Text="<<<" OnClick="Add_Click" CssClass="btn1 auth" />
                                            &nbsp;
                                            <asp:Button ID="Del" runat="server" Text=">>>" OnClick="Del_Click" CssClass="btn1 auth" />
                                        </td>
                                        <td>
                                            <asp:ListBox ID="Templist" runat="server" Height="100px" Width="160px" OnSelectedIndexChanged="Templist_SelectedIndexChanged"
                                                CssClass="dfinput" SelectionMode="Multiple"></asp:ListBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Add" />
                            <asp:AsyncPostBackTrigger ControlID="Del" />
                            <asp:AsyncPostBackTrigger ControlID="btnUpdTab1" />
                            <asp:AsyncPostBackTrigger ControlID="hideTreeSel" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div id="tab2" class="tabson">
                <asp:HiddenField ID="hideTreeSel2" runat="server" />
                <asp:Button ID="btnUpdTab2" runat="server" CssClass="btnhide" OnClick="btnUpdTab2_Click" />
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="framelist">
                            <table class="tablelist">
                                <tbody>
                                    <tr>
                                        <td height="30px" width="100px">
                                            报表名
                                        </td>
                                        <td colspan="3">
                                           <asp:DropDownList runat="server" ID ="listReport" CssClass ="drpdwnlist" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="30px" width="100px">
                                            工作表
                                        </td>
                                        <td>
                                            <asp:TextBox ID="Sheet1" runat="server" CssClass="dfinput" Width="150px"></asp:TextBox>
                                        </td>
                                        <td width="100px">
                                            工作表索引
                                        </td>
                                        <td>
                                            <asp:TextBox ID="Index" runat="server" CssClass="dfinput" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="30">
                                            目标行
                                        </td>
                                        <td>
                                            <asp:TextBox ID="DesR" runat="server" CssClass="dfinput" Width="150px"></asp:TextBox>
                                        </td>
                                        <td>
                                            目标列
                                        </td>
                                        <td>
                                            <asp:TextBox ID="DesC" runat="server" CssClass="dfinput" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            SQL语句
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="SQLText" runat="server" CssClass="dfinput" Width="450px" TextMode="MultiLine"
                                                Height="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            记录行数
                                        </td>
                                        <td>
                                            <asp:TextBox ID="Rows" runat="server" CssClass="dfinput" Width="150px"></asp:TextBox>
                                        </td>
                                        <td>
                                            记录列数
                                        </td>
                                        <td>
                                            <asp:TextBox ID="Columns" runat="server" CssClass="dfinput" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="tools">
                            <asp:Button ID="Save" runat="server" Text="保存" OnClick="Save_Click" CssClass="btnmodify  auth" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnSegDel" runat="server" Text="删除" CssClass="btndel  auth" OnClick="btnSegDel_Click"  OnClientClick="javascript:return confirm('确认删除？');"/>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnUpdTab2" />
                        <asp:AsyncPostBackTrigger ControlID="hideTreeSel2" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <script type="text/javascript">
                $("#usual1 ul").idTabs(); 
            </script>
            <script type="text/javascript">
                $('.tablelist tbody tr:odd').addClass('odd');
            </script>
        </div>
        <!--mainright end-->
    </div>
    </form>
</body>
</html>
