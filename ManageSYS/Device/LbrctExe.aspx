<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LbrctExe.aspx.cs" Inherits="Device_LbrctExe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>维保计划处理</title>
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
            <li><a href="#">设备润滑管理</a></li>
            <li><a href="#">润滑执行</a></li>
        </ul>
    </div>
    <div class="formbody">
        
   
            <div class="framelist">
                <div class="listtitle">
                    查询条件<span style="position: relative; float: right">
                        <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview"  OnClick ="btnSearch_Click"/>
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
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:CheckBox ID="ckDone" runat="server" Text="己执行" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                  <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                   <div class="listtitle" style="margin-top: 10px">
                    润滑计划列表<span> 
                        <asp:HiddenField ID="hdcode" runat="server" /> </span>
                </div>
              
                            <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="PZ_CODE" AutoGenerateColumns="False">
                                <Columns>
                                      <asp:TemplateField      HeaderText="序号">
                                        <ItemTemplate>
                                            <%#(Container.DataItemIndex+1).ToString()%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField    DataField="润滑计划" HeaderText="润滑计划" />
                                    <asp:BoundField    DataField="部门" HeaderText="部门" />
                                    <asp:BoundField    DataField="审批状态" HeaderText="审批状态" />
                                    <asp:BoundField    DataField="执行状态" HeaderText="执行状态" />
                                    <asp:BoundField    DataField="备注" HeaderText="备注" />                                 
                                    <asp:TemplateField      ItemStyle-Width="80">
                                        <ItemTemplate>
                                            <asp:Button ID="btnGridview" runat="server" Text="查看" CssClass="btn1 auth" Width="75" OnClick = "btnGridview_Click" />
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
                 <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                <div class="listtitle" style="margin-top: 10px">
                    润滑计划明细表<span style="position: relative; float: right">
                         <asp:Button ID="btnGrid2Save" runat="server" Text="保存" CssClass="btnmodify" Width="75"
                                            OnClick="btnGrid2Save_Click" />
                        <asp:HiddenField ID ="txtCode" runat ="server" />
                            
                        </span>
                </div>
               
                            <asp:GridView ID="GridView2" runat="server" class="grid" DataKeyNames="ID" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField      HeaderText="序号">
                                        <ItemTemplate>
                                            <%#(Container.DataItemIndex+1).ToString()%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                <asp:TemplateField      HeaderText="工段">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="listGridsct" runat="server" CssClass="drpdwnlist" Width ="180px"  DataSource = "<%# sectionbind() %>"  DataTextField = "Section_NAME"  DataValueField = "Section_CODE" Enabled="False">
                                          </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField      HeaderText="设备名称">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="listGridEq" runat="server" CssClass="drpdwnlist" DataSource = "<%# eqbind() %>"  DataTextField = "EQ_NAME"  DataValueField = "IDKEY" Enabled="False" Width ="180px">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField      HeaderText="润滑部位">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGridpos" runat="server" DataValueField="润滑部位" DataTextField="润滑部位"
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField      HeaderText="润滑点数">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGridnum" runat="server" DataValueField="润滑点数" DataTextField="润滑点数" onKeyUp="value=value.replace(/\D/g,'')" 
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField      HeaderText="润滑油脂">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGridoil" runat="server" DataValueField="润滑油脂" DataTextField="润滑油脂"
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField      HeaderText="润滑周期(天）">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGriPric" runat="server" DataValueField="润滑周期" DataTextField="润滑周期" onKeyUp="value=value.replace(/\D/g,'')" 
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField      HeaderText="润滑方式">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGridStyle" runat="server" DataValueField="润滑方式" DataTextField="润滑方式"
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField      HeaderText="润滑量">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGridamount" runat="server" DataValueField="润滑量" DataTextField="润滑量" onkeyup="value=value.replace(/[^\d\.]/g,'')" 
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField      HeaderText="过期时间">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGridExptime" runat="server" DataValueField="过期时间" DataTextField="过期时间"    CssClass="tbinput1"  Width ="80px" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>                            
                                    <asp:TemplateField      HeaderText="状态">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="listGrid2Status" runat="server" CssClass="drpdwnlist" Width="70px" Enabled="False"  DataSource = "<%# statusbind() %>"  DataTextField = "Name"  DataValueField = "ID"  >  </asp:DropDownList>
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
                            <asp:AsyncPostBackTrigger ControlID="GridView1" />
                            <asp:AsyncPostBackTrigger ControlID="GridView2" />
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
    </div>
    </form>
</body>
</html>
