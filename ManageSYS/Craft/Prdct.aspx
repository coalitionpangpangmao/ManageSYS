<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Prdct.aspx.cs" Inherits="Craft_Prdct" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>产品管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".click1").click(function () {
                $("#addtip").fadeIn(200);
            });

            $(".click2").click(function () {
                $("#mdftip").fadeIn(200);
            });

            $(".click3").click(function () {
                $("#deltip").fadeIn(200);
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
            <li><a href="#">工艺管理</a></li>
            <li><a href="#">产品管理</a></li>
        </ul>
    </div>
    <div class="formbody">
        <div id="usual1" class="usual">
            <div class="itab">
                <ul>
                    <li><a href="#tab1" class="selected" id="tabtop1">产品管理</a></li>
                    <li><a href="#tab2" id="tabtop2">产品信息编辑</a></li>
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
                                产品名称
                            </td>
                            <td>
                                <asp:TextBox ID="txtNameS" runat="server" class="dfinput1"></asp:TextBox>
                            </td>
                            <td width="100">
                                产品编码
                            </td>
                            <td>
                                <asp:TextBox ID="txtCodeS" runat="server" class="dfinput1"></asp:TextBox>
                            </td>
                            <td width="100">
                                是否有效
                            </td>
                            <td width="100">
                                <asp:CheckBox ID="rdValidS" runat="server" Text=" " />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="listtitle" style="margin-top: 10px">
                    产品列表</div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="产品编码" AllowPaging="True"
                            AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnSubmit" runat="server" Text="提交审批" CssClass="btn1 auth" Width="100px"
                                            OnClick="btnSubmit_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnGridDetail" runat="server" Text="产品详情" CssClass="btn1" Width="100px"
                                            OnClick="btnGridDetail_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnFLow" runat="server" Text="审批进度" CssClass="btn1" Width="100px"
                                            OnClick="btnFLow_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="产品编码" HeaderText="产品编码" />
                                <asp:BoundField DataField="产品名称" HeaderText="产品名称" />
                                <asp:BoundField DataField="包装规格" HeaderText="包装规格" />
                                <asp:BoundField DataField="加工方式" HeaderText="加工方式" />
                                <asp:BoundField DataField="是否有效" HeaderText="是否有效" />
                                <asp:TemplateField HeaderText="审批状态">
                                    <ItemTemplate>
                                        <asp:Label ID="labGrid1Status" runat="server" CssClass="labstatu" Width="60px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnGrid1Del" runat="server" Text="删除" CssClass="btn1" OnClick="btnGrid1Del_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                        <asp:AsyncPostBackTrigger ControlID="btnModify" />
                        <asp:AsyncPostBackTrigger ControlID="btnDel" />
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="tab2" class="tabson">
            <div class="gridtools  auth">               
                    <asp:Button ID="btnAdd" CssClass="btnadd auth" runat="server" OnClick="btnAdd_Click"
                        Text="新增" />
                    &nbsp; &nbsp;
                    <asp:Button ID="btnModify" CssClass="btnview  auth" runat="server" OnClick="btnModify_Click"
                        Text="保存" />
                    &nbsp; &nbsp;
                    <asp:Button ID="btnDel" CssClass="btndel  auth" runat="server" Text="删除" OnClick="btnDel_Click" />               
            </div>
            <div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="framelist">
                            <table class="tablelist">
                                <tbody>
                                    <tr>
                                        <td width="100">
                                            产品名称
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtName" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                        <td width="100">
                                            产品编码
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCode" runat="server" class="dfinput1" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100">
                                            包装规格
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPack" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                        <td width="100">
                                            加工方式
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listType" runat="server" CssClass="drpdwnlist">
                                                <asp:ListItem Value="1">自主加工</asp:ListItem>
                                                <asp:ListItem Value="2">来料加工</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100">
                                            技术标准编码
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listTechStd" runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="100">
                                            标准值
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtValue" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100">
                                            原料配方编码
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listMtrl" runat="server" class="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="100">
                                            辅料配方编码
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listAux" runat="server" class="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100">
                                            回填液配方编码
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listcoat" runat="server" class="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="100">
                                            质量考核标准
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listqlt" runat="server" class="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100">
                                            描述
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtDscpt" runat="server" class="dfinput1" Width="450px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
                        <asp:AsyncPostBackTrigger ControlID = "btnAdd" />
                    </Triggers>
                </asp:UpdatePanel>
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
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
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
    </form>
</body>
</html>
