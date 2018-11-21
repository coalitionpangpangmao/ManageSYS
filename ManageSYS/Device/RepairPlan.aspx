<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RepairPlan.aspx.cs" Inherits="Device_RepairPlan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>维修计划</title>
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
            <li><a href="#">维修计划与派工</a></li>
        </ul>
    </div>
    <div class="formbody">
        <div id="usual1" class="usual">
            <div class="itab">
                <ul>
                    <li><a href="#tab1" class="selected" id="tabtop1">维修计划列表</a></li>
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
                                <asp:TextBox ID="txtStart" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>至
                                <asp:TextBox ID="txtStop" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="listtitle" style="margin-top: 10px">
                    维修计划列表<span style="position: relative; float: right">
                        <asp:Button ID="btnGridDel" runat="server" Text="删除" class="btndel auth" OnClick = "btnGridDel_Click"  OnClientClick="javascript:return confirm('确认删除？');"/>
                        <asp:Button ID="btnGridNew" runat="server" Text="新增" class="btnadd  auth"   OnClick = "btnGridNew_Click"/>
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
                                     <asp:BoundField    DataField="维修计划" HeaderText="维修计划" />
                                     <asp:BoundField    DataField="部门" HeaderText="部门" />
                                      <asp:TemplateField       HeaderText ="审批状态">
                                        <ItemTemplate>
                                            <asp:Label ID="labAprv" runat="server"  CssClass="labstatu" Width ="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField      HeaderText ="执行状态">
                                        <ItemTemplate>
                                            <asp:Label ID="labexe" runat="server"  CssClass="labstatu" Width ="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                   
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
                                            <asp:Button ID="btnGridview" runat="server" Text="查看" CssClass="btn1 auth" Width="75" OnClick = "btnGridview_Click" />
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
                              <asp:AsyncPostBackTrigger ControlID = "btnDspcth" />
                        <asp:AsyncPostBackTrigger ControlID ="btnTrack" />
                             <asp:AsyncPostBackTrigger ControlID ="btnDone" />
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
                            维修计划<span style="position: relative; float: right">
                                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btnview  auth" OnClick="btnSave_Click" />
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
                                        <asp:TextBox ID="txtCode" runat="server" class="dfinput1" Enabled="false"></asp:TextBox>
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
                                        <asp:TextBox ID="txtExptime" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
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
                                
                            </tbody>
                        </table>
                        <div class="listtitle" style="margin-top: 10px">
                            维修计划明细<span style="position: relative; float: right">
                                <asp:Button ID="btnAdd" runat="server" CssClass="btnadd  auth" Text="新增" OnClick="btnAdd_Click" />
                                <asp:Button ID="btnCkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnCkAll_Click" />
                                <asp:Button ID="btnDelSel" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnDelSel_Click"  OnClientClick="javascript:return confirm('确认删除？');"/>
                              <asp:Button ID="btnDispatch" runat="server" CssClass="btnpatch auth" Text="派工"  OnClientClick="patchClick()"/>
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
                                <asp:TemplateField      HeaderText="区域">
                                    <ItemTemplate>
                                          <asp:DropDownList ID="listGridarea" runat="server" CssClass="drpdwnlist"   DataSource = "<%# sectionbind() %>"  DataTextField = "Section_NAME"  DataValueField = "Section_CODE"  OnSelectedIndexChanged = "listGridarea_SelectedIndexChanged" AutoPostBack="True" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField      HeaderText="设备名称">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="listGridEq" runat="server" CssClass="drpdwnlist" Width ="180px">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField      HeaderText="维修原因">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGridReason" runat="server" DataValueField="维修原因" DataTextField="维修原因" 
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField      HeaderText="维修内容">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGridcntnt" runat="server" DataValueField="维修内容" DataTextField="维修内容" Width ="250px"
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField      HeaderText="期望完成时间">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGridExptime" runat="server" DataValueField="期望完成时间" DataTextField="期望完成时间" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"                      CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField      HeaderText="状态">
                                    <ItemTemplate>
                                       <asp:DropDownList ID="listGrid2Status" runat="server" CssClass="drpdwnlist" Width="70px" Enabled="False"  DataSource = "<%# statusbind() %>"  DataTextField = "Name"  DataValueField = "ID"  >  </asp:DropDownList>
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
                                <asp:TemplateField      ItemStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:Button ID="btnGrid2View" runat="server" Text="查看" CssClass="btnred" Width="75"
                                                    OnClick="btnGrid2View_Click" />
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" />
                                        </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridheader" />
                             <RowStyle CssClass="gridrow" /> 
                            <AlternatingRowStyle CssClass="gridalterrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>                   
                    <asp:AsyncPostBackTrigger ControlID = "GridView1" />
                    <asp:AsyncPostBackTrigger ControlID = "GridView2" />
                    <asp:AsyncPostBackTrigger ControlID = "btnAdd" />
                    <asp:AsyncPostBackTrigger ControlID = "btnCkAll" />
                    <asp:AsyncPostBackTrigger ControlID = "btnDelSel" />
                      <asp:AsyncPostBackTrigger ControlID = "btnDspcth" />
                        <asp:AsyncPostBackTrigger ControlID ="btnTrack" />
                             <asp:AsyncPostBackTrigger ControlID ="btnDone" />
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
                             <RowStyle CssClass="gridrow" /> <AlternatingRowStyle CssClass="gridalterrow" />
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

        <div class="shade">
                <div  style="width:900px; height:380px; position:absolute;top:6%; left:8%;background:#fcfdfd;box-shadow:1px 8px 10px 1px #9b9b9b;border-radius:1px;behavior:url(js/pie.htc); ">
                    <div class="tiphead">
                        <span>维修详情</span><a onclick="$('.shade').fadeOut(100);"></a>
                    </div>
                    <div class="gridinfo">                  
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                        <table class="tablelist" style="margin-bottom: 10px">
                                <tbody>
                                    <tr>
                                         
                                       
                                        <td width="100">单据编号
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCodeS" runat="server" class="dfinput1"  Enabled="false"></asp:TextBox>
                                        </td>
                                        <td width="100">区域
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listArea" runat="server" CssClass="drpdwnlist"  >
                                            </asp:DropDownList>
                                        </td>
                                         <td width="100">维保设备
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listEq" runat="server" CssClass="drpdwnlist" >
                                            </asp:DropDownList>
                                        </td>
                                       
                                    </tr>
                                    <tr>
                                        
                                        <td width="100">操作员
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listOptor" runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="100">操作时间
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOpttime" runat="server" class="dfinput1"  onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                        </td>
                                        <td>操作时长</td>
                                        <td>
                                            <asp:TextBox ID="txtSegcount" runat="server" class="dfinput1" ></asp:TextBox>
                                        </td>


                                    </tr>
                                    <tr>
                                        <td width="100" height="50px">操作记录
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtRecord" runat="server" class="dfinput1" Height="50px" TextMode="MultiLine"
                                                Width="650px"></asp:TextBox>
                                        </td>                                       

                                    </tr>
                                    <tr>
                                        <td width="100" height="50px">故障情况
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtFalut" runat="server" class="dfinput1" Height="50px" TextMode="MultiLine"
                                                Width="650px"></asp:TextBox>
                                        </td>                                       

                                    </tr>
                                    <tr>
                                        <td width="100" height="50px">反馈情况
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtFeedback" runat="server" class="dfinput1" Height="50px" TextMode="MultiLine"
                                                Width="650px"></asp:TextBox>
                                        </td>                                       

                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                        <asp:AsyncPostBackTrigger ControlID = "GridView2" />
                       
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
