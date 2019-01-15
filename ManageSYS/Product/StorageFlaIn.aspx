<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageFlaIn.aspx.cs" Inherits="Product_StorageFlaIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>香精香料退库管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
     <script type="text/javascript" src="../js/jquery.PrintArea.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">库存管理</a></li>
                <li><a href="#">香精香料退库管理</a></li>
            </ul>
        </div>
        <div class="formbody">
            <div id="usual1" class="usual">
                <div class="itab">
                    <ul>
                        <li><a href="#tab1" class="selected" id="tabtop1">退库管理</a></li>
                        <li><a href="#tab2" id="tabtop2">退库明细</a></li>
                    </ul>
                </div>
            </div>
            <div id="tab1" class="tabson">

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
                    香精香料退库表<span style="position: relative; float: right">

                        <asp:Button ID="btnGridNew" runat="server" Text="新增" class="btnadd  auth" OnClick="btnGridNew_Click" />
                        <asp:Button ID="btnCkAll1" runat="server" CssClass="btnset" Text="全选" OnClick="btnCkAll1_Click" />
                        <asp:Button ID="btnGridDel" runat="server" Text="删除" class="btndel auth" OnClick="btnGridDel_Click" OnClientClick="javascript:return confirm('确认删除？');" />
                    </span>
                </div>
                <div style="height: 400px" >
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="单据号" AutoGenerateColumns="False" HeaderStyle-Wrap="False" AlternatingRowStyle-Wrap="False" EditRowStyle-Wrap="False" RowStyle-Wrap="False"  OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="12" AllowPaging ="true">
                                <Columns>
                                    <asp:TemplateField  >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField   HeaderText="出入库类型">
                                        <ItemTemplate>
                                            <asp:Label ID="labStrg" runat="server" CssClass="labstatu" Width="50px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField   HeaderText="审批状态">
                                        <ItemTemplate>
                                            <asp:Label ID="labAudit" runat="server" CssClass="labstatu" Width="60px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField   HeaderText="出入库状态">
                                        <ItemTemplate>
                                            <asp:Label ID="labIssue" runat="server" CssClass="labstatu" Width="60px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:BoundField    DataField="单据号" HeaderText="单据号" />
                                     <asp:BoundField    DataField="产品" HeaderText="产品" />
                                      <asp:BoundField    DataField="投料批次" HeaderText="投料批次" />                                                     <asp:BoundField    DataField="领退日期" HeaderText="领退日期" />                                                      <asp:BoundField    DataField="香精香料总量" HeaderText="香精香料总量" />
                                     <asp:BoundField    DataField="料液总量" HeaderText="料液总量" />
                                     <asp:BoundField    DataField="编制人" HeaderText="编制人" />
                                     <asp:BoundField    DataField="收发人" HeaderText="收发人" />
                                    <asp:TemplateField  >
                                        <ItemTemplate>
                                            <asp:Button ID="btnSubmit" runat="server" Text="提交审批" CssClass="btn1 auth" Width="75"
                                                OnClick="btnSubmit_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  >
                                        <ItemTemplate>
                                            <asp:Button ID="btnGridIssue" runat="server" Text="审批进度" CssClass="btn1" Width="75"
                                                OnClick="btnGridIssue_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  >
                                        <ItemTemplate>
                                            <asp:Button ID="btnGridopt" runat="server" Text="出入库" CssClass="btn1 auth" Width="75"
                                                OnClick="btnGridopt_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  >
                                        <ItemTemplate>
                                            <asp:Button ID="btnGridview" runat="server" Text="查看" CssClass="btn1" Width="75"
                                                OnClick="btnGridview_Click" />
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
                            <asp:AsyncPostBackTrigger ControlID="btnModify" />                          
                            <asp:AsyncPostBackTrigger ControlID="btnGridDel" />
                            <asp:AsyncPostBackTrigger ControlID="btnCkAll1" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

            </div>
            <div id="tab2" class="tabson">
                  <div id ="report">
                <div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="listtitle">
                                香精香料领退管理<span style="position: relative; float: right" class="click2">
                                   
                                    <asp:Button ID="btnReset" runat="server" Text="重置" CssClass="btnset" OnClick="btnReset_Click" />
                                    <asp:Button ID="btnModify" class="btnmodify auth" runat="server" Text="保存" OnClick="btnModify_Click" />   <input id="btnPrint" type="button" value="打印" class="btnpatch" onclick="$('#report').printArea();" />
                                </span>
                            </div>

                            <table class="tablelist">
                                <tbody>
                                    <tr>
                                        <td width="100">单据编号
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCode" runat="server" class="dfinput1" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td width="100">退库日期
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPrdctdate" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                        </td>
                                        <td width="100">失效日期
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtValiddate" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                        </td>
                                        <td width="100">领用部门
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listApt" runat="server" CssClass="drpdwnlist"
                                                Enabled="False">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                         <td width="100">产品名称
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listPrdct" runat="server" CssClass="drpdwnlist" 
                                                OnSelectedIndexChanged="listPrdct_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="100">关联出库表单
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listStrgOut" runat="server" CssClass="drpdwnlist"
                                                OnSelectedIndexChanged="listStrgOut_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                              <asp:TextBox ID="txtStrgOut" runat="server" class="dfinput1" Visible ="false"></asp:TextBox>
                                        </td>
                                        <td width="100">关联计划号
                                        </td>
                                        <td>
                                           
                                              <asp:TextBox ID="txtPlanno" runat="server" class="dfinput1"  Enabled ="false"></asp:TextBox>
                                        </td>
                                        <td width="100">审批状态
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listStatus" runat="server" CssClass="drpdwnlist"
                                                Enabled="False">
                                            </asp:DropDownList>
                                        </td>
                                       
                                    </tr>
                                    <tr>
                                        <td width="100" class="btnhide">仓库
                                        </td>
                                        <td class="btnhide">
                                            <asp:DropDownList ID="listStorage" runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="100">烟梗总量
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtStemSum" runat="server" class="dfinput1" Enabled ="false"></asp:TextBox>
                                        </td>
                                        <td width="100">碎片总量
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtChipSum" runat="server" class="dfinput1" Enabled ="false"></asp:TextBox>
                                        </td>
                                        <td width="100">创建人
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listCreator" runat="server" CssClass="drpdwnlist"
                                                Enabled="False">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnGridNew" />
                            <asp:AsyncPostBackTrigger ControlID="GridView1" />
                            <asp:AsyncPostBackTrigger ControlID="listStrgOut" />
                            <asp:AsyncPostBackTrigger ControlID="btnReset" />
                            <asp:AsyncPostBackTrigger ControlID ="listPrdct" />
                             <asp:AsyncPostBackTrigger ControlID="btnGrid2Save" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="listtitle" style="margin-top: 10px">
                                明细<span style="position: relative; float: right">
                                   
                                   <asp:Button ID="btnGrid2Save" runat="server" Text="保存" CssClass="btnmodify auth"  OnClick="btnGrid2Save_Click" />
                                    <asp:Button ID="btnCkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnCkAll_Click" />
                                    <asp:Button ID="btnDelSel" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnDelSel_Click" OnClientClick="javascript:return confirm('确认删除？');" />
                                </span>
                            </div>
                          
                            <asp:GridView ID="GridView2" runat="server" class="grid" AllowPaging="True" AutoGenerateColumns="False"
                                DataKeyNames="ID">
                                <Columns>
                                    <asp:TemplateField  >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>                                  
                                    <asp:TemplateField   HeaderText="原料名称" SortExpression="物料名称">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="listGridName" runat="server" CssClass="drpdwnlist" OnSelectedIndexChanged="listGridName_SelectedIndexChanged" AutoPostBack="True" Width="230px"  DataSource='<%# gridHTYbind("0410")%>' DataValueField="material_code" DataTextField="material_name">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField   HeaderText="原料编码" SortExpression="物料编码">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGridcode" runat="server" DataValueField="原料编码" DataTextField="原料编码"
                                                CssClass="tbinput1" Enabled="False"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField   HeaderText="计量单位" SortExpression="计量单位">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGridUnit" runat="server" DataValueField="计量单位" DataTextField="计量单位"
                                                CssClass="tbinput"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField   HeaderText="退料量" SortExpression="退料量">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGridAmount" runat="server" DataValueField="领料量" DataTextField="领料量" onkeyup="value=value.replace(/[^\d\.]/g,'')"
                                                CssClass="tbinput"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField   HeaderText="仓库" SortExpression="仓库">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="listGridstrg" runat="server" CssClass="drpdwnlist">
                                                <asp:ListItem> </asp:ListItem>
                                                <asp:ListItem Value="1">库存商品库</asp:ListItem>
                                                <asp:ListItem Value="2">烟厂原料库</asp:ListItem>
                                                <asp:ListItem Value="3">鑫源原料库</asp:ListItem>
                                                <asp:ListItem Value="4">烟厂免费原料库</asp:ListItem>
                                                <asp:ListItem Value="5">鑫源免费原料库</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                </Columns>
                                <HeaderStyle CssClass="gridheader" />
                                <RowStyle CssClass="gridrow" />
                                <AlternatingRowStyle CssClass="gridalterrow" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnCkAll" />
                            <asp:AsyncPostBackTrigger ControlID="btnDelSel" />
                            <asp:AsyncPostBackTrigger ControlID="GridView2" />
                            <asp:AsyncPostBackTrigger ControlID="btnModify" />
                            <asp:AsyncPostBackTrigger ControlID="btnGrid2Save" />
                            <asp:AsyncPostBackTrigger ControlID="GridView1" />
                            <asp:AsyncPostBackTrigger ControlID ="btnGridNew" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                       </div>
            </div>


            <div class="aprvinfo" id="flowinfo">
                <div class="tiptop">
                    <span>审批流程详情</span><a onclick="$('#flowinfo').fadeOut(100)"></a>
                </div>
                <div class="flowinfo">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView3" runat="server" class="grid">
                                <HeaderStyle CssClass="gridheader" />
                                <RowStyle CssClass="gridrow" />
                                <AlternatingRowStyle CssClass="gridalterrow" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GridView1" />
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
