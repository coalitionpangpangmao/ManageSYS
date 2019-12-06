<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Craft_Event.aspx.cs" Inherits="Craft_Event" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>实时工艺事件</title>
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
                <li><a href="#">工艺管理</a></li>
                <li><a href="#">工艺事件管理</a></li>
                <li><a href="#">实时工艺事件</a></li>
            </ul>
        </div>

        <div class="mainbox">
            <div class="listtitle">
                查询条件<span style="position: relative; float: right">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" OnClick="btnSearch_Click" />
                </span>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="tablelist" style="margin-bottom: 5px">
                        <tbody>
                            <tr>
                                <td>时间  
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBtime" runat="server" CssClass="dfinput1"
                                        onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" OnTextChanged="txtBtime_TextChanged" AutoPostBack="true" Width="90px"></asp:TextBox>至
                                    <asp:TextBox ID="txtEtime" runat="server" CssClass="dfinput1"
                                        onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" OnTextChanged="txtEtime_TextChanged" AutoPostBack="true" Width="90px"></asp:TextBox>
                                </td>
                                <td>产品：
                                </td>
                                <td>
                                    <asp:DropDownList ID="listProd" runat="server" CssClass="drpdwnlist" Width="200px"></asp:DropDownList></td>
                                <td>工艺段：</td>
                                <td>
                                    <asp:DropDownList ID="listSection" runat="server" CssClass="drpdwnlist" OnSelectedIndexChanged="listSection_SelectedIndexChanged" AutoPostBack="true" Width="200px"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>批次：</td>
                                <td>
                                    <asp:DropDownList ID="listBatch" runat="server" CssClass="drpdwnlist" Width="200px"></asp:DropDownList>

                                </td>                             
                                <td>数据点：</td>
                                <td>
                                    <asp:DropDownList ID="listPoint" runat="server" CssClass="drpdwnlist" Width="200px"></asp:DropDownList>

                                </td>
                            </tr>
                        </tbody>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtBtime" />
                    <asp:AsyncPostBackTrigger ControlID="txtEtime" />
                    <asp:AsyncPostBackTrigger ControlID="listSection" />
                </Triggers>
            </asp:UpdatePanel>

            <div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" class="grid" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="15" HeaderStyle-Wrap="False" RowStyle-Wrap="False" AutoGenerateColumns="False" DataKeyNames="ID">
                            <Columns>
                                <asp:BoundField HeaderText="产品" DataField="产品" />
                                <asp:BoundField HeaderText="批次号" DataField="批次号" />
                                <asp:BoundField HeaderText="工艺点" DataField="工艺点" />
                                
                                <asp:BoundField HeaderText="开始时间" DataField="开始时间" />
                                <asp:BoundField HeaderText="结束时间" DataField="结束时间" />

                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:Button ID="btngrid1View" runat="server" Text="编辑" CssClass="btn1"  OnClick="btngrid1View_Click" OnClientClick="$('.shade').fadeIn(100);" />

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" BorderStyle="Solid" BorderWidth="1" />
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
                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>


        <div class="shade">
            <div class="info" style="width: 800px; height: 500px">
                <div class="tiphead">
                    <span>工艺事件处理</span><a onclick="$('.shade').fadeOut(100);"></a>
                </div>
                <div class="gridinfo">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table class="tablelist">
                                <tbody>
                                    <tr>
                                        <td width="100">工艺点
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPara" runat="server" class="dfinput1" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td width="100">批次号
                                        </td>
                                        <td>
                                            
                                            <asp:TextBox ID="txtPlanno" runat="server" class="dfinput1" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100">开始时间
                                        </td>
                                        <td>                                           
                                            <asp:TextBox ID="txtStarttime" runat="server" class="dfinput1" Enabled="false"></asp:TextBox>
                                        </td>

                                        <td width="100">结束时间
                                        </td>
                                        <td>                                          
                                            <asp:TextBox ID="txtEndtime" runat="server" class="dfinput1" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td>处理人
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEditor" runat="server" class="dfinput1" Enabled="false"></asp:TextBox>
                                        </td>

                                        <td width="100">处理时间
                                        </td>
                                        <td>                                           
                                              <asp:TextBox ID="txtEdittime" runat="server" class="dfinput1" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td width="100" height="100">原因分析
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtReason" runat="server" class="dfinput1" Height="150px" TextMode="MultiLine"
                                                Width="600px"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td width="100" height="55">改措措施
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtDescrpt" runat="server" class="dfinput1" Height="150px" TextMode="MultiLine"
                                                Width="600px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td colspan="4" height="55px" align="center">
                                            <asp:HiddenField ID="hideEventID" runat="server" />
                                           
                                            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btnmodify" OnClick="btnSave_Click" OnClientClick ="$('.shade').fadeOut(100);" />
                                        </td>
                                    </tr>


                                </tbody>
                            </table>

                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GridView1" />
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>


    </form>
</body>

</html>
