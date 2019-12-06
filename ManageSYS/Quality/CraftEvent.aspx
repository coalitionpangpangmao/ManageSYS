<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CraftEvent.aspx.cs" Inherits="Quality_CraftEvent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>工艺事件管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
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
                <li><a href="#">工艺事件管理</a></li>
            </ul>
        </div>

        <div class="mainbox">
            <table class="tablelist" style="margin-bottom: 5px">
                <tbody>
                    <tr>
                        <td colspan="7" align="center">时间     
                    <asp:TextBox ID="txtBtime" runat="server" CssClass="dfinput1"
                        onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                            至
            <asp:TextBox ID="txtEtime" runat="server" CssClass="dfinput1"
                onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;
                            状态：
                        <asp:DropDownList ID="listStatus" runat="server" CssClass="drpdwnlist" Width ="80px"></asp:DropDownList>
                            <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" OnClick="btnSearch_Click" />
                        </td>

                    </tr>
                </tbody>
            </table>
            <div id="usual1" class="usual">
                <div class="itab">
                    <ul>
                        <li><a href="#tab1" class="selected" id="tabtop1">在线工艺事件</a></li>
                        <li><a href="#tab2" id="tabtop2">离线工艺事件</a></li>
                    </ul>
                </div>
            </div>
            <div id="tab1" class="tabson" style="margin-top: 0px; padding-top: 0px;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="listtitle">
                            <span style="position: relative; float: right">
                                <asp:Button ID="btnSelAll1" runat="server" Text="全选" CssClass="btnview auth" OnClick="btnSelAll1_Click" />
                                <asp:Button ID="btnIgnore1" runat="server" Text="忽略" CssClass="btndel auth" OnClick="btnIgnore1_Click" />
                                <asp:Button ID="btnConfirm1" runat="server" Text="确认" CssClass="btnpatch auth" OnClick="btnConfirm1_Click"  />
                                   <asp:Button ID="btnFeed1" runat="server" Text="反馈" CssClass="btnmodify auth" OnClick="btnFeed1_Click"  Visible ="false"/>
                                <asp:Button ID="btnDone1" runat="server" Text="完成" CssClass="btndone auth" OnClick="btnDone1_Click" Visible ="false"/>
                            </span>
                        </div>


                        <asp:GridView ID="GridView1" runat="server" class="grid" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="14" AutoGenerateColumns="False"
                            DataKeyNames="id,type,prod_code,para_code">
                            <Columns>
                                <asp:TemplateField     >
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ck" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:BoundField    HeaderText="产品" DataField="prod_name" />
                                 <asp:BoundField    HeaderText="工艺点" DataField="para_name" />
                                 <asp:BoundField    HeaderText="类型" DataField="typename" />
                                 <asp:BoundField    HeaderText="值" DataField="value" />
                                 <asp:BoundField    HeaderText="范围" DataField="range" />
                                 <asp:BoundField    HeaderText="开始时间" DataField="b_time" />
                                 <asp:BoundField    HeaderText="结束时间" DataField="e_time" />
                                 <asp:BoundField    HeaderText="班组" DataField="team_name" />
                                <asp:TemplateField      HeaderText="状态">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="labStatus" CssClass="labstatuGreen" Width="60px"> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:BoundField    DataField="minus_score" HeaderText="扣分" />
                                <asp:TemplateField      HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:Button ID="btngrid1Ignore" runat="server" Text="忽略" CssClass="btn1" OnClick="btngrid1Ignore_Click" />
                                        <asp:Button ID="btngrid1Sure" runat="server" Text="确认" CssClass="btn1" OnClick="btngrid1Sure_Click" />
                                        <asp:Button ID="btngrid1fdback" runat="server" Text="反馈" CssClass="btn1" OnClick="btngrid1fdback_Click" />
                                        <asp:Button ID="btngrid1done" runat="server" Text="完成" CssClass="btn1" OnClick="btngrid1done_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField      HeaderText="查看">
                                    <ItemTemplate>
                                        <asp:Button ID="btngrid1View" runat="server" Text="查看" CssClass="btn1" OnClick="btngrid1View_Click" />
                                       
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
                        <asp:AsyncPostBackTrigger ControlID="btnConfirm1" />
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        <asp:AsyncPostBackTrigger ControlID="btnIgnore1" />
                        <asp:AsyncPostBackTrigger ControlID="btnSelAll1" />
                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
                        <asp:AsyncPostBackTrigger ControlID ="btnFeed1" />
                        <asp:AsyncPostBackTrigger ControlID ="btnDone1" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
            <div id="tab2" class="tabson" style="margin-top: 0px; padding-top: 0px;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="listtitle">
                            <span style="position: relative; float: right">
                                <asp:Button ID="btnSelAll" runat="server" Text="全选" CssClass="btnview auth" OnClick="btnSelAll_Click" />
                                <asp:Button ID="btnIgnore" runat="server" Text="忽略" CssClass="btndel auth" OnClick="btnIgnore_Click" />
                                <asp:Button ID="btnConfirm" runat="server" Text="确认" CssClass="btnpatch auth" OnClick="btnConfirm_Click" />
                                 <asp:Button ID="btnFeed" runat="server" Text="反馈" CssClass="btnmodify auth" OnClick="btnFeed_Click"  Visible ="false"/>
                                <asp:Button ID="btnDone" runat="server" Text="完成" CssClass="btndone auth" OnClick="btnDone_Click"  Visible ="false"/>
                            </span>
                        </div>


                        <asp:GridView ID="GridView2" runat="server" class="grid" AllowPaging="true" OnPageIndexChanging="GridView2_PageIndexChanging" PageSize="14" AutoGenerateColumns="False"
                            DataKeyNames="ID,inspect_code">
                            <Columns>
                                <asp:TemplateField     >
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ck" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:BoundField    DataField="RECORD_TIME" HeaderText="检测时间" />
                                   <asp:BoundField    HeaderText="产品" DataField="prod_name" />
                                   <asp:BoundField    HeaderText="班组" DataField="team_name" />
                                 <asp:BoundField    HeaderText="检验类型" DataField="inspect_type" />
                                 <asp:BoundField    HeaderText="分组" DataField="insgroup" />
                                 <asp:BoundField    HeaderText="检验项目" DataField="inspect_name" />
                                 <asp:BoundField    HeaderText="检测值" DataField="value" />
                                 <asp:BoundField    HeaderText="标准范围" DataField="range" />
                                 <asp:BoundField    DataField="unit" HeaderText="单位" />

                                <asp:TemplateField      HeaderText="状态">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="labStatus" CssClass="labstatuGreen" Width="60px"> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:BoundField    DataField="minus_score" HeaderText="扣分" />
                                <asp:TemplateField      HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:Button ID="btngrid2Ignore" runat="server" Text="忽略" CssClass="btn1" OnClick="btngrid2Ignore_Click" />
                                        <asp:Button ID="btngrid2Sure" runat="server" Text="确认" CssClass="btn1" OnClick="btngrid2Sure_Click" />
                                        <asp:Button ID="btngrid2fdback" runat="server" Text="反馈" CssClass="btn1" OnClick="btngrid2fdback_Click" />
                                        <asp:Button ID="btngrid2done" runat="server" Text="完成" CssClass="btn1" OnClick="btngrid2done_Click" />
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
                        <asp:AsyncPostBackTrigger ControlID="btnConfirm" />
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        <asp:AsyncPostBackTrigger ControlID="btnIgnore" />
                        <asp:AsyncPostBackTrigger ControlID="btnSelAll" />
                        <asp:AsyncPostBackTrigger ControlID="GridView2" />
                          <asp:AsyncPostBackTrigger ControlID ="btnFeed" />
                        <asp:AsyncPostBackTrigger ControlID ="btnDone" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>


            <div class="shade">
                <div class="info" style="width: 700px;height:450px">
                    <div class="tiphead">
                        <span>数据点详情</span><a onclick="$('.shade').fadeOut(100);"></a>
                    </div>
                    <div class="gridinfo">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div  >
                                     <asp:Label ID="lbPoint" runat="server" style=" display: inline"></asp:Label>
                                                 <asp:Label ID="lbpara_code" runat="server" class="btnhide" ></asp:Label>
                                                 <asp:Label ID="lbprod_code" runat="server" class="btnhide"></asp:Label>
                                                <asp:Label ID="lbsTime" runat="server"  style=" display: inline"></asp:Label>~<asp:Label ID="lbeTime" runat="server" style=" display: inline"></asp:Label>
                                                <asp:RadioButton ID="rdShowAll" runat="server" GroupName="showAll" Text="全部显示" /> 
                                                 <asp:RadioButton ID="rdUnQualified" runat="server" GroupName="showAll" Text="不合格点" /> 
                                                <asp:Button ID="btnPointView" runat="server" Text="查看" class="btnview" OnClick ="btnPointView_Click"/>
                                                 <asp:Button ID="btnExport" runat="server" Text="导出" class="btnset" OnClick="btnExport_Click"/>
                                </div>
                                <div  style="height: 350px; overflow: scroll">
                                   <asp:GridView ID="GridView3" runat="server" class="grid"  >   
                                                    <HeaderStyle CssClass="gridheader" />
                                                    <RowStyle CssClass="gridrow" />
                                                    <AlternatingRowStyle CssClass="gridalterrow" />
                                                </asp:GridView>
                                </div>                                
                                
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="GridView1" />
                               <asp:AsyncPostBackTrigger ControlID ="btnPointView" />
                                <asp:AsyncPostBackTrigger ControlID ="btnExport" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
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
