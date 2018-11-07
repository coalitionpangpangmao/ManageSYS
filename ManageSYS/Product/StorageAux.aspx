<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageAux.aspx.cs" Inherits="Product_StorageAux" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>辅料库管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
       <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>   
    <script type="text/javascript">
    
        function GridClick() {
            $('#tabtop2').click();

        }
        function Aprvlist() {
            $("#flowinfo").fadeIn(200);
        };

        function Aprvlisthide() {
            $("#flowinfo").fadeOut(100);
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="#">库存管理</a></li>
            <li><a href="#">辅料库管理</a></li>
        </ul>
    </div>
    <div class="formbody">
        <div id="usual1" class="usual">
            <div class="itab">
                <ul>
                    <li><a href="#tab1" class="selected" id="tabtop1">出入库管理</a></li>
                    <li><a href="#tab2" id="tabtop2">辅料领退明细</a></li>
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
                                <asp:TextBox ID="txtStart" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>至
                                <asp:TextBox ID="txtStop" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdOut1" runat="server" Text="领用" GroupName="Storage1" Checked="True" />
                                <asp:RadioButton ID="rdIn1" runat="server" Text="退库" GroupName="Storage1" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="listtitle" style="margin-top: 10px">
                    辅料领退表<span style="position: relative; float: right">
                      
                        <asp:Button ID="btnGridNew" runat="server" Text="新建" class="btnadd  auth" OnClick="btnGridNew_Click" />
                          <asp:Button ID="btnCkAll1" runat="server" CssClass="btnset" Text="全选" OnClick="btnCkAll1_Click" />
                          <asp:Button ID="btnGridDel" runat="server" Text="删除" class="btndel auth" OnClick="btnGridDel_Click"  OnClientClick="javascript:return confirm('确认删除？');"/>
                    </span>
                </div>
                <div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="单据号"
                             AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="出入库类型">
                                        <ItemTemplate>
                                            <asp:Label ID="labStrg" runat="server" CssClass="labstatu" Width="45px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="审批状态">
                                        <ItemTemplate>
                                            <asp:Label ID="labAudit" runat="server" CssClass="labstatu" Width="55px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="下发状态">
                                        <ItemTemplate>
                                            <asp:Label ID="labIssue" runat="server" CssClass="labstatu" Width="55px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:BoundField DataField="领退日期" HeaderText="领退日期" />
                                    <asp:BoundField DataField="单据号" HeaderText="单据号" />
                                    <asp:BoundField DataField="出入库类型" HeaderText="出入库类型" /> 
                                                       
                                    <asp:BoundField DataField="编制人" HeaderText="编制人" />
                                    <asp:BoundField DataField="收发人" HeaderText="收发人" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnSubmit" runat="server" Text="提交审批" CssClass="btn1 auth" Width="75"
                                                OnClick="btnSubmit_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnGridIssue" runat="server" Text="审批进度" CssClass="btn1" Width="75"
                                                OnClick="btnGridIssue_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnGridopt" runat="server" Text="出入库" CssClass="btn1 auth" Width="75"
                                                OnClick="btnGridopt_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField >
                                        <ItemTemplate>
                                            <asp:Button ID="btnGridview" runat="server" Text="查看" CssClass="btn1" Width="75"
                                                OnClick="btnGridview_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridheader" />
                             <RowStyle CssClass="gridrow" /> <AlternatingRowStyle CssClass="gridalterrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        <asp:AsyncPostBackTrigger ControlID="btnModify" />
                        <asp:AsyncPostBackTrigger ControlID = "btnGridDel" />
                        <asp:AsyncPostBackTrigger ControlID ="btnCkAll1" />
                    </Triggers>
                </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div id="tab2" class="tabson">
            <div class="framelist">
                <div class="listtitle">
                    辅料领退管理<span style="position: relative; float: right" class="click2">
                        <asp:RadioButton ID="rdOut" runat="server" Text="领用" GroupName="Storage" Checked="True" />
                        <asp:RadioButton ID="rdIn" runat="server" Text="退库" GroupName="Storage" />
                           <asp:Button ID="btnReset" runat="server" Text="重置" CssClass= "btnset"
                                            OnClick="btnReset_Click" />
                         <asp:Button ID="btnModify" class="btnmodify auth" runat="server" Text="保存" OnClick="btnModify_Click" />
                       
                    </span>
                </div>
                <div>
                  <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                <table class="tablelist">
                    <tbody>
                        <tr>
                            <td width="100">
                                单据编号
                            </td>
                            <td>
                                <asp:TextBox ID="txtCode" runat="server" class="dfinput1" Enabled="false"></asp:TextBox>
                            </td>
                            <td width="100">
                                生产日期
                            </td>
                            <td>
                                <asp:TextBox ID="txtPrdctdate" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                            </td>
                            <td width="100">
                                失效日期
                            </td>
                            <td>
                                <asp:TextBox ID="txtValiddate" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                            </td>
                            <td width="100">
                                领用部门
                            </td>
                            <td>
                                 <asp:DropDownList ID="listApt" runat="server" CssClass="drpdwnlist" 
                                                Enabled="False">
                                            </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="100">
                                生产计划号
                            </td>
                            <td>
                                <asp:DropDownList ID="listPrdctPlan" runat="server" CssClass="drpdwnlist" 
                                    onselectedindexchanged="listPrdctPlan_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td width="100">
                                产品名称
                            </td>
                            <td>
                                <asp:DropDownList ID="listPrdct" runat="server" CssClass="drpdwnlist" 
                                    Enabled="False">
                                </asp:DropDownList>
                            </td>
                            <td width="100">
                                状态
                            </td>
                            <td>
                                <asp:DropDownList ID="listStatus" runat="server" CssClass="drpdwnlist">
                                    <asp:ListItem Value="0">未下发</asp:ListItem>
                                    <asp:ListItem Value="1">己下发</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                           <td width="100" class ="btnhide">
                                仓库
                            </td>
                            <td class ="btnhide">
                                  <asp:DropDownList ID="listStorage" runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                           
                          
                            <td width="100">
                                创建人
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
                        <asp:AsyncPostBackTrigger ControlID = "listPrdctPlan" />
                        <asp:AsyncPostBackTrigger ControlID = "btnReset" />
                    </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="listtitle" style="margin-top: 10px">
                    领退明细<span style="position: relative; float: right">                     
                    <asp:Button ID="btnAdd" runat="server"  CssClass="btnadd  auth" Text="新增" OnClick="btnAdd_Click" />
                        <asp:Button ID="btnCkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnCkAll_Click" />
                        <asp:Button ID="btnDelSel" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnDelSel_Click" OnClientClick="javascript:return confirm('确认删除？');"/>
                      
                    </span>
                </div>
                <div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView2" runat="server" class="grid" AllowPaging="True" AutoGenerateColumns="False"
                            DataKeyNames="ID">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>                             
                                <asp:TemplateField HeaderText="辅料名称" SortExpression="辅料名称">
                                    <ItemTemplate>
                                          <asp:DropDownList ID="listGridName" runat="server" CssClass="drpdwnlist" AutoPostBack="true"  OnSelectedIndexChanged="listGridName_SelectedIndexChanged"   DataSource='<%# gridNamebind()%>' DataTextField ="material_name" DataValueField ="material_code" >
                                                </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="辅料编码" SortExpression="辅料编码">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGridcode" runat="server" DataValueField="辅料编码" DataTextField="辅料编码" Enabled ="false"  CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderText="件数" SortExpression="件数">
                                    <ItemTemplate>
                                         <asp:TextBox ID="txtNum" runat="server" DataValueField="件数" DataTextField="件数"  onkeyup="value=value.replace(/[^\d\.]/g,'')" 
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="规格" SortExpression="规格">
                                    <ItemTemplate>
                                         <asp:TextBox ID="txtAvgWeight" runat="server" DataValueField="件数" DataTextField="件数"
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="领用量" SortExpression="领用量">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGridAmount" runat="server" DataValueField="领用量" DataTextField="领用量"  onkeyup="value=value.replace(/[^\d\.]/g,'')" 
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="计量单位" SortExpression="计量单位">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGridUnit" runat="server" DataValueField="计量单位" DataTextField="计量单位"
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="操作" ItemStyle-Width="80">
                                    <ItemTemplate>
                                        <asp:Button ID="btnGrid2Save" runat="server" Text="保存" CssClass="btn1 auth" Width="75"
                                            OnClick="btnGrid2Save_Click" />
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
                        <asp:AsyncPostBackTrigger ControlID = "btnAdd" />
                        <asp:AsyncPostBackTrigger ControlID = "GridView1" />
                    </Triggers>
                </asp:UpdatePanel>
                </div>
            </div>
            
        </div>
        <div class="aprvinfo" id="flowinfo">
            <div class="tiptop">
                <span>审批流程详情</span><a onclick="Aprvlisthide()"></a></div>
            <div class="flowinfo">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView3" runat="server" class="grid">
                            <HeaderStyle CssClass="gridheader" />
                             <RowStyle CssClass="gridrow" /> <AlternatingRowStyle CssClass="gridalterrow" />
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
