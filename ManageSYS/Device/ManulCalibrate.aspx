<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManulCalibrate.aspx.cs" Inherits="Device_ManulCalibrate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>校准计划执行</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/select.css" rel="stylesheet" type="text/css" />
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
                <li><a href="#">人工校准</a></li>
            </ul>
        </div>
        <div class="formbody">
            <div id="usual1" class="usual">
                <div class="itab">
                    <ul>
                        <li><a href="#tab1" class="selected" id="tabtop1">校准计划列表</a></li>
                        <li><a href="#tab2" id="tabtop2">人工校准记录</a></li>
                    </ul>
                </div>
            </div>
            <div id="tab1" class="tabson">
                <div class="framelist">
                    <div class="listtitle">
                        查询条件<span style="position: relative; float: right">
                            <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" OnClick="btnSearch_Click" />
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
                            </tr>
                        </tbody>
                    </table>
                    <div class="listtitle" style="margin-top: 10px">
                        校准计划列表
                    </div>
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                 
                                <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="PZ_CODE" AutoGenerateColumns="False">
                                    <Columns>                                     
                                        <asp:BoundField DataField="校准计划" HeaderText="校准计划" />
                                        <asp:BoundField DataField="计划号" HeaderText="计划号" />
                                          <asp:BoundField DataField="过期时间" HeaderText="过期时间" />
                                        <asp:BoundField DataField="申请人" HeaderText="申请人" />
                                         <asp:BoundField DataField="状态" HeaderText="状态" />
                                        <asp:BoundField DataField="备注" HeaderText="备注" />

                                        <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:Button ID="btnGridview" runat="server" Text="编辑" CssClass="btn1 auth" Width="75" OnClick="btnGridview_Click" />
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
                               
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div id="tab2" class="tabson">
                <div class="framelist">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="listtitle" style="margin-top: 10px">
                                校准记录<span style="position: relative; float: right">
                            <asp:Button ID="btnGrid2Save" runat="server" Text="保存" CssClass="btnview auth" 
                                                OnClick="btnGrid2Save_Click" />
                                    <asp:HiddenField ID ="txtCode" runat ="server" />
                        </span> 
                            </div>

                            <asp:GridView ID="GridView2" runat="server" class="grid" AllowPaging="True" AutoGenerateColumns="False"
                                DataKeyNames="ID">
                                <Columns>
                                    <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="工段">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="listGridsct" runat="server" CssClass="drpdwnlist"   DataSource = "<%# sectionbind() %>"  DataTextField = "Section_NAME"  DataValueField = "Section_CODE" Enabled="false" >
                                          </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="设备名称">
                                    <ItemTemplate>
                                          <asp:DropDownList ID="listGridEq" runat="server" CssClass="drpdwnlist"  Enabled="false"> </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                           <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="数据点">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="listGridPoint" runat="server" CssClass="drpdwnlist" Enabled ="false">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="原值">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGridOldvalue" runat="server" DataValueField="原值" DataTextField="原值"
                                                CssClass="tbinput"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="校准值">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGridNewvalue" runat="server" DataValueField="校准值" DataTextField="校准值"
                                                CssClass="tbinput"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="校准时间">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGridClbrtime" runat="server" DataValueField="校准时间" DataTextField="校准时间" CssClass="tbinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="状态">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="listGrid2Status" runat="server" CssClass="drpdwnlist" Width="70px" Enabled="False"  DataSource = "<%# statusbind() %>"  DataTextField = "Name"  DataValueField = "ID"  >  </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="备注">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGridremark" runat="server" DataValueField="备注" DataTextField="备注"
                                                CssClass="tbinput"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                </Columns>
                                <HeaderStyle CssClass="gridheader" />
                                <RowStyle CssClass="gridrow" />
                                <AlternatingRowStyle CssClass="gridalterrow" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>                          
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
