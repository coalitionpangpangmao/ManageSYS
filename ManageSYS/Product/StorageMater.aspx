<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageMater.aspx.cs" Inherits="Product_StorageMater" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>原料库管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $(".click2").click(function () {
                $("#mdftip").fadeIn(200);
            });
            $(".tiptop a").click(function () {
                $(".tip").fadeOut(200);
            });
            $(".sure").click(function () {
                $(".tip").fadeOut(100);
            });

            $(".cancel").click(function () {
                $(".tip").fadeOut(100);
            });

        });
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
            <li><a href="#">原料库管理</a></li>
        </ul>
    </div>
    <div class="formbody">
        <div id="usual1" class="usual">
            <div class="itab">
                <ul>
                    <li><a href="#tab1" class="selected" id="tabtop1">出入库管理</a></li>
                    <li><a href="#tab2" id="tabtop2">原料领退明细</a></li>
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
                    原料领退表<span style="position: relative; float: right">
                        <asp:Button ID="btnGridDel" runat="server" Text="删除" class="btndel auth" OnClick="btnGridDel_Click" />
                        <asp:Button ID="btnGridNew" runat="server" Text="新建" class="btnadd  auth" OnClick="btnGridNew_Click" />
                    </span>
                </div>
                <div style="overflow: auto">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="单据号" AutoGenerateColumns="False" HeaderStyle-Wrap="False" AlternatingRowStyle-Wrap="False" EditRowStyle-Wrap="False" RowStyle-Wrap="False">
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
                                    <asp:BoundField DataField="烟梗总量" HeaderText="烟梗总量" />
                                    <asp:BoundField DataField="碎片总量" HeaderText="碎片总量" />
                                    <asp:BoundField DataField="仓库" HeaderText="仓库" />
                                
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
                                <RowStyle CssClass="gridrow" />
                                <AlternatingRowStyle CssClass="gridalterrow" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                            <asp:AsyncPostBackTrigger ControlID="btnModify" />
                            <asp:AsyncPostBackTrigger ControlID="btnCreate" />
                            <asp:AsyncPostBackTrigger ControlID="btnGridDel" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div id="tab2" class="tabson">
            <div class="framelist">
                <div class="listtitle">
                    原料领退管理<span style="position: relative; float: right" class="click2">
                        <asp:RadioButton ID="rdOut" runat="server" Text="领用" GroupName="Storage" Checked="True" />
                        <asp:RadioButton ID="rdIn" runat="server" Text="退库" GroupName="Storage" />
                        <asp:Button ID="btnReset" runat="server" Text="重置" CssClass="btnset" OnClick="btnReset_Click" />
                        <input id="Button2" type="button" value="保存" class="btnmodify auth" />
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
                                                OnSelectedIndexChanged="listPrdctPlan_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="100">
                                            产品名称
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listPrdct" runat="server" CssClass="drpdwnlist" Enabled="False">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="100">
                                            审批状态
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listStatus" runat="server" CssClass="drpdwnlist" 
                                                Enabled="False">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="100">
                                            投料批次
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBatchNum" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100">
                                            仓库
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listStorage" runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="100">
                                            烟梗总量
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtStemSum" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                        <td width="100">
                                            碎片总量
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtChipSum" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
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
                            <asp:AsyncPostBackTrigger ControlID="listPrdctPlan" />
                            <asp:AsyncPostBackTrigger ControlID="btnReset" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="listtitle" style="margin-top: 10px">
                    领退明细<span style="position: relative; float: right">
                        <asp:Button ID="btnCreate" runat="server" CssClass="btnview auth" Text="按明细生成领退单"
                            OnClick="btnCreate_Click" Width="160px" />
                        <asp:Button ID="btnAdd" runat="server" CssClass="btnadd  auth" Text="新增" OnClick="btnAdd_Click" />
                        <asp:Button ID="btnCkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnCkAll_Click" />
                        <asp:Button ID="btnDelSel" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnDelSel_Click" />
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
                                    <asp:TemplateField HeaderText="仓库" SortExpression="仓库">
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
                                    <asp:TemplateField HeaderText="类型" SortExpression="类型">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="listGridtype" runat="server" CssClass="drpdwnlist" Width="70">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="YG">烟梗类</asp:ListItem>
                                                <asp:ListItem Value="SP">碎片类</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="计量单位" SortExpression="计量单位">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGridUnit" runat="server" DataValueField="计量单位" DataTextField="计量单位"
                                                CssClass="tbinput"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="原料编码" SortExpression="原料编码">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGridcode" runat="server" DataValueField="原料编码" DataTextField="原料编码"
                                                CssClass="tbinput"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="原料名称" SortExpression="原料名称">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGridName" runat="server" DataValueField="原料名称" DataTextField="原料名称"
                                                CssClass="tbinput"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="领用量" SortExpression="领用量">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGridAmount" runat="server" DataValueField="领用量" DataTextField="领用量"
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
                            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                            <asp:AsyncPostBackTrigger ControlID="GridView1" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="tip" id="mdftip">
                <div class="tiptop">
                    <span>提示信息</span><a></a></div>
                <div class="tipinfo">
                    <span>
                        <img src="../images/ticon.png" /></span>
                    <div class="tipright">
                        <p>
                            确认保存此条记录 ？</p>
                        <cite>如果是请点击确定按钮 ，否则请点取消。</cite>
                    </div>
                </div>
                <div class="tipbtn">
                    <asp:Button ID="btnModify" class="sure" runat="server" Text="确定" OnClick="btnModify_Click" />&nbsp;
                    <input name="" type="button" class="cancel" value="取消" />
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
