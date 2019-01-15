<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MonthTeamEva.aspx.cs" Inherits="Quality_MonthTeamEva" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>班组评估</title>
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
                <li><a href="#">质量评估与分析</a></li>
                 <li><a href="#">质量考核</a></li>          
                <li><a href="#">月度班组考评</a></li>
            </ul>
        </div>
        <div class="mainbox">
            <table class="tablelist" style="margin-bottom: 8px">
                <tr>
                    <td align="center">时间
                    <asp:TextBox ID="txtStartTime" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM'})"
                        CssClass="dfinput1"></asp:TextBox>
                     
                        <asp:Button ID="btnSearch" runat="server" CssClass="btnview" Text="查询" OnClick="btnSearch_Click" />
                      
                  
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridAll" runat="server" class="grid" AutoGenerateColumns ="False" RowStyle-HorizontalAlign="Left" RowStyle-BorderStyle="Solid" RowStyle-BorderWidth="1">
                                    <Columns>
                                        <asp:BoundField DataField="班组" HeaderText="班组" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="班组得分" HeaderText="班组得分" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="产品" HeaderText="产品" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="产量" HeaderText="产量" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="产量占比" HeaderText="产量占比" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="在线考核分" HeaderText="在线考核分" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="理化检测得分" HeaderText="理化检测得分"  >
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="感观评测得分" HeaderText="感观评测得分"  >
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="过程检测得分" HeaderText="过程检测得分"  >
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="产品得分" HeaderText="产品得分"  >
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>

                                    <HeaderStyle CssClass="gridheader" HorizontalAlign="Left" />
                                    <RowStyle CssClass="gridrow" HorizontalAlign="Left" />
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
         

        </div>
        
        <script type="text/javascript">
            $('.tablelist tbody tr:odd').addClass('odd');
        </script>
    </form>
</body>
</html>
