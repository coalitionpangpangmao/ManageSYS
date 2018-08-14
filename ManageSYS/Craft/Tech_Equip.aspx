<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tech_Equip.aspx.cs" Inherits="Device_EquipmentInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1. 0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">   
    <title>工艺模型</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>  
  
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>  
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
        <div class="framelist">
       
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>                      
                            <table class="tablelist">
                                <tbody>
                                    <tr>
                                       <td>
                                            工艺段
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listSection" runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                       <td width="100">
                                            设备分类
                                        </td>
                                        <td>
                                       <asp:DropDownList ID="listSort"  runat="server" CssClass="drpdwnlist">
                                            <asp:ListItem Value="01">生产设备</asp:ListItem>
                                            </asp:DropDownList>
                                           </td>
                                     
                                     
                                    </tr>
                                    <tr>
                                          <td width="100">
                                            设备编码
                                        </td>
                                        <td>
                                        <asp:TextBox ID="txtCode" runat="server" class="dfinput1" Enabled="False" ></asp:TextBox>
                                           </td>
                                     
                                          <td width="100">
                                            设备名称
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtName" runat="server" class="dfinput1" ></asp:TextBox>
                                           </td>
                                     </tr>
                                    <tr>
                                       <td width="100">
                                            控制设备
                                        </td>
                                        <td >
                                            <asp:CheckBox ID="ckCtrl" runat="server"  Text=""/>
                                        </td>
                                   
                                       <td width="100">
                                            备注
                                        </td>
                                        <td >
                                            <asp:TextBox ID="txtDscpt" runat="server" class="dfinput1" ></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>                       
                    </ContentTemplate>
                    <Triggers>  
                        <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                       <asp:AsyncPostBackTrigger ControlID ="btnAdd" />
                          <asp:AsyncPostBackTrigger ControlID="btnModify" />
                       <asp:AsyncPostBackTrigger ControlID ="btnDel" />
                    </Triggers>
                </asp:UpdatePanel>
       
        </div>
    <script type="text/javascript">
        $('.tablelist tbody tr:odd').addClass('odd');
    </script>
    </form>
</body>
</html>
