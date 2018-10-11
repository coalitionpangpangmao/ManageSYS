<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginRecord.aspx.cs" Inherits="Authority_LoginRecord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>日志管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript">
        $.ready(function () {
            debugger;
            $(".btn").hide();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">系统管理</a></li>
                <li><a href="#">日志管理</a></li>
            </ul>
        </div>
        <div class="rightinfo">
            <div>
                时间 
                <asp:TextBox ID="StartTime" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                至<asp:TextBox ID="EndTime" class="dfinput1" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                &nbsp; 
                 <asp:Button ID="btnSearch" runat="server" Height="25px" Width="72px"
                     Text="查询" CssClass="btn" OnClick="btnSearch_Click" />
                &nbsp;
                  <asp:Button ID="btnDelete" runat="server" Height="25px" Width="72px"
                      Text="删除" CssClass="btn auth" OnClick="btnDelete_Click" OnClientClick="javascript:return confirm('确认删除？');"/>
            </div>
            <div>
                <asp:UpdatePanel ID="updtpanel" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" Width="95%" CssClass="grid" border="0"
                            CellPadding="2" CellSpacing="1" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="14" HeaderStyle-Wrap="False" AutoGenerateColumns ="false">
                            <Columns>
                                <asp:BoundField DataField="用户" HeaderText="用户">
                                <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="操作站" HeaderText="操作站">
                                <ItemStyle Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="时间" HeaderText="时间">
                                <ItemStyle Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="描述" HeaderText="描述" >
                                      <ItemStyle Width="650px" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                            <AlternatingRowStyle CssClass="gridalterrow" />                        
                            <PagerStyle CssClass="gridpager" />
                            <PagerTemplate>
                                <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> ' Width="120px"></asp:Label>
                                <asp:LinkButton ID="lbnFirst" runat="Server" Text="首页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="First"></asp:LinkButton>
                                <asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="Prev"></asp:LinkButton>
                                <asp:LinkButton ID="lbnNext" runat="Server" Text="下一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Next"></asp:LinkButton>
                                <asp:LinkButton ID="lbnLast" runat="Server" Text="尾页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Last"></asp:LinkButton>
                                到第
                                <asp:TextBox ID="txtNewPageIndex" runat="server" Width="20px" Text='<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>' />
                                页  
             <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-2"
                 CommandName="Page" Text="跳转" />

                            </PagerTemplate>
                        </asp:GridView>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        <asp:AsyncPostBackTrigger ControlID="btnDelete" />
                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

    </form>
</body>
</html>
