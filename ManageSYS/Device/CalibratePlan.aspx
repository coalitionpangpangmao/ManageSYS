<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CalibratePlan.aspx.cs" Inherits="Device_CalibratePlan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>计量设备校准</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />

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
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">设备管理</a></li>
                <li><a href="#">校准计划与派工</a></li>
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
                            <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" OnClick="btnSearch_Click" />
                        </span>
                    </div>
                    <table class="tablelist">
                        <tbody>
                            <tr>
                                <td width="100">时间
                                </td>
                                <td>
                                    <asp:TextBox ID="txtStart" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>至
                                <asp:TextBox ID="txtStop" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div class="listtitle" style="margin-top: 10px">
                        校准计划列表<span style="position: relative; float: right">
                            <asp:Button ID="btnGridDel" runat="server" Text="删除" class="btndel auth" OnClick="btnGridDel_Click" OnClientClick="javascript:return confirm('确认删除？');" />
                            <asp:Button ID="btnGridNew" runat="server" Text="新增" class="btnadd  auth" OnClick="btnGridNew_Click" />
                        </span>
                    </div>
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="PZ_CODE" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="8">
                                    <Columns>
                                        <asp:TemplateField     >
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField    DataField="校准计划" HeaderText="校准计划" />
                                        <asp:TemplateField      HeaderText="审批状态">
                                            <ItemTemplate>
                                                <asp:Label ID="labAprv" runat="server" CssClass="labstatu" Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField      HeaderText="执行状态">
                                            <ItemTemplate>
                                                <asp:Label ID="labexe" runat="server" CssClass="labstatu" Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField    DataField="校准方式" HeaderText="校准方式" />
                                        <asp:BoundField    DataField="备注" HeaderText="备注" />
                                        <asp:TemplateField      ItemStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:Button ID="btnSubmit" runat="server" Text="提交审批" CssClass="btn1 auth" Width="75"
                                                    OnClick="btnSubmit_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField      ItemStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:Button ID="btnGridIssue" runat="server" Text="审批进度" CssClass="btn1 auth" Width="75"
                                                    OnClick="btnGridIssue_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField      ItemStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:Button ID="btnGridview" runat="server" Text="查看" CssClass="btn1 auth" Width="75" OnClick="btnGridview_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridheader" />
                                    <RowStyle CssClass="gridrow" />
                                    <AlternatingRowStyle CssClass="gridalterrow" />
                                    <PagerStyle CssClass="gridpager" />
                                    <PagerTemplate>
                                        <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> ' Width="120px"></asp:Label>
                                        <asp:LinkButton ID="lbnFirst" runat="Server" Text="首页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="First"></asp:LinkButton>
                                        <asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="Prev"></asp:LinkButton>
                                        <asp:LinkButton ID="lbnNext" runat="Server" Text="下一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Next"></asp:LinkButton>
                                        <asp:LinkButton ID="lbnLast" runat="Server" Text="尾页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Last"></asp:LinkButton>
                                        到第
                                <asp:TextBox ID="txtNewPageIndex" runat="server" Width="20px" Text='<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>' />
                                        页  
             <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-2"
                 CommandName="Page" Text="跳转" />
                                    </PagerTemplate>
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                                <asp:AsyncPostBackTrigger ControlID="btnGridDel" />
                                <asp:AsyncPostBackTrigger ControlID="btnGridNew" />
                                <asp:AsyncPostBackTrigger ControlID="GridView1" />
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                <asp:AsyncPostBackTrigger ControlID="btnDspcth" />
                                <asp:AsyncPostBackTrigger ControlID="btnTrack" />
                                <asp:AsyncPostBackTrigger ControlID="btnDone" />
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
                                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btnmodify  auth" OnClick="btnSave_Click" />
                                </span>
                            </div>
                            <table class="tablelist">
                                <tbody>
                                    <tr>
                                        <td width="100">计划名：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtName" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                        <td width="100">凭证号：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCode" runat="server" class="dfinput1" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100">申请人：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listEditor" runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="100">申请部门：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listApt" runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100">过期时间：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtExptime" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                        </td>
                                        <td width="100">备注：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtdscrpt" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td width="100">校准类型：
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rdType1" runat="server" GroupName="Type" Text="人工校准" />
                                            <asp:RadioButton ID="rdType2" runat="server" GroupName="Type" Text="自动校准" />
                                        </td>
                                        <td width="100">保存为模版：
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="ckModel" runat="server" Text=" " />
                                        </td>

                                    </tr>
                                </tbody>
                            </table>
                            <div class="listtitle" style="margin-top: 10px">
                                校准计划明细
                             <span style="display: inline-block">
                                 <asp:DropDownList ID="listModel" runat="server" CssClass="drpdwnlist">
                                 </asp:DropDownList>
                                 <asp:Button ID="btnCreate" runat="server" CssClass="btn" Text="从模版生成明细" OnClick="btnCreate_Click" /></span>
                                <span style="position: relative; float: right">
                                    <asp:Button ID="btnAdd" runat="server" CssClass="btnadd  auth" Text="新增" OnClick="btnAdd_Click" />
                                    <asp:Button ID="btnCkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnCkAll_Click" />
                                    <asp:Button ID="btnDelSel" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnDelSel_Click" OnClientClick="javascript:return confirm('确认删除？');" />
                                    <asp:Button ID="btnDispatch" runat="server" CssClass="btnpatch auth" Text="派工" OnClientClick="patchClick()" />
                                    <asp:Button ID="btnTrack" runat="server" CssClass="btnview auth" Text="跟踪" OnClick="btnTrack_Click" />
                                    <asp:Button ID="btnDone" runat="server" CssClass="btndone auth" Text="完成" OnClick="btnDone_Click" />
                                </span>
                            </div>

                            <asp:GridView ID="GridView2" runat="server" class="grid" AllowPaging="True" AutoGenerateColumns="False"
                                DataKeyNames="ID">
                                <Columns>
                                    <asp:TemplateField     >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField      HeaderText="工段">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="listGridsct" runat="server" CssClass="drpdwnlist" DataSource="<%# sectionbind() %>" DataTextField="Section_NAME" DataValueField="Section_CODE" OnSelectedIndexChanged="listGridsct_SelectedIndexChanged" AutoPostBack="True" Width ="180px">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField      HeaderText="设备名称">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="listGridEq" runat="server" CssClass="drpdwnlist" OnSelectedIndexChanged="listGridEq_SelectedIndexChanged" AutoPostBack="True" Width ="180px"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField      HeaderText="数据点">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="listGridPoint" runat="server" CssClass="drpdwnlist" Width ="180px">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField      HeaderText="期望完成时间">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGridExptime" runat="server" DataValueField="期望完成时间" DataTextField="期望完成时间" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                                                CssClass="tbinput1"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField      HeaderText="状态">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="listGrid2Status" runat="server" CssClass="drpdwnlist" Width="70px" Enabled="False" DataSource="<%# statusbind() %>" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField    DataField="执行人" HeaderText="执行人" ControlStyle-Width="60px" />
                                    <asp:TemplateField      HeaderText="备注">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGridremark" runat="server" DataValueField="备注" DataTextField="备注"
                                                CssClass="tbinput"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField      HeaderText="操作" ItemStyle-Width="80">
                                        <ItemTemplate>
                                            <asp:Button ID="btnGrid2Save" runat="server" Text="保存" CssClass="btn1 auth" Width="75"
                                                OnClick="btnGrid2Save_Click" />
                                        </ItemTemplate>                                      
                                    </asp:TemplateField>
                                     <asp:TemplateField     >
                                    <ItemTemplate>
                                        <asp:Button ID="btngrid2Deal" runat="server" Text="详情" CssClass="btnred" OnClick="btngrid2Deal_Click" Width="100px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridheader" />
                                <RowStyle CssClass="gridrow" />
                                <AlternatingRowStyle CssClass="gridalterrow" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnCreate" />
                            <asp:AsyncPostBackTrigger ControlID="GridView1" />
                            <asp:AsyncPostBackTrigger ControlID="GridView2" />
                            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                            <asp:AsyncPostBackTrigger ControlID="btnCkAll" />
                            <asp:AsyncPostBackTrigger ControlID="btnDelSel" />
                            <asp:AsyncPostBackTrigger ControlID="btnGridNew" />
                            <asp:AsyncPostBackTrigger ControlID="btnDspcth" />
                            <asp:AsyncPostBackTrigger ControlID="btnTrack" />
                            <asp:AsyncPostBackTrigger ControlID="btnDone" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="aprvinfo" id="flowinfo">
                <div class="tiptop">
                    <span>审批流程详情</span><a onclick="Aprvlisthide()"></a>
                </div>
                <div class="flowinfo">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView3" runat="server" class="grid">
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
            </div>
            <div id="dspcthor" class="userbox">
                <div class="tiptop">
                    <span>请选择任务执行人员</span><a onclick="$('#dspcthor').hide()"></a>
                </div>
                <div class="flowinfo">
                    <asp:DropDownList ID="listdspcth" runat="server" CssClass="drpdwnlist">
                    </asp:DropDownList>
                </div>
                <div style="margin-top: 10px;" align="center">
                    <asp:Button ID="btnDspcth" runat="server" CssClass="sure" Text="确定" OnClick="btnDspcth_Click" Height="30px" Width="80px" />
                    <input name="" type="button" class="cancel" value="取消"
                        style="width: 80px; height: 30px" onclick="$('#dspcthor').hide()" />
                </div>
            </div>

            <div class="shade">
                <div class="info">
                    <div class="tiphead">
                        <span>反馈情况</span><a onclick="$('.shade').fadeOut(100);"></a>
                    </div>
                    <div class="gridinfo">
                        <asp:UpdatePanel ID="updtpanel1" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdType" runat="server" />
                                <table class="tablelist">                                   
                                    <tr style="padding-top: 5px">
                                        <td>明细序号： </td>
                                        <td>
                                              <asp:TextBox ID="txtID" runat="server" CssClass="dfinput1" Width="550px" Enabled="False"></asp:TextBox></td>
                                        </td>
                                    </tr>
                                     <tr style="padding-top: 5px">
                                        <td>校准详情
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDesp" runat="server" CssClass="dfinput1" Height="60px"
                                                Width="550px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="padding-top: 5px">
                                        <td>反馈情况
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtScean" runat="server" CssClass="dfinput1" Height="60px"
                                                Width="550px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="padding-top: 5px">
                                        <td>补充说明
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPlus" runat="server" CssClass="dfinput1" Height="60px"
                                                Width="550px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>

                                <div class="shadebtn" align="center">
                                  
                                    <input name="" type="button" class="cancel" value="关闭" onclick="$('.shade').fadeOut(100);" />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="GridView1" />
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
        </div>
    </form>
</body>
</html>
