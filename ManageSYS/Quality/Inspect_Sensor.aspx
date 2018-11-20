<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Inspect_Sensor.aspx.cs"
    Inherits="Quality_Inspect_Sensor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>感观评测</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
      <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>  
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">质量管理</a></li>
                <li><a href="#">感观评测</a></li>
            </ul>
        </div>
        <div class="formbody">
            <div class="listtitle">
                检验详情 <span style="position: relative; float: right">
                        <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btnview auth" OnClick="btnSave_Click" />
                    </span>
            </div>
            <table class="tablelist">
                <tbody>
                    <tr>
                           <td width="100">生产日期：
                        </td>
                        <td>
                            <asp:TextBox ID="txtProdTime" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" OnTextChanged="txtProdTime_TextChanged" AutoPostBack="True"></asp:TextBox>  
                        </td>
                        <td width="100">产品：
                        </td>
                        <td>
                            <asp:DropDownList ID="listProd" runat="server" CssClass="drpdwnlist" OnSelectedIndexChanged ="listProd_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>  <td width="100">记录人：
                        </td>
                        <td>
                            <asp:DropDownList ID="listEditor" runat="server" CssClass="drpdwnlist">
                            </asp:DropDownList>
                        </td>
                       
                        </tr>
                    <tr>
                       <td width="100">班组：
                        </td>
                        <td>
                            <asp:DropDownList ID="listTeam" runat="server" CssClass="drpdwnlist" OnSelectedIndexChanged="listTeam_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                         <td width="100">班时：
                        </td>
                        <td>
                            <asp:DropDownList ID="listShift" runat="server" CssClass="drpdwnlist" Enabled="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" class="grid" AllowPaging="True" AutoGenerateColumns="False"
                            DataKeyNames="inspect_code">
                            <Columns>
                            
                                <asp:BoundField HeaderText="检验项目" DataField="inspect_name"/>
                                <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="检测值">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPara" runat="server" CssClass='tbinput1'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="标准范围">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtValue" runat="server" CssClass='tbinput1' Enabled="False"></asp:TextBox>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                  <asp:BoundField DataField="unit" HeaderText="单位" />
                                <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="状态">
                                    <ItemTemplate>
                                        <asp:Label ID="labStatus" runat="server" Text="" CssClass="labstatu" Width ="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="扣分">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtScore" runat="server" CssClass='tbinput1' Enabled="False"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                             
                            </Columns>
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                            <AlternatingRowStyle CssClass="gridalterrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="listProd" />
                        <asp:AsyncPostBackTrigger ControlID ="btnSave" />
                         <asp:AsyncPostBackTrigger ControlID ="txtProdTime" />
                        <asp:AsyncPostBackTrigger ControlID ="listTeam" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
</body>
</html>
