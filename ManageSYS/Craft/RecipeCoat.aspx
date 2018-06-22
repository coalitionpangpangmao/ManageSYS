<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecipeCoat.aspx.cs" Inherits="Craft_RecipeCoat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
     <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>   
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
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
    <div class="framelist">
        <table class="tablelist">
            <tr>
                <td>
                    <div>
                        <div class="listtitle">
                            配方信息<span style="position: relative; float: right">
                                <asp:Button ID="btnAddR" class="btnadd  auth" runat="server" Text="新增" OnClick="btnAddR_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnModify" class="btnmodify auth" runat="server" Text="保存" OnClick="btnModify_Click" />&nbsp;
                                <asp:HiddenField ID="hdcode" runat="server" />
                                <asp:Button ID="btnUpdate" runat="server" Text="Button" CssClass="btnhide" OnClick="btnUpdate_Click" />
                            </span>
                        </div>
                        <div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
        </table>
        <div id="usual1" class="usual">
            <div class="itab">
                <ul>
                    <li><a href="#tab1" id="tabtop1">香精香料</a></li>
                    <li><a href="#tab2" id="tabtop2">回填液</a></li>
                </ul>
            </div>
        </div>
        <div id="tab1" class="tabson">
            <div class="listtitle">
                配方详情<span style="position: relative; float: right"><asp:Button ID="btnAdd" runat="server"
                    CssClass="btnadd  auth" Text="新增" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnCkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnCkAll_Click" />
                    <asp:Button ID="btnDelSel" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnDelSel_Click" />
                </span>
            </div>
            <div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" class="grid" AllowPaging="True" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="种类">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCodeM" runat="server" DataValueField="种类" DataTextField="种类"
                                            CssClass="tbinput1" Enabled="False"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="比例%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtScale" runat="server" DataValueField="比例" DataTextField="比例"
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="每罐调配所需">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPercent" runat="server" DataValueField="每罐调配所需" DataTextField="每罐调配所需"
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn1 auth" OnClick="btnSave_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="btn1 auth" OnClick="btnDel_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                        <asp:AsyncPostBackTrigger ControlID="btnCkAll" />
                        <asp:AsyncPostBackTrigger ControlID="btnDelSel" />
                        <asp:AsyncPostBackTrigger ControlID="btnModify" />
                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
                        <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="tab2" class="tabson">
            <div class="listtitle">
                配方详情<span style="position: relative; float: right"><asp:Button ID="btnAdd2" runat="server"
                    CssClass="btnadd  auth" Text="新增" OnClick="btnAdd2_Click" />
                    <asp:Button ID="btnCkAll2" runat="server" CssClass="btnset" Text="全选" OnClick="btnCkAll2_Click" />
                    <asp:Button ID="btnDelSel2" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnDelSel2_Click" />
                </span>
            </div>
            <div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView2" runat="server" class="grid" AllowPaging="True" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="种类">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCodeM2" runat="server" DataValueField="种类" DataTextField="种类"
                                            CssClass="tbinput1" Enabled="False"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="比例%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtScale2" runat="server" DataValueField="比例" DataTextField="比例"
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="备注">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemark" runat="server" DataValueField="每罐调配所需" DataTextField="每罐调配所需"
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn1 auth" OnClick="btnSave_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="btn1 auth" OnClick="btnDel_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAdd2" />
                        <asp:AsyncPostBackTrigger ControlID="btnCkAll2" />
                        <asp:AsyncPostBackTrigger ControlID="btnModify" />
                        <asp:AsyncPostBackTrigger ControlID="btnDelSel2" />
                        <asp:AsyncPostBackTrigger ControlID="GridView2" />
                        <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $("#usual1 ul").idTabs(); 
    </script>
    </form>
</body>
</html>
