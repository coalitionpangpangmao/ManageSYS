<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Evaluat_Phychem.aspx.cs" Inherits="Quality_Evaluat_PhyChem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>理化检测报告</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/report.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
    <script type="text/javascript" src="../js/jquery.PrintArea.js"></script>
    <script type="text/javascript" src="../js/msys/export.js"></script>
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tabtop2').parent().hide();
            $('#tabtop3').parent().hide();
            $('#btnPrint').hide();
            $('#btnExport').hide();
        });

    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">质量评估与分析</a></li>
                <li><a href="#">质量考核</a></li>
                <li><a href="#">理化检测报告</a></li>
            </ul>
        </div>

        <div class="mainbox">
            <table class="tablelist" style="margin-bottom: 5px">
                <tbody>
                    <tr>
                        
                        <td colspan="7" align="center">请选择月度：     
                    <asp:TextBox ID="txtBtime" runat="server" CssClass="dfinput1"
                        onclick="WdatePicker({dateFmt:'yyyy-MM'})"></asp:TextBox>
                          &nbsp;&nbsp;产品：
                            <asp:DropDownList ID="listProd" runat="server" CssClass="drpdwnlist"></asp:DropDownList>
                            <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" OnClick="btnSearch_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <input id="btnPrint" type="button" value="打印" class="btnpatch" onclick="$('#report').printArea();" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              <input id="btnExport" type="button" value="导出" class="btnset" onclick="MSYS_Export('Msysexport');" />
                        </td>

                    </tr>
                </tbody>
            </table>
            <div id="usual1" class="usual">
                <div class="itab">
                    <ul>
                        <li><a href="#tab1" class="selected" id="tabtop1">产品理化得分</a></li>
                        <li><a href="#tab2" id="tabtop2">产品检测报告</a><span onclick="$('#tabtop1').click();$('#tabtop2').parent().hide(); $('#btnPrint').hide(); $('#btnExport').hide();"></span></li>
                        <li><a href="#tab3" id="tabtop3">检测得分详情</a><span onclick="$('#tabtop1').click();$('#tabtop3').parent().hide(); "></span></li>
                    </ul>
                </div>
            </div>
            <div id="tab1" class="tabson" style="margin-top: 0px; padding-top: 0px;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="width: 100%; height: 100%; overflow-x: scroll">
                            <asp:GridView ID="GridView1" runat="server" class="grid" AutoGenerateColumns="True" OnRowCreated="GridView1_RowCreated" HeaderStyle-Wrap="False" RowStyle-Wrap="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="220px">
                                        <ItemTemplate>
                                            <asp:Button ID="btngridview" runat="server" Text="检测报告" CssClass="btn1" OnClick="btngridview_Click"  Width="100px"  OnClientClick=" $('#tabtop2').parent().show(); $('#tabtop2').click();$('#btnPrint').show();$('#btnExport').show();"/>
                                            <asp:Button ID="btngridDetail" runat="server" Text="查看详情" CssClass="btn1" OnClick="btngridDetail_Click" Width="100px" OnClientClick =" $('#tabtop3').parent().show(); $('#tabtop3').click();"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridheader" />
                                <RowStyle CssClass="gridrow" />
                                <AlternatingRowStyle CssClass="gridalterrow" />
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
            <div id="tab2" class="tabson" style="margin-top: 0px; padding-top: 0px;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <!--startprint-->
                        <div id="report" class="PrintArea" style="width: 90%; margin-left: 50px">
                            <% = htmltable %>
                        </div>
                        <!--endprint-->
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div id="tab3" class="tabson" style="margin-top: 0px; padding-top: 0px;">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:HiddenField ID="hideprod" runat="server" />
                        <div style="width: 100%; height: 100%; overflow-x: scroll">
                            <asp:GridView ID="GridView2" runat="server" class="grid" AllowPaging="false"  HeaderStyle-Wrap="False" RowStyle-Wrap="False" OnRowCreated="GridView2_RowCreated">
                                <HeaderStyle CssClass="gridheader" />
                                <RowStyle CssClass="gridrow" />
                                <AlternatingRowStyle CssClass="gridalterrow" />                              
                            </asp:GridView>
                        </div>
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
