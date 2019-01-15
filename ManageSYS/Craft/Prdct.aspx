<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Prdct.aspx.cs" Inherits="Craft_Prdct" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>产品管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
  
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
                                <asp:TextBox ID="txtNameS" runat="server" class="dfinput1"></asp:TextBox>
                            </td>
                            <td width="100">产品编码
                            </td>
                            <td>
                                <asp:TextBox ID="txtCodeS" runat="server" class="dfinput1"></asp:TextBox>
                            </td>
                            <td width="100" >是否有效
                            </td>
                            <td width="100">
                                <asp:CheckBox ID="rdValidS" runat="server" Text=" " Checked="true" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="listtitle" style="margin-top: 10px">
                    产品列表 <span style="position: relative; float: right">
                        <asp:Button ID="btnAdd" CssClass="btnadd auth" runat="server" OnClick="btnAdd_Click"
                            Text="新增" />
                    </span>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="产品编码" AllowPaging="True"  PageSize="14"
                            AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField  >
                                    <ItemTemplate>
                                        <asp:Button ID="btnSubmit" runat="server" Text="提交审批" CssClass="btn1 auth" Width="100px"
                                            OnClick="btnSubmit_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  >
                                    <ItemTemplate>
                                        <asp:Button ID="btnGridDetail" runat="server" Text="产品详情" CssClass="btn1" Width="100px"
                                            OnClick="btnGridDetail_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  >
                                    <ItemTemplate>
                                        <asp:Button ID="btnFLow" runat="server" Text="审批进度" CssClass="btn1" Width="100px"
                                            OnClick="btnFLow_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField    DataField="产品编码" HeaderText="产品编码" />
                                <asp:BoundField    DataField="产品名称" HeaderText="产品名称" />
                                <asp:BoundField    DataField="包装规格" HeaderText="包装规格" />
                                <asp:BoundField    DataField="加工方式" HeaderText="加工方式" />
                                <asp:BoundField    DataField="是否有效" HeaderText="是否有效" />
                                <asp:TemplateField   HeaderText="审批状态">
                                    <ItemTemplate>
                                        <asp:Label ID="labGrid1Status" runat="server" CssClass="labstatu" Width="60px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  >
                                    <ItemTemplate>
                                        <asp:Button ID="btnGrid1Del" runat="server" Text="删除" CssClass="btn1 auth" OnClick="btnGrid1Del_Click" OnClientClick="javascript:return confirm('确认删除？');" />
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
                        <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                        <asp:AsyncPostBackTrigger ControlID="btnModify" />
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>

            <div class="shade" >
                <div class="info" style="width: 950px;height:430px">
                    <div class="tiphead">
                        <span>产品信息</span><a onclick="$('.shade').fadeOut(100);"></a>
                    </div>
                    <div class="gridinfo">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table class="tablelist">
                                    <tbody>
                                        <tr>
                                            <td width="100">产品名称
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtName" runat="server" class="dfinput1"></asp:TextBox>
                                            </td>
                                            <td width="100">产品编码
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCode" runat="server" class="dfinput1" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td width="110">包装规格
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPack" runat="server" class="dfinput1"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="100">加工方式
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="listType" runat="server" CssClass="drpdwnlist">
                                                    <asp:ListItem Value="1">自主加工</asp:ListItem>
                                                    <asp:ListItem Value="2">来料加工</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>

                                            <td width="100">技术标准编码
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="listTechStd" runat="server" CssClass="drpdwnlist">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="110">产品基础分
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtValue" runat="server" class="dfinput1"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="100">原料配方编码
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="listMtrl" runat="server" class="drpdwnlist">
                                                </asp:DropDownList>
                                            </td>
                                           

                                            <td width="110">回填液配方编码
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="listcoat" runat="server" class="drpdwnlist">
                                                </asp:DropDownList>
                                            </td>
                                            
                                             <td width="100">香精香料配方
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="listFla" runat="server" class="drpdwnlist">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="100">质量考核标准
                                            </td>
                                            <td>
                                              
                                                <asp:DropDownList ID="listqlt" runat="server" class="drpdwnlist">
                                                </asp:DropDownList>
                                            </td>

                                            <td width="100">描述
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDscpt" runat="server" class="dfinput1" ></asp:TextBox>
                                            </td>
                                            <td>路径编码</td>
                                            <td>
                                                <asp:TextBox ID="txtPathcode" runat="server"  Enabled="false" class="dfinput1" ></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:GridView ID="GridView4" runat="server" class="grid" DataKeyNames="section_code"
                                                    AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField   HeaderText="工艺段">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtSection" runat="server" DataValueField="工艺段" DataTextField="工艺段"
                                                                    CssClass="tbinput" Enabled="False" Width="150px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField   HeaderText="路径选择">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="listpath" runat="server" CssClass="drpdwnlist" Width="250px"
                                                                    OnSelectedIndexChanged="listpath_SelectedIndexChanged" AutoPostBack="True">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField   HeaderText="路径详情">
                                                            <ItemTemplate>
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
                                <div class="shadebtn" align="center">
                                    <asp:HiddenField ID="hdScrollY" runat="server" />
                                    <asp:Button ID="btnModify" class="sure" runat="server" Text="保存" OnClick="btnModify_Click" />
                                    <input name="" type="button" class="cancel" value="关闭" onclick="$('.shade').fadeOut(100);" />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="GridView1" />
                                <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="aprvinfo" id="flowinfo">
                <div class="tiptop">
                    <span>审批流程详情</span><a onclick="$('#flowinfo').fadeOut(100);"></a>
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

            
        </div>


        <script type="text/javascript">
            $('.tablelist tbody tr:odd').addClass('odd');
        </script>
    </form>
</body>
</html>
