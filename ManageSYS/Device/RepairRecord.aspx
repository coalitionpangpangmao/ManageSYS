<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RepairRecord.aspx.cs" Inherits="Device_RepairRecord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>维修记录查询</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />   
    <script type="text/javascript" src="../js/jquery.js"></script>   
    
</head>
<body>
      <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>   
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="#">设备管理</a></li>
            <li><a href="#">维修记录查询</a></li>
        </ul>
    </div>
    <div class="formbody">
        
      
            <div class="framelist">
                <div class="listtitle">
                    查询条件<span style="position: relative; float: right">
                        <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" OnClick="btnSearch_Click" />
                    </span>
                </div>
                <table class="tablelist">
                    <tbody>
                        <tr>
                            <td width="100">
                                时间
                            </td>
                            <td>
                                <asp:TextBox ID="txtStart" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>至
                                <asp:TextBox ID="txtStop" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
 <asp:HiddenField ID="hideequip" runat="server" />
                               
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="listtitle" style="margin-top: 10px">
                    维修计划明细表
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                              <asp:HiddenField ID="hideMainid" runat="server" />
                                <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="ID" AutoGenerateColumns="False"  AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="12">
                                <Columns>
                                    <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="序号">
                                        <ItemTemplate>
                                            <%#(Container.DataItemIndex+1).ToString()%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="维保计划名" HeaderText="维保计划名" />
                                          <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="凭证号" HeaderText="凭证号" />                                        
                                         <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="区域" HeaderText="区域" />
                                     <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="设备名称" HeaderText="设备名称" />
                                     <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="维修原因" HeaderText="维修原因" />
                                     <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="维修内容" HeaderText="维修内容" />
                                     <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="期望完成时间" HeaderText="期望完成时间" />
                                      <asp:BoundField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="状态" HeaderText="状态" />
                                        <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="操作" ItemStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:Button ID="btnGrid1View" runat="server" Text="查看" CssClass="btnred" Width="75"
                                                    OnClick="btnGrid1View_Click" />
                                            </ItemTemplate>
                                            
                                        </asp:TemplateField>
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
                        <asp:AsyncPostBackTrigger ControlID = "btnSearch" />
                       
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
       
        <div class="shade">
                <div  style="width:1000px; height:380px; position:absolute;top:6%; left:5%;background:#fcfdfd;box-shadow:1px 8px 10px 1px #9b9b9b;border-radius:1px;behavior:url(js/pie.htc); ">
                    <div class="tiphead">
                        <span>维修详情</span><a onclick="$('.shade').fadeOut(100);"></a>
                    </div>
                    <div class="gridinfo">                  
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                        <table class="tablelist" style="margin-bottom: 10px">
                                <tbody>
                                    <tr>
                                         <td width="100">维保计划名：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPlanname" runat="server" class="dfinput1"  Enabled="false"></asp:TextBox>
                                             <td width="100">凭证号
                                        </td>
                                        <td>
                                             <asp:TextBox ID="txtPlanno" runat="server" class="dfinput1"  Enabled="false"></asp:TextBox>
                                        </td>
                                        <td width="100">单据编号
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCode" runat="server" class="dfinput1"  Enabled="false"></asp:TextBox>
                                        </td>
                                              </tr>
                                    <tr>
                                        <td width="100">区域
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listArea" runat="server" CssClass="drpdwnlist"  >
                                            </asp:DropDownList>
                                        </td>
                                       
                                
                                         <td width="100">维保设备
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listEq" runat="server" CssClass="drpdwnlist" >
                                            </asp:DropDownList>
                                        </td>
                                        <td width="100">操作员
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listOptor" runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                           </tr>
                                    <tr>
                                        <td width="100">操作时间
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOpttime" runat="server" class="dfinput1"  onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                        </td>
                                        <td>操作时长</td>
                                        <td>
                                            <asp:TextBox ID="txtSegcount" runat="server" class="dfinput1" ></asp:TextBox>
                                        </td>


                                    </tr>
                                    <tr>
                                        <td width="100" height="50px">操作记录
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtRecord" runat="server" class="dfinput1" Height="50px" TextMode="MultiLine"
                                                Width="800px"></asp:TextBox>
                                        </td>                                       

                                    </tr>
                                    <tr>
                                        <td width="100" height="50px">故障情况
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtFalut" runat="server" class="dfinput1" Height="50px" TextMode="MultiLine"
                                                Width="800px"></asp:TextBox>
                                        </td>                                       

                                    </tr>
                                    <tr>
                                        <td width="100" height="50px">反馈情况
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtFeedback" runat="server" class="dfinput1" Height="50px" TextMode="MultiLine"
                                                Width="800px"></asp:TextBox>
                                        </td>                                       

                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                        <asp:AsyncPostBackTrigger ControlID = "GridView1" />
                       
                        </Triggers>
                    </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
      
        <script type="text/javascript">
            $('.tablelist tbody tr:odd').addClass('odd');
        </script>
   
    </form>
</body>
</html>
