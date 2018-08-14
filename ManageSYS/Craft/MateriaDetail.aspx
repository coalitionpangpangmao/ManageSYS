<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MateriaDetail.aspx.cs" Inherits="Craft_MateriaDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="framelist">
            <div class="gridtools  auth">
                <asp:Button ID="btnAdd" CssClass="btnadd auth" runat="server" OnClick="btnAdd_Click"
                    Text="新增" />
                &nbsp; &nbsp;
                    <asp:Button ID="btnModify" CssClass="btnview  auth" runat="server" OnClick="btnModify_Click"
                        Text="保存" />
                &nbsp; &nbsp;
                    <asp:Button ID="btnDel" CssClass="btndel  auth" runat="server" Text="删除" OnClick="btnDel_Click" />
                <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" CssClass="btnhide" />
                <asp:HiddenField ID="hdcode" runat="server" />
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="tablelist">
                        <tbody>
                            <tr>
                                <td width="100">物料编码</td>
                                <td>
                                    <asp:TextBox ID="txtCode" runat="server" class="dfinput1"></asp:TextBox></td>
                                <td width="100">物料名称</td>
                                <!-- code name-->
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" class="dfinput1"></asp:TextBox></td>
                                <td width="100">物料类别</td>
                                <td>
                                    <asp:DropDownList ID="listType" runat="server" CssClass="drpdwnlist"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td width="100">物料类型</td>
                                <td>
                                    <asp:TextBox ID="txtCtgr" runat="server" class="dfinput1"></asp:TextBox></td>
                                <td width="100">单位</td>
                                <td>
                                    <asp:TextBox ID="txtUint" runat="server" class="dfinput1"></asp:TextBox></td>
                                <td width="100">用友编码</td>
                                <td>
                                    <asp:TextBox ID="txtPkmtr" runat="server" class="dfinput1"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td width="100">等级</td>
                                <td>
                                    <asp:TextBox ID="txtLevel" runat="server" class="dfinput1"></asp:TextBox></td>
                                <td width="100">种类</td>
                                <td>
                                    <asp:TextBox ID="txtVrt" runat="server" class="dfinput1"></asp:TextBox></td>
                                <td width="100">单重</td>
                                <td>
                                    <asp:TextBox ID="txtWeight" runat="server" class="dfinput1"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td width="100">是否有效</td>
                                <td>
                                    <asp:CheckBox ID="ckValid" runat="server" /></td>
                                <td width="100">备注</td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtDscrp" runat="server" class="dfinput1"></asp:TextBox></td>

                            </tr>
                        </tbody>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                    <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                    <asp:AsyncPostBackTrigger ControlID="btnDel" />
                    <asp:AsyncPostBackTrigger ControlID="btnModify" />
                </Triggers>
            </asp:UpdatePanel>
        </div>




    </form>
</body>
</html>
