<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SeasonPlan.aspx.cs" Inherits="Product_Plan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>季度生产计划管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
    <script type="text/javascript" src="../js/select-ui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".click1").click(function () {
                $("#addtip").fadeIn(200);
            });

            $(".click2").click(function () {
                $("#mdftip").fadeIn(200);
            });

            $(".click3").click(function () {
                $("#deltip").fadeIn(200);
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
        function Aprvlist() {
            $("#flowinfo").fadeIn(200);
        };

        function Aprvlisthide() {
            $("#flowinfo").fadeOut(100);
        };
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
            <li><a href="#">季度生产计划管理</a></li>
        </ul>
    </div>
    <div class="formbody">
        <div id="usual1" class="usual">
            <div class="itab">
                <ul>
                    <li><a href="#tab1" class="selected" id="tabtop1">季度生产计划</a></li>
                    <li><a href="#tab2" id="tabtop2">生产任务编制</a></li>
                </ul>
            </div>
        </div>
        <div id="tab1" class="tabson">
            <div class="framelist">
                <div class="listtitle">
                    查询条件<span style="position: relative; float: right">
                        <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" OnClick="btnSearch_Click" />
                    </span>
                </div>
                <table class="tablelist">
                    <tbody>
                        <tr>
                            <td width="100">
                                年份
                            </td>
                            <td>
                                <asp:TextBox ID="txtYears" runat="server" class="dfinput1"></asp:TextBox>
                            </td>
                            <td>
                                季度
                            </td>
                            <td>
                                <asp:DropDownList ID="listSeason" runat="server" CssClass="drpdwnlist">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value="1">一季度</asp:ListItem>
                                    <asp:ListItem Value="2">二季度</asp:ListItem>
                                    <asp:ListItem Value="3">三季度</asp:ListItem>
                                    <asp:ListItem Value="4">四季度</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="listtitle" style="margin-top: 10px">
                    生产计划表<span style="position: relative; float: right">
                        <asp:Button ID="btnIssued" runat="server" Text="下发" class="btn1" OnClick="btnIssued_Click" />
                        <asp:Button ID="btnGridDel" runat="server" Text="删除" class="btn1" OnClick="btnGridDel_Click" />
                    </span>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="ID" AllowPaging="True"
                            AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="年份" HeaderText="年份" />
                                <asp:BoundField DataField="季度" HeaderText="季度" />
                                <asp:BoundField DataField="计划总产量" HeaderText="计划总产量" />
                                <asp:BoundField DataField="单位" HeaderText="单位" />
                                <asp:BoundField DataField="审批状态" HeaderText="审批状态" />
                                <asp:BoundField DataField="下发状态" HeaderText="下发状态" />
                                <asp:BoundField DataField="编制人" HeaderText="编制人" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnGridEdit" runat="server" Text="编制计划" CssClass="btn1" Width="75"
                                            OnClick="btnGridEdit_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnSubmit" runat="server" Text="提交审批" CssClass="btn1" Width="75"
                                            OnClick="btnSubmit_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnFLow" runat="server" Text="审批进度" CssClass="btn1" Width="75" OnClick="btnFLow_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnIssued" />
                        <asp:AsyncPostBackTrigger ControlID="btnGridDel" />
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        <asp:AsyncPostBackTrigger ControlID="btnModify" />
                        <asp:AsyncPostBackTrigger ControlID="Gridview1" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="tab2" class="tabson">
            <div class="framelist">
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="listtitle">
                                季度生产计划<span style="position: relative; float: right" class="click2">
                                    <input id="Button2" type="button" value="保存" class="btnmodify" />
                                </span>
                            </div>
                            <table class="tablelist">
                                <tbody>
                                    <tr>
                                        <td width="100">
                                            年度
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtYear" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                        <td width="100">
                                            季度
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listSeason2" runat="server" CssClass="drpdwnlist" 
                                                AutoPostBack="True" >
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="1">一季度</asp:ListItem>
                                                <asp:ListItem Value="2">二季度</asp:ListItem>
                                                <asp:ListItem Value="3">三季度</asp:ListItem>
                                                <asp:ListItem Value="4">四季度</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="listtitle" style="margin-top: 10px">
                                生产任务编制<span style="position: relative; float: right"><asp:Button ID="btnAdd" runat="server"
                                    CssClass="btnadd" Text="新增" OnClick="btnAdd_Click" />
                                    <asp:Button ID="btnCkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnCkAll_Click" />
                                    <asp:Button ID="btnDelSel" runat="server" CssClass="btndel" Text="删除" OnClick="btnDelSel_Click" />
                                </span>
                            </div>
                            <asp:HiddenField ID="hidePlanID" runat="server" />
                            <asp:GridView ID="GridView2" runat="server" class="grid" AllowPaging="True" AutoGenerateColumns="False"
                                DataKeyNames="id">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="产品" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                           <asp:DropDownList ID="listProd" runat="server" DataSource='<%# ddlbind()%>' DataValueField="prod_code"
                                                CssClass="drpdwnlist" DataTextField="产品规格">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="计划数量(吨)">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtOutput" runat="server" DataValueField="计划数量" DataTextField="计划数量"
                                                CssClass="tbinput1"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table width="270px" align="center">
                                                <tr>
                                                    <td colspan="3" align="center">
                                                        生产月份
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="90px">
                                                       第1个月
                                                    </td>
                                                    <td width="90px">
                                                        第2个月
                                                    </td>
                                                    <td width="90px">
                                                       第3个月
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="270px">  
                            <tr>  
                                <td width="90px">  
                                    <asp:TextBox ID="txtAmount1" runat="server" DataValueField="计划数量" DataTextField="计划数量"
                                                CssClass="tbinput"></asp:TextBox>
                                </td>  
                                <td width="90px">  
                                 <asp:TextBox ID="txtAmount2" runat="server" DataValueField="计划数量" DataTextField="计划数量"
                                                CssClass="tbinput"></asp:TextBox>
                                </td> 
                                <td width="90px">
                                 <asp:TextBox ID="txtAmount3" runat="server" DataValueField="计划数量" DataTextField="计划数量"
                                                CssClass="tbinput"></asp:TextBox>
                                </td> 
                            </tr>  
                        </table>     
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnGrid2Save" runat="server" Text="保存" CssClass="btn1" OnClick="btnGrid2Save_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnGrid2Del" runat="server" Text="删除" CssClass="btn1" OnClick="btnGrid2Del_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#F0F5F7" Font-Bold="True" ForeColor="Black" />  
                                <RowStyle CssClass="gridrow" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                            <asp:AsyncPostBackTrigger ControlID="btnDelSel" />
                            <asp:AsyncPostBackTrigger ControlID="GridView1" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="tip" id="mdftip">
                <div class="tiptop">
                    <span>提示信息</span><a></a></div>
                <div class="tipinfo">
                    <span>
                        <img src="../images/ticon.png" /></span>
                    <div class="tipright">
                        <p>
                            确认保存此条记录 ？</p>
                        <cite>如果是请点击确定按钮 ，否则请点取消。</cite>
                    </div>
                </div>
                <div class="tipbtn">
                    <asp:Button ID="btnModify" class="sure" runat="server" Text="确定" OnClick="btnModify_Click" />&nbsp;
                    <input name="" type="button" class="cancel" value="取消" />
                </div>
            </div>
        </div>
        <div class="aprvinfo" id="flowinfo">
            <div class="tiptop">
                <span>审批流程详情</span><a onclick="Aprvlisthide()"></a></div>
            <div class="flowinfo">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView3" runat="server" class="grid">
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <script type="text/javascript">
            $("#usual1 ul").idTabs(); 
        </script>
        <script type="text/javascript">
            $('.tablelist tbody tr:odd').addClass('odd');
        </script>
    </div>
    </form>
</body>
</html>
