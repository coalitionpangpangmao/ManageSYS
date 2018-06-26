<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Materia.aspx.cs" Inherits="Craft_Materia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>产品管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/select.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript">      
        function GridClick(code) {
            window.parent.tab2Click(code);        
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="framelist">
        <div class="listtitle">
            类型查询与维护<span style="position: relative; float: right">
                <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" OnClick="btnSearch_Click" />
                <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" CssClass="btnhide" />
                <asp:HiddenField ID="hdcode" runat="server" />
            </span>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="tablelist">
                        <tbody>
                            <tr>
                                <td width="100">
                                    分类名称
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                                <td width="100">
                                    分类编码
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCode" runat="server" class="dfinput1" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="100">
                                    父级分类
                                </td>
                                <td>
                                    <asp:DropDownList ID="listPrt" runat="server" CssClass="drpdwnlist">
                                    </asp:DropDownList>
                                </td>
                                <td width="100">
                                    是否有效
                                </td>
                                <td>
                                    <asp:CheckBox ID="ckValid" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                    <asp:AsyncPostBackTrigger ControlID = "btnAdd" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
      <div class="gridtools  auth">               
                    <asp:Button ID="btnAdd" CssClass="btnadd auth" runat="server" OnClick="btnAdd_Click"
                        Text="新增" />
                    &nbsp; &nbsp;
                    <asp:Button ID="btnModify" CssClass="btnview  auth" runat="server" OnClick="btnModify_Click"
                        Text="保存" />
                    &nbsp; &nbsp;
                    <asp:Button ID="btnDel" CssClass="btndel  auth" runat="server" Text="删除" OnClick="btnDel_Click" />               
            </div>
        <div class="listtitle" style="margin-top: 10px">
            物料列表</div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="物料编码" AllowPaging="True">
                        <Columns>
                         <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button  ID="btnGridDel" runat="server" Text="删除" CssClass = "btn1" Width = "90px"  OnClick = "btnGridDel_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button  ID="btnDetail" runat="server" Text="物料详情" CssClass = "btn1" Width = "90px"  OnClick = "btnDetail_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="gridheader" />
                        <RowStyle CssClass="gridrow" />
                        <AlternatingRowStyle CssClass="gridalterrow" />
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                    <asp:AsyncPostBackTrigger ControlID="btnModify" />
                    <asp:AsyncPostBackTrigger ControlID="btnDel" />
                    <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
  
    <script type="text/javascript">
        $('.tablelist tbody tr:odd').addClass('odd');
    </script>
    </form>
</body>
</html>
