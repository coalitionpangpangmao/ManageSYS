<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="Product_Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>生产报表</title>
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
        function tab1Click(code) {
            $('#hideoldid').val($('#hidebookid').val());
            $('#hidebookid').val(code);
            $('#btnUpdate').click();

        }
    </script>
</head>
<body>
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <form id="Form1" runat="server">
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">生产管理</a></li>
                <li><a href="#">生产报表</a></li>
            </ul>
        </div>

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="mainbox">
            <div class="mainleft">
                <div class="leftinfo">
                    <div class="listtitle">报表</div>
                    <% = tvHtml %>
                </div>
            </div>
            <!--mainleft end-->
            <div class="mainright">
                <div class="listtitle">
                    查询条件
                      <span style="position: relative; float: right">

                          <asp:Button ID="btnSearch" runat="server" Text="查询" class="btnview" OnClick="btnSearch_Click" />
                          <asp:Button ID="btnExport" runat="server" Text="导出" class="btnset" OnClick="btnExport_Click" />
                          <asp:HiddenField ID="hidebookid" runat="server" />
                          <asp:HiddenField ID="hideoldid" runat="server" />
                          <asp:Button ID="btnUpdate" runat="server" class="btnhide" OnClick="btnUpdate_Click" />
                      </span>
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:HiddenField ID="hideMerge" runat="server" Value="0" />
                            <asp:HiddenField ID="hideParaset" runat="server" Value="1" />
                            <table class="tablelist">
                                <tr>
                                    <td>
                                        <asp:Label ID="lab2" runat="server" Text="开始时间" Width="70px" CssClass="labinfo" />
                                        <asp:TextBox ID="txtStartTime" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox></td>
                                    <td>
                                        <asp:Label ID="lab3" runat="server" Text="结束时间" Width="70px" CssClass="labinfo" />
                                        <asp:TextBox ID="txtEndTime" class="dfinput1" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox></td>
                                    <td>
                                        <asp:Label ID="lab1" runat="server" Text="产品" Width="50px" CssClass="labinfo" /><asp:DropDownList ID="listProd" runat="server" CssClass="drpdwnlist" AutoPostBack="true"></asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lab4" runat="server" Text="班组" Width="50px" CssClass="labinfo" /><asp:DropDownList ID="listTeam" runat="server" CssClass="drpdwnlist" AutoPostBack="true"></asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                            <asp:AsyncPostBackTrigger ControlID="listProd" />
                            <asp:AsyncPostBackTrigger ControlID="listTeam" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <iframe id="Frame1" name="Frame1" height="400"
                                style="width: 100%; position: relative;"></iframe>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSearch" />
                            <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <!--mainright end-->


    </form>
</body>
</html>
