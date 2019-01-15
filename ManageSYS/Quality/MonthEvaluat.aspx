<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MonthEvaluat.aspx.cs" Inherits="Quality_MonthEvaluat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>月度工艺总结</title>
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
                <li><a href="#">质量评估与分析</a></li>
                 <li><a href="#">质量考核</a></li>          
                <li><a href="#">月度工艺总结</a></li>
            </ul>
        </div>
        <div class="mainbox">
               
            <table class="tablelist" style="margin-bottom: 8px">
                <tr>
                    <td align="center" >月度
                    <asp:TextBox ID="txtStartTime" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM'})"
                        CssClass="dfinput1"></asp:TextBox>  <asp:Button ID="btnSearch" runat="server" CssClass="btnview" Text="查询" OnClick="btnSearch_Click" />                  
                     <asp:Button ID="btnExport" runat="server" CssClass="btnset" Text="导出" OnClick="btnExport_Click" />  </td>
                </tr>
                
               
            </table>
            <div id="usual1" class="usual">
                <div class="itab">
                    <ul>
                        <li><a href="#tab1" class="selected" id="tabtop1">月度工艺总结</a></li>
                        <li><a href="#tab2" id="tabtop2">工艺详情</a></li>
                     
                    </ul>
                </div>
            </div>
            <div id="tab1" class="tabson">

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                  <div class="listtitle" style="margin-top: 10px">
                                      <table><tr><td width ="100px">
                                     月度产量：</td><td><asp:Label ID="labout" runat="server" Text="0"></asp:Label></td><tdwidth="100px"></tdwidth="100px"><td width="100px"> 工艺总分：</td><td><asp:Label ID="labScore" runat="server" Text="0"></asp:Label></td>
                                   </tr> </table>
                                </div>                              
                                <asp:GridView ID="GridAll" runat="server" class="grid"  AutoGenerateColumns="False" DataKeyNames="prod_code">    
                                     <Columns>
                                         <asp:BoundField DataField="产品" HeaderText="产品" >
                                         <HeaderStyle HorizontalAlign="Left" />
                                         <ItemStyle HorizontalAlign="Left" />
                                         </asp:BoundField>
                                          <asp:BoundField DataField="产量" HeaderText="产量" >
                                         <HeaderStyle HorizontalAlign="Left" />
                                         <ItemStyle HorizontalAlign="Left" />
                                         </asp:BoundField>
                                          <asp:BoundField DataField="产量占比" HeaderText="产量占比" >
                                         <HeaderStyle HorizontalAlign="Left" />
                                         <ItemStyle HorizontalAlign="Left" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="在线考核分" HeaderText="在线考核分">
                                         <HeaderStyle HorizontalAlign="Left" />
                                         <ItemStyle HorizontalAlign="Left" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="理化检测得分" HeaderText="理化检测得分" >
                                         <HeaderStyle HorizontalAlign="Left" />
                                         <ItemStyle HorizontalAlign="Left" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="感观评测得分" HeaderText="感观评测得分">
                                         <HeaderStyle HorizontalAlign="Left" />
                                         <ItemStyle HorizontalAlign="Left" />
                                         </asp:BoundField>
                                          <asp:BoundField DataField="过程检测得分" HeaderText="过程检测得分" >
                                         <HeaderStyle HorizontalAlign="Left" />
                                         <ItemStyle HorizontalAlign="Left" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="产品得分" HeaderText="产品得分">
                                         <HeaderStyle HorizontalAlign="Left" />
                                         <ItemStyle HorizontalAlign="Left" />
                                         </asp:BoundField>
                                          <asp:TemplateField HeaderText="操作" HeaderStyle-Width="220px">
                                        <ItemTemplate>  
                                            <asp:Button ID="btngridDetail" runat="server" Text="查看详情" CssClass="btn1" OnClick="btngridDetail_Click" Width="100px" OnClientClick =" $('#tabtop2').parent().show(); $('#tabtop2').click();"/>
                                        </ItemTemplate>
                                              <HeaderStyle Width="220px" />
                                    </asp:TemplateField>
                                     </Columns>
                                     <HeaderStyle CssClass="gridheader" Wrap="False" />
                                    <RowStyle CssClass="gridrow" Wrap="False"/>
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
                         <div class="listtitle" >
                             在线考核
                             </div>
                        <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True"
                            CssClass="grid" AutoGenerateColumns ="true" OnRowCreated="GridView1_RowCreated">

                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                            <AlternatingRowStyle CssClass="gridalterrow" />
                        </asp:GridView>
                          <div class="listtitle" >
                             理化检测
                             </div>
                        <asp:GridView ID="GridView2" runat="server" ShowHeaderWhenEmpty="True"
                            CssClass="grid" AutoGenerateColumns ="true" OnRowCreated="GridView2_RowCreated">

                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                            <AlternatingRowStyle CssClass="gridalterrow" />
                        </asp:GridView>
                          <div class="listtitle" >
                             过程检测
                             </div>
                        <asp:GridView ID="GridView3" runat="server" ShowHeaderWhenEmpty="True"
                            CssClass="grid" AutoGenerateColumns ="true" OnRowCreated="GridView3_RowCreated">

                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                            <AlternatingRowStyle CssClass="gridalterrow" />
                        </asp:GridView>
                          <div class="listtitle" >
                             感观评测
                             </div>
                        <asp:GridView ID="GridView4" runat="server" ShowHeaderWhenEmpty="True"
                            CssClass="grid" AutoGenerateColumns ="true" OnRowCreated="GridView4_RowCreated">

                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                            <AlternatingRowStyle CssClass="gridalterrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridAll" />
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
