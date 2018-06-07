<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AutoCalibrate.aspx.cs" Inherits="Device_AutoCalibrate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>校准计划</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/select.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
    <script type="text/javascript">
        
        function patchClick(event) {            
            var e = event || window.event;
            var scrollX = document.documentElement.scrollLeft || document.body.scrollLeft;
            var scrollY = document.documentElement.scrollTop || document.body.scrollTop;
            var x = e.pageX || e.clientX + scrollX;
            x = parseInt(x) - 200;
            var y = e.pageY || e.clientY + scrollY;
            $("#dspcthor").css({ "position": "fix", "top": y + "px", "left": x + "px" });
            $('#dspcthor').show();
        }
        function GridClick() {
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
            <li><a href="#">设备管理</a></li>
            <li><a href="#">自动校准</a></li>
        </ul>
    </div>
    <div class="formbody">
        <div id="usual1" class="usual">
            <div class="itab">
                <ul>
                    <li><a href="#tab1" class="selected" id="tabtop1">校准计划列表</a></li>
                    <li><a href="#tab2" id="tabtop2">计划明细</a></li>
                </ul>
            </div>
        </div>
        <div id="tab1" class="tabson">
            <div class="framelist">
                <div class="listtitle">
                    查询条件<span style="position: relative; float: right">
                        <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" OnClick = "btnSearch_Click" />
                    </span>
                </div>
                <table class="tablelist">
                    <tbody>
                        <tr>
                            <td width="100">
                                时间
                            </td>
                            <td>
                                <asp:TextBox ID="txtStart" runat="server" class="dfinput1"></asp:TextBox>至
                                <asp:TextBox ID="txtStop" runat="server" class="dfinput1"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="listtitle" style="margin-top: 10px">
                    校准计划列表<span style="position: relative; float: right">
                        <asp:Button ID="btnGridDel" runat="server" Text="删除" class="btndel" OnClick = "btnGridDel_Click" />
                        <asp:Button ID="btnGridNew" runat="server" Text="新建" class="btnadd"   OnClick = "btnGridNew_Click"/>
                    </span>
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="PZ_CODE" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="校准计划" HeaderText="校准计划" />
                                    <asp:BoundField DataField="部门" HeaderText="部门" />
                                    <asp:BoundField DataField="审批状态" HeaderText="审批状态" />
                                    <asp:BoundField DataField="执行状态" HeaderText="执行状态" />
                                    <asp:BoundField DataField="备注" HeaderText="备注" />
                                    <asp:TemplateField ItemStyle-Width="80">
                                        <ItemTemplate>
                                            <asp:Button ID="btnSubmit" runat="server" Text="提交审批" CssClass="btn1" Width="75"
                                                OnClick="btnSubmit_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="80">
                                        <ItemTemplate>
                                            <asp:Button ID="btnGridIssue" runat="server" Text="审批进度" CssClass="btn1" Width="75"
                                                OnClick="btnGridIssue_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="80">
                                        <ItemTemplate>
                                            <asp:Button ID="btnGridview" runat="server" Text="查看" CssClass="btn1" Width="75" OnClick = "btnGridview_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridheader" />
                                <RowStyle CssClass="gridrow" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                            <asp:AsyncPostBackTrigger ControlID="btnGridDel" />
                            <asp:AsyncPostBackTrigger ControlID="btnGridNew" />
                            <asp:AsyncPostBackTrigger ControlID="GridView1" />
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div id="tab2" class="tabson">
            <div class="framelist">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="listtitle">
                            校准计划<span style="position: relative; float: right">
                                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btnview" OnClick="btnSave_Click" />
                            </span>
                        </div>
                        <table class="tablelist">
                            <tbody>
                                <tr>
                                    <td width="100">
                                        计划名：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtName" runat="server" class="dfinput1"></asp:TextBox>
                                    </td>
                                    <td width="100">
                                        凭证号：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCode" runat="server" class="dfinput1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100">
                                        申请人：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="listEditor" runat="server" CssClass="drpdwnlist">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="100">
                                        申请部门：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="listApt" runat="server" CssClass="drpdwnlist">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100">
                                        过期时间：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExptime" runat="server" class="dfinput1"></asp:TextBox>
                                    </td>
                                    <td width="100">
                                        备注：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtdscrpt" runat="server" class="dfinput1"></asp:TextBox>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td width="100">
                                        保存为模版：
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="ckModel" runat="server" Text=" " />
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </tbody>
                        </table>
                        <div class="listtitle" style="margin-top: 10px">
                            校准计划明细<span style="position: relative; float: right">
                                <asp:Button ID="btnAdd" runat="server" CssClass="btnadd" Text="新增" OnClick="btnAdd_Click" />
                                <asp:Button ID="btnCkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnCkAll_Click" />
                                <asp:Button ID="btnDelSel" runat="server" CssClass="btndel" Text="删除" OnClick="btnDelSel_Click" />
                                <input id="btnDispatch" type="button" value="派工"  class = "btnpatch" onclick = "patchClick()"; />
                            </span>
                        </div>
                        <table>
                            <tr>
                                <td height="30px" align="center">
                                    <asp:DropDownList ID="listModel" runat="server" CssClass="drpdwnlist">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnCreate" runat="server" CssClass="btn" Text="从模版生成明细" OnClick="btnCreate_Click" />
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="GridView2" runat="server" class="grid" AllowPaging="True" AutoGenerateColumns="False"
                            DataKeyNames="ID">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="区域">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="listGridarea" runat="server" CssClass="drpdwnlist" Width="80px">
                                          <asp:ListItem></asp:ListItem>
                                            <asp:ListItem >A区</asp:ListItem>
                                            <asp:ListItem> B区</asp:ListItem>
                                            <asp:ListItem >C区</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="设备名称">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="listGridEq" runat="server" CssClass="drpdwnlist" DataSource = "<%# eqbind() %>"  DataTextField = "EQ_NAME"  DataValueField = "IDKEY">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="校准原因">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGridReason" runat="server" DataValueField="校准原因" DataTextField="校准原因"
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="校准内容">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGridcntnt" runat="server" DataValueField="校准内容" DataTextField="校准内容"
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="期望完成时间">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGridExptime" runat="server" DataValueField="期望完成时间" DataTextField="期望完成时间"
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="状态">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="listGrid2Status" runat="server" CssClass="drpdwnlist" Width="70px" Enabled="False">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value='0'>未派工</asp:ListItem>
                                            <asp:ListItem Value='1'> 己派工</asp:ListItem>
                                            <asp:ListItem Value='2'>己完成</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="备注">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGridremark" runat="server" DataValueField="备注" DataTextField="备注"
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="操作" ItemStyle-Width="80">
                                    <ItemTemplate>
                                        <asp:Button ID="btnGrid2Save" runat="server" Text="保存" CssClass="btn1" Width="75"
                                            OnClick="btnGrid2Save_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                    <asp:AsyncPostBackTrigger ControlID = "btnCreate" />
                    <asp:AsyncPostBackTrigger ControlID = "GridView1" />
                    <asp:AsyncPostBackTrigger ControlID = "GridView2" />
                     <asp:AsyncPostBackTrigger ControlID = "btnAdd" />
                    <asp:AsyncPostBackTrigger ControlID = "btnCkAll" />
                    <asp:AsyncPostBackTrigger ControlID = "btnDelSel" />
                    <asp:AsyncPostBackTrigger ControlID = "btnDspcth" />
                    <asp:AsyncPostBackTrigger ControlID = "btnGridNew" />
                    </Triggers>
                </asp:UpdatePanel>
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
        <div id = "dspcthor"  class = "userbox">
         <div class="tiptop">
                <span>请选择任务执行人员</span><a onclick="$('#dspcthor').hide()"></a></div>
                 <div class="flowinfo">
            <asp:DropDownList ID="listdspcth" runat="server" CssClass = "drpdwnlist">
            </asp:DropDownList>
            </div>
              <div style=" margin-top: 10px;" align="center" >
               <asp:Button ID="btnDspcth" runat="server" CssClass="sure" Text="确定" OnClick="btnDspcth_Click"  Height="30px" Width="80px" />               
                <input name="" type="button" class="cancel" value="取消" 
                      style="width: 80px; height: 30px" onclick="$('#dspcthor').hide()"/>
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
