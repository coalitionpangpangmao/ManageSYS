<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserInfo.aspx.cs" Inherits="Authority_UserInfo"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户信息</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript">
    $().ready(function () {  $("#gridPanel").scrollTop = $("#hdScrollY").val(); });
     function saveScroll() {       
         var y = $("#gridPanel").scrollTop();
         $("#hdScrollY").val(y);
     }
     </script>
</head>
<body>
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="#">用户信息</a></li>          
        </ul>
    </div>
    <div class="rightinfo">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table class="tablelist">
                                <tbody>
                                    <tr>
                                        <td width="120">
                                            用户ID
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtID" runat="server" class="dfinput1" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td width="120">
                                            用户名
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtName" runat="server" class="dfinput1" Enabled="False"></asp:TextBox>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        
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
                                    </tr>
                                    <tr>
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
                                            <asp:DropDownList ID="listApt" runat="server" CssClass="drpdwnlist" 
                                                Enabled="False">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="120">
                                            角色
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listRole" runat="server" CssClass="drpdwnlist" 
                                                Enabled="False">
                                            </asp:DropDownList>
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
                                           个人说明
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtDscp" runat="server" class="dfinput1" Width="600px" 
                                                Height="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                
                                </tbody>
                            </table>
                            <div class = "shadebtn" align="center">
                            
                               <asp:Button ID="btnModify" class="sure" runat="server" Text="保存" OnClick="btnModify_Click" />
             </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnModify" />                        
                        </Triggers>
                    </asp:UpdatePanel>
</div>
    <script type="text/javascript">
        $('.tablelist tbody tr:odd').addClass('odd');
    </script>
    </form>
</body>
</html>
