<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShiftChange.aspx.cs" Inherits="Product_ShiftChange" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>班组交接班记录</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
 
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
      <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>    <script type ="text/javascript" src ="../js/jquery.PrintArea.js"></script>
    
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="#">生产管理</a></li>
            <li><a href="#">生产班组交接班</a></li>
        </ul>
    </div>
    <div class="formbody">
        <div id="usual1" class="usual">
            <div class="itab">
                <ul>
                    <li><a href="#tab1" class="selected" id="tabtop1">班组交接班列表</a></li>
                    <li><a href="#tab2" id="tabtop2">交接班详情</a></li>
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
                            <td width="100">
                                时间
                            </td>
                            <td>
                                <asp:TextBox ID="txtStartDate" runat="server" class="dfinput1"  onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>至
                                <asp:TextBox ID="txtStopDate" runat="server" class="dfinput1"  onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="listtitle" style="margin-top: 10px">
                    交接班列表
                </div>
                <div >
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdID" runat="server" />
                            <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="ID" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging"
                                AutoGenerateColumns="False" PageSize="12">
                                <Columns>
                                     <asp:BoundField    DataField="日期" HeaderText="日期" />
                                     <asp:BoundField    DataField="班组" HeaderText="班组" />
                                     <asp:BoundField    DataField="班时" HeaderText="班时" />
                                     <asp:BoundField    DataField="开始时间" HeaderText="开始时间" />
                                     <asp:BoundField    DataField="结束时间" HeaderText="结束时间" />
                                    <asp:TemplateField      HeaderText="操作">
                                        <ItemTemplate>
                                            <asp:Button ID="btnGrid1Edit" runat="server" Text="填写" CssClass="btn1 auth" OnClick="btnGrid1Edit_Click"   OnClientClick ="$('#tabtop2').click()"/>
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
                            <asp:AsyncPostBackTrigger ControlID ="btnSave" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div id="tab2" class="tabson">
            <div class="framelist">
                <div class="listtitle">
                    交接班详情<span style="position: relative; float: right">
                        <asp:Button ID="btnSave" runat="server" Text="保存" class="btnmodify auth" OnClick="btnSave_Click" />
                               <input id="btnPrint" type="button" value="打印" class ="btnpatch"  onclick ="$('#report').printArea();"/> <asp:Button ID="btnExport" runat="server" Text="导出" class="btnset" OnClick="btnExport_Click" />
                    </span>
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                              <div id="report" class="PrintArea" style="width: 100%; ">
                            <table class="tablelist">
                                <tbody>
                                    <tr>
                                        <td width="100">
                                            统计日期
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDate" runat="server" class="dfinput1" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td width="100">
                                            班时
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listShift" runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="100">
                                            班组
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listTeam" runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                  
                                        <td width="100">
                                            产品
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listProd" runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                          </tr>
                                    <tr>
                                        <td width="100">
                                            计划号
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listPlanno" runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                                           
                                        </td>
                                        <td>
                                            填写人
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEditor" runat="server" class="dfinput1" Enabled="false"></asp:TextBox>
                                        </td>                                 
                                     
                                        <td width="100">
                                            交班人
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listOlder" runat="server" CssClass ="drpdwnlist"></asp:DropDownList>
                                          
                                        </td>
                                        <td width="100">
                                            接班人
                                        </td>
                                        <td>
                                              <asp:DropDownList ID="listNewer" runat="server" CssClass ="drpdwnlist"></asp:DropDownList>
                                          
                                        </td>
                                    </tr>
                                    <tr>
                                       <td width="100">
                                            当班产量(箱)
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOutput" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                           <td width="100">
                                            零头重量(kg)
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOutPlus" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                        </tr>
                                    <tr>
                                        <td width="100" height="55">
                                            设备运行情况
                                        </td>
                                        <td colspan="7">
                                            <asp:TextBox ID="txtDevice" runat="server" class="dfinput1" Height="50px" TextMode="MultiLine"
                                                Width="500px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="btnhide">
                                        <td width="100" height="55">
                                            工艺质量情况
                                        </td>
                                        <td colspan="7">
                                            <asp:TextBox ID="txtQlt" runat="server" class="dfinput1" Height="50px" TextMode="MultiLine"
                                                Width="500px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100" height="55">
                                            现场情况
                                        </td>
                                        <td colspan="7">
                                            <asp:TextBox ID="txtScean" runat="server" class="dfinput1" Height="50px" TextMode="MultiLine"
                                                Width="500px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100" height="55px">
                                            备注
                                        </td>
                                        <td colspan="7">
                                            <asp:TextBox ID="txtRemark" runat="server" class="dfinput1" Height="50px" TextMode="MultiLine"
                                                Width="500px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th colspan="8" height="40px">
                                            班组交接明细列表<span style="position: relative; float: right">
                                                <asp:Button ID="btnAdd" runat="server" Text="增加" class="btn1 auth" OnClick="btnAdd_Click" />
                                                <asp:Button ID="btnckAll" runat="server" Text="全选" class="btn1 auth" OnClick="btnCkAll_Click" />
                                                <asp:Button ID="btnDelSel" runat="server" Text="删除" class="btn1 auth" OnClick="btnDelSel_Click" OnClientClick="javascript:return confirm('确认删除？');"/>
                                            </span>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            <asp:GridView ID="GridView2" runat="server" class="grid" AllowPaging="False" AutoGenerateColumns="False"  PageSize="12">
                                                <Columns>
                                                    <asp:TemplateField    HeaderText="选择" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField      HeaderText="物料名称" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="listMater" runat="server" CssClass="drpdwnlist"   DataSource ="<%#bindpara() %>" DataTextField="para_name" DataValueField="para_code" Width="200px">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField      HeaderText="数量" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="tbinput1"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField      HeaderText="单位" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtUnit" runat="server" CssClass="tbinput1"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField      HeaderText="备注" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDescpt" runat="server" CssClass="tbinput1"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField      HeaderText="操作" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnGrid2Save" runat="server" Text="保存" CssClass="btn1 auth" OnClick="btnGrid2Save_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="gridheader" />
                                                 <RowStyle CssClass="gridrow" /> 
                                                <AlternatingRowStyle CssClass="gridalterrow" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                                  </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            <asp:AsyncPostBackTrigger ControlID="GridView1" />
                            <asp:AsyncPostBackTrigger ControlID="Gridview2" />
                            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                            <asp:AsyncPostBackTrigger ControlID="btnckAll" />
                            <asp:AsyncPostBackTrigger ControlID="btnDelSel" />                           
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
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
