<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataInput.aspx.cs" Inherits="Product_DataInput" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>过程物料录入</title>
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
                <li><a href="#">生产管理</a></li>
                <li><a href="#">过程物料录入</a></li>
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
                            <td width="100">产品名称
                            </td>
                            <td>
                                <asp:DropDownList ID="listProd" runat="server" CssClass="drpdwnlist"></asp:DropDownList>
                            </td>
                            <td width="100">班组
                            </td>
                            <td>
                                <asp:DropDownList ID="listTeam" runat="server" CssClass="drpdwnlist"></asp:DropDownList>
                            </td>
                            <td width="100">记录时间
                            </td>
                            <td width="100">
                                <asp:TextBox ID="txtRecordtime" runat="server" CssClass="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="listtitle" style="margin-top: 10px">
                    物料记录 <span style="position: relative; float: right">
                        <asp:Button ID="btnAdd" CssClass="btnadd auth" runat="server" OnClick="btnAdd_Click"
                            Text="新增" />
                        <asp:Button ID="btnCkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnCkAll_Click" />
                        <asp:Button ID="btnDelSel" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnDelSel_Click" OnClientClick="javascript:return confirm('确认删除？');" />
                    </span>
                </div>
                <div id="usual1" class="usual">
                    <div class="itab">
                        <ul>
                            <li><a href="#tab1" id="tabtop1">过程记录</a></li>
                            <li><a href="#tab2" id="tabtop2">产品总记录</a></li>
                        </ul>
                    </div>
                </div>

                <div id="tab1" class="tabson">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" class="grid" AllowPaging="True" DataKeyNames="rowid"
                                AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="计划号" HeaderText="计划号" />
                                    <asp:BoundField DataField="产品" HeaderText="产品" />
                                    <asp:BoundField DataField="记录项目" HeaderText="记录项目" />
                                    <asp:BoundField DataField="记录值" HeaderText="记录值" />
                                    <asp:BoundField DataField="班组" HeaderText="班组" />
                                    <asp:BoundField DataField="记录时间" HeaderText="记录时间" />
                                    <asp:BoundField DataField="记录人" HeaderText="记录人" />
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
                            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                            <asp:AsyncPostBackTrigger ControlID="btnCkAll" />
                            <asp:AsyncPostBackTrigger ControlID="btnDelSel" />
                            <asp:AsyncPostBackTrigger ControlID="btnModify" />
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />

                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div id="tab2" class="tabson">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView3" runat="server" class="grid" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="GridView3_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="计划号" HeaderText="计划号" />
                                    <asp:BoundField DataField="产品" HeaderText="产品" />
                                    <asp:BoundField DataField="记录项目" HeaderText="记录项目" />
                                    <asp:BoundField DataField="记录值" HeaderText="记录值" />

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
                            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                            <asp:AsyncPostBackTrigger ControlID="btnCkAll" />
                            <asp:AsyncPostBackTrigger ControlID="btnDelSel" />
                            <asp:AsyncPostBackTrigger ControlID="btnModify" />
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="shade">
                    <div class="recordinfo">
                        <div class="tiphead">
                            <span>物料记录</span><a onclick="$('.shade').fadeOut(100);"></a>
                        </div>
                        <div class="gridinfo">

                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <table class="tablelist">
                                        <tbody>
                                            <tr>
                                                <td width="100">产品名称
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="listProd2" runat="server" CssClass="drpdwnlist" AutoPostBack="true" OnSelectedIndexChanged="ListProd2_SelectedIndexChanged"></asp:DropDownList>
                                                </td>
                                                <td width="100">计划号
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="listPlanno" runat="server" CssClass="drpdwnlist"></asp:DropDownList>
                                                </td>
                                                <td width="100">班组
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="listTeam2" runat="server" CssClass="drpdwnlist"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100">记录类型
                                 
                                                </td>
                                                <td width="100">
                                                    <asp:RadioButton ID="rd1" runat="server" Text="过程记录" GroupName="RecordStyle" onclick="$('#listTeam2').attr('disabled',false);$('#listTeam2').removeClass('disableinput');$('#txtDate').removeClass('disableinput');$('#txtDate').attr('disabled',false);" OnCheckedChanged ="rd1_CheckedChanged"/>
                                                    <asp:RadioButton ID="rd2" runat="server" Text="产品总记录" GroupName="RecordStyle"  onclick="$('#listTeam2').attr('disabled',true);$('#listTeam2').addClass('disableinput');$('#txtDate').addClass('disableinput');$('#txtDate').attr('disabled',true);"  OnCheckedChanged ="rd2_CheckedChanged"/>
                                                </td>
                                                <td width="100">日期
                                                </td>
                                                <td width="100">
                                                    <asp:TextBox ID="txtDate" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" AutoPostBack="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <div class="listtitle" style="margin-top: 10px">
                                        录入记录 <span style="position: relative; float: right">
                                            <asp:HiddenField ID="hdScrollY" runat="server" />
                                            <asp:Button ID="btnView" class="btnview" runat="server" Text="查看记录" OnClick="btnView_Click" Width="150px" />
                                            <asp:Button ID="btnModify" class="sure" runat="server" Text="保存" OnClick="btnModify_Click" />
                                        </span>
                                    </div>
                                    <asp:GridView ID="GridView2" runat="server" class="grid" DataKeyNames="ID"
                                        AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="参数点">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="listPara" runat="server" CssClass="drpdwnlist" DataSource="<%#bindpara() %>" DataTextField="para_name" DataValueField="para_code" Width="200px"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="参数值">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtParavalue" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <HeaderStyle CssClass="gridheader" />
                                        <RowStyle CssClass="gridrow" />
                                        <AlternatingRowStyle CssClass="gridalterrow" />
                                    </asp:GridView>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                                    <asp:AsyncPostBackTrigger ControlID="btnView" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>

        <script type="text/javascript">
            $('.tablelist tbody tr:odd').addClass('odd');
            $("#usual1 ul").idTabs();
        </script>
    </form>
</body>
</html>
