<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Evaluat_online.aspx.cs" Inherits="Quality_Evaluat_online" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>在线评测报告</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/report.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
    <script type ="text/javascript" src ="../js/jquery.PrintArea.js"></script>
    
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tabtop2').parent().hide();          
            $('#btnPrint').hide();
        });


    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">质量评估</a></li>
                <li><a href="#">在线评测报告</a></li>
            </ul>
        </div>

        <div class="mainbox">
            <table class="tablelist" style="margin-bottom: 5px">
                <tbody>
                    <tr>
                        <td colspan="7" align="center">请选择月度：     
                    <asp:TextBox ID="txtBtime" runat="server" CssClass="dfinput1"
                        onclick="WdatePicker({dateFmt:'yyyy-MM'})"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp; 
                            <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" OnClick="btnSearch_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <input id="btnPrint" type="button" value="打印" class ="btnpatch"  onclick ="$('#report').printArea();"/>
                           
                        </td>

                    </tr>
                </tbody>
            </table>
            <div id="usual1" class="usual">
                <div class="itab">
                    <ul>
                        <li><a href="#tab1" class="selected" id="tabtop1">在线评测得分</a></li>
                        <li><a href="#tab2" id="tabtop2">在线评测报告</a><span onclick="$('#tabtop1').click();$('#tabtop2').parent().hide();"></span></li>
                       
                    </ul>
                </div>
            </div>
            <div id="tab1" class="tabson" style="margin-top: 0px; padding-top: 0px;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <asp:GridView ID="GridView1" runat="server" class="grid" AutoGenerateColumns="True">
                            <Columns>
                                <asp:TemplateField      HeaderText="操作"  HeaderStyle-Width="220px">
                                    <ItemTemplate>
                                        <asp:Button ID="btngridview" runat="server" Text="检测报告" CssClass="btn1" OnClick="btngridview_Click" Width="100px" />
                                      
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
                    </Triggers>
                </asp:UpdatePanel>

            </div>
            <div id="tab2" class="tabson" style="margin-top: 0px; padding-top: 0px;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <!--startprint-->
                        <div id="report" class="PrintArea" style="width: 90%; margin-left:50px">
                            <% = htmltable %>
                        </div>
                        <!--endprint-->
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