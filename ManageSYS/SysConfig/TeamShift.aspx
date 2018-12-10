<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TeamShift.aspx.cs" Inherits="SysConfig_TeamShift" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>班组班时配置</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        function GridClick(code) {

            $('#tabtop2').click();
            $('#hdcode').attr('value', code.substr(4));
            $('#btnUpdate').click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="#">系统配置</a></li>
            <li><a href="#">班组班时配置</a></li>
        </ul>
    </div>
    <div class="formbody">
        <div id="usual1" class="usual">
            <div class="itab">
                <ul>
                    <li><a href="#tab1" class="selected" id="tabtop1">班组配置</a></li>
                    <li><a href="#tab2" id="tabtop2">班时配置</a></li>
                </ul>
            </div>
        </div>
        <div id="tab1" class="tabson">          
                <div class="listtitle">
                    编辑<span style="position: relative; float: right">
                        <asp:Button ID="btnSaveT" runat="server" Text="保存" CssClass="btnmodify  auth" OnClick="btnSaveT_Click" />
                    </span>
                </div>
                  <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                <table class="tablelist">
                    <tbody>
                        <tr>
                            <td width="100">
                                班组名称
                            </td>
                            <td>
                                <asp:TextBox ID="txtNameT" runat="server" CssClass="dfinput1"></asp:TextBox>
                            </td>
                            <td width="100">
                                班组编码
                            </td>
                            <td>
                                <asp:TextBox ID="txtCodeT" runat="server" class="dfinput1 hintfont" Text="请输入两位顺序编码XX"
                                    onmousedown="if('请输入两位顺序编码XX' == this.value){ this.value = ''}" onblur="if('' == this.value){this.className = 'dfinput1 hintfont';this.value ='请输入两位顺序编码XX';}else{this.className = 'dfinput1 showfont';} "></asp:TextBox>
                            </td>
                            <td width="100">
                                车间
                            </td>
                            <td width="100">
                                <asp:DropDownList ID="listLineT" runat="server" CssClass="drpdwnlist">
                                    <asp:ListItem Value="703">再造梗丝生产线</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="listtitle">
                    列表
                </div>
              
                            <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="班组编码" AllowPaging="True">
                                <Columns>
                                    <asp:TemplateField     >
                                        <ItemTemplate>
                                            <asp:Button ID="btnGrid1Del" runat="server" Text="删除" CssClass="btn1  auth" OnClick="btnGrid1Del_Click"  OnClientClick="javascript:return confirm('确认删除？');"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField     >
                                        <ItemTemplate>
                                            <asp:Button ID="btnGrid1Edit" runat="server" Text="编辑" CssClass="btn1  auth" OnClick="btnGrid1Edit_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridheader" />
                                <RowStyle CssClass="gridrow" />
                                <AlternatingRowStyle CssClass="gridalterrow" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSaveT" />
                            <asp:AsyncPostBackTrigger ControlID="GridView1" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
           
        </div>
        <div id="tab2" class="tabson">
          
                <div class="listtitle">
                    编辑<span style="position: relative; float: right">
                        <asp:Button ID="btnSaveS" runat="server" Text="保存" CssClass="btnmodify auth" OnClick="btnSaveS_Click" />
                    </span>
                </div>
                  <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                <table class="tablelist">
                    <tbody>
                        <tr>
                            <td width="100">
                                班时名称
                            </td>
                            <td>
                                <asp:TextBox ID="txtNameS" runat="server" class="dfinput1"></asp:TextBox>
                            </td>
                            <td width="100">
                                班时编码
                            </td>
                            <td>
                                <asp:TextBox ID="txtCodeS" runat="server" class="dfinput1 hintfont" Text="请输入两位顺序编码XX"
                                    onmousedown="if('请输入两位顺序编码XX' == this.value){ this.value = ''}" onblur="if('' == this.value){this.className = 'dfinput1 hintfont';this.value ='请输入两位顺序编码XX';}else{this.className = 'dfinput1 showfont';} "></asp:TextBox>
                            </td>
                            <td width="100">
                                车间
                            </td>
                            <td width="100">
                                <asp:DropDownList ID="listLineS" runat="server" CssClass="drpdwnlist">
                                    <asp:ListItem Value="703">再造梗丝生产线</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="100">
                                开始时间
                            </td>
                            <td>
                                <asp:TextBox ID="txtStarttime" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'HH:mm:ss'})"></asp:TextBox>
                            </td>
                            <td width="100">
                                结束时间
                            </td>
                            <td>
                                <asp:TextBox ID="txtEndtime" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'HH:mm:ss'})"></asp:TextBox>
                            </td>
                            <td width="100">
                                是否跨天
                            </td>
                            <td width="100">
                                <asp:CheckBox ID="ckInter" runat="server" Text=" " />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="listtitle">
                    列表
                </div>
              
                            <asp:GridView ID="GridView2" runat="server" class="grid" DataKeyNames="班时编码" AllowPaging="True">
                                <Columns>
                                    <asp:TemplateField     >
                                        <ItemTemplate>
                                            <asp:Button ID="btnGrid2Del" runat="server" Text="删除" CssClass="btn1  auth" OnClick="btnGrid2Del_Click" OnClientClick="javascript:return confirm('确认删除？');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField     >
                                        <ItemTemplate>
                                            <asp:Button ID="btnGrid2Edit" runat="server" Text="编辑" CssClass="btn1  auth" OnClick="btnGrid2Edit_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridheader" />
                                <RowStyle CssClass="gridrow" />
                                <AlternatingRowStyle CssClass="gridalterrow" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSaveS" />
                            <asp:AsyncPostBackTrigger ControlID="GridView2" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
          
        </div>
    </div>
    <script type="text/javascript">
        $("#usual1 ul").idTabs(); 
    </script>
    <script type="text/javascript">
        $('.tablelist tbody tr:odd').addClass('odd');
    </script>
    </form>
</body>
</html>
