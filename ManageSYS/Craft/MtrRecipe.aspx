<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MtrRecipe.aspx.cs" Inherits="Craft_MtrRecipe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
     <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>   
    <script type="text/javascript">
        function treeClick(code) {
            $("#hdcode").val(code);
            $("#btnUpdate").click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table class="framelist" >
        <tr>
            <td>
                <div>
                    <div class="listtitle">
                        配方信息<span style="position: relative; float: right">
                            <asp:Button ID="btnAddR" class="btnadd  auth" runat="server" Text="新增" OnClick="btnAddR_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnModify" class="btnmodify  auth" runat="server" Text="保存" OnClick="btnModify_Click" />&nbsp;
                            <asp:HiddenField ID="hdcode" runat="server" />
                            <asp:Button ID="btnUpdate" runat="server" Text="Button" CssClass="btnhide" OnClick="btnUpdate_Click" />
                        </span>
                    </div>
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table class="tablelist">
                                    <tbody>
                                        <tr>
                                            <td width="100">
                                                配方名称
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtName" runat="server" class="dfinput1"></asp:TextBox>
                                            </td>
                                            <td width="100">
                                                配方编码
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCode" runat="server" class="dfinput1" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td width="100">
                                                产品编码
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="listPro" runat="server" CssClass="drpdwnlist">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="100">
                                                标准版本号
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtVersion" runat="server" class="dfinput1"></asp:TextBox>
                                            </td>
                                            <td width="100">
                                                执行日期
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtExeDate" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                            </td>
                                            <td width="100">
                                                结束日期
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEndDate" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="100">
                                                受控状态
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="listStatus" runat="server" CssClass="drpdwnlist">
                                                </asp:DropDownList>
                                            </td>
                                             <td width="100">
                                                    编制人
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="listCreator" runat="server" CssClass="drpdwnlist" 
                                                        Enabled="False">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="100">
                                                    编制日期
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCrtDate" runat="server" class="dfinput1" 
                                                        onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100">
                                                    编制部门
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="listCrtApt" runat="server" CssClass="drpdwnlist" 
                                                        Enabled="False">
                                                    </asp:DropDownList>
                                                </td>
                                            <td width="100">
                                                是否有效
                                            </td>
                                            <td width="100">
                                                <asp:CheckBox ID="ckValid" runat="server" Text=" " />
                                            </td>
                                            <td width="100">
                                                描述
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDscpt" runat="server" class="dfinput1"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                                <asp:AsyncPostBackTrigger ControlID="btnAddR" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td >
                <div style="margin-top: 10px">
                    <div class="listtitle">
                        配方详情<span style="position: relative; float: right"><asp:Button ID="btnAdd" runat="server"
                            CssClass="btnadd  auth" Text="新增" OnClick="btnAdd_Click" />
                            <asp:Button ID="btnCkAll" runat="server" CssClass="btnset  auth" Text="全选" OnClick="btnCkAll_Click" />
                            <asp:Button ID="btnDelSel" runat="server" CssClass="btndel  auth" Text="删除" OnClick="btnDelSel_Click"  OnClientClick="javascript:return confirm('确认删除？');"/>
                              <asp:Button ID="btnGridSave" class="btnmodify  auth" runat="server" Text="全部保存" Width="90px" OnClick="btnGridSave_Click" />
                        </span>
                    </div>
                    <div >
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                              
                                <asp:GridView ID="GridView1" runat="server" class="grid" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="9">
                                    <Columns>
                                        <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="物料分类">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="listGridType" runat="server" CssClass="drpdwnlist" Width="80px" OnSelectedIndexChanged="listGridType_SelectedIndexChanged"
                                                    AutoPostBack="True" DataSource='<%# gridTypebind()%>' DataValueField="mattree_name"    DataTextField="mattree_name">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="物料名称" SortExpression="物料名称">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="listGridName" runat="server" CssClass="drpdwnlist" OnSelectedIndexChanged="listGirdName_SelectedIndexChanged"  AutoPostBack="True" Width ="300px" >
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="物料编码" SortExpression="物料编码">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCodeM" runat="server" DataValueField="物料编码" DataTextField="物料编码"
                                                    CssClass="tbinput1" Enabled="False"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="批投料量" SortExpression="批投料量">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAmountM" runat="server" DataValueField="批投料量" DataTextField="批投料量"  onkeyup="value=value.replace(/[^\d\.]/g,'')" 
                                                    CssClass="tbinput"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="优先组" SortExpression="优先组">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGroupM" runat="server" DataValueField="优先组" DataTextField="优先组"
                                                    CssClass="tbinput"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                            <ItemTemplate>
                                                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn1  auth" OnClick="btnSave_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                            <ItemTemplate>
                                                <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="btn1  auth" OnClick="btnDel_Click"  OnClientClick="javascript:return confirm('确认删除？');"/>
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
                                <asp:AsyncPostBackTrigger ControlID="btnCkAll" />
                                <asp:AsyncPostBackTrigger ControlID="btnModify" />
                                <asp:AsyncPostBackTrigger ControlID="GridView1" />
                                <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                                <asp:AsyncPostBackTrigger ControlID="btnGridSave" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
