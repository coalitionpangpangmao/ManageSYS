<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Inspect_Sensor.aspx.cs" Inherits="Quality_Inspect_Sensor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>感观评测</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                 <li><a href="#">质量评估与分析</a></li>
                  <li><a href="#">离线数据评估</a></li>
                <li><a href="#">感观评测</a></li>
            </ul>
        </div>
        <div class="formbody">
            <div id="usual1" class="usual">
                <div class="itab">
                    <ul>
                        <li><a href="#tab1" class="selected" id="tabtop1">感观评测原始记录</a></li>
                        <li><a href="#tab2" id="tabtop2">感观评测-编辑</a></li>
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
                                <td width="100">评吸人
                                </td>
                                <td>
                                    <asp:DropDownList ID="listEditor" runat="server" CssClass="drpdwnlist"></asp:DropDownList>
                                </td>
                                 <td width="100">检测月份
                                </td>
                                <td>
                                     <asp:TextBox ID="txtMonth" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM'})"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div class="listtitle" style="margin-top: 10px">
                        感观评测记录列表<span style="position: relative; float: right">
                            <asp:Button ID="btnAddPlan" runat="server" Text="新增" class="btnadd auth" OnClick="btnAddPlan_Click"  OnClientClick ="$('#tabtop2').click()"/>
                                   <asp:Button ID="btnDel" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnDel_Click" OnClientClick="javascript:return confirm('确认删除所选生产计划吗？');" />
                        </span>
                    </div>
                    <div style="overflow: auto">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="ID,creat_id" AllowPaging="True"
                                    AutoGenerateColumns="False">
                                    <Columns>
                                          <asp:TemplateField  >
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      

                                         <asp:BoundField    DataField="sensor_month" HeaderText="评吸月份" />
                                         <asp:BoundField    DataField="sensor_time" HeaderText="评吸时间" />
                                         <asp:BoundField    DataField="name" HeaderText="评吸人" />
                                          <asp:BoundField    DataField="record_time" HeaderText="录入时间" />
                                        <asp:TemplateField  >
                                            <ItemTemplate>
                                                <asp:Button ID="btnGridEdit" runat="server" Text="编制" CssClass="btn1" Width="60px" OnClientClick ="$('#tabtop2').click()"
                                                    OnClick="btnGridEdit_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>                                      
                                    </Columns>
                                    <HeaderStyle CssClass="gridheader" />
                                    <RowStyle CssClass="gridrow" />
                                    <AlternatingRowStyle CssClass="gridalterrow" />
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                            <asp:AsyncPostBackTrigger ControlID ="btnModify" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div id="tab2" class="tabson">
                <div class="framelist">
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="listtitle">
                                    感观评吸原始记录<span style="position: relative; float: right" class="click2">
                                        <asp:Button ID="btnModify" class="btnmodify auth" runat="server" Text="保存" OnClick="btnModify_Click" />
                                    </span>
                                </div>
                                <table class="tablelist">
                                    <tbody>
                                        <tr>
                                            <td width="100">评吸月份
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMonth2" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM'})"></asp:TextBox>
                                            </td>
                                            <td width="100">评吸时间
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSensorTime" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="100">评吸人
                                            </td>
                                            <td>
                                                 <asp:DropDownList ID="listEditor2" runat="server" CssClass="drpdwnlist" ></asp:DropDownList>
                                            </td>
                                            <td width="100">录入时间
                                            </td>
                                            <td>
                                               <asp:TextBox ID="txtRecordTime" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <div class="listtitle" style="margin-top: 10px">
                                    原始记录明细<span style="position: relative; float: right"><asp:Button ID="btnAdd" runat="server"
                                        CssClass="btnadd  auth" Text="新增" OnClick="btnAdd_Click" />
                                      
                                        <asp:Button ID="btnDelSel" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnDelSel_Click" OnClientClick="javascript:return confirm('确认删除记录吗？');" />
                                              <asp:Button ID="btnGrid2Modify" class="btnmodify auth" runat="server" Text="保存" Width ="90px" OnClick="btnGrid2Modify_Click" />
                                           <asp:HiddenField ID="hideID" runat="server" />
                                    </span>
                                </div>
                             
                              
                                <asp:GridView ID="GridView2" runat="server" class="grid"  DataKeyNames="record_time"  AutoGenerateColumns="false">
                                    
                                    <HeaderStyle CssClass="gridheader" />
                                    <RowStyle CssClass="gridrow" />
                                    <AlternatingRowStyle CssClass="gridalterrow" />
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                                <asp:AsyncPostBackTrigger ControlID="btnDelSel" />
                                <asp:AsyncPostBackTrigger ControlID="GridView2" />
                                <asp:AsyncPostBackTrigger ControlID="GridView1" />
                                <asp:AsyncPostBackTrigger ControlID="btnAddPlan" /> 
                                <asp:AsyncPostBackTrigger ControlID ="btnGrid2Modify" />
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
