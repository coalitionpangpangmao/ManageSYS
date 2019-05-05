<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProdLinePath.aspx.cs" Inherits="Craft_Prdct" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>全线路径管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
   

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">工艺管理</a></li>
                <li><a href="#">全线路径管理</a></li>
            </ul>
        </div>
        <div class="formbody">
            <div>
              <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
            <div class="listtitle">
                路径名称：
                <asp:DropDownList ID="listPathAll" runat="server" CssClass="drpdwnlist" Width ="300px" AutoPostBack="true" OnSelectedIndexChanged="listPathAll_SelectedIndexChanged"></asp:DropDownList> 
                  <span style="position: relative; float: right" class="btnhide">
                      审批状态：<asp:DropDownList ID="listAprv" runat="server" CssClass="drpdwnlist" Width="80px" Enabled="False"></asp:DropDownList> 
                 &nbsp;    &nbsp;  <asp:Button ID="btnSubmit" runat="server" Text="提交审批" CssClass="btnpatch"  OnClick="btnSubmit_Click" Width ="100px" /> &nbsp;    &nbsp;   
                    <asp:Button ID="btnFLow" runat="server" Text="审批进度" CssClass="btnview"  OnClick="btnFLow_Click"  Width ="100px"/> &nbsp;    &nbsp;   
              
                      </span>
                 </div>
                  </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="listPathAll" />                    
                    <asp:AsyncPostBackTrigger ControlID ="btnSave" />
                    <asp:AsyncPostBackTrigger ControlID ="btnDel" />    
                    <asp:AsyncPostBackTrigger ControlID ="btnSubmit" />             
                </Triggers>
            </asp:UpdatePanel>          

            </div>
             
           
            <div>
              <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
             <table class="tablelist">
                    <tbody>
                        <tr style="height: 50px">
                            <td width="100">路径名称
                            </td>
                            <td>
                                <asp:TextBox ID="txtNameS" runat="server" class="dfinput1" Width ="250px"></asp:TextBox>
                            </td>
                            <td width="100">路径编码
                            </td>
                            <td>
                                <asp:TextBox ID="txtCodeS" runat="server" class="dfinput1" Width ="250px"></asp:TextBox>
                            </td>                           
                            <td >

                                <asp:Button ID="btnSave" CssClass="btnmodify" runat="server" OnClick="btnSave_Click"
                        Text="保存" />

                                  <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="btndel" OnClick="btnDel_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>

           
                    <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="section_code"
                        AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="工艺段">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSection" runat="server" DataValueField="工艺段" DataTextField="工艺段"
                                        CssClass="tbinput" Enabled="False" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="路径选择">
                                <ItemTemplate>
                                    <asp:DropDownList ID="listpath" runat="server" CssClass="drpdwnlist" Width="250px" 
                                        OnSelectedIndexChanged="listpath_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="路径详情">
                                <ItemTemplate>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <HeaderStyle CssClass="gridheader" />
                        <RowStyle CssClass="gridrow" />
                        <AlternatingRowStyle CssClass="gridalterrow" />
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="listPathAll" />  
                    <asp:AsyncPostBackTrigger ControlID ="GridView1" />                    
                    <asp:AsyncPostBackTrigger ControlID ="btnDel" />   
                         
                </Triggers>
            </asp:UpdatePanel>          

            </div>
        </div>

        <div class="aprvinfo" id="flowinfo">
                <div class="tiptop">
                    <span>审批流程详情</span><a onclick="$('#flowinfo').fadeOut(100);"></a>
                </div>
                <div class="flowinfo">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView3" runat="server" class="grid">
                                <HeaderStyle CssClass="gridheader" />
                                <RowStyle CssClass="gridrow" />
                                <AlternatingRowStyle CssClass="gridalterrow" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnFLow" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        <script type="text/javascript">
            $('.tablelist tbody tr:odd').addClass('odd');
            $("input[type$='checkbox']").attr('disabled', 'disabled');
        </script>
    </form>
</body>
</html>
