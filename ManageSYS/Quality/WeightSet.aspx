<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WeightSet.aspx.cs" Inherits="Quality_WeightSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>质量管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
  
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="#">质量管理</a></li>
            <li><a href="#">权重设置</a></li>
        </ul>
    </div>
    <div class="formbody">
        <div id="usual1" class="usual">
            <div class="itab">
                <ul>
                    <li><a href="#tab1" class="selected" id="tabtop1">工艺段权重</a></li>
                    <li><a href="#tab2" id="tabtop2">月度考核权重</a></li>
                </ul>
            </div>
        </div>
        <div id="tab1" class="tabson">
            <div class="framelist"> 
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" class="grid" AllowPaging="True" AutoGenerateColumns="False"
                            DataKeyNames="检查项目编码">
                            <Columns>
                                <asp:TemplateField HeaderText = "工艺段">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="listSection" runat="server" CssClass = 'drpdwnlist'  DataSource = "<%#bindInspect() %>"  DataTextField = "INSPECT_TYPE"  DataValueField = "ID">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText = "权重">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtUpper" runat="server" CssClass = 'tbinput1'></asp:TextBox>  
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText = "备注">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemark" runat="server" CssClass = 'tbinput1'></asp:TextBox>  
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn1" OnClick="btnSave_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                            <AlternatingRowStyle CssClass="gridalterrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>                      
                        <asp:AsyncPostBackTrigger ControlID="GridView1" />                     
                    </Triggers>
                </asp:UpdatePanel>
              
        </div>
        <div id="tab2" class="tabson">
           <div class="framelist"> 
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table class = "tablelist">
                        <thead>
                        <td>考核项</td>
                        <td>权重</td>
                         <td>备注</td>
                        <td>操作</td>
                        </thead>
                        <tr>
                        <td>月度在线评测得分</td>
                         <td>
                             <asp:TextBox ID="txtWeight1" runat="server" CssClass = 'dfinput1'></asp:TextBox>
                         </td>
                           <td>
                             <asp:TextBox ID="txtRemark1" runat="server" CssClass = 'dfinput1'></asp:TextBox>
                         </td>
                          <td>
                              <asp:Button ID="btnSave1" runat="server" Text="保存"  CssClass = 'btn1'/>
                          </td>
                        </tr>
                        <tr>
                        <td>成品理化指标得分</td>
                         <td>
                             <asp:TextBox ID="txtWeight2" runat="server" CssClass = 'dfinput1'></asp:TextBox>
                         </td>
                           <td>
                             <asp:TextBox ID="txtRemark2" runat="server" CssClass = 'dfinput1'></asp:TextBox>
                         </td>
                          <td>
                              <asp:Button ID="btnSave2" runat="server" Text="保存"  CssClass = 'btn1'/>
                          </td>
                        </tr>
                        <tr>
                        <td>感官评测得分</td>
                         <td>
                             <asp:TextBox ID="txtWeight3" runat="server" CssClass = 'dfinput1'></asp:TextBox>
                         </td>
                           <td>
                             <asp:TextBox ID="txtRemark3" runat="server" CssClass = 'dfinput1'></asp:TextBox>
                         </td>
                          <td>
                              <asp:Button ID="btnSave3" runat="server" Text="保存"  CssClass = 'btn1'/>
                          </td>
                        </tr>
                        </table>
                    </ContentTemplate>
                  
                </asp:UpdatePanel>
             
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
