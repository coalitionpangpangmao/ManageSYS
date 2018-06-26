<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Schedual.aspx.cs" Inherits="Product_Schedual" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>生产排班</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/select.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>    
    <script type="text/javascript">
        $(document).ready(function () {
            $(".click1").click(function () {
                $("#addtip").fadeIn(200);
            });
            $(".tiptop a").click(function () {
                $(".tip").fadeOut(200);
            });

            $(".sure").click(function () {
                $(".tip").fadeOut(100);
            });

            $(".cancel").click(function () {
                $(".tip").fadeOut(100);
            });

        });
        function GridClick(code) {
            $('#tabtop2').click();

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
            <li><a href="#">生产管理</a></li>
            <li><a href="#">生产排班</a></li>
        </ul>
    </div>
    <div class="formbody">
        <div class="framelist">
            <div class="listtitle">
                排班管理<span style="position: relative; float: right">
                    <asp:Button ID="btnAdd" runat="server" Text="自动排班" CssClass="btnview auth"  OnClick = "btnAdd_Click" Width = "100"/>
                </span>
            </div>
            <table class="tablelist">
                <tbody>
                    <tr>
                        <td width="100">
                            车间名称
                        </td>
                        <td>
                                        <asp:DropDownList ID="listPrdline" runat="server" 
                                CssClass="drpdwnlist">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value="703">再造梗丝生产线</asp:ListItem> 
                                        </asp:DropDownList>
                        </td>
                        <td width="100">
                            排班区间
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartDate" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>至
                            <asp:TextBox ID="txtEndDate" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td colspan="4">
                            <asp:GridView ID="GridView1" runat="server" class="grid" AllowPaging="False"  DataKeyNames = "班时编码"
                        AutoGenerateColumns="False" >
                <Columns>                  
                    <asp:TemplateField HeaderText="班时" >
                        <ItemTemplate>
                            <asp:TextBox ID="txtShift" runat="server" DataValueField="班时" DataTextField="班时"  cssclass = "tbinput" Enabled = "false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="开始时间" >
                        <ItemTemplate>
                            <asp:TextBox ID="txtStarttime" runat="server" DataValueField="开始时间" DataTextField="开始时间"  cssclass = "tbinput1" Enabled = "false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="结束时间" >
                        <ItemTemplate>
                            <asp:TextBox ID="txtEndtime" runat="server" DataValueField="结束时间" DataTextField="结束时间" Enabled = "false"  cssclass = "tbinput1"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="是否跨天" >
                        <ItemTemplate>
                            <asp:CheckBox ID="ckInter" runat="server" Enabled = "false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="班组" >
                        <ItemTemplate>
                            <asp:DropDownList ID="listTeam" runat="server" CssClass = "drpdwnlist" Width = "70px">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="状态">
                        <ItemTemplate>
                             <asp:DropDownList ID="listStatus" runat="server" CssClass = "drpdwnlist" Width = "70px">                     <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value = "1">工作</asp:ListItem>
                                            <asp:ListItem  Value = "0">休息</asp:ListItem>           
                            </asp:DropDownList>
                            </ItemTemplate>
                    </asp:TemplateField>                              
                </Columns>
                <HeaderStyle CssClass="gridheader" />
                 <RowStyle CssClass="gridrow" /> <AlternatingRowStyle CssClass="gridalterrow" />
            </asp:GridView>
                        </td>
                    </tr>                    
                </tbody>
            </table>
            <div class="listtitle" style="margin-top: 10px">
                生产日历<span style="position: relative; float: right">
                    <asp:Button ID="btnckAll" runat="server" Text="全选" class="btn1 auth"  OnClick = "btnckAll_Click"/>
                    <asp:Button ID="btnGridDel" runat="server" Text="删除" class="btn1 auth"  OnClick = "btnGridDel_Click"/>
                    <asp:Button ID="btnGridEdit" runat="server" Text="保存" class="btn1 auth"  OnClick = "btnGridEdit_Click"/>
                </span>
            </div>
            <div style="overflow: scroll; width: 100%; height: 300px;" >
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="GridView2" runat="server" class="grid" DataKeyNames="ID" AllowPaging="False"
                        AutoGenerateColumns="False" PageSize="12">
                        <Columns>                           
                            <asp:BoundField DataField="日期" HeaderText="日期" />
                            <asp:BoundField DataField="班组" HeaderText="班组" />
                            <asp:BoundField DataField="班时" HeaderText="班时" />
                            <asp:BoundField DataField="开始时间" HeaderText="开始时间" />
                            <asp:BoundField DataField="结束时间" HeaderText="结束时间" />
                             <asp:TemplateField HeaderText="状态">
                        <ItemTemplate>
                             <asp:DropDownList ID="listStatus2" runat="server" CssClass = "drpdwnlist" Width = "60px">                     <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value = "1">工作</asp:ListItem>
                                            <asp:ListItem Value = "0">休息</asp:ListItem>           
                            </asp:DropDownList>
                            </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="gridheader" />
                         <RowStyle CssClass="gridrow" /> <AlternatingRowStyle CssClass="gridalterrow" />
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID = "btnAdd" />
                <asp:AsyncPostBackTrigger ControlID = "btnckAll" />
                <asp:AsyncPostBackTrigger ControlID = "btnGridEdit" />
                <asp:AsyncPostBackTrigger ControlID = "btnGridDel" />
                </Triggers>
            </asp:UpdatePanel>
            </div>
        </div>
        <script type="text/javascript">
            $('.tablelist tbody tr:odd').addClass('odd');
        </script>
    </div>
    </form>
</body>
</html>
