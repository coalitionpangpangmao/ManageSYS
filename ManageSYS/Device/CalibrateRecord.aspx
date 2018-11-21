<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CalibrateRecord.aspx.cs" Inherits="Device_CalibrateRecord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>校准记录查询</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
  
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
    
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
                <li><a href="#">校准记录查询</a></li>
            </ul>
        </div>
        <div class="formbody">
            
                     
                    <div class="listtitle">
                        查询条件<span style="position: relative; float: right">
                            <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" OnClick="btnSearch_Click" />
                            <asp:HiddenField ID="hidecode" runat="server" />
                        </span>
                    </div>
                    <table class="tablelist">
                        <tbody>
                            <tr>
                                <td width="100">时间
                                </td>
                                <td>
                                    <asp:TextBox ID="txtStart" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>至
                                <asp:TextBox ID="txtStop" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RadioButton ID="rdAuto" runat="server" Text="自动校准" GroupName="clbrType" />
                                    <asp:RadioButton ID="rdManual" runat="server" Text="人工校准" GroupName="clbrType" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div class="listtitle" style="margin-top: 10px">
                        校准计划列表
                    </div>
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="PZ_CODE" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="12">
                                    <Columns>                                     
                                        <asp:BoundField    DataField="校准计划" HeaderText="校准计划" />
                                        <asp:BoundField    DataField="计划号" HeaderText="计划号" />
                                          <asp:BoundField    DataField="过期时间" HeaderText="过期时间" />
                                        <asp:BoundField    DataField="申请人" HeaderText="申请人" />
                                         <asp:BoundField    DataField="状态" HeaderText="状态" />
                                        
                                        <asp:BoundField    DataField="备注" HeaderText="备注" />

                                        <asp:TemplateField      ItemStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:Button ID="btnGridview" runat="server" Text="查看" CssClass="btnred" Width="75" OnClick="btnGridview_Click" />
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
                                <asp:AsyncPostBackTrigger ControlID="btnSearch" />                           
                                <asp:AsyncPostBackTrigger ControlID="GridView1" />
                               
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>             
           
           <div class="shade">
                <div  style="width:1000px; height:380px; position:absolute;top:6%; left:8%;background:#fcfdfd;box-shadow:1px 8px 10px 1px #9b9b9b;border-radius:1px;behavior:url(js/pie.htc); ">
                    <div class="tiphead">
                        <span>校准详情</span><a onclick="$('.shade').fadeOut(100);"></a>
                    </div>
                    <div class="gridinfo">                  
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                                                        
                            <asp:GridView ID="GridView2" runat="server" class="grid" AllowPaging="True" AutoGenerateColumns="False"  OnPageIndexChanging="GridView2_PageIndexChanging" PageSize="9"
                                DataKeyNames="ID">
                                <Columns>
                                    <asp:TemplateField     >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField      HeaderText="工段">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="listGridsct" runat="server" CssClass="drpdwnlist"   DataSource = "<%# sectionbind() %>" Width ="180px"  DataTextField = "Section_NAME"  DataValueField = "Section_CODE" Enabled="false" >
                                          </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField      HeaderText="设备名称">
                                    <ItemTemplate>
                                          <asp:DropDownList ID="listGridEq" runat="server" CssClass="drpdwnlist"  Enabled="false" Width ="180px"> </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                           <asp:TemplateField      HeaderText="数据点">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="listGridPoint" runat="server" CssClass="drpdwnlist" Enabled ="false" Width ="180px">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField      HeaderText="设定值">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGridOldvalue" runat="server" DataValueField="原值" DataTextField="原值"
                                                CssClass="tbinput" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField      HeaderText="测量值">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGridNewvalue" runat="server" DataValueField="校准值" DataTextField="校准值"
                                                CssClass="tbinput" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField      HeaderText="校准时间">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGridClbrtime" runat="server" DataValueField="校准时间" DataTextField="校准时间" CssClass="tbinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField      HeaderText="状态">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="listGrid2Status" runat="server" CssClass="drpdwnlist" Width="70px" Enabled="False"  DataSource = "<%# statusbind() %>"  DataTextField = "Name"  DataValueField = "ID"  >  </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField      HeaderText="备注">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGridremark" runat="server" DataValueField="备注" DataTextField="备注"
                                                CssClass="tbinput"></asp:TextBox>
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
                            <asp:AsyncPostBackTrigger ControlID="GridView1" />
                            <asp:AsyncPostBackTrigger ControlID="GridView2" />    
                                     
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
