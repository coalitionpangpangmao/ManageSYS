<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Recipe.aspx.cs" Inherits="Craft_Recipe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>配方管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
    <link rel="stylesheet" href="../js/jquery-treeview/jquery.treeview.css" />
    <link rel="stylesheet" href="../js/jquery-treeview/screen.css" />
    <script type="text/javascript" src="../js/jquery-treeview/jquery.cookie.js"></script>
    <script src="../js/jquery-treeview/jquery.treeview.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            initTree();
        });
        function initTree() {
            $('#browser').treeview(
                {
                    toggle: function ()
                    { console.log('%s was toggled.', $(this).find('>span').text()); },
                    persist: 'cookie',
                    collapsed: true
                });
            $(".folder").click(function () {
                $('.folder').removeClass("selectedbold");
                $('.file').removeClass("selectedbold");
                $(this).addClass("selectedbold");
                var code = $(this).attr('value');
                $('#tabtop1').click();
                $('#hdcode').attr('value', code);
                $('#hdcode1').attr('value', code);
                $('#hdcode2').attr('value', code);
                $('#hdcode3').attr('value', code);
                $('#btnUpdateList').click();


            });
            $(".file").click(function () {
                $('.folder').removeClass("selectedbold");
                $('.file').removeClass("selectedbold");
                $(this).addClass("selectedbold");
                var code = $(this).attr('value');
                if (code.substr(0, 5) == '70306') {
                    $('#tabtop2').click();
                    $('#hdcode1').attr('value', code);
                    $('#btnUpdate1').click();
                }
                else if (code.substr(0, 5) == '70308') {
                    $('#tabtop3').click();
                    $('#hdcode2').attr('value', code);
                    $('#btnUpdate2').click();
                }
                else {
                    $('#tabtop4').click();
                    $('#hdcode3').attr('value', code);
                    $('#btnUpdate3').click();
                }
            });
        }



    </script>
</head>
<body>
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <form id="Form1" runat="server">
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">工艺管理</a></li>
                <li><a href="#">配方管理</a></li>
            </ul>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <div class="mainbox">
            <div class="mainleft">
                <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button ID="btnUpdate" CssClass="btnhide" runat="server" OnClick="btnUpdate_Click" />
                        <div class="leftinfo">
                            <div class="listtitle">配方管理</div>
                            <% = tvHtml %>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <!--mainleft end-->
            <div class="mainright">
                <div id="usual1" class="usual">
                    <div class="itab">
                        <ul>
                            <li><a href="#tab1" id="tabtop1">产品配方表</a></li>
                            <li><a href="#tab2" id="tabtop2">原料配方</a></li>
                            <li><a href="#tab3" id="tabtop3">回填液配方</a></li>
                            <li><a href="#tab4" id="tabtop4">香精香料</a></li>
                        </ul>
                    </div>
                </div>
                <div id="tab1" class="tabson">
                    <asp:UpdatePanel ID="UpdatePanelall" runat="server">
                        <ContentTemplate>
                            <table class="framelist">
                                <tr>
                                    <td>
                                        <div class="listtitle">
                                            配方详情<span style="position: relative; float: right">
                                                <asp:Button ID="btnCkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnCkAll_Click" />
                                                <asp:Button ID="btnDel" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnDel_Click" OnClientClick="javascript:return confirm('确认删除？');" />
                                                <asp:HiddenField ID="hdcode" runat="server" />
                                                <asp:Button ID="btnUpdateList" runat="server" Text="Button" CssClass="btnhide" OnClick="btnUpdateList_Click" />
                                            </span>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridViewAll" runat="server" class="grid" DataKeyNames="配方编码" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="选择">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnGridDetail" runat="server" Text="配方详情" CssClass="btn1" Width="100px"
                                                            OnClick="btnGridDetail_Click" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="110px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnSubmit" runat="server" Text="提交审批" CssClass="btn1  auth" Width="100px"
                                                            OnClick="btnSubmit_Click" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="110px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnFLow" runat="server" Text="审批进度" CssClass="btn1" Width="100px"
                                                            OnClick="btnFLow_Click" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="110px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="配方编码" HeaderText="配方编码" />
                                                <asp:BoundField DataField="配方名称" HeaderText="配方名称" />
                                                <asp:BoundField DataField="启用时间" HeaderText="启用时间" />
                                                <asp:BoundField DataField="编辑人员" HeaderText="编辑人员" />
                                                <asp:TemplateField HeaderText="审批状态">
                                                    <ItemTemplate>
                                                        <asp:Label ID="labAprv" runat="server" CssClass="labstatu" Width="60px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="gridheader" />
                                            <RowStyle CssClass="gridrow" />
                                            <AlternatingRowStyle CssClass="gridalterrow" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnCkAll" />
                            <asp:AsyncPostBackTrigger ControlID="btnDel" />
                            <asp:AsyncPostBackTrigger ControlID="btnUpdateList" />
                            <asp:AsyncPostBackTrigger ControlID="GridViewAll" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div id="tab2" class="tabson">
                    <table class="framelist">
                        <tr>
                            <td>
                                <div>
                                    <div class="listtitle">
                                        配方信息<span style="position: relative; float: right">
                                            <asp:Button ID="btnAddR1" class="btnadd  auth" runat="server" Text="新增" OnClick="btnAddR1_Click" />
                                            &nbsp;&nbsp;
                            <asp:Button ID="btnModify1" class="btnmodify  auth" runat="server" Text="保存" OnClick="btnModify1_Click" />&nbsp;
                            <asp:HiddenField ID="hdcode1" runat="server" />
                                            <asp:Button ID="btnUpdate1" runat="server" Text="Button" CssClass="btnhide" OnClick="btnUpdate1_Click" />
                                        </span>
                                    </div>
                                    <div>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <table class="tablelist">
                                                    <tbody>
                                                        <tr>
                                                            <td width="100">配方名称
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtName1" runat="server" class="dfinput1"></asp:TextBox>
                                                            </td>
                                                            <td width="100">配方编码
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCode1" runat="server" class="dfinput1" Enabled="False"></asp:TextBox>
                                                            </td>
                                                            <td width="100">产品编码
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="listPro1" runat="server" CssClass="drpdwnlist">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="100">标准版本号
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtVersion1" runat="server" class="dfinput1"></asp:TextBox>
                                                            </td>
                                                            <td width="100">执行日期
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtExeDate1" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                                            </td>
                                                            <td width="100">结束日期
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtEndDate1" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="100">受控状态
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="listStatus1" runat="server" CssClass="drpdwnlist">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td width="100">编制人
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="listCreator1" runat="server" CssClass="drpdwnlist"
                                                                    Enabled="False">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td width="100">编制日期
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCrtDate1" runat="server" class="dfinput1"
                                                                    onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" Enabled="False"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="100">编制部门
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="listCrtApt1" runat="server" CssClass="drpdwnlist"
                                                                    Enabled="False">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td width="100">是否有效
                                                            </td>
                                                            <td width="100">
                                                                <asp:CheckBox ID="ckValid1" runat="server" Text=" " />
                                                            </td>
                                                            <td width="100">描述
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDscpt1" runat="server" class="dfinput1"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnUpdate1" />
                                                <asp:AsyncPostBackTrigger ControlID="btnAddR1" />
                                                <asp:AsyncPostBackTrigger ControlID="btnUpdateList" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="usuals1" class="usual">
                                    <div class="itab">
                                        <ul>
                                            <li><a href="#tab5" id="tabtop5">长梗原料配方</a></li>
                                            <li><a href="#tab6" id="tabtop6">碎片原料配方</a></li>
                                        </ul>
                                    </div>
                                </div>
                                <div id="tab5" class="tabson">
                                    <div class="listtitle">
                                        配方详情<span style="position: relative; float: right"><asp:Button ID="btnAdd1" runat="server"
                                            CssClass="btnadd  auth" Text="新增" OnClick="btnAdd1_Click" />
                                            <asp:Button ID="btnCkAll1" runat="server" CssClass="btnset  auth" Text="全选" OnClick="btnCkAll1_Click" />
                                            <asp:Button ID="btnDelSel1" runat="server" CssClass="btndel  auth" Text="删除" OnClick="btnDelSel1_Click" OnClientClick="javascript:return confirm('确认删除？');" />
                                            <asp:Button ID="btnGridSave1" class="btnmodify  auth" runat="server" Text="全部保存" Width="90px" OnClick="btnGridSave1_Click" />
                                        </span>
                                    </div>
                                    <div>
                                        <asp:UpdatePanel ID="UpdatePanel1_2" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>

                                                <asp:GridView ID="GridView1" runat="server" class="grid" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="ID" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="9">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="物料分类">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="listGridType1" runat="server" CssClass="drpdwnlist" Width="80px" OnSelectedIndexChanged="listGridType1_SelectedIndexChanged" AutoPostBack="True" DataSource='<%# gridTypebind("0202")%>' DataValueField="mattree_code" DataTextField="mattree_name">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="物料名称" SortExpression="物料名称">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="listGridName1" runat="server" CssClass="drpdwnlist" OnSelectedIndexChanged="listGirdName1_SelectedIndexChanged" AutoPostBack="True" Width="300px">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="物料编码" SortExpression="物料编码">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCodeM" runat="server" DataValueField="物料编码" DataTextField="物料编码"
                                                                    CssClass="tbinput1" Enabled="False"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="批投料量" SortExpression="批投料量">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAmountM" runat="server" DataValueField="批投料量" DataTextField="批投料量" onkeyup="value=value.replace(/[^\d\.]/g,'')"
                                                                    CssClass="tbinput"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="优先组" SortExpression="优先组">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtGroupM" runat="server" DataValueField="优先组" DataTextField="优先组"
                                                                    CssClass="tbinput"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnSave1" runat="server" Text="保存" CssClass="btn1  auth" OnClick="btnSave1_Click" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnDel1" runat="server" Text="删除" CssClass="btn1  auth" OnClick="btnDel1_Click" OnClientClick="javascript:return confirm('确认删除？');" />
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
                                                <asp:AsyncPostBackTrigger ControlID="btnAdd1" />
                                                <asp:AsyncPostBackTrigger ControlID="btnCkAll1" />
                                                <asp:AsyncPostBackTrigger ControlID="btnModify1" />
                                                <asp:AsyncPostBackTrigger ControlID="GridView1" />
                                                <asp:AsyncPostBackTrigger ControlID="btnUpdate1" />
                                                <asp:AsyncPostBackTrigger ControlID="btnGridSave1" />
                                                <asp:AsyncPostBackTrigger ControlID="btnUpdateList" />
                                                <asp:AsyncPostBackTrigger ControlID="btnDelSel1" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div id="tab6" class="tabson">
                                    <div class="listtitle">
                                        配方详情<span style="position: relative; float: right"><asp:Button ID="btnAdd1_2" runat="server"
                                            CssClass="btnadd  auth" Text="新增" OnClick="btnAdd1_2_Click" />
                                            <asp:Button ID="btnCkAll1_2" runat="server" CssClass="btnset  auth" Text="全选" OnClick="btnCkAll1_2_Click" />
                                            <asp:Button ID="btnDelSel1_2" runat="server" CssClass="btndel  auth" Text="删除" OnClick="btnDelSel1_2_Click" OnClientClick="javascript:return confirm('确认删除？');" />
                                            <asp:Button ID="btnGridSave1_2" class="btnmodify  auth" runat="server" Text="全部保存" Width="90px" OnClick="btnGridSave1_2_Click" />
                                        </span>
                                    </div>
                                    <div>
                                        <asp:UpdatePanel ID="UpdatePanel1_3" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>

                                                <asp:GridView ID="GridView1_2" runat="server" class="grid" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="ID" OnPageIndexChanging="GridView1_2_PageIndexChanging" PageSize="9">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="物料分类">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="listGridType1" runat="server" CssClass="drpdwnlist" Width="80px" OnSelectedIndexChanged="listGridType1_SelectedIndexChanged" AutoPostBack="True" DataSource='<%# gridTypebind("0204")%>' DataValueField="mattree_code" DataTextField="mattree_name">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="物料名称" SortExpression="物料名称">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="listGridName1" runat="server" CssClass="drpdwnlist" OnSelectedIndexChanged="listGirdName1_SelectedIndexChanged" AutoPostBack="True" Width="300px">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="物料编码" SortExpression="物料编码">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCodeM" runat="server" DataValueField="物料编码" DataTextField="物料编码"
                                                                    CssClass="tbinput1" Enabled="False"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="批投料量" SortExpression="批投料量">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAmountM" runat="server" DataValueField="批投料量" DataTextField="批投料量" onkeyup="value=value.replace(/[^\d\.]/g,'')"
                                                                    CssClass="tbinput"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="优先组" SortExpression="优先组">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtGroupM" runat="server" DataValueField="优先组" DataTextField="优先组"
                                                                    CssClass="tbinput"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnSave1_2" runat="server" Text="保存" CssClass="btn1  auth" OnClick="btnSave1_2_Click" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnDel1_2" runat="server" Text="删除" CssClass="btn1  auth" OnClick="btnDel1_2_Click" OnClientClick="javascript:return confirm('确认删除？');" />
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
                                                <asp:AsyncPostBackTrigger ControlID="btnAdd1_2" />
                                                <asp:AsyncPostBackTrigger ControlID="btnCkAll1_2" />
                                                <asp:AsyncPostBackTrigger ControlID="btnModify1" />
                                                <asp:AsyncPostBackTrigger ControlID="GridView1_2" />
                                                <asp:AsyncPostBackTrigger ControlID="btnUpdate1" />
                                                <asp:AsyncPostBackTrigger ControlID="btnGridSave1_2" />
                                                <asp:AsyncPostBackTrigger ControlID="btnUpdateList" />
                                                <asp:AsyncPostBackTrigger ControlID="btnDelSel1_2" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="tab3" class="tabson">
                    <div class="framelist">
                        <table class="tablelist">
                            <tr>
                                <td>
                                    <div>
                                        <div class="listtitle">
                                            配方信息<span style="position: relative; float: right">
                                                <asp:Button ID="btnAddR2" class="btnadd  auth" runat="server" Text="新增" OnClick="btnAddR2_Click" />
                                                &nbsp;&nbsp;
                                <asp:Button ID="btnModify2" class="btnmodify auth" runat="server" Text="保存" OnClick="btnModify2_Click" />&nbsp;
                                <asp:HiddenField ID="hdcode2" runat="server" />
                                                <asp:Button ID="btnUpdate2" runat="server" Text="Button" CssClass="btnhide" OnClick="btnUpdate2_Click" />
                                            </span>
                                        </div>
                                        <div>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <table class="tablelist">
                                                        <tbody>
                                                            <tr>
                                                                <td width="100">配方名称
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtName2" runat="server" class="dfinput1"></asp:TextBox>
                                                                </td>
                                                                <td width="100">配方编码
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCode2" runat="server" class="dfinput1" Enabled="False"></asp:TextBox>
                                                                </td>
                                                                <td width="100">产品编码
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="listPro2" runat="server" CssClass="drpdwnlist">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="100">标准版本号
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtVersion2" runat="server" class="dfinput1"></asp:TextBox>
                                                                </td>
                                                                <td width="100">执行日期
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtExeDate2" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                                                </td>
                                                                <td width="100">结束日期
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtEndDate2" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="100">受控状态
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="listStatus2" runat="server" CssClass="drpdwnlist">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td width="100">编制人
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="listCreator2" runat="server" CssClass="drpdwnlist"
                                                                        Enabled="False">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td width="100">编制日期
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCrtDate2" runat="server" class="dfinput1"
                                                                        onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" Enabled="False"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="100">编制部门
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="listCrtApt2" runat="server" CssClass="drpdwnlist"
                                                                        Enabled="False">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td width="100">是否有效
                                                                </td>
                                                                <td width="100">
                                                                    <asp:CheckBox ID="ckValid2" runat="server" Text=" " />
                                                                </td>
                                                                <td width="100">描述
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDscpt2" runat="server" class="dfinput1"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnUpdate2" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnAddR2" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnUpdateList" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>

                        <div id="usualc" class="usual">
                            <div class="itab">
                                <ul>
                                    <li><a href="#tab7" id="tabtop7">回填液配方</a></li>
                                    <li><a href="#tab8" id="tabtop8">料液配方</a></li>
                                </ul>
                            </div>
                        </div>
                        <div id="tab7" class="tabson">
                            <div class="listtitle">
                                配方详情<span style="position: relative; float: right"><asp:Button ID="btnAdd2" runat="server"
                                    CssClass="btnadd  auth" Text="新增" OnClick="btnAdd2_Click" />
                                    <asp:Button ID="btnCkAll2" runat="server" CssClass="btnset" Text="全选" OnClick="btnCkAll2_Click" />
                                    <asp:Button ID="btnDelSel2" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnDelSel2_Click" OnClientClick="javascript:return confirm('确认删除？');" />
                                    <asp:Button ID="btnGridSave2" class="btnmodify auth" runat="server" Text="全部保存" Width="90px" OnClick="btnGridSave2_Click" />
                                </span>
                            </div>
                            <div>
                                <asp:UpdatePanel ID="UpdatePanel2_1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView2" runat="server" class="grid" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="GridView2_PageIndexChanging" PageSize="9" DataKeyNames="ID">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="回填液类型">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="listGridName2"  runat="server" CssClass ="drpdwnlist" DataSource='<%# gridHTYbind("0410,0401,0402,0407")%>' DataValueField="material_code" DataTextField="material_name" AutoPostBack="true" OnSelectedIndexChanged="listGirdName2_SelectedIndexChanged"></asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="回填液编码" SortExpression="物料编码">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCodeM" runat="server" DataValueField="物料编码" DataTextField="物料编码"
                                                            CssClass="tbinput1" Enabled="False"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="比例%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtScale" runat="server" DataValueField="比例" DataTextField="比例" onkeyup="value=value.replace(/[^\d\.]/g,'')"
                                                            CssClass="tbinput"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="每罐调配所需">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPercent" runat="server" DataValueField="每罐调配所需" DataTextField="每罐调配所需" onkeyup="value=value.replace(/[^\d\.]/g,'')"
                                                            CssClass="tbinput"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="每批用量">
                                                 <ItemTemplate>
                                                        <asp:TextBox ID="txtBatchNum" runat="server" DataValueField="每批用量" DataTextField="每批用量" onkeyup="value=value.replace(/[^\d\.]/g,'')"
                                                            CssClass="tbinput"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnSave2" runat="server" Text="保存" CssClass="btn1 auth" OnClick="btnSave2_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnDel2" runat="server" Text="删除" CssClass="btn1 auth" OnClick="btnDel2_Click" OnClientClick="javascript:return confirm('确认删除？');" />
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
                                        <asp:AsyncPostBackTrigger ControlID="btnAdd2" />
                                        <asp:AsyncPostBackTrigger ControlID="btnCkAll2" />
                                        <asp:AsyncPostBackTrigger ControlID="btnDelSel2" />
                                        <asp:AsyncPostBackTrigger ControlID="btnModify2" />
                                        <asp:AsyncPostBackTrigger ControlID="GridView2" />
                                        <asp:AsyncPostBackTrigger ControlID="btnUpdate2" />
                                        <asp:AsyncPostBackTrigger ControlID="btnGridSave2" />
                                        <asp:AsyncPostBackTrigger ControlID="btnUpdateList" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div id="tab8" class="tabson">
                            <div class="listtitle">
                                配方详情<span style="position: relative; float: right"><asp:Button ID="btnAdd2_2" runat="server"
                                    CssClass="btnadd  auth" Text="新增" OnClick="btnAdd2_2_Click" />
                                    <asp:Button ID="btnCkAll2_2" runat="server" CssClass="btnset" Text="全选" OnClick="btnCkAll2_2_Click" />
                                    <asp:Button ID="btnDelSel2_2" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnDelSel2_2_Click" OnClientClick="javascript:return confirm('确认删除？');" />
                                    <asp:Button ID="btnGridSave2_2" class="btnmodify auth" runat="server" Text="全部保存" Width="90px" OnClick="btnGridSave2_2_Click" />
                                </span>
                            </div>
                            <div>
                                <asp:UpdatePanel ID="UpdatePanel2_2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView2_2" runat="server" class="grid" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="GridView2_2_PageIndexChanging" PageSize="9" DataKeyNames="ID">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="料液名称">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="listGridName2" runat="server" CssClass="drpdwnlist" DataSource='<%# gridHTYbind("0408,0409")%>' DataValueField="material_code" DataTextField="material_name" AutoPostBack="true" OnSelectedIndexChanged="listGirdName2_SelectedIndexChanged"></asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="料液编码" SortExpression="物料编码">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCodeM" runat="server" DataValueField="物料编码" DataTextField="物料编码"
                                                            CssClass="tbinput1" Enabled="False"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="比例%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtScale" runat="server" DataValueField="比例" DataTextField="比例" onkeyup="value=value.replace(/[^\d\.]/g,'')"
                                                            CssClass="tbinput"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="每罐调配所需">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPercent" runat="server" DataValueField="每罐调配所需" DataTextField="每罐调配所需" onkeyup="value=value.replace(/[^\d\.]/g,'')"
                                                            CssClass="tbinput"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="每批用量">
                                                 <ItemTemplate>
                                                        <asp:TextBox ID="txtBatchNum" runat="server" DataValueField="每批用量" DataTextField="每批用量" onkeyup="value=value.replace(/[^\d\.]/g,'')"
                                                            CssClass="tbinput"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnSave2_2" runat="server" Text="保存" CssClass="btn1 auth" OnClick="btnSave2_2_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnDel2_2" runat="server" Text="删除" CssClass="btn1 auth" OnClick="btnDel2_2_Click" OnClientClick="javascript:return confirm('确认删除？');" />
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
                                        <asp:AsyncPostBackTrigger ControlID="btnAdd2_2" />
                                        <asp:AsyncPostBackTrigger ControlID="btnCkAll2_2" />
                                        <asp:AsyncPostBackTrigger ControlID="btnDelSel2_2" />
                                        <asp:AsyncPostBackTrigger ControlID="btnModify2" />
                                        <asp:AsyncPostBackTrigger ControlID="GridView2_2" />
                                        <asp:AsyncPostBackTrigger ControlID="btnUpdate2" />
                                        <asp:AsyncPostBackTrigger ControlID="btnGridSave2_2" />
                                        <asp:AsyncPostBackTrigger ControlID="btnUpdateList" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>
                </div>
                <div id="tab4" class="tabson">
                    <div class="framelist">
                        <table class="tablelist">
                            <tr>
                                <td>
                                    <div>
                                        <div class="listtitle">
                                            配方信息<span style="position: relative; float: right">
                                                <asp:Button ID="btnAddR3" class="btnadd  auth" runat="server" Text="新增" OnClick="btnAddR3_Click" />
                                                &nbsp;&nbsp;
                                <asp:Button ID="btnModify3" class="btnmodify auth" runat="server" Text="保存" OnClick="btnModify3_Click" />&nbsp;
                                <asp:HiddenField ID="hdcode3" runat="server" />
                                                <asp:Button ID="btnUpdate3" runat="server" Text="Button" CssClass="btnhide" OnClick="btnUpdate3_Click" />
                                            </span>
                                        </div>
                                        <div>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <table class="tablelist">
                                                        <tbody>
                                                            <tr>
                                                                <td width="100">配方名称
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtName3" runat="server" class="dfinput1"></asp:TextBox>
                                                                </td>
                                                                <td width="100">配方编码
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCode3" runat="server" class="dfinput1" Enabled="False"></asp:TextBox>
                                                                </td>
                                                                <td width="100">产品编码
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="listPro3" runat="server" CssClass="drpdwnlist">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="100">标准版本号
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtVersion3" runat="server" class="dfinput1"></asp:TextBox>
                                                                </td>
                                                                <td width="100">执行日期
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtExeDate3" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                                                </td>
                                                                <td width="100">结束日期
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtEndDate3" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="100">受控状态
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="listStatus3" runat="server" CssClass="drpdwnlist">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td width="100">编制人
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="listCreator3" runat="server" CssClass="drpdwnlist"
                                                                        Enabled="False">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td width="100">编制日期
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCrtDate3" runat="server" class="dfinput1"
                                                                        onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" Enabled="False"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="100">编制部门
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="listCrtApt3" runat="server" CssClass="drpdwnlist"
                                                                        Enabled="False">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td width="100">是否有效
                                                                </td>
                                                                <td width="100">
                                                                    <asp:CheckBox ID="ckValid3" runat="server" Text=" " />
                                                                </td>
                                                                <td width="100">描述
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDscpt3" runat="server" class="dfinput1"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnUpdate3" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnAddR3" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnUpdateList" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>


                        <div class="listtitle">
                            配方详情<span style="position: relative; float: right"><asp:Button ID="btnAdd3" runat="server"
                                CssClass="btnadd  auth" Text="新增" OnClick="btnAdd3_Click" />
                                <asp:Button ID="btnCkAll3" runat="server" CssClass="btnset" Text="全选" OnClick="btnCkAll3_Click" />
                                <asp:Button ID="btnDelSel3" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnDelSel3_Click" OnClientClick="javascript:return confirm('确认删除？');" />
                                <asp:Button ID="btnGridSave3" class="btnmodify auth" runat="server" Text="全部保存" Width="90px" OnClick="btnGridSave3_Click" />
                            </span>
                        </div>
                        <div>
                            <asp:UpdatePanel ID="UpdatePanel3_1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="GridView3" runat="server" class="grid" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="GridView3_PageIndexChanging" PageSize="9" DataKeyNames="ID">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="香料种类">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="listGridName3" runat="server" CssClass="drpdwnlist" DataSource='<%# gridHTYbind("0410")%>' DataValueField="material_code" DataTextField="material_name" AutoPostBack="true" OnSelectedIndexChanged="listGirdName3_SelectedIndexChanged"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="香料编码" SortExpression="物料编码">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCodeM" runat="server" DataValueField="物料编码" DataTextField="物料编码"
                                                        CssClass="tbinput1" Enabled="False"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="比例%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtScale" runat="server" DataValueField="比例" DataTextField="比例" onkeyup="value=value.replace(/[^\d\.]/g,'')"
                                                        CssClass="tbinput"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="每罐调配所需">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPercent" runat="server" DataValueField="每罐调配所需" DataTextField="每罐调配所需" onkeyup="value=value.replace(/[^\d\.]/g,'')"
                                                        CssClass="tbinput"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="每批用量">
                                                 <ItemTemplate>
                                                        <asp:TextBox ID="txtBatchNum" runat="server" DataValueField="每批用量" DataTextField="每批用量" onkeyup="value=value.replace(/[^\d\.]/g,'')"
                                                            CssClass="tbinput"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnSave3" runat="server" Text="保存" CssClass="btn1 auth" OnClick="btnSave3_Click" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnDel3" runat="server" Text="删除" CssClass="btn1 auth" OnClick="btnDel3_Click" OnClientClick="javascript:return confirm('确认删除？');" />
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
                                    <asp:AsyncPostBackTrigger ControlID="btnAdd3" />
                                    <asp:AsyncPostBackTrigger ControlID="btnCkAll3" />
                                    <asp:AsyncPostBackTrigger ControlID="btnDelSel3" />
                                    <asp:AsyncPostBackTrigger ControlID="btnModify3" />
                                    <asp:AsyncPostBackTrigger ControlID="GridView3" />
                                    <asp:AsyncPostBackTrigger ControlID="btnUpdate3" />
                                    <asp:AsyncPostBackTrigger ControlID="btnGridSave3" />
                                    <asp:AsyncPostBackTrigger ControlID="btnUpdateList" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>


                    </div>
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
                        <asp:GridView ID="GridView4" runat="server" class="grid">
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                            <AlternatingRowStyle CssClass="gridalterrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridViewAll" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <!--mainright end-->
        <script type="text/javascript">
            $("#usual1 ul").idTabs();
            $("#usuals1 ul").idTabs();
            $("#usualc ul").idTabs();
        </script>

    </form>
</body>
</html>
