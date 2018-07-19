<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserConfig.aspx.cs" Inherits="Authority_UserConfig"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>人员管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    
</head>
<body>
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="#">系统管理</a></li>
            <li><a href="#">用户管理</a></li>
        </ul>
    </div>
    <div class="rightinfo">
    <div class="gridtools  auth">    
    	<ul class="toolbar">
      <asp:Button ID="btnAdd" CssClass = "btnadd auth" runat = "server" OnClick="btnAdd_Click" Text="添加" /> 
            &nbsp; &nbsp;
       <asp:Button ID="btnView" CssClass = "btnview  auth" runat = "server" OnClick="btnView_Click" Text="查看" />
          &nbsp; &nbsp;
       <asp:Button ID="btnDel" CssClass="btndel  auth" runat="server" Text="删除" OnClick="btnDel_Click" />
                     
        </ul>
     </div>
        <div id = "gridPanel" >
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" CssClass="grid" DataKeyNames = "人员ID" AllowPaging ="true"  OnPageIndexChanging ="GridView1_PageIndexChanging" PagerSettings-FirstPageText="第一页" PagerSettings-LastPageText="最后页" PagerSettings-Mode="NumericFirstLast" PageSize="12">
                    <Columns>
                    <asp:TemplateField>
                    <HeaderTemplate>
                     <asp:CheckBox ID="headck" runat="server" OnCheckedChanged = "ck_CheckedChanged" AutoPostBack="True" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="ck" runat="server" />
                    </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
                        <HeaderStyle CssClass="gridheader" />
                        <RowStyle CssClass="gridrow" />
                         <AlternatingRowStyle CssClass="gridalterrow" />
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnView" />
                    <asp:AsyncPostBackTrigger ControlID="btnModify" />
                    <asp:AsyncPostBackTrigger ControlID="btnDel" />
                    <asp:AsyncPostBackTrigger ControlID = "GridView1" />
                    <asp:AsyncPostBackTrigger ControlID = 'btnAdd' />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="shade">
            <div class="info">
                <div class="tiphead">
                    <span>员工信息</span><a  onclick = "$('.shade').fadeOut(100);"></a>
                    </div>
                <div class="gridinfo">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table class="tablelist">
                                <tbody>
                                    <tr>
                                        <td width="120">
                                            人员ID
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtID" runat="server" class="dfinput1" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td width="120">
                                            人员名称
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtName" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                        <td width="120">
                                            权重
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWeight" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="120">
                                            父级标识
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPrt" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                        <td width="120">
                                            手机
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPhone" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                        <td width="120">
                                            座机
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCallNO" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="120">
                                            传真
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFax" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                        <td width="120">
                                            性别
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rdMale" runat="server" Text="男" GroupName="rdGender" Checked="True" />
                                            <asp:RadioButton ID="rdFemale" runat="server" Text="女" GroupName="rdGender" />
                                        </td>
                                        <td width="120">
                                            登陆名
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUser" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="120">
                                            密码（加密）
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPswd" runat="server" class="dfinput1" TextMode="Password"></asp:TextBox>
                                        </td>
                                        <td width="120">
                                            电子邮件
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEmail" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                        <td width="120">
                                            组织机构名称
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listApt" runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="120">
                                            是否本地
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rdLocal" runat="server" Text=" " />
                                        </td>
                                        <td width="120">
                                            是否同步
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rdAsyn" runat="server" Text=" " />
                                        </td>
                                        <td width="120">
                                            删除标识
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rdDel" runat="server" Text=" " />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="120">
                                            角色
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listRole" runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="120">
                                            描述
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtDscp" runat="server" class="dfinput1" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                
                                </tbody>
                            </table>
                            <div class = "shadebtn" align="center">                              
                               <asp:Button ID="btnModify" class="sure" runat="server" Text="保存" OnClick="btnModify_Click" />
                <input name="" type="button" class="cancel" value="关闭" onclick = "$('.shade').fadeOut(100);"/></div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnView" />
                            <asp:AsyncPostBackTrigger ControlID="GridView1" />
                            <asp:AsyncPostBackTrigger ControlID = "btnAdd" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
          
        </div>
    </div>
    <script type="text/javascript">
        $('.tablelist tbody tr:odd').addClass('odd');
    </script>
    </form>
</body>
</html>
