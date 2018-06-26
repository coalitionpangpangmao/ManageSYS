<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tech_Equip.aspx.cs" Inherits="Device_EquipmentInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1. 0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>工艺模型</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
  
  
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>   
       <asp:HiddenField ID="hdcode" runat="server" />         
        <asp:Button ID="btnUpdate" runat="server"  CssClass = "btnhide"  OnClick = "btnUpdate_Click"/>      
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
                                            设备名称
                                        </td>
                                        <td>
                                       <asp:DropDownList ID="listEquip"  runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                                           </td>
                                     
                                     
                                    </tr>
                                    <tr>
                                       <td width="100">
                                            备注
                                        </td>
                                        <td colspan="3" width="500px">
                                            <asp:TextBox ID="txtDscpt" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>                       
                    </ContentTemplate>
                    <Triggers>  
                        <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                       
                    </Triggers>
                </asp:UpdatePanel>
       
        </div>
    <script type="text/javascript">
        $('.tablelist tbody tr:odd').addClass('odd');
    </script>
    </form>
</body>
</html>
