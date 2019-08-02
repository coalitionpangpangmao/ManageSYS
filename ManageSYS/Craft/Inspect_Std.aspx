<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Inspect_Std.aspx.cs" Inherits="Inspect_Std" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>工艺检查标准管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <link rel="stylesheet" href="../js/jquery-treeview/jquery.treeview.css" />
    <link rel="stylesheet" href="../js/jquery-treeview/screen.css" />
    <script type="text/javascript" src="../js/jquery-treeview/jquery.cookie.js"></script>
    <script src="../js/jquery-treeview/jquery.treeview.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#browser").treeview({
                toggle: function () {
                    console.log("%s was toggled.", $(this).find(">span").text());
                }
            });
            $(".folder").click(function () {
                $('.folder').removeClass("selectedbold");
                $('.file').removeClass("selectedbold");
                $(this).addClass("selectedbold");
            });

        });
        function tabClick(code) {
            $("#hideprc").attr('value', code);
            $("#UpdateGrid").click();
        }
    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">工艺管理</a></li>
                <li><a href="#">工艺检查标准</a></li>
            </ul>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="mainbox">
            <table class="dflist">
                <tr>
                    <td colspan="2">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="listtitle">
                                    工艺检查标准信息<asp:DropDownList ID="listVersion" runat="server" CssClass="drpdwnlist" AutoPostBack="True" OnSelectedIndexChanged="listVersion_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <span style="position: relative; float: right">审批状态：<asp:DropDownList ID="listAprv" runat="server" CssClass="drpdwnlist" Width="80px" Enabled="False"></asp:DropDownList>
                                        <asp:Button ID="btnSubmit" runat="server" Text="提交审批" CssClass="btn1  auth" Width="80px" OnClick="btnSubmit_Click" />
                                        &nbsp;   
                                       <asp:Button ID="btnFLow" runat="server" Text="审批进度" CssClass="btn1" Width="80px" OnClick="btnFLow_Click" />
                                        &nbsp;&nbsp;           
                                  <asp:Button ID="btnAdd" runat="server" CssClass="btnadd auth" Text="新增" OnClick="btnAdd_Click" />
                                          <asp:Button ID="btnDelete" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnDelete_Click" OnClientClick="javascript:return confirm('确认删除？');" />
                                        <asp:Button ID="btnModify" runat="server" CssClass="btnmodify auth" Text="保存" OnClick="btnModify_Click" />
                                      
                                        <asp:Button ID="btnUpdate" runat="server" CssClass="btnhide" OnClick="btnUpdate_Click" />
                                    </span>
                                </div>
                                <table class="tablelist">
                                    <tbody>
                                        <tr>
                                            <td width="100">检查标准名
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtName" runat="server" class="dfinput1"></asp:TextBox>
                                            </td>
                                            <td width="100">标准编码
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCode" runat="server" class="dfinput1" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td width="100">标准版本号
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtVersion" runat="server" class="dfinput1"></asp:TextBox>
                                            </td>
                                            <td width="100">编制人
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="listCreator" runat="server" CssClass="drpdwnlist"
                                                    Enabled="False">
                                                </asp:DropDownList>
                                            </td>

                                        </tr>
                                        <tr>   
                                            <td width="100">编制日期
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCrtDate" runat="server" class="dfinput1" Enabled="False"></asp:TextBox>
                                            </td>                                  
                                       
                                           
                                            <td width="100">是否有效
                                            </td>
                                            <td width="100">
                                                <asp:CheckBox ID="ckValid" runat="server" Text=" " />
                                            </td>
                                            <td width="100">描述
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDscpt" runat="server" class="dfinput1"></asp:TextBox>
                                            </td>
                                           
                                        </tr>
                                    </tbody>
                                </table>
                                <div class="listtitle">
                                    从标准
                        <asp:DropDownList ID="listtech" runat="server" CssClass="drpdwnlist">
                        </asp:DropDownList>
                                    复制为标准
                        <asp:DropDownList ID="listtechC" runat="server" CssClass="drpdwnlist">
                        </asp:DropDownList>
                                    <asp:Button ID="btnCopy" runat="server" CssClass="btnmodify auth" Text="复制" OnClick="btnCopy_Click" />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                                <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                                <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
                                <asp:AsyncPostBackTrigger ControlID="btnDelete" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                 <tr>
                    <td>
                           <div id="usual1" class="usual">
                <div class="itab">
                    <ul>
                        <li><a href="#tab1" id="tabtop1">理化检测</a></li>                      
                        <li><a href="#tab3" id="tabtop3">过程检测</a></li>
                    </ul>
                </div>
            </div>
            <div id="tab1" class="tabson">
                <div class="listtitle" style="margin-top: 10px">
                    检查项目列表 <span style="position: relative; float: right">

                        <asp:Button ID="btnGrid1CkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnGrid1CkAll_Click" />
                        <asp:Button ID="btnGrid1Save" runat="server" Text="保存" CssClass="btnmodify" OnClick="btnGrid1Save_Click" />
                        <asp:Button ID="btnGrid1DelSel" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnGrid1DelSel_Click" OnClientClick="javascript:return confirm('确认删除？');" /></span>
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" class="grid" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="14"
                                DataKeyNames="检查项目编码">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ck" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="分组">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="listGroup" runat="server" CssClass='drpdwnlist' Enabled="false" Width="200px">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="检查项目">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="listInspect" runat="server" CssClass='drpdwnlist' Enabled="false">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="下限">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtLower" runat="server" onkeyup="value=value.replace(/[^\d\|\-\.]/g,'')" CssClass='tbinput'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="上限">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUpper" runat="server" onkeyup="value=value.replace(/[^\d\|\-\.]/g,'')" CssClass='tbinput'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="单次扣分">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtScore" runat="server" onkeyup="value=value.replace(/[^\d\|\-\.]/g,'')" CssClass='tbinput'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="备注">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass='tbinput1'></asp:TextBox>
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

                            <asp:AsyncPostBackTrigger ControlID="btnGrid1CkAll" />
                            <asp:AsyncPostBackTrigger ControlID="btnGrid1Save" />
                            <asp:AsyncPostBackTrigger ControlID="btnGrid1DelSel" />
                             <asp:AsyncPostBackTrigger ControlID ="listVersion" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>            

            <div id="tab3" class="tabson">
                <div class="listtitle" style="margin-top: 10px">
                    检查项目列表 <span style="position: relative; float: right">

                        <asp:Button ID="btnGrid2CkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnGrid2CkAll_Click" />
                        <asp:Button ID="btnGrid2Save" runat="server" Text="保存" CssClass="btnmodify" OnClick="btnGrid2Save_Click" />
                        <asp:Button ID="btnGrid2DelSel" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnGrid2DelSel_Click" OnClientClick="javascript:return confirm('确认删除？');" /></span>
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView2" runat="server" class="grid" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="GridView2_PageIndexChanging" PageSize="14"
                                DataKeyNames="检查项目编码">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ck" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="分组">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="listGroup" runat="server" CssClass='drpdwnlist' Enabled="false" Width="200px">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="检查项目">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="listInspect" runat="server" CssClass='drpdwnlist' Enabled="false">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="下限">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtLower" runat="server" onkeyup="value=value.replace(/[^\d\|\-\.]/g,'')" CssClass='tbinput'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="上限">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUpper" runat="server" onkeyup="value=value.replace(/[^\d\|\-\.]/g,'')" CssClass='tbinput'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="单次扣分">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtScore" runat="server" onkeyup="value=value.replace(/[^\d\|\-\.]/g,'')" CssClass='tbinput'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="备注">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass='tbinput1'></asp:TextBox>
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

                            <asp:AsyncPostBackTrigger ControlID="btnGrid2CkAll" />
                            <asp:AsyncPostBackTrigger ControlID="btnGrid2Save" />
                            <asp:AsyncPostBackTrigger ControlID="btnGrid2DelSel" />
                             <asp:AsyncPostBackTrigger ControlID ="listVersion" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
                    </td>
                </tr>
            </table>
        </div>
        <!--mainright end-->
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
                        <asp:AsyncPostBackTrigger ControlID="btnFlow" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <script type="text/javascript">
            $('.tablelist tbody tr:odd').addClass('odd');
            $("#usual1 ul").idTabs();
        </script>
    </form>
</body>
</html>
