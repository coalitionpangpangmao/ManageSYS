<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tech_Session.aspx.cs" Inherits="Craft_Tech_Session" %>

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
    <div   class="framelist">
    <div class="gridtools  auth">               
                    <asp:Button ID="btnAdd" CssClass="btnadd auth" runat="server" OnClick="btnAdd_Click"
                        Text="新增" />
                    &nbsp; &nbsp;
                    <asp:Button ID="btnModify" CssClass="btnview  auth" runat="server" OnClick="btnModify_Click"
                        Text="保存" />
                    &nbsp; &nbsp;
                    <asp:Button ID="btnDel" CssClass="btndel  auth" runat="server" Text="删除" OnClick="btnDel_Click" />       
                     <asp:HiddenField ID="hdcode" runat="server" />         
        <asp:Button ID="btnUpdate" runat="server"  CssClass = "btnhide"  OnClick = "btnUpdate_Click"/>      
            </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <table class="tablelist">    	
        <tbody>
        <tr>
        <td width="100">工艺段编码</td>
        <td><asp:TextBox ID="txtCode" runat="server" class="dfinput1" Enabled="False"    ></asp:TextBox></td>
        <td  width="100">名称</td>
        <td><asp:TextBox ID="txtName" runat="server" class="dfinput1"    ></asp:TextBox></td>
        </tr> 
        <tr>
        <td width="100">备注</td>
        <td><asp:TextBox ID="txtDscrp" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td  width="100">是否有效</td>
        <td title=" ">
            <asp:CheckBox ID="rdValid" runat="server" Text=" " />
       </td>
      
        </tr>
        </tbody>
        </table>
        </ContentTemplate>
        <Triggers>
        <asp:AsyncPostBackTrigger ControlID = "btnAdd" />
        <asp:AsyncPostBackTrigger ControlID = "btnUpdate" />
        <asp:AsyncPostBackTrigger ControlID = "btnDel" />
        <asp:AsyncPostBackTrigger ControlID = "btnModify" />
        </Triggers>
          </asp:UpdatePanel>
        </div>
      
    </form>
</body>
</html>
