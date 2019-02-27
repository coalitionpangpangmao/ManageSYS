<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AlarmSearch.aspx.cs" Inherits="Device_AlarmSearch" %>

<!DOCTYPE html>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>报警查询</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
        <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../js/jquery.js"></script> 
  
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">设备管理</a></li>
                <li><a href="#">报警查询</a></li>
            </ul>
        </div>
        <div class="formbody">
            <div class="framelist">
                <div class="listtitle">
                    查询条件<span style="position: relative; float: right">
                        <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" OnClick="btnSearch_Click" />
                    </span>
                </div>
                <table class="tablelist">
                    <tbody>
                        <tr>
                            <td width="100">标签
                            </td>
                            <td width="100">
                                <asp:DropDownList ID="tag" runat="server" CssClass="drpdwnlist" AutoPostBack="True"
                                     Width ="200px">
                                </asp:DropDownList>
                            </td>
                            <td width="100">区域
                            </td>
                            <td width="100">

                                        <asp:DropDownList ID="area" runat="server" CssClass="drpdwnlist" Width ="200px">
                                        </asp:DropDownList>

                            </td>
                            <td width="100">开始时间
                            </td>
                            <td width="100">

                    <asp:TextBox ID="stime" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" CssClass="dfinput1"></asp:TextBox>

                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="listtitle" style="margin-top: 10px">
                    检查项目列表 <span style="position: relative; float: right">
                        
                        <asp:Button ID="btnGrid1CkAll" runat="server" CssClass="btnset" Text="全选" />
                        <asp:Button ID="btnGrid1DelSel" runat="server" CssClass="btndel auth" Text="删除"   OnClientClick="javascript:return confirm('确认删除？');"/></span>
                </div>
                <div style="top:0px; bottom:8px;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" class="grid" AutoGenerateColumns="False"
                                DataKeyNames="ALM_NATIVETIMEIN">
                                <Columns>
                                    <asp:TemplateField     >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ck" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField    DataField="ALM_NATIVETIMEIN" HeaderText="报警开始时间" />
                                    <asp:BoundField    DataField="ALM_NATIVETIMELAST" HeaderText="报警结束时间" />
                                    <asp:BoundField    DataField="ALM_TAGNAME" HeaderText="标签" />
                                    <asp:BoundField    DataField="ALM_TAGDESC" HeaderText="详情" />
                                    <asp:BoundField    DataField="ALM_ALMAREA" HeaderText="区域" />
                                    <asp:TemplateField      HeaderText="操作">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEdit" runat="server" Text="编辑" CssClass="btn1" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridheader" />
                                <RowStyle CssClass="gridrow" />
                                <AlternatingRowStyle CssClass="gridalterrow" />
                                <PagerStyle CssClass="gridpager" />
                              
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                            <asp:AsyncPostBackTrigger ControlID="btnGrid1CkAll" />
                            <asp:AsyncPostBackTrigger ControlID="btnGrid1DelSel" />

                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="shade">
            <div class="info">
                <div class="tiphead">
                    <span>产品信息</span><a onclick="$('.shade').fadeOut(100);"></a>
                </div>
                <div class="gridinfo">

                            <table class="tablelist">
                                <tbody>
                                    <tr>
                                        <td width="100">检查类型:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listType2" runat="server" CssClass="drpdwnlist"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="100">检查分组:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listSection2" runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td width="100">检验项目编码:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCode" runat="server" class="dfinput1" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td width="100">检验项目名称:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtName" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td width="100">单位:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUnit" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                        <td width="100">编制人:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listCreator" runat="server"
                                                CssClass="drpdwnlist" Enabled="False">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>备注
                                        </td>
                                        <td colspan="3" height="90px">
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="dfinput1" Height="80px" TextMode="MultiLine"
                                                Width="500px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="shadebtn" align="center">
                                <asp:HiddenField ID="hdScrollY" runat="server" />
                                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="sure auth"  />
                                <input name="" type="button" class="cancel" value="关闭" onclick="$('.shade').fadeOut(100);" />
                            </div>


                </div>
            </div>
        </div>

        <script type="text/javascript">
            $('.tablelist tbody tr:odd').addClass('odd');
        </script>
    </form>
</body>
</html>
